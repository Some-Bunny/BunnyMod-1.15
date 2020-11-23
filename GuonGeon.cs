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



namespace BunnyMod
{
	internal class GuonGeon : IounStoneOrbitalItem
	{
		public static void Init()
		{
			string name = "Guon-Geon";
			string resourcePath = "BunnyMod/Resources/GuonGeon/guongeon.png";
			GameObject gameObject = new GameObject();
			GuonGeon boomGuon = gameObject.AddComponent<GuonGeon>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "Enter The Guon-Geon";
			string longDesc = "A miniature replica of the Gungeon in the form of a Guon Stone. Placing your ear onto the mouth part lets you hear gun-based rap.";
			boomGuon.SetupItem(shortDesc, longDesc, "bny");
			boomGuon.quality = PickupObject.ItemQuality.S;
			boomGuon.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			GuonGeon.BuildPrefab();
			boomGuon.OrbitalPrefab = GuonGeon.orbitalPrefab;
			boomGuon.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
			List<string> mandatoryConsoleIDs = new List<string>
			{
				"bny:guon-geon",
			};
			List<string> optionalConsoleIDs = new List<string>
			{
				"tangler",
				"shotgun_full_of_hate",
				"cold_45",
				"grasschopper",
				"pea_shooter",
				"blooper",
				"bee_hive",
				"zorgun",
				"glacier",
				"the_judge",
				"plague_pistol",
				"gunbow",
				"ice_breaker",
				"dungeon_eagle",
				"gungeon_ant",
				"zilla_shotgun",
				"vertebraek47"
			};
			CustomSynergies.Add("You made the cut!", mandatoryConsoleIDs, optionalConsoleIDs, true);
			List<string> mandatoryConsoleID1s = new List<string>
			{
				"bny:guon-geon",
			};
			List<string> optionalConsoleID1s = new List<string>
			{
				"disintegrator",
				"demon_head",
				"unicorn_horn"
			};
			CustomSynergies.Add("You didn't make the cut...", mandatoryConsoleID1s, optionalConsoleID1s, true);

		}

		public static void BuildPrefab()
		{
			bool flag = GuonGeon.orbitalPrefab != null;
			if (!flag)
			{
				GameObject gameObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/GuonGeon/guongeonfloaty.png", null, true);
				gameObject.name = "GuonGeon";
				SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(10, 10));
				speculativeRigidbody.CollideWithTileMap = false;
				speculativeRigidbody.CollideWithOthers = true;
				speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
				GuonGeon.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
				GuonGeon.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
				GuonGeon.orbitalPrefab.shouldRotate = false;
				GuonGeon.orbitalPrefab.orbitRadius = 3.5f;
				GuonGeon.orbitalPrefab.orbitDegreesPerSecond = 45f;
				GuonGeon.orbitalPrefab.SetOrbitalTier(0);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				FakePrefab.MarkAsFakePrefab(gameObject);
				gameObject.SetActive(false);
			}
		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			GuonGeon.guonHook = new Hook(typeof(PlayerOrbital).GetMethod("Initialize"), typeof(GuonGeon).GetMethod("GuonInit"));
			bool flag = player.gameObject.GetComponent<GuonGeon.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				player.gameObject.GetComponent<GuonGeon.BaBoom>().Destroy();
			}
			player.gameObject.AddComponent<GuonGeon.BaBoom>();
			GameManager.Instance.OnNewLevelFullyLoaded += this.FixGuon;
			bool flag4 = this.m_extantOrbital != null;
			bool flag5 = flag4;
			if (flag5)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00019660 File Offset: 0x00017860
		private void OnPreCollison(SpeculativeRigidbody myRigidbody, PixelCollider myCollider, SpeculativeRigidbody other, PixelCollider otherCollider)
		{
			bool flag = base.Owner != null;
			bool flag2 = flag;
			if (flag2)
			{
				Projectile component = other.GetComponent<Projectile>();
				bool flag3 = component != null && !(component.Owner is PlayerController);
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = !GuonGeon.onCooldown;
					bool flag6 = flag5;
					if (flag6)
                    {
						GuonGeon.onCooldown = true;
						GameManager.Instance.StartCoroutine(GuonGeon.StartCooldown(base.Owner));
						{
							this.GunBasedRap(myRigidbody.sprite.WorldCenter, myRigidbody);
						}
					}
				}
			}
		}
		public static IEnumerator StartCooldown(PlayerController player)
		{
			bool flag1 = player.HasPickupID(121) || player.HasPickupID(100) || player.HasPickupID(60);
			if (flag1)
            {
				yield return new WaitForSeconds(2f);
				GuonGeon.onCooldown = false;
				yield break;
			}
            else
            {
				yield return new WaitForSeconds(4f);
				GuonGeon.onCooldown = false;
				yield break;
			}
		}
		private static bool onCooldown;

		// Token: 0x0600025B RID: 603 RVA: 0x000196DC File Offset: 0x000178DC
		public override DebrisObject Drop(PlayerController player)
		{
			player.GetComponent<GuonGeon.BaBoom>().Destroy();
			GuonGeon.guonHook.Dispose();
			GameManager.Instance.OnNewLevelFullyLoaded -= this.FixGuon;
			bool flag = this.m_extantOrbital != null;
			bool flag2 = flag;
			if (flag2)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
			return base.Drop(player);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001976C File Offset: 0x0001796C
		private void FixGuon()
		{
			bool flag = base.Owner && base.Owner.GetComponent<GuonGeon.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				base.Owner.GetComponent<GuonGeon.BaBoom>().Destroy();
			}
			bool flag4 = this.m_extantOrbital != null;
			bool flag5 = flag4;
			if (flag5)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
			PlayerController owner = base.Owner;
			owner.gameObject.AddComponent<GuonGeon.BaBoom>();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00019818 File Offset: 0x00017A18
		protected override void OnDestroy()
		{
			GuonGeon.guonHook.Dispose();
			bool flag = base.Owner && base.Owner.GetComponent<GuonGeon.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				base.Owner.GetComponent<GuonGeon.BaBoom>().Destroy();
			}
			GameManager.Instance.OnNewLevelFullyLoaded -= this.FixGuon;
			base.OnDestroy();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B3AF File Offset: 0x000095AF
		public static void GuonInit(Action<PlayerOrbital, PlayerController> orig, PlayerOrbital self, PlayerController player)
		{
			orig(self, player);
		}

		// Token: 0x040000D5 RID: 213
		public static Hook guonHook;

		// Token: 0x040000D6 RID: 214
		public static PlayerOrbital orbitalPrefab;

		// Token: 0x0200009E RID: 158
		private class BaBoom : BraveBehaviour
		{
			// Token: 0x060003BD RID: 957 RVA: 0x0002320E File Offset: 0x0002140E
			private void Start()
			{
				this.owner = base.GetComponent<PlayerController>();
			}

			// Token: 0x060003BE RID: 958 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
			public void Destroy()
			{
				UnityEngine.Object.Destroy(this);
			}

			// Token: 0x04000215 RID: 533
			private PlayerController owner;
		}
		public void GunBasedRap(Vector3 position, SpeculativeRigidbody myRigidbody)
		{
			int num3 = UnityEngine.Random.Range(1, 19);
			bool flag4 = num3 == 1;
			if (flag4)
			{
				bool flag1 = Owner.HasPickupID(143);
				if (flag1)
                {
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[143]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
                else
                {
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[143]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
					
			}
			bool flag5 = num3 == 2;
			if (flag5)
			{
				bool flag1 = Owner.HasPickupID(175);
				if (flag1)
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[175]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[175]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag6 = num3 == 3;
			if (flag6)
			{
				bool flag1 = Owner.HasPickupID(223);
				if (flag1)
                {
					for (int counter = 0; counter < 10; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[223]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
                {
					for (int counter = 0; counter < 5; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[223]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}

			}
			bool flag7 = num3 == 4;
			if (flag7)
			{
				bool flag1 = Owner.HasPickupID(180);
				if (flag1)
				{
					for (int counter = 0; counter < 2; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[180]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 1; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[180]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}

			}
			bool flag9 = num3 == 5;
			if (flag9)
			{
				bool flag1 = Owner.HasPickupID(197);
				if (flag1)
				{
					for (int counter = 0; counter < 12; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[197]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[197]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag10 = num3 == 6;
			if (flag10)
			{
				bool flag1 = Owner.HasPickupID(18);
				if (flag1)
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[18]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[18]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag11 = num3 == 7;
			if (flag11)
			{
				for (int counter = 0; counter < 5; counter++)
				{
					Projectile projectile = ((Gun)ETGMod.Databases.Items[89]).DefaultModule.projectiles[0];
					Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
					Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
					GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
					Projectile component = gameObject.GetComponent<Projectile>();
					HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
					homing.HomingRadius = 20;
					homing.AngularVelocity = 30;
					{
						component.Owner = base.Owner;
						component.Shooter = base.Owner.specRigidbody;
					}
				}
			}
			bool flag12 = num3 == 8;
			if (flag12)
			{
				bool flag1 = Owner.HasPickupID(14);
				if (flag1)
				{
					for (int counter = 0; counter < 14; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[14]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 7; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[14]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag13 = num3 == 9;
			if (flag13)
			{
				bool flag1 = Owner.HasPickupID(6);
				if (flag1)
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[6]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[6]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag14 = num3 == 10;
			if (flag14)
			{
				for (int counter = 0; counter < 2; counter++)
				{
					Projectile projectile = ((Gun)ETGMod.Databases.Items[130]).DefaultModule.projectiles[0];
					Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
					Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
					GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
					Projectile component = gameObject.GetComponent<Projectile>();
					HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
					homing.HomingRadius = 20;
					homing.AngularVelocity = 30;
					{
						component.Owner = base.Owner;
						component.Shooter = base.Owner.specRigidbody;
					}
				}
			}
			bool flag15 = num3 == 11;
			if (flag15)
			{
				bool flag1 = Owner.HasPickupID(184);
				if (flag1)
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[184]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[184]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag16 = num3 == 12;
			if (flag16)
			{
				bool flag1 = Owner.HasPickupID(207);
				if (flag1)
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[207]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[207]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag17 = num3 == 13;
			if (flag17)
			{
				bool flag1 = Owner.HasPickupID(210);
				if (flag1)
				{
					for (int counter = 0; counter < 2; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[210]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 1; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[210]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag18 = num3 == 14;
			if (flag18)
			{
				bool flag1 = Owner.HasPickupID(225);
				if (flag1)
				{
					for (int counter = 0; counter < 6; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[225]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 3; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[225]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag19 = num3 == 15;
			if (flag19)
			{
				for (int counter = 0; counter < 4; counter++)
				{
					Projectile projectile = ((Gun)ETGMod.Databases.Items[88]).DefaultModule.projectiles[0];
					Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
					Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
					GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
					Projectile component = gameObject.GetComponent<Projectile>();
					HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
					homing.HomingRadius = 20;
					homing.AngularVelocity = 30;
					{
						component.Owner = base.Owner;
						component.Shooter = base.Owner.specRigidbody;
					}
				}
			}
			bool flag20 = num3 == 16;
			if (flag20)
			{
				bool flag1 = Owner.HasPickupID(23);
				if (flag1)
				{
					for (int counter = 0; counter < 10; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[23]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 5; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[23]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag21 = num3 == 17;
			if (flag21)
			{
				bool flag1 = Owner.HasPickupID(176);
				if (flag1)
				{
					for (int counter = 0; counter < 8; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[176]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 4; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[176]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag22 = num3 == 18;
			if (flag22)
			{
				bool flag1 = Owner.HasPickupID(329);
				if (flag1)
				{
					for (int counter = 0; counter < 14; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[329]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 7; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[329]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
			bool flag23 = num3 == 19;
			if (flag23)
			{
				bool flag1 = Owner.HasPickupID(29);
				if (flag1)
				{
					for (int counter = 0; counter < 8; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[29]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
				else
				{
					for (int counter = 0; counter < 4; counter++)
					{
						Projectile projectile = ((Gun)ETGMod.Databases.Items[29]).DefaultModule.projectiles[0];
						Vector3 vector = base.Owner.unadjustedAimPoint - base.Owner.LockedApproximateSpriteCenter;
						Vector3 vector2 = base.Owner.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, myRigidbody.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 20;
						homing.AngularVelocity = 30;
						{
							component.Owner = base.Owner;
							component.Shooter = base.Owner.specRigidbody;
						}
					}
				}
			}
		}
	}
}
