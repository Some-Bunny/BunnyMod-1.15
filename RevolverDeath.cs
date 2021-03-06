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
	public class Death : AdvancedGunBehavior
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Death", "deathbny");
			Game.Items.Rename("outdated_gun_mods:death", "bny:death");
			gun.gameObject.AddComponent<Death>();
			GunExt.SetShortDescription(gun, "Blood On The Chamber Floor");
			GunExt.SetLongDescription(gun, "One of two antique revolvers carried by a long-gone Reaper.\n\nThis revolver has a skull engraved onto its barrel.");
			GunExt.SetupSprite(gun, null, "deathbny_idle_001", 25);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 13);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, "magnum", true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(38) as Gun).gunSwitchGroup;
			gun.carryPixelOffset = new IntVector2((int)4.5f, (int)-0.25f);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.225f;
			gun.DefaultModule.cooldownTime = .225f;
			gun.DefaultModule.numberOfShotsInClip = 7;
			gun.SetBaseMaxAmmo(210);
			gun.quality = PickupObject.ItemQuality.B;
			gun.DefaultModule.angleVariance = 7f;
			gun.DefaultModule.burstShotCount = 1;
			gun.encounterTrackable.EncounterGuid = "I am Death, Destroyer of Worlds.";
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
			Death.DeathID = gun.PickupObjectId;
		}
	

		public override void PostProcessProjectile(Projectile projectile)
		{
			try
			{
				projectile.specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(projectile.specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.HandlePreCollision));
			}
			catch (Exception ex)
			{
				ETGModConsole.Log(ex.Message, false);
			}
		}
		private void HandlePreCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider)
		{
			bool flag = otherRigidbody && otherRigidbody.healthHaver && otherRigidbody != null;
			if (flag)
			{

				float maxHealth = otherRigidbody.healthHaver.GetMaxHealth();
				float num = maxHealth * 0.50f;
				float currentHealth = otherRigidbody.healthHaver.GetCurrentHealth();
				bool flag2 = currentHealth < num;
				if (flag2)
				{
					float damage = myRigidbody.projectile.baseData.damage;
					myRigidbody.projectile.baseData.damage *= 1.66f;
					GameManager.Instance.StartCoroutine(this.ChangeProjectileDamage(myRigidbody.projectile, damage));
				}
				
			}
		}
		// Token: 0x06000662 RID: 1634 RVA: 0x0003A90C File Offset: 0x00038B0C




		// Token: 0x0600042E RID: 1070 RVA: 0x00027D59 File Offset: 0x00025F59
		private IEnumerator ChangeProjectileDamage(Projectile bullet, float oldDamage)
		{
			yield return new WaitForSeconds(0.1f);
			bool flag = bullet != null;
			if (flag)
			{
				bullet.baseData.damage = oldDamage;
			}
			yield break;
		}

		protected override void OnPickup(PlayerController player)
		{
			WeightedGameObject item = new WeightedGameObject
			{
				pickupId = Taxes.TaxesID,
				weight = 1.33f,
				rawGameObject = ETGMod.Databases.Items["Death"].gameObject,
				forceDuplicatesPossible = false
			};
			bool flag = !player.HasPickupID(Taxes.TaxesID);
			if (flag)
			{
				GameManager.Instance.RewardManager.ItemsLootTable.defaultItemDrops.elements.Add(item);
			}
			base.OnPickup(player);
		}

		public static int DeathID;
	}
}