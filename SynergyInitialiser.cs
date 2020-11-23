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
				"bny:chaos_revolver"
			};
			CustomSynergies.Add("Reunion", mandatoryConsoleIDs1, null, true);
		}
	}
}