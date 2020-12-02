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

namespace BunnyMod
{
    public class TableTechSoul : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Table Tech Soul";

            string resourceName = "BunnyMod/Resources/tabletechsoul.png";

            GameObject obj = new GameObject(itemName);

            TableTechSoul minigunrounds = obj.AddComponent<TableTechSoul>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Spiritual Flips";
            string longDesc = "This ancient technique allows the user to push souls from out of tables.\n\n Chapter 11 of the Table Sutra. One who imbues spirit into their flip shall be granted an audience by the flipped spirit.";

            minigunrounds.SetupItem(shortDesc, longDesc, "bny");
            minigunrounds.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            minigunrounds.quality = PickupObject.ItemQuality.A;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnTableFlipped = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipped, new Action<FlippableCover>(this.HandleFlip));
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
        private void HandleFlip(FlippableCover table)
        {
            //AkSoundEngine.PostEvent("Play_OBJ_ammo_pickup_01", base.gameObject);
            this.CreateNewCompanion(base.Owner);
        }
        private void CreateNewCompanion(PlayerController player)
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
                int num3 = UnityEngine.Random.Range(0, 4);
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

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnTableFlipped = (Action<FlippableCover>)Delegate.Remove(player.OnTableFlipped, new Action<FlippableCover>(this.HandleFlip));
            return base.Drop(player);
        }
        private List<CompanionController> companionsSpawned = new List<CompanionController>();
        private float random;
    }
}