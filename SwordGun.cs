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
	public class ASwordGun : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Sword", "sword");
			Game.Items.Rename("outdated_gun_mods:sword", "bny:sword");
			gun.gameObject.AddComponent<ASwordGun>();
			GunExt.SetShortDescription(gun, "Nothing Unusual");
			GunExt.SetLongDescription(gun, "This sword is completely ordinary. Just a normal sword, I promise you.");
			GunExt.SetupSprite(gun, null, "sword_idle_001", 26);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 14);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, "excaliber", true, false);
			gun.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 2.2f;
			gun.DefaultModule.cooldownTime = 0.15f;
			gun.DefaultModule.numberOfShotsInClip = 60;
			gun.SetBaseMaxAmmo(600);
			gun.quality = PickupObject.ItemQuality.B;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(377) as Gun).muzzleFlashEffects;
			gun.encounterTrackable.EncounterGuid = "you fool, you absolute buffoon. you thought this was a SWORD? HAHA YOU FOOL! ITS a GUN!!!!";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage *= 0.90f;
			projectile.baseData.speed *= 0.7f;
			projectile.baseData.force = 1.5f;
			projectile.SetProjectileSpriteRight("sword_projectile_001", 16, 7, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(8), new int?(4), null, null, null);
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_blasphemy_shot_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
			}
		}
	}
}