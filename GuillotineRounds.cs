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
    public class GuillotineRounds : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Guillotine Rounds";

            string resourceName = "BunnyMod/Resources/guillotinebullets";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<GuillotineRounds>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Overthrow!";
            string longDesc = "These special bullets were designed specifically to help overthrow any ruling body.\n\n" +
                "Unluckily for the smaller kin, gun size means everything here.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, .025f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DamageToBosses, .25f, StatModifier.ModifyMethod.ADDITIVE);
            item.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            item.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            item.quality = PickupObject.ItemQuality.C;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}