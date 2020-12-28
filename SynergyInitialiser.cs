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
	// Token: 0x02000079 RID: 121
	internal class InitialiseSynergies
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00019418 File Offset: 0x00017618
		public static void DoInitialisation()
		{
			List<string> mandatoryConsoleIDs1 = new List<string>
			{
				"bny:chaos_chamber",
				"bny:chaos_revolver",
				"bny:chaos_trigger",
				"bny:chaos_hammer"
			};
			CustomSynergies.Add("Reunion", mandatoryConsoleIDs1, null, true);
			List<string> mandatoryConsoleIDs2 = new List<string>
			{
				"bny:death",
				"bny:taxes"
			};
			CustomSynergies.Add("Death & Taxes", mandatoryConsoleIDs2, null, true);
			List<string> mandatoryConsoleIDs3 = new List<string>
			{
				"bny:gunthemimic",
				"gunther"
			};
			CustomSynergies.Add("Imposter Syndrome", mandatoryConsoleIDs3, null, true);
			List<string> mandatoryConsoleIDs4 = new List<string>
			{
				"bny:mimikey47",
				"akey47"
			};
			CustomSynergies.Add("One Locks, Other Unlocks", mandatoryConsoleIDs4, null, true);
			List<string> mandatoryConsoleIDs5 = new List<string>
			{
				"bny:casemimic",
				"casey"
			};
			CustomSynergies.Add("Suspicion On #ff0000", mandatoryConsoleIDs5, null, true);
			List<string> mandatoryConsoleIDs6 = new List<string>
			{
				"bny:blasphemimic",
				"blasphemy"
			};
			CustomSynergies.Add("Double-Edged Sword", mandatoryConsoleIDs6, null, true);
			List<string> mandatoryConsoleIDs7 = new List<string>
			{
				"bny:chambemimic_gun",
				"chamber_gun"
			};
			CustomSynergies.Add("Russian Roulette", mandatoryConsoleIDs7, null, true);
		}
	}
}