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
	public class ABlasphemimic : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun trollmimic = ETGMod.Databases.Items.NewGun("Blasphemimic", "blasphemimic");
			Game.Items.Rename("outdated_gun_mods:blasphemimic", "bny:blasphemimic");
			trollmimic.gameObject.AddComponent<ABlasphemimic>();
			GunExt.SetShortDescription(trollmimic, "To The Point");
			GunExt.SetLongDescription(trollmimic, "Infinite ammo. Does not reveal secret walls. Betra- hey wait a minute.");
			GunExt.SetupSprite(trollmimic, null, "blasphemimic_idle_001", 11);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.shootAnimation, 42);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.reloadAnimation, 100);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.idleAnimation, 4);
			GunExt.AddProjectileModuleFrom(trollmimic, "pitchfork", true, false);
			trollmimic.DefaultModule.ammoCost = 1;
			trollmimic.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			trollmimic.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			trollmimic.reloadTime = 1.5f;
			trollmimic.DefaultModule.cooldownTime = .16f;
			trollmimic.DefaultModule.numberOfShotsInClip = 40;
			trollmimic.SetBaseMaxAmmo(400);
			trollmimic.quality = PickupObject.ItemQuality.B;
			trollmimic.DefaultModule.angleVariance = 25f;
			trollmimic.DefaultModule.burstShotCount = 1;
			trollmimic.encounterTrackable.EncounterGuid = "i swear to god if one of you fuckers already made this weapon, im gonna shit asnger and piss fury";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(trollmimic.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			trollmimic.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 0.9f;
			projectile.baseData.speed *= 1.1f;
			projectile.AdditionalScaleMultiplier = 0.6f;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 1;
			BounceProjModifier bouncy = projectile.gameObject.AddComponent<BounceProjModifier>();
			bouncy.numberOfBounces = 1;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			MimicBlasphemyHandler trollmimichandler = projectile.gameObject.AddComponent<MimicBlasphemyHandler>();
			trollmimichandler.projectileToSpawn = (PickupObjectDatabase.GetById(207) as Gun).DefaultModule.projectiles[0];
			projectile.transform.parent = trollmimic.barrelOffset;
			ETGMod.Databases.Items.Add(trollmimic, null, "ANY");
			ABlasphemimic.BlashempmicID = trollmimic.PickupObjectId;
		}
		public static int BlashempmicID;

		public override void OnPostFired(PlayerController player, Gun trollmimic)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun trollmimic, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_VO_mimic_awake_01", gameObject);
			}
		}
	}
}