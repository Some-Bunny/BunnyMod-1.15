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
	public class Starbounder : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun trollmimic = ETGMod.Databases.Items.NewGun("Starbounder", "starbounder");
			Game.Items.Rename("outdated_gun_mods:starbounder", "bny:starbounder");
			trollmimic.gameObject.AddComponent<Starbounder>();
			GunExt.SetShortDescription(trollmimic, "Unstable");
			GunExt.SetLongDescription(trollmimic, "Don't expect this thing to fire normally. Don't.");
			GunExt.SetupSprite(trollmimic, null, "starbounder_idle_001", 11);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.shootAnimation, 18);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.reloadAnimation, 3);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.idleAnimation, 4);
			GunExt.AddProjectileModuleFrom(trollmimic, PickupObjectDatabase.GetById(38) as Gun, true, false);
			trollmimic.gunSwitchGroup = (PickupObjectDatabase.GetById(38) as Gun).gunSwitchGroup;
			trollmimic.DefaultModule.ammoCost = 1;
			trollmimic.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			trollmimic.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			trollmimic.reloadTime = 1.9f;
			trollmimic.DefaultModule.cooldownTime = .125f;
			trollmimic.DefaultModule.numberOfShotsInClip = 40;
			trollmimic.SetBaseMaxAmmo(320);
			trollmimic.quality = PickupObject.ItemQuality.D;
			trollmimic.DefaultModule.angleVariance = 15f;
			trollmimic.DefaultModule.burstShotCount = 1;
			trollmimic.encounterTrackable.EncounterGuid = "uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuhgasdhgjd";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(trollmimic.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			trollmimic.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 10f;
			projectile.baseData.speed = 1f;
			projectile.AdditionalScaleMultiplier = 1f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.transform.parent = trollmimic.barrelOffset;
			ETGMod.Databases.Items.Add(trollmimic, null, "ANY");
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			int num33 = UnityEngine.Random.Range(0, 3);
			bool sadsda = num33 == 0;
			if (sadsda)
			{
				projectile.baseData.damage = UnityEngine.Random.Range(1, 3);
			}
			bool aaaaa = num33 == 1;
			if (aaaaa)
			{
				projectile.baseData.damage = UnityEngine.Random.Range(7, 12);

			}
			bool fldsaaag5 = num33 == 2;
			if (fldsaaag5)
			{
				projectile.baseData.damage = UnityEngine.Random.Range(10, 15);
			}
			int num3 = UnityEngine.Random.Range(0, 3);
			bool fuck = num3 == 0;
			if (fuck)
			{
				base.StartCoroutine(this.Speed(projectile));
				projectile.baseData.speed = (UnityEngine.Random.Range(20, 30));
				projectile.UpdateSpeed();
			}
			bool aaaa = num3 == 1;
			if (aaaa)
			{
				base.StartCoroutine(this.Speed(projectile));
				projectile.baseData.speed = (UnityEngine.Random.Range(5, 15));
				projectile.UpdateSpeed();
			}
			bool fldsaag5 = num3 == 2;
			if (fldsaag5)
			{
				base.StartCoroutine(this.Speed(projectile));
				projectile.baseData.speed = 0f;
				projectile.UpdateSpeed();
			}
		}
		public IEnumerator Speed(Projectile projectile)
		{
			bool flag = this.gun.CurrentOwner;
			bool flag3 = flag;
			if (flag3)
			{
				yield return new WaitForSeconds(UnityEngine.Random.Range(.1f, 1.2f));
				{
					int num3 = UnityEngine.Random.Range(0, 3);
					bool fuck = num3 == 0;
					if (fuck)
					{
						base.StartCoroutine(this.Speed(projectile));
						projectile.baseData.speed = (UnityEngine.Random.Range(20, 30));
						projectile.UpdateSpeed();
					}
					bool aaaa = num3 == 1;
					if (aaaa)
					{
						base.StartCoroutine(this.Speed(projectile));
						projectile.baseData.speed = (UnityEngine.Random.Range(5, 15));
						projectile.UpdateSpeed();
					}
					bool fldsaag5 = num3 == 2;
					if (fldsaag5)
					{
						base.StartCoroutine(this.Speed(projectile));
						projectile.baseData.speed = 0f;
						projectile.UpdateSpeed();
					}
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