using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dungeonator;
using MonoMod.RuntimeDetour;
using UnityEngine;

/*
namespace BunnyMod
{
	// Token: 0x02000084 RID: 132
	internal class AbyssChamberDungeon : SpecialDungeon
	{
		// Token: 0x06000340 RID: 832 RVA: 0x000228AC File Offset: 0x00020AAC
		public override Dungeon BuildDungeon(Dungeon dungeon)
		{
			dungeon.gameObject.name = "AbyssChamber";
			dungeon.LevelOverrideType = GameManager.LevelOverrideState.NONE;
			dungeon.contentSource = ContentSource.BASE;
			dungeon.DungeonShortName = "Abyss";
			dungeon.DungeonFloorName = "The Abyss";
			dungeon.DungeonFloorLevelTextOverride = "Gungeons Forgotten";
			BunnyModule.Strings.Core.Set("#BUNNY_DUNGEON_ABYSS", "\"Gungeons Forgotten\"");
			BunnyModule.Strings.Core.Set("#BUNNY_DUNGEON_ABYSS_FLOOR_TEXT", "The Abyss");
			BunnyModule.Strings.Core.Set("#BUNNY_DUNGEON_ABYSS_SHORT", "Abyss");
			dungeon.PatternSettings = new SemioticDungeonGenSettings
			{
				DEBUG_RENDER_CANVASES_SEPARATELY = dungeon.PatternSettings.DEBUG_RENDER_CANVASES_SEPARATELY,
				flows = new List<DungeonFlow>
				{
					AbyssDungeonFlow.BuildFlow()
				},
				mandatoryExtraRooms = dungeon.PatternSettings.mandatoryExtraRooms,
				MAX_GENERATION_ATTEMPTS = dungeon.PatternSettings.MAX_GENERATION_ATTEMPTS,
				optionalExtraRooms = dungeon.PatternSettings.optionalExtraRooms
			};
			dungeon.tileIndices.tilesetId = (GlobalDungeonData.ValidTilesets)694201337;
			return dungeon;
		}
		public override float BossDPSCap
		{
			get
			{
				return 125f;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000229CA File Offset: 0x00020BCA
		public override float DamageCap
		{
			get
			{
				return 125f;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000229D1 File Offset: 0x00020BD1
		public override float EnemyHealthMultiplier
		{
			get
			{
				return 1.7f;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000229D8 File Offset: 0x00020BD8
		public override List<DungeonFlowLevelEntry> FlowEntries
		{
			get
			{
				return base.FlowEntries;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000229E0 File Offset: 0x00020BE0
		public override string PrefabPath
		{
			get
			{
				return "Abyss_Chamber";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000229E7 File Offset: 0x00020BE7
		public override float PriceMultiplier
		{
			get
			{
				return 1.2f;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000229EE File Offset: 0x00020BEE
		public override string SceneName
		{
			get
			{
				return "bny_abyss_chamber";
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000229F5 File Offset: 0x00020BF5
		public override float SecretDoorHealthMultiplier
		{
			get
			{
				return 1.7f;
			}
		}
	}
}
*/