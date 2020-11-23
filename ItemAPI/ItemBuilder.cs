using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gungeon;
using UnityEngine;

namespace ItemAPI
{
	// Token: 0x02000007 RID: 7
	public static class ItemBuilder
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002DFF File Offset: 0x00000FFF
		public static void Init()
		{
			FakePrefabHooks.Init();
			CompanionBuilder.Init();
			Tools.Init();
			ItemBuilder.LoadShopTables();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E1C File Offset: 0x0000101C
		private static void LoadShopTables()
		{
			ItemBuilder.shopInventories = new Dictionary<ItemBuilder.ShopType, GenericLootTable>();
			ItemBuilder.shopInventories.Add(ItemBuilder.ShopType.Flynt, ItemBuilder.LoadShopTable("Shop_Key_Items_01"));
			ItemBuilder.shopInventories.Add(ItemBuilder.ShopType.Trorc, ItemBuilder.LoadShopTable("Shop_Truck_Items_01"));
			ItemBuilder.shopInventories.Add(ItemBuilder.ShopType.Cursula, ItemBuilder.LoadShopTable("Shop_Curse_Items_01"));
			ItemBuilder.shopInventories.Add(ItemBuilder.ShopType.Goopton, ItemBuilder.LoadShopTable("Shop_Goop_Items_01"));
			ItemBuilder.shopInventories.Add(ItemBuilder.ShopType.OldRed, ItemBuilder.LoadShopTable("Shop_Blank_Items_01"));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002EA4 File Offset: 0x000010A4
		public static GenericLootTable LoadShopTable(string assetName)
		{
			return Tools.sharedAuto1.LoadAsset<GenericLootTable>(assetName);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002EC4 File Offset: 0x000010C4
		public static GameObject AddSpriteToObject(string name, string resourcePath, GameObject obj = null, bool v = false)
		{
			GameObject gameObject = SpriteBuilder.SpriteFromResource(resourcePath, obj, false);
			gameObject.name = name;
			return gameObject;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002EE8 File Offset: 0x000010E8
		public static void SetupItem(this PickupObject item, string shortDesc, string longDesc, string idPool = "customItems")
		{
			try
			{
				item.encounterTrackable = null;
				ETGMod.Databases.Items.SetupItem(item, item.name);
				SpriteBuilder.AddToAmmonomicon(item.sprite.GetCurrentSpriteDef());
				item.encounterTrackable.journalData.AmmonomiconSprite = item.sprite.GetCurrentSpriteDef().name;
				GunExt.SetName(item, item.name);
				GunExt.SetShortDescription(item, shortDesc);
				GunExt.SetLongDescription(item, longDesc);
				bool flag = item is PlayerItem;
				if (flag)
				{
					(item as PlayerItem).consumable = false;
				}
				Game.Items.Add(idPool + ":" + item.name.ToLower().Replace(" ", "_"), item);
				ETGMod.Databases.Items.Add(item, false, "ANY");
				FakePrefab.MarkAsFakePrefab(item.gameObject);
				item.gameObject.SetActive(false);
			}
			catch (Exception ex)
			{
				ETGModConsole.Log(ex.Message, false);
				ETGModConsole.Log(ex.StackTrace, false);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003008 File Offset: 0x00001208
		public static void AddToSubShop(this PickupObject po, ItemBuilder.ShopType type, float weight)
		{
			ItemBuilder.shopInventories[type].defaultItemDrops.Add(new WeightedGameObject
			{
				pickupId = po.PickupObjectId,
				weight = weight,
				rawGameObject = po.gameObject,
				forceDuplicatesPossible = false,
				additionalPrerequisites = new DungeonPrerequisite[0]
			});
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003068 File Offset: 0x00001268
		public static void SetCooldownType(this PlayerItem item, ItemBuilder.CooldownType cooldownType, float value)
		{
			item.damageCooldown = -1f;
			item.roomCooldown = -1;
			item.timeCooldown = -1f;
			switch (cooldownType)
			{
				case ItemBuilder.CooldownType.Timed:
					item.timeCooldown = value;
					break;
				case ItemBuilder.CooldownType.Damage:
					item.damageCooldown = value;
					break;
				case ItemBuilder.CooldownType.PerRoom:
					item.roomCooldown = (int)value;
					break;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000030C8 File Offset: 0x000012C8
		public static StatModifier AddPassiveStatModifier(this PickupObject po, PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod method = StatModifier.ModifyMethod.ADDITIVE)
		{
			StatModifier statModifier = new StatModifier();
			statModifier.amount = amount;
			statModifier.statToBoost = statType;
			statModifier.modifyType = method;
			po.AddPassiveStatModifier(statModifier);
			return statModifier;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003100 File Offset: 0x00001300
		public static void AddPassiveStatModifier(this PickupObject po, StatModifier modifier)
		{
			bool flag = po is PlayerItem;
			if (flag)
			{
				PlayerItem playerItem = po as PlayerItem;
				bool flag2 = playerItem.passiveStatModifiers == null;
				if (flag2)
				{
					playerItem.passiveStatModifiers = new StatModifier[]
					{
						modifier
					};
				}
				else
				{
					playerItem.passiveStatModifiers = playerItem.passiveStatModifiers.Concat(new StatModifier[]
					{
						modifier
					}).ToArray<StatModifier>();
				}
			}
			else
			{
				bool flag3 = po is PassiveItem;
				if (!flag3)
				{
					throw new NotSupportedException("Object must be of type PlayerItem or PassiveItem");
				}
				PassiveItem passiveItem = po as PassiveItem;
				bool flag4 = passiveItem.passiveStatModifiers == null;
				if (flag4)
				{
					passiveItem.passiveStatModifiers = new StatModifier[]
					{
						modifier
					};
				}
				else
				{
					passiveItem.passiveStatModifiers = passiveItem.passiveStatModifiers.Concat(new StatModifier[]
					{
						modifier
					}).ToArray<StatModifier>();
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000031D0 File Offset: 0x000013D0
		public static bool RemovePassiveStatModifier(this PickupObject po, StatModifier modifier)
		{
			bool flag = po is PlayerItem;
			bool result;
			if (flag)
			{
				PlayerItem playerItem = po as PlayerItem;
				bool flag2 = playerItem.passiveStatModifiers == null;
				if (flag2)
				{
					return false;
				}
				List<StatModifier> list = playerItem.passiveStatModifiers.ToList<StatModifier>();
				result = list.Remove(modifier);
				playerItem.passiveStatModifiers = list.ToArray();
			}
			else
			{
				bool flag3 = po is PassiveItem;
				if (!flag3)
				{
					throw new NotSupportedException("Object must be of type PlayerItem or PassiveItem");
				}
				PassiveItem passiveItem = po as PassiveItem;
				bool flag4 = passiveItem.passiveStatModifiers == null;
				if (flag4)
				{
					return false;
				}
				List<StatModifier> list2 = passiveItem.passiveStatModifiers.ToList<StatModifier>();
				result = list2.Remove(modifier);
				passiveItem.passiveStatModifiers = list2.ToArray();
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003297 File Offset: 0x00001497
		public static IEnumerator HandleDuration(PlayerItem item, float duration, PlayerController user, Action<PlayerController> OnFinish)
		{
			bool isCurrentlyActive = item.IsCurrentlyActive;
			if (isCurrentlyActive)
			{
				yield break;
			}
			ItemBuilder.SetPrivateType<PlayerItem>(item, "m_isCurrentlyActive", true);
			ItemBuilder.SetPrivateType<PlayerItem>(item, "m_activeElapsed", 0f);
			ItemBuilder.SetPrivateType<PlayerItem>(item, "m_activeDuration", duration);
			Action<PlayerItem> onActivationStatusChanged = item.OnActivationStatusChanged;
			if (onActivationStatusChanged != null)
			{
				onActivationStatusChanged(item);
			}
			float elapsed = ItemBuilder.GetPrivateType<PlayerItem, float>(item, "m_activeElapsed");
			float dur = ItemBuilder.GetPrivateType<PlayerItem, float>(item, "m_activeDuration");
			while (ItemBuilder.GetPrivateType<PlayerItem, float>(item, "m_activeElapsed") < ItemBuilder.GetPrivateType<PlayerItem, float>(item, "m_activeDuration") && item.IsCurrentlyActive)
			{
				yield return null;
			}
			ItemBuilder.SetPrivateType<PlayerItem>(item, "m_isCurrentlyActive", false);
			Action<PlayerItem> onActivationStatusChanged2 = item.OnActivationStatusChanged;
			if (onActivationStatusChanged2 != null)
			{
				onActivationStatusChanged2(item);
			}
			if (OnFinish != null)
			{
				OnFinish(user);
			}
			yield break;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000032BC File Offset: 0x000014BC
		private static void SetPrivateType<T>(T obj, string field, bool value)
		{
			FieldInfo field2 = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
			field2.SetValue(obj, value);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032F0 File Offset: 0x000014F0
		private static void SetPrivateType<T>(T obj, string field, float value)
		{
			FieldInfo field2 = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
			field2.SetValue(obj, value);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003324 File Offset: 0x00001524
		private static T2 GetPrivateType<T, T2>(T obj, string field)
		{
			FieldInfo field2 = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
			return (T2)((object)field2.GetValue(obj));
		}

		// Token: 0x04000005 RID: 5
		public static Dictionary<ItemBuilder.ShopType, GenericLootTable> shopInventories;

		// Token: 0x0200003E RID: 62
		public enum CooldownType
		{
			// Token: 0x0400005E RID: 94
			Timed,
			// Token: 0x0400005F RID: 95
			Damage,
			// Token: 0x04000060 RID: 96
			PerRoom,
			// Token: 0x04000061 RID: 97
			None
		}

		// Token: 0x0200003F RID: 63
		public enum ShopType
		{
			// Token: 0x04000063 RID: 99
			Goopton,
			// Token: 0x04000064 RID: 100
			Flynt,
			// Token: 0x04000065 RID: 101
			Cursula,
			// Token: 0x04000066 RID: 102
			Trorc,
			// Token: 0x04000067 RID: 103
			OldRed
		}
		public static void PlaceItemInAmmonomiconAfterItemById(this PickupObject item, int id)
		{
			item.ForcedPositionInAmmonomicon = PickupObjectDatabase.GetById(id).ForcedPositionInAmmonomicon;
		}
	}
}
