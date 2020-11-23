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
	public class StickGun : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun stickgun = ETGMod.Databases.Items.NewGun("Stick Gun", "stickgun");
			Game.Items.Rename("outdated_gun_mods:stick_gun", "bny:stick_gun");
			stickgun.gameObject.AddComponent<StickGun>();
			GunExt.SetShortDescription(stickgun, "Get stick gunned lol");
			GunExt.SetLongDescription(stickgun, "A purely abstract weapon made from 2 intersecting lines. Truly the most confusing weapon here, unless you're 5.");
			GunExt.SetupSprite(stickgun, null, "stickgun_idle_001", 25);
			GunExt.SetAnimationFPS(stickgun, stickgun.shootAnimation, 12);
			GunExt.SetAnimationFPS(stickgun, stickgun.reloadAnimation, 21);
			GunExt.SetAnimationFPS(stickgun, stickgun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(stickgun, "ak-47", true, false);
			stickgun.DefaultModule.ammoCost = 1;
			stickgun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			stickgun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			stickgun.reloadTime = 1.6f;
			stickgun.DefaultModule.cooldownTime = .17f;
			stickgun.DefaultModule.numberOfShotsInClip = 10;
			stickgun.SetBaseMaxAmmo(300);
			stickgun.quality = PickupObject.ItemQuality.D;
			stickgun.DefaultModule.angleVariance = 7f;
			stickgun.DefaultModule.burstShotCount = 1;
			stickgun.encounterTrackable.EncounterGuid = "The *fuckING* Stick Gun";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(stickgun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			stickgun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= .9f;
			projectile.baseData.speed *= 1f;
			projectile.shouldRotate = true;
			projectile.transform.parent = stickgun.barrelOffset;
			projectile.SetProjectileSpriteRight("stickgun_projectile_001", 7, 1, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(3), new int?(1), null, null, null);
			ETGMod.Databases.Items.Add(stickgun, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun stickgun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_nailgun_shot_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun stickgun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_dartgun_reload_01", gameObject);
			}
		}
	}
}