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
using MultiplayerBasicExample;

//this is basically Frequent Flyer but damage what the fuck am i doing with my life
namespace BunnyMod
{
    public class SimpBullets : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Simp Bullets";
            string resourceName = "BunnyMod/Resources/simpbullets";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<SimpBullets>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Tier 3 Lich Sub";
            string longDesc = "These bullets will happily drop hundreds of casings just to feel full of less empty inside.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            item.quality = PickupObject.ItemQuality.B;
        }
        private void SIMPIN(PlayerController player, ShopItemController shop)
        {
            {
                ApplyStat(player, PlayerStats.StatType.Damage, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
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
            player.OnItemPurchased += this.SIMPIN;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}
