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
    public class LunarGlassSyringe : PlayerItem
    {
        public float Random { get; private set; }
        private bool onCooldown;
        public static void Init()
        {
            string itemName = "Lunar Glass Syringe";
            string resourceName = "BunnyMod/Resources/lunarglasssyringe";
            GameObject obj = new GameObject(itemName);
            LunarGlassSyringe glassSyringe = obj.AddComponent<LunarGlassSyringe>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Filled with liquid time";
            string longDesc = "A syringe formed out of Lunar glass, a material that can bend time. Due to its time bending properties, it never runs out.\n\nInjecting yourself with it bends time around you, hopefully for the better.";
            glassSyringe.SetupItem(shortDesc, longDesc, "bny");
            glassSyringe.AddPassiveStatModifier(PlayerStats.StatType.Curse, 1.5f, StatModifier.ModifyMethod.ADDITIVE);
            glassSyringe.SetCooldownType(ItemBuilder.CooldownType.Timed, 15f);
            glassSyringe.consumable = false;
            glassSyringe.quality = PickupObject.ItemQuality.B;
            glassSyringe.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            glassSyringe.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            List<string> mandatoryConsoleIDs = new List<string>
            {
                "bny:lunar_glass_syringe"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "bloody_eye",
                "rolling_eye",
                "luxin_cannon",
                "cursed_bullets",
                "blue_guon_stone",
                "crestfaller"
            };
            CustomSynergies.Add("Cosmic Adrenaline", mandatoryConsoleIDs, optionalConsoleIDs, true);
            List<string> mandatoryConsoleIDs2 = new List<string>
            {
                "bny:lunar_glass_syringe",
                "holey_grail"
            };
            CustomSynergies.Add("Lunar Artifact", mandatoryConsoleIDs2, null, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.healthHaver.OnDamaged += new HealthHaver.OnDamagedEvent(this.Check4LunarSyngergy);
        }
        private void Check4LunarSyngergy(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
        {
            bool flag1 = base.LastOwner.HasPickupID(631);
            if (flag1 && !this.onCooldown)
            {
                this.onCooldown = true;
                ApplyStat(base.LastOwner, PlayerStats.StatType.Damage, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
                GameManager.Instance.StartCoroutine(StartCooldown());
            }
        }

        protected override void DoEffect(PlayerController user)
        {
            bool synergy = user.PlayerHasActiveSynergy("Cosmic Adrenaline");
            if (synergy)
            {
                AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
                ApplyStat(user, PlayerStats.StatType.Damage, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
                ApplyStat(user, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, 0.03f, StatModifier.ModifyMethod.ADDITIVE);
                ApplyStat(user, PlayerStats.StatType.ProjectileSpeed, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
                ApplyStat(user, PlayerStats.StatType.MovementSpeed, 0.175f, StatModifier.ModifyMethod.ADDITIVE);
                ApplyStat(user, PlayerStats.StatType.DamageToBosses, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
            }
            else
            {
                AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
                ApplyStat(user, PlayerStats.StatType.Damage, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
                ApplyStat(user, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, 0.025f, StatModifier.ModifyMethod.ADDITIVE);
                ApplyStat(user, PlayerStats.StatType.ProjectileSpeed, 0.025f, StatModifier.ModifyMethod.ADDITIVE);

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
        private IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(60f);
            this.onCooldown = false;
            yield break;
        }
    }
}
