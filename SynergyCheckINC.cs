using System;
using System.Linq;
/*
namespace BunnyMod
{
	// Token: 0x02000097 RID: 151
	public static class SynergyCheckerINC
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0001E88C File Offset: 0x0001CA8C
		public static bool PlayerHasActiveSynergy(this PlayerController player, string synergyNameToCheck)
		{
			foreach (int num in player.ActiveExtraSynergies)
			{
				AdvancedSynergyEntry advancedSynergyEntry = GameManager.Instance.SynergyManager.synergies[num];
				bool flag = advancedSynergyEntry.NameKey == synergyNameToCheck;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public static void AddPassiveStatModifier(this Gun gun, PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod modifyMethod)
		{
			gun.passiveStatModifiers = gun.passiveStatModifiers.Concat(new StatModifier[]
			{
				new StatModifier
				{
					statToBoost = statType,
					amount = amount,
					modifyType = modifyMethod
				}
			}).ToArray<StatModifier>();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001E948 File Offset: 0x0001CB48
		public static void AddCurrentGunStatModifier(this Gun gun, PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod modifyMethod)
		{
			gun.currentGunStatModifiers = gun.currentGunStatModifiers.Concat(new StatModifier[]
			{
				new StatModifier
				{
					statToBoost = statType,
					amount = amount,
					modifyType = modifyMethod
				}
			}).ToArray<StatModifier>();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001E984 File Offset: 0x0001CB84
		public static void AddCurrentGunDamageTypeModifier(this Gun gun, CoreDamageTypes damageTypes, float damageMultiplier)
		{
			gun.currentGunDamageTypeModifiers = gun.currentGunDamageTypeModifiers.Concat(new DamageTypeModifier[]
			{
				new DamageTypeModifier
				{
					damageType = damageTypes,
					damageMultiplier = damageMultiplier
				}
			}).ToArray<DamageTypeModifier>();
		}
	}
}

*/