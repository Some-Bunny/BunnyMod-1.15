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
	public class ModuleCannon : GunBehaviour
	{
		public static void Add()
		{
			Gun modcannon = ETGMod.Databases.Items.NewGun("Modular Cannon", "modulecannon");
			Game.Items.Rename("outdated_gun_mods:modular_cannon", "bny:modular_cannon");
			modcannon.gameObject.AddComponent<ModuleCannon>();
			GunExt.SetShortDescription(modcannon, "Base Hand Cannon V1.395");
			GunExt.SetLongDescription(modcannon, "A basic model hand cannon fitted with modular tech compatibility.");
			GunExt.SetupSprite(modcannon, null, "modulecannon_idle_001", 25);
			GunExt.SetAnimationFPS(modcannon, modcannon.shootAnimation, 36);
			GunExt.SetAnimationFPS(modcannon, modcannon.reloadAnimation, 4);
			GunExt.SetAnimationFPS(modcannon, modcannon.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(modcannon, PickupObjectDatabase.GetById(88) as Gun, true, false);
			modcannon.DefaultModule.ammoCost = 1;
			modcannon.gunSwitchGroup = (PickupObjectDatabase.GetById(88) as Gun).gunSwitchGroup;
			modcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			modcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			modcannon.reloadTime = 2f;
			modcannon.DefaultModule.cooldownTime = .33f;
			modcannon.DefaultModule.numberOfShotsInClip = 12;
			modcannon.SetBaseMaxAmmo(300);
			modcannon.InfiniteAmmo = true;
			modcannon.barrelOffset.transform.localPosition = new Vector3(.5625f, 0.25f, 0f);
			modcannon.quality = PickupObject.ItemQuality.EXCLUDED;
			modcannon.DefaultModule.angleVariance = 3f;
			modcannon.DefaultModule.burstShotCount = 1;
			modcannon.encounterTrackable.EncounterGuid = "ModularCannon";
			modcannon.CanBeDropped = false;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(modcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			modcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 4f;
			projectile.baseData.speed *= 1f;
			projectile.shouldRotate = true;
			projectile.baseData.range = 1000;
			projectile.transform.parent = modcannon.barrelOffset;
			ETGMod.Databases.Items.Add(modcannon, null, "ANY");
			ModuleCannon.ModuleGunID = modcannon.PickupObjectId;

		}
		protected void Update()
		{
			bool flag = this.gun.CurrentOwner;
			if (flag)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				bool flag2 = !this.gun.IsReloading && !this.HasReloaded;
				if (flag2)
				{
					this.HasReloaded = true;
				}
				this.currentItems = playerController.passiveItems.Count;
				this.currentActives = playerController.activeItems.Count;
				bool mithrixitemcheck = this.currentItems != this.lastItems || this.currentActives != this.lastActives;
				if (mithrixitemcheck)
				{
					this.lastItems = this.currentItems;
					this.lastActives = this.currentActives;
				}
				foreach (PassiveItem passiveItem in playerController.passiveItems)
                {
					bool flagcheckfor = ModuleCannon.ItemsThatUpgrade.Contains(passiveItem.PickupObjectId);
					if (flagcheckfor)
					{
						bool flag4 = !this.hadzbTimeChecked;
						if (flag4)
						{
							this.UpgradeGun(playerController);
							this.hadzbTimeChecked = false;
						}
					}
					else
					{
						bool flag6 = this.hadzbTimeChecked;
						if (flag6)
						{
							this.UpgradeGun(playerController);
							this.hadzbTimeChecked = true;
						}
					}
				}
			}
		}
		private void UpgradeGun(PlayerController player)
		{
			int num = 12;
			int num2 = (int)0.33f;
            {
				bool flag = player.HasPickupID(Game.Items["zombie_bullets"].PickupObjectId);
				if (flag)
				{
					num += 3;
					foreach (ProjectileModule modcannon in this.gun.Volley.projectiles)
					{
						modcannon.numberOfShotsInClip = num;
					}
				}
			}
            {
				bool yurkey = player.HasPickupID(Game.Items["turkey"].PickupObjectId);
				if (yurkey)
				{
					num2 -= (int)0.083f;
					foreach (ProjectileModule modcannon in this.gun.Volley.projectiles)
					{
						modcannon.cooldownTime = num2;
					}
				}
			}
            {
				bool ammosynth = player.HasPickupID(Game.Items["ammo_synthesizer"].PickupObjectId);
				if (ammosynth)
				{
					num += 3;
					foreach (ProjectileModule modcannon in this.gun.Volley.projectiles)
					{
						modcannon.numberOfShotsInClip = num;
					}
				}
			}
            {
				bool belt = player.HasPickupID(Game.Items["ammo_belt"].PickupObjectId);
				if (belt)
				{
					num2 -= (int)0.083f;
					foreach (ProjectileModule modcannon in this.gun.Volley.projectiles)
					{
						modcannon.cooldownTime = num2;
					}
				}
			}
		}

		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = projectile.Owner as PlayerController;
			base.PostProcessProjectile(projectile);
			bool flag4 = player.HasPickupID(Game.Items["unity"].PickupObjectId);
			if (flag4)
			{
				projectile.baseData.damage += 1f;
			}
			bool chance = player.HasPickupID(Game.Items["chance_bullets"].PickupObjectId);
			if (chance)
			{
				projectile.baseData.damage += 1f;
			}
			bool bandana = player.HasPickupID(Game.Items["ancient_heros_bandana"].PickupObjectId);
			if (bandana)
			{
				projectile.baseData.damage += 2f;
				projectile.baseData.speed *= 1.5f;
			}
		}
		public static List<int> ItemsThatUpgrade = new List<int>
		{
			528,
			134,
			116,
			632
		};
		private bool hadzbTimeChecked;
		private int currentItems;
		private int lastItems;
		private int currentActives;
		private int lastActives;
		private bool HasReloaded;
		public static int ModuleGunID;
	}
}