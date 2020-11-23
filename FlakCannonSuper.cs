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
	public class SuperFlakCannon : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun superflakcannon = ETGMod.Databases.Items.NewGun("Super Flak Cannon", "superflakcannon");
			Game.Items.Rename("outdated_gun_mods:super_flak_cannon", "bny:super_flak_cannon");
			superflakcannon.gameObject.AddComponent<SuperFlakCannon>();
			GunExt.SetShortDescription(superflakcannon, "Complete Overkill");
			GunExt.SetLongDescription(superflakcannon, "A flak cannon that has been buffed up so badly you should probably be careful firing it.");
			GunExt.SetupSprite(superflakcannon, null, "superflakcannon_idle_001", 11);
			GunExt.SetAnimationFPS(superflakcannon, superflakcannon.shootAnimation, 15);
			GunExt.SetAnimationFPS(superflakcannon, superflakcannon.reloadAnimation, 12);
			GunExt.SetAnimationFPS(superflakcannon, superflakcannon.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(superflakcannon, "magnum", true, false);
			superflakcannon.DefaultModule.ammoCost = 1;
			superflakcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			superflakcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			superflakcannon.reloadTime = 3.8f;
			superflakcannon.DefaultModule.cooldownTime = .4f;
			superflakcannon.DefaultModule.numberOfShotsInClip = 1;
			superflakcannon.SetBaseMaxAmmo(30);
			superflakcannon.quality = PickupObject.ItemQuality.A;
			superflakcannon.DefaultModule.angleVariance = 5f;
			superflakcannon.DefaultModule.burstShotCount = 1;
			superflakcannon.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(superflakcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			superflakcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 50f;
			projectile.baseData.speed *= 1.5f;
			projectile.AdditionalScaleMultiplier = 2.75f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.baseData.range = 6.2f;
			BounceProjModifier bouncy = projectile.gameObject.AddComponent<BounceProjModifier>();
			bouncy.numberOfBounces = 1;
			superflakcannon.encounterTrackable.EncounterGuid = "THE BIG FLAK BIG GUN OH YEAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH";
			ETGMod.Databases.Items.Add(superflakcannon, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun superflakcannon)
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

		public override void OnReloadPressed(PlayerController player, Gun superflakcannon, bool bSOMETHING)
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
			projectile.OnDestruction += this.FlakTime1;
		}
		private void FlakTime1(Projectile projectile)
		{
			for (int counter = 0; counter < UnityEngine.Random.Range(3f, 5f); counter++)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[38]).DefaultModule.projectiles[0];
				GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
				Projectile bigboy = gameObject.GetComponent<Projectile>();
				bool flag = bigboy != null;
				bool flag2 = flag;
				if (flag2)
				{
					bigboy.SpawnedFromOtherPlayerProjectile = true;
					bigboy.Shooter = this.gun.CurrentOwner.specRigidbody;
					bigboy.Owner = playerController;
					bigboy.Shooter = playerController.specRigidbody;
					bigboy.baseData.speed = 11.5f;
					bigboy.baseData.damage = 13f;
					bigboy.AdditionalScaleMultiplier = 1.5f;
					bigboy.baseData.range = 5f;
					bigboy.baseData.range = 3f;
					BounceProjModifier bouncy = bigboy.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					bigboy.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					bigboy.ignoreDamageCaps = true;
					bigboy.OnDestruction += this.FlakTime2;
				}
			}
		}

		private void FlakTime2(Projectile projectile)
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
					component.baseData.damage = 8f;
					component.AdditionalScaleMultiplier = 0.8f;
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					component.baseData.range = 8f;
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
					component.baseData.damage = 11f;
					component.AdditionalScaleMultiplier = 1.1f;
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
					spook.penetration = 1;
					component.baseData.range = 8f;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
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
					component.baseData.damage = 16f;
					component.AdditionalScaleMultiplier = 1.65f;
					BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
					bouncy.numberOfBounces = 2;
					PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
					spook.penetration = 1;
					component.baseData.range = 8f;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
				}
			}
		}
	}
}