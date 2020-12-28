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
using SaveAPI;


namespace BunnyMod
{
    public class AncientWhisper : PlayerItem
    {
        private float random;

        public static void Init()
        {
            string itemName = "Ancient Whisper";
            string resourceName = "BunnyMod/Resources/ancientwhisper";
            GameObject obj = new GameObject(itemName);
            AncientWhisper whisper = obj.AddComponent<AncientWhisper>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Gaze into the abyss...";
            string longDesc = "A scroll stained with the concepts of the universe. Simply gazing at it can permanantly affect you, positively or negatively.\n\nIf you dare to glimpse at it, your best, and only resort is to cast it away from yourself, run away and pray to a higher deity that you're fine.";
            whisper.SetupItem(shortDesc, longDesc, "bny");
            whisper.SetCooldownType(ItemBuilder.CooldownType.Timed, 5f);
            whisper.consumable = true;
            whisper.quality = PickupObject.ItemQuality.C;
            whisper.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            //whisper.SetupUnlockOnStat(TrackedStats.TIMES_CLEARED_FORGE, DungeonPrerequisite.PrerequisiteOperation.GREATER_THAN, 14);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.95f)
            {
                AkSoundEngine.PostEvent("Play_VO_lichA_chuckle_01", base.gameObject);
                AkSoundEngine.PostEvent("Play_WPN_Life_Orb_Blast_01", base.gameObject);
                AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
                ApplyStat(user, PlayerStats.StatType.Damage, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.MoneyMultiplierFromEnemies, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.ReloadSpeed, UnityEngine.Random.Range(1.4f, 0.5f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.RateOfFire, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.DodgeRollDistanceMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.DodgeRollSpeedMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.DamageToBosses, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.AmmoCapacityMultiplier, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.AdditionalClipCapacityMultiplier, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.GlobalPriceMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(user, PlayerStats.StatType.PlayerBulletScale, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
            }
            else
            {
                PickupObject var = Gungeon.Game.Items["bny:ancient_enigma"];
                LootEngine.GivePrefabToPlayer(var.gameObject, user);
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
    }
}
