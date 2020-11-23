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
	// Token: 0x02000105 RID: 261
	public class MasteryReplacementRNG : DungeonDatabase
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x000367F8 File Offset: 0x000349F8
		public static void InitDungeonHook()
		{
			Hook hook = new Hook(typeof(DungeonDatabase).GetMethod("GetOrLoadByName", BindingFlags.Static | BindingFlags.Public), typeof(MasteryReplacementRNG).GetMethod("GetOrLoadByNameHook", BindingFlags.Static | BindingFlags.Public));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00036838 File Offset: 0x00034A38
		public static Dungeon GetOrLoadByNameHook(Func<string, Dungeon> orig, string name)
		{
			Dungeon dungeon = null;
			bool flag = name.ToLower() == "base_nakatomi";
			if (flag)
			{
				dungeon = MasteryReplacementRNG.RNGDungeonMods(MasteryReplacementRNG.GetOrLoadByName_Orig(name));
			}
			bool flag3 = dungeon;
			Dungeon result;
			if (flag3)
			{
				DebugTime.RecordStartTime();
				DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[]
				{
					name
				});
				result = dungeon;
			}
			else
			{
				result = orig(name);
			}
			return result;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000368C8 File Offset: 0x00034AC8
		public static Dungeon GetOrLoadByName_Orig(string name)
		{
			AssetBundle assetBundle = ResourceManager.LoadAssetBundle("dungeons/" + name.ToLower());
			DebugTime.RecordStartTime();
			Dungeon component = assetBundle.LoadAsset<GameObject>(name).GetComponent<Dungeon>();
			DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[]
			{
				name
			});
			return component;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0003691C File Offset: 0x00034B1C
		public static Dungeon RNGDungeonMods(Dungeon dungeon)
		{
			dungeon.BossMasteryTokenItemId = TestItemBNY.TestItem1ID;
			return dungeon;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0003693C File Offset: 0x00034B3C
	}
}
