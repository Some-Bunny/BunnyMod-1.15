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
using BunnyMod;
using Brave.BulletScript;




namespace GungeonAPI
{
	// Token: 0x0200003F RID: 63
	public static class ChaosCorruptionShrine
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000128FC File Offset: 0x00010AFC
		public static void Add()
		{
			ShrineFactory shrineFactorya = new ShrineFactory();
			{
				shrineFactorya.name = "ChaosCorruptionShrine";
				shrineFactorya.modID = "bny";
				shrineFactorya.text = "Whatever this shrine may have been, it has been eaten away by chaotic corruption.";
				shrineFactorya.spritePath = "BunnyMod/Resources/shrines/chaoscorruptionshrine.png";
				shrineFactorya.room = RoomFactory.BuildFromResource("BunnyMod/Resources/rooms/chaosshrine1.room").room;
				shrineFactorya.acceptText = "Poke at it like the curious idiot that you are.";
				shrineFactorya.declineText = "Leave it alone.";
				shrineFactorya.OnAccept = new Action<PlayerController, GameObject>(ChaosCorruptionShrine.Accept);
				shrineFactorya.OnDecline = null;
				shrineFactorya.CanUse = new Func<PlayerController, GameObject, bool>(ChaosCorruptionShrine.CanUse);
				shrineFactorya.offset = new Vector3(-1f, -1f, 0f);
				shrineFactorya.talkPointOffset = new Vector3(0f, 1f, 0f);
				shrineFactorya.isToggle = false;
				shrineFactorya.isBreachShrine = false;
			}
			GameObject gameObject = shrineFactorya.Build();

		}


		// Token: 0x060001C3 RID: 451 RVA: 0x000129D8 File Offset: 0x00010BD8
		public static bool CanUse(PlayerController player, GameObject shrine)
		{
			return shrine.GetComponent<ShrineFactory.CustomShrineController>().numUses == 0;

		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00012A0C File Offset: 0x00010C0C
		public static void Accept(PlayerController player, GameObject shrine)
		{
			AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", shrine);
			string header = "Chaos has consumed you!";
			shrine.GetComponent<ShrineFactory.CustomShrineController>().numUses++;
			int num3 = UnityEngine.Random.Range(0, 4);
			bool flag3 = num3 == 0;
			if (flag3)
			{
				AkSoundEngine.PostEvent("Play_OBJ_weapon_pickup_01", shrine);
				ApplyStat(player, PlayerStats.StatType.Damage, UnityEngine.Random.Range(1.15f, 1.35f), StatModifier.ModifyMethod.MULTIPLICATIVE);
				ApplyStat(player, PlayerStats.StatType.Health, 1f, StatModifier.ModifyMethod.ADDITIVE);
				if (player.characterIdentity == PlayableCharacters.Robot)
				{
					player.healthHaver.Armor += 2f;
				}
				int num = 2;
				Gun gun;
				do
				{
					gun = PickupObjectDatabase.GetRandomGun();
					num--;
				}
				while (num > 0 && player.HasGun(gun.PickupObjectId));
				bool flag = num == 0;
				if (flag)
				{
					gun = (Game.Items["bny:chaos_malice"] as Gun);
				}
				ChaosCorruptionShrine.m_extantGun = player.inventory.AddGunToInventory(gun, true);
				ChaosCorruptionShrine.m_extantGun.CanBeDropped = false;
				ChaosCorruptionShrine.m_extantGun.CanBeSold = false;
				bool modualrpog = player.HasPickupID(Game.Items["bny:modular_weapon_chip"].PickupObjectId);
				if (!modualrpog)
                {
					player.inventory.GunLocked.SetOverride("chaos curse", true, null);
				}
				player.gameObject.AddComponent<DeathHowl>();
				string text = "The Gungeon roars...";
				ChaosCorruptionShrine.Notify(header, text);
			}
			bool flag4 = num3 == 1;
			if (flag4)
			{
				ApplyStat(player, PlayerStats.StatType.Curse, -2f, StatModifier.ModifyMethod.ADDITIVE);
				ApplyStat(player, PlayerStats.StatType.Coolness, 2f, StatModifier.ModifyMethod.ADDITIVE);
				if (!GameManager.HasInstance || !GameManager.Instance.BestActivePlayer || GameManager.Instance.BestActivePlayer.CurrentRoom == null)
				{
					return;
				}
				GameObject superReaper = PrefabDatabase.Instance.SuperReaper;
				Vector2 vector = GameManager.Instance.BestActivePlayer.CurrentRoom.GetRandomVisibleClearSpot(2, 2).ToVector2();
				SpeculativeRigidbody component = superReaper.GetComponent<SpeculativeRigidbody>();
				if (component)
				{
					PixelCollider primaryPixelCollider = component.PrimaryPixelCollider;
					Vector2 a = PhysicsEngine.PixelToUnit(new IntVector2(primaryPixelCollider.ManualOffsetX, primaryPixelCollider.ManualOffsetY));
					Vector2 vector2 = PhysicsEngine.PixelToUnit(new IntVector2(primaryPixelCollider.ManualWidth, primaryPixelCollider.ManualHeight));
					Vector2 vector3 = new Vector2((float)Mathf.CeilToInt(vector2.x), (float)Mathf.CeilToInt(vector2.y));
					Vector2 b = new Vector2((vector3.x - vector2.x) / 2f, 0f).Quantize(0.0625f);
					vector -= a - b;
				}
				UnityEngine.Object.Instantiate<GameObject>(superReaper, vector.ToVector3ZUp(0f), Quaternion.identity);
				string text = "The Gungeon cackles...";
				ChaosCorruptionShrine.Notify(header, text);
			}
			bool flag5 = num3 == 2;
			if (flag5)
			{
				float num = 0f;
				ApplyStat(player, PlayerStats.StatType.Damage, 1.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
				ApplyStat(player, PlayerStats.StatType.DamageToBosses, 1.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
				num = (player.stats.GetStatValue(PlayerStats.StatType.Health));
				ApplyStat(player, PlayerStats.StatType.Health, (-num)+1, StatModifier.ModifyMethod.ADDITIVE);
				player.gameObject.AddComponent<OopsAllMasterRounds>();
				string text = "The Gungeon trembles...";
				ChaosCorruptionShrine.Notify(header, text);
			}
			bool flag6 = num3 == 3;
			if (flag6)
			{
				string text = "The Gungeon shifts...";
				ChaosCorruptionShrine.Notify(header, text);
				if (GameManager.Instance.Dungeon.IsGlitchDungeon)
				{
					GameManager.Instance.InjectedFlowPath = "Core Game Flows/Secret_DoubleBeholster_Flow";
				}
				if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) GameManager.Instance.LoadCustomLevel("tt_castle");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.SEWERGEON) GameManager.Instance.LoadCustomLevel("tt_sewer");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON) GameManager.Instance.LoadCustomLevel("tt5");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) GameManager.Instance.LoadCustomLevel("tt_cathedral");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) GameManager.Instance.LoadCustomLevel("tt_mines");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) GameManager.Instance.LoadCustomLevel("tt_catacombs");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) GameManager.Instance.LoadCustomLevel("tt_forge");
				else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) GameManager.Instance.LoadCustomLevel("tt_bullethell");
				else
				{
					IntVector2 bestRewardLocation = player.CurrentRoom.GetBestRewardLocation(IntVector2.One * 3, RoomHandler.RewardLocationStyle.PlayerCenter, true);
					Chest rainbow_Chest = GameManager.Instance.RewardManager.Rainbow_Chest;
					rainbow_Chest.IsLocked = false;
					Chest.Spawn(rainbow_Chest, bestRewardLocation);
					//fuck you im not changing the reward lol
				}
			}
			bool flag7 = num3 == 4;
			if (flag7)
			{
				string text = "The Gungeon screeches...";
				ChaosCorruptionShrine.Notify(header, text);
			}
		}

		private static void Notify(string header, string text)
		{
			tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
			int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/chaosthing");
			GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, encounterIconCollection, spriteIdByName, UINotificationController.NotificationColor.SILVER, false, false);
		}
		private static void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod modifyMethod)
		{
			player.stats.RecalculateStats(player, false, false);
			StatModifier statModifier = new StatModifier()
			{
				statToBoost = statType,
				amount = amountToApply,
				modifyType = modifyMethod
			};
			player.ownerlessStatModifiers.Add(statModifier);
			player.stats.RecalculateStats(player, false, false);
		}
		public class DeathHowl : BraveBehaviour
		{
			public void Start()
			{

			}
			public void Update()
			{
				PlayerController player = GameManager.Instance.PrimaryPlayer;
				bool owo = player.CurrentGun.CurrentAmmo == 0 && ChaosCorruptionShrine.m_extantGun;
				if (owo)
				{
					AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", base.gameObject);
					player.inventory.GunLocked.RemoveOverride("chaos curse");
					player.inventory.DestroyGun(ChaosCorruptionShrine.m_extantGun);
					ChaosCorruptionShrine.m_extantGun = null;
					
				}
			}
			
		}
		public class OopsAllMasterRounds : BraveBehaviour
		{
			public void Start()
			{
				PlayerController player = GameManager.Instance.PrimaryPlayer;
				player.healthHaver.OnDamaged += this.OnDamaged;
			}
			public void Update()
			{

			}
			private void OnDamaged(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
			{
				PlayerController player = GameManager.Instance.PrimaryPlayer;
				bool flag = player.CurrentRoom != null;
				if (flag)
				{
					player.CurrentRoom.PlayerHasTakenDamageInThisRoom = false;
				}
			}

		}
		public static PickupObject.ItemQuality Spawnquality;
		public static PickupObject target;
		public static PickupObject target1;
		public static Gun m_extantGun;
	}
}
