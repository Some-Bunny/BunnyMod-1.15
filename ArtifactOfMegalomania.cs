using System;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ArtifactOfMegalomania : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Megalomania";
            string resourceName = "BunnyMod/Resources/Artifacts/megalomania";
            GameObject obj = new GameObject(itemName);
            ArtifactOfMegalomania greandeParasite = obj.AddComponent<ArtifactOfMegalomania>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Power Trip.";
            string longDesc = "Every item/gun in your inventory will slightly increase curse.";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
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
					foreach (PassiveItem passiveItem in player.passiveItems)
					{
						this.AddStat(PlayerStats.StatType.Curse, .2f, StatModifier.ModifyMethod.ADDITIVE);
					}
					foreach (Gun gun in player.inventory.AllGuns)
					{
						this.AddStat(PlayerStats.StatType.Curse, .2f, StatModifier.ModifyMethod.ADDITIVE);
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


		public override void Pickup(PlayerController player)
		{
			this.CanBeDropped = false;
			base.Pickup(player);
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




