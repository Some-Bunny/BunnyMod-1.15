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
	public class SoulStealer : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun trollmimic = ETGMod.Databases.Items.NewGun("Soul Stealer", "soulstealer");
			Game.Items.Rename("outdated_gun_mods:soul_stealer", "bny:soul_stealer");
			trollmimic.gameObject.AddComponent<SoulStealer>();
			GunExt.SetShortDescription(trollmimic, "S u c c");
			GunExt.SetLongDescription(trollmimic, "Although very weak, it has the ability to suck the souls of those it slays.");
			GunExt.SetupSprite(trollmimic, null, "soulstealer_idle_001", 11);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.shootAnimation, 36);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.reloadAnimation, 14);
			GunExt.SetAnimationFPS(trollmimic, trollmimic.idleAnimation, 8);
            GunExt.AddProjectileModuleFrom(trollmimic, PickupObjectDatabase.GetById(56) as Gun, true, false);
            trollmimic.gunSwitchGroup = (PickupObjectDatabase.GetById(336) as Gun).gunSwitchGroup;
            trollmimic.barrelOffset.transform.localPosition = new Vector3(2f, 0.375f, 0f);
            trollmimic.DefaultModule.ammoCost = 1;
			trollmimic.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			trollmimic.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			trollmimic.reloadTime = 1.5f;
			trollmimic.DefaultModule.cooldownTime = .1f;
			trollmimic.DefaultModule.numberOfShotsInClip = 100;
			trollmimic.SetBaseMaxAmmo(300);
			trollmimic.quality = PickupObject.ItemQuality.A;
			trollmimic.DefaultModule.angleVariance = 2f;
			trollmimic.DefaultModule.burstShotCount = 1;
			trollmimic.encounterTrackable.EncounterGuid = "bonk";
            trollmimic.muzzleFlashEffects = (PickupObjectDatabase.GetById(223) as Gun).muzzleFlashEffects;
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(trollmimic.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			trollmimic.DefaultModule.projectiles[0] = projectile;
            projectile.DefaultTintColor = new Color(0f, 0f, 10f).WithAlpha(0.5f);
            projectile.HasDefaultTint = true;
            projectile.baseData.damage = 2.5f;
			projectile.baseData.speed *= 1.25f;
			projectile.AdditionalScaleMultiplier = 1.1f;
			projectile.pierceMinorBreakables = true;
            projectile.shouldRotate = true;
			projectile.transform.parent = trollmimic.barrelOffset;
            projectile.SetProjectileSpriteRight("soulstealer_projectile_001", 18, 9, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(9), new int?(5), null, null, null);
            ETGMod.Databases.Items.Add(trollmimic, null, "ANY");
            trollmimic.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            trollmimic.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
        }

        public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnWillKillEnemy = (Action<Projectile, SpeculativeRigidbody>)Delegate.Combine(projectile.OnWillKillEnemy, new Action<Projectile, SpeculativeRigidbody>(this.OnKill));
		}
		private void OnKill(Projectile arg1, SpeculativeRigidbody arg2)
        {
            PlayerController player = this.gun.CurrentOwner as PlayerController;
            bool flag = !arg2.aiActor.healthHaver.IsDead;
			if (flag)
			{
                AssetBundle assetBundle1 = ResourceManager.LoadAssetBundle("shared_auto_001");
                this.Nuke = assetBundle1.LoadAsset<GameObject>("assets/data/vfx prefabs/vfx_explosion_firework.prefab");
                GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.Nuke);
                gameObject1.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(arg2.specRigidbody.UnitCenter, tk2dBaseSprite.Anchor.LowerCenter);
                gameObject1.transform.position = gameObject.transform.position.Quantize(0.0625f);
                gameObject1.GetComponent<tk2dBaseSprite>().UpdateZDepth();
                this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                if (random <= 0.33f)
                {
                    AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulBlue.guid1);
                    Vector3 vector = arg2.transform.position;
                    bool flag9 = GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER;
                    bool flag10 = flag9;
                    bool flag11 = flag10;
                    bool flag12 = flag11;
                    if (flag12)
                    {
                        vector += new Vector3(1.125f, -0.3125f, 0f);
                    }
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
                    CompanionController orAddComponent = gameObject.GetOrAddComponent<CompanionController>();
                    this.companionsSpawned.Add(orAddComponent);
                    orAddComponent.Initialize(player);
                    bool flag13 = orAddComponent.specRigidbody;
                    bool flag14 = flag13;
                    bool flag15 = flag14;
                    bool flag16 = flag15;
                    if (flag16)
                    {
                        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
                    }
                }
                else
                {
                    int num3 = UnityEngine.Random.Range(0, 4);
                    bool flag3 = num3 == 0;
                    if (flag3)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulGreen.guid2);
                        Vector3 vector = arg2.transform.position;
                        bool flag9 = GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER;
                        bool flag10 = flag9;
                        bool flag11 = flag10;
                        bool flag12 = flag11;
                        if (flag12)
                        {
                            vector += new Vector3(1.125f, -0.3125f, 0f);
                        }
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
                        CompanionController orAddComponent = gameObject.GetOrAddComponent<CompanionController>();
                        this.companionsSpawned.Add(orAddComponent);
                        orAddComponent.Initialize(player);
                        bool flag13 = orAddComponent.specRigidbody;
                        bool flag14 = flag13;
                        bool flag15 = flag14;
                        bool flag16 = flag15;
                        if (flag16)
                        {
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
                        }
                    }
                    bool flag4 = num3 == 1;
                    if (flag4)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulRed.guid3);
                        Vector3 vector = arg2.transform.position;
                        bool flag9 = GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER;
                        bool flag10 = flag9;
                        bool flag11 = flag10;
                        bool flag12 = flag11;
                        if (flag12)
                        {
                            vector += new Vector3(1.125f, -0.3125f, 0f);
                        }
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
                        CompanionController orAddComponent = gameObject.GetOrAddComponent<CompanionController>();
                        this.companionsSpawned.Add(orAddComponent);
                        orAddComponent.Initialize(player);
                        bool flag13 = orAddComponent.specRigidbody;
                        bool flag14 = flag13;
                        bool flag15 = flag14;
                        bool flag16 = flag15;
                        if (flag16)
                        {
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
                        }
                    }
                    bool flag5 = num3 == 2;
                    if (flag5)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulYellow.guid4);
                        Vector3 vector = arg2.transform.position;
                        bool flag9 = GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER;
                        bool flag10 = flag9;
                        bool flag11 = flag10;
                        bool flag12 = flag11;
                        if (flag12)
                        {
                            vector += new Vector3(1.125f, -0.3125f, 0f);
                        }
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
                        CompanionController orAddComponent = gameObject.GetOrAddComponent<CompanionController>();
                        this.companionsSpawned.Add(orAddComponent);
                        orAddComponent.Initialize(player);
                        bool flag13 = orAddComponent.specRigidbody;
                        bool flag14 = flag13;
                        bool flag15 = flag14;
                        bool flag16 = flag15;
                        if (flag16)
                        {
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
                        }
                    }
                    bool flag6a = num3 == 3;
                    if (flag6a)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulPink.guid5);
                        Vector3 vector = arg2.transform.position;
                        bool flag9 = GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER;
                        bool flag10 = flag9;
                        bool flag11 = flag10;
                        bool flag12 = flag11;
                        if (flag12)
                        {
                            vector += new Vector3(1.125f, -0.3125f, 0f);
                        }
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
                        CompanionController orAddComponent = gameObject.GetOrAddComponent<CompanionController>();
                        this.companionsSpawned.Add(orAddComponent);
                        orAddComponent.Initialize(player);
                        bool flag13 = orAddComponent.specRigidbody;
                        bool flag14 = flag13;
                        bool flag15 = flag14;
                        bool flag16 = flag15;
                        if (flag16)
                        {
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
                        }
                    }
                }
            }
		}
        public GameObject Nuke;
        private List<CompanionController> companionsSpawned = new List<CompanionController>();
        private float random;
    }
}