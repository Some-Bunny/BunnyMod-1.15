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
using SaveAPI;


namespace BunnyMod
{
    public class Infusion : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Infusion";
            string resourceName = "BunnyMod/Resources/infusion.png";
            GameObject obj = new GameObject(itemName);
            Infusion lizardBlood = obj.AddComponent<Infusion>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Health Leech";
            string longDesc = "A vial of assorted blood types.\n\nMix enough into yourself and you may find yourself healthier.\n\nYes, I studied Biology in school, how can you tell?";
            lizardBlood.SetupItem(shortDesc, longDesc, "bny");
            lizardBlood.quality = PickupObject.ItemQuality.A;
            lizardBlood.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
			lizardBlood.SetupUnlockOnCustomFlag(CustomDungeonFlags.INFUSION_GOOPTON_FLAG, true);
			lizardBlood.AddItemToGooptonMetaShop(20, null);
		}
		private void KillCount(PlayerController player)
		{
			this.Killed += 1f;
			bool flag = this.Killed == 100f;
			if (flag)
			{
				bool CAP = this.HPUP == 3f;
				if(!CAP)
                {
					this.Killed = 0f;
					this.HPUP += 1f;
					this.RemoveStat(PlayerStats.StatType.Health);
					{
						this.AddStat(PlayerStats.StatType.Health, (float)HPUP, StatModifier.ModifyMethod.ADDITIVE);
						base.Owner.stats.RecalculateStats(base.Owner, true, false);
					}
				}
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
			foreach (StatModifier statModifier2 in this.passiveStatModifiers)
			{
				bool flag = statModifier2.statToBoost == statType;
				bool flag2 = flag;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.passiveStatModifiers == null;
			bool flag4 = flag3;
			if (flag4)
			{
				this.passiveStatModifiers = new StatModifier[]
				{
					statModifier
				};
				return;
			}
			this.passiveStatModifiers = this.passiveStatModifiers.Concat(new StatModifier[]
			{
				statModifier
			}).ToArray<StatModifier>();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00010DBC File Offset: 0x0000EFBC
		private void RemoveStat(PlayerStats.StatType statType)
		{
			List<StatModifier> list = new List<StatModifier>();
			for (int i = 0; i < this.passiveStatModifiers.Length; i++)
			{
				bool flag = this.passiveStatModifiers[i].statToBoost != statType;
				bool flag2 = flag;
				if (flag2)
				{
					list.Add(this.passiveStatModifiers[i]);
				}
			}
			this.passiveStatModifiers = list.ToArray();
		}
		public override void Pickup(PlayerController player)
		{
			player.OnKilledEnemy += this.KillCount;
			base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
        public override DebrisObject Drop(PlayerController player)
		{
			player.OnKilledEnemy += this.KillCount;
			Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);

        }
		private float Killed;
		private float HPUP;

	}
}
