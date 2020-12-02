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
	public class FlakCannon : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun flakcannon = ETGMod.Databases.Items.NewGun("Flak Cannon", "flakcannon");
			Game.Items.Rename("outdated_gun_mods:flak_cannon", "bny:flak_cannon");
			flakcannon.gameObject.AddComponent<FlakCannon>();
			GunExt.SetShortDescription(flakcannon, "Bullets in Bullets");
			GunExt.SetLongDescription(flakcannon, "A cannon that was made to shoot large shells with smaller bullets. Younger gundead use these as firework guns.");
			GunExt.SetupSprite(flakcannon, null, "flakcannon_idle_001", 11);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.shootAnimation, 15);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.reloadAnimation, 12);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(flakcannon, "magnum", true, false);
			flakcannon.DefaultModule.ammoCost = 1;
			flakcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			flakcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			flakcannon.reloadTime = 1.8f;
			flakcannon.DefaultModule.cooldownTime = .2f;
			flakcannon.DefaultModule.numberOfShotsInClip = 1;
			flakcannon.SetBaseMaxAmmo(80);
			flakcannon.quality = PickupObject.ItemQuality.C;
			flakcannon.DefaultModule.angleVariance = 10f;
			flakcannon.DefaultModule.burstShotCount = 1;
			flakcannon.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(flakcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			flakcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 30f;
			projectile.baseData.speed *= 1.5f;
			projectile.AdditionalScaleMultiplier = 1.75f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.baseData.range = 5.8f;
			BounceProjModifier bouncy = projectile.gameObject.AddComponent<BounceProjModifier>();
			bouncy.numberOfBounces = 1;
			flakcannon.encounterTrackable.EncounterGuid = "flakcannonyeah";
			ETGMod.Databases.Items.Add(flakcannon, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun flakcannon)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_golddoublebarrelshotgun_shot_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun flakcannon, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_shotgun_reload", gameObject);
			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnDestruction += this.FlakTime;
		}
		//for the love of god quarantine this gun

		private void FlakTime(Projectile projectile)
		{
			PlayerController playerController1 = this.gun.CurrentOwner as PlayerController;
			for (int counter = 0; counter < UnityEngine.Random.Range(2f, 12f); counter++)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[38]).DefaultModule.projectiles[0];
				GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
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
					component.baseData.damage = 5f;
					component.baseData.range = 5f;
					component.AdditionalScaleMultiplier = 0.8f;
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
				}
			}
			for (int counter = 0; counter < UnityEngine.Random.Range(4f, 10f); counter++)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[38]).DefaultModule.projectiles[0];
				GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
				Projectile component = gameObject.GetComponent<Projectile>();
				bool flag = component != null;
				bool flag2 = flag;
				if (flag2)
				{
					component.SpawnedFromOtherPlayerProjectile = true;
					component.Shooter = this.gun.CurrentOwner.specRigidbody;
					component.Owner = playerController;
					component.Shooter = playerController.specRigidbody;
					component.baseData.speed = 14.5f;
					component.baseData.damage = 8f;
					component.AdditionalScaleMultiplier = 1.1f;
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
					spook.penetration = 1;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
					component.baseData.range = 4f;

				}
			}
			for (int counter = 0; counter < UnityEngine.Random.Range(6f, 8f); counter++)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[38]).DefaultModule.projectiles[0];
				GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
				Projectile component = gameObject.GetComponent<Projectile>();
				bool flag = component != null;
				bool flag2 = flag;
				if (flag2)
				{
					component.SpawnedFromOtherPlayerProjectile = true;
					component.Shooter = this.gun.CurrentOwner.specRigidbody;
					component.Owner = playerController;
					component.Shooter = playerController.specRigidbody;
					component.baseData.speed = 17.0f;
					component.baseData.damage = 11.5f;
					component.AdditionalScaleMultiplier = 1.65f;
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
					spook.penetration = 1;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
					component.baseData.range = 6f;
				}
			}
		}
	}
}