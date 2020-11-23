using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Pathfinding;
using UnityEngine;
using Dungeonator;

using Gungeon;
using ItemAPI;





namespace GungeonAPI
{
	// Token: 0x0200003F RID: 63
	public static class ShrineOfTheLeadLord
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000128FC File Offset: 0x00010AFC
		public static void Add()
		{
			ShrineFactory shrineFactory = new ShrineFactory();
			{
				shrineFactory.name = "MountainBny Shrine";
				shrineFactory.modID = "bny";
				shrineFactory.text = "A shrine to the Gungeon Master. Voices of the Jammed whisper promises of power.";
				shrineFactory.spritePath = "BunnyMod/Resources/shrines/shrineoftheleadlord.png";
				shrineFactory.room = RoomFactory.BuildFromResource("BunnyMod/Resources/rooms/shrineofleadlordroom.room").room;
				shrineFactory.acceptText = "Accept their promises.";
				shrineFactory.declineText = "Leave them be.";
				shrineFactory.OnAccept = new Action<PlayerController, GameObject>(ShrineOfTheLeadLord.Accept);
				shrineFactory.OnDecline = null;
				shrineFactory.CanUse = new Func<PlayerController, GameObject, bool>(ShrineOfTheLeadLord.CanUse);
				shrineFactory.offset = new Vector3(-1f, -1f, 0f);
				shrineFactory.talkPointOffset = new Vector3(0f, 3f, 0f);
				shrineFactory.isToggle = false;
				shrineFactory.isBreachShrine = false;
			}
			GameObject gameObject = shrineFactory.Build();

		}


		// Token: 0x060001C3 RID: 451 RVA: 0x000129D8 File Offset: 0x00010BD8
		public static bool CanUse(PlayerController player, GameObject shrine)
		{
            {
				if (GameManager.HasInstance && GameManager.Instance.Dungeon != null && GameManager.Instance.Dungeon.data != null)
				{
					foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms)
					{
						if (room.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS && room.area.PrototypeRoomBossSubcategory == PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS && room.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null)
						{
							foreach (AIActor aiactor in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear))
							{
								if (aiactor != null && aiactor.healthHaver != null && aiactor.healthHaver.IsBoss && aiactor.healthHaver.IsAlive)
								{
									return shrine.GetComponent<ShrineFactory.CustomShrineController>().numUses == 0;
								}
							}
						}
					}
				}
				return false;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00012A0C File Offset: 0x00010C0C
		public static void Accept(PlayerController player, GameObject shrine)
		{
			int num3 = UnityEngine.Random.Range(0, 19);
			bool DTier = num3 == 0 | num3 == 1 | num3 == 2;
			if (DTier)
			{
				ShrineOfTheLeadLord.RnG = 1;
			}
			bool CTier = num3 == 3 | num3 == 4 | num3 == 5 | num3 == 6 | num3 == 7 | num3 == 8 | num3 == 9;
			if (CTier)
			{
				ShrineOfTheLeadLord.RnG = 2;
			}
			bool BTier = num3 == 10 | num3 == 11 | num3 == 12 | num3 == 13 | num3 == 14 | num3 == 15;
			if (BTier)
			{
				ShrineOfTheLeadLord.RnG = 3;
			}
			bool ATier = num3 == 16 | num3 == 17 | num3 == 18;
			if (ATier)
			{
				ShrineOfTheLeadLord.RnG = 4;
			}
			bool STier = num3 == 19;
			if (STier)
			{
				ShrineOfTheLeadLord.RnG = 5;
			}
			
			PlayerController primaryPlayer = GameManager.Instance.PrimaryPlayer;
			ShrineOfTheLeadLord.Spawnquality = (PickupObject.ItemQuality)(RnG);
			int num4 = UnityEngine.Random.Range(0, 2);
			bool DTGUN = num4 == 0;
			if (DTGUN)
			{
				ShrineOfTheLeadLord.target = LootEngine.GetItemOfTypeAndQuality<PickupObject>(ShrineOfTheLeadLord.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
				LootEngine.SpawnItem(ShrineOfTheLeadLord.target.gameObject, primaryPlayer.specRigidbody.UnitCenter, Vector2.left, 0f, false, true, false);
			}
			bool ietm = num4 == 1;
			if (ietm)
			{
				ShrineOfTheLeadLord.target1 = LootEngine.GetItemOfTypeAndQuality<PickupObject>(ShrineOfTheLeadLord.Spawnquality, GameManager.Instance.RewardManager.GunsLootTable, false);
				LootEngine.SpawnItem(ShrineOfTheLeadLord.target1.gameObject, primaryPlayer.specRigidbody.UnitCenter, Vector2.right, 0f, false, true, false);
			}
			string header = "The bargain has been accepted.";
			string text = "The Jammed are released.";
			player.PlayEffectOnActor(ResourceCache.Acquire("Global VFX/VFX_Curse") as GameObject, Vector3.zero, true, false, false);
			ShrineOfTheLeadLord.Notify(header, text);
			shrine.GetComponent<ShrineFactory.CustomShrineController>().numUses++;
			shrine.GetComponent<ShrineFactory.CustomShrineController>().GetRidOfMinimapIcon();
			AkSoundEngine.PostEvent("Play_ENM_darken_world_01", shrine);
			if (GameManager.HasInstance && GameManager.Instance.Dungeon != null && GameManager.Instance.Dungeon.data != null)
			{
				foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms)
				{
					if (room.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS && room.area.PrototypeRoomBossSubcategory == PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS && room.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null)
					{
						foreach (AIActor aiactor in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear))
						{
							//bool result;
							if (aiactor != null && aiactor.healthHaver != null && aiactor.healthHaver.IsBoss && aiactor.healthHaver.IsAlive && !aiactor.IsBlackPhantom)
							{
								aiactor.BecomeBlackPhantom();
							}
						}
					}
				}
			}
		}
		private static void Notify(string header, string text)
		{
			tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
			int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/death_mark");
			GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, encounterIconCollection, spriteIdByName, UINotificationController.NotificationColor.SILVER, false, false);
		}
		private static float RnG;
		public static PickupObject.ItemQuality Spawnquality;
		public static PickupObject target;
		public static PickupObject target1;
		//private static bool CanBUsed = false;
	}
}
