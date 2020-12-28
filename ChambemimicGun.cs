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
	// Token: 0x0200002D RID: 45
	public class ChambemimicGun : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Chambemimic Gun", "chambemimic");
			Game.Items.Rename("outdated_gun_mods:chambemimic_gun", "bny:chambemimic_gun");
			gun.gameObject.AddComponent<ChambemimicGun>();
			GunExt.SetShortDescription(gun, "Non-Product of Environment");
			GunExt.SetLongDescription(gun, "Transforms based on Nothing.\n\nThis gun uses an advanced shape - mimic flesh.When exposed to different environmental stimuli it drinks up the salts in Gungeoneer’s bodies and transforms into nothing special.Master rounds cannot influence the shape of- Hold on is it breathing?");
			GunExt.SetupSprite(gun, null, "chambemimic_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 24);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 15);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(86) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(53) as Gun).gunSwitchGroup;

			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.6f;
			gun.carryPixelOffset = new IntVector2((int)7f, (int)-0.6f);
			gun.DefaultModule.cooldownTime = 0.3f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 25;
			gun.SetBaseMaxAmmo(250);
			gun.DefaultModule.angleVariance = 10f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(146) as Gun).muzzleFlashEffects;
			projectile.baseData.damage = 12.5f;
			projectile.baseData.speed = 20f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			projectile.SetProjectileSpriteRight("chambemimic_projectile_001", 18, 21, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(11), new int?(5), null, null, null);
			gun.quality = PickupObject.ItemQuality.C;
			gun.encounterTrackable.EncounterGuid = "Chamber Chamber Chamber Chamber Chamber Chamber Chamber Chamber ";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			ChambemimicGun.ChambemimicID = gun.PickupObjectId;

		}
        public static int ChambemimicID;
        public override void PostProcessProjectile(Projectile projectile)
		{
            bool hasDeterminedValidFloor = false;
            if (GameManager.Instance.Dungeon.IsGlitchDungeon) //GLITCHED FLOOR BONUS
            {
                projectile.baseData.damage *= 3;
            }
            else
            {
                switch (GameManager.Instance.Dungeon.tileIndices.tilesetId)
                {
                    case GlobalDungeonData.ValidTilesets.CASTLEGEON: //KEEP
                        {
                            projectile.baseData.damage *= 1.4f;
                            projectile.baseData.speed *= 1.4f;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.SEWERGEON: //OUBLIETTE
                        {
                            PoisonForDummiesLikeMe auegh = projectile.gameObject.AddComponent<PoisonForDummiesLikeMe>();
                            auegh.procChance = 1;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.JUNGLEGEON: //JUNGLE
                        {
                            projectile.baseData.damage *= 1.6f;
                            projectile.baseData.speed *= 1.6f;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.GUNGEON: //GUNGEON PROPER
                        {
                            projectile.baseData.range = 9f;
                            projectile.baseData.speed = 15f;
                            projectile.AdditionalScaleMultiplier *= 1.25f;
                            projectile.baseData.damage *= 2f;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.CATHEDRALGEON: //ABBEY
                        {
                            HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
                            homing.HomingRadius = 25f;
                            homing.AngularVelocity = 100f;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.MINEGEON: //MINES
                        {
                            projectile.baseData.damage *= 1.6f;
                            projectile.baseData.force *= 2f;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.RATGEON: //RAT LAIR
                        {
                            Cheesesprayer cheese = projectile.gameObject.AddComponent<Cheesesprayer>();
                            cheese.projectileToSpawn = (PickupObjectDatabase.GetById(626) as Gun).DefaultModule.projectiles[0];
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.CATACOMBGEON: // HOLLOW
                        {
                            projectile.OnHitEnemy += (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.cold));

                            //FIX FREEZE EFFECT
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.OFFICEGEON: //R&G DEPT
                        {
                            projectile.OnDestruction += this.kerboomer;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.WESTGEON: //OLD WEST FLOOR (EXPAND)
                        {
                            projectile.baseData.damage *= 1.8f;
                            projectile.baseData.speed *= 1.8f;
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.FORGEGEON: //FORGE
                        {
                            projectile.OnHitEnemy += (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.fire));

                           
                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.HELLGEON: //BULLET HELL
                        if (GameManager.IsGunslingerPast)
                        {
                            projectile.specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(projectile.specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.HandlePreCollision));
                        }
                        else
                        {
                            projectile.specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(projectile.specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.HandlePreCollision));

                        }
                        hasDeterminedValidFloor = true;
                        break;
                    case GlobalDungeonData.ValidTilesets.BELLYGEON: //BELLY
                        {
                            projectile.baseData.damage *= 1.8f;
                            projectile.baseData.speed *= 1.8f;
                        }
                        break;
                }
                //-----------------------------------------DEFAULT CATCH EFFECT
                if (!hasDeterminedValidFloor)
                {
                    projectile.baseData.damage *= 3;
                }
            }

        }
        private void HandlePreCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider)
        {
            bool flag = otherRigidbody && otherRigidbody.healthHaver;
            if (flag)
            {
                float maxHealth = otherRigidbody.healthHaver.GetMaxHealth();
                float num = maxHealth * 0.5f;
                float currentHealth = otherRigidbody.healthHaver.GetCurrentHealth();
                bool flag2 = currentHealth < num;
                if (flag2)
                {
                    float damage = myRigidbody.projectile.baseData.damage;
                    myRigidbody.projectile.baseData.damage *= 4f;
                    GameManager.Instance.StartCoroutine(this.ChangeProjectileDamage(myRigidbody.projectile, damage));
                }
            }
        }
        private void fire(Projectile arg1, SpeculativeRigidbody arg2, bool arg3)
        {
            PlayerController player = (GameManager.Instance.PrimaryPlayer);
            bool isInCombat = player.IsInCombat;
            if (isInCombat)
            {
                List<AIActor> activeEnemies = player.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                Vector2 centerPosition = arg1.sprite.WorldCenter;
                foreach (AIActor aiactor in activeEnemies)
                {
                    BulletStatusEffectItem Firecomponent = PickupObjectDatabase.GetById(295).GetComponent<BulletStatusEffectItem>();
                    GameActorFireEffect gameActorFire = Firecomponent.FireModifierEffect;
                    bool flag3 = Vector2.Distance(aiactor.CenterPosition, centerPosition) < 3f && aiactor.healthHaver.GetMaxHealth() > 0f && aiactor != null && aiactor.specRigidbody != null && player != null;
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        {
                            aiactor.ApplyEffect(gameActorFire, 1f, null);
                        }
                    }
                }
            }
        }
        private void cold(Projectile arg1, SpeculativeRigidbody arg2, bool arg3)
        {
            PlayerController player = (GameManager.Instance.PrimaryPlayer);
            //FIX FIRE EFFECT
            bool isInCombat = player.IsInCombat;
            if (isInCombat)
            {
                List<AIActor> activeEnemies = player.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                Vector2 centerPosition = arg1.sprite.WorldCenter;
                foreach (AIActor aiactor in activeEnemies)
                {
                    BulletStatusEffectItem Freezzecomponent = PickupObjectDatabase.GetById(278).GetComponent<BulletStatusEffectItem>();
                    GameActorFreezeEffect gameActorFreeze = Freezzecomponent.FreezeModifierEffect;
                    bool flag3 = Vector2.Distance(aiactor.CenterPosition, centerPosition) < 3f && aiactor.healthHaver.GetMaxHealth() > 0f && aiactor != null && aiactor.specRigidbody != null && player != null;
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        {
                            aiactor.ApplyEffect(gameActorFreeze, 1f, null);
                        }
                    }
                }
            }
        }
        private void kerboomer(Projectile projectile)
        {
            Exploder.DoDefaultExplosion(projectile.specRigidbody.UnitTopCenter, default(Vector2), null, false, CoreDamageTypes.None, true);
        }
        public void Blam(Vector3 position)
        {
            AkSoundEngine.PostEvent("Play_OBJ_nuke_blast_01", gameObject);
            ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
            this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
            this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
        }
        private ExplosionData smallPlayerSafeExplosion = new ExplosionData
        {
            damageRadius = 4f,
            damageToPlayer = 0f,
            doDamage = true,
            damage = 40f,
            doExplosionRing = true,
            doDestroyProjectiles = true,
            doForce = true,
            debrisForce = 50f,
            preventPlayerForce = true,
            explosionDelay = 0f,
            usesComprehensiveDelay = false,
            doScreenShake = true,
            playDefaultSFX = true,
        };
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
        public GameActorFreezeEffect FreezeModifierEffect;
        public bool FreezeScalesWithDamage;
        public float FreezeAmountPerDamage = 1f;
        public Color FreezeTintColor;
    }
}


namespace BunnyMod
{
    // Token: 0x02000075 RID: 117
    public class Cheesesprayer : MonoBehaviour
    {
        // Token: 0x06000297 RID: 663 RVA: 0x000184B9 File Offset: 0x000166B9
        public Cheesesprayer()
        {
            this.projectileToSpawn = null;
        }

        // Token: 0x06000298 RID: 664 RVA: 0x000184D5 File Offset: 0x000166D5
        private void Awake()
        {
            this.m_projectile = base.GetComponent<Projectile>();
            this.speculativeRigidBoy = base.GetComponent<SpeculativeRigidbody>();
        }

        // Token: 0x06000299 RID: 665 RVA: 0x000184F0 File Offset: 0x000166F0
        private void Update()
        {
            bool flag = this.m_projectile == null;
            if (flag)
            {
                this.m_projectile = base.GetComponent<Projectile>();
            }
            bool flag2 = this.speculativeRigidBoy == null;
            if (flag2)
            {
                this.speculativeRigidBoy = base.GetComponent<SpeculativeRigidbody>();
            }
            this.elapsed += BraveTime.DeltaTime;
            bool flag3 = this.elapsed > 0.33f;
            if (flag3)
            {
                this.spawnAngle = 150f;
                this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
                this.spawnAngle = 210f;
                this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
                this.elapsed = 0f;
                this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
            }
        }

        // Token: 0x0600029A RID: 666 RVA: 0x000185BC File Offset: 0x000167BC
        private void SpawnProjectile(Projectile proj, Vector3 spawnPosition, float zRotation, SpeculativeRigidbody collidedRigidbody = null)
        {
            GameObject gameObject = SpawnManager.SpawnProjectile(proj.gameObject, spawnPosition, Quaternion.Euler(0f, 0f, zRotation), true);
            Projectile component = gameObject.GetComponent<Projectile>();
            bool flag = component;
            if (flag)
            {
                component.SpawnedFromOtherPlayerProjectile = true;
                PlayerController playerController = this.m_projectile.Owner as PlayerController;
                component.baseData.damage *= playerController.stats.GetStatValue(PlayerStats.StatType.Damage);
                component.baseData.speed *= 1.5f;
                playerController.DoPostProcessProjectile(component);
                PierceProjModifier spook = component.gameObject.AddComponent<PierceProjModifier>();
                spook.penetration = 2;
                component.AdditionalScaleMultiplier = 0.8f;
                component.baseData.range = 5f;
            }
        }

        // Token: 0x040000EA RID: 234
        private float spawnAngle = 90f;

        // Token: 0x040000EB RID: 235
        private Projectile m_projectile;

        // Token: 0x040000EC RID: 236
        private SpeculativeRigidbody speculativeRigidBoy;

        // Token: 0x040000ED RID: 237
        public Projectile projectileToSpawn;

        // Token: 0x040000EE RID: 238
        private float elapsed;
    }
}