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
	public class ArtemissileRocket : AdvancedGunBehavior
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Artemis", "artemissile");
			Game.Items.Rename("outdated_gun_mods:artemis", "bny:artemis");
			gun.gameObject.AddComponent<ArtemissileRocket>();
			GunExt.SetShortDescription(gun, "Not To Be Confused With Leto");
			GunExt.SetLongDescription(gun, "A large ship-mounted missile launcher. Originally fitted with an auto-charger, the user now has to charge it manually for it to fire.");
			GunExt.SetupSprite(gun, null, "artemissile_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 2);
			GunExt.SetAnimationFPS(gun, gun.chargeAnimation, 1);
			gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(86) as Gun, true, false);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(384) as Gun).muzzleFlashEffects;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 2.2f;
			gun.DefaultModule.cooldownTime = 1f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(60);

			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).loopStart = 4;

			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 70f;
			projectile.baseData.speed *= 1.8f;
			projectile.baseData.range *= 100f;
			projectile.SetProjectileSpriteRight("artemissile_projectile_001", 17, 6, true, tk2dBaseSprite.Anchor.MiddleCenter, 15, 4);
			projectile.shouldRotate = true;
			ProjectileModule.ChargeProjectile chargeProj = new ProjectileModule.ChargeProjectile
			{
				Projectile = projectile,
				ChargeTime = 4f,
			};
			gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile> { chargeProj };

			gun.quality = PickupObject.ItemQuality.B;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}

		private bool HasReloaded;

		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			AkSoundEngine.PostEvent("Play_WPN_comm4nd0_shot_01", gameObject);
			projectile.OnDestruction += this.kerboomer;
		}

		private void kerboomer(Projectile projectile)
		{
			Exploder.DoDefaultExplosion(projectile.specRigidbody.UnitTopCenter, default(Vector2), null, false, CoreDamageTypes.None, true);
		}
		public void Blam(Vector3 position)
		{
			AkSoundEngine.PostEvent("Play_OBJ_nuke_blast_01", gameObject);
			ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 4f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 40f,
			doExplosionRing = true,
			doDestroyProjectiles = true,
			doForce = true,
			debrisForce = 50f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = true,
			playDefaultSFX = true,
		};
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_rpg_reload_01", gameObject);
			}
		}
	}
}
