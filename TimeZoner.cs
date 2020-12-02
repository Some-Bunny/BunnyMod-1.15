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
	public class TimeZoner : GunBehaviour
	{
		public static void Add()
		{
			Gun boomrevolver = ETGMod.Databases.Items.NewGun("Time-Zoner", "timezoner");
			Game.Items.Rename("outdated_gun_mods:time-zoner", "bny:time-zoner");
			boomrevolver.gameObject.AddComponent<TimeZoner>();
			GunExt.SetShortDescription(boomrevolver, "Time travel, Yeah!");
			GunExt.SetLongDescription(boomrevolver, "A test-version revolver made for spies to adapt to sudden time-zone switches. On its first experiment, it slipped through a crack in time and ended up here.");
			GunExt.SetupSprite(boomrevolver, null, "timezoner_idle_001", 19);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.shootAnimation, 24);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.reloadAnimation, 10);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.idleAnimation, 5);
			GunExt.AddProjectileModuleFrom(boomrevolver, "magnum", true, false);
			boomrevolver.gunSwitchGroup = (PickupObjectDatabase.GetById(577) as Gun).gunSwitchGroup;
			boomrevolver.DefaultModule.ammoCost = 1;
			boomrevolver.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			boomrevolver.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			boomrevolver.reloadTime = 2.2f;
			boomrevolver.DefaultModule.cooldownTime = .2f;
			boomrevolver.DefaultModule.numberOfShotsInClip = 20;
			boomrevolver.SetBaseMaxAmmo(400);
			boomrevolver.quality = PickupObject.ItemQuality.C;
			boomrevolver.DefaultModule.angleVariance = 3f;
			boomrevolver.encounterTrackable.EncounterGuid = "ZA WARUDO *BFPPFPFPFPFPFPFPFPFPFPFPFPT T TT T T *";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(boomrevolver.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			projectile.SetProjectileSpriteRight("timezoner_projectile_001", 11, 5, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(2), null, null, null);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			boomrevolver.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage = 7f;
			projectile.baseData.speed *= 1.2f;
			projectile.AdditionalScaleMultiplier = 1.2f;
			projectile.transform.parent = boomrevolver.barrelOffset;
			ETGMod.Databases.Items.Add(boomrevolver, null, "ANY");
			boomrevolver.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
			boomrevolver.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
		}

		private bool HasReloaded;

		protected void Update()
		{
			bool flag = this.gun.CurrentOwner;
			if (flag)
			{
				bool flag2 = !this.gun.IsReloading && !this.HasReloaded;
				if (flag2)
				{
					this.HasReloaded = true;
				}
			}
		}

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			bool flag = this.HasReloaded && gun.ClipShotsRemaining == 0;
			if (flag)
			{
				this.HasReloaded = false;
				bool flagA = !TimeZoner.onCooldown;
				bool flag2 = flagA;
				if (flag2)
				{
					TimeZoner.onCooldown = true;
					GameManager.Instance.StartCoroutine(TimeZoner.StartCooldown());
					this.activateSlow(player);
				}
			}
			base.OnReloadPressed(player, gun, bSOMETHING);
		}
		protected void activateSlow(PlayerController user)
		{
			new RadialSlowInterface
			{
				DoesSepia = true,
				RadialSlowHoldTime = 1.5f,
				RadialSlowTimeModifier = 0.25f
			}.DoRadialSlow(user.specRigidbody.UnitCenter, user.CurrentRoom);
		}
		private static IEnumerator StartCooldown()
		{
			yield return new WaitForSeconds(1.5f);
			TimeZoner.onCooldown = false;
			yield break;
		}
		private static bool onCooldown;
	}
}