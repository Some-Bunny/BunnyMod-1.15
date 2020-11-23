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
    public class Microscope : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Micro Scope";
            string resourceName = "BunnyMod/Resources/microscope";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<Microscope>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Laymans terms";
            string longDesc = "An ineffective attachment made for the sole reason that it functions as a pun.\n\nAt least in the Gungeon it finds new life.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Accuracy, .80f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalShotPiercing, 1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.ProjectileSpeed, .2f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.D;
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:micro_scope",
            };
            List<string> optionalConsoleID1s = new List<string>
             {
                "scope",
                "laser_sight",
                "sniper_rifle",
                "awp"
            };
            CustomSynergies.Add("Macro Scope", mandatoryConsoleIDs1, optionalConsoleID1s, true);
        }
        protected override void Update()
        {
            bool flag = base.Owner;
            if (flag)
            {
                this.CalculateStats(base.Owner);
            }
        }
        private void CalculateStats(PlayerController player)
        {
            this.currentItems = player.passiveItems.Count;
            this.currentGuns = player.inventory.AllGuns.Count;
            bool flag = this.currentItems != this.lastItems;
            bool flag2 = this.currentGuns != this.lastGuns;
            bool flag3 = flag || flag2;
            if (flag3)
            {
                this.RemoveStat(PlayerStats.StatType.Damage);
                this.RemoveStat(PlayerStats.StatType.RateOfFire);
                bool aaa = base.Owner.PlayerHasActiveSynergy("Macro Scope");
                if (aaa)
                {
                    this.AddStat(PlayerStats.StatType.Damage, .2f, StatModifier.ModifyMethod.ADDITIVE);
                    this.AddStat(PlayerStats.StatType.RateOfFire, .25f, StatModifier.ModifyMethod.ADDITIVE);
                }
                this.lastItems = this.currentItems;
                this.lastGuns = this.currentGuns;
                player.stats.RecalculateStats(player, true, false);
            }
        }
        private void AddStat(PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod method = StatModifier.ModifyMethod.ADDITIVE)
        {
            StatModifier statModifier = new StatModifier
            {
                amount = amount,
                statToBoost = statType,
                modifyType = method
            };
            bool flag = this.passiveStatModifiers == null;
            if (flag)
            {
                this.passiveStatModifiers = new StatModifier[]
                {
                    statModifier
                };
            }
            else
            {
                this.passiveStatModifiers = this.passiveStatModifiers.Concat(new StatModifier[]
                {
                    statModifier
                }).ToArray<StatModifier>();
            }
        }
        private void RemoveStat(PlayerStats.StatType statType)
        {
            List<StatModifier> list = new List<StatModifier>();
            for (int i = 0; i < this.passiveStatModifiers.Length; i++)
            {
                bool flag = this.passiveStatModifiers[i].statToBoost != statType;
                if (flag)
                {
                    list.Add(this.passiveStatModifiers[i]);
                }
            }
            this.passiveStatModifiers = list.ToArray();
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
        private int currentItems;
        private int lastItems;
        private int currentGuns;
        private int lastGuns;
    }
}



