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
		private static Hook BlankHook = new Hook(typeof(SilencerInstance).GetMethod("ProcessBlankModificationItemAdditionalEffects", BindingFlags.Instance | BindingFlags.NonPublic), typeof(LunarGlassAmmolet).GetMethod("BlankModHook", BindingFlags.Instance | BindingFlags.Public), typeof(SilencerInstance));

		public void BlankModHook(Action<SilencerInstance, BlankModificationItem, Vector2, PlayerController> orig, SilencerInstance silencer, BlankModificationItem bmi, Vector2 centerPoint, PlayerController user)
		{
			orig(silencer, bmi, centerPoint, user);
			try
			{
				if (user.HasPickupID(FlahBaAmmoletID))
				{
					RoomHandler currentRoom = user.CurrentRoom;
					if (currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All))
					{
						//AkSoundEngine.PostEvent("Play_OBJ_glass_shatter_01", base.gameObject);
						foreach (AIActor aiactor in currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
						{
							if (aiactor.behaviorSpeculator != null)
							{
								this.AffectEnemy(aiactor);

							}
						}
					}
				}
			}
			catch (Exception e)
			{
				ETGModConsole.Log(e.Message);
				ETGModConsole.Log(e.StackTrace);
			}
		}
		private void AffectEnemy(AIActor target)
		{
			target.ApplyEffect(Library.Shatter, 1f, null);
		}

		// Token: 0x0400014B RID: 331
		private static int FlahBaAmmoletID;


	}
}



