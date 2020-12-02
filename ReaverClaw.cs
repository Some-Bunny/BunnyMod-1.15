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
	public class ReaverClaw : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Reaver Claw", "reaverclaw");
			Game.Items.Rename("outdated_gun_mods:reaver_claw", "bny:reaver_claw");
			gun.gameObject.AddComponent<ReaverClaw>();
			GunExt.SetShortDescription(gun, "A Rifle Eventually Reloads");
			GunExt.SetLongDescription(gun, "The foot ripped from a Void Reaver. Though detached, it's powers still flow in from the Void.");
			GunExt.SetupSprite(gun, null, "reaverclaw_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 18);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 3);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 4);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(35) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(377) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Burst;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 2.3f;
			gun.DefaultModule.burstShotCount = 2;
			gun.DefaultModule.burstCooldownTime = 0.2f;
			gun.DefaultModule.cooldownTime = .6f;
			gun.DefaultModule.numberOfShotsInClip = 4;
			gun.SetBaseMaxAmmo(120);
			gun.quality = PickupObject.ItemQuality.A;
			gun.DefaultModule.angleVariance = 7f;
			gun.DefaultModule.burstShotCount = 1;
			gun.encounterTrackable.EncounterGuid = "And don't you listen to the Song Of Life";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			ReaveOnReloadModifier reaveonReloadModifier = gun.gameObject.AddComponent<ReaveOnReloadModifier>();
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 8f;
			projectile.baseData.speed = 18f;
			projectile.baseData.range = 8f;
			projectile.SetProjectileSpriteRight("reaverclaw_projectile_002", 10, 10, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(2), null, null, null);
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 1;
			projectile.baseData.force = 0f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			gun.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);

		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.SpeedApplyChance = 1f;
			projectile.AppliesSpeedModifier = true;
			projectile.speedEffect = (GameActorSpeedEffect)ReaverClaw.DetainEffect;
		}

		public override void OnPostFired(PlayerController owner, Gun gun)
		{
			AkSoundEngine.PostEvent("Play_wpn_kthulu_soul_01", base.gameObject);
			gun.PreventNormalFireAudio = true;
		}

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_BOSS_doormimic_vanish_01", base.gameObject);
			}
		}
		public bool HasReloaded;
		public static PlayerController owner;
		public static GameActorSpeedEffect DetainEffect = new GameActorSpeedEffect
		{
			AffectsPlayers = false,
			duration = 6f,
			AppliesTint = true,
			SpeedMultiplier = 0f,
		    TintColor = new Color(0.3f, 0f, 0.3f).WithAlpha(0.9f)
	    };
	}
}
