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
using MultiplayerBasicExample;

namespace BunnyMod
{
    public class BulluonStone : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Bulluon Stone";

            string resourceName = "BunnyMod/Resources/BulluonStone";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<BulluonStone>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Guon Lover";
            string longDesc = "This poor mix of a bullet and guon stone empowers its bearer at the sight of a guon stone.\n\n" +
                "Because it is a cross-breed, it can't act as either a bullet or guon stone, so that's how it compensates.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
			item.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			item.quality = PickupObject.ItemQuality.C;
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
			bool flag = this.currentItems != this.lastItems;
			if (flag)
			{
				this.RemoveStat(PlayerStats.StatType.Damage);
				foreach (PassiveItem passiveItem in player.passiveItems)
				{
					bool flag2 = passiveItem.PickupObjectId == 260 || passiveItem.PickupObjectId == 262 || passiveItem.PickupObjectId == 263 || passiveItem.PickupObjectId == 264 || passiveItem.PickupObjectId == 466 || passiveItem.PickupObjectId == 269 || passiveItem.PickupObjectId == 270;
					if (flag2)
					{
						this.AddStat(PlayerStats.StatType.Damage, 0.15f, StatModifier.ModifyMethod.ADDITIVE);
					}
					bool flag3 = passiveItem.PickupObjectId == 565;
					if (flag3)
					{
						this.AddStat(PlayerStats.StatType.Damage, 0.05f, StatModifier.ModifyMethod.ADDITIVE);
					}
				}
				this.lastItems = this.currentItems;
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

		// Token: 0x0600051A RID: 1306 RVA: 0x0002C293 File Offset: 0x0002A493
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
		}

		private int currentItems;
		private int lastItems;
	}
}
