using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GungeonAPI;
using MonoMod.RuntimeDetour;
using UnityEngine;
using Dungeonator;

namespace BunnyMod
{
	// Token: 0x02000018 RID: 24
	public static class HookYeah
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public static void Init()
		{
			try
			{

				Hook hook = new Hook(typeof(Foyer).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic), typeof(BunnyModule).GetMethod("LateStart1"));
				Hook openchesthook = new Hook(typeof(Chest).GetMethod("Open", BindingFlags.Instance | BindingFlags.NonPublic), typeof(BunnysFoot).GetMethod("LootPlus"));
				//Hook curse = new Hook(typeof(Chest).GetMethod("Open", BindingFlags.Instance | BindingFlags.NonPublic), typeof(BunnyModule).GetMethod("LootCurse"));
				Hook a = new Hook(typeof(PlayerController).GetProperty("LocalShaderName", BindingFlags.Public | BindingFlags.Instance).GetGetMethod(),typeof(BunnyModule).GetMethod("LocalShaderNameGetHook"));
				//Hook ascrifice = new Hook(typeof(Chest).GetMethod("Open", BindingFlags.Instance | BindingFlags.NonPublic), typeof(ArtifactOfSacrifice).GetMethod("Lootless"));
				Hook ascrifice1 = new Hook(typeof(Chest).GetMethod("OnBroken", BindingFlags.Instance | BindingFlags.NonPublic), typeof(ArtifactOfSacrifice).GetMethod("DenyDrops"));
				Hook ascrifice2 = new Hook(typeof(Chest).GetMethod("DetermineContents", BindingFlags.Instance | BindingFlags.NonPublic), typeof(ArtifactOfSacrifice).GetMethod("DenyDropsMimic"));
				Hook LOTJ = new Hook(typeof(SuperReaperController).GetMethod("SpawnProjectiles", BindingFlags.Instance | BindingFlags.NonPublic), typeof(BunnyModule).GetMethod("SpawnProjectilesLOTJ"));
				Hook CurseRoomRewards = new Hook(typeof(RoomHandler).GetMethod("HandleRoomClearReward", BindingFlags.Instance | BindingFlags.Public), typeof(CurseRoomReward).GetMethod("CurseRoomRewardMethod"));
				Hook uhohgun = new Hook(typeof(GameManager).GetMethod("DelayedQuickRestart", BindingFlags.Instance | BindingFlags.Public), typeof(HookYeah).GetMethod("OnQuickRestart"));
				Hook pearls = new Hook(typeof(AIActor).GetMethod("PreDeath", BindingFlags.Instance | BindingFlags.NonPublic), typeof(CursedPearl).GetMethod("CursePearlDrops"));

			}
			catch (Exception e)
			{
				Tools.PrintException(e, "FF0000");
			}
		}
		public static void OnQuickRestart(Action<GameManager, float, QuickRestartOptions> orig, GameManager self, float duration, QuickRestartOptions options = default(QuickRestartOptions))
		{
			orig(self, duration, options);
			Bugun.ThisIsBasicallyCelsRNGUNButTakenToASillyLevel();
		}
	}
}

