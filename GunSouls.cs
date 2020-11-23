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

//this has all the Gun Soul code for EACH individual one, which is why its so bulky

namespace BunnyMod
{
    public class GunSoulBlue : CompanionItem
    {
        public static void BlueBuildPrefab()
        {

            bool flag = GunSoulBlue.GunSoulBluePrefab != null || CompanionBuilder.companionDictionary.ContainsKey(GunSoulBlue.guid1);
            bool flag2 = flag;
            if (!flag2)
            {
                GunSoulBlue.GunSoulBluePrefab = CompanionBuilder.BuildPrefab("BlueGunSoul", GunSoulBlue.guid1, GunSoulBlue.spritePaths[0], new IntVector2(3, 2), new IntVector2(8, 9));
                GunSoulBlue.GunSoulBlueBehaviour GunSoulBlueBehavior = GunSoulBlue.GunSoulBluePrefab.AddComponent<GunSoulBlue.GunSoulBlueBehaviour>();
                AIAnimator aiAnimator = GunSoulBlueBehavior.aiAnimator;
                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "run_right",
                        "run_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_right",
                        "idle_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "attack_right",
                        "attack_left"
                    }
                };
                bool flag3 = GunSoulBlue.GunSoulBlueCollection == null;
                if (flag3)
                {
                    GunSoulBlue.GunSoulBlueCollection = SpriteBuilder.ConstructCollection(GunSoulBlue.GunSoulBluePrefab, "Penguin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(GunSoulBlue.GunSoulBlueCollection);
                    for (int i = 0; i < GunSoulBlue.spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(GunSoulBlue.spritePaths[i], GunSoulBlue.GunSoulBlueCollection);
                    }
                    SpriteBuilder.AddAnimation(GunSoulBlueBehavior.spriteAnimator, GunSoulBlue.GunSoulBlueCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;
                    SpriteBuilder.AddAnimation(GunSoulBlueBehavior.spriteAnimator, GunSoulBlue.GunSoulBlueCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;
                    SpriteBuilder.AddAnimation(GunSoulBlueBehavior.spriteAnimator, GunSoulBlue.GunSoulBlueCollection, new List<int>
                    {
                        8,
                        9,
                        10,
                        11,
                        12,
                        13,
                        14,
                        15
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;
                    SpriteBuilder.AddAnimation(GunSoulBlueBehavior.spriteAnimator, GunSoulBlue.GunSoulBlueCollection, new List<int>
                    {
                        16,
                        17,
                        18,
                        19,
                        20,
                        21,
                        22,
                        23
                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;
                    SpriteBuilder.AddAnimation(GunSoulBlueBehavior.spriteAnimator, GunSoulBlue.GunSoulBlueCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;
                    SpriteBuilder.AddAnimation(GunSoulBlueBehavior.spriteAnimator, GunSoulBlue.GunSoulBlueCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 8f;
                }
                GunSoulBlueBehavior.aiActor.MovementSpeed = 5f;
                GunSoulBlueBehavior.CanInterceptBullets = true;
                GunSoulBlueBehavior.aiActor.healthHaver.PreventAllDamage = false;
                GunSoulBlueBehavior.aiActor.specRigidbody.CollideWithOthers = true;
                GunSoulBlueBehavior.aiActor.specRigidbody.CollideWithTileMap = false;
                GunSoulBlueBehavior.aiActor.healthHaver.ForceSetCurrentHealth(100f);
                GunSoulBlueBehavior.aiActor.healthHaver.SetHealthMaximum(100f, null, false);
                GunSoulBlueBehavior.aiActor.specRigidbody.PixelColliders.Clear();
                GunSoulBlueBehavior.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
                {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyBulletBlocker,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 10,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                });
                BehaviorSpeculator behaviorSpeculator = GunSoulBlueBehavior.behaviorSpeculator;
                behaviorSpeculator.AttackBehaviors.Add(new GunSoulBlue.GunSoulBlueAttackBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new GunSoulBlue.ApproachEnemiesBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior
                {
                    IdleAnimations = new string[]
                    {
                        "idle"
                    }
                });
                UnityEngine.Object.DontDestroyOnLoad(GunSoulBlue.GunSoulBluePrefab);
                FakePrefab.MarkAsFakePrefab(GunSoulBlue.GunSoulBluePrefab);
                GunSoulBlue.GunSoulBluePrefab.SetActive(false);
            }
        }

        // Token: 0x04000016 RID: 22
        public static GameObject GunSoulBluePrefab;

        // Token: 0x04000017 RID: 23
        public static readonly string guid1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        private List<CompanionController> companionsSpawned = new List<CompanionController>();


        // Token: 0x04000018 RID: 24
        private static string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_001.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_002.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_003.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_004.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_005.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_006.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_007.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Idle/gunsoulblue_idle_008.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_001.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_002.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_003.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_004.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_005.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_006.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_007.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Left/gunsoulblue_moveleft_008.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_001.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_002.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_003.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_004.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_005.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_006.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_007.png",
            "BunnyMod/Resources/GunSoulSprites/Blue/Right/gunsoulblue_moveright_008.png",

        };

        // Token: 0x04000019 RID: 25
        private static tk2dSpriteCollectionData GunSoulBlueCollection;

        // Token: 0x02000031 RID: 49
        public class GunSoulBlueBehaviour : CompanionController
        {
            // Token: 0x06000119 RID: 281 RVA: 0x0000A9D9 File Offset: 0x00008BD9
            private void Start()
            {
                base.spriteAnimator.Play("idle");
                this.Owner = this.m_owner;
            }

            // Token: 0x04000079 RID: 121
            public PlayerController Owner;
        }

        // Token: 0x02000032 RID: 50
        public class GunSoulBlueAttackBehavior : AttackBehaviorBase
        {


            public override void Destroy()
            {

                base.Destroy();
            }

            // Token: 0x0600011C RID: 284 RVA: 0x0000A9F9 File Offset: 0x00008BF9
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
                this.Owner = this.m_aiActor.GetComponent<GunSoulBlue.GunSoulBlueBehaviour>().Owner;
            }

            // Token: 0x0600011D RID: 285 RVA: 0x0000AA1C File Offset: 0x00008C1C
            public override BehaviorResult Update()
            {
                bool flag = this.attackTimer > 0f && this.isAttacking;
                if (flag)
                {
                    base.DecrementTimer(ref this.attackTimer, false);
                }
                else
                {

                    bool flag2 = this.attackCooldownTimer > 0f && !this.isAttacking;
                    if (flag2)
                    {

                        base.DecrementTimer(ref this.attackCooldownTimer, false);
                    }
                }
                bool flag3 = this.IsReady();
                bool flag4 = (!flag3 || this.attackCooldownTimer > 0f || this.attackTimer == 0f || this.m_aiActor.TargetRigidbody == null) && this.isAttacking;
                BehaviorResult result;
                if (flag4)
                {

                    this.StopAttacking();
                    result = BehaviorResult.Continue;
                }
                else
                {

                    bool flag5 = flag3 && this.attackCooldownTimer == 0f && !this.isAttacking;
                    if (flag5)
                    {

                        this.attackTimer = this.attackDuration;
                        this.isAttacking = true;
                    }
                    bool flag6 = this.attackTimer > 0f && flag3;
                    if (flag6)
                    {

                        this.Attack();
                        result = BehaviorResult.SkipAllRemainingBehaviors;
                    }
                    else
                    {

                        result = BehaviorResult.Continue;
                    }
                }
                return result;
            }

            // Token: 0x0600011E RID: 286 RVA: 0x0000AB30 File Offset: 0x00008D30
            private void StopAttacking()
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.attackCooldownTimer = this.attackCooldown;
            }

            // Token: 0x0600011F RID: 287 RVA: 0x0000AB54 File Offset: 0x00008D54
            public AIActor GetNearestEnemy(List<AIActor> activeEnemies, Vector2 position, out float nearestDistance, string[] filter)
            {
                AIActor aiactor = null;
                nearestDistance = float.MaxValue;
                bool flag = activeEnemies == null;
                bool flag2 = flag;
                bool flag3 = flag2;
                AIActor result;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        AIActor aiactor2 = activeEnemies[i];
                        bool flag4 = aiactor2.healthHaver && aiactor2.healthHaver.IsVulnerable;
                        bool flag5 = flag4;
                        bool flag6 = flag5;
                        if (flag6)
                        {
                            bool flag7 = !aiactor2.healthHaver.IsDead;
                            bool flag8 = flag7;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                bool flag10 = filter == null || !filter.Contains(aiactor2.EnemyGuid);
                                bool flag11 = flag10;
                                bool flag12 = flag11;
                                if (flag12)
                                {
                                    float num = Vector2.Distance(position, aiactor2.CenterPosition);
                                    bool flag13 = num < nearestDistance;
                                    bool flag14 = flag13;
                                    bool flag15 = flag14;
                                    if (flag15)
                                    {
                                        nearestDistance = num;
                                        aiactor = aiactor2;
                                    }
                                }
                            }
                        }
                    }
                    result = aiactor;
                }
                return result;
            }

            // Token: 0x06000120 RID: 288 RVA: 0x0000AC74 File Offset: 0x00008E74
            private void Attack()
            {

                bool flag = this.Owner == null;
                if (flag)
                {
                    this.Owner = this.m_aiActor.GetComponent<GunSoulBlue.GunSoulBlueBehaviour>().Owner;
                }
                float num = -1f;

                List<AIActor> activeEnemies = this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                bool flag2 = activeEnemies == null | activeEnemies.Count <= 0;
                if (!flag2)
                {
                    AIActor nearestEnemy = this.GetNearestEnemy(activeEnemies, this.m_aiActor.sprite.WorldCenter, out num, null);
                    bool flag3 = nearestEnemy && num < 10f;
                    if (flag3)
                    {
                        bool flag4 = this.IsInRange(nearestEnemy);
                        if (flag4)
                        {
                            bool flag5 = !nearestEnemy.IsHarmlessEnemy && nearestEnemy.IsNormalEnemy && !nearestEnemy.healthHaver.IsDead && nearestEnemy != this.m_aiActor;
                            if (flag5)
                            {

                                Vector2 unitCenter = this.m_aiActor.specRigidbody.UnitCenter;
                                Vector2 unitCenter2 = nearestEnemy.specRigidbody.HitboxPixelCollider.UnitCenter;
                                float z = BraveMathCollege.Atan2Degrees((unitCenter2 - unitCenter).normalized);
                                Projectile projectile = ((Gun)ETGMod.Databases.Items[13]).DefaultModule.projectiles[0];
                                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, z + 0f), true);
                                Projectile component = gameObject.GetComponent<Projectile>();
                                HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                                homing.HomingRadius = 10;
                                homing.AngularVelocity = 20;
                                bool flag6 = component != null;
                                bool flag7 = flag6;
                                if (flag7)
                                {
                                    component.Shooter = m_aiActor.specRigidbody;
                                    component.Owner = Owner;
                                    component.baseData.damage = 8f;
                                    component.baseData.force = .5f;
                                    component.collidesWithPlayer = false;
                                }
                            }
                        }
                    }
                }

            }

            // Token: 0x06000121 RID: 289 RVA: 0x0000AE80 File Offset: 0x00009080
            public override float GetMaxRange()
            {
                return 3f;
            }

            // Token: 0x06000122 RID: 290 RVA: 0x0000AE98 File Offset: 0x00009098
            public override float GetMinReadyRange()
            {
                return 10f;
            }

            // Token: 0x06000123 RID: 291 RVA: 0x0000AEB0 File Offset: 0x000090B0
            public override bool IsReady()
            {
                AIActor aiActor = this.m_aiActor;
                bool flag;
                if (aiActor == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody targetRigidbody = aiActor.TargetRigidbody;
                    Vector2? vector = (targetRigidbody != null) ? new Vector2?(targetRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, this.m_aiActor.TargetRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x06000124 RID: 292 RVA: 0x0000AF30 File Offset: 0x00009130
            public bool IsInRange(AIActor enemy)
            {

                bool flag;
                if (enemy == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody specRigidbody = enemy.specRigidbody;
                    Vector2? vector = (specRigidbody != null) ? new Vector2?(specRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, enemy.specRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x0400007A RID: 122
            private bool isAttacking;

            // Token: 0x0400007B RID: 123
            private float attackCooldown = 3f;

            // Token: 0x0400007C RID: 124
            private float attackDuration = 0.1f;

            // Token: 0x0400007D RID: 125
            private float attackTimer;

            // Token: 0x0400007E RID: 126
            private float attackCooldownTimer;

            // Token: 0x0400007F RID: 127
            private PlayerController Owner;

            // Token: 0x04000080 RID: 128
            private List<AIActor> roomEnemies = new List<AIActor>();
        }


        public class ApproachEnemiesBehavior : MovementBehaviorBase
        {
            // Token: 0x06000126 RID: 294 RVA: 0x00009E97 File Offset: 0x00008097
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
            }

            // Token: 0x06000127 RID: 295 RVA: 0x0000AFCF File Offset: 0x000091CF
            public override void Upkeep()
            {
                base.Upkeep();
                base.DecrementTimer(ref this.repathTimer, false);
            }

            // Token: 0x06000128 RID: 296 RVA: 0x0000AFE8 File Offset: 0x000091E8
            public override BehaviorResult Update()
            {
                SpeculativeRigidbody overrideTarget = this.m_aiActor.OverrideTarget;
                bool flag = this.repathTimer > 0f;
                BehaviorResult result;
                if (flag)
                {
                    result = ((overrideTarget == null) ? BehaviorResult.Continue : BehaviorResult.SkipRemainingClassBehaviors);
                }
                else
                {
                    bool flag2 = overrideTarget == null;
                    if (flag2)
                    {
                        this.PickNewTarget();
                        result = BehaviorResult.Continue;
                    }
                    else
                    {
                        this.isInRange = (Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, overrideTarget.UnitCenter) <= this.DesiredDistance);
                        bool flag3 = overrideTarget != null && !this.isInRange;
                        if (flag3)
                        {
                            this.m_aiActor.PathfindToPosition(overrideTarget.UnitCenter, null, true, null, null, null, false);
                            this.repathTimer = this.PathInterval;
                            result = BehaviorResult.SkipRemainingClassBehaviors;
                        }
                        else
                        {
                            bool flag4 = overrideTarget != null && this.repathTimer >= 0f;
                            if (flag4)
                            {
                                this.m_aiActor.ClearPath();
                                this.repathTimer = -1f;
                            }
                            result = BehaviorResult.Continue;
                        }
                    }
                }
                return result;
            }

            // Token: 0x06000129 RID: 297 RVA: 0x0000B104 File Offset: 0x00009304
            private void PickNewTarget()
            {

                bool flag = this.m_aiActor == null;
                if (!flag)
                {
                    bool flag2 = this.Owner == null;
                    if (flag2)
                    {
                        this.Owner = this.m_aiActor.GetComponent<GunSoulBlue.GunSoulBlueBehaviour>().Owner;
                    }
                    this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref this.roomEnemies);
                    for (int i = 0; i < this.roomEnemies.Count; i++)
                    {
                        AIActor aiactor = this.roomEnemies[i];
                        bool flag3 = aiactor.IsHarmlessEnemy || !aiactor.IsNormalEnemy || aiactor.healthHaver.IsDead || aiactor == this.m_aiActor || aiactor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc";
                        if (flag3)
                        {

                            this.roomEnemies.Remove(aiactor);

                        }
                    }
                    bool flag4 = this.roomEnemies.Count == 0;
                    if (flag4)
                    {
                        this.m_aiActor.OverrideTarget = null;
                    }
                    else
                    {
                        AIActor aiActor = this.m_aiActor;
                        AIActor aiactor2 = this.roomEnemies[UnityEngine.Random.Range(0, this.roomEnemies.Count)];
                        aiActor.OverrideTarget = ((aiactor2 != null) ? aiactor2.specRigidbody : null);
                    }
                }
            }


            public float PathInterval = 1f;

            public float DesiredDistance = 5f;

            private float repathTimer;

            private List<AIActor> roomEnemies = new List<AIActor>();

            private bool isInRange;

            private PlayerController Owner;
        }
    }
}

namespace BunnyMod
{
    public class GunSoulGreen : CompanionItem
    {
        public static void GreenBuildPrefab()
        {

            bool flag = GunSoulGreen.GunSoulGreenPrefab != null || CompanionBuilder.companionDictionary.ContainsKey(GunSoulGreen.guid2);
            bool flag2 = flag;
            if (!flag2)
            {
                GunSoulGreen.GunSoulGreenPrefab = CompanionBuilder.BuildPrefab("GreenGunSoul", GunSoulGreen.guid2, GunSoulGreen.spritePaths[0], new IntVector2(3, 2), new IntVector2(8, 9));
                GunSoulGreen.GunSoulGreenBehaviour GunSoulGreenBehavior = GunSoulGreen.GunSoulGreenPrefab.AddComponent<GunSoulGreen.GunSoulGreenBehaviour>();
                AIAnimator aiAnimator = GunSoulGreenBehavior.aiAnimator;
                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "run_right",
                        "run_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_right",
                        "idle_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "attack_right",
                        "attack_left"
                    }
                };
                bool flag3 = GunSoulGreen.GunSoulGreenCollection == null;
                if (flag3)
                {
                    GunSoulGreen.GunSoulGreenCollection = SpriteBuilder.ConstructCollection(GunSoulGreen.GunSoulGreenPrefab, "Penguin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(GunSoulGreen.GunSoulGreenCollection);
                    for (int i = 0; i < GunSoulGreen.spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(GunSoulGreen.spritePaths[i], GunSoulGreen.GunSoulGreenCollection);
                    }
                    SpriteBuilder.AddAnimation(GunSoulGreenBehavior.spriteAnimator, GunSoulGreen.GunSoulGreenCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 7f;
                    SpriteBuilder.AddAnimation(GunSoulGreenBehavior.spriteAnimator, GunSoulGreen.GunSoulGreenCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 7f;
                    SpriteBuilder.AddAnimation(GunSoulGreenBehavior.spriteAnimator, GunSoulGreen.GunSoulGreenCollection, new List<int>
                    {
                        8,
                        9,
                        10,
                        11,
                        12,
                        13,
                        14,
                        15
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 7f;
                    SpriteBuilder.AddAnimation(GunSoulGreenBehavior.spriteAnimator, GunSoulGreen.GunSoulGreenCollection, new List<int>
                    {
                        16,
                        17,
                        18,
                        19,
                        20,
                        21,
                        22,
                        23
                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 7f;
                    SpriteBuilder.AddAnimation(GunSoulGreenBehavior.spriteAnimator, GunSoulGreen.GunSoulGreenCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 7f;
                    SpriteBuilder.AddAnimation(GunSoulGreenBehavior.spriteAnimator, GunSoulGreen.GunSoulGreenCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 7f;
                }
                GunSoulGreenBehavior.aiActor.MovementSpeed = 4f;
                GunSoulGreenBehavior.CanInterceptBullets = true;
                GunSoulGreenBehavior.aiActor.healthHaver.PreventAllDamage = false;
                GunSoulGreenBehavior.aiActor.specRigidbody.CollideWithOthers = true;
                GunSoulGreenBehavior.aiActor.specRigidbody.CollideWithTileMap = false;
                GunSoulGreenBehavior.aiActor.healthHaver.ForceSetCurrentHealth(80f);
                GunSoulGreenBehavior.aiActor.healthHaver.SetHealthMaximum(80f, null, false);
                GunSoulGreenBehavior.aiActor.specRigidbody.PixelColliders.Clear();
                GunSoulGreenBehavior.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
                {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyBulletBlocker,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 10,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                });
                BehaviorSpeculator behaviorSpeculator = GunSoulGreenBehavior.behaviorSpeculator;
                behaviorSpeculator.AttackBehaviors.Add(new GunSoulGreen.GunSoulGreenAttackBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new GunSoulGreen.ApproachEnemiesBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior
                {
                    IdleAnimations = new string[]
                    {
                        "idle"
                    }
                });
                UnityEngine.Object.DontDestroyOnLoad(GunSoulGreen.GunSoulGreenPrefab);
                FakePrefab.MarkAsFakePrefab(GunSoulGreen.GunSoulGreenPrefab);
                GunSoulGreen.GunSoulGreenPrefab.SetActive(false);
            }
        }

        // Token: 0x04000016 RID: 22
        public static GameObject GunSoulGreenPrefab;

        // Token: 0x04000017 RID: 23
        public static readonly string guid2 = "duhkjadsuhjkasdsdjhukdasjhkadsjhksdjhkadsjhksadhjkjshdkaasdjhkjhkadsasjhkdhjksdahjksadhjasdyawweai7ayuwhzijauydaiojuydexoiu";



        // Token: 0x04000018 RID: 24
        private static string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_001.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_002.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_003.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_004.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_005.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_006.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_007.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Idle/gunsoulgreen_idle_008.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_001.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_002.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_003.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_004.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_005.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_006.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_007.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Left/gunsoulgreen_moveleft_008.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_001.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_002.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_003.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_004.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_005.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_006.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_007.png",
            "BunnyMod/Resources/GunSoulSprites/Green/Right/gunsoulgreen_moveright_008.png"
        };

        // Token: 0x04000019 RID: 25
        private static tk2dSpriteCollectionData GunSoulGreenCollection;

        // Token: 0x02000031 RID: 49
        public class GunSoulGreenBehaviour : CompanionController
        {
            // Token: 0x06000119 RID: 281 RVA: 0x0000A9D9 File Offset: 0x00008BD9
            private void Start()
            {
                base.spriteAnimator.Play("idle");
                this.Owner = this.m_owner;
            }

            // Token: 0x04000079 RID: 121
            public PlayerController Owner;
        }

        // Token: 0x02000032 RID: 50
        public class GunSoulGreenAttackBehavior : AttackBehaviorBase
        {


            public override void Destroy()
            {

                base.Destroy();
            }

            // Token: 0x0600011C RID: 284 RVA: 0x0000A9F9 File Offset: 0x00008BF9
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
                this.Owner = this.m_aiActor.GetComponent<GunSoulGreen.GunSoulGreenBehaviour>().Owner;
            }

            // Token: 0x0600011D RID: 285 RVA: 0x0000AA1C File Offset: 0x00008C1C
            public override BehaviorResult Update()
            {
                bool flag = this.attackTimer > 0f && this.isAttacking;
                if (flag)
                {
                    base.DecrementTimer(ref this.attackTimer, false);
                }
                else
                {

                    bool flag2 = this.attackCooldownTimer > 0f && !this.isAttacking;
                    if (flag2)
                    {

                        base.DecrementTimer(ref this.attackCooldownTimer, false);
                    }
                }
                bool flag3 = this.IsReady();
                bool flag4 = (!flag3 || this.attackCooldownTimer > 0f || this.attackTimer == 0f || this.m_aiActor.TargetRigidbody == null) && this.isAttacking;
                BehaviorResult result;
                if (flag4)
                {

                    this.StopAttacking();
                    result = BehaviorResult.Continue;
                }
                else
                {

                    bool flag5 = flag3 && this.attackCooldownTimer == 0f && !this.isAttacking;
                    if (flag5)
                    {

                        this.attackTimer = this.attackDuration;
                        this.isAttacking = true;
                    }
                    bool flag6 = this.attackTimer > 0f && flag3;
                    if (flag6)
                    {

                        this.Attack();
                        result = BehaviorResult.SkipAllRemainingBehaviors;
                    }
                    else
                    {

                        result = BehaviorResult.Continue;
                    }
                }
                return result;
            }

            // Token: 0x0600011E RID: 286 RVA: 0x0000AB30 File Offset: 0x00008D30
            private void StopAttacking()
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.attackCooldownTimer = this.attackCooldown;
            }

            // Token: 0x0600011F RID: 287 RVA: 0x0000AB54 File Offset: 0x00008D54
            public AIActor GetNearestEnemy(List<AIActor> activeEnemies, Vector2 position, out float nearestDistance, string[] filter)
            {
                AIActor aiactor = null;
                nearestDistance = float.MaxValue;
                bool flag = activeEnemies == null;
                bool flag2 = flag;
                bool flag3 = flag2;
                AIActor result;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        AIActor aiactor2 = activeEnemies[i];
                        bool flag4 = aiactor2.healthHaver && aiactor2.healthHaver.IsVulnerable;
                        bool flag5 = flag4;
                        bool flag6 = flag5;
                        if (flag6)
                        {
                            bool flag7 = !aiactor2.healthHaver.IsDead;
                            bool flag8 = flag7;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                bool flag10 = filter == null || !filter.Contains(aiactor2.EnemyGuid);
                                bool flag11 = flag10;
                                bool flag12 = flag11;
                                if (flag12)
                                {
                                    float num = Vector2.Distance(position, aiactor2.CenterPosition);
                                    bool flag13 = num < nearestDistance;
                                    bool flag14 = flag13;
                                    bool flag15 = flag14;
                                    if (flag15)
                                    {
                                        nearestDistance = num;
                                        aiactor = aiactor2;
                                    }
                                }
                            }
                        }
                    }
                    result = aiactor;
                }
                return result;
            }

            // Token: 0x06000120 RID: 288 RVA: 0x0000AC74 File Offset: 0x00008E74
            private void Attack()
            {

                bool flag = this.Owner == null;
                if (flag)
                {
                    this.Owner = this.m_aiActor.GetComponent<GunSoulGreen.GunSoulGreenBehaviour>().Owner;
                }
                float num = -1f;

                List<AIActor> activeEnemies = this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                bool flag2 = activeEnemies == null | activeEnemies.Count <= 0;
                if (!flag2)
                {
                    AIActor nearestEnemy = this.GetNearestEnemy(activeEnemies, this.m_aiActor.sprite.WorldCenter, out num, null);
                    bool flag3 = nearestEnemy && num < 10f;
                    if (flag3)
                    {
                        bool flag4 = this.IsInRange(nearestEnemy);
                        if (flag4)
                        {
                            bool flag5 = !nearestEnemy.IsHarmlessEnemy && nearestEnemy.IsNormalEnemy && !nearestEnemy.healthHaver.IsDead && nearestEnemy != this.m_aiActor;
                            if (flag5)
                            {

                                Vector2 unitCenter = this.m_aiActor.specRigidbody.UnitCenter;
                                Vector2 unitCenter2 = nearestEnemy.specRigidbody.HitboxPixelCollider.UnitCenter;
                                float z = BraveMathCollege.Atan2Degrees((unitCenter2 - unitCenter).normalized);
                                Projectile projectile = ((Gun)ETGMod.Databases.Items[207]).DefaultModule.projectiles[0];
                                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, z + 0f), true);
                                Projectile component = gameObject.GetComponent<Projectile>();
                                HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                                homing.HomingRadius = 10;
                                homing.AngularVelocity = 20;
                                bool flag6 = component != null;
                                bool flag7 = flag6;
                                if (flag7)
                                {
                                    component.Shooter = m_aiActor.specRigidbody;
                                    component.Owner = Owner;
                                    component.baseData.damage = 5f;
                                    component.baseData.force = .5f;
                                    component.collidesWithPlayer = false;
                                }
                            }
                        }
                    }
                }

            }

            // Token: 0x06000121 RID: 289 RVA: 0x0000AE80 File Offset: 0x00009080
            public override float GetMaxRange()
            {
                return 3f;
            }

            // Token: 0x06000122 RID: 290 RVA: 0x0000AE98 File Offset: 0x00009098
            public override float GetMinReadyRange()
            {
                return 10f;
            }

            // Token: 0x06000123 RID: 291 RVA: 0x0000AEB0 File Offset: 0x000090B0
            public override bool IsReady()
            {
                AIActor aiActor = this.m_aiActor;
                bool flag;
                if (aiActor == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody targetRigidbody = aiActor.TargetRigidbody;
                    Vector2? vector = (targetRigidbody != null) ? new Vector2?(targetRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, this.m_aiActor.TargetRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x06000124 RID: 292 RVA: 0x0000AF30 File Offset: 0x00009130
            public bool IsInRange(AIActor enemy)
            {

                bool flag;
                if (enemy == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody specRigidbody = enemy.specRigidbody;
                    Vector2? vector = (specRigidbody != null) ? new Vector2?(specRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, enemy.specRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x0400007A RID: 122
            private bool isAttacking;

            // Token: 0x0400007B RID: 123
            private float attackCooldown = 5f;

            // Token: 0x0400007C RID: 124
            private float attackDuration = 0.1f;

            // Token: 0x0400007D RID: 125
            private float attackTimer;

            // Token: 0x0400007E RID: 126
            private float attackCooldownTimer;

            // Token: 0x0400007F RID: 127
            private PlayerController Owner;

            // Token: 0x04000080 RID: 128
            private List<AIActor> roomEnemies = new List<AIActor>();
        }


        public class ApproachEnemiesBehavior : MovementBehaviorBase
        {
            // Token: 0x06000126 RID: 294 RVA: 0x00009E97 File Offset: 0x00008097
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
            }

            // Token: 0x06000127 RID: 295 RVA: 0x0000AFCF File Offset: 0x000091CF
            public override void Upkeep()
            {
                base.Upkeep();
                base.DecrementTimer(ref this.repathTimer, false);
            }

            // Token: 0x06000128 RID: 296 RVA: 0x0000AFE8 File Offset: 0x000091E8
            public override BehaviorResult Update()
            {
                SpeculativeRigidbody overrideTarget = this.m_aiActor.OverrideTarget;
                bool flag = this.repathTimer > 0f;
                BehaviorResult result;
                if (flag)
                {
                    result = ((overrideTarget == null) ? BehaviorResult.Continue : BehaviorResult.SkipRemainingClassBehaviors);
                }
                else
                {
                    bool flag2 = overrideTarget == null;
                    if (flag2)
                    {
                        this.PickNewTarget();
                        result = BehaviorResult.Continue;
                    }
                    else
                    {
                        this.isInRange = (Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, overrideTarget.UnitCenter) <= this.DesiredDistance);
                        bool flag3 = overrideTarget != null && !this.isInRange;
                        if (flag3)
                        {
                            this.m_aiActor.PathfindToPosition(overrideTarget.UnitCenter, null, true, null, null, null, false);
                            this.repathTimer = this.PathInterval;
                            result = BehaviorResult.SkipRemainingClassBehaviors;
                        }
                        else
                        {
                            bool flag4 = overrideTarget != null && this.repathTimer >= 0f;
                            if (flag4)
                            {
                                this.m_aiActor.ClearPath();
                                this.repathTimer = -1f;
                            }
                            result = BehaviorResult.Continue;
                        }
                    }
                }
                return result;
            }

            // Token: 0x06000129 RID: 297 RVA: 0x0000B104 File Offset: 0x00009304
            private void PickNewTarget()
            {

                bool flag = this.m_aiActor == null;
                if (!flag)
                {
                    bool flag2 = this.Owner == null;
                    if (flag2)
                    {
                        this.Owner = this.m_aiActor.GetComponent<GunSoulGreen.GunSoulGreenBehaviour>().Owner;
                    }
                    this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref this.roomEnemies);
                    for (int i = 0; i < this.roomEnemies.Count; i++)
                    {
                        AIActor aiactor = this.roomEnemies[i];
                        bool flag3 = aiactor.IsHarmlessEnemy || !aiactor.IsNormalEnemy || aiactor.healthHaver.IsDead || aiactor == this.m_aiActor || aiactor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc";
                        if (flag3)
                        {

                            this.roomEnemies.Remove(aiactor);

                        }
                    }
                    bool flag4 = this.roomEnemies.Count == 0;
                    if (flag4)
                    {
                        this.m_aiActor.OverrideTarget = null;
                    }
                    else
                    {
                        AIActor aiActor = this.m_aiActor;
                        AIActor aiactor2 = this.roomEnemies[UnityEngine.Random.Range(0, this.roomEnemies.Count)];
                        aiActor.OverrideTarget = ((aiactor2 != null) ? aiactor2.specRigidbody : null);
                    }
                }
            }


            public float PathInterval = 1f;

            public float DesiredDistance = 5f;

            private float repathTimer;

            private List<AIActor> roomEnemies = new List<AIActor>();

            private bool isInRange;

            private PlayerController Owner;
        }
    }
}

namespace BunnyMod
{
    public class GunSoulRed : CompanionItem
    {
        public static void RedBuildPrefab()
        {

            bool flag = GunSoulRed.GunSoulRedPrefab != null || CompanionBuilder.companionDictionary.ContainsKey(GunSoulRed.guid3);
            bool flag2 = flag;
            if (!flag2)
            {
                GunSoulRed.GunSoulRedPrefab = CompanionBuilder.BuildPrefab("RED", GunSoulRed.guid3, GunSoulRed.spritePaths[0], new IntVector2(3, 2), new IntVector2(8, 9));
                GunSoulRed.GunSoulRedBehaviour GunSoulRedBehavior = GunSoulRed.GunSoulRedPrefab.AddComponent<GunSoulRed.GunSoulRedBehaviour>();
                AIAnimator aiAnimator = GunSoulRedBehavior.aiAnimator;
                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "run_right",
                        "run_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_right",
                        "idle_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "attack_right",
                        "attack_left"
                    }
                };
                bool flag3 = GunSoulRed.GunSoulRedCollection == null;
                if (flag3)
                {
                    GunSoulRed.GunSoulRedCollection = SpriteBuilder.ConstructCollection(GunSoulRed.GunSoulRedPrefab, "Penguin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(GunSoulRed.GunSoulRedCollection);
                    for (int i = 0; i < GunSoulRed.spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(GunSoulRed.spritePaths[i], GunSoulRed.GunSoulRedCollection);
                    }
                    SpriteBuilder.AddAnimation(GunSoulRedBehavior.spriteAnimator, GunSoulRed.GunSoulRedCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                    SpriteBuilder.AddAnimation(GunSoulRedBehavior.spriteAnimator, GunSoulRed.GunSoulRedCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                    SpriteBuilder.AddAnimation(GunSoulRedBehavior.spriteAnimator, GunSoulRed.GunSoulRedCollection, new List<int>
                    {
                        8,
                        9,
                        10,
                        11,
                        12,
                        13,
                        14,
                        15
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                    SpriteBuilder.AddAnimation(GunSoulRedBehavior.spriteAnimator, GunSoulRed.GunSoulRedCollection, new List<int>
                    {
                        16,
                        17,
                        18,
                        19,
                        20,
                        21,
                        22,
                        23
                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                    SpriteBuilder.AddAnimation(GunSoulRedBehavior.spriteAnimator, GunSoulRed.GunSoulRedCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                    SpriteBuilder.AddAnimation(GunSoulRedBehavior.spriteAnimator, GunSoulRed.GunSoulRedCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 10f;
                }
                GunSoulRedBehavior.aiActor.MovementSpeed = 6.5f;
                GunSoulRedBehavior.CanInterceptBullets = true;
                GunSoulRedBehavior.aiActor.healthHaver.PreventAllDamage = false;
                GunSoulRedBehavior.aiActor.specRigidbody.CollideWithOthers = true;
                GunSoulRedBehavior.aiActor.specRigidbody.CollideWithTileMap = false;
                GunSoulRedBehavior.aiActor.healthHaver.ForceSetCurrentHealth(60f);
                GunSoulRedBehavior.aiActor.healthHaver.SetHealthMaximum(60f, null, false);
                GunSoulRedBehavior.aiActor.specRigidbody.PixelColliders.Clear();
                GunSoulRedBehavior.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
                {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyBulletBlocker,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 10,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                });
                BehaviorSpeculator behaviorSpeculator = GunSoulRedBehavior.behaviorSpeculator;
                behaviorSpeculator.AttackBehaviors.Add(new GunSoulRed.GunSoulRedAttackBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new GunSoulRed.ApproachEnemiesBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior
                {
                    IdleAnimations = new string[]
                    {
                        "idle"
                    }
                });
                UnityEngine.Object.DontDestroyOnLoad(GunSoulRed.GunSoulRedPrefab);
                FakePrefab.MarkAsFakePrefab(GunSoulRed.GunSoulRedPrefab);
                GunSoulRed.GunSoulRedPrefab.SetActive(false);
            }
        }

        // Token: 0x04000016 RID: 22
        public static GameObject GunSoulRedPrefab;

        // Token: 0x04000017 RID: 23
        public static readonly string guid3 = "oooeeaaathingtagwalalawallabingbang";

        private List<CompanionController> companionsSpawned = new List<CompanionController>();


        // Token: 0x04000018 RID: 24
        private static string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_001.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_002.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_003.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_004.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_005.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_006.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_007.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Idle/gunsoulred_idle_008.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_001.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_002.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_003.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_004.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_005.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_006.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_007.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Left/gunsoulred_moveleft_008.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_001.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_002.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_003.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_004.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_005.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_006.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_007.png",
            "BunnyMod/Resources/GunSoulSprites/Red/Right/gunsoulred_moveright_008.png",

        };

        // Token: 0x04000019 RID: 25
        private static tk2dSpriteCollectionData GunSoulRedCollection;

        // Token: 0x02000031 RID: 49
        public class GunSoulRedBehaviour : CompanionController
        {
            // Token: 0x06000119 RID: 281 RVA: 0x0000A9D9 File Offset: 0x00008BD9
            private void Start()
            {
                base.spriteAnimator.Play("idle");
                this.Owner = this.m_owner;
            }

            // Token: 0x04000079 RID: 121
            public PlayerController Owner;
        }

        // Token: 0x02000032 RID: 50
        public class GunSoulRedAttackBehavior : AttackBehaviorBase
        {


            public override void Destroy()
            {

                base.Destroy();
            }

            // Token: 0x0600011C RID: 284 RVA: 0x0000A9F9 File Offset: 0x00008BF9
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
                this.Owner = this.m_aiActor.GetComponent<GunSoulRed.GunSoulRedBehaviour>().Owner;
            }

            // Token: 0x0600011D RID: 285 RVA: 0x0000AA1C File Offset: 0x00008C1C
            public override BehaviorResult Update()
            {
                bool flag = this.attackTimer > 0f && this.isAttacking;
                if (flag)
                {
                    base.DecrementTimer(ref this.attackTimer, false);
                }
                else
                {

                    bool flag2 = this.attackCooldownTimer > 0f && !this.isAttacking;
                    if (flag2)
                    {

                        base.DecrementTimer(ref this.attackCooldownTimer, false);
                    }
                }
                bool flag3 = this.IsReady();
                bool flag4 = (!flag3 || this.attackCooldownTimer > 0f || this.attackTimer == 0f || this.m_aiActor.TargetRigidbody == null) && this.isAttacking;
                BehaviorResult result;
                if (flag4)
                {

                    this.StopAttacking();
                    result = BehaviorResult.Continue;
                }
                else
                {

                    bool flag5 = flag3 && this.attackCooldownTimer == 0f && !this.isAttacking;
                    if (flag5)
                    {

                        this.attackTimer = this.attackDuration;
                        this.isAttacking = true;
                    }
                    bool flag6 = this.attackTimer > 0f && flag3;
                    if (flag6)
                    {

                        this.Attack();
                        result = BehaviorResult.SkipAllRemainingBehaviors;
                    }
                    else
                    {

                        result = BehaviorResult.Continue;
                    }
                }
                return result;
            }

            // Token: 0x0600011E RID: 286 RVA: 0x0000AB30 File Offset: 0x00008D30
            private void StopAttacking()
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.attackCooldownTimer = this.attackCooldown;
            }

            // Token: 0x0600011F RID: 287 RVA: 0x0000AB54 File Offset: 0x00008D54
            public AIActor GetNearestEnemy(List<AIActor> activeEnemies, Vector2 position, out float nearestDistance, string[] filter)
            {
                AIActor aiactor = null;
                nearestDistance = float.MaxValue;
                bool flag = activeEnemies == null;
                bool flag2 = flag;
                bool flag3 = flag2;
                AIActor result;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        AIActor aiactor2 = activeEnemies[i];
                        bool flag4 = aiactor2.healthHaver && aiactor2.healthHaver.IsVulnerable;
                        bool flag5 = flag4;
                        bool flag6 = flag5;
                        if (flag6)
                        {
                            bool flag7 = !aiactor2.healthHaver.IsDead;
                            bool flag8 = flag7;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                bool flag10 = filter == null || !filter.Contains(aiactor2.EnemyGuid);
                                bool flag11 = flag10;
                                bool flag12 = flag11;
                                if (flag12)
                                {
                                    float num = Vector2.Distance(position, aiactor2.CenterPosition);
                                    bool flag13 = num < nearestDistance;
                                    bool flag14 = flag13;
                                    bool flag15 = flag14;
                                    if (flag15)
                                    {
                                        nearestDistance = num;
                                        aiactor = aiactor2;
                                    }
                                }
                            }
                        }
                    }
                    result = aiactor;
                }
                return result;
            }

            // Token: 0x06000120 RID: 288 RVA: 0x0000AC74 File Offset: 0x00008E74
            private void Attack()
            {

                bool flag = this.Owner == null;
                if (flag)
                {
                    this.Owner = this.m_aiActor.GetComponent<GunSoulRed.GunSoulRedBehaviour>().Owner;
                }
                float num = -1f;

                List<AIActor> activeEnemies = this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                bool flag2 = activeEnemies == null | activeEnemies.Count <= 0;
                if (!flag2)
                {
                    AIActor nearestEnemy = this.GetNearestEnemy(activeEnemies, this.m_aiActor.sprite.WorldCenter, out num, null);
                    bool flag3 = nearestEnemy && num < 10f;
                    if (flag3)
                    {
                        bool flag4 = this.IsInRange(nearestEnemy);
                        if (flag4)
                        {
                            bool flag5 = !nearestEnemy.IsHarmlessEnemy && nearestEnemy.IsNormalEnemy && !nearestEnemy.healthHaver.IsDead && nearestEnemy != this.m_aiActor;
                            if (flag5)
                            {

                                Vector2 unitCenter = this.m_aiActor.specRigidbody.UnitCenter;
                                Vector2 unitCenter2 = nearestEnemy.specRigidbody.HitboxPixelCollider.UnitCenter;
                                float z = BraveMathCollege.Atan2Degrees((unitCenter2 - unitCenter).normalized);
                                Projectile projectile = ((Gun)ETGMod.Databases.Items[336]).DefaultModule.projectiles[0];
                                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, z + UnityEngine.Random.Range(-30, 30)), true);
                                Projectile component = gameObject.GetComponent<Projectile>();
                                bool flag6 = component != null;
                                bool flag7 = flag6;
                                if (flag7)
                                {
                                    component.Shooter = m_aiActor.specRigidbody;
                                    component.Owner = Owner;
                                    component.baseData.damage = 2f;
                                    component.baseData.force = .5f;
                                    component.collidesWithPlayer = false;
                                }
                            }
                        }
                    }
                }

            }

            // Token: 0x06000121 RID: 289 RVA: 0x0000AE80 File Offset: 0x00009080
            public override float GetMaxRange()
            {
                return 4f;
            }

            // Token: 0x06000122 RID: 290 RVA: 0x0000AE98 File Offset: 0x00009098
            public override float GetMinReadyRange()
            {
                return 7f;
            }

            // Token: 0x06000123 RID: 291 RVA: 0x0000AEB0 File Offset: 0x000090B0
            public override bool IsReady()
            {
                AIActor aiActor = this.m_aiActor;
                bool flag;
                if (aiActor == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody targetRigidbody = aiActor.TargetRigidbody;
                    Vector2? vector = (targetRigidbody != null) ? new Vector2?(targetRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, this.m_aiActor.TargetRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x06000124 RID: 292 RVA: 0x0000AF30 File Offset: 0x00009130
            public bool IsInRange(AIActor enemy)
            {

                bool flag;
                if (enemy == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody specRigidbody = enemy.specRigidbody;
                    Vector2? vector = (specRigidbody != null) ? new Vector2?(specRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, enemy.specRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x0400007A RID: 122
            private bool isAttacking;

            // Token: 0x0400007B RID: 123
            private float attackCooldown = 2f;

            // Token: 0x0400007C RID: 124
            private float attackDuration = 0.1f;

            // Token: 0x0400007D RID: 125
            private float attackTimer;

            // Token: 0x0400007E RID: 126
            private float attackCooldownTimer;

            // Token: 0x0400007F RID: 127
            private PlayerController Owner;

            // Token: 0x04000080 RID: 128
            private List<AIActor> roomEnemies = new List<AIActor>();
        }


        public class ApproachEnemiesBehavior : MovementBehaviorBase
        {
            // Token: 0x06000126 RID: 294 RVA: 0x00009E97 File Offset: 0x00008097
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
            }

            // Token: 0x06000127 RID: 295 RVA: 0x0000AFCF File Offset: 0x000091CF
            public override void Upkeep()
            {
                base.Upkeep();
                base.DecrementTimer(ref this.repathTimer, false);
            }

            // Token: 0x06000128 RID: 296 RVA: 0x0000AFE8 File Offset: 0x000091E8
            public override BehaviorResult Update()
            {
                SpeculativeRigidbody overrideTarget = this.m_aiActor.OverrideTarget;
                bool flag = this.repathTimer > 0f;
                BehaviorResult result;
                if (flag)
                {
                    result = ((overrideTarget == null) ? BehaviorResult.Continue : BehaviorResult.SkipRemainingClassBehaviors);
                }
                else
                {
                    bool flag2 = overrideTarget == null;
                    if (flag2)
                    {
                        this.PickNewTarget();
                        result = BehaviorResult.Continue;
                    }
                    else
                    {
                        this.isInRange = (Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, overrideTarget.UnitCenter) <= this.DesiredDistance);
                        bool flag3 = overrideTarget != null && !this.isInRange;
                        if (flag3)
                        {
                            this.m_aiActor.PathfindToPosition(overrideTarget.UnitCenter, null, true, null, null, null, false);
                            this.repathTimer = this.PathInterval;
                            result = BehaviorResult.SkipRemainingClassBehaviors;
                        }
                        else
                        {
                            bool flag4 = overrideTarget != null && this.repathTimer >= 0f;
                            if (flag4)
                            {
                                this.m_aiActor.ClearPath();
                                this.repathTimer = -1f;
                            }
                            result = BehaviorResult.Continue;
                        }
                    }
                }
                return result;
            }

            // Token: 0x06000129 RID: 297 RVA: 0x0000B104 File Offset: 0x00009304
            private void PickNewTarget()
            {

                bool flag = this.m_aiActor == null;
                if (!flag)
                {
                    bool flag2 = this.Owner == null;
                    if (flag2)
                    {
                        this.Owner = this.m_aiActor.GetComponent<GunSoulRed.GunSoulRedBehaviour>().Owner;
                    }
                    this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref this.roomEnemies);
                    for (int i = 0; i < this.roomEnemies.Count; i++)
                    {
                        AIActor aiactor = this.roomEnemies[i];
                        bool flag3 = aiactor.IsHarmlessEnemy || !aiactor.IsNormalEnemy || aiactor.healthHaver.IsDead || aiactor == this.m_aiActor || aiactor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc";
                        if (flag3)
                        {

                            this.roomEnemies.Remove(aiactor);

                        }
                    }
                    bool flag4 = this.roomEnemies.Count == 0;
                    if (flag4)
                    {
                        this.m_aiActor.OverrideTarget = null;
                    }
                    else
                    {
                        AIActor aiActor = this.m_aiActor;
                        AIActor aiactor2 = this.roomEnemies[UnityEngine.Random.Range(0, this.roomEnemies.Count)];
                        aiActor.OverrideTarget = ((aiactor2 != null) ? aiactor2.specRigidbody : null);
                    }
                }
            }


            public float PathInterval = 1f;

            public float DesiredDistance = 5f;

            private float repathTimer;

            private List<AIActor> roomEnemies = new List<AIActor>();

            private bool isInRange;

            private PlayerController Owner;
        }
    }
}

namespace BunnyMod
{
    public class GunSoulYellow : CompanionItem
    {
        public static void YellowBuildPrefab()
        {

            bool flag = GunSoulYellow.GunSoulYellowPrefab != null || CompanionBuilder.companionDictionary.ContainsKey(GunSoulYellow.guid4);
            bool flag2 = flag;
            if (!flag2)
            {
                GunSoulYellow.GunSoulYellowPrefab = CompanionBuilder.BuildPrefab("Yellow", GunSoulYellow.guid4, GunSoulYellow.spritePaths[0], new IntVector2(3, 2), new IntVector2(8, 9));
                GunSoulYellow.GunSoulYellowBehaviour GunSoulYellowBehavior = GunSoulYellow.GunSoulYellowPrefab.AddComponent<GunSoulYellow.GunSoulYellowBehaviour>();
                AIAnimator aiAnimator = GunSoulYellowBehavior.aiAnimator;
                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "run_right",
                        "run_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_right",
                        "idle_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "attack_right",
                        "attack_left"
                    }
                };
                bool flag3 = GunSoulYellow.GunSoulYellowCollection == null;
                if (flag3)
                {
                    GunSoulYellow.GunSoulYellowCollection = SpriteBuilder.ConstructCollection(GunSoulYellow.GunSoulYellowPrefab, "Penguin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(GunSoulYellow.GunSoulYellowCollection);
                    for (int i = 0; i < GunSoulYellow.spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(GunSoulYellow.spritePaths[i], GunSoulYellow.GunSoulYellowCollection);
                    }
                    SpriteBuilder.AddAnimation(GunSoulYellowBehavior.spriteAnimator, GunSoulYellow.GunSoulYellowCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 12f;
                    SpriteBuilder.AddAnimation(GunSoulYellowBehavior.spriteAnimator, GunSoulYellow.GunSoulYellowCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 12f;
                    SpriteBuilder.AddAnimation(GunSoulYellowBehavior.spriteAnimator, GunSoulYellow.GunSoulYellowCollection, new List<int>
                    {
                        8,
                        9,
                        10,
                        11,
                        12,
                        13,
                        14,
                        15
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 12f;
                    SpriteBuilder.AddAnimation(GunSoulYellowBehavior.spriteAnimator, GunSoulYellow.GunSoulYellowCollection, new List<int>
                    {
                        16,
                        17,
                        18,
                        19,
                        20,
                        21,
                        22,
                        23
                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 12f;
                    SpriteBuilder.AddAnimation(GunSoulYellowBehavior.spriteAnimator, GunSoulYellow.GunSoulYellowCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 12f;
                    SpriteBuilder.AddAnimation(GunSoulYellowBehavior.spriteAnimator, GunSoulYellow.GunSoulYellowCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 12f;
                }
                GunSoulYellowBehavior.aiActor.MovementSpeed = 9f;
                GunSoulYellowBehavior.CanInterceptBullets = true;
                GunSoulYellowBehavior.aiActor.healthHaver.PreventAllDamage = false;
                GunSoulYellowBehavior.aiActor.specRigidbody.CollideWithOthers = true;
                GunSoulYellowBehavior.aiActor.specRigidbody.CollideWithTileMap = false;
                GunSoulYellowBehavior.aiActor.healthHaver.ForceSetCurrentHealth(30f);
                GunSoulYellowBehavior.aiActor.healthHaver.SetHealthMaximum(30f, null, false);
                GunSoulYellowBehavior.aiActor.specRigidbody.PixelColliders.Clear();
                GunSoulYellowBehavior.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
                {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyBulletBlocker,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 10,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                });
                BehaviorSpeculator behaviorSpeculator = GunSoulYellowBehavior.behaviorSpeculator;
                behaviorSpeculator.AttackBehaviors.Add(new GunSoulYellow.GunSoulYellowAttackBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new GunSoulYellow.ApproachEnemiesBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior
                {
                    IdleAnimations = new string[]
                    {
                        "idle"
                    }
                });
                UnityEngine.Object.DontDestroyOnLoad(GunSoulYellow.GunSoulYellowPrefab);
                FakePrefab.MarkAsFakePrefab(GunSoulYellow.GunSoulYellowPrefab);
                GunSoulYellow.GunSoulYellowPrefab.SetActive(false);
            }
        }

        // Token: 0x04000016 RID: 22
        public static GameObject GunSoulYellowPrefab;

        // Token: 0x04000017 RID: 23
        public static readonly string guid4 = "asddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd";

        private List<CompanionController> companionsSpawned = new List<CompanionController>();


        // Token: 0x04000018 RID: 24
        private static string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_001.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_002.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_003.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_004.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_005.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_006.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_007.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Idle/gunsoulyellow_idle_008.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_001.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_002.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_003.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_004.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_005.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_006.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_007.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Left/gunsoulyellow_moveleft_008.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_001.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_002.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_003.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_004.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_005.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_006.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_007.png",
            "BunnyMod/Resources/GunSoulSprites/Yellow/Right/gunsoulyellow_moveright_008.png",

        };

        // Token: 0x04000019 RID: 25
        private static tk2dSpriteCollectionData GunSoulYellowCollection;

        // Token: 0x02000031 RID: 49
        public class GunSoulYellowBehaviour : CompanionController
        {
            // Token: 0x06000119 RID: 281 RVA: 0x0000A9D9 File Offset: 0x00008BD9
            private void Start()
            {
                base.spriteAnimator.Play("idle");
                this.Owner = this.m_owner;
            }

            // Token: 0x04000079 RID: 121
            public PlayerController Owner;
        }

        // Token: 0x02000032 RID: 50
        public class GunSoulYellowAttackBehavior : AttackBehaviorBase
        {


            public override void Destroy()
            {

                base.Destroy();
            }

            // Token: 0x0600011C RID: 284 RVA: 0x0000A9F9 File Offset: 0x00008BF9
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
                this.Owner = this.m_aiActor.GetComponent<GunSoulYellow.GunSoulYellowBehaviour>().Owner;
            }

            // Token: 0x0600011D RID: 285 RVA: 0x0000AA1C File Offset: 0x00008C1C
            public override BehaviorResult Update()
            {
                bool flag = this.attackTimer > 0f && this.isAttacking;
                if (flag)
                {
                    base.DecrementTimer(ref this.attackTimer, false);
                }
                else
                {

                    bool flag2 = this.attackCooldownTimer > 0f && !this.isAttacking;
                    if (flag2)
                    {

                        base.DecrementTimer(ref this.attackCooldownTimer, false);
                    }
                }
                bool flag3 = this.IsReady();
                bool flag4 = (!flag3 || this.attackCooldownTimer > 0f || this.attackTimer == 0f || this.m_aiActor.TargetRigidbody == null) && this.isAttacking;
                BehaviorResult result;
                if (flag4)
                {

                    this.StopAttacking();
                    result = BehaviorResult.Continue;
                }
                else
                {

                    bool flag5 = flag3 && this.attackCooldownTimer == 0f && !this.isAttacking;
                    if (flag5)
                    {

                        this.attackTimer = this.attackDuration;
                        this.isAttacking = true;
                    }
                    bool flag6 = this.attackTimer > 0f && flag3;
                    if (flag6)
                    {

                        this.Attack();
                        result = BehaviorResult.SkipAllRemainingBehaviors;
                    }
                    else
                    {

                        result = BehaviorResult.Continue;
                    }
                }
                return result;
            }

            // Token: 0x0600011E RID: 286 RVA: 0x0000AB30 File Offset: 0x00008D30
            private void StopAttacking()
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.attackCooldownTimer = this.attackCooldown;
            }

            // Token: 0x0600011F RID: 287 RVA: 0x0000AB54 File Offset: 0x00008D54
            public AIActor GetNearestEnemy(List<AIActor> activeEnemies, Vector2 position, out float nearestDistance, string[] filter)
            {
                AIActor aiactor = null;
                nearestDistance = float.MaxValue;
                bool flag = activeEnemies == null;
                bool flag2 = flag;
                bool flag3 = flag2;
                AIActor result;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        AIActor aiactor2 = activeEnemies[i];
                        bool flag4 = aiactor2.healthHaver && aiactor2.healthHaver.IsVulnerable;
                        bool flag5 = flag4;
                        bool flag6 = flag5;
                        if (flag6)
                        {
                            bool flag7 = !aiactor2.healthHaver.IsDead;
                            bool flag8 = flag7;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                bool flag10 = filter == null || !filter.Contains(aiactor2.EnemyGuid);
                                bool flag11 = flag10;
                                bool flag12 = flag11;
                                if (flag12)
                                {
                                    float num = Vector2.Distance(position, aiactor2.CenterPosition);
                                    bool flag13 = num < nearestDistance;
                                    bool flag14 = flag13;
                                    bool flag15 = flag14;
                                    if (flag15)
                                    {
                                        nearestDistance = num;
                                        aiactor = aiactor2;
                                    }
                                }
                            }
                        }
                    }
                    result = aiactor;
                }
                return result;
            }

            // Token: 0x06000120 RID: 288 RVA: 0x0000AC74 File Offset: 0x00008E74
            private void Attack()
            {

                bool flag = this.Owner == null;
                if (flag)
                {
                    this.Owner = this.m_aiActor.GetComponent<GunSoulYellow.GunSoulYellowBehaviour>().Owner;
                }
                float num = -1f;

                List<AIActor> activeEnemies = this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                bool flag2 = activeEnemies == null | activeEnemies.Count <= 0;
                if (!flag2)
                {
                    AIActor nearestEnemy = this.GetNearestEnemy(activeEnemies, this.m_aiActor.sprite.WorldCenter, out num, null);
                    bool flag3 = nearestEnemy && num < 10f;
                    if (flag3)
                    {
                        bool flag4 = this.IsInRange(nearestEnemy);
                        if (flag4)
                        {
                            bool flag5 = !nearestEnemy.IsHarmlessEnemy && nearestEnemy.IsNormalEnemy && !nearestEnemy.healthHaver.IsDead && nearestEnemy != this.m_aiActor;
                            if (flag5)
                            {
                                for (int counter = 0; counter < UnityEngine.Random.Range(2f, 5f); counter++)
                                {
                                    Vector2 unitCenter = this.m_aiActor.specRigidbody.UnitCenter;
                                    Vector2 unitCenter2 = nearestEnemy.specRigidbody.HitboxPixelCollider.UnitCenter;
                                    float z = BraveMathCollege.Atan2Degrees((unitCenter2 - unitCenter).normalized);
                                    Projectile projectile = ((Gun)ETGMod.Databases.Items[38]).DefaultModule.projectiles[0];
                                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, z + UnityEngine.Random.Range(-40, 40)), true);
                                    Projectile component = gameObject.GetComponent<Projectile>();
                                    bool flag6 = component != null;
                                    bool flag7 = flag6;
                                    if (flag7)
                                    {
                                        component.Shooter = m_aiActor.specRigidbody;
                                        component.Owner = Owner;
                                        component.baseData.damage = 4f;
                                        component.baseData.force = .5f;
                                        component.collidesWithPlayer = false;
                                    }
                                }
                            }
                        }
                    }
                }

            }

            // Token: 0x06000121 RID: 289 RVA: 0x0000AE80 File Offset: 0x00009080
            public override float GetMaxRange()
            {
                return 5f;
            }

            // Token: 0x06000122 RID: 290 RVA: 0x0000AE98 File Offset: 0x00009098
            public override float GetMinReadyRange()
            {
                return 8.5f;
            }

            // Token: 0x06000123 RID: 291 RVA: 0x0000AEB0 File Offset: 0x000090B0
            public override bool IsReady()
            {
                AIActor aiActor = this.m_aiActor;
                bool flag;
                if (aiActor == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody targetRigidbody = aiActor.TargetRigidbody;
                    Vector2? vector = (targetRigidbody != null) ? new Vector2?(targetRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, this.m_aiActor.TargetRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x06000124 RID: 292 RVA: 0x0000AF30 File Offset: 0x00009130
            public bool IsInRange(AIActor enemy)
            {

                bool flag;
                if (enemy == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody specRigidbody = enemy.specRigidbody;
                    Vector2? vector = (specRigidbody != null) ? new Vector2?(specRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, enemy.specRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x0400007A RID: 122
            private bool isAttacking;

            // Token: 0x0400007B RID: 123
            private float attackCooldown = .7f;

            // Token: 0x0400007C RID: 124
            private float attackDuration = 0.1f;

            // Token: 0x0400007D RID: 125
            private float attackTimer;

            // Token: 0x0400007E RID: 126
            private float attackCooldownTimer;

            // Token: 0x0400007F RID: 127
            private PlayerController Owner;

            // Token: 0x04000080 RID: 128
            private List<AIActor> roomEnemies = new List<AIActor>();
        }


        public class ApproachEnemiesBehavior : MovementBehaviorBase
        {
            // Token: 0x06000126 RID: 294 RVA: 0x00009E97 File Offset: 0x00008097
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
            }

            // Token: 0x06000127 RID: 295 RVA: 0x0000AFCF File Offset: 0x000091CF
            public override void Upkeep()
            {
                base.Upkeep();
                base.DecrementTimer(ref this.repathTimer, false);
            }

            // Token: 0x06000128 RID: 296 RVA: 0x0000AFE8 File Offset: 0x000091E8
            public override BehaviorResult Update()
            {
                SpeculativeRigidbody overrideTarget = this.m_aiActor.OverrideTarget;
                bool flag = this.repathTimer > 0f;
                BehaviorResult result;
                if (flag)
                {
                    result = ((overrideTarget == null) ? BehaviorResult.Continue : BehaviorResult.SkipRemainingClassBehaviors);
                }
                else
                {
                    bool flag2 = overrideTarget == null;
                    if (flag2)
                    {
                        this.PickNewTarget();
                        result = BehaviorResult.Continue;
                    }
                    else
                    {
                        this.isInRange = (Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, overrideTarget.UnitCenter) <= this.DesiredDistance);
                        bool flag3 = overrideTarget != null && !this.isInRange;
                        if (flag3)
                        {
                            this.m_aiActor.PathfindToPosition(overrideTarget.UnitCenter, null, true, null, null, null, false);
                            this.repathTimer = this.PathInterval;
                            result = BehaviorResult.SkipRemainingClassBehaviors;
                        }
                        else
                        {
                            bool flag4 = overrideTarget != null && this.repathTimer >= 0f;
                            if (flag4)
                            {
                                this.m_aiActor.ClearPath();
                                this.repathTimer = -1f;
                            }
                            result = BehaviorResult.Continue;
                        }
                    }
                }
                return result;
            }

            // Token: 0x06000129 RID: 297 RVA: 0x0000B104 File Offset: 0x00009304
            private void PickNewTarget()
            {

                bool flag = this.m_aiActor == null;
                if (!flag)
                {
                    bool flag2 = this.Owner == null;
                    if (flag2)
                    {
                        this.Owner = this.m_aiActor.GetComponent<GunSoulYellow.GunSoulYellowBehaviour>().Owner;
                    }
                    this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref this.roomEnemies);
                    for (int i = 0; i < this.roomEnemies.Count; i++)
                    {
                        AIActor aiactor = this.roomEnemies[i];
                        bool flag3 = aiactor.IsHarmlessEnemy || !aiactor.IsNormalEnemy || aiactor.healthHaver.IsDead || aiactor == this.m_aiActor || aiactor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc";
                        if (flag3)
                        {

                            this.roomEnemies.Remove(aiactor);

                        }
                    }
                    bool flag4 = this.roomEnemies.Count == 0;
                    if (flag4)
                    {
                        this.m_aiActor.OverrideTarget = null;
                    }
                    else
                    {
                        AIActor aiActor = this.m_aiActor;
                        AIActor aiactor2 = this.roomEnemies[UnityEngine.Random.Range(0, this.roomEnemies.Count)];
                        aiActor.OverrideTarget = ((aiactor2 != null) ? aiactor2.specRigidbody : null);
                    }
                }
            }


            public float PathInterval = 1f;

            public float DesiredDistance = 6.25f;

            private float repathTimer;

            private List<AIActor> roomEnemies = new List<AIActor>();

            private bool isInRange;

            private PlayerController Owner;
        }
    }
}

namespace BunnyMod
{
    public class GunSoulPink : CompanionItem
    {
        public static void PinkBuildPrefab()
        {

            bool flag = GunSoulPink.GunSoulPinkPrefab != null || CompanionBuilder.companionDictionary.ContainsKey(GunSoulPink.guid5);
            bool flag2 = flag;
            if (!flag2)
            {
                GunSoulPink.GunSoulPinkPrefab = CompanionBuilder.BuildPrefab("Peenk", GunSoulPink.guid5, GunSoulPink.spritePaths[0], new IntVector2(3, 2), new IntVector2(8, 9));
                GunSoulPink.GunSoulPinkBehaviour GunSoulPinkBehavior = GunSoulPink.GunSoulPinkPrefab.AddComponent<GunSoulPink.GunSoulPinkBehaviour>();
                AIAnimator aiAnimator = GunSoulPinkBehavior.aiAnimator;
                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "run_right",
                        "run_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_right",
                        "idle_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "attack_right",
                        "attack_left"
                    }
                };
                bool flag3 = GunSoulPink.GunSoulPinkCollection == null;
                if (flag3)
                {
                    GunSoulPink.GunSoulPinkCollection = SpriteBuilder.ConstructCollection(GunSoulPink.GunSoulPinkPrefab, "Penguin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(GunSoulPink.GunSoulPinkCollection);
                    for (int i = 0; i < GunSoulPink.spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(GunSoulPink.spritePaths[i], GunSoulPink.GunSoulPinkCollection);
                    }
                    SpriteBuilder.AddAnimation(GunSoulPinkBehavior.spriteAnimator, GunSoulPink.GunSoulPinkCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 4.5f;
                    SpriteBuilder.AddAnimation(GunSoulPinkBehavior.spriteAnimator, GunSoulPink.GunSoulPinkCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 4.5f;
                    SpriteBuilder.AddAnimation(GunSoulPinkBehavior.spriteAnimator, GunSoulPink.GunSoulPinkCollection, new List<int>
                    {
                        8,
                        9,
                        10,
                        11,
                        12,
                        13,
                        14,
                        15
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 4.5f;
                    SpriteBuilder.AddAnimation(GunSoulPinkBehavior.spriteAnimator, GunSoulPink.GunSoulPinkCollection, new List<int>
                    {
                        16,
                        17,
                        18,
                        19,
                        20,
                        21,
                        22,
                        23
                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 4.5f;
                    SpriteBuilder.AddAnimation(GunSoulPinkBehavior.spriteAnimator, GunSoulPink.GunSoulPinkCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 4.5f;
                    SpriteBuilder.AddAnimation(GunSoulPinkBehavior.spriteAnimator, GunSoulPink.GunSoulPinkCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 4.5f;
                }
                GunSoulPinkBehavior.aiActor.MovementSpeed = 2.5f;
                GunSoulPinkBehavior.CanInterceptBullets = true;
                GunSoulPinkBehavior.aiActor.healthHaver.PreventAllDamage = false;
                GunSoulPinkBehavior.aiActor.specRigidbody.CollideWithOthers = true;
                GunSoulPinkBehavior.aiActor.specRigidbody.CollideWithTileMap = false;
                GunSoulPinkBehavior.aiActor.healthHaver.ForceSetCurrentHealth(40f);
                GunSoulPinkBehavior.aiActor.healthHaver.SetHealthMaximum(40f, null, false);
                GunSoulPinkBehavior.aiActor.specRigidbody.PixelColliders.Clear();
                GunSoulPinkBehavior.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
                {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyBulletBlocker,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 10,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                });
                BehaviorSpeculator behaviorSpeculator = GunSoulPinkBehavior.behaviorSpeculator;
                behaviorSpeculator.AttackBehaviors.Add(new GunSoulPink.GunSoulPinkAttackBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new GunSoulPink.ApproachEnemiesBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior
                {
                    IdleAnimations = new string[]
                    {
                        "idle"
                    }
                });
                UnityEngine.Object.DontDestroyOnLoad(GunSoulPink.GunSoulPinkPrefab);
                FakePrefab.MarkAsFakePrefab(GunSoulPink.GunSoulPinkPrefab);
                GunSoulPink.GunSoulPinkPrefab.SetActive(false);
            }
        }

        // Token: 0x04000016 RID: 22
        public static GameObject GunSoulPinkPrefab;

        // Token: 0x04000017 RID: 23
        public static readonly string guid5 = "lepeenkflaem";

        private List<CompanionController> companionsSpawned = new List<CompanionController>();


        // Token: 0x04000018 RID: 24
        private static string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_001.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_002.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_003.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_004.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_005.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_006.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_007.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Idle/gunsoulpink_idle_008.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_001.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_002.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_003.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_004.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_005.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_006.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_007.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Left/gunsoulpink_moveleft_008.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_001.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_002.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_003.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_004.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_005.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_006.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_007.png",
            "BunnyMod/Resources/GunSoulSprites/Pink/Right/gunsoulpink_moveright_008.png",

        };

        // Token: 0x04000019 RID: 25
        private static tk2dSpriteCollectionData GunSoulPinkCollection;

        // Token: 0x02000031 RID: 49
        public class GunSoulPinkBehaviour : CompanionController
        {
            // Token: 0x06000119 RID: 281 RVA: 0x0000A9D9 File Offset: 0x00008BD9
            private void Start()
            {
                base.spriteAnimator.Play("idle");
                this.Owner = this.m_owner;
            }

            // Token: 0x04000079 RID: 121
            public PlayerController Owner;
        }

        // Token: 0x02000032 RID: 50
        public class GunSoulPinkAttackBehavior : AttackBehaviorBase
        {


            public override void Destroy()
            {

                base.Destroy();
            }

            // Token: 0x0600011C RID: 284 RVA: 0x0000A9F9 File Offset: 0x00008BF9
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
                this.Owner = this.m_aiActor.GetComponent<GunSoulPink.GunSoulPinkBehaviour>().Owner;
            }

            // Token: 0x0600011D RID: 285 RVA: 0x0000AA1C File Offset: 0x00008C1C
            public override BehaviorResult Update()
            {
                bool flag = this.attackTimer > 0f && this.isAttacking;
                if (flag)
                {
                    base.DecrementTimer(ref this.attackTimer, false);
                }
                else
                {

                    bool flag2 = this.attackCooldownTimer > 0f && !this.isAttacking;
                    if (flag2)
                    {

                        base.DecrementTimer(ref this.attackCooldownTimer, false);
                    }
                }
                bool flag3 = this.IsReady();
                bool flag4 = (!flag3 || this.attackCooldownTimer > 0f || this.attackTimer == 0f || this.m_aiActor.TargetRigidbody == null) && this.isAttacking;
                BehaviorResult result;
                if (flag4)
                {

                    this.StopAttacking();
                    result = BehaviorResult.Continue;
                }
                else
                {

                    bool flag5 = flag3 && this.attackCooldownTimer == 0f && !this.isAttacking;
                    if (flag5)
                    {

                        this.attackTimer = this.attackDuration;
                        this.isAttacking = true;
                    }
                    bool flag6 = this.attackTimer > 0f && flag3;
                    if (flag6)
                    {

                        this.Attack();
                        result = BehaviorResult.SkipAllRemainingBehaviors;
                    }
                    else
                    {

                        result = BehaviorResult.Continue;
                    }
                }
                return result;
            }

            // Token: 0x0600011E RID: 286 RVA: 0x0000AB30 File Offset: 0x00008D30
            private void StopAttacking()
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.attackCooldownTimer = this.attackCooldown;
            }

            // Token: 0x0600011F RID: 287 RVA: 0x0000AB54 File Offset: 0x00008D54
            public AIActor GetNearestEnemy(List<AIActor> activeEnemies, Vector2 position, out float nearestDistance, string[] filter)
            {
                AIActor aiactor = null;
                nearestDistance = float.MaxValue;
                bool flag = activeEnemies == null;
                bool flag2 = flag;
                bool flag3 = flag2;
                AIActor result;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        AIActor aiactor2 = activeEnemies[i];
                        bool flag4 = aiactor2.healthHaver && aiactor2.healthHaver.IsVulnerable;
                        bool flag5 = flag4;
                        bool flag6 = flag5;
                        if (flag6)
                        {
                            bool flag7 = !aiactor2.healthHaver.IsDead;
                            bool flag8 = flag7;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                bool flag10 = filter == null || !filter.Contains(aiactor2.EnemyGuid);
                                bool flag11 = flag10;
                                bool flag12 = flag11;
                                if (flag12)
                                {
                                    float num = Vector2.Distance(position, aiactor2.CenterPosition);
                                    bool flag13 = num < nearestDistance;
                                    bool flag14 = flag13;
                                    bool flag15 = flag14;
                                    if (flag15)
                                    {
                                        nearestDistance = num;
                                        aiactor = aiactor2;
                                    }
                                }
                            }
                        }
                    }
                    result = aiactor;
                }
                return result;
            }

            // Token: 0x06000120 RID: 288 RVA: 0x0000AC74 File Offset: 0x00008E74
            private void Attack()
            {

                bool flag = this.Owner == null;
                if (flag)
                {
                    this.Owner = this.m_aiActor.GetComponent<GunSoulPink.GunSoulPinkBehaviour>().Owner;
                }
                float num = -1f;

                List<AIActor> activeEnemies = this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                bool flag2 = activeEnemies == null | activeEnemies.Count <= 0;
                if (!flag2)
                {
                    AIActor nearestEnemy = this.GetNearestEnemy(activeEnemies, this.m_aiActor.sprite.WorldCenter, out num, null);
                    bool flag3 = nearestEnemy && num < 10f;
                    if (flag3)
                    {
                        bool flag4 = this.IsInRange(nearestEnemy);
                        if (flag4)
                        {
                            bool flag5 = !nearestEnemy.IsHarmlessEnemy && nearestEnemy.IsNormalEnemy && !nearestEnemy.healthHaver.IsDead && nearestEnemy != this.m_aiActor;
                            if (flag5)
                            {

                                Vector2 unitCenter = this.m_aiActor.specRigidbody.UnitCenter;
                                Vector2 unitCenter2 = nearestEnemy.specRigidbody.HitboxPixelCollider.UnitCenter;
                                float z = BraveMathCollege.Atan2Degrees((unitCenter2 - unitCenter).normalized);
                                Projectile projectile = ((Gun)ETGMod.Databases.Items[61]).DefaultModule.projectiles[0];
                                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, z + 0f), true);
                                Projectile component = gameObject.GetComponent<Projectile>();
                                HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                                homing.HomingRadius = 10;
                                homing.AngularVelocity = 20;
                                bool flag6 = component != null;
                                bool flag7 = flag6;
                                if (flag7)
                                {
                                    component.Shooter = m_aiActor.specRigidbody;
                                    component.Owner = Owner;
                                    component.baseData.damage = 15f;
                                    component.baseData.force = .5f;
                                    component.baseData.speed = 30f;
                                    projectile.AdditionalScaleMultiplier = 1.2f;
                                    component.collidesWithPlayer = false;
                                }
                            }
                        }
                    }
                }

            }

            // Token: 0x06000121 RID: 289 RVA: 0x0000AE80 File Offset: 0x00009080
            public override float GetMaxRange()
            {
                return 5f;
            }

            // Token: 0x06000122 RID: 290 RVA: 0x0000AE98 File Offset: 0x00009098
            public override float GetMinReadyRange()
            {
                return 13f;
            }

            // Token: 0x06000123 RID: 291 RVA: 0x0000AEB0 File Offset: 0x000090B0
            public override bool IsReady()
            {
                AIActor aiActor = this.m_aiActor;
                bool flag;
                if (aiActor == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody targetRigidbody = aiActor.TargetRigidbody;
                    Vector2? vector = (targetRigidbody != null) ? new Vector2?(targetRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, this.m_aiActor.TargetRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x06000124 RID: 292 RVA: 0x0000AF30 File Offset: 0x00009130
            public bool IsInRange(AIActor enemy)
            {

                bool flag;
                if (enemy == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody specRigidbody = enemy.specRigidbody;
                    Vector2? vector = (specRigidbody != null) ? new Vector2?(specRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, enemy.specRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x0400007A RID: 122
            private bool isAttacking;

            // Token: 0x0400007B RID: 123
            private float attackCooldown = 6f;

            // Token: 0x0400007C RID: 124
            private float attackDuration = 0.1f;

            // Token: 0x0400007D RID: 125
            private float attackTimer;

            // Token: 0x0400007E RID: 126
            private float attackCooldownTimer;

            // Token: 0x0400007F RID: 127
            private PlayerController Owner;

            // Token: 0x04000080 RID: 128
            private List<AIActor> roomEnemies = new List<AIActor>();
        }


        public class ApproachEnemiesBehavior : MovementBehaviorBase
        {
            // Token: 0x06000126 RID: 294 RVA: 0x00009E97 File Offset: 0x00008097
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
            }

            // Token: 0x06000127 RID: 295 RVA: 0x0000AFCF File Offset: 0x000091CF
            public override void Upkeep()
            {
                base.Upkeep();
                base.DecrementTimer(ref this.repathTimer, false);
            }

            // Token: 0x06000128 RID: 296 RVA: 0x0000AFE8 File Offset: 0x000091E8
            public override BehaviorResult Update()
            {
                SpeculativeRigidbody overrideTarget = this.m_aiActor.OverrideTarget;
                bool flag = this.repathTimer > 0f;
                BehaviorResult result;
                if (flag)
                {
                    result = ((overrideTarget == null) ? BehaviorResult.Continue : BehaviorResult.SkipRemainingClassBehaviors);
                }
                else
                {
                    bool flag2 = overrideTarget == null;
                    if (flag2)
                    {
                        this.PickNewTarget();
                        result = BehaviorResult.Continue;
                    }
                    else
                    {
                        this.isInRange = (Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, overrideTarget.UnitCenter) <= this.DesiredDistance);
                        bool flag3 = overrideTarget != null && !this.isInRange;
                        if (flag3)
                        {
                            this.m_aiActor.PathfindToPosition(overrideTarget.UnitCenter, null, true, null, null, null, false);
                            this.repathTimer = this.PathInterval;
                            result = BehaviorResult.SkipRemainingClassBehaviors;
                        }
                        else
                        {
                            bool flag4 = overrideTarget != null && this.repathTimer >= 0f;
                            if (flag4)
                            {
                                this.m_aiActor.ClearPath();
                                this.repathTimer = -1f;
                            }
                            result = BehaviorResult.Continue;
                        }
                    }
                }
                return result;
            }

            // Token: 0x06000129 RID: 297 RVA: 0x0000B104 File Offset: 0x00009304
            private void PickNewTarget()
            {

                bool flag = this.m_aiActor == null;
                if (!flag)
                {
                    bool flag2 = this.Owner == null;
                    if (flag2)
                    {
                        this.Owner = this.m_aiActor.GetComponent<GunSoulPink.GunSoulPinkBehaviour>().Owner;
                    }
                    this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref this.roomEnemies);
                    for (int i = 0; i < this.roomEnemies.Count; i++)
                    {
                        AIActor aiactor = this.roomEnemies[i];
                        bool flag3 = aiactor.IsHarmlessEnemy || !aiactor.IsNormalEnemy || aiactor.healthHaver.IsDead || aiactor == this.m_aiActor || aiactor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc";
                        if (flag3)
                        {

                            this.roomEnemies.Remove(aiactor);

                        }
                    }
                    bool flag4 = this.roomEnemies.Count == 0;
                    if (flag4)
                    {
                        this.m_aiActor.OverrideTarget = null;
                    }
                    else
                    {
                        AIActor aiActor = this.m_aiActor;
                        AIActor aiactor2 = this.roomEnemies[UnityEngine.Random.Range(0, this.roomEnemies.Count)];
                        aiActor.OverrideTarget = ((aiactor2 != null) ? aiactor2.specRigidbody : null);
                    }
                }
            }


            public float PathInterval = 1f;

            public float DesiredDistance = 8f;

            private float repathTimer;

            private List<AIActor> roomEnemies = new List<AIActor>();

            private bool isInRange;

            private PlayerController Owner;
        }
    }
}

namespace BunnyMod
{
    public class GunSoulPurple : CompanionItem
    {
        public static void PurpleBuildPrefab()
        {

            bool flag = GunSoulPurple.GunSoulPurplePrefab != null || CompanionBuilder.companionDictionary.ContainsKey(GunSoulPurple.guid6);
            bool flag2 = flag;
            if (!flag2)
            {
                GunSoulPurple.GunSoulPurplePrefab = CompanionBuilder.BuildPrefab("Peenk", GunSoulPurple.guid6, GunSoulPurple.spritePaths[0], new IntVector2(3, 2), new IntVector2(8, 9));
                GunSoulPurple.GunSoulPurpleBehaviour GunSoulPurpleBehavior = GunSoulPurple.GunSoulPurplePrefab.AddComponent<GunSoulPurple.GunSoulPurpleBehaviour>();
                AIAnimator aiAnimator = GunSoulPurpleBehavior.aiAnimator;
                aiAnimator.MoveAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "run_right",
                        "run_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "idle_right",
                        "idle_left"
                    }
                };
                aiAnimator.IdleAnimation = new DirectionalAnimation
                {
                    Type = DirectionalAnimation.DirectionType.TwoWayHorizontal,
                    Flipped = new DirectionalAnimation.FlipType[2],
                    AnimNames = new string[]
                    {
                        "attack_right",
                        "attack_left"
                    }
                };
                bool flag3 = GunSoulPurple.GunSoulPurpleCollection == null;
                if (flag3)
                {
                    GunSoulPurple.GunSoulPurpleCollection = SpriteBuilder.ConstructCollection(GunSoulPurple.GunSoulPurplePrefab, "Penguin_Collection");
                    UnityEngine.Object.DontDestroyOnLoad(GunSoulPurple.GunSoulPurpleCollection);
                    for (int i = 0; i < GunSoulPurple.spritePaths.Length; i++)
                    {
                        SpriteBuilder.AddSpriteToCollection(GunSoulPurple.spritePaths[i], GunSoulPurple.GunSoulPurpleCollection);
                    }
                    SpriteBuilder.AddAnimation(GunSoulPurpleBehavior.spriteAnimator, GunSoulPurple.GunSoulPurpleCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 9.5f;
                    SpriteBuilder.AddAnimation(GunSoulPurpleBehavior.spriteAnimator, GunSoulPurple.GunSoulPurpleCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "idle_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 9.5f;
                    SpriteBuilder.AddAnimation(GunSoulPurpleBehavior.spriteAnimator, GunSoulPurple.GunSoulPurpleCollection, new List<int>
                    {
                        8,
                        9,
                        10,
                        11,
                        12,
                        13,
                        14,
                        15
                    }, "run_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 9.5f;
                    SpriteBuilder.AddAnimation(GunSoulPurpleBehavior.spriteAnimator, GunSoulPurple.GunSoulPurpleCollection, new List<int>
                    {
                        16,
                        17,
                        18,
                        19,
                        20,
                        21,
                        22,
                        23
                    }, "run_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 9.5f;
                    SpriteBuilder.AddAnimation(GunSoulPurpleBehavior.spriteAnimator, GunSoulPurple.GunSoulPurpleCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_right", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 9.5f;
                    SpriteBuilder.AddAnimation(GunSoulPurpleBehavior.spriteAnimator, GunSoulPurple.GunSoulPurpleCollection, new List<int>
                    {
                        0,
                        1,
                        2,
                        3,
                        4,
                        5,
                        6,
                        7
                    }, "attack_left", tk2dSpriteAnimationClip.WrapMode.Loop).fps = 9.5f;
                }
                GunSoulPurpleBehavior.aiActor.MovementSpeed = 11.5f;
                GunSoulPurpleBehavior.CanInterceptBullets = true;
                GunSoulPurpleBehavior.aiActor.healthHaver.PreventAllDamage = false;
                GunSoulPurpleBehavior.aiActor.specRigidbody.CollideWithOthers = true;
                GunSoulPurpleBehavior.aiActor.specRigidbody.CollideWithTileMap = false;
                GunSoulPurpleBehavior.aiActor.healthHaver.ForceSetCurrentHealth(100f);
                GunSoulPurpleBehavior.aiActor.healthHaver.SetHealthMaximum(100f, null, false);
                GunSoulPurpleBehavior.aiActor.specRigidbody.PixelColliders.Clear();
                GunSoulPurpleBehavior.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
                {
                    ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
                    CollisionLayer = CollisionLayer.EnemyBulletBlocker,
                    IsTrigger = false,
                    BagleUseFirstFrameOnly = false,
                    SpecifyBagelFrame = string.Empty,
                    BagelColliderNumber = 0,
                    ManualOffsetX = 0,
                    ManualOffsetY = 0,
                    ManualWidth = 10,
                    ManualHeight = 10,
                    ManualDiameter = 0,
                    ManualLeftX = 0,
                    ManualLeftY = 0,
                    ManualRightX = 0,
                    ManualRightY = 0
                });
                BehaviorSpeculator behaviorSpeculator = GunSoulPurpleBehavior.behaviorSpeculator;
                behaviorSpeculator.AttackBehaviors.Add(new GunSoulPurple.GunSoulPurpleAttackBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new GunSoulPurple.ApproachEnemiesBehavior());
                behaviorSpeculator.MovementBehaviors.Add(new CompanionFollowPlayerBehavior
                {
                    IdleAnimations = new string[]
                    {
                        "idle"
                    }
                });
                UnityEngine.Object.DontDestroyOnLoad(GunSoulPurple.GunSoulPurplePrefab);
                FakePrefab.MarkAsFakePrefab(GunSoulPurple.GunSoulPurplePrefab);
                GunSoulPurple.GunSoulPurplePrefab.SetActive(false);
            }
        }

        // Token: 0x04000016 RID: 22
        public static GameObject GunSoulPurplePrefab;

        // Token: 0x04000017 RID: 23
        public static readonly string guid6 = "the purple flame behind the slaughter";

        private List<CompanionController> companionsSpawned = new List<CompanionController>();


        // Token: 0x04000018 RID: 24
        private static string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_001.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_002.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_003.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_004.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_005.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_006.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_007.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Idle/gunsoulpurple_idle_008.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_001.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_002.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_003.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_004.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_005.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_006.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_007.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Left/gunsoulpurple_moveleft_008.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_001.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_002.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_003.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_004.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_005.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_006.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_007.png",
            "BunnyMod/Resources/GunSoulSprites/Purple/Right/gunsoulpurple_moveright_008.png",

        };

        // Token: 0x04000019 RID: 25
        private static tk2dSpriteCollectionData GunSoulPurpleCollection;

        // Token: 0x02000031 RID: 49
        public class GunSoulPurpleBehaviour : CompanionController
        {
            // Token: 0x06000119 RID: 281 RVA: 0x0000A9D9 File Offset: 0x00008BD9
            private void Start()
            {
                base.spriteAnimator.Play("idle");
                this.Owner = this.m_owner;
            }

            // Token: 0x04000079 RID: 121
            public PlayerController Owner;
        }

        // Token: 0x02000032 RID: 50
        public class GunSoulPurpleAttackBehavior : AttackBehaviorBase
        {


            public override void Destroy()
            {

                base.Destroy();
            }

            // Token: 0x0600011C RID: 284 RVA: 0x0000A9F9 File Offset: 0x00008BF9
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
                this.Owner = this.m_aiActor.GetComponent<GunSoulPurple.GunSoulPurpleBehaviour>().Owner;
            }

            // Token: 0x0600011D RID: 285 RVA: 0x0000AA1C File Offset: 0x00008C1C
            public override BehaviorResult Update()
            {
                bool flag = this.attackTimer > 0f && this.isAttacking;
                if (flag)
                {
                    base.DecrementTimer(ref this.attackTimer, false);
                }
                else
                {

                    bool flag2 = this.attackCooldownTimer > 0f && !this.isAttacking;
                    if (flag2)
                    {

                        base.DecrementTimer(ref this.attackCooldownTimer, false);
                    }
                }
                bool flag3 = this.IsReady();
                bool flag4 = (!flag3 || this.attackCooldownTimer > 0f || this.attackTimer == 0f || this.m_aiActor.TargetRigidbody == null) && this.isAttacking;
                BehaviorResult result;
                if (flag4)
                {

                    this.StopAttacking();
                    result = BehaviorResult.Continue;
                }
                else
                {

                    bool flag5 = flag3 && this.attackCooldownTimer == 0f && !this.isAttacking;
                    if (flag5)
                    {

                        this.attackTimer = this.attackDuration;
                        this.isAttacking = true;
                    }
                    bool flag6 = this.attackTimer > 0f && flag3;
                    if (flag6)
                    {

                        this.Attack();
                        result = BehaviorResult.SkipAllRemainingBehaviors;
                    }
                    else
                    {

                        result = BehaviorResult.Continue;
                    }
                }
                return result;
            }

            // Token: 0x0600011E RID: 286 RVA: 0x0000AB30 File Offset: 0x00008D30
            private void StopAttacking()
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.attackCooldownTimer = this.attackCooldown;
            }

            // Token: 0x0600011F RID: 287 RVA: 0x0000AB54 File Offset: 0x00008D54
            public AIActor GetNearestEnemy(List<AIActor> activeEnemies, Vector2 position, out float nearestDistance, string[] filter)
            {
                AIActor aiactor = null;
                nearestDistance = float.MaxValue;
                bool flag = activeEnemies == null;
                bool flag2 = flag;
                bool flag3 = flag2;
                AIActor result;
                if (flag3)
                {
                    result = null;
                }
                else
                {
                    for (int i = 0; i < activeEnemies.Count; i++)
                    {
                        AIActor aiactor2 = activeEnemies[i];
                        bool flag4 = aiactor2.healthHaver && aiactor2.healthHaver.IsVulnerable;
                        bool flag5 = flag4;
                        bool flag6 = flag5;
                        if (flag6)
                        {
                            bool flag7 = !aiactor2.healthHaver.IsDead;
                            bool flag8 = flag7;
                            bool flag9 = flag8;
                            if (flag9)
                            {
                                bool flag10 = filter == null || !filter.Contains(aiactor2.EnemyGuid);
                                bool flag11 = flag10;
                                bool flag12 = flag11;
                                if (flag12)
                                {
                                    float num = Vector2.Distance(position, aiactor2.CenterPosition);
                                    bool flag13 = num < nearestDistance;
                                    bool flag14 = flag13;
                                    bool flag15 = flag14;
                                    if (flag15)
                                    {
                                        nearestDistance = num;
                                        aiactor = aiactor2;
                                    }
                                }
                            }
                        }
                    }
                    result = aiactor;
                }
                return result;
            }

            // Token: 0x06000120 RID: 288 RVA: 0x0000AC74 File Offset: 0x00008E74
            private void Attack()
            {

                bool flag = this.Owner == null;
                if (flag)
                {
                    this.Owner = this.m_aiActor.GetComponent<GunSoulPurple.GunSoulPurpleBehaviour>().Owner;
                }
                float num = -1f;

                List<AIActor> activeEnemies = this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                bool flag2 = activeEnemies == null | activeEnemies.Count <= 0;
                if (!flag2)
                {
                    AIActor nearestEnemy = this.GetNearestEnemy(activeEnemies, this.m_aiActor.sprite.WorldCenter, out num, null);
                    bool flag3 = nearestEnemy && num < 10f;
                    if (flag3)
                    {
                        bool flag4 = this.IsInRange(nearestEnemy);
                        if (flag4)
                        {
                            bool flag5 = !nearestEnemy.IsHarmlessEnemy && nearestEnemy.IsNormalEnemy && !nearestEnemy.healthHaver.IsDead && nearestEnemy != this.m_aiActor;
                            if (flag5)
                            {

                                Vector2 unitCenter = this.m_aiActor.specRigidbody.UnitCenter;
                                Vector2 unitCenter2 = nearestEnemy.specRigidbody.HitboxPixelCollider.UnitCenter;
                                float z = BraveMathCollege.Atan2Degrees((unitCenter2 - unitCenter).normalized);
                                Projectile projectile = ((Gun)ETGMod.Databases.Items[178]).DefaultModule.projectiles[0];
                                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, z + 0f), true);
                                Projectile component = gameObject.GetComponent<Projectile>();
                                HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                                homing.HomingRadius = 10;
                                homing.AngularVelocity = 20;
                                bool flag6 = component != null;
                                bool flag7 = flag6;
                                if (flag7)
                                {
                                    component.Shooter = m_aiActor.specRigidbody;
                                    component.Owner = Owner;
                                    component.baseData.damage = 20f;
                                    component.baseData.force = .5f;
                                    component.baseData.speed = 30f;
                                    component.baseData.range = 2f;
                                    projectile.AdditionalScaleMultiplier = 0.2f;
                                    component.collidesWithPlayer = false;
                                }
                            }
                        }
                    }
                }

            }

            // Token: 0x06000121 RID: 289 RVA: 0x0000AE80 File Offset: 0x00009080
            public override float GetMaxRange()
            {
                return 1f;
            }

            // Token: 0x06000122 RID: 290 RVA: 0x0000AE98 File Offset: 0x00009098
            public override float GetMinReadyRange()
            {
                return 1.5f;
            }

            // Token: 0x06000123 RID: 291 RVA: 0x0000AEB0 File Offset: 0x000090B0
            public override bool IsReady()
            {
                AIActor aiActor = this.m_aiActor;
                bool flag;
                if (aiActor == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody targetRigidbody = aiActor.TargetRigidbody;
                    Vector2? vector = (targetRigidbody != null) ? new Vector2?(targetRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, this.m_aiActor.TargetRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x06000124 RID: 292 RVA: 0x0000AF30 File Offset: 0x00009130
            public bool IsInRange(AIActor enemy)
            {

                bool flag;
                if (enemy == null)
                {
                    flag = true;
                }
                else
                {
                    SpeculativeRigidbody specRigidbody = enemy.specRigidbody;
                    Vector2? vector = (specRigidbody != null) ? new Vector2?(specRigidbody.UnitCenter) : null;
                    flag = (vector == null);
                }
                bool flag2 = flag;
                return !flag2 && Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, enemy.specRigidbody.UnitCenter) <= this.GetMinReadyRange();
            }

            // Token: 0x0400007A RID: 122
            private bool isAttacking;

            // Token: 0x0400007B RID: 123
            private float attackCooldown = 5f;

            // Token: 0x0400007C RID: 124
            private float attackDuration = 0.1f;

            // Token: 0x0400007D RID: 125
            private float attackTimer;

            // Token: 0x0400007E RID: 126
            private float attackCooldownTimer;

            // Token: 0x0400007F RID: 127
            private PlayerController Owner;

            // Token: 0x04000080 RID: 128
            private List<AIActor> roomEnemies = new List<AIActor>();
        }


        public class ApproachEnemiesBehavior : MovementBehaviorBase
        {
            // Token: 0x06000126 RID: 294 RVA: 0x00009E97 File Offset: 0x00008097
            public override void Init(GameObject gameObject, AIActor aiActor, AIShooter aiShooter)
            {
                base.Init(gameObject, aiActor, aiShooter);
            }

            // Token: 0x06000127 RID: 295 RVA: 0x0000AFCF File Offset: 0x000091CF
            public override void Upkeep()
            {
                base.Upkeep();
                base.DecrementTimer(ref this.repathTimer, false);
            }

            // Token: 0x06000128 RID: 296 RVA: 0x0000AFE8 File Offset: 0x000091E8
            public override BehaviorResult Update()
            {
                SpeculativeRigidbody overrideTarget = this.m_aiActor.OverrideTarget;
                bool flag = this.repathTimer > 0f;
                BehaviorResult result;
                if (flag)
                {
                    result = ((overrideTarget == null) ? BehaviorResult.Continue : BehaviorResult.SkipRemainingClassBehaviors);
                }
                else
                {
                    bool flag2 = overrideTarget == null;
                    if (flag2)
                    {
                        this.PickNewTarget();
                        result = BehaviorResult.Continue;
                    }
                    else
                    {
                        this.isInRange = (Vector2.Distance(this.m_aiActor.specRigidbody.UnitCenter, overrideTarget.UnitCenter) <= this.DesiredDistance);
                        bool flag3 = overrideTarget != null && !this.isInRange;
                        if (flag3)
                        {
                            this.m_aiActor.PathfindToPosition(overrideTarget.UnitCenter, null, true, null, null, null, false);
                            this.repathTimer = this.PathInterval;
                            result = BehaviorResult.SkipRemainingClassBehaviors;
                        }
                        else
                        {
                            bool flag4 = overrideTarget != null && this.repathTimer >= 0f;
                            if (flag4)
                            {
                                this.m_aiActor.ClearPath();
                                this.repathTimer = -1f;
                            }
                            result = BehaviorResult.Continue;
                        }
                    }
                }
                return result;
            }

            // Token: 0x06000129 RID: 297 RVA: 0x0000B104 File Offset: 0x00009304
            private void PickNewTarget()
            {

                bool flag = this.m_aiActor == null;
                if (!flag)
                {
                    bool flag2 = this.Owner == null;
                    if (flag2)
                    {
                        this.Owner = this.m_aiActor.GetComponent<GunSoulPurple.GunSoulPurpleBehaviour>().Owner;
                    }
                    this.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All, ref this.roomEnemies);
                    for (int i = 0; i < this.roomEnemies.Count; i++)
                    {
                        AIActor aiactor = this.roomEnemies[i];
                        bool flag3 = aiactor.IsHarmlessEnemy || !aiactor.IsNormalEnemy || aiactor.healthHaver.IsDead || aiactor == this.m_aiActor || aiactor.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc";
                        if (flag3)
                        {

                            this.roomEnemies.Remove(aiactor);

                        }
                    }
                    bool flag4 = this.roomEnemies.Count == 0;
                    if (flag4)
                    {
                        this.m_aiActor.OverrideTarget = null;
                    }
                    else
                    {
                        AIActor aiActor = this.m_aiActor;
                        AIActor aiactor2 = this.roomEnemies[UnityEngine.Random.Range(0, this.roomEnemies.Count)];
                        aiActor.OverrideTarget = ((aiactor2 != null) ? aiactor2.specRigidbody : null);
                    }
                }
            }


            public float PathInterval = 1f;

            public float DesiredDistance = 1f;

            private float repathTimer;

            private List<AIActor> roomEnemies = new List<AIActor>();

            private bool isInRange;

            private PlayerController Owner;
        }
    }
}

