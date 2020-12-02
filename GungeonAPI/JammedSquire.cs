using System;
using System.Collections.Generic;
using Gungeon;
using GungeonAPI;
using UnityEngine;
using Dungeonator;
using System.Linq;
using System.Reflection;
using MonoMod.RuntimeDetour;

namespace BunnyMod
{
	// Token: 0x0200000E RID: 14
	public static class JammedSquire
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000480C File Offset: 0x00002A0C
		public static void Add()
		{
			ShrineFactory SQUIRE = new ShrineFactory
			{
				name = "Squire of the Jammed",
				modID = "BunnyMod",
				spritePath = "BunnyMod/Resources/JammedSquire/Idle/squire_idle_001.png",
				shadowSpritePath = "BunnyMod/Resources/JammedSquire/Idle/squire_idle_001.png",
				acceptText = "I accept His offer.",
				declineText = "I refuse.",
				OnAccept = new Action<PlayerController, GameObject>(JammedSquire.Accept),
				OnDecline = null,
				CanUse = new Func<PlayerController, GameObject, bool>(JammedSquire.CanUse),
				offset = new Vector3(48.375f, 50.8f, 51.3f),
				talkPointOffset = new Vector3(0f, 3f, 0f),
				isToggle = false,
				isBreachShrine = true,
				interactableComponent = typeof(JammedSquireInteractable)
			};
			GameObject gameObject = SQUIRE.Build();
			gameObject.AddAnimation("idle", "BunnyMod/Resources/JammedSquire/Idle/", 4, NPCBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk", "BunnyMod/Resources/JammedSquire/Talk/", 4, NPCBuilder.AnimationType.Talk, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk_start", "BunnyMod/Resources/JammedSquire/TalkStart/", 4, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("do_effect", "BunnyMod/Resources/JammedSquire/DoEffect/", 7, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			JammedSquireInteractable component1 = gameObject.GetComponent<JammedSquireInteractable>();
			component1.conversation = new List<string>
			{
				"...",
				"My Lord has always been curious of your kind...",
				"Trying to manipulate things out of your control...",
				"Messing with things you shouldn't...",
				"Obtaining treasure that overpowers you...",
				"My Lord is... interested...",
				"Interested in you sheer stubborness for power...",
				"So, He sent me here to for one thing...",
				"To give you power, strength, riches...",
				"At the cost of Him not holding back...",
				"His army... And Himself...",
				"What do you say, Gungeoneer...?"

			};
			component1.conversationB = new List<string>
			{
				"What do you ask of Him...?"
			};
			component1.declineTextB = "I ask nothing more.";
			component1.acceptTextB = "Remove His offer from me.";
			//gameObject.SetActive(false);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0003ACFC File Offset: 0x00038EFC

		private static bool CanUse(PlayerController player, GameObject npc)
		{
			return true;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00040650 File Offset: 0x0003E850
		public static void Accept(PlayerController player, GameObject npc)
		{
			npc.GetComponent<tk2dSpriteAnimator>().PlayForDuration("do_effect", 2f, "idle", false);
			bool harderlotj = JammedSquire.NoHarderLotJ;
			if (harderlotj)
			{
				//npc.GetComponent<tk2dSpriteAnimator>().PlayForDuration("do_effect", -2f, "idle", false);
				string header2 = "Curse 2.0 Enabled";
				string text2 = "The Jammed show true potential.";
				JammedSquire.Notify(header2, text2);
				JammedSquire.NoHarderLotJ = false;
				player.PlayEffectOnActor(ResourceCache.Acquire("Global VFX/VFX_Curse") as GameObject, Vector3.zero, true, false, false);
			}
			else
			{
				//npc.GetComponent<tk2dSpriteAnimator>().PlayForDuration("do_effect", -2f, "idle", false);
				string header = "Curse 2.0 Disabled";
				string text = "The Jammed hold back.";
				JammedSquire.Notify(header, text);
				JammedSquire.NoHarderLotJ = true;
			}
		}
		public static bool NoHarderLotJ = true;
		// Token: 0x06000752 RID: 1874 RVA: 0x000406DC File Offset: 0x0003E8DC
		private static void Notify(string header, string text)
		{
			isSingleLine = false;
			tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
			int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/curse2icon.png");
			GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, null, spriteIdByName, UINotificationController.NotificationColor.PURPLE, false, true);
		}
		public static bool isSingleLine;
	}
}


namespace BunnyMod
{
	public static class CurseRoomReward
    {
		public static void CurseRoomRewardMethod(Action<RoomHandler> orig, RoomHandler self)
		{
			bool harderlotj = JammedSquire.NoHarderLotJ;
			if (harderlotj)
			{
				orig(self);
			}
			else
            {
				orig(self);
				FloorRewardData currentRewardData = GameManager.Instance.RewardManager.CurrentRewardData;
				LootEngine.AmmoDropType ammoDropType = LootEngine.AmmoDropType.DEFAULT_AMMO;
				bool flag = LootEngine.DoAmmoClipCheck(currentRewardData, out ammoDropType);
				string path = (ammoDropType != LootEngine.AmmoDropType.SPREAD_AMMO) ? "Ammo_Pickup" : "Ammo_Pickup_Spread";
				float value = UnityEngine.Random.value;
				float num = currentRewardData.ChestSystem_ChestChanceLowerBound;
				//float num2 = GameManager.Instance.PrimaryPlayer.stats.GetStatValue(PlayerStats.StatType.Coolness) / 100f;
				float num3 = (GameManager.Instance.PrimaryPlayer.stats.GetStatValue(PlayerStats.StatType.Curse) / 250f);
				if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER)
				{
					num3 += GameManager.Instance.SecondaryPlayer.stats.GetStatValue(PlayerStats.StatType.Curse) / 250f;
				}
				if (PassiveItem.IsFlagSetAtAll(typeof(ChamberOfEvilItem)))
				{
					num3 *= 1.25f;
				}
				num = Mathf.Clamp(num + GameManager.Instance.PrimaryPlayer.AdditionalChestSpawnChance, currentRewardData.ChestSystem_ChestChanceLowerBound, currentRewardData.ChestSystem_ChestChanceUpperBound) + num3;
				bool flag2 = currentRewardData.SingleItemRewardTable != null;
				bool flag3 = false;
				float num4 = 0.1f;
				if (!RoomHandler.HasGivenRoomChestRewardThisRun && MetaInjectionData.ForceEarlyChest)
				{
					flag3 = true;
				}
				if (flag3)
				{
					if (!RoomHandler.HasGivenRoomChestRewardThisRun && (GameManager.Instance.CurrentFloor == 1 || GameManager.Instance.CurrentFloor == -1))
					{
						flag2 = false;
						num += num4;
						if (GameManager.Instance.PrimaryPlayer && GameManager.Instance.PrimaryPlayer.NumRoomsCleared > 4)
						{
							num = 1f;
						}
					}
					if (!RoomHandler.HasGivenRoomChestRewardThisRun && self.distanceFromEntrance < RoomHandler.NumberOfRoomsToPreventChestSpawning)
					{
						GameManager.Instance.Dungeon.InformRoomCleared(false, false);
						return;
					}
				}
				BraveUtility.Log("Current chest spawn chance: " + num, Color.yellow, BraveUtility.LogVerbosity.IMPORTANT);
				if (value > num)
				{
					if (flag)
					{
						IntVector2 bestRewardLocation = self.GetBestRewardLocation(new IntVector2(1, 1), RoomHandler.RewardLocationStyle.CameraCenter, true);
						LootEngine.SpawnItem((GameObject)BraveResources.Load(path, ".prefab"), bestRewardLocation.ToVector3(), Vector2.up, 1f, true, true, false);
					}
					GameManager.Instance.Dungeon.InformRoomCleared(false, false);
					return;
				}
				if (flag2)
				{
					float num5 = currentRewardData.PercentOfRoomClearRewardsThatAreChests;
					if (PassiveItem.IsFlagSetAtAll(typeof(AmazingChestAheadItem)))
					{
						num5 *= 2f;
						num5 = Mathf.Max(0.5f, num5);
					}
					flag2 = (UnityEngine.Random.value > num5);
				}
				if (flag2)
				{
					float num6 = (GameManager.Instance.CurrentGameType != GameManager.GameType.COOP_2_PLAYER) ? GameManager.Instance.RewardManager.SinglePlayerPickupIncrementModifier : GameManager.Instance.RewardManager.CoopPickupIncrementModifier;
					GameObject gameObject;
					if (UnityEngine.Random.value < 1f / num6)
					{
						gameObject = currentRewardData.SingleItemRewardTable.SelectByWeight(false);
					}
					else
					{
						gameObject = ((UnityEngine.Random.value >= 0.9f) ? GameManager.Instance.RewardManager.FullHeartPrefab.gameObject : GameManager.Instance.RewardManager.HalfHeartPrefab.gameObject);
					}
					UnityEngine.Debug.Log(gameObject.name + "SPAWNED");
					DebrisObject debrisObject = LootEngine.SpawnItem(gameObject, self.GetBestRewardLocation(new IntVector2(1, 1), RoomHandler.RewardLocationStyle.CameraCenter, true).ToVector3() + new Vector3(0.25f, 0f, 0f), Vector2.up, 1f, true, true, false);
					Exploder.DoRadialPush(debrisObject.sprite.WorldCenter.ToVector3ZUp(debrisObject.sprite.WorldCenter.y), 8f, 3f);
					AkSoundEngine.PostEvent("Play_OBJ_item_spawn_01", debrisObject.gameObject);
					GameManager.Instance.Dungeon.InformRoomCleared(true, false);
				}
				else
				{
					IntVector2 bestRewardLocation = self.GetBestRewardLocation(new IntVector2(2, 1), RoomHandler.RewardLocationStyle.CameraCenter, true);
					bool isRainbowRun = GameStatsManager.Instance.IsRainbowRun;
					if (isRainbowRun)
					{
						LootEngine.SpawnBowlerNote(GameManager.Instance.RewardManager.BowlerNoteChest, bestRewardLocation.ToCenterVector2(), self, true);
						RoomHandler.HasGivenRoomChestRewardThisRun = true;
					}
					else
					{
						Chest exists = self.SpawnRoomRewardChest(null, bestRewardLocation);
						if (exists)
						{
							RoomHandler.HasGivenRoomChestRewardThisRun = true;
						}
					}
					GameManager.Instance.Dungeon.InformRoomCleared(true, true);
				}
				if (flag)
				{
					IntVector2 bestRewardLocation = self.GetBestRewardLocation(new IntVector2(1, 1), RoomHandler.RewardLocationStyle.CameraCenter, true);
					LootEngine.DelayedSpawnItem(1f, (GameObject)BraveResources.Load(path, ".prefab"), bestRewardLocation.ToVector3() + new Vector3(0.25f, 0f, 0f), Vector2.up, 1f, true, true, false);
				}
			}

		}

	
	}
}




