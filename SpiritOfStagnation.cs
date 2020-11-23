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
    public class SpiritOfStagnation : PlayerItem
    {

        public static void Init()
        {
            string itemName = "Spirit Of Stagnation";
            string resourceName = "BunnyMod/Resources/spiritofstagnation";
            GameObject obj = new GameObject(itemName);
            SpiritOfStagnation whisper = obj.AddComponent<SpiritOfStagnation>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Slowly...";
            string longDesc = "A spirit of stagnation. It's bite cools down reality around it, making it stagnate, slower and cooler. Let it bite you with caution.";
            whisper.SetupItem(shortDesc, longDesc, "bny");
            whisper.SetCooldownType(ItemBuilder.CooldownType.Timed, 5f);
            whisper.consumable = false;
            whisper.quality = PickupObject.ItemQuality.D;
            whisper.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
            ApplyStat(user, PlayerStats.StatType.MovementSpeed, 0.95f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.Coolness, 0.5f, StatModifier.ModifyMethod.ADDITIVE);
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
    }
}
