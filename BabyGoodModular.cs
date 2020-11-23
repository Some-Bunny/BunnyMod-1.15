using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;









namespace BunnyMod
{
	public class BabyGoodModular : CompanionItem
	{
		public static void Init()
		{
			string name = "Baby Good Modular";
			string resourcePath = "BunnyMod/Resources/BabyGoodModular/babygoodmodular.png";
			GameObject gameObject = new GameObject();
			BabyGoodModular module = gameObject.AddComponent<BabyGoodModular>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "Friend Module Enabled";
			string longDesc = "A miniature version of the Modular robots left to rust in the Gungeon. This one has imprinted onto you and thinks you're its dad.\n\nHow cute.";
			module.SetupItem(shortDesc, longDesc, "bny");
			module.quality = PickupObject.ItemQuality.B;
			module.CompanionGuid = BabyGoodModular.guid;
			BabyGoodModular.BuildPrefab();
		}

		public override void Pickup(PlayerController player)
		{
			GameManager.Instance.OnNewLevelFullyLoaded += this.OnNewFloor;
			player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
			//player.healthHaver.OnDamaged += this.PlayerTookDamage;
			base.Pickup(player);
		}
		private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemy)
		{
			bool flag = enemy.aiActor && enemy.IsBoss && fatal;
			bool flag2 = flag;
			if (flag2)
			{
				if (GivesModuleOnce == true)
                {
					GivesModuleOnce = false;
					int num3 = UnityEngine.Random.Range(0, 5);
					bool flag3 = num3 == 0;
					if (flag3)
					{
						LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Damage Module"].gameObject, base.Owner, true);
					}
					bool flag4 = num3 == 1;
					if (flag4)
					{
						LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Clip Size Module"].gameObject, base.Owner, true);
					}
					bool flag5 = num3 == 2;
					if (flag5)
					{
						LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Reload Module"].gameObject, base.Owner, true);
					}
					bool flag6 = num3 == 3;
					if (flag6)
					{
						LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fire Rate Module"].gameObject, base.Owner, true);
					}
					bool flag7 = num3 == 4;
					if (flag7)
					{
						LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Splitter Module"].gameObject, base.Owner, true);
					}
				}
			}
		}
		private void OnNewFloor()
		{
			GivesModuleOnce = true;
		}
		private void PlayerTookDamage(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
		{
			AIActor component = BabyGoodModular.ModulePrefab.GetComponent<AIActor>();
			AIActor beans = component as AIActor;
			BunnyModule.Log("start");
			Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
		//	Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
			//Vector3 vector2 = player.specRigidbody.UnitCenter;
			GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, beans.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-180, 180)), true);
			Projectile component1 = gameObject.GetComponent<Projectile>();
			BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
			bouncy.numberOfBounces = 2;
			{
				component1.Owner = base.Owner;
				component1.Shooter = base.Owner.specRigidbody;
			}
			//LootEngine.SpawnItem(PickupObjectDatabase.GetById(68).gameObject, beans.sprite.WorldCenter, Vector2.down, .7f, false, true, false);
			//BunnyModule.Log("end");
		}
		public override DebrisObject Drop(PlayerController player)
		{
			GameManager.Instance.OnNewLevelFullyLoaded -= this.OnNewFloor;
			player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Remove(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
			//player.healthHaver.OnDamaged -= this.PlayerTookDamage;
			return base.Drop(player);
		}
		public static void BuildPrefab()
		{
			bool flag = BabyGoodModular.ModulePrefab != null || CompanionBuilder.companionDictionary.ContainsKey(BabyGoodModular.guid);
			bool flag2 = !flag;
			if (flag2)
			{
				BabyGoodModular.ModulePrefab = CompanionBuilder.BuildPrefab("Baby Good Chance Kin", BabyGoodModular.guid, "BunnyMod/Resources/BabyGoodModular/babygoodmodular_idle_left_001", new IntVector2(8, 0), new IntVector2(6, 11));
				BabyGoodModular.ModuleBehaviorplswork chanceKinBehavioar = BabyGoodModular.ModulePrefab.AddComponent<BabyGoodModular.ModuleBehaviorplswork>();
				chanceKinBehavioar.aiActor.MovementSpeed = 5f;
				BabyGoodModular.ModulePrefab.AddAnimation("idle_right", "BunnyMod/Resources/BabyGoodModular/babygoodmodular_idle_right", 3, CompanionBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal, DirectionalAnimation.FlipType.None);
				BabyGoodModular.ModulePrefab.AddAnimation("idle_left", "BunnyMod/Resources/BabyGoodModular/babygoodmodular_idle_left", 3, CompanionBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal, DirectionalAnimation.FlipType.None);
				BabyGoodModular.ModulePrefab.AddAnimation("run_right", "BunnyMod/Resources/BabyGoodModular/babygoodmodular_run_right", 10, CompanionBuilder.AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal, DirectionalAnimation.FlipType.None);
				BabyGoodModular.ModulePrefab.AddAnimation("run_left", "BunnyMod/Resources/BabyGoodModular/babygoodmodular_run_left", 10, CompanionBuilder.AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal, DirectionalAnimation.FlipType.None);
				BehaviorSpeculator component = BabyGoodModular.ModulePrefab.GetComponent<BehaviorSpeculator>();
				component.MovementBehaviors.Add(new CompanionFollowPlayerBehavior

				{
					IdleAnimations = new string[]
					{
						"idle"
					}
				});
				UnityEngine.Object.DontDestroyOnLoad(BabyGoodModular.ModulePrefab);
				FakePrefab.MarkAsFakePrefab(BabyGoodModular.ModulePrefab);
			}
		}
		// Token: 0x0400008D RID: 141
		public static GameObject ModulePrefab;
		private List<CompanionController> companionsSpawned = new List<CompanionController>();

		private static readonly string guid = "minimodularminimodularminimodular";
		private bool GivesModuleOnce = true;

		public class ModuleBehaviorplswork : CompanionController
		{
			public void Start()
			{
				PlayerController player = m_owner as PlayerController;
				Material mat = new Material(EnemyDatabase.GetOrLoadByName("GunNut").sprite.renderer.material);
				mat.mainTexture = base.aiActor.sprite.renderer.material.mainTexture;
				mat.SetColor("_EmissiveColor", new Color32(67, 225, 240, 255));
				mat.SetFloat("_EmissiveColorPower", 1.55f);
				mat.SetFloat("_EmissivePower", 100);
				aiActor.sprite.renderer.material = mat;
				this.Owner = this.m_owner;
			}
			private PlayerController Owner;
		}
	}
}
