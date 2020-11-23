using System;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ZenithDesign : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Lunar Soul-Engine";
            string resourceName = "BunnyMod/Resources/zenithdesign.png";
            GameObject obj = new GameObject(itemName);
			ZenithDesign greandeParasite = obj.AddComponent<ZenithDesign>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Zenith Design";
            string longDesc = "A soul-powered engine designed by a lunar king of a planet with a closed time loop similar to Gunymedes. Markings along its smooth sides read 'Speed is War.'";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.AddPassiveStatModifier(PlayerStats.StatType.MovementSpeed, 1.05f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            greandeParasite.quality = PickupObject.ItemQuality.C;
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
			float num = 0f;
			this.CurrentSpeed = (player.stats.GetStatValue(PlayerStats.StatType.MovementSpeed));
			bool sped = this.CurrentSpeed > 7;
			if (sped)
            {
				num = (player.stats.GetStatValue(PlayerStats.StatType.MovementSpeed));
				this.RemoveStat(PlayerStats.StatType.Damage);
				{
					this.AddStat(PlayerStats.StatType.Damage, (num / 7) - 1, StatModifier.ModifyMethod.ADDITIVE);
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
            base.Pickup(player);
		}
		public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
		private float CurrentSpeed;

	}
}



