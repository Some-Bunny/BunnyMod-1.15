using System;
using System.Diagnostics;
using System.Reflection;
using Dungeonator;
using MonoMod.RuntimeDetour;

namespace GungeonAPI
{
	// Token: 0x02000012 RID: 18
	public static class DungeonHooks
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007F RID: 127 RVA: 0x00005700 File Offset: 0x00003900
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x00005734 File Offset: 0x00003934
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<LoopDungeonGenerator, Dungeon, DungeonFlow, int> OnPreDungeonGeneration;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000081 RID: 129 RVA: 0x00005768 File Offset: 0x00003968
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x0000579C File Offset: 0x0000399C
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnPostDungeonGeneration;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000083 RID: 131 RVA: 0x000057D0 File Offset: 0x000039D0
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x00005804 File Offset: 0x00003A04
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action OnFoyerAwake;

		// Token: 0x06000085 RID: 133 RVA: 0x00005838 File Offset: 0x00003A38
		public static void FoyerAwake(Action<MainMenuFoyerController> orig, MainMenuFoyerController self)
		{
			orig(self);
			Action onFoyerAwake = DungeonHooks.OnFoyerAwake;
			bool flag = onFoyerAwake != null;
			if (flag)
			{
				onFoyerAwake();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005868 File Offset: 0x00003A68
		public static void LoopGenConstructor(Action<LoopDungeonGenerator, Dungeon, int> orig, LoopDungeonGenerator self, Dungeon dungeon, int dungeonSeed)
		{
			Tools.Print<string>("-Loop Gen Called-", "5599FF", false);
			orig(self, dungeon, dungeonSeed);
			bool flag = GameManager.Instance != null && GameManager.Instance != DungeonHooks.targetInstance;
			bool flag2 = flag;
			if (flag2)
			{
				DungeonHooks.targetInstance = GameManager.Instance;
				DungeonHooks.targetInstance.OnNewLevelFullyLoaded += DungeonHooks.OnLevelLoad;
			}
			DungeonFlow arg = (DungeonFlow)DungeonHooks.m_assignedFlow.GetValue(self);
			Action<LoopDungeonGenerator, Dungeon, DungeonFlow, int> onPreDungeonGeneration = DungeonHooks.OnPreDungeonGeneration;
			bool flag3 = onPreDungeonGeneration != null;
			if (flag3)
			{
				onPreDungeonGeneration(self, dungeon, arg, dungeonSeed);
			}
			dungeon = null;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000590C File Offset: 0x00003B0C
		public static void OnLevelLoad()
		{
			Tools.Print<string>("-Post Gen Called-", "5599FF", false);
			Action onPostDungeonGeneration = DungeonHooks.OnPostDungeonGeneration;
			bool flag = onPostDungeonGeneration != null;
			if (flag)
			{
				onPostDungeonGeneration();
			}
		}

		// Token: 0x04000016 RID: 22
		private static GameManager targetInstance;

		// Token: 0x04000017 RID: 23
		public static FieldInfo m_assignedFlow = typeof(LoopDungeonGenerator).GetField("m_assignedFlow", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000018 RID: 24
		private static Hook preDungeonGenHook = new Hook(typeof(LoopDungeonGenerator).GetConstructor(new Type[]
		{
			typeof(Dungeon),
			typeof(int)
		}), typeof(DungeonHooks).GetMethod("LoopGenConstructor"));

		// Token: 0x04000019 RID: 25
		private static Hook foyerAwakeHook = new Hook(typeof(MainMenuFoyerController).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic), typeof(DungeonHooks).GetMethod("FoyerAwake"));
	}
}
