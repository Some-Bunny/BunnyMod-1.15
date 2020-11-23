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
    public class LunarGlassAmmolet : BlankModificationItem
    {

		public static void Init()
		{
			string name = "Lunar Glass Ammolet";
			string resourcePath = "BunnyMod/Resources/flashbangammolet.png";
			GameObject gameObject = new GameObject(name);
			LunarGlassAmmolet glassmolet = gameObject.AddComponent<LunarGlassAmmolet>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "BREAK";
			string longDesc = "An ammolet forged out of Lunar Glass. This ammolet turns anything nearby to glass.";
			glassmolet.SetupItem(shortDesc, longDesc, "bny");
			glassmolet.quality = PickupObject.ItemQuality.B;
			glassmolet.AddPassiveStatModifier(PlayerStats.StatType.AdditionalBlanksPerFloor, 1f, StatModifier.ModifyMethod.ADDITIVE);
			glassmolet.AddToSubShop(ItemBuilder.ShopType.OldRed, 1f);
			LunarGlassAmmolet.FlahBaAmmoletID = glassmolet.PickupObjectId;

		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			player.OnUsedBlank += this.Shatter;
		}
		private void Shatter(PlayerController player, int integer)
		{
			List<AIActor> activeEnemies = base.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
			bool flag = activeEnemies != null && base.Owner != null;
			if (flag)
			{
				for (int i = 0; i < activeEnemies.Count; i++)
				{
					AkSoundEngine.PostEvent("Play_OBJ_glass_shatter_01", base.gameObject);
					activeEnemies[i].ApplyEffect(this.GlassEffect, 1f, null);
				}
			}
		}
		public GameActorFreezeEffect GlassEffect = new GameActorFreezeEffect
		{
			TintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(1f),
			DeathTintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(1f),
			AppliesTint = true,
			AppliesDeathTint = true,
			effectIdentifier = "Shatter",
			FreezeAmount = 100f,
			UnfreezeDamagePercent = 0f,
			crystalNum = 0,
			crystalRot = 0,
			crystalVariation = new Vector2(0.05f, 0.05f),
			debrisMinForce = 5,
			debrisMaxForce = 5,
			debrisAngleVariance = 15f,
			PlaysVFXOnActor = true,
			duration = 7f,
			OverheadVFX = ShatterEffect.ShatterVFXObject,
		};
		// Token: 0x0400014B RID: 331
		private static int FlahBaAmmoletID;


	}
}



