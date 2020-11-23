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
	public class Pickshot : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun trollmimic = ETGMod.Databases.Items.NewGun("Pickshot", "pickshot");
			Game.Items.Rename("outdated_gun_mods:pickshot", "bny:pickshot");
			trollmimic.gameObject.AddComponent<Pickshot>();
			GunExt.SetShortDescription(trollmimic, "Diggy Diggy Hole");
			GunExt.SetLongDescription(trollmimic, "A pickaxe filled to the brim with dwarven souls. Apparently those look like pickaxes.");
			GunExt.SetupSprite(trollmimic, null, "pickshot_idle_001", 11);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.shootAnimation, 18);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.reloadAnimation, 3);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.idleAnimation, 4);
			GunExt.AddProjectileModuleFrom(trollmimic, PickupObjectDatabase.GetById(377) as Gun, true, false);
			trollmimic.gunSwitchGroup = (PickupObjectDatabase.GetById(377) as Gun).gunSwitchGroup;
			trollmimic.DefaultModule.ammoCost = 1;
			trollmimic.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			trollmimic.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			trollmimic.reloadTime = 1.5f;
			trollmimic.DefaultModule.cooldownTime = .15f;
			trollmimic.DefaultModule.numberOfShotsInClip = 15;
			trollmimic.SetBaseMaxAmmo(300);
			trollmimic.quality = PickupObject.ItemQuality.B;
			trollmimic.DefaultModule.angleVariance = 10f;
			trollmimic.DefaultModule.burstShotCount = 1;
			trollmimic.carryPixelOffset += new IntVector2((int)0f, (int)-3f);
			trollmimic.encounterTrackable.EncounterGuid = "I am a dwarf and I'm digging a hole Diggy, diggy hole, diggy, diggy hole I am a dwarf and I'm digging a hole Diggy, diggy hole, digging a hole";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(trollmimic.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			trollmimic.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 12f;
			projectile.baseData.speed = 1f;
			projectile.AdditionalScaleMultiplier = 1.3f;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 2;
			projectile.shouldRotate = true;
			projectile.SetProjectileSpriteRight("pickshot_projectile_001", 13, 7, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(2), null, null, null);
			projectile.pierceMinorBreakables = true;
			projectile.transform.parent = trollmimic.barrelOffset;
			ETGMod.Databases.Items.Add(trollmimic, null, "ANY");
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.baseData.speed = 1f;
			base.StartCoroutine(this.Speed(projectile));
		}
		public IEnumerator Speed(Projectile projectile)
		{
			bool flag = this.gun.CurrentOwner;
			bool flag3 = flag;
			if (flag3)
			{
				yield return new WaitForSeconds(0.2f);
				{
					base.StartCoroutine(this.Speed(projectile));
					projectile.baseData.speed *= 1.8f;
					projectile.UpdateSpeed();
				}
			}
			yield break;
		}
		public override void OnPostFired(PlayerController player, Gun trollmimic)
		{
			gun.PreventNormalFireAudio = false;
		}
	}
}