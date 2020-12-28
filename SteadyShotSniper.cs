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
	public class SteadyShotSniper : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Steady Shot Sniper", "steadysniper");
			Game.Items.Rename("outdated_gun_mods:steady_shot_sniper", "bny:steady_shot_sniper");
			gun.gameObject.AddComponent<SteadyShotSniper>();
			GunExt.SetShortDescription(gun, "Ready... Aim...");
			GunExt.SetLongDescription(gun, "A sniper rifle built for the most dedicated campers. Stay still and ensure a great shot.");
			GunExt.SetupSprite(gun, null, "steadysniper_idle_001", 25);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 18);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 4);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(5) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(5) as Gun).gunSwitchGroup;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(5) as Gun).muzzleFlashEffects;
			gun.carryPixelOffset = new IntVector2((int)5.5f, (int)0.125f);
			gun.barrelOffset.transform.localPosition = new Vector3(1.125f, 0.625f, 0f);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 3.2f;
			gun.DefaultModule.cooldownTime = 1.3f;
			gun.DefaultModule.numberOfShotsInClip = 5;
			gun.SetBaseMaxAmmo(100);
			gun.quality = PickupObject.ItemQuality.C;
			gun.DefaultModule.angleVariance = 0f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 20f;
			projectile.baseData.speed *= 5f;
			projectile.shouldRotate = true;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 0;
			projectile.pierceMinorBreakables = true;
			//PlayerController player = (GameManager.Instance.PrimaryPlayer);
			SteadyShotSniper.SniperID = gun.PickupObjectId;
			projectile.transform.position = gun.barrelOffset.transform.localPosition= new Vector3(1.125f, 0.625f, 0f);
			projectile.HasDefaultTint = true;
//			TrailController yomama = projectile.gameObject.AddComponent<TrailController>();

			//projectile.SetProjectileSpriteRight("steadysniper_projectile_001", 4, 4, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(3), new int?(3), null, null, null);
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			List<string> mandatoryConsoleID1s = new List<string>
			{
				"bny:steady_shot_sniper",
			};
			List<string> optionalConsoleID1s = new List<string>
			{
				"scope",
				"bny:micro_scope",
				"laser_sight",
				"muscle_relaxant",
				"scouter"
			};
			CustomSynergies.Add("Cutting Edge Shot", mandatoryConsoleID1s, optionalConsoleID1s, true);
		}
		public bool HasFlagContingentMomentum;
		public static int SniperID;
		protected void Update()
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			if (player.CurrentGun == this.gun)

			{
				bool yeah = player.Velocity.magnitude > 0.05f;
				if (!yeah)
				{
					//Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(player.sprite);
					Material outlineMaterial1 = SpriteOutlineManager.GetOutlineMaterial(gun.sprite);
					//outlineMaterial.SetColor("_OverrideColor", new Color(20f, 20f, 42f));
					outlineMaterial1.SetColor("_OverrideColor", new Color(20f, 20f, 42f));
				}
				else
				{
					//Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(player.sprite);
					Material outlineMaterial1 = SpriteOutlineManager.GetOutlineMaterial(gun.sprite);
					//outlineMaterial.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
					outlineMaterial1.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
				}
			}
			else
			{
				//Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(player.sprite);
				Material outlineMaterial1 = SpriteOutlineManager.GetOutlineMaterial(gun.sprite);
				//outlineMaterial.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
				outlineMaterial1.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController playerController = this.gun.CurrentOwner as PlayerController;
			//projectile.specRigidbody.Position = new Position(player.sprite.WorldCenter);
			projectile.transform.position = playerController.sprite.WorldCenter;

			if (playerController.Velocity.magnitude > 0.05f)
			{
				projectile.baseData.damage *= 1f;
			}
			else
			{
				bool aaa = playerController.PlayerHasActiveSynergy("Cutting Edge Shot");
				if (aaa)
                {
					projectile.baseData.damage *= 2f;
				}
				else
                {
					projectile.baseData.damage *= 1.5f;
				}
			}
		}
	}
}