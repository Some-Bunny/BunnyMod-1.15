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
	public class TrainGun : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun railsgun = ETGMod.Databases.Items.NewGun("Rail Gun", "railgunn");
			Game.Items.Rename("outdated_gun_mods:rail_gun", "bny:rail_gun");
			railsgun.gameObject.AddComponent<TrainGun>();
			GunExt.SetShortDescription(railsgun, "Here comes the Pain Train");
			GunExt.SetLongDescription(railsgun, "A piece of railway that was haphazardly welded to the butt of a gun.\n\nChoo Choo!");
			GunExt.SetupSprite(railsgun, null, "railgunn_idle_001", 11);
			GunExt.SetAnimationFPS(railsgun, railsgun.shootAnimation, 14);
			GunExt.SetAnimationFPS(railsgun, railsgun.reloadAnimation, 5);
			GunExt.SetAnimationFPS(railsgun, railsgun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(railsgun, "ak-47", true, false);
			railsgun.DefaultModule.ammoCost = 1;
			railsgun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			railsgun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			railsgun.reloadTime = 2.8f;
			railsgun.DefaultModule.cooldownTime = .2f;
			railsgun.DefaultModule.numberOfShotsInClip = 1;
			railsgun.SetBaseMaxAmmo(50);
			railsgun.quality = PickupObject.ItemQuality.C;
			railsgun.DefaultModule.angleVariance = 5f;
			railsgun.DefaultModule.burstShotCount = 1;
			railsgun.encounterTrackable.EncounterGuid = "the_rail_gun_trains";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(railsgun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			railsgun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 22.5f;
			projectile.baseData.speed *= 0.4f;
			projectile.AdditionalScaleMultiplier = 1.1f;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 10;
			BounceProjModifier bouncy = projectile.gameObject.AddComponent<BounceProjModifier>();
			bouncy.numberOfBounces = 8;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			RailsGunHandler railsHandler = projectile.gameObject.AddComponent<RailsGunHandler>();
			railsHandler.projectileToSpawn = ((Gun)ETGMod.Databases.Items["phoenix"]).DefaultModule.projectiles[0];
			projectile.SetProjectileSpriteRight("railgunn_projectile_001", 22, 11, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(11), new int?(5), null, null, null);
			projectile.transform.parent = railsgun.barrelOffset;
			railsgun.encounterTrackable.EncounterGuid = "the train gun please WORK";
			ETGMod.Databases.Items.Add(railsgun, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun railsgun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_ENM_smiley_whistle_01", gameObject);
			AkSoundEngine.PostEvent("Play_WPN_blunderbuss_shot_01", gameObject);
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
	}
}