using System;
using System.Collections.Generic;
using System.Linq;
using Dungeonator;

namespace GungeonAPI
{
	// Token: 0x02000008 RID: 8
	internal class OfficialFlows
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00003B50 File Offset: 0x00001D50
		public static int GetLevelIndex(string dungeonName)
		{
			for (int i = 0; i < OfficialFlows.dungeonPrefabNames.Length; i++)
			{
				bool flag = OfficialFlows.dungeonPrefabNames[i].ToLower().Contains(dungeonName.ToLower());
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003B9C File Offset: 0x00001D9C
		public static Dungeon GetDungeonPrefab(string floor)
		{
			return DungeonDatabase.GetOrLoadByName(floor);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public static Dungeon GetDungeonPrefab(int floor)
		{
			return DungeonDatabase.GetOrLoadByName(OfficialFlows.dungeonPrefabNames[floor]);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static List<PrototypeDungeonRoom> GetRoomsFromRoomTables(string floor)
		{
			Dungeon dungeon = OfficialFlows.GetDungeonPrefab(floor);
			List<PrototypeDungeonRoom> list = new List<PrototypeDungeonRoom>();
			for (int i = 0; i < dungeon.PatternSettings.flows.Count; i++)
			{
				foreach (WeightedRoom weightedRoom in dungeon.PatternSettings.flows[i].fallbackRoomTable.includedRooms.elements)
				{
					list.Add(weightedRoom.room);
				}
			}
			dungeon = null;
			return list;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003C88 File Offset: 0x00001E88
		public static List<PrototypeDungeonRoom> GetRoomsFromRoomTables(int floor)
		{
			return OfficialFlows.GetRoomsFromRoomTables(OfficialFlows.dungeonPrefabNames[floor]);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public static PrototypeDungeonRoom GetRoomFromDungeon(string roomName, string floor)
		{
			roomName = roomName.ToLower();
			List<PrototypeDungeonRoom> roomsFromRoomTables = OfficialFlows.GetRoomsFromRoomTables(floor);
			foreach (PrototypeDungeonRoom prototypeDungeonRoom in roomsFromRoomTables)
			{
				Tools.Log<string>(prototypeDungeonRoom.name, "roomnames.txt");
				bool flag = prototypeDungeonRoom.name.ToLower().Equals(roomName);
				if (flag)
				{
					return prototypeDungeonRoom;
				}
			}
			List<DungeonFlowNode> allFlowNodes = OfficialFlows.GetAllFlowNodes(floor);
			bool flag2 = allFlowNodes == null;
			PrototypeDungeonRoom result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				foreach (DungeonFlowNode dungeonFlowNode in allFlowNodes)
				{
					PrototypeDungeonRoom overrideExactRoom = dungeonFlowNode.overrideExactRoom;
					bool flag3 = overrideExactRoom != null;
					if (flag3)
					{
						Tools.Log<string>(overrideExactRoom.name, "roomnames.txt");
					}
					bool flag4 = overrideExactRoom != null && overrideExactRoom.name.ToLower().Equals(roomName);
					if (flag4)
					{
						return overrideExactRoom;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public static PrototypeDungeonRoom GetRoomFromDungeon(string roomName, int floor)
		{
			return OfficialFlows.GetRoomFromDungeon(roomName, OfficialFlows.dungeonPrefabNames[floor]);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003E08 File Offset: 0x00002008
		public static DungeonFlowNode GetNodeFromDungeon(string roomName, string floor)
		{
			roomName = roomName.ToLower();
			List<DungeonFlowNode> allFlowNodes = OfficialFlows.GetAllFlowNodes(floor);
			bool flag = allFlowNodes == null;
			DungeonFlowNode result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (DungeonFlowNode dungeonFlowNode in allFlowNodes)
				{
					PrototypeDungeonRoom overrideExactRoom = dungeonFlowNode.overrideExactRoom;
					bool flag2 = overrideExactRoom != null && overrideExactRoom.name.ToLower().Equals(roomName);
					if (flag2)
					{
						return dungeonFlowNode;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003EAC File Offset: 0x000020AC
		public static List<DungeonFlowNode> GetAllFlowNodes(string floor)
		{
			Dungeon dungeonPrefab = OfficialFlows.GetDungeonPrefab(floor);
			List<DungeonFlowNode> allNodes = dungeonPrefab.PatternSettings.flows[0].AllNodes;
			for (int i = 1; i < dungeonPrefab.PatternSettings.flows.Count; i++)
			{
				allNodes.Concat(dungeonPrefab.PatternSettings.flows[i].AllNodes);
			}
			return allNodes;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003F20 File Offset: 0x00002120
		public static List<DungeonFlowNode> GetAllFlowNodes(int floor)
		{
			return OfficialFlows.GetAllFlowNodes(OfficialFlows.dungeonPrefabNames[floor]);
		}

		// Token: 0x0400000D RID: 13
		public static string[] dungeonPrefabNames = new string[]
		{
			"Base_Castle",
			"Base_Gungeon",
			"Base_Mines",
			"Base_Catacombs",
			"Base_Forge",
			"Base_Sewer",
			"Base_Cathedral ",
			"Base_ResourcefulRat",
			"Base_Nakatomi",
			"Base_BulletHell"
		};

		// Token: 0x0400000E RID: 14
		public static string[] dungeonPrefabNamesInOrder = new string[]
		{
			"Foyer",
			"Base_Castle",
			"Base_Sewer",
			"Base_Gungeon",
			"Base_Cathedral",
			"Base_Mines",
			"Base_Catacombs",
			"Base_Forge",
			"Base_BulletHell"
		};

		// Token: 0x0400000F RID: 15
		public static string[] dungeonSceneNamesInOrder = new string[]
		{
			"tt_foyer",
			"tt_castle",
			"tt_sewer",
			"tt5",
			"tt_cathedral",
			"tt_mines",
			"tt_catacombs",
			"tt_forge",
			"tt_bullethell"
		};

		// Token: 0x0200012F RID: 303
		public enum FLOORS
		{
			// Token: 0x04000249 RID: 585
			KEEP,
			// Token: 0x0400024A RID: 586
			PROPER,
			// Token: 0x0400024B RID: 587
			MINES,
			// Token: 0x0400024C RID: 588
			HOLLOW,
			// Token: 0x0400024D RID: 589
			FORGE,
			// Token: 0x0400024E RID: 590
			HELL
		}
	}
}
