﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ArtifactOfAvarice : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Avarice";
            string resourceName = "BunnyMod/Resources/Artifacts/avarice";
            GameObject obj = new GameObject(itemName);
            ArtifactOfAvarice greandeParasite = obj.AddComponent<ArtifactOfAvarice>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "MORE.";
            string longDesc = "Massively increased monetary gain and item drops. Lose ALL money and pickups every floor";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.AddPassiveStatModifier(PlayerStats.StatType.Coolness, 10f, StatModifier.ModifyMethod.ADDITIVE);
            greandeParasite.AddPassiveStatModifier(PlayerStats.StatType.MoneyMultiplierFromEnemies, 4f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            greandeParasite.AddPassiveStatModifier(PlayerStats.StatType.AdditionalBlanksPerFloor, -100f, StatModifier.ModifyMethod.ADDITIVE);
            greandeParasite.quality = PickupObject.ItemQuality.EXCLUDED;
        }
		protected override void Update()
		{
			bool flag = base.Owner;
			if (flag)
			{
				this.CalculateStats(base.Owner);
			}
		}

		private int currentItems;

		private int lastItems;

		private int currentGuns;

		private int lastGuns;

		// Token: 0x06000355 RID: 853 RVA: 0x0001F480 File Offset: 0x0001D680
		private void CalculateStats(PlayerController player)
		{
			this.currentItems = player.passiveItems.Count;
			this.currentGuns = player.inventory.AllGuns.Count;
			bool flag = this.currentItems != this.lastItems;
			bool flag2 = this.currentGuns != this.lastGuns;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				this.RemoveStat(PlayerStats.StatType.Curse);
				{
					bool flag5 = base.Owner.HasPickupID(Game.Items["bny:megalomania"].PickupObjectId);
					if (flag5)
					{
						this.AddStat(PlayerStats.StatType.Curse, -.2f, StatModifier.ModifyMethod.ADDITIVE);
					}
					this.lastItems = this.currentItems;
					this.lastGuns = this.currentGuns;
					player.stats.RecalculateStats(player, true, false);
				}

			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001F684 File Offset: 0x0001D884
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

		private void OnNewFloor()
        {
            PlayerController owner = base.Owner;
            this.AvariceCount = 0;
        }
        private void Avarice()
        {
            this.AvariceCount += 1f;
            bool flag = this.AvariceCount == 1f;
            if (flag)
            {
                PlayerController owner = base.Owner;
                owner.carriedConsumables.KeyBullets = 0;
                owner.carriedConsumables.Currency = 0;
                owner.Blanks = 0;
            }
        }

        public override void Pickup(PlayerController player)
        {
			this.AvariceCount = 1;
            player.OnEnteredCombat += (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.Avarice));
            GameManager.Instance.OnNewLevelFullyLoaded += this.OnNewFloor;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
        private float AvariceCount;
    }
}



