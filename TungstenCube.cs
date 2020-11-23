using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;
using MonoMod;

namespace BunnyMod
{
	// Token: 0x0200002D RID: 45
	public class TungstenCube : AdvancedGunBehavior
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Cube Of Tungsten", "tungstencube");
			Game.Items.Rename("outdated_gun_mods:cube_of_tungsten", "bny:cube_of_tungsten");
			gun.gameObject.AddComponent<TungstenCube>();
			GunExt.SetShortDescription(gun, "THE CUBE");
			GunExt.SetLongDescription(gun, "A large cube of tungsten, perfect for chucking at your enemies!");
			GunExt.SetupSprite(gun, null, "tungstencube_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 9);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 6);
			GunExt.SetAnimationFPS(gun, gun.chargeAnimation, 4);
			gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(86) as Gun, true, false);
			//gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(384) as Gun).muzzleFlashEffects;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.9f;
			gun.DefaultModule.cooldownTime = 1f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(200);

			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).loopStart = 3;

			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 30f;
			projectile.baseData.speed *= 1.3f;
			projectile.baseData.range *= 100f;
			projectile.SetProjectileSpriteRight("tungstencube_projectile_001", 17, 18, true, tk2dBaseSprite.Anchor.MiddleCenter, 15, 16);
			projectile.shouldRotate = true;
			ProjectileModule.ChargeProjectile chargeProj = new ProjectileModule.ChargeProjectile
			{
				Projectile = projectile,
				ChargeTime = 0.9f,
			};
			gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile> { chargeProj };

			gun.quality = PickupObject.ItemQuality.C;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			this.preventNormalFireAudio = true;
			base.OnPostFired(player, gun);
			AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
			//gun.PreventNormalFireAudio = true;

			//AkSoundEngine.PostEvent("Play_PET_dog_bark_02", gameObject);
		}
		public override void OnReload(PlayerController player, Gun gun)
		{
			this.preventNormalReloadAudio = true;
			base.OnReload(player, gun);
			AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);

		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			AkSoundEngine.PostEvent("Play_OBJ_item_throw_01", gameObject);
			projectile.OnDestruction += this.bop;
		}
		private void bop(Projectile projectile)
		{
			AkSoundEngine.PostEvent("Play_WPN_metalbullet_impact_01", gameObject);
		}
		//private bool HasReloaded;
	}
}


