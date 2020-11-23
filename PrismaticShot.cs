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
	public class PrismaticShot : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Prismatic Shot", "prismaticshot");
			Game.Items.Rename("outdated_gun_mods:prismatic_shot", "bny:prismatic_shot");
			gun.gameObject.AddComponent<PrismaticShot>();
			GunExt.SetShortDescription(gun, "Rainguns!");
			GunExt.SetLongDescription(gun, "A gun made from pure, compressed crystals. Although weak in firepower, it makes up for it with the pretty colours it shoots.");
			GunExt.SetupSprite(gun, null, "prismaticshot_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 16);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 16);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(56) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(199) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.burstShotCount = 7;
			gun.DefaultModule.burstCooldownTime = 0.05f;
			gun.barrelOffset.transform.localPosition = new Vector3(2f, 0.4375f, 0f);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Burst;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0.8f;
			gun.DefaultModule.cooldownTime = 0.3f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 70;
			gun.SetBaseMaxAmmo(700);
			gun.DefaultModule.angleVariance = 1f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(199) as Gun).muzzleFlashEffects;
			projectile.baseData.damage = 4f;
			projectile.baseData.speed *= 0.9f;
			projectile.AdditionalScaleMultiplier = 0.4f;
			projectile.baseData.force = 3f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.SetProjectileSpriteRight("prismaticshot_projectile_001", 10, 10, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(2), null, null, null);
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			//projectile.DefaultTintColor = new Color(50f, 0f, 0f).WithAlpha(2f);
			projectile.HasDefaultTint = true;
			gun.quality = PickupObject.ItemQuality.C;
			gun.encounterTrackable.EncounterGuid = "Ok, this gun is now officially an ad for Prismatism.";
			ETGMod.Databases.Items.Add(gun, null, "ANY");

		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			int num3 = UnityEngine.Random.Range(0, 7);
			bool red = num3 == 0;
			if (red)
			{
				projectile.DefaultTintColor = new Color32(255, 0, 0, 255);
			}
			bool orange = num3 == 1;
			if (orange)
			{
				projectile.DefaultTintColor = new Color32(255, 69, 0, 255);
			}
			bool yellow = num3 == 2;
			if (yellow)
			{
				projectile.DefaultTintColor = new Color32(255, 255, 0, 255);
			}
			bool green = num3 == 3;
			if (green)
			{
				projectile.DefaultTintColor = new Color32(0, 255, 0, 255);
			}
			bool cyan = num3 == 4;
			if (cyan)
			{
				projectile.DefaultTintColor = new Color32(0, 255, 127, 255);
			}
			bool blue = num3 == 5;
			if (blue)
			{
				projectile.DefaultTintColor = new Color32(0, 255, 255, 255);
			}
			bool purple = num3 == 6;
			if (purple)
			{
				projectile.DefaultTintColor = new Color32(128, 0, 128, 255);
			}
		}

	}
}
