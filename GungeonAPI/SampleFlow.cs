using System;
using System.Collections.Generic;
using System.Linq;
using Dungeonator;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x0200000B RID: 11
	public static class SampleFlow
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000055AC File Offset: 0x000037AC
		public static DungeonFlow CreateDebugFlow(Dungeon dungeon)
		{
			DungeonFlow dungeonFlow = SampleFlow.CreateEntranceExitFlow(dungeon);
			dungeonFlow.name = "debug_flow";
			DungeonFlowNode dungeonFlowNode = new DungeonFlowNode(dungeonFlow)
			{
				overrideExactRoom = RoomFactory.CreateEmptyRoom(12, 12)
			};
			DungeonFlowNode parent = dungeonFlowNode;
			dungeonFlow.AddNodeToFlow(dungeonFlowNode, dungeonFlow.FirstNode);
			foreach (RoomFactory.RoomData roomData in RoomFactory.rooms.Values)
			{
				string str = "Adding room to flow: ";
				PrototypeDungeonRoom room = roomData.room;
				Tools.Log<string>(str + ((room != null) ? room.ToString() : null));
				DungeonFlowNode dungeonFlowNode2 = new DungeonFlowNode(dungeonFlow)
				{
					overrideExactRoom = roomData.room
				};
				dungeonFlow.AddNodeToFlow(dungeonFlowNode2, parent);
				dungeonFlowNode = new DungeonFlowNode(dungeonFlow)
				{
					overrideExactRoom = RoomFactory.CreateEmptyRoom(12, 12)
				};
				dungeonFlow.AddNodeToFlow(dungeonFlowNode, dungeonFlowNode2);
				parent = dungeonFlowNode;
			}
			dungeon = null;
			return dungeonFlow;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000056A8 File Offset: 0x000038A8
		public static DungeonFlow CreateRoomTypeSampleFlow(Dungeon dungeon)
		{
			DungeonFlow dungeonFlow = SampleFlow.CreateNewFlow(dungeon);
			dungeonFlow.name = "type_sample_flow";
			DungeonFlowNode dungeonFlowNode = SampleFlow.NodeFromAssetName(dungeonFlow, "elevator entrance");
			dungeonFlow.FirstNode = dungeonFlowNode;
			dungeonFlow.AddNodeToFlow(dungeonFlowNode, null);
			DungeonFlowNode parent = dungeonFlow.FirstNode;
			DungeonMaterial[] roomMaterialDefinitions = dungeon.roomMaterialDefinitions;
			Tools.Print<int?>((roomMaterialDefinitions != null) ? new int?(roomMaterialDefinitions.Length) : null, "FFFFFF", false);
			for (int i = 0; i < dungeon.roomMaterialDefinitions.Length; i++)
			{
				bool flag = dungeon.name == OfficialFlows.dungeonPrefabNames[3] && i == 5;
				if (!flag)
				{
					PrototypeDungeonRoom prototypeDungeonRoom = RoomFactory.CreateEmptyRoom(14, 14);
					prototypeDungeonRoom.overrideRoomVisualType = i;
					DungeonFlowNode dungeonFlowNode2 = new DungeonFlowNode(dungeonFlow)
					{
						overrideExactRoom = prototypeDungeonRoom
					};
					dungeonFlow.AddNodeToFlow(dungeonFlowNode2, parent);
					parent = dungeonFlowNode2;
				}
			}
			dungeonFlow.AddNodeToFlow(SampleFlow.NodeFromAssetName(dungeonFlow, "exit_room_basic"), parent);
			dungeon = null;
			return dungeonFlow;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000057A8 File Offset: 0x000039A8
		public static DungeonFlow CreateEntranceExitFlow(Dungeon dungeon)
		{
			DungeonFlow dungeonFlow = SampleFlow.CreateNewFlow(dungeon);
			dungeonFlow.name = "entrance_exit_flow";
			DungeonFlowNode dungeonFlowNode = SampleFlow.NodeFromAssetName(dungeonFlow, "elevator entrance");
			dungeonFlow.FirstNode = dungeonFlowNode;
			dungeonFlow.AddNodeToFlow(dungeonFlowNode, null);
			dungeonFlow.AddNodeToFlow(SampleFlow.NodeFromAssetName(dungeonFlow, "exit_room_basic"), dungeonFlowNode);
			dungeon = null;
			return dungeonFlow;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00005804 File Offset: 0x00003A04
		public static DungeonFlow CreateMazeFlow(Dungeon dungeon)
		{
			DungeonFlow dungeonFlow = SampleFlow.CreateNewFlow(dungeon);
			dungeonFlow.name = "maze_flow";
			DungeonFlowNode dungeonFlowNode = SampleFlow.NodeFromAssetName(dungeonFlow, "elevator entrance");
			dungeonFlow.FirstNode = dungeonFlowNode;
			dungeonFlow.AddNodeToFlow(dungeonFlowNode, null);
			DungeonFlowNode dungeonFlowNode2 = new DungeonFlowNode(dungeonFlow)
			{
				overrideExactRoom = RoomFactory.BuildFromResource("resource/rooms/maze.room").room
			};
			dungeonFlow.AddNodeToFlow(dungeonFlowNode2, dungeonFlowNode);
			dungeonFlow.AddNodeToFlow(SampleFlow.NodeFromAssetName(dungeonFlow, "exit_room_basic"), dungeonFlowNode2);
			dungeon = null;
			return dungeonFlow;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005884 File Offset: 0x00003A84
		public static DungeonFlow CreateNewFlow(Dungeon dungeon)
		{
			DungeonFlow dungeonFlow = ScriptableObject.CreateInstance<DungeonFlow>();
			dungeonFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>
			{
				new DungeonFlowSubtypeRestriction()
			};
			dungeonFlow.flowInjectionData = new List<ProceduralFlowModifierData>();
			dungeonFlow.sharedInjectionData = new List<SharedInjectionData>();
			GenericRoomTable fallbackRoomTable = dungeon.PatternSettings.flows[0].fallbackRoomTable;
			dungeonFlow.fallbackRoomTable = fallbackRoomTable;
			dungeonFlow.evolvedRoomTable = fallbackRoomTable;
			dungeonFlow.phantomRoomTable = fallbackRoomTable;
			dungeonFlow.Initialize();
			dungeon = null;
			return dungeonFlow;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005900 File Offset: 0x00003B00
		public static DungeonFlowNode NodeFromAssetName(DungeonFlow flow, string name)
		{
			DungeonFlowNode dungeonFlowNode = new DungeonFlowNode(flow);
			PrototypeDungeonRoom prototypeDungeonRoom = SampleFlow.RoomFromAssetName(name);
			bool flag = prototypeDungeonRoom == null;
			if (flag)
			{
				Tools.Print<string>("Error loading room " + name, "FF0000", false);
			}
			dungeonFlowNode.overrideExactRoom = prototypeDungeonRoom;
			return dungeonFlowNode;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005954 File Offset: 0x00003B54
		public static PrototypeDungeonRoom RoomFromAssetName(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			PrototypeDungeonRoom result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string assetName = name;
				bool flag2 = name.Contains('/');
				if (flag2)
				{
					assetName = name.Substring(name.LastIndexOf('/') + 1).Replace(".asset", "").Trim();
				}
				PrototypeDungeonRoom asset = StaticReferences.GetAsset<PrototypeDungeonRoom>(assetName);
				result = asset;
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000059B4 File Offset: 0x00003BB4
		public static void ListNodes(this DungeonFlow flow)
		{
			Tools.Print<string>(flow.name + " node:", "FFFFFF", false);
			Tools.Print<string>("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", "FFFFFF", false);
			foreach (DungeonFlowNode dungeonFlowNode in flow.AllNodes)
			{
				bool flag = dungeonFlowNode != null && dungeonFlowNode.overrideExactRoom;
				if (flag)
				{
					Tools.Print<PrototypeDungeonRoom>(dungeonFlowNode.overrideExactRoom, "FFFFFF", false);
				}
			}
		}
	}
}
