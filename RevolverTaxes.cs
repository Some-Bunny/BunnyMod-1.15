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
	public class Taxes : AdvancedGunBehavior
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Taxes", "taxesbny");
			Game.Items.Rename("outdated_gun_mods:taxes", "bny:taxes");
			gun.gameObject.AddComponent<Taxes>();
			GunExt.SetShortDescription(gun, "Gundead Man Walkin'");
			GunExt.SetLongDescription(gun, "One of two antique revolvers carried by a long-gone Reaper.\n\nThis revolver has a sack of money engraved onto its barrel.");
			GunExt.SetupSprite(gun, null, "taxesbny_idle_001", 25);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 13);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, "magnum", true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(38) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.carryPixelOffset = new IntVector2((int)4.5f, (int)-0.25f);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.675f;
			gun.DefaultModule.cooldownTime = .175f;
			gun.DefaultModule.numberOfShotsInClip = 7;
			gun.SetBaseMaxAmmo(210);
			gun.quality = PickupObject.ItemQuality.B;
			gun.DefaultModule.angleVariance = 7f;
			gun.DefaultModule.burstShotCount = 1;
			gun.encounterTrackable.EncounterGuid = "I am Taxes, Destroyer of Wallets.";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 16f;
			projectile.baseData.speed *= 1.3f;
			projectile.transform.parent = gun.barrelOffset;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 1;
			//projectile.SetProjectileSpriteRight("chaosrevolver_projectile_001", 10, 10, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(7), new int?(7), null, null, null);
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			Taxes.TaxesID = gun.PickupObjectId;
	}

		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnWillKillEnemy = (Action<Projectile, SpeculativeRigidbody>)Delegate.Combine(projectile.OnWillKillEnemy, new Action<Projectile, SpeculativeRigidbody>(this.OnKill));
		}
		private void OnKill(Projectile arg1, SpeculativeRigidbody arg2)
		{
			PlayerController owner = (gun.CurrentOwner as PlayerController);
			int num3 = UnityEngine.Random.Range(0, 3);
			bool flag3 = num3 == 0;
			if (flag3)
            {
				LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, owner);
			}
		}

		protected override void OnPickup(PlayerController player)
		{
			WeightedGameObject item = new WeightedGameObject
			{
				pickupId = Death.DeathID,
				weight = 1.33f,
				rawGameObject = ETGMod.Databases.Items["Death"].gameObject,
				forceDuplicatesPossible = false
			};
			bool flag = !player.HasPickupID(Death.DeathID);
			if (flag)
			{
				GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements.Add(item);
			}
			base.OnPickup(player);
		}
		public static int TaxesID;
	}
}