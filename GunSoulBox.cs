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

    public class GunSoulBox : PassiveItem
    {
        public static void Init()
        {
            string name = "Gun Soul Phylactery";
            string resourcePath = "BunnyMod/Resources/gunsoulphylactery.png";
            GameObject gameObject = new GameObject();
            GunSoulBox GunSoulBox = gameObject.AddComponent<GunSoulBox>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject, true);
            string shortDesc = "Little whispers...";
            string longDesc = "A container containing various gun souls. They come out to play once it gets too packed with others.";
            GunSoulBox.SetupItem(shortDesc, longDesc, "bny");
            GunSoulBox.quality = PickupObject.ItemQuality.B;

        }
        private float random;


        // Token: 0x06000060 RID: 96 RVA: 0x0000534D File Offset: 0x0000354D
        public override void Pickup(PlayerController player)
        {
            player.OnKilledEnemy += this.OnKill;
            base.Pickup(player);
        }
        private void OnKill(PlayerController player)
        {
            int num3 = UnityEngine.Random.Range(0, 6);
            bool flag3 = num3 == 5;
            if (flag3)
            {
                this.CreateNewCompanion(base.Owner);
            }
        }
        private List<CompanionController> companionsSpawned = new List<CompanionController>();

        private void CreateNewCompanion(PlayerController player)
        {

            {
                this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                if (random <= 0.33f)
                {
                    AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulBlue.guid1);
                    Vector3 vector = player.transform.position;
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
                    int num3 = UnityEngine.Random.Range(0, 5);
                    bool flag3 = num3 == 0;
                    if (flag3)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulGreen.guid2);
                        Vector3 vector = player.transform.position;
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
                        Vector3 vector = player.transform.position;
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
                        Vector3 vector = player.transform.position;
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
                    bool flag6 = num3 == 4;
                    if (flag6)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_doormimic_flame_01", base.gameObject);
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(GunSoulPurple.guid6);
                        Vector3 vector = player.transform.position;
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
                        Vector3 vector = player.transform.position;
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
    }
}



