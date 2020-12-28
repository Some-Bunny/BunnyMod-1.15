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
    public class AncientEnigma : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Ancient Enigma";
            string resourceName = "BunnyMod/Resources/ancientenigma";
            GameObject obj = new GameObject(itemName);
            AncientEnigma ancientEngima = obj.AddComponent<AncientEnigma>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "... And the abyss gazes back";
            string longDesc = "A perfectly contained chunk of reality. It's existence is permanent,as it has written itself into a constant.\n\nIt releases some of its concepts over time, altering you for the better or worse.";
            ancientEngima.SetupItem(shortDesc, longDesc, "bny");
            ancientEngima.quality = PickupObject.ItemQuality.SPECIAL;
        }
        private void Enigmatize(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
            ApplyStat(user, PlayerStats.StatType.Damage, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.MoneyMultiplierFromEnemies, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.ReloadSpeed, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.RateOfFire, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.DodgeRollDistanceMultiplier, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.DodgeRollSpeedMultiplier, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.DamageToBosses, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.AmmoCapacityMultiplier, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.AdditionalClipCapacityMultiplier, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.GlobalPriceMultiplier, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            ApplyStat(user, PlayerStats.StatType.PlayerBulletScale, UnityEngine.Random.Range(1.05f, 0.95f), StatModifier.ModifyMethod.MULTIPLICATIVE);
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
            AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
            this.CanBeDropped = false;
            bool pickedUp = this.m_pickedUp;
            if (!pickedUp)
            {
                base.Pickup(player);
                player.OnRoomClearEvent += this.Enigmatize;
            }
        }
    }
}
