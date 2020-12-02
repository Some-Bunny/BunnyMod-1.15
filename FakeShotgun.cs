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
	public class FakeShotgun : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Fake Shotgun", "fakeshotgun");
			Game.Items.Rename("outdated_gun_mods:fake_shotgun", "bny:fake_shotgun");
			gun.gameObject.AddComponent<FakeShotgun>();
			GunExt.SetShortDescription(gun, "What the hell..?");
			GunExt.SetLongDescription(gun, "The 'shotgun' of a long-time out of business gun manufacturer, who never actually knew how to make a shotgun, so they grabbed an assault weapon, made it look like a shotgun, and made it horrifically inaccurate.");
			GunExt.SetupSprite(gun, null, "fakeshotgun_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 45);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 21);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(231) as Gun, true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.6f;
			gun.DefaultModule.cooldownTime = 0.05f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 10;
			gun.SetBaseMaxAmmo(600);
			gun.DefaultModule.angleVariance = 15f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(93) as Gun).muzzleFlashEffects;
			projectile.baseData.damage *= .6f;
			projectile.baseData.speed *= 1.1f;
			projectile.baseData.range = 6.4f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			//projectile.FreezeApplyChance = 1f;
			//projectile.AppliesFreeze = true;
			//projectile.freezeEffect = FakeShotgun.ShatteredEffect;
			gun.quality = PickupObject.ItemQuality.C;
			gun.encounterTrackable.EncounterGuid = "stupid fucking shotgun";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D693 File Offset: 0x0000B893
		public override void OnPostFired(PlayerController player, Gun gun)
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

		public override void OnReloadPressed(PlayerController player, Gun railsgun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_shotgun_reload", gameObject);
			}
		}
		private static GameActorFreezeEffect ShatteredEffect = new GameActorFreezeEffect
		{
			TintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(0.7f),
			DeathTintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(0.7f),
		    AppliesTint = true,
			AppliesDeathTint = true,
			effectIdentifier = "Shatter",
			FreezeAmount = 125f,
			UnfreezeDamagePercent = 0f,
			crystalNum = 0,
			crystalRot = 0,
			crystalVariation = new Vector2(0.05f, 0.05f),
			debrisMinForce = 5,
			debrisMaxForce = 5,
			debrisAngleVariance = 15f,
			OverheadVFX = ShatterEffect.ShatterVFXObject,
		};
		public static GameObject ShatterVFXObject;
	}
}
