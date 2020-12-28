using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;
using AnimationType = ItemAPI.EnemyBuilder.AnimationType;
using System.Collections;
using Dungeonator;
using System.Linq;
using Brave.BulletScript;
using GungeonAPI;

namespace BunnyMod
{
	public class DopplegamnerClone : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "DoppelGanger";
		//private static tk2dSpriteCollectionData AbyssKinCollection;


		public static void Init()
		{

			DopplegamnerClone.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			AIActor source = EnemyDatabase.GetOrLoadByGuid("e21ac9492110493baef6df02a2682a0d");
			bool flag = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
			bool flag2 = flag;
			if (!flag2)
			{
				prefab = EnemyBuilder.BuildPrefab("DoppelGanger", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), false);
				var companion = prefab.AddComponent<EnemyBehavior>();
				ChaosBeing.shootpoint = new GameObject("fuck");
				ChaosBeing.shootpoint.transform.parent = companion.transform;
				ChaosBeing.shootpoint.transform.position = companion.sprite.WorldCenter;
				companion.aiActor.knockbackDoer.weight = 200000;
				companion.aiActor.MovementSpeed *= 0.25f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = true;
				companion.aiActor.aiAnimator.HitReactChance = 0f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = true;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(75f);
				companion.aiActor.CollisionKnockbackStrength = 0f;
				companion.aiActor.CanTargetPlayers = true;
				companion.aiActor.healthHaver.SetHealthMaximum(75f, null, false);
				companion.aiActor.specRigidbody.PixelColliders.Clear();

				GameObject AttachPoint = companion.transform.Find("fuck").gameObject;

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
				companion.aiActor.CorpseObject = EnemyDatabase.GetOrLoadByGuid("1a78cfb776f54641b832e92c44021cf2").CorpseObject;
				companion.aiActor.PreventBlackPhantom = false;
				AIAnimator aiAnimator = companion.aiAnimator;

				prefab.AddAnimation("idle_right", "BunnyMod/Resources/ChaosEnemy/IdleRight/", fps:4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("idle_left", "BunnyMod/Resources/ChaosEnemy/IdleLeft/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_right", "BunnyMod/Resources/ChaosEnemy/MoveRight/", fps: 4, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_left", "BunnyMod/Resources/ChaosEnemy/MoveLeft/", fps: 4, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_left", "BunnyMod/Resources/ChaosEnemy/DeathLeft/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_right", "BunnyMod/Resources/ChaosEnemy/DeathRight/", fps: 4, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);

				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("e21ac9492110493baef6df02a2682a0d").behaviorSpeculator;
				BehaviorSpeculator load = EnemyDatabase.GetOrLoadByGuid("e21ac9492110493baef6df02a2682a0d").behaviorSpeculator;
				bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
				bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
				bs.TargetBehaviors = new List<TargetBehaviorBase>
			{
				new TargetPlayerBehavior
				{
					Radius = 1000f,
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

                    //ShootPoint = AttachPoint,

					//GroupCooldownVariance = 0.2f,
					//LineOfSight = false,
					//WeaponType = WeaponType.BulletScript,
					//OverrideBulletName = null,
					BulletScript = new CustomBulletScriptSelector(typeof(ChaosAttck)),
					//FixTargetDuringAttack = false,
					//StopDuringAttack = false,
					LeadAmount = 0.7f,
					//LeadChance = 0.62f,
					//RespectReload = true,
					//MagazineCapacity = 1,
					//ReloadSpeed = 3,
					//EmptiesClip = true,
					//SuppressReloadAnim = false,
					//TimeBetweenShots = 0.5f,
					PreventTargetSwitching = false,
					//OverrideAnimation = null,
					//OverrideDirectionalAnimation = null,
					HideGun = true,
					//UseLaserSight = false,
					//UseGreenLaser = false,
					//PreFireLaserTime = -1,
					//AimAtFacingDirectionWhenSafe = false,
					Cooldown = 1f,
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
					MaxUsages = 0,
				}
			};


				//Tools.DebugInformation(load);
				AIActor aIActor = EnemyDatabase.GetOrLoadByGuid("e21ac9492110493baef6df02a2682a0d");
				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;

				GameObject m_CachedGunAttachPoint = companion.transform.Find("GunAttachPoint").gameObject;
				EnemyBuilder.DuplicateAIShooterAndAIBulletBank(prefab, aIActor.aiShooter, aIActor.GetComponent<AIBulletBank>(), 734, m_CachedGunAttachPoint.transform);
				Game.Enemies.Add("bny:doppleganger", companion.aiActor);


			}
		}

		public static GameObject shootpoint;


		private static string[] spritePaths = new string[]
		{

			"BunnyMod/Resources/ChaosEnemy/DeathLeft/chaosbeing_die_left_001.png",

			"BunnyMod/Resources/ChaosEnemy/DeathRight/chaosbeing_die_right_001.png",


			"BunnyMod/Resources/ChaosEnemy/IdleLeft/chaosbeing_idle_left_001.png",
			"BunnyMod/Resources/ChaosEnemy/IdleLeft/chaosbeing_idle_left_002.png",
			"BunnyMod/Resources/ChaosEnemy/IdleLeft/chaosbeing_idle_left_003.png",
			"BunnyMod/Resources/ChaosEnemy/IdleLeft/chaosbeing_idle_left_004.png",

			"BunnyMod/Resources/ChaosEnemy/IdleRight/chaosbeing_idle_right_001.png",
			"BunnyMod/Resources/ChaosEnemy/IdleRight/chaosbeing_idle_right_002.png",
			"BunnyMod/Resources/ChaosEnemy/IdleRight/chaosbeing_idle_right_003.png",
			"BunnyMod/Resources/ChaosEnemy/IdleRight/chaosbeing_idle_right_004.png",

			"BunnyMod/Resources/ChaosEnemy/MoveRight/chaosbeing_idle_right_001.png",
			"BunnyMod/Resources/ChaosEnemy/MoveRight/chaosbeing_idle_right_002.png",
			"BunnyMod/Resources/ChaosEnemy/MoveRight/chaosbeing_idle_right_003.png",
			"BunnyMod/Resources/ChaosEnemy/IdleRight/chaosbeing_idle_right_004.png",

			"BunnyMod/Resources/ChaosEnemy/MoveLeft/chaosbeing_move_left_001.png",
			"BunnyMod/Resources/ChaosEnemy/MoveLeft/chaosbeing_move_left_002.png",
			"BunnyMod/Resources/ChaosEnemy/MoveLeft/chaosbeing_move_left_003.png",
			"BunnyMod/Resources/ChaosEnemy/MoveLeft/chaosbeing_move_left_004.png",


		};

		public class EnemyBehavior : BraveBehaviour
		{

			private void Start()
			{
				base.aiActor.healthHaver.OnPreDeath += (obj) =>
				{
					//replace later
					AkSoundEngine.PostEvent("Play_VO_lichB_death_01", base.aiActor.gameObject);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
					AkSoundEngine.PostEvent("Play_OBJ_chestwarp_use_01", gameObject);

				};
			}


		}

		public class ChaosAttck: Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				//BunnyModule.Log("Attempting to shoot bullets");
				float aimDirection = base.GetAimDirection((float)(((double)UnityEngine.Random.value >= 0.5) ? 1 : 0), 11f);
				for (int i = 0; i < 8; i++)
				{
					for (int A = -1; A < 2; A++)
                    {
						base.Fire(new Direction(aimDirection + (40* A), DirectionType.Absolute, -1f), new Speed(7f, SpeedType.Absolute), new ChaosAttck.SnakeBullet(i * 3));
					}

				}
				yield return base.Wait(40);

				//base.PostWwiseEvent("Play_VO_lichB_death_01", null);
				yield break;


				/*
				 * 				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				float startAngle = base.RandomAngle();
				float delta = 30f;
				for (int a = 0; a < 7; a++)
				{
					base.Fire(new Direction(base.RandomAngle(), DirectionType.Absolute, -1f), new Speed(4f, SpeedType.Absolute), new BubbleLizardBubble1.BubbleBullet());
					yield return base.Wait(5);
				}
				
				float startAngle = base.RandomAngle();
				float delta = 30f;
				for (int A = 0; A < 12; A++)
				{
					float num = startAngle + (float)A * delta;
					this.Fire(new Direction(num + 9, DirectionType.Absolute, -1f), new Speed(8.5f, SpeedType.Relative), new ChaosAttck.Break(base.Position, num));
					//yield return this.Wait(5);
				}
				yield return base.Wait(30);

				yield break;
				*/
			}


			// Token: 0x0400008D RID: 141
			private const int NumBullets = 8;

			// Token: 0x0400008E RID: 142
			private const int BulletSpeed = 11;

			// Token: 0x0400008F RID: 143
			private const float SnakeMagnitude = 0.6f;

			// Token: 0x04000090 RID: 144
			private const float SnakePeriod = 3f;

			// Token: 0x02000026 RID: 38
			public class SnakeBullet : Bullet
			{
				// Token: 0x0600008B RID: 139 RVA: 0x00004080 File Offset: 0x00002280
				public SnakeBullet(int delay) : base("snakeBullet", false, false, false)
				{
					this.delay = delay;
				}

				// Token: 0x0600008C RID: 140 RVA: 0x00004098 File Offset: 0x00002298
				protected override IEnumerator Top()
				{
					base.ManualControl = true;
					yield return base.Wait(this.delay);
					Vector2 truePosition = base.Position;
					for (int i = 0; i < 360; i++)
					{
						float offsetMagnitude = Mathf.SmoothStep(-1.6f, 1.6f, Mathf.PingPong(0.5f + (float)i / 60f * 3f, 1f));
						truePosition += BraveMathCollege.DegreesToVector(this.Direction, this.Speed / 60f);
						base.Position = truePosition + BraveMathCollege.DegreesToVector(this.Direction - 90f, offsetMagnitude);
						yield return base.Wait(1);
					}
					base.Vanish(false);
					yield break;
				}

				// Token: 0x04000091 RID: 145
				private int delay;
			}
		}
	}

}







