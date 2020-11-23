using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dungeonator;
using MonoMod.RuntimeDetour;
using UnityEngine;

namespace BunnyMod
{
	// Token: 0x02000097 RID: 151
	internal abstract class SpecialDungeon
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00026BB0 File Offset: 0x00024DB0
		public static void Init()
		{
			Hook hook = new Hook(typeof(DungeonDatabase).GetMethod("GetOrLoadByName", BindingFlags.Static | BindingFlags.Public), typeof(SpecialDungeon).GetMethod("GetOrLoadByNameHook"));
			foreach (Type type in from myType in Assembly.GetAssembly(typeof(SpecialDungeon)).GetTypes()
								  where myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(SpecialDungeon))
								  select myType)
			{
				SpecialDungeon dungeon = (SpecialDungeon)Activator.CreateInstance(type);
				SpecialDungeon.RegisterCustomDungeon(new Func<Dungeon, Dungeon>(dungeon.BuildDungeon), dungeon.PrefabPath);
				GameLevelDefinition def = new GameLevelDefinition
				{
					bossDpsCap = dungeon.BossDPSCap,
					dungeonPrefabPath = dungeon.PrefabPath,
					damageCap = dungeon.DamageCap,
					dungeonSceneName = dungeon.SceneName,
					enemyHealthMultiplier = dungeon.EnemyHealthMultiplier,
					flowEntries = dungeon.FlowEntries,
					priceMultiplier = dungeon.PriceMultiplier,
					secretDoorHealthMultiplier = dungeon.SecretDoorHealthMultiplier
				};
				GameManager.Instance.customFloors.Add(def);
				SpecialDungeon.addedLevelDefs.Add(def);
			}
			ETGModMainBehaviour.Instance.gameObject.AddComponent<DetectMissingDefinitions>();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00026D20 File Offset: 0x00024F20
		public static Dungeon GetOrLoadByNameHook(Func<string, Dungeon> orig, string prefabPath)
		{
			Func<Dungeon, Dungeon> buildDungeon;
			bool flag = SpecialDungeon.customDungeons.TryGetValue(prefabPath, out buildDungeon);
			Dungeon result;
			if (flag)
			{
				Dungeon d = buildDungeon(SpecialDungeon.GetOrLoadByNameOrig("Base_ResourcefulRat"));
				DebugTime.RecordStartTime();
				DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[]
				{
					prefabPath
				});
				result = d;
			}
			else
			{
				result = orig(prefabPath);
			}
			return result;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00026D7C File Offset: 0x00024F7C
		public static Dungeon GetOrLoadByNameOrig(string name)
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

		// Token: 0x060003BE RID: 958 RVA: 0x00026DCD File Offset: 0x00024FCD
		public static void RegisterCustomDungeon(Func<Dungeon, Dungeon> buildDungeon, string prefabPath)
		{
			SpecialDungeon.customDungeons.Add(prefabPath, buildDungeon);
		}

		// Token: 0x060003BF RID: 959
		public abstract Dungeon BuildDungeon(Dungeon orig);

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060003C0 RID: 960
		public abstract string PrefabPath { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060003C1 RID: 961
		public abstract string SceneName { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060003C2 RID: 962
		public abstract float BossDPSCap { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060003C3 RID: 963
		public abstract float DamageCap { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060003C4 RID: 964
		public abstract float EnemyHealthMultiplier { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060003C5 RID: 965
		public abstract float PriceMultiplier { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060003C6 RID: 966
		public abstract float SecretDoorHealthMultiplier { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public virtual List<DungeonFlowLevelEntry> FlowEntries
		{
			get
			{
				return new List<DungeonFlowLevelEntry>(0);
			}
		}

		// Token: 0x040001A8 RID: 424
		public static Dictionary<string, Func<Dungeon, Dungeon>> customDungeons = new Dictionary<string, Func<Dungeon, Dungeon>>();

		// Token: 0x040001A9 RID: 425
		public static List<GameLevelDefinition> addedLevelDefs = new List<GameLevelDefinition>();
	}
}
namespace BunnyMod
{
	// Token: 0x02000098 RID: 152
	public class DetectMissingDefinitions : MonoBehaviour
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00026E18 File Offset: 0x00025018
		public void Update()
		{
			bool hasInstance = GameManager.HasInstance;
			if (hasInstance)
			{
				foreach (GameLevelDefinition def in SpecialDungeon.addedLevelDefs)
				{
					bool flag = !GameManager.Instance.customFloors.Contains(def);
					if (flag)
					{
						GameManager.Instance.customFloors.Add(def);
					}
				}
			}
		}
	}
}