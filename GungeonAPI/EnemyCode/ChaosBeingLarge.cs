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
	public class ChaosBeingLarge : AIActor
	{
		public static GameObject prefab;
		public static readonly string guid = "ChaosBeingLarge";
		//private static tk2dSpriteCollectionData AbyssKinCollection;


		public static void Init()
		{

			ChaosBeingLarge.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			AIActor source = EnemyDatabase.GetOrLoadByGuid("e21ac9492110493baef6df02a2682a0d");
			bool flag = prefab != null || EnemyBuilder.Dictionary.ContainsKey(guid);
			bool flag2 = flag;
			if (!flag2)
			{
				prefab = EnemyBuilder.BuildPrefab("ChaosBeingLarge", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), false);
				var companion = prefab.AddComponent<EnemyBehavior>();
				ChaosBeing.shootpoint = new GameObject("fuck");
				ChaosBeing.shootpoint.transform.parent = companion.transform;
				ChaosBeing.shootpoint.transform.position = companion.sprite.WorldCenter;
				companion.aiActor.knockbackDoer.weight = 200000;
				companion.aiActor.MovementSpeed *= 1.1f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = true;
				companion.aiActor.aiAnimator.HitReactChance = 0f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = true;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(150);
				companion.aiActor.CollisionKnockbackStrength = 0f;
				companion.aiActor.CanTargetPlayers = true;
				companion.aiActor.healthHaver.SetHealthMaximum(150f, null, false);
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
					ManualWidth = 32,
					ManualHeight = 32,
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
					ManualWidth = 32,
					ManualHeight = 32,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0,



				});
				companion.aiActor.CorpseObject = EnemyDatabase.GetOrLoadByGuid("1a78cfb776f54641b832e92c44021cf2").CorpseObject;
				companion.aiActor.PreventBlackPhantom = false;
				AIAnimator aiAnimator = companion.aiAnimator;

				prefab.AddAnimation("idle_right", "BunnyMod/Resources/ChaosLarge/IdleRight/", fps:7, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("idle_left", "BunnyMod/Resources/ChaosLarge/IdleLeft/", fps: 7, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_right", "BunnyMod/Resources/ChaosLarge/MoveRight/", fps: 12, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("move_left", "BunnyMod/Resources/ChaosLarge/MoveLeft/", fps: 12, AnimationType.Move, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_left", "BunnyMod/Resources/ChaosLarge/DeathLeft/", fps: 7, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				prefab.AddAnimation("death_right", "BunnyMod/Resources/ChaosLarge/DeathRight/", fps: 7, AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				//prefab.AddAnimation("attack_left", "BunnyMod/Resources/ChaosLarge/AttackLeft/", fps: 7, AnimationType.Other, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				//prefab.AddAnimation("attack_right", "BunnyMod/Resources/ChaosLarge/AttackRight/", fps: 7, AnimationType.Other, DirectionalAnimation.DirectionType.TwoWayHorizontal);
				var bs = prefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("1a78cfb776f54641b832e92c44021cf2").behaviorSpeculator;
				BehaviorSpeculator load = EnemyDatabase.GetOrLoadByGuid("1a78cfb776f54641b832e92c44021cf2").behaviorSpeculator;
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
				new ShootBehavior() {

                    ShootPoint = AttachPoint,

					//GroupCooldownVariance = 0.2f,
					//LineOfSight = false,
					//WeaponType = WeaponType.BulletScript,
					//OverrideBulletName = null,
					BulletScript = new CustomBulletScriptSelector(typeof(ChaosAttckbeeg1)),
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
					Cooldown = 1.5f,
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
				},
				new ShootBehavior() {

					ShootPoint = AttachPoint,

					//GroupCooldownVariance = 0.2f,
					//LineOfSight = false,
					//WeaponType = WeaponType.BulletScript,
					//OverrideBulletName = null,
					BulletScript = new CustomBulletScriptSelector(typeof(ChaosAttckbeeg2)),
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
					Cooldown = 10f,
					CooldownVariance = 0,
					AttackCooldown = 0,
					GlobalCooldown = 3,
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

				//GameObject m_CachedGunAttachPoint = companion.transform.Find("GunAttachPoint").gameObject;
				//EnemyBuilder.DuplicateAIShooterAndAIBulletBank(prefab, aIActor.aiShooter, aIActor.GetComponent<AIBulletBank>(), ChaosRevolver.ChaosRevolverID, m_CachedGunAttachPoint.transform);
				Game.Enemies.Add("bny:chaos_being_large", companion.aiActor);


			}
		}

		public static GameObject shootpoint;


		private static string[] spritePaths = new string[]
		{

			"BunnyMod/Resources/ChaosLarge/DeathLeft/largechaos_death_left_001.png",

			"BunnyMod/Resources/ChaosLarge/DeathRight/largechaos_death_right_001.png",


			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_001.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_002.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_003.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_004.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_005.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_006.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_007.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_008.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_left_009.png",


			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_001.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_002.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_003.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_004.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_005.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_006.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_007.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_008.png",
			"BunnyMod/Resources/ChaosLarge/IdleLeft/largechaos_idle_right_009.png",

			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_001.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_002.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_003.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_004.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_005.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_006.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_007.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_008.png",
			"BunnyMod/Resources/ChaosLarge/MoveLeft/largechaos_move_left_009.png",

			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_001.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_002.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_003.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_004.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_005.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_006.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_007.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_008.png",
			"BunnyMod/Resources/ChaosLarge/MoveRight/largechaos_move_right_009.png",

			"BunnyMod/Resources/ChaosLarge/AttackLeft/largechaos_attack_left_001.png",
			"BunnyMod/Resources/ChaosLarge/AttackLeft/largechaos_attack_left_002.png",
			"BunnyMod/Resources/ChaosLarge/AttackLeft/largechaos_attack_left_003.png",
			"BunnyMod/Resources/ChaosLarge/AttackLeft/largechaos_attack_left_004.png",

			"BunnyMod/Resources/ChaosLarge/AttackRight/largechaos_attack_right_001.png",
			"BunnyMod/Resources/ChaosLarge/AttackRight/largechaos_attack_right_002.png",
			"BunnyMod/Resources/ChaosLarge/AttackRight/largechaos_attack_right_003.png",
			"BunnyMod/Resources/ChaosLarge/AttackRight/largechaos_attack_right_004.png"


		};

		public class EnemyBehavior : BraveBehaviour
		{

			private void Start()
			{
				base.aiActor.healthHaver.OnPreDeath += (obj) =>
				{
					PlayerController player = (GameManager.Instance.PrimaryPlayer);
					//replace later

					AkSoundEngine.PostEvent("Play_VO_lichB_death_01", base.aiActor.gameObject);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.healthHaver.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
					Projectile projectile = ((Gun)ETGMod.Databases.Items["black_hole_gun"]).DefaultModule.projectiles[0];
					GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, BraveMathCollege.Atan2Degrees(player.sprite.WorldCenter - base.aiActor.sprite.WorldCenter)), true);
					Projectile component = gameObject.GetComponent<Projectile>();
					bool flag = component != null;
					bool flag2 = flag;
					if (flag2)
					{
						component.Owner = base.aiActor;
						component.Shooter = base.aiActor.specRigidbody;
						component.baseData.speed = 2f;
						component.AdditionalScaleMultiplier *= 0.7f;
						component.baseData.damage = 0f;
					}
					AkSoundEngine.PostEvent("Play_OBJ_chestwarp_use_01", gameObject);

				};
			}
			public GameObject spawnedPlayerObject;
			public bool canBounce = true;
			public float tossForce;
			public GameObject objectToSpawn;

		}
		public class ChaosAttckbeeg2 : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				{
					
					base.PostWwiseEvent("Play_ENM_demonwall_barf_01", null);
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
					for (int u = 0; u < 3; u++)//i dont need any for scripts
                    {
						for (int i = 0; i < 4; i++)//i dont need any for scripts
						{
							int ae = 90 * i;
							int speedandspread2 = 10;
							for (int e = -4; e < 5; e++)
							{
								this.Fire(new Direction(0 + ((speedandspread2 / 2) * e + ae + (45*u) ), DirectionType.Aim, -1f), new Speed(5 - (float)Mathf.Abs(e) * 0.75f, SpeedType.Absolute), new ChaosAttckbeeg2.SnakeBullet());
							}
						}
						for (int i = 0; i < 4; i++)//i dont need any for scripts
						{
							int ae = 90 * i;
							int speedandspread2 = 10;
							for (int e = -4; e < 5; e++)
							{
								this.Fire(new Direction(0 + ((speedandspread2 / 2) * e + ae + (45 * u)), DirectionType.Aim, -1f), new Speed(15 - (float)Mathf.Abs(e) * 0.75f, SpeedType.Absolute), new ChaosAttckbeeg2.SnakeBullet());
							}
						}
					}

					yield break;
				}
			}
			public class SnakeBullet : Bullet
			{
				// Token: 0x0600008B RID: 139 RVA: 0x00004080 File Offset: 0x00002280
				public SnakeBullet() : base("snakeBullet", false, false, false)
				{
				}
			}
		}
		public class ChaosAttckbeeg1: Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
		{
			protected override IEnumerator Top() // This is just a simple example, but bullet scripts can do so much more.
			{
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("b1770e0f1c744d9d887cc16122882b4f").bulletBank.GetBullet("chain"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("6868795625bd46f3ae3e4377adce288b").bulletBank.GetBullet("dagger"));
				}
				//BunnyModule.Log("Attempting to shoot bullets");
				base.PostWwiseEvent("Play_BOSS_spacebaby_vomit_01", null);
				float aimDirection = base.GetAimDirection((float)(((double)UnityEngine.Random.value >= 0.5) ? 1 : 0), 11f);
				for (int i = 0; i < 12; i++)
				{
					base.Fire(new Direction(aimDirection, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new ChaosAttckbeeg1.SnakeBullet(i * 3));
					base.Fire(new Direction(aimDirection, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new ChaosAttckbeeg1.SnakeBullet1(i * 3));

				}
				yield return base.Wait(40);

				//base.PostWwiseEvent("Play_VO_lichB_death_01", null);
				yield break;



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
			public class SnakeBullet1 : Bullet
			{
				// Token: 0x0600008B RID: 139 RVA: 0x00004080 File Offset: 0x00002280
				public SnakeBullet1(int delay) : base("snakeBullet", false, false, false)
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
						base.Position = truePosition + BraveMathCollege.DegreesToVector(this.Direction + 90f, offsetMagnitude);
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







