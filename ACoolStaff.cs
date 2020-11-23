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
	public class CoolStaff : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun staffyeah = ETGMod.Databases.Items.NewGun("Cool Staff", "coolstaff");
			Game.Items.Rename("outdated_gun_mods:cool_staff", "bny:cool_staff");
			staffyeah.gameObject.AddComponent<CoolStaff>();
			GunExt.SetShortDescription(staffyeah, "Wielded by cool Wardens");
			GunExt.SetLongDescription(staffyeah, "A cool staff found within the pockets of these robes. A Pure weapon, to be sure.");
			GunExt.SetupSprite(staffyeah, null, "coolstaff_idle_001", 11);
			GunExt.SetAnimationFPS(staffyeah, staffyeah.shootAnimation, 15);
			GunExt.SetAnimationFPS(staffyeah, staffyeah.reloadAnimation, 12);
			GunExt.SetAnimationFPS(staffyeah, staffyeah.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(staffyeah, PickupObjectDatabase.GetById(61) as Gun, true, false);
			staffyeah.DefaultModule.ammoCost = 1;
			staffyeah.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			staffyeah.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			staffyeah.reloadTime = 2.1f;
			staffyeah.DefaultModule.numberOfShotsInClip = 10;
			staffyeah.SetBaseMaxAmmo(150);
			staffyeah.quality = PickupObject.ItemQuality.SPECIAL;
			staffyeah.DefaultModule.angleVariance = 1f;
			staffyeah.DefaultModule.burstShotCount = 1;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(staffyeah.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			staffyeah.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 1.4f;
			projectile.baseData.speed *= 1.1f;
			projectile.AdditionalScaleMultiplier = 1.5f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 1;
			CoolStaffHandler staffHandler = projectile.gameObject.AddComponent<CoolStaffHandler>();
			staffHandler.projectileToSpawn = ((Gun)ETGMod.Databases.Items["pitchfork"]).DefaultModule.projectiles[0];
			projectile.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 0f);
			staffyeah.encounterTrackable.EncounterGuid = "coolstaff";
			ETGMod.Databases.Items.Add(staffyeah, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun staffyeah)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_wandbundle_shot_01", gameObject);
		}
		private bool HasReloaded;

		protected void Update()
		{
			if (gun.CurrentOwner)
			{

				if (!gun.PreventNormalFireAudio)
				{
					this.gun.PreventNormalFireAudio = true;
				}
				if (!gun.IsReloading && !HasReloaded)
				{
					this.HasReloaded = true;
				}
			}
		}

		public override void OnReloadPressed(PlayerController player, Gun staffyeah, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_blackhole_impact_01", gameObject);
			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnDestruction += this.staff1;
			projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.staff2));
			base.PostProcessProjectile(projectile);
		}
		private void staff1(Projectile projectile)
		{
			for (int counter = 0; counter < UnityEngine.Random.Range(1f, 2f); counter++)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[52]).DefaultModule.projectiles[0];
				GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
				Projectile component = gameObject.GetComponent<Projectile>();
				bool flag = component != null;
				bool flag2 = flag;
				if (flag2)
				{
					component.SpawnedFromOtherPlayerProjectile = true;
					component.Shooter = this.gun.CurrentOwner.specRigidbody;
					component.Owner = playerController;
					component.Shooter = playerController.specRigidbody;
					component.baseData.speed = 30.5f;
					component.baseData.damage = 2.1f;
					component.AdditionalScaleMultiplier = 1.25f;
					component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 0f);
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 3;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
				}
			}
			
		}
		private void staff2(Projectile sourceProjectile, SpeculativeRigidbody hitRigidbody, bool fatal)
		{
			bool flag1 = fatal;
			if (flag1)
            {
				PlayerController playerController1 = this.gun.CurrentOwner as PlayerController;
				for (int counter = 0; counter < UnityEngine.Random.Range(2f, 4f); counter++)
				{
					PlayerController playerController = this.gun.CurrentOwner as PlayerController;
					Projectile projectile1 = ((Gun)ETGMod.Databases.Items[385]).DefaultModule.projectiles[0];
					GameObject gameObject = SpawnManager.SpawnProjectile(sourceProjectile.gameObject, sourceProjectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
					Projectile component = gameObject.GetComponent<Projectile>();
					bool flag = component != null;
					bool flag2 = flag;
					if (flag2)
					{
						component.SpawnedFromOtherPlayerProjectile = true;
						component.Shooter = this.gun.CurrentOwner.specRigidbody;
						component.Owner = playerController;
						component.Shooter = playerController.specRigidbody;
						component.baseData.speed = 11.5f;
						component.baseData.damage = .7f;
						component.AdditionalScaleMultiplier = 0.5f;
						component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 0f);
						HomingModifier homing = sourceProjectile.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 60f;
						homing.AngularVelocity = 300;
						component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
						component.ignoreDamageCaps = true;
					}
				}
				for (int counter = 0; counter < UnityEngine.Random.Range(1f, 3f); counter++)
				{
					PlayerController playerController = this.gun.CurrentOwner as PlayerController;
					Projectile projectile1 = ((Gun)ETGMod.Databases.Items[377]).DefaultModule.projectiles[0];
					GameObject gameObject = SpawnManager.SpawnProjectile(sourceProjectile.gameObject, sourceProjectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
					Projectile component = gameObject.GetComponent<Projectile>();
					bool flag = component != null;
					bool flag2 = flag;
					if (flag2)
					{
						component.SpawnedFromOtherPlayerProjectile = true;
						component.Shooter = this.gun.CurrentOwner.specRigidbody;
						component.Owner = playerController;
						component.Shooter = playerController.specRigidbody;
						component.baseData.speed = 40.5f;
						component.baseData.damage = 1.4f;
						component.AdditionalScaleMultiplier = 1.1f;
						component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 0f);
						BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
						bouncy.numberOfBounces = 4;
						component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
						component.ignoreDamageCaps = true;
					}
				}
			}
		}
	}
}