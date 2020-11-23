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
    public class LizardBloodTransfusion : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Reptile Blood Transfusion";
            string resourceName = "BunnyMod/Resources/liazrdbloodtransfusion";
            GameObject obj = new GameObject(itemName);
            LizardBloodTransfusion lizardBlood = obj.AddComponent<LizardBloodTransfusion>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Works scarily well";
            string longDesc = "A bag of reptile blood that your body just sort of happily accepted.\n\n" +
                "Like expected, you feel a lot cooler, cause you're part reptile now.";
            lizardBlood.SetupItem(shortDesc, longDesc, "bny");
            lizardBlood.AddPassiveStatModifier(PlayerStats.StatType.Coolness, 3f, StatModifier.ModifyMethod.ADDITIVE);
            lizardBlood.quality = PickupObject.ItemQuality.C;
            lizardBlood.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            float num2 = player.stats.GetBaseStatValue(PlayerStats.StatType.Health);
            num2 -= 1f;
            player.stats.SetBaseStatValue(PlayerStats.StatType.Health, num2, player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            float num3 = player.stats.GetBaseStatValue(PlayerStats.StatType.Health);
            num3 += 1f;
            player.stats.SetBaseStatValue(PlayerStats.StatType.Health, num3, player);
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);

        }
    }
}
