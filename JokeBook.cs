using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;
using MultiplayerBasicExample;

namespace BunnyMod
{
    public class JokeBook : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Joke Book";
            string resourceName = "BunnyMod/Resources/jokebook";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<JokeBook>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Comedy Copper!";
            string longDesc = "It's not actually a joke book, its just my pastebin.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Coolness, 1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalItemCapacity, 1f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.D;
        }
		private float aaaaaaaaaaaaaaaaaaaa = 1;
		private void RoomCleared(PlayerController obj)
		{
			int num = UnityEngine.Random.Range(1, 6);
			bool flag = (float)num < this.aaaaaaaaaaaaaaaaaaaa;
			if (flag)
			{
				string header = "";
				string text = "";
				int num2 = UnityEngine.Random.Range(0, 101);
				bool flag2 = num2 <= 99;
				if (flag2)
				{
					int num3 = UnityEngine.Random.Range(0, 5);
					bool flag3 = num3 == 0;
					if (flag3)
					{
						header = "Have you ever eaten a clock?";
						text = "It's very time-consuming.";
					}
					bool flag4 = num3 == 1;
					if (flag4)
					{
						header = "Prismatism";
						text = "https://modworkshop.net/mod/27616";
					}
					bool flag5 = num3 == 2;
					if (flag5)
					{
						header = "Wood Fired Pizza?";
						text = "How will pizza get a job now?";
					}
					bool flag6 = num3 == 3;
					if (flag6)
					{
						header = "Gungeoneer";
						text = "Gungeonfar";
					}
					bool flag7 = num3 == 4;
					if (flag7)
					{
						header = "| ||";
						text = "|| |_";
					}
					bool flag9 = num3 == 5;
					if (flag9)
					{
						header = "Isaac Balancing.";
						text = "/shrug";
					}
				}
				bool flag8 = num2 >= 100;
				if (flag8)
				{
					header = "subscribe to my";
					text = "youtube channel";
				}
				this.Notify(header, text);
			}
		}
		public override void Pickup(PlayerController player)
        {
            player.OnRoomClearEvent += this.RoomCleared;
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnRoomClearEvent -= this.RoomCleared;
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
		private void Notify(string header, string text)
		{
			tk2dBaseSprite notificationObjectSprite = GameUIRoot.Instance.notificationController.notificationObjectSprite;
			GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, notificationObjectSprite.Collection, notificationObjectSprite.spriteId, UINotificationController.NotificationColor.SILVER, false, false);
		}
	}
}




