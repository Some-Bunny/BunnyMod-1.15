﻿using System;
using System.Collections.Generic;
using UnityEngine;
using ItemAPI;
using Dungeonator;


namespace BunnyMod
{
    public class LeadHand : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Lead Hand";
            string resourceName = "BunnyMod/Resources/theleadhand";
            GameObject obj = new GameObject(itemName);
            LeadHand leadhand = obj.AddComponent<LeadHand>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "The Reclaimer";
            string longDesc = "The Jammed are a gift from Kaliber, you are simply not attuned to be granted their gifts.\n\nMay this relic showcase some of the hidden gifts of the Jammed.";
            leadhand.SetupItem(shortDesc, longDesc, "bny");
            leadhand.quality = PickupObject.ItemQuality.B;
            leadhand.AddPassiveStatModifier(PlayerStats.StatType.Curse, 1.5f, StatModifier.ModifyMethod.ADDITIVE);
            leadhand.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            leadhand.Spawnquality = (PickupObject.ItemQuality)UnityEngine.Random.Range(1, 6);
            leadhand.target = LootEngine.GetItemOfTypeAndQuality<PassiveItem>(leadhand.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag1 = enemyHealth.aiActor && fatal;
                if (flag1)
                {
                    this.DropLoot(enemyHealth, fatal);
                }
            }
        }

        public void DropLoot(HealthHaver enemyHealth, bool fatal)
        {
            bool flag = enemyHealth.aiActor && enemyHealth.aiActor.IsBlackPhantom;
            if (flag)
            {
                this.random = UnityEngine.Random.Range(0.00f, 1.00f);
                if (random <= 0.166f)
                {
                    this.random = UnityEngine.Random.Range(0.00f, 1.00f);
                    if (random <= 0.99f)
                    {
                        int id = BraveUtility.RandomElement<int>(LeadHand.Lootdrops);
                        LootEngine.SpawnItem(PickupObjectDatabase.GetById(id).gameObject, enemyHealth.specRigidbody.UnitCenter, Vector2.zero, 0f, false, true, false);
                    }
                    else
                    {
                        this.Spawnquality = (PickupObject.ItemQuality)UnityEngine.Random.Range(1, 6);
                        this.target = LootEngine.GetItemOfTypeAndQuality<PassiveItem>(this.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
                        LootEngine.SpawnItem(this.target.gameObject, enemyHealth.specRigidbody.UnitCenter, Vector2.up, 1f, false, true, false);
                    }

                }
            }
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
        public bool IsBlackPhantom;
        public PickupObject.ItemQuality Spawnquality;
        public PassiveItem target;
        public static List<int> Lootdrops = new List<int>
        {
            73,
            85,
            120,
            67,
            224,
            600,
            78
        }; private float random;
    }
}



