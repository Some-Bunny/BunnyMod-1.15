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
    public class Lacon1Scrap : PassiveItem
    {
        public static void Register()
        {
            string itemName = "LaCon Upgrade Scrap";
            string resourceName = "BunnyMod/Resources/dlaconscrap.png";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<Lacon1Scrap>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Plug & Play";
            string longDesc = "Special Modular upgrades used by LaCon weaponry. Consumed when holding a LaCon-brand weapon.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
            Lacon1Scrap.Scrap1ID = item.PickupObjectId;
        }
        public static int Scrap1ID;
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
