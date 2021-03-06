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
    public class MinigunRounds : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Minigun Clip";

            string resourceName = "BunnyMod/Resources/minigunclip";

            GameObject obj = new GameObject(itemName);

            MinigunRounds minigunrounds = obj.AddComponent<MinigunRounds>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Ridiculous Firepower";
            string longDesc = "These minigun clips were designed to hold a ridiculous amount of ammunition for whatever reason.\n\n" +
                "How they fit into literally every single gun is beyond solving. Not like you'll have time here to figure it out.";

            minigunrounds.SetupItem(shortDesc, longDesc, "bny");

            minigunrounds.AddPassiveStatModifier(PlayerStats.StatType.Damage, .4f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            minigunrounds.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, .75f, StatModifier.ModifyMethod.ADDITIVE);
            minigunrounds.AddPassiveStatModifier(PlayerStats.StatType.AdditionalClipCapacityMultiplier, 3, StatModifier.ModifyMethod.MULTIPLICATIVE);
            minigunrounds.AddPassiveStatModifier(PlayerStats.StatType.AmmoCapacityMultiplier, 2, StatModifier.ModifyMethod.ADDITIVE);
            minigunrounds.AddPassiveStatModifier(PlayerStats.StatType.Accuracy, 1.6f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            minigunrounds.AddPassiveStatModifier(PlayerStats.StatType.RateOfFire, 2.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            minigunrounds.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            minigunrounds.quality = PickupObject.ItemQuality.B;
        }
    }
}