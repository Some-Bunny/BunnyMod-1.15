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
using MonoMod;


namespace BunnyMod
{
    public class ReaverAmmolet : BlankModificationItem
    {

		public static void Init()
		{
			string name = "Reaver Ammolet";
			string resourcePath = "BunnyMod/Resources/reaverammolet.png";
			GameObject gameObject = new GameObject(name);
			ReaverAmmolet glassmolet = gameObject.AddComponent<ReaverAmmolet>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "FWOOMP";
			string longDesc = "An ammolet embedded with the heart of a Void Reaver. The Heart expells a great deal of Void energy when a blank is used.";
			glassmolet.SetupItem(shortDesc, longDesc, "bny");
			glassmolet.quality = PickupObject.ItemQuality.A;
			glassmolet.AddPassiveStatModifier(PlayerStats.StatType.AdditionalBlanksPerFloor, 1f, StatModifier.ModifyMethod.ADDITIVE);
			glassmolet.AddToSubShop(ItemBuilder.ShopType.OldRed, 1f);

		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			player.OnUsedBlank += this.Reave;
		}
		private void Reave(PlayerController player, int integer)
		{
			List<AIActor> activeEnemies = base.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
			bool flag = activeEnemies != null && base.Owner != null;
			if (flag)
			{
				for (int i = 0; i < activeEnemies.Count; i++)
				{
					AkSoundEngine.PostEvent("Play_wpn_kthulu_soul_01", base.gameObject);
					activeEnemies[i].ApplyEffect(this.DetainEffect, 1f, null);
				}
			}
		}
		public GameActorSpeedEffect DetainEffect = new GameActorSpeedEffect
		{
			AffectsPlayers = false,
			duration = 6f,
			AppliesTint = true,
			SpeedMultiplier = 0f,
			maxStackedDuration = 12f,
			TintColor = new Color(0.3f, 0f, 0.3f).WithAlpha(0.9f)
		};
		// Token: 0x0400014B RID: 331


	}
}



