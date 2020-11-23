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
	public class BulletCaster : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun castergun = ETGMod.Databases.Items.NewGun("Bullet Creator", "bulletcaster");
			Game.Items.Rename("outdated_gun_mods:bullet_creator", "bny:bullet_creator");
			castergun.gameObject.AddComponent<BulletCaster>();
			GunExt.SetShortDescription(castergun, "Creates bullets");
			GunExt.SetLongDescription(castergun, "A novice gunsmith had the idea of a gun that could create bullets. However, they were not able to fire the created bullets, so they just rely on gusts of wind to move.");
			GunExt.SetupSprite(castergun, null, "bulletcaster_idle_001", 25);
			GunExt.SetAnimationFPS(castergun, castergun.shootAnimation, 14);
			GunExt.SetAnimationFPS(castergun, castergun.reloadAnimation, 8);
			GunExt.SetAnimationFPS(castergun, castergun.idleAnimation, 12);
			GunExt.AddProjectileModuleFrom(castergun, "ak-47", true, false);
			castergun.DefaultModule.ammoCost = 1;
			castergun.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			castergun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			castergun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			castergun.reloadTime = 1f;
			castergun.DefaultModule.cooldownTime = .2f;
			castergun.DefaultModule.numberOfShotsInClip = 5;
			castergun.SetBaseMaxAmmo(300);
			castergun.quality = PickupObject.ItemQuality.D;
			castergun.DefaultModule.angleVariance = 0f;
			castergun.DefaultModule.burstShotCount = 1;
			castergun.encounterTrackable.EncounterGuid = "bullet_caster";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(castergun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			castergun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 2f;
			projectile.baseData.speed *= .001f;
			projectile.transform.parent = castergun.barrelOffset;
			projectile.SetProjectileSpriteRight("bulletcaster_projectile_001", 4, 4, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(2), new int?(2), null, null, null);
			ETGMod.Databases.Items.Add(castergun, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun castergun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_ENM_iceslime_blast_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun castergun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_crossbow_reload_01", gameObject);
			}
		}
	}
}