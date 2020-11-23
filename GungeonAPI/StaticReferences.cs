using System;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x02000011 RID: 17
	public static class StaticReferences
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00006768 File Offset: 0x00004968
		public static void Init()
		{
			StaticReferences.AssetBundles = new Dictionary<string, AssetBundle>();
			foreach (string text in StaticReferences.assetBundleNames)
			{
				try
				{
					AssetBundle assetBundle = ResourceManager.LoadAssetBundle(text);
					StaticReferences.AssetBundles.Add(text, ResourceManager.LoadAssetBundle(text));
				}
				catch (Exception e)
				{
					Tools.PrintError<string>("Failed to load asset bundle: " + text, "FF0000");
					Tools.PrintException(e, "FF0000");
				}
			}
			StaticReferences.RoomTables = new Dictionary<string, GenericRoomTable>();
			foreach (KeyValuePair<string, string> keyValuePair in StaticReferences.roomTableMap)
			{
				try
				{
					GenericRoomTable genericRoomTable = StaticReferences.GetAsset<GenericRoomTable>(keyValuePair.Value);
					bool flag = genericRoomTable == null;
					bool flag2 = flag;
					if (flag2)
					{
						genericRoomTable = DungeonDatabase.GetOrLoadByName("base_" + keyValuePair.Key).PatternSettings.flows[0].fallbackRoomTable;
					}
					StaticReferences.RoomTables.Add(keyValuePair.Key, genericRoomTable);
				}
				catch (Exception e2)
				{
					Tools.PrintError<string>("Failed to load room table: " + keyValuePair.Key + ":" + keyValuePair.Value, "FF0000");
					Tools.PrintException(e2, "FF0000");
				}
			}
			Tools.Print<string>("Static references initialized.", "FFFFFF", false);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006900 File Offset: 0x00004B00
		public static GenericRoomTable GetRoomTable(GlobalDungeonData.ValidTilesets tileset)
		{
			bool flag = tileset <= GlobalDungeonData.ValidTilesets.MINEGEON;
			if (flag)
			{
				switch (tileset)
				{
					case GlobalDungeonData.ValidTilesets.GUNGEON:
						return StaticReferences.RoomTables["gungeon"];
					case GlobalDungeonData.ValidTilesets.CASTLEGEON:
						return StaticReferences.RoomTables["castle"];
					case GlobalDungeonData.ValidTilesets.GUNGEON | GlobalDungeonData.ValidTilesets.CASTLEGEON:
						break;
					case GlobalDungeonData.ValidTilesets.SEWERGEON:
						return StaticReferences.RoomTables["sewer"];
					default:
						{
							bool flag2 = tileset == GlobalDungeonData.ValidTilesets.CATHEDRALGEON;
							if (flag2)
							{
								return StaticReferences.RoomTables["cathedral"];
							}
							bool flag3 = tileset == GlobalDungeonData.ValidTilesets.MINEGEON;
							if (flag3)
							{
								return StaticReferences.RoomTables["mines"];
							}
							break;
						}
				}
			}
			else
			{
				bool flag4 = tileset == GlobalDungeonData.ValidTilesets.CATACOMBGEON;
				if (flag4)
				{
					return StaticReferences.RoomTables["catacombs"];
				}
				bool flag5 = tileset == GlobalDungeonData.ValidTilesets.FORGEGEON;
				if (flag5)
				{
					return StaticReferences.RoomTables["forge"];
				}
				bool flag6 = tileset == GlobalDungeonData.ValidTilesets.HELLGEON;
				if (flag6)
				{
					return StaticReferences.RoomTables["bullethell"];
				}
			}
			return StaticReferences.RoomTables["gungeon"];
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006A34 File Offset: 0x00004C34
		public static T GetAsset<T>(string assetName) where T : UnityEngine.Object
		{
			T t = default(T);
			foreach (AssetBundle assetBundle in StaticReferences.AssetBundles.Values)
			{
				t = assetBundle.LoadAsset<T>(assetName);
				bool flag = t != null;
				bool flag2 = flag;
				if (flag2)
				{
					break;
				}
			}
			return t;
		}

		// Token: 0x0400003D RID: 61
		public static Dictionary<string, AssetBundle> AssetBundles;

		// Token: 0x0400003E RID: 62
		public static Dictionary<string, GenericRoomTable> RoomTables;

		// Token: 0x0400003F RID: 63
		public static SharedInjectionData subShopTable;

		// Token: 0x04000040 RID: 64
		public static Dictionary<string, string> roomTableMap = new Dictionary<string, string>
		{
			{
				"special",
				"basic special rooms (shrines, etc)"
			},
			{
				"shop",
				"Shop Room Table"
			},
			{
				"secret",
				"secret_room_table_01"
			},
			{
				"gungeon",
				"Gungeon_RoomTable"
			},
			{
				"castle",
				"Castle_RoomTable"
			},
			{
				"mines",
				"Mines_RoomTable"
			},
			{
				"catacombs",
				"Catacomb_RoomTable"
			},
			{
				"forge",
				"Forge_RoomTable"
			},
			{
				"sewer",
				"Sewer_RoomTable"
			},
			{
				"cathedral",
				"Cathedral_RoomTable"
			},
			{
				"bullethell",
				"BulletHell_RoomTable"
			}
		};

		// Token: 0x04000041 RID: 65
		public static string[] assetBundleNames = new string[]
		{
			"shared_auto_001",
			"shared_auto_002"
		};

		// Token: 0x04000042 RID: 66
		public static string[] dungeonPrefabNames = new string[]
		{
			"base_mines",
			"base_catacombs",
			"base_forge",
			"base_sewer",
			"base_cathedral",
			"base_bullethell"
		};
	}
}
