using ItemAPI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BunnyMod
{
    public class AstralCounterweight : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Astral Counterweight";
            string resourceName = "BunnyMod/Resources/astralcounterweight.png";
            GameObject obj = new GameObject(itemName);
			AstralCounterweight greandeParasite = obj.AddComponent<AstralCounterweight>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Equillibrium";
            string longDesc = "Those unaffected by the great leveller shall now be raised in kind to hold their own.";
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
			float dmg = (player.stats.GetStatValue(PlayerStats.StatType.Damage));
			float dmgbos = (player.stats.GetStatValue(PlayerStats.StatType.DamageToBosses));
			float amocap = (player.stats.GetStatValue(PlayerStats.StatType.AmmoCapacityMultiplier));
			float firerate = (player.stats.GetStatValue(PlayerStats.StatType.RateOfFire));
			float konk = (player.stats.GetStatValue(PlayerStats.StatType.KnockbackMultiplier));
			float equillibrium = (dmg + dmgbos + amocap + firerate + konk) / 5;
			bool nirvana = this.Equillibrium != equillibrium;
			if (nirvana)
            {
				this.Equillibrium = equillibrium;
				this.RemoveStat(PlayerStats.StatType.Damage);
				this.RemoveStat(PlayerStats.StatType.DamageToBosses);
				this.RemoveStat(PlayerStats.StatType.AmmoCapacityMultiplier);
				this.RemoveStat(PlayerStats.StatType.RateOfFire);
				this.RemoveStat(PlayerStats.StatType.KnockbackMultiplier);
				{

					this.AddStat(PlayerStats.StatType.Damage, ((this.CurrentDamage * equillibrium) - dmg), StatModifier.ModifyMethod.ADDITIVE);
					this.AddStat(PlayerStats.StatType.DamageToBosses, ((this.CurrentBossDamage * equillibrium) - dmgbos), StatModifier.ModifyMethod.ADDITIVE);
					this.AddStat(PlayerStats.StatType.AmmoCapacityMultiplier, ((this.CurrentAmmoCap * equillibrium) - amocap), StatModifier.ModifyMethod.ADDITIVE);
					this.AddStat(PlayerStats.StatType.RateOfFire, ((this.CurrentFireRate * equillibrium) - firerate), StatModifier.ModifyMethod.ADDITIVE);
					this.AddStat(PlayerStats.StatType.RateOfFire, ((this.CurrentKnockBack* equillibrium) - konk), StatModifier.ModifyMethod.ADDITIVE);
					dmg = (player.stats.GetStatValue(PlayerStats.StatType.Damage));
					dmgbos = (player.stats.GetStatValue(PlayerStats.StatType.DamageToBosses));
					amocap = (player.stats.GetStatValue(PlayerStats.StatType.AmmoCapacityMultiplier));
					firerate = (player.stats.GetStatValue(PlayerStats.StatType.RateOfFire));
					firerate = (player.stats.GetStatValue(PlayerStats.StatType.KnockbackMultiplier));
					equillibrium = (dmg + dmgbos + amocap + firerate + konk) / 5;
					player.stats.RecalculateStats(player, false, false);
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
			bool flag = !this.m_pickedUpThisRun;
			bool flag2 = flag;
			if (flag2)
			{
				this.CurrentDamage = player.stats.GetStatValue(PlayerStats.StatType.Damage);
				this.CurrentBossDamage = player.stats.GetStatValue(PlayerStats.StatType.DamageToBosses);
				this.CurrentAmmoCap = player.stats.GetStatValue(PlayerStats.StatType.AmmoCapacityMultiplier);
				this.CurrentFireRate = player.stats.GetStatValue(PlayerStats.StatType.RateOfFire);
				this.CurrentKnockBack = player.stats.GetStatValue(PlayerStats.StatType.KnockbackMultiplier);
				this.Equillibrium = (this.CurrentDamage + this.CurrentBossDamage + this.CurrentAmmoCap + this.CurrentFireRate + CurrentKnockBack) / 5;

			}
			base.Pickup(player);
		}
		public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
		private float CurrentDamage;
		private float CurrentBossDamage;
		private float CurrentAmmoCap;
		private float Equillibrium;
		private float CurrentFireRate;
		private float CurrentKnockBack;


	}
}



