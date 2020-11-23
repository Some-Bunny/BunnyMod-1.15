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
    public class EmpoweringCore : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Empowering Core";
            string resourceName = "BunnyMod/Resources/empoweringcore.png";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<EmpoweringCore>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Powe Eterna";
            string longDesc = "A pure energy core. No flaws, no diminishing returns. Just pure eternal power.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, 1.40f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            item.quality = PickupObject.ItemQuality.S;
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}



