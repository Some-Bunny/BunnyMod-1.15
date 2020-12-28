﻿using System;
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
	public class Lacon2 : AdvancedGunBehavior
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("LaCon Mk.2", "clacon");
			Game.Items.Rename("outdated_gun_mods:lacon_mk.2", "bny:lacon_mk.2");
			gun.gameObject.AddComponent<Lacon2>();
			GunExt.SetShortDescription(gun, "Built From Modules");
			GunExt.SetLongDescription(gun, "With some experimentation, the Hegemony developed a weapon that could be fitted with simple upgrades on the fly.");
			GunExt.SetupSprite(gun, null, "clacon_idle_001", 25);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 18);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 7);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(54) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(57) as Gun).gunSwitchGroup;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(57) as Gun).muzzleFlashEffects;
			gun.carryPixelOffset = new IntVector2((int)4.5f, (int)0.125f);
			gun.barrelOffset.transform.localPosition = new Vector3(0.875f, 0.375f, 0f);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 3.0f;
			gun.DefaultModule.cooldownTime = 0.333f;
			gun.DefaultModule.numberOfShotsInClip = 30;
			gun.SetBaseMaxAmmo(300);
			gun.quality = PickupObject.ItemQuality.C;
			gun.DefaultModule.angleVariance = 0f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 9f;
			projectile.baseData.speed *= 5f;
			projectile.shouldRotate = true;

			projectile.pierceMinorBreakables = true;
			//PlayerController player = (GameManager.Instance.PrimaryPlayer);
			//projectile.transform.position = gun.barrelOffset.transform.localPosition= new Vector3(1.125f, 0.625f, 0f);
			projectile.HasDefaultTint = true;
			projectile.DefaultTintColor = new Color32(0, 255, 127, 255);

			ETGMod.Databases.Items.Add(gun, null, "ANY");
			Lacon2.Lacon2ID = gun.PickupObjectId;

		}
		public static int Lacon2ID;

		protected override void Update()
		{
			PlayerController player = this.gun.CurrentOwner as PlayerController;
			if (gun.CurrentOwner)
			{
				bool check = player.HasPickupID(Game.Items["bny:lacon_upgrade_scrap"].PickupObjectId);
				if (check)
				{
					AkSoundEngine.PostEvent("Play_OBJ_spears_clank_01", base.gameObject);
					Upgrades++;
					player.RemovePassiveItem(Lacon1Scrap.Scrap1ID);
				}
			}
			if (Upgrades >= 5 || Upgrades == 5)
			{
				Gun newgun;
				Gun oldgun;
				newgun = (Game.Items["bny:lacon_mk.3"] as Gun);
				oldgun = (Game.Items["bny:lacon_mk.2"] as Gun);
				player.inventory.AddGunToInventory(newgun, true);
				player.inventory.DestroyGun(oldgun);

			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.baseData.damage = 9f + (0.9f * Upgrades);
			projectile.DefaultTintColor = new Color32(0, 255, 127, 255);
			PlayerController playerController = this.gun.CurrentOwner as PlayerController;
		}

		protected override void OnPickup(PlayerController player)
		{
			WeightedGameObject item = new WeightedGameObject
			{
				pickupId = Lacon1.Lacon1ID,
				weight = 0f,
				rawGameObject = ETGMod.Databases.Items["LaCon Mk.1"].gameObject,
				forceDuplicatesPossible = false
			};
			bool flag = player.HasPickupID(Lacon2.Lacon2ID);
			if (flag)
			{
				GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements.Remove(item);
			}
			WeightedGameObject item1 = new WeightedGameObject
			{
				pickupId = Lacon3.Lacon3ID,
				weight = 0f,
				rawGameObject = ETGMod.Databases.Items["LaCon Mk.3"].gameObject,
				forceDuplicatesPossible = false
			};
			bool a = player.HasPickupID(Lacon2.Lacon2ID);
			if (a)
			{
				GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements.Remove(item1);
			}
			WeightedGameObject item2 = new WeightedGameObject
			{
				pickupId = Lacon4.Lacon4ID,
				weight = 0f,
				rawGameObject = ETGMod.Databases.Items["LaCon Mk.4"].gameObject,
				forceDuplicatesPossible = false
			};
			bool b = player.HasPickupID(Lacon2.Lacon2ID);
			if (b)
			{
				GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements.Remove(item2);
			}
			WeightedGameObject item3 = new WeightedGameObject
			{
				pickupId = Lacon5.Lacon5ID,
				weight = 0f,
				rawGameObject = ETGMod.Databases.Items["LaCon Mk.5"].gameObject,
				forceDuplicatesPossible = false
			};
			bool br = player.HasPickupID(Lacon2.Lacon2ID);
			if (br)
			{
				GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements.Remove(item3);
			}
			base.OnPickup(player);
		}
		protected override void OnPostDrop(PlayerController user)
		{
			base.OnPostDrop(user);
		}
		private static int Upgrades = 0;
	}
}