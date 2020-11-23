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
	public class ChaosRevolverSynergyForme : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("True Revolver", "truerevolver");
			Game.Items.Rename("outdated_gun_mods:true_revolver", "bny:true_revolver");
			gun.gameObject.AddComponent<ChaosRevolver>();
			GunExt.SetShortDescription(gun, "Deus Ex Machina");
			GunExt.SetLongDescription(gun, "It begins.");
			GunExt.SetupSprite(gun, null, "truerevolver_idle_001", 25);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 14);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 8);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 5);
			GunExt.AddProjectileModuleFrom(gun, "ak-47", true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.4f;
			gun.DefaultModule.cooldownTime = .166f;
			gun.DefaultModule.numberOfShotsInClip = 6;
			gun.SetBaseMaxAmmo(150);
			gun.quality = PickupObject.ItemQuality.EXCLUDED;
			gun.DefaultModule.angleVariance = 10f;
			gun.DefaultModule.burstShotCount = 1;
			gun.encounterTrackable.EncounterGuid = "truegun";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 25f;
			projectile.baseData.speed *= 1.3f;
			projectile.transform.parent = gun.barrelOffset;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 50;
			projectile.SetProjectileSpriteRight("chaosrevolver_projectile_001", 10, 10, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(7), new int?(7), null, null, null);
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			ChaosRevolverSynergyForme.ChaosRevolverSynergyFormeID = gun.PickupObjectId;
	}

	public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_dragun_shot_01", gameObject);
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
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
			}
		}
		public static int ChaosRevolverSynergyFormeID;
	}
}