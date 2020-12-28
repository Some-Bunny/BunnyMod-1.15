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

namespace BunnyMod
{
    public class SpeckOfDust : PassiveItem
    {
        //private float random;

        public static void Init()
        {
            string itemName = "A Speck Of Dust";

            string resourceName = "BunnyMod/Resources/speckofdust";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<SpeckOfDust>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "How in the..?";
            string longDesc = "After severe scientific analysis of this pile of dust, you determine that it is the size of one speck.\n\n" +
                "Now that you've concentrated VERY hard on it, you feel slightly, very slightly better.";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");

            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalClipCapacityMultiplier, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AmmoCapacityMultiplier, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Accuracy, -.01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.RateOfFire, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DamageToBosses, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.KnockbackMultiplier, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.MovementSpeed, .07f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.ReloadSpeed, .01f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.RangeMultiplier, .01f, StatModifier.ModifyMethod.ADDITIVE);

            item.quality = PickupObject.ItemQuality.D;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.OnItemPurchased += this.Check4MoreDustShop;
            player.OnItemStolen += this.Check4MoreDustShop;
        }
        private void Check4MoreDustShop(PlayerController player, ShopItemController shop)
        {
            bool flag = this.doost == 0f;
            if (flag)
            {
                this.doost += 1f;
                PickupObject var = Gungeon.Game.Items["bny:a_speck_of_dust"];
                LootEngine.GivePrefabToPlayer(var.gameObject, player);
            }
        }

        private float doost;


        public override DebrisObject Drop(PlayerController player)
        {
            player.OnItemPurchased -= this.Check4MoreDustShop;
            player.OnItemStolen -= this.Check4MoreDustShop;
            return base.Drop(player);
        }
    }
}