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
	public class AGunSword : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gunsword = ETGMod.Databases.Items.NewGun("Gun", "gun");
			Game.Items.Rename("outdated_gun_mods:gun", "bny:gun");
			gunsword.gameObject.AddComponent<AGunSword>();
			GunExt.SetShortDescription(gunsword, "Nothing Unusual");
			GunExt.SetLongDescription(gunsword, "This gun is completely ordinary. Just a normal gun, I promise you.");
			GunExt.SetupSprite(gunsword, null, "gun_idle_001", 11);
			GunExt.SetAnimationFPS(gunsword, gunsword.shootAnimation, 15);
			GunExt.SetAnimationFPS(gunsword, gunsword.reloadAnimation, 12);
			GunExt.SetAnimationFPS(gunsword, gunsword.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gunsword, "magnum", true, false);
			gunsword.DefaultModule.ammoCost = 1;
			gunsword.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gunsword.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gunsword.reloadTime = 0f;
			gunsword.DefaultModule.cooldownTime = .6f;
			gunsword.DefaultModule.numberOfShotsInClip = 6;
			gunsword.SetBaseMaxAmmo(80);
			gunsword.quality = PickupObject.ItemQuality.C;
			gunsword.DefaultModule.angleVariance = 0f;
			gunsword.DefaultModule.burstShotCount = 1;
			gunsword.InfiniteAmmo = true;
			gunsword.muzzleFlashEffects = (PickupObjectDatabase.GetById(417) as Gun).muzzleFlashEffects;
			gunsword.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			gunsword.IsHeroSword = false;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gunsword.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gunsword.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= .9f;
			projectile.baseData.speed *= 8f;
			projectile.AdditionalScaleMultiplier = 5f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.baseData.range = 1.3f;
			projectile.sprite.renderer.enabled = false;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 10;
			projectile.SetProjectileSpriteRight("gun_projectile_001", 16, 26, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(8), new int?(13), null, null, null);
			gunsword.encounterTrackable.EncounterGuid = "you fool, you absolute buffoon. you thought this was a GUN? HAHA YOU FOOL! ITS a SWORD!!!!";
			ETGMod.Databases.Items.Add(gunsword, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun gunsword)
		{
			AkSoundEngine.PostEvent("Play_WPN_shadesrevolver_shot_01", gameObject);
			gun.PreventNormalFireAudio = true;
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

		public override void OnReloadPressed(PlayerController player, Gun gunsword, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_shotgun_reload", gameObject);
			}
		}
	}
}