using System;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;
//using DirectionType = DirectionalAnimation.DirectionType;
using AnimationType = ItemAPI.BossBuilder.AnimationType;
using System.Collections;
using Dungeonator;
using System.Linq;
using Brave.BulletScript;
using GungeonAPI;

namespace BunnyMod
{
	public class TheStranger : AIActor
	{
		public static GameObject fuckyouprefab;
		public static readonly string guid = "The_Stranger";
		private static tk2dSpriteCollectionData StagngerCollection;
		public static GameObject shootpoint;
		private static Texture2D BossCardTexture = ItemAPI.ResourceExtractor.GetTextureFromResource("BunnyMod/Resources/thestranger_bosscard.png");
		public static string TargetVFX;
		public static void Init()
		{

			TheStranger.BuildPrefab();
		}

		public static void BuildPrefab()
		{
			// source = EnemyDatabase.GetOrLoadByGuid("c50a862d19fc4d30baeba54795e8cb93");
			bool flag = fuckyouprefab != null || BossBuilder.Dictionary.ContainsKey(guid);
			bool flag2 = flag;
			if (!flag2)
			{
				fuckyouprefab = BossBuilder.BuildPrefab("TheStranger", guid, spritePaths[0], new IntVector2(0, 0), new IntVector2(8, 9), false, true);
				var companion = fuckyouprefab.AddComponent<EnemyBehavior>();
				companion.aiActor.knockbackDoer.weight = 200;
				companion.aiActor.MovementSpeed = 1.33f;
				companion.aiActor.healthHaver.PreventAllDamage = false;
				companion.aiActor.CollisionDamage = 1f;
				companion.aiActor.HasShadow = false;
				companion.aiActor.IgnoreForRoomClear = false;
				companion.aiActor.aiAnimator.HitReactChance = 0.05f;
				companion.aiActor.specRigidbody.CollideWithOthers = true;
				companion.aiActor.specRigidbody.CollideWithTileMap = true;
				companion.aiActor.PreventFallingInPitsEver = true;
				companion.aiActor.healthHaver.ForceSetCurrentHealth(600f);
				companion.aiActor.healthHaver.SetHealthMaximum(600f);
				companion.aiActor.CollisionKnockbackStrength = 2f;
				companion.aiActor.procedurallyOutlined = false;
				companion.aiActor.CanTargetPlayers = true;
				///	
				BunnyModule.Strings.Enemies.Set("#THE_STRANGER", "Stranger");
				BunnyModule.Strings.Enemies.Set("#????", "???");
				BunnyModule.Strings.Enemies.Set("#SUBTITLE", "Glocked And Loaded");
				BunnyModule.Strings.Enemies.Set("#QUOTE", "");
				companion.aiActor.healthHaver.overrideBossName = "#THE_STRANGER";
				companion.aiActor.OverrideDisplayName = "#THE_STRANGER";
				companion.aiActor.ActorName = "#THE_STRANGER";
				companion.aiActor.name = "#THE_STRANGER";
				fuckyouprefab.name = companion.aiActor.OverrideDisplayName;
				
				GenericIntroDoer miniBossIntroDoer = fuckyouprefab.AddComponent<GenericIntroDoer>();
				fuckyouprefab.AddComponent<TheStrangerIntro>();
				miniBossIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
				miniBossIntroDoer.initialDelay = 0.15f;
				miniBossIntroDoer.cameraMoveSpeed = 14;
				miniBossIntroDoer.specifyIntroAiAnimator = null;
				miniBossIntroDoer.BossMusicEvent = "Play_MUS_Boss_Theme_Beholster";
				miniBossIntroDoer.PreventBossMusic = false;
				miniBossIntroDoer.InvisibleBeforeIntroAnim = true;
				miniBossIntroDoer.preIntroAnim = string.Empty;
				miniBossIntroDoer.preIntroDirectionalAnim = string.Empty;
				miniBossIntroDoer.introAnim = "intro";
				miniBossIntroDoer.introDirectionalAnim = string.Empty;
				miniBossIntroDoer.continueAnimDuringOutro = false;
				miniBossIntroDoer.cameraFocus = null;
				miniBossIntroDoer.roomPositionCameraFocus = Vector2.zero;
				miniBossIntroDoer.restrictPlayerMotionToRoom = false;
				miniBossIntroDoer.fusebombLock = false;
				miniBossIntroDoer.AdditionalHeightOffset = 0;
				miniBossIntroDoer.portraitSlideSettings = new PortraitSlideSettings()
				{
					bossNameString = "#THE_STRANGER",
					bossSubtitleString = "#SUBTITLE",
					bossQuoteString = "#QUOTE",
					bossSpritePxOffset = IntVector2.Zero,
					topLeftTextPxOffset = IntVector2.Zero,
					bottomRightTextPxOffset = IntVector2.Zero,
					bgColor = Color.yellow
				};
				if (BossCardTexture)
				{
					miniBossIntroDoer.portraitSlideSettings.bossArtSprite = BossCardTexture;
					miniBossIntroDoer.SkipBossCard = false;
					companion.aiActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.MainBar;
				}
				else
				{
					miniBossIntroDoer.SkipBossCard = true;
					companion.aiActor.healthHaver.bossHealthBar = HealthHaver.BossBarType.SubbossBar;
				}
				miniBossIntroDoer.SkipFinalizeAnimation = true;
				miniBossIntroDoer.RegenerateCache();
			
				//BehaviorSpeculator aIActor = EnemyDatabase.GetOrLoadByGuid("465da2bb086a4a88a803f79fe3a27677").behaviorSpeculator;
				//Tools.DebugInformation(aIActor);

				/////







				companion.aiActor.healthHaver.SetHealthMaximum(600f, null, false);
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
					ManualOffsetY = 10,
					ManualWidth = 16,
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
					ManualOffsetY = 10,
					ManualWidth = 16,
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
				aiAnimator.IdleAnimation = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "idle",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				DirectionalAnimation anim = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
					AnimNames = new string[]
					{
						"flare",

					},
					Flipped = new DirectionalAnimation.FlipType[2]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "flare",
						anim = anim
					}
				};
				DirectionalAnimation anim2 = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
					AnimNames = new string[]
					{
						"brrap",

					},
					Flipped = new DirectionalAnimation.FlipType[2]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "brrap",
						anim = anim2
					}
				};
				DirectionalAnimation anim3 = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					AnimNames = new string[]
					{
						"tell",

					},
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "tell",
						anim = anim3
					}
				};
				DirectionalAnimation Hurray = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "fire1",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "fire1",
						anim = Hurray
					}
				};
				DirectionalAnimation almostdone = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "intro",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "intro",
						anim = almostdone
					}
				};
				DirectionalAnimation done = new DirectionalAnimation
				{
					Type = DirectionalAnimation.DirectionType.Single,
					Prefix = "death",
					AnimNames = new string[1],
					Flipped = new DirectionalAnimation.FlipType[1]
				};
				aiAnimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>
				{
					new AIAnimator.NamedDirectionalAnimation
					{
						name = "death",
						anim = done
					}
				};
				bool flag3 = StagngerCollection == null;
				if (flag3)
				{
					StagngerCollection = SpriteBuilder.ConstructCollection(fuckyouprefab, "The_Stranger_Collection");
					UnityEngine.Object.DontDestroyOnLoad(StagngerCollection);
					for (int i = 0; i < spritePaths.Length; i++)
					{
						SpriteBuilder.AddSpriteToCollection(spritePaths[i], StagngerCollection);
					}
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{

					0,
					1,
					2,
					3

					}, "idle", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 3f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{

					4,
					5,
					6,
					7,
					8,
					9,
					10,
					11,
					12,
					13,
					14,


					}, "flare", tk2dSpriteAnimationClip.WrapMode.Once).fps = 12f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{

					15,
					16,
					17,
					18,
					19,
					20,
					21,
					22,
					23,
					24,
					25,
					26,
					27,
					28,
					29,
					30,


					}, "brrap", tk2dSpriteAnimationClip.WrapMode.Once).fps = 8f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{

					31,
					32,
					33,
					34,
					35,
					36,


					}, "tell", tk2dSpriteAnimationClip.WrapMode.Once).fps = 8f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{
					37,
					38,
					39,
					40,
					41,
					42,
					43,
					44,



					}, "fire1", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{
				45,
				46,
				47,
				48,
				49,
				50,
				51,
				52,
				53,
				54,
				55,
				56,
				57,
				58,
				59,
				60,
				61,
				
					}, "intro", tk2dSpriteAnimationClip.WrapMode.Once).fps = 5f;
					SpriteBuilder.AddAnimation(companion.spriteAnimator, StagngerCollection, new List<int>
					{
				62,
				63,
				64,
				65,
				66,
				67,
				68,
				69,
				70,
				
					}, "death", tk2dSpriteAnimationClip.WrapMode.Once).fps = 6f;

				}
				var bs = fuckyouprefab.GetComponent<BehaviorSpeculator>();
				BehaviorSpeculator behaviorSpeculator = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").behaviorSpeculator;
				bs.OverrideBehaviors = behaviorSpeculator.OverrideBehaviors;
				bs.OtherBehaviors = behaviorSpeculator.OtherBehaviors;
				shootpoint = new GameObject("attach");
				shootpoint.transform.parent = companion.transform;
				shootpoint.transform.position = companion.sprite.WorldCenter;
				GameObject m_CachedGunAttachPoint = companion.transform.Find("attach").gameObject;
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

				bs.AttackBehaviorGroup.AttackBehaviors = new List<AttackBehaviorGroup.AttackGroupItem>
				{

					new AttackBehaviorGroup.AttackGroupItem()
					{

						Probability = 0.4f,
						Behavior = new ShootBehavior{
						ShootPoint = m_CachedGunAttachPoint,
					   BulletScript = new BunnyMod.CustomBulletScriptSelector(typeof(SwirlScript)),
					   LeadAmount = 0f,
					   AttackCooldown = 1.5f,
					   Cooldown = 5,
					   InitialCooldown = 8f,
					    FireAnimation = "flare",
						RequiresLineOfSight = false,
						StopDuring = ShootBehavior.StopType.Attack,
						Uninterruptible = true,
						},
						NickName = "FireFlare"

					},
					new AttackBehaviorGroup.AttackGroupItem()
					{
						Probability = 1f,
						Behavior = new ShootBehavior{
						ShootPoint = m_CachedGunAttachPoint,
						BulletScript = new BunnyMod.CustomBulletScriptSelector(typeof(BrrapScript)),
						LeadAmount = 0f,
						AttackCooldown = 1.5f,
						FireAnimation = "brrap",
						RequiresLineOfSight = false,
						StopDuring = ShootBehavior.StopType.Attack,
						Uninterruptible = true
						},
						NickName = "BRRRRRRRRAP"
					},
					new AttackBehaviorGroup.AttackGroupItem()
					{
						Probability = 0.25f,
						Behavior = new ShootBehavior{
						ShootPoint = m_CachedGunAttachPoint,
						BulletScript = new BunnyMod.CustomBulletScriptSelector(typeof(GrenadeChuck)),
						LeadAmount = 0f,
						MaxUsages = 3,
						Cooldown = 7f,
						InitialCooldown = 3f,

						AttackCooldown = 1.66f,
						TellAnimation = "tell",
						FireAnimation = "fire1",
						RequiresLineOfSight = false,
						StopDuring = ShootBehavior.StopType.Attack,
						Uninterruptible = true
						},
						NickName = "Grenade Toss."
					},
					new AttackBehaviorGroup.AttackGroupItem()
					{
						Probability = 0.7f,
						Behavior = new ShootBehavior{
						ShootPoint = m_CachedGunAttachPoint,
						BulletScript = new BunnyMod.CustomBulletScriptSelector(typeof(CannonScript)),
						LeadAmount = 0f,
						AttackCooldown = 1.2f,
						TellAnimation = "tell",
						FireAnimation = "fire1",
						RequiresLineOfSight = false,
						Cooldown = 6f,

						StopDuring = ShootBehavior.StopType.Attack,
						Uninterruptible = true
						},
						NickName = "CANNON"
					},
					new AttackBehaviorGroup.AttackGroupItem()
					{
						Probability = 1f,
						Behavior = new ShootBehavior{
						ShootPoint = m_CachedGunAttachPoint,
						BulletScript = new BunnyMod.CustomBulletScriptSelector(typeof(SniperScript)),
						LeadAmount = 0f,
						AttackCooldown = 2f,
						Cooldown = 5f,

						TellAnimation = "tell",
						FireAnimation = "fire1",
						RequiresLineOfSight = false,
						StopDuring = ShootBehavior.StopType.Attack,
						Uninterruptible = true
						},
						NickName = "Sniper"
					},

				};
				bs.InstantFirstTick = behaviorSpeculator.InstantFirstTick;
				bs.TickInterval = behaviorSpeculator.TickInterval;
				bs.PostAwakenDelay = behaviorSpeculator.PostAwakenDelay;
				bs.RemoveDelayOnReinforce = behaviorSpeculator.RemoveDelayOnReinforce;
				bs.OverrideStartingFacingDirection = behaviorSpeculator.OverrideStartingFacingDirection;
				bs.StartingFacingDirection = behaviorSpeculator.StartingFacingDirection;
				bs.SkipTimingDifferentiator = behaviorSpeculator.SkipTimingDifferentiator;
				Game.Enemies.Add("bny:the_stranger", companion.aiActor);

			}
		}
		public static List<int> Lootdrops = new List<int>
		{
			73,
			85,
			120,
			67,
			224,
			600,
			78
		};





		private static string[] spritePaths = new string[]
		{
			
			//idles
			"BunnyMod/TheStranger/stranger_idle_001.png",
			"BunnyMod/TheStranger/stranger_idle_002.png",
			"BunnyMod/TheStranger/stranger_idle_003.png",
			"BunnyMod/TheStranger/stranger_idle_004.png",
			//flare
			"BunnyMod/TheStranger/stranger_fireup_001.png",
			"BunnyMod/TheStranger/stranger_fireup_002.png",
			"BunnyMod/TheStranger/stranger_fireup_003.png",
			"BunnyMod/TheStranger/stranger_fireup_004.png",
			"BunnyMod/TheStranger/stranger_fireup_005.png",
			"BunnyMod/TheStranger/stranger_fireup_006.png",
			"BunnyMod/TheStranger/stranger_fireup_007.png",
			"BunnyMod/TheStranger/stranger_fireup_008.png",
			"BunnyMod/TheStranger/stranger_fireup_009.png",
			"BunnyMod/TheStranger/stranger_fireup_010.png",
			"BunnyMod/TheStranger/stranger_fireup_011.png",

			//brrap
			"BunnyMod/TheStranger/stranger_burstfire_001.png",
			"BunnyMod/TheStranger/stranger_burstfire_002.png",
			"BunnyMod/TheStranger/stranger_burstfire_003.png",
			"BunnyMod/TheStranger/stranger_burstfire_004.png",
			"BunnyMod/TheStranger/stranger_burstfire_005.png",
			"BunnyMod/TheStranger/stranger_burstfire_006.png",
			"BunnyMod/TheStranger/stranger_burstfire_007.png",
			"BunnyMod/TheStranger/stranger_burstfire_008.png",
			"BunnyMod/TheStranger/stranger_burstfire_009.png",
			"BunnyMod/TheStranger/stranger_burstfire_010.png",
			"BunnyMod/TheStranger/stranger_burstfire_011.png",
			"BunnyMod/TheStranger/stranger_burstfire_012.png",
			"BunnyMod/TheStranger/stranger_burstfire_013.png",
			"BunnyMod/TheStranger/stranger_burstfire_014.png",
			"BunnyMod/TheStranger/stranger_burstfire_015.png",
			"BunnyMod/TheStranger/stranger_burstfire_016.png",

			//tell
			"BunnyMod/TheStranger/stranger_tell1_001.png",
			"BunnyMod/TheStranger/stranger_tell1_002.png",
			"BunnyMod/TheStranger/stranger_tell1_003.png",
			"BunnyMod/TheStranger/stranger_tell1_004.png",
			"BunnyMod/TheStranger/stranger_tell1_005.png",
			"BunnyMod/TheStranger/stranger_tell1_006.png",

			//fire1
			"BunnyMod/TheStranger/stranger_fire1_001.png",
			"BunnyMod/TheStranger/stranger_fire1_002.png",
			"BunnyMod/TheStranger/stranger_fire1_003.png",
			"BunnyMod/TheStranger/stranger_fire1_004.png",
			"BunnyMod/TheStranger/stranger_fire1_005.png",
			"BunnyMod/TheStranger/stranger_fire1_006.png",
			"BunnyMod/TheStranger/stranger_fire1_007.png",
			"BunnyMod/TheStranger/stranger_fire1_008.png",
			//intro /
			"BunnyMod/TheStranger/stranger_intro_001.png",
			"BunnyMod/TheStranger/stranger_intro_002.png",
			"BunnyMod/TheStranger/stranger_intro_003.png",
			"BunnyMod/TheStranger/stranger_intro_004.png",
			"BunnyMod/TheStranger/stranger_intro_005.png",
			"BunnyMod/TheStranger/stranger_intro_006.png",
			"BunnyMod/TheStranger/stranger_intro_007.png",
			"BunnyMod/TheStranger/stranger_intro_008.png",
			"BunnyMod/TheStranger/stranger_intro_009.png",
			"BunnyMod/TheStranger/stranger_intro_010.png",
			"BunnyMod/TheStranger/stranger_intro_011.png",
			"BunnyMod/TheStranger/stranger_intro_012.png",
			"BunnyMod/TheStranger/stranger_intro_013.png",
			"BunnyMod/TheStranger/stranger_intro_014.png",
			"BunnyMod/TheStranger/stranger_intro_015.png",
			"BunnyMod/TheStranger/stranger_intro_016.png",
			"BunnyMod/TheStranger/stranger_intro_017.png",

			//die /
			"BunnyMod/TheStranger/stranger_death_001.png",
			"BunnyMod/TheStranger/stranger_death_002.png",
			"BunnyMod/TheStranger/stranger_death_003.png",
			"BunnyMod/TheStranger/stranger_death_004.png",
			"BunnyMod/TheStranger/stranger_death_005.png",
			"BunnyMod/TheStranger/stranger_death_006.png",
			"BunnyMod/TheStranger/stranger_death_007.png",
			"BunnyMod/TheStranger/stranger_death_008.png",
			"BunnyMod/TheStranger/stranger_death_009.png",


				};
	}

	public class SwirlScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
		protected override IEnumerator Top()
		{
			if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("4d164ba3f62648809a4a82c90fc22cae").bulletBank.GetBullet("big_one"));
			}
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			yield return base.Wait(20);
			base.PostWwiseEvent("Play_BOSS_RatMech_Whistle_01", null);
			yield return base.Wait(80);
			base.Fire(Offset.OverridePosition(player.sprite.WorldBottomCenter + new Vector2(0f, 30f)), new Direction(-90f, DirectionType.Absolute, -1f), new Speed(30f, SpeedType.Absolute), new SwirlScript.BigBullet());
			yield break;
		}
		private class BigBullet : Bullet
		{
			// Token: 0x060009DC RID: 2524 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
			public BigBullet() : base("big_one", false, false, false)
			{
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x0002FD00 File Offset: 0x0002DF00
			public override void Initialize()
			{
				this.Projectile.spriteAnimator.StopAndResetFrameToDefault();
				base.Initialize();
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x0002FD18 File Offset: 0x0002DF18
			protected override IEnumerator Top()
			{
				this.Projectile.specRigidbody.CollideWithTileMap = false;
				this.Projectile.specRigidbody.CollideWithOthers = false;
				yield return base.Wait(60);
				base.PostWwiseEvent("Play_BOSS_RatMech_Stomp_01", null);
				this.Speed = 0f;
				this.Projectile.spriteAnimator.Play();
				base.Vanish(true);
				yield break;
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x0002FD34 File Offset: 0x0002DF34
			public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
			{
				if (!preventSpawningProjectiles)
				{
					float num = base.RandomAngle();
					float Amount = 30;
					float Angle = 360 / Amount;
					for (int i = 0; i < Amount; i++)
					{
						base.Fire(new Direction(num + Angle * (float)i+18, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), new SpeedUpBullet());
						base.Fire(new Direction(num + Angle * (float)i + 18, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new WallBullet());
						//base.Fire(new Direction(num + Angle * (float)i + 12, DirectionType.Absolute, -1f), new Speed(9f, SpeedType.Absolute), new WallBullet());
						//base.Fire(new Direction(num + Angle * (float)i + 6, DirectionType.Absolute, -1f), new Speed(7f, SpeedType.Absolute), new WallBullet());
					}
					return;
				}
			}
		}
	}
		

	public class GrenadeChuck : Script
	{
		protected override IEnumerator Top()
		{
			if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").bulletBank.GetBullet("grenade"));
			}
			base.PostWwiseEvent("Play_BOSS_lasthuman_volley_01", null);
			float airTime = base.BulletBank.GetBullet("grenade").BulletObject.GetComponent<ArcProjectile>().GetTimeInFlight();
			Vector2 vector = this.BulletManager.PlayerPosition();
			Bullet bullet2 = new Bullet("grenade", false, false, false);
			float direction2 = (vector - base.Position).ToAngle();
			base.Fire(new Direction(direction2, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet2);
			(bullet2.Projectile as ArcProjectile).AdjustSpeedToHit(vector);
			bullet2.Projectile.ImmuneToSustainedBlanks = true;
			//yield return base.Wait(150);
			for (int a = 0; a < 2; a++)
            {
				base.PostWwiseEvent("Play_BOSS_lasthuman_volley_01", null);
				for (int i = 0; i < 4; i++)
				{
					for (int h = 0; h < 2; h++)
					{
						base.Fire(new Direction(UnityEngine.Random.Range(-75f, 75f), DirectionType.Aim, -1f), new Speed(7.5f + h, SpeedType.Absolute), new WallBullet());
						yield return base.Wait(1);
					}
					yield return base.Wait(12);
					Vector2 targetVelocity = this.BulletManager.PlayerVelocity();
					float startAngle;
					float dist;
					if (targetVelocity != Vector2.zero && targetVelocity.magnitude > 0.5f)
					{
						startAngle = targetVelocity.ToAngle();
						dist = targetVelocity.magnitude * airTime;
					}
					else
					{
						startAngle = base.RandomAngle();
						dist = (7f*a) * airTime;
					}
					float angle = base.SubdivideCircle(startAngle, 4, i, 1f, false);
					Vector2 targetPoint = this.BulletManager.PlayerPosition() + BraveMathCollege.DegreesToVector(angle, dist);
					float direction = (targetPoint - base.Position).ToAngle();
					if (i > 0)
					{
						direction += UnityEngine.Random.Range(-12.5f, 12.5f);
					}
					Bullet bullet = new Bullet("grenade", false, false, false);
					base.Fire(new Direction(direction, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet);
					(bullet.Projectile as ArcProjectile).AdjustSpeedToHit(targetPoint);
					bullet.Projectile.ImmuneToSustainedBlanks = true;
				}
			}

			yield break;
		}
	}

	public class SpawnSkeletonBullet : Bullet
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x000085A7 File Offset: 0x000067A7
		public SpawnSkeletonBullet() : base("skull", false, false, false)
		{
		}
		/*
		public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
		{
			if (preventSpawningProjectiles)
			{
				return;
			}
			var list = new List<string> {
				//"shellet",
				"336190e29e8a4f75ab7486595b700d4a"
			};
			string guid = BraveUtility.RandomElement<string>(list);
			var Enemy = EnemyDatabase.GetOrLoadByGuid(guid);
			AIActor.Spawn(Enemy.aiActor, this.Projectile.sprite.WorldCenter, GameManager.Instance.PrimaryPlayer.CurrentRoom, true, AIActor.AwakenAnimationType.Default, true);
		}
		*/
	}


	public class BrrapScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
		protected override IEnumerator Top()
		{
			if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("796a7ed4ad804984859088fc91672c7f").bulletBank.GetBullet("default"));
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("1bc2a07ef87741be90c37096910843ab").bulletBank.GetBullet("reversible"));
			}
			for (int u = 0; u < 4; u++)
            {
				int scatter = UnityEngine.Random.Range(4, 7);
				AkSoundEngine.PostEvent("Play_WPN_golddoublebarrelshotgun_shot_01", this.BulletBank.aiActor.gameObject);
				for (int k = 0; k < scatter; k++)
				{
					base.Fire(new Direction(UnityEngine.Random.Range(-25f, 25f), DirectionType.Aim, -1f), new Speed(UnityEngine.Random.Range(9f, 13f), SpeedType.Absolute), new WallBullet());
				}
				yield return Wait(scatter-1);
				base.Fire(new Direction(UnityEngine.Random.Range(-60f, 60f), DirectionType.Aim, -1f), new Speed(UnityEngine.Random.Range(5f, 7f), SpeedType.Absolute), new SpeedUpBullet());
				base.Fire(new Direction(UnityEngine.Random.Range(-60f, 60f), DirectionType.Aim, -1f), new Speed(UnityEngine.Random.Range(6f, 8f), SpeedType.Absolute), new SpeedUpBullet());
				yield return Wait(4);
			}
			yield break;
		}

	}
	public class SniperScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
		protected override IEnumerator Top()
		{
			if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("796a7ed4ad804984859088fc91672c7f").bulletBank.GetBullet("default"));
			}
			int shots = UnityEngine.Random.Range(2, 6);
			for (int e = 0; e < shots; e++)
            {
				base.PostWwiseEvent("Play_WPN_sniperrifle_shot_01", null);
				for (int u = -2; u < 1; u++)
				{
					for (int h = 0; h < 2; h++)
					{
						base.Fire(new Direction((25 * u), DirectionType.Aim, -1f), new Speed(11.5f + h, SpeedType.Absolute), new WallBullet());

					}

				}
				for (int p = 0; p < 4; p++)
				{
					base.Fire(new Direction(UnityEngine.Random.Range(-60f, 60f), DirectionType.Aim, -1f), new Speed(UnityEngine.Random.Range(8f, 10f), SpeedType.Absolute), new WallBullet());
				}
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(12.5f- (shots/3), SpeedType.Absolute), new WallBullet());
				base.PostWwiseEvent("Play_WPN_sniperrifle_shot_01", null);
				yield return Wait(15 + shots);
				for (int u = 0; u < 1; u++)
				{
					for (int h = 0; h < 2; h++)
					{
						base.Fire(new Direction(25f, DirectionType.Aim, -1f), new Speed(11.5f + h, SpeedType.Absolute), new WallBullet());
						base.Fire(new Direction(-25f, DirectionType.Aim, -1f), new Speed(11.5f + h, SpeedType.Absolute), new WallBullet());
					}
					for (int p = 0; p < 3; p++)
					{
						base.Fire(new Direction(UnityEngine.Random.Range(-60f, 60f), DirectionType.Aim, -1f), new Speed(UnityEngine.Random.Range(8f, 10f), SpeedType.Absolute), new WallBullet());
					}

				}
				base.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(12.5f- (shots / 3), SpeedType.Absolute), new WallBullet());
				yield return Wait(15+shots);
			}	
			base.PostWwiseEvent("Play_ENM_hammer_target_01", null);
			yield return Wait(30);
			base.PostWwiseEvent("Play_BOSS_RatMech_Stomp_01", null);
			for (int u = 0; u < 4; u++)
			{
				for (int h = 0; h < 20; h++)
				{
					base.Fire(new Direction(18 * h+(9*u), DirectionType.Aim, -1f), new Speed(11 + u, SpeedType.Absolute), new WallBullet());

				}
			}

			yield break;
		}

	}
	public class CannonScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
		protected override IEnumerator Top()
		{
			if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("da797878d215453abba824ff902e21b4").bulletBank.GetBullet("snakeBullet"));
			}
			AkSoundEngine.PostEvent("Play_BOSS_RatMech_Cannon_01", base.BulletBank.gameObject);
			this.Fire(new Direction(0f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new CannonScript.SplitBall());
			this.Fire(new Direction(120f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new CannonScript.SplitBall());
			this.Fire(new Direction(240f, DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new CannonScript.SplitBall());

			yield break;
			//yield return base.Wait(20);
		}
		public class SplitBall : Bullet
		{
			// Token: 0x06000007 RID: 7 RVA: 0x000021A2 File Offset: 0x000003A2
			public SplitBall() : base("bigBullet", false, false, false)
			{
			}
			protected override IEnumerator Top()
			{
				base.ChangeSpeed(new Speed(30f, SpeedType.Absolute), 120);
				yield break;
			}
			public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
			{
				if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
				{
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("ec6b674e0acd4553b47ee94493d66422").bulletBank.GetBullet("bigBullet"));
					base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("1bc2a07ef87741be90c37096910843ab").bulletBank.GetBullet("reversible"));
				}
				if (!preventSpawningProjectiles)
				{
					float num = base.RandomAngle();
					float Amount = 16;
					float Angle = 360 / Amount;
					for (int i = 0; i < Amount; i++)
					{
						base.Fire(new Direction(num + Angle * (float)i, DirectionType.Absolute, -1f), new Speed(10f, SpeedType.Absolute), new WallBullet());
					}
					for (int i = -1; i < 2; i++)
					{
						base.Fire(new Direction((10*i), DirectionType.Aim, -1f), new Speed(1f, SpeedType.Absolute), new SpeedUpBullet());
					}
				}
			}
		}
			// Token: 0x04000091 RID: 145		
	}


	public class BurstBullet : Bullet
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x000085A7 File Offset: 0x000067A7
		public BurstBullet() : base("reversible", false, false, false)
		{
		}

		protected override IEnumerator Top()
		{
			this.Projectile.spriteAnimator.Play();
			yield break;
		}
		public override void OnBulletDestruction(Bullet.DestroyType destroyType, SpeculativeRigidbody hitRigidbody, bool preventSpawningProjectiles)
		{
			if (preventSpawningProjectiles)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				base.Fire(new Direction((float)(i * 45), DirectionType.Absolute, -1f), new Speed(7f, SpeedType.Absolute), new WallBullet());
			}
		}
	}

	public class SpeedUpBullet : Bullet
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x00030B38 File Offset: 0x0002ED38
		public SpeedUpBullet() : base("reversible", false, false, false)
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00030B48 File Offset: 0x0002ED48
		protected override IEnumerator Top()
		{
			float speed = this.Speed;
			yield return this.Wait(100);
			this.ChangeSpeed(new Speed(12f, SpeedType.Absolute), 20);
			yield break;
		}
	}

	public class WallBullet : Bullet
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x00030B38 File Offset: 0x0002ED38
		public WallBullet() : base("default", false, false, false)
		{
		}

	}
	public class ChuckScript : Script // This BulletScript is just a modified version of the script BulletManShroomed, which you can find with dnSpy.
	{
        protected override IEnumerator Top()
		{
			if (this.BulletBank && this.BulletBank.aiActor && this.BulletBank.aiActor.TargetRigidbody)
			{
				base.BulletBank.Bullets.Add(EnemyDatabase.GetOrLoadByGuid("880bbe4ce1014740ba6b4e2ea521e49d").bulletBank.GetBullet("grenade"));
			}
			float airTime = base.BulletBank.GetBullet("grenade").BulletObject.GetComponent<ArcProjectile>().GetTimeInFlight();
			Vector2 vector = this.BulletManager.PlayerPosition();
			Bullet bullet2 = new Bullet("grenade", false, false, false);
			float direction2 = (vector - base.Position).ToAngle();
			base.Fire(new Direction(direction2, DirectionType.Absolute, -1f), new Speed(1f, SpeedType.Absolute), bullet2);
			(bullet2.Projectile as ArcProjectile).AdjustSpeedToHit(vector);
			bullet2.Projectile.ImmuneToSustainedBlanks = true;
			yield break;
		}
		
	}
		
	

	public class EnemyBehavior : BraveBehaviour
	{




		private void Start()
		{
			//base.aiActor.HasBeenEngaged = true;
			base.aiActor.healthHaver.OnPreDeath += (obj) =>
			{
				//AkSoundEngine.PostEvent("Play_ENM_beholster_death_01", base.aiActor.gameObject);
				//Chest chest2 = GameManager.Instance.RewardManager.SpawnTotallyRandomChest(spawnspot)rg;
				//chest2.IsLocked = false;

			};
			base.healthHaver.healthHaver.OnDeath += (obj) =>
			{
				float itemsToSpawn = UnityEngine.Random.Range(3, 6);
                float spewItemDir = 360 / itemsToSpawn;
               // new Vector2(spewItemDir * itemsToSpawn, 0);

				for (int i = 0; i < itemsToSpawn; i++)
                {
					int id = BraveUtility.RandomElement<int>(TheStranger.Lootdrops);
					LootEngine.SpawnItem(PickupObjectDatabase.GetById(id).gameObject, base.aiActor.sprite.WorldCenter, new Vector2(spewItemDir * itemsToSpawn, spewItemDir * itemsToSpawn), 2.2f, false, true, false);
				}

				Chest chest2 = GameManager.Instance.RewardManager.SpawnTotallyRandomChest(GameManager.Instance.PrimaryPlayer.CurrentRoom.GetRandomVisibleClearSpot(1, 1));
				chest2.IsLocked = false;

			}; ;
			this.aiActor.knockbackDoer.SetImmobile(true, "nope.");

		}


	}

}







