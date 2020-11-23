using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;
//using DirectionType = DirectionalAnimation.DirectionType;
using AnimationType = ItemAPI.EnemyBuilder.AnimationType;
using System.Collections;
using Dungeonator;
using System.Linq;
using Brave.BulletScript;
using GungeonAPI;

namespace BunnyMod
{
	public class AbyssShotgunner : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "AbyssShotgunner";
		//private static tk2dSpriteCollectionData AbyssKinCollection;


		public static void Init()
		{

			AbyssShotgunner.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			AIActor source = EnemyDatabase.GetOrLoadByGuid("c50a862d19fc4d30baeba54795e8cb93");
			bool flag = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
			bool flag2 = flag;
			if (!flag2)
			{
				prefab = EnemyBuilder.BuildPrefab("AbyssShotgunner", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), true);
				var companion = prefab.AddComponent<EnemyBehavior>();
				companion.aiActor.knockbackDoer.weight = 200;
				companion.aiActor.MovementSpeed = 1.8f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = false;
				companion.aiActor.aiAnimator.HitReactChance = 0.05f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = true;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(55f);
				companion.aiActor.CollisionKnockbackStrength = 5f;
				companion.aiActor.CanTargetPlayers = true;
				companion.aiActor.healthHaver.SetHealthMaximum(55f, null, false);
				companion.aiActor.specRigidbody.PixelColliders.Clear();
				companion.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider

				{
					ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
					CollisionLayer = CollisionLayer.EnemyCollider,
					IsTrigger = false,
					BagleUseFirstFrameOnly = false,
					SpecifyBagelFrame = string.Empty,
					BagelColliderNumber = 0,
					ManualOffsetX = 0,
					ManualOffsetY = 0,
					ManualWidth = 15,
					ManualHeight = 17,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0
				});
				companion.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
				{

					ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
					CollisionLayer = CollisionLayer.EnemyHitBox,
					IsTrigger = false,
					BagleUseFirstFrameOnly = false,
					SpecifyBagelFrame = string.Empty,
					BagelColliderNumber = 0,
					ManualOffsetX = 0,
					ManualOffsetY = 0,
					ManualWidth = 15,
					ManualHeight = 17,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0,



				});
				companion.aiActor.CorpseObject = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").CorpseObject;
				companion.aiActor.PreventBlackPhantom = false;
				AIAnimator aiAnimator = companion.aiAnimator;

				prefab.AddAnimation("idle_right", "BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/IdleRight/", fps: 1, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("idle_left", "BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/IdleLeft/", fps: 1, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_right", "BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/", fps: 7, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_left", "BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/", fps: 7, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_left", "BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/DeathLeft/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_right", "BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/DeathRight/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);

				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("1bd8e49f93614e76b140077ff2e33f2b").behaviorSpeculator;
				BehaviorSpeculator load = EnemyDatabase.GetOrLoadByGuid("1bd8e49f93614e76b140077ff2e33f2b").behaviorSpeculator;
				bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
				bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
				bs.TargetBehaviors = new List<TargetBehaviorBase>
			{
				new TargetPlayerBehavior
				{
					Radius = 35f,
					LineOfSight = false,
					ObjectPermanence = true,
					SearchInterval = 0.25f,
					PauseOnTargetSwitch = false,
					PauseTime = 0.25f
				}
			};
				bs.MovementBehaviors = new List<MovementBehaviorBase>() {
				new SeekTargetBehavior() {
					StopWhenInRange = true,
					CustomRange = 6,
					LineOfSight = true,
					ReturnToSpawn = true,
					SpawnTetherDistance = 0,
					PathInterval = 0.5f,
					SpecifyRange = false,
					MinActiveRange = 1,
					MaxActiveRange = 10
				}
			};
				bs.AttackBehaviors = new List<AttackBehaviorBase>() {
				new ShootGunBehavior() {
					GroupCooldownVariance = 0.2f,
					LineOfSight = false,
					WeaponType = WeaponType.BulletScript,
					OverrideBulletName = null,
					BulletScript = new CustomBulletScriptSelector(typeof(AbyssShottyAttack)),
					FixTargetDuringAttack = false,
					StopDuringAttack = false,
					LeadAmount = 0,
					LeadChance = 0.62f,
					RespectReload = true,
					MagazineCapacity = 1,
					ReloadSpeed = 3,
					EmptiesClip = true,
					SuppressReloadAnim = false,
					TimeBetweenShots = 0.5f,
					PreventTargetSwitching = false,
					OverrideAnimation = null,
					OverrideDirectionalAnimation = null,
					HideGun = false,
					UseLaserSight = false,
					UseGreenLaser = false,
					PreFireLaserTime = -1,
					AimAtFacingDirectionWhenSafe = false,
					Cooldown = 0.2f,
					CooldownVariance = 0,
					AttackCooldown = 0,
					GlobalCooldown = 0,
					InitialCooldown = 0,
					InitialCooldownVariance = 0,
					GroupName = null,
					GroupCooldown = 0,
					MinRange = 0,
					Range = 16,
					MinWallDistance = 0,
					MaxEnemiesInRoom = 0,
					MinHealthThreshold = 0,
					MaxHealthThreshold = 1,
					HealthThresholds = new float[0],
					AccumulateHealthThresholds = true,
					targetAreaStyle = null,
					IsBlackPhantom = false,
					resetCooldownOnDamage = null,
					RequiresLineOfSight = false,
					MaxUsages = 0
				}
			};
				//Tools.DebugInformation(load);
				AIActor aIActor = EnemyDatabase.GetOrLoadByGuid("1bd8e49f93614e76b140077ff2e33f2b");
				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
				GameObject m_CachedGunAttachPoint = companion.transform.Find("GunAttachPoint").gameObject;
				EnemyBuilder.DuplicateAIShooterAndAIBulletBank(prefab, aIActor.aiShooter, aIActor.GetComponent<AIBulletBank>(), 93, m_CachedGunAttachPoint.transform);
				Game.Enemies.Add("bny:abyss_shotgunner", companion.aiActor);


			}
		}



		private static string[] spritePaths = new string[]
		{
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Corpse/abysskin_corpse_001",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/AbyssShotgunner/abyssshotgunner_die_left_001",

			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/DeathRight/abyssshotgunner_die_left_001",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Hit/abysskin_hit_001",

			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/IdleLeft/abyssshotgunner_idle_left_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/IdleLeft/abyssshotgunner_idle_left_002",

			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/IdleRight/abyssshotgunner_idle_left_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/IdleRight/abyssshotgunner_idle_left_002",

			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/abyssshotgunner_move_left_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/abyssshotgunner_move_left_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/abyssshotgunner_move_left_003",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/abyssshotgunner_move_left_004",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/abyssshotgunner_move_left_005",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveLeft/abyssshotgunner_move_left_006",

			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/abyssshotgunner_move_right_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/abyssshotgunner_move_right_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/abyssshotgunner_move_right_003",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/abyssshotgunner_move_right_004",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/abyssshotgunner_move_right_005",
			"BunnyMod/Resources/AbyssEnemies/AbyssShotgunner/MoveRight/abyssshotgunner_move_right_006",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Pitfall/abysskin_pitfall_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Pitfall/abysskin_pitfall_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Pitfall/abysskin_pitfall_003",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Pitfall/abysskin_pitfall_004",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Pitfall/abysskin_pitfall_005",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Spawn/abysskin_spawn_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Spawn/abysskin_spawn_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Spawn/abysskin_spawn_003",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Spawn/abysskin_spawn_004",

		};

		public class EnemyBehavior : BraveBehaviour
		{

			private void Start()
			{

				base.aiActor.healthHaver.OnPreDeath += (obj) =>
				{
					//replace later
					//AkSoundEngine.PostEvent("Play_VO_kali_death_01", base.aiActor.gameObject);
				};
			}


		}

		public class AbyssShottyAttack: Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), new AbyssShottyAttack.Chain());
				base.Fire(new Direction(-30f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new AbyssShottyAttack.Dagger());
				base.Fire(new Direction(-20f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(-10f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), null);
				for (int a = 0; a < 7; a++)
				{
					base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(3f + a, SpeedType.Absolute), new AbyssShottyAttack.Chain());
				}
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(11f, SpeedType.Absolute), new AbyssShottyAttack.Dagger());


				base.Fire(new Direction(10f, DirectionType.Aim, -1f), new Speed(9f, SpeedType.Absolute), null);
				base.Fire(new Direction(20f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), null);
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(8f, SpeedType.Absolute), new AbyssShottyAttack.Dagger());
				base.Fire(new Direction(30f, DirectionType.Aim, -1f), new Speed(7f, SpeedType.Absolute), new AbyssShottyAttack.Chain());

				yield return base.Wait(30);

				yield break;
			}
			public class Chain : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Chain() : base("chain", false, false, false)
				{
				}
			}
			public class Dagger : Bullet
			{
				// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
				public Dagger() : base("dagger", false, false, false)
				{
				}
			}
		}
	}

}







