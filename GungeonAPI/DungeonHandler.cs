using System;
using System.Collections.Generic;
using Dungeonator;

namespace GungeonAPI
{
	// Token: 0x02000003 RID: 3
	public static class DungeonHandler
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public static void Init()
		{
			bool flag = !DungeonHandler.initialized;
			bool flag2 = flag;
			if (flag2)
			{
				RoomFactory.LoadRoomsFromRoomDirectory();
				DungeonHooks.OnPreDungeonGeneration += DungeonHandler.OnPreDungeonGen;
				DungeonHandler.initialized = true;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public static void OnPreDungeonGen(LoopDungeonGenerator generator, Dungeon dungeon, DungeonFlow flow, int dungeonSeed)
		{
			Tools.Print<string>("Attempting to override floor layout...", "5599FF", false);
			bool flag = flow.name != "Foyer Flow" && !GameManager.IsReturningToFoyerWithPlayer;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = DungeonHandler.debugFlow;
				bool flag4 = flag3;
				if (flag4)
				{
					generator.AssignFlow(flow);
				}
				Tools.Print<string>("Dungeon name: " + dungeon.name, "FFFFFF", false);
				Tools.Print<string>("Override Flow set to: " + flow.name, "FFFFFF", false);
			}
			dungeon = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002D88 File Offset: 0x00000F88
		public static void Register(RoomFactory.RoomData roomData)
		{
			PrototypeDungeonRoom room = roomData.room;
			WeightedRoom w = new WeightedRoom
			{
				room = room,
				additionalPrerequisites = new DungeonPrerequisite[0],
				weight = ((roomData.weight == 0f) ? DungeonHandler.GlobalRoomWeight : roomData.weight)
			};
			switch (room.category)
			{
				case PrototypeDungeonRoom.RoomCategory.BOSS:
					return;
				case PrototypeDungeonRoom.RoomCategory.SPECIAL:
					{
						PrototypeDungeonRoom.RoomSpecialSubCategory subCategorySpecial = room.subCategorySpecial;
						PrototypeDungeonRoom.RoomSpecialSubCategory roomSpecialSubCategory = subCategorySpecial;
						bool flag = roomSpecialSubCategory != PrototypeDungeonRoom.RoomSpecialSubCategory.STANDARD_SHOP;
						if (flag)
						{
							bool flag2 = roomSpecialSubCategory != PrototypeDungeonRoom.RoomSpecialSubCategory.WEIRD_SHOP;
							if (flag2)
							{
								StaticReferences.RoomTables["special"].includedRooms.Add(w);
							}
							else
							{
								StaticReferences.subShopTable.InjectionData.Add(DungeonHandler.GetFlowModifier(roomData));
							}
						}
						else
						{
							StaticReferences.RoomTables["shop"].includedRooms.Add(w);
						}
						return;
					}
				case PrototypeDungeonRoom.RoomCategory.SECRET:
					StaticReferences.RoomTables["secret"].includedRooms.Add(w);
					return;
			}
			List<DungeonPrerequisite> list = new List<DungeonPrerequisite>();
			foreach (DungeonPrerequisite dungeonPrerequisite in room.prerequisites)
			{
				bool requireTileset = dungeonPrerequisite.requireTileset;
				bool flag3 = requireTileset;
				if (flag3)
				{
					StaticReferences.GetRoomTable(dungeonPrerequisite.requiredTileset).includedRooms.Add(w);
					list.Add(dungeonPrerequisite);
				}
			}
			foreach (DungeonPrerequisite item in list)
			{
				room.prerequisites.Remove(item);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002F78 File Offset: 0x00001178
		public static ProceduralFlowModifierData GetFlowModifier(RoomFactory.RoomData roomData)
		{
			return new ProceduralFlowModifierData
			{
				annotation = roomData.room.name,
				placementRules = new List<ProceduralFlowModifierData.FlowModifierPlacementType>
				{
					ProceduralFlowModifierData.FlowModifierPlacementType.END_OF_CHAIN,
					ProceduralFlowModifierData.FlowModifierPlacementType.HUB_ADJACENT_NO_LINK
				},
				exactRoom = roomData.room,
				selectionWeight = roomData.weight,
				chanceToSpawn = 1f,
				prerequisites = roomData.room.prerequisites.ToArray(),
				CanBeForcedSecret = true
			};
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002FFC File Offset: 0x000011FC
		public static bool BelongsOnThisFloor(RoomFactory.RoomData data, string dungeonName)
		{
			bool flag = data.floors == null || data.floors.Length == 0;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = false;
				foreach (string text in data.floors)
				{
					bool flag4 = text.ToLower().Equals(dungeonName.ToLower());
					bool flag5 = flag4;
					if (flag5)
					{
						flag3 = true;
						break;
					}
				}
				result = flag3;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00003080 File Offset: 0x00001280
		public static GenericRoomTable GetSpecialRoomTable()
		{
			foreach (MetaInjectionDataEntry metaInjectionDataEntry in GameManager.Instance.GlobalInjectionData.entries)
			{
				SharedInjectionData injectionData = metaInjectionDataEntry.injectionData;
				bool flag = ((injectionData != null) ? injectionData.InjectionData : null) != null;
				bool flag2 = flag;
				if (flag2)
				{
					foreach (ProceduralFlowModifierData proceduralFlowModifierData in metaInjectionDataEntry.injectionData.InjectionData)
					{
						bool flag3 = proceduralFlowModifierData.roomTable != null && proceduralFlowModifierData.roomTable.name.ToLower().Contains("basic special rooms");
						bool flag4 = flag3;
						if (flag4)
						{
							return proceduralFlowModifierData.roomTable;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003198 File Offset: 0x00001398
		public static void CollectDataForAnalysis(DungeonFlow flow, Dungeon dungeon)
		{
			try
			{
				foreach (WeightedRoom weightedRoom in flow.fallbackRoomTable.includedRooms.elements)
				{
					string str = "Fallback table: ";
					bool flag = weightedRoom == null;
					string str2;
					if (flag)
					{
						str2 = null;
					}
					else
					{
						PrototypeDungeonRoom room = weightedRoom.room;
						str2 = ((room != null) ? room.name : null);
					}
					Tools.Print<string>(str + str2, "FFFFFF", false);
				}
			}
			catch (Exception e)
			{
				Tools.PrintException(e, "FF0000");
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00003260 File Offset: 0x00001460
		public static void LogProtoRoomData(PrototypeDungeonRoom room)
		{
			int num = 0;
			Tools.LogPropertiesAndFields<PrototypeDungeonRoom>(room, "ROOM");
			foreach (PrototypePlacedObjectData prototypePlacedObjectData in room.placedObjects)
			{
				Tools.Log<string>(string.Format("\n----------------Object #{0}----------------", num++));
				Tools.LogPropertiesAndFields<PrototypePlacedObjectData>(prototypePlacedObjectData, "PLACED OBJECT");
				Tools.LogPropertiesAndFields<DungeonPlaceable>((prototypePlacedObjectData != null) ? prototypePlacedObjectData.placeableContents : null, "PLACEABLE CONTENT");
				bool flag = prototypePlacedObjectData == null;
				DungeonPlaceableVariant obj;
				if (flag)
				{
					obj = null;
				}
				else
				{
					DungeonPlaceable placeableContents = prototypePlacedObjectData.placeableContents;
					obj = ((placeableContents != null) ? placeableContents.variantTiers[0] : null);
				}
				Tools.LogPropertiesAndFields<DungeonPlaceableVariant>(obj, "VARIANT TIERS");
			}
			Tools.Print<string>("==LAYERS==", "FFFFFF", false);
			foreach (PrototypeRoomObjectLayer prototypeRoomObjectLayer in room.additionalObjectLayers)
			{
			}
		}

		// Token: 0x04000002 RID: 2
		public static float GlobalRoomWeight = 1f;

		// Token: 0x04000003 RID: 3
		private static bool initialized = false;

		// Token: 0x04000004 RID: 4
		public static bool debugFlow = false;
	}
}
