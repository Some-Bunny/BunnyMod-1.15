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
    public class FrequentFlyer : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Frequent Flyer";
            string resourceName = "BunnyMod/Resources/frequentflyer";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<FrequentFlyer>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "A kind gesture goes a long way.";
            string longDesc = "Shopkeepers always appreciate a kind face to talk to.\n\n" +
                "What shopkeepers appreciate even more is a kind face that also buys from them.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            item.quality = PickupObject.ItemQuality.C;
        }
        private void KindnessBoost(PlayerController player, ShopItemController shop)
        {
            bool flag = this.PurchaseCounter == 30f;
            if (!flag)
            {
                this.PurchaseCounter += 1f;
                ApplyStat(player, PlayerStats.StatType.GlobalPriceMultiplier, -0.01f, StatModifier.ModifyMethod.ADDITIVE);
            }
        }
        private void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod modifyMethod)
        {
            player.stats.RecalculateStats(player, false, false);
            StatModifier statModifier = new StatModifier()
            {
                statToBoost = statType,
                amount = amountToApply,
                modifyType = modifyMethod
            };
            player.ownerlessStatModifiers.Add(statModifier);
            player.stats.RecalculateStats(player, false, false);
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnItemPurchased += this.KindnessBoost;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnItemPurchased -= this.KindnessBoost;
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
        private float PurchaseCounter;

    }
}
