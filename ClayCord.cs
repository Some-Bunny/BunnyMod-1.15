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
using System.Xml.Serialization;

namespace BunnyMod
{
    public class Claycord : PassiveItem
    {
        public static void Init()
        {
            string itemName = "The Clay Cord";
            string resourceName = "BunnyMod/Resources/claycord";
            GameObject obj = new GameObject(itemName);
            Claycord clacord = obj.AddComponent<Claycord>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Hells Mould";
            string longDesc = "A lump of clay that moulds itself into the shape of its owner at somewhat morbid times.";
            clacord.SetupItem(shortDesc, longDesc, "bny");
            clacord.quality = PickupObject.ItemQuality.D;
            clacord.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            clacord.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
        }
        private void ResetCount()
        {
            this.StatueCount = false;
        }
        private List<CompanionController> companionsSpawned = new List<CompanionController>();
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag = enemyHealth.aiActor && fatal;
                if (flag)
                {
                    bool flag2 = StatueCount == false;
                    if (flag2)
                    {
                        this.StatueCount = true;
                        PlayerController player = base.Owner as PlayerController;
                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(ClayCordStatue.guidclay);
                        Vector3 vector = enemyHealth.transform.position;
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
        public override void Pickup(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            player.OnEnteredCombat += (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.ResetCount));
            base.Pickup(player);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            player.OnEnteredCombat -= (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.ResetCount));
            return base.Drop(player);
        }
        public bool StatueCount;
    }
}