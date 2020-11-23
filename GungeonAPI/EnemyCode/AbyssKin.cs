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
	public class AbyssKinPlease : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "AbyssKin";
		//private static tk2dSpriteCollectionData AbyssKinCollection;


		public static void Init()
		{

			AbyssKinPlease.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			AIActor source = EnemyDatabase.GetOrLoadByGuid("c50a862d19fc4d30baeba54795e8cb93");
			bool flag = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
			bool flag2 = flag;
			if (!flag2)
			{
				prefab = EnemyBuilder.BuildPrefab("AbyssKin", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), true);
				var companion = prefab.AddComponent<EnemyBehavior>();
				companion.aiActor.knockbackDoer.weight = 200;
				companion.aiActor.MovementSpeed = 3f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = false;
				companion.aiActor.aiAnimator.HitReactChance = 0.05f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = true;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(25f);
				companion.aiActor.CollisionKnockbackStrength = 5f;
				companion.aiActor.CanTargetPlayers = true;
				companion.aiActor.healthHaver.SetHealthMaximum(25f, null, false);
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

				prefab.AddAnimation("idle_right", "BunnyMod/Resources/AbyssEnemies/AbyssKin/IdleRight/", fps: 1, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("idle_left", "BunnyMod/Resources/AbyssEnemies/AbyssKin/IdleLeft/", fps: 1, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_right", "BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/", fps: 8, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_left", "BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/", fps: 8, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_left", "BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathLeft/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_right", "BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathRight/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);

				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;
				BehaviorSpeculator load = EnemyDatabase.GetOrLoadByGuid("6e972cd3b11e4b429b888b488e308551").behaviorSpeculator;
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
					BulletScript = new CustomBulletScriptSelector(typeof(AbyssKinAttack)),
					FixTargetDuringAttack = false,
					StopDuringAttack = false,
					LeadAmount = 0,
					LeadChance = 0.62f,
					RespectReload = true,
					MagazineCapacity = 2,
					ReloadSpeed = 4,
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
				AIActor aIActor = EnemyDatabase.GetOrLoadByGuid("1a78cfb776f54641b832e92c44021cf2");
				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
				GameObject m_CachedGunAttachPoint = companion.transform.Find("GunAttachPoint").gameObject;
				EnemyBuilder.DuplicateAIShooterAndAIBulletBank(prefab, aIActor.aiShooter, aIActor.GetComponent<AIBulletBank>(), 45, m_CachedGunAttachPoint.transform);
				Game.Enemies.Add("bny:abyss_kin", companion.aiActor);


			}
		}



		private static string[] spritePaths = new string[]
		{
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Corpse/abysskin_corpse_001",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathLeft/abysskin_death_left_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathLeft/abysskin_death_left_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathLeft/abysskin_death_left_003",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathRight/abysskin_death_right_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathRight/abysskin_death_right_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/DeathRight/abysskin_death_right_003",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/Hit/abysskin_hit_001",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/IdleLeft/abysskin_idle_left_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/IdleLeft/abysskin_idle_left_002",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/IdleRight/abysskin_idle_right_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/IdleRight/abysskin_idle_right_002",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/abysskin_move_left_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/abysskin_move_left_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/abysskin_move_left_003",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/abysskin_move_left_004",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/abysskin_move_left_005",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveLeft/abysskin_move_left_006",

			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/abysskin_move_right_001",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/abysskin_move_right_002",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/abysskin_move_right_003",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/abysskin_move_right_004",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/abysskin_move_right_005",
			"BunnyMod/Resources/AbyssEnemies/AbyssKin/MoveRight/abysskin_move_right_006",

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

		public class AbyssKinAttack : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
				}
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixBullet(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixBullet(true));
				//yield return base.Wait(10);
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet1(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet1(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet2(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet2(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet3(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet3(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet4(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet4(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet5(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet5(true));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet6(false));
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(9.5f, SpeedType.Absolute), new HelixChainBullet6(true));
				yield break;
			}
			public class HelixBullet : Bullet
			{
				public HelixBullet(bool reverse) : base("bigBullet", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet : Bullet
			{
				public HelixChainBullet(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(5);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet1 : Bullet
			{
				public HelixChainBullet1(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(10);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet2 : Bullet
			{
				public HelixChainBullet2(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(15);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet3 : Bullet
			{
				public HelixChainBullet3(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(20);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet4 : Bullet
			{
				public HelixChainBullet4(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(25);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet5 : Bullet
			{
				public HelixChainBullet5(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(30);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
			public class HelixChainBullet6 : Bullet
			{
				public HelixChainBullet6(bool reverse) : base("chain", false, false, false)
				{
					this.reverse = reverse;
					base.SuppressVfx = true;
				}
				protected override IEnumerator Top()
				{
					float startSpeed = base.Speed;
					base.ChangeSpeed(new Speed(0f, SpeedType.Absolute), 0);
					yield return base.Wait(35);
					base.ChangeSpeed(new Speed(9.5f, SpeedType.Absolute), 0);
					this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
					this.ManualControl = true;
					Vector2 truePosition = this.Position;
					float startVal = 1;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-.75f, .75f, Mathf.PingPong(startVal + (float)i / 90f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 90f);
						this.Position = truePosition + (this.reverse ? BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude) : BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude));
						yield return this.Wait(1);
					}
					this.Vanish(false);
					yield break;
				}
				private bool reverse;
			}
		}
	}

}







