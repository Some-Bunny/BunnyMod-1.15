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
    public class TheMonolith : PlayerItem
    {

        public float Random { get; private set; }
        public static void Init()
        {
            string itemName = "Monolith";
            string resourceName = "BunnyMod/Resources/TheMonolith";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<TheMonolith>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Eternal Nightmare";
            string longDesc = "Let history repeat itself infinitely.\n\nGrant the ability to bring previous skill again and again, as if you had always started with it.\n\nBut THEY will know.\n\nAnd THEY will hold back less, the more you bring.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 5f);
            item.consumable = false;
            item.quality = PickupObject.ItemQuality.S;
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }
        protected override void DoEffect(PlayerController user)
        {
            PickupObject var = Gungeon.Game.Items["bny:sigil_of_infinity"];
            LootEngine.GivePrefabToPlayer(var.gameObject, user);
            {
                GameManager.Instance.LoadCustomLevel("tt_castle");

            }

        }
    }
}

namespace BunnyMod
{
    public class LoopMarker : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Sigil of Infinity";
            string resourceName = "BunnyMod/Resources/loopmarker";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<LoopMarker>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Loop Marker";
            string longDesc = "Applies the weight of repeated history to you.\n\n" +
                "A marker of how many times someone has decided to retry history.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            item.quality = PickupObject.ItemQuality.SPECIAL;
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, 0.7f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, .3f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.GlobalPriceMultiplier, .2f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
    }
}

