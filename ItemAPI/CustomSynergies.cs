using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gungeon;
using MonoMod.RuntimeDetour;

namespace ItemAPI
{
	// Token: 0x02000019 RID: 25
	public static class CustomSynergies
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00009464 File Offset: 0x00007664
		public static AdvancedSynergyEntry Add(string name, List<string> mandatoryConsoleIDs, List<string> optionalConsoleIDs = null, bool ignoreLichEyeBullets = true)
		{
			bool flag = mandatoryConsoleIDs == null || mandatoryConsoleIDs.Count == 0;
			AdvancedSynergyEntry result;
			if (flag)
			{
				ETGModConsole.Log("Synergy " + name + " has no mandatory items/guns.", false);
				result = null;
			}
			else
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				List<int> list3 = new List<int>();
				List<int> list4 = new List<int>();
				foreach (string text in mandatoryConsoleIDs)
				{
					PickupObject pickupObject = Game.Items[text];
					bool flag2 = pickupObject && pickupObject.GetComponent<Gun>();
					if (flag2)
					{
						list2.Add(pickupObject.PickupObjectId);
					}
					else
					{
						bool flag3 = pickupObject && (pickupObject.GetComponent<PlayerItem>() || pickupObject.GetComponent<PassiveItem>());
						if (flag3)
						{
							list.Add(pickupObject.PickupObjectId);
						}
					}
				}
				bool flag4 = optionalConsoleIDs != null;
				if (flag4)
				{
					foreach (string text2 in optionalConsoleIDs)
					{
						PickupObject pickupObject = Game.Items[text2];
						bool flag5 = pickupObject && pickupObject.GetComponent<Gun>();
						if (flag5)
						{
							list4.Add(pickupObject.PickupObjectId);
						}
						else
						{
							bool flag6 = pickupObject && (pickupObject.GetComponent<PlayerItem>() || pickupObject.GetComponent<PassiveItem>());
							if (flag6)
							{
								list3.Add(pickupObject.PickupObjectId);
							}
						}
					}
				}
				AdvancedSynergyEntry advancedSynergyEntry = new AdvancedSynergyEntry
				{
					NameKey = name,
					MandatoryItemIDs = list,
					MandatoryGunIDs = list2,
					OptionalItemIDs = list3,
					OptionalGunIDs = list4,
					bonusSynergies = new List<CustomSynergyType>(),
					statModifiers = new List<StatModifier>()
				};
				CustomSynergies.Add(advancedSynergyEntry);
				result = advancedSynergyEntry;
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009698 File Offset: 0x00007898
		public static void Add(AdvancedSynergyEntry synergyEntry)
		{
			AdvancedSynergyEntry[] second = new AdvancedSynergyEntry[]
			{
				synergyEntry
			};
			GameManager.Instance.SynergyManager.synergies = GameManager.Instance.SynergyManager.synergies.Concat(second).ToArray<AdvancedSynergyEntry>();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000096DC File Offset: 0x000078DC
		public static string SynergyStringHook(Func<string, int, string> orig, string key, int index = -1)
		{
			string text = orig(key, index);
			bool flag = string.IsNullOrEmpty(text);
			bool flag2 = flag;
			if (flag2)
			{
				text = key;
			}
			return text;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009708 File Offset: 0x00007908
		public static bool HasMTGConsoleID(this PlayerController player, string consoleID)
		{
			bool flag = !Game.Items.ContainsID(consoleID);
			return !flag && player.HasPickupID(Game.Items[consoleID].PickupObjectId);
		}

		// Token: 0x04000059 RID: 89
		public static Hook synergyHook = new Hook(typeof(StringTableManager).GetMethod("GetSynergyString", BindingFlags.Static | BindingFlags.Public), typeof(CustomSynergies).GetMethod("SynergyStringHook"));
	}
}
