using System;
using System.Collections.Generic;
using Dungeonator;
using GungeonAPI;
using UnityEngine;
using Gungeon;
using ItemAPI;

namespace BunnyMod
{
	// Token: 0x02000085 RID: 133
	internal class AbyssDungeonFlow
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00022A08 File Offset: 0x00020C08
		public static DungeonFlow BuildFlow()
		{
			DungeonFlow flow = ScriptableObject.CreateInstance<DungeonFlow>();
			flow.name = "AbyssDungeonFlow";
			flow.fallbackRoomTable = null;
			flow.phantomRoomTable = null;
			flow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);
			flow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
			flow.sharedInjectionData = new List<SharedInjectionData>
			{
				GungeonAPI.Tools.shared_auto_002.LoadAsset<SharedInjectionData>("Base Shared Injection Data")
			};
			//string entrance = BraveUtility.RandomElement<string>(AbyssDungeonFlow.EntranceRooms);
			DungeonFlowNode node = GungeonAPI.Tools.GenerateFlowNode(flow, PrototypeDungeonRoom.RoomCategory.ENTRANCE, RoomFactory.BuildFromResource(BraveUtility.RandomElement<string>(AbyssDungeonFlow.EntranceRooms)).room, null, false, false, true, 1f, DungeonFlowNode.NodePriority.MANDATORY, "");
			DungeonFlowNode boss = GungeonAPI.Tools.GenerateFlowNode(flow, PrototypeDungeonRoom.RoomCategory.BOSS, RoomFactory.BuildFromResource(BraveUtility.RandomElement<string>(AbyssDungeonFlow.BossRooms)).room, null, false, false, true, 1f, DungeonFlowNode.NodePriority.MANDATORY, "");
			DungeonFlowNode bossEntranceRoom = SampleFlow.NodeFromAssetName(flow, "boss foyer");
			DungeonFlowNode node2 = GungeonAPI.Tools.GenerateFlowNode(flow, PrototypeDungeonRoom.RoomCategory.EXIT, RoomFactory.BuildFromResource("BunnyMod/Resources/rooms/TheAbyss/AbyssExit.room").room, null, false, false, true, 1f, DungeonFlowNode.NodePriority.MANDATORY, "");
			flow.Initialize();
			flow.AddNodeToFlow(node, null);
			flow.AddNodeToFlow(bossEntranceRoom, node);
			flow.AddNodeToFlow(boss, bossEntranceRoom);
			flow.AddNodeToFlow(SampleFlow.NodeFromAssetName(flow, "exit_room_basic"), boss);

			//flow.AddNodeToFlow(node2, boss);
			//GiantAbyssHole.Add();
			flow.FirstNode = node;
			return flow;

		}
		public static string[] EntranceRooms = new string[]
		{
			"BunnyMod/Resources/rooms/TheAbyss/AbyssEntrance.room",
			"BunnyMod/Resources/rooms/TheAbyss/AbyssEntrance2.room",
			"BunnyMod/Resources/rooms/TheAbyss/AbyssEntrance3.room"
		};
		public static string[] BossRooms = new string[]
		{
			"BunnyMod/Resources/rooms/TheAbyss/AbyssBossRoom.room",
			"BunnyMod/Resources/rooms/TheAbyss/AbyssBossRoom2.room",
			"BunnyMod/Resources/rooms/TheAbyss/AbyssBossRoom3.room"
		};
		//public static List<string> EntranceRooms;
	}
}
