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
			ReaverAmmolet.ReaverletID = glassmolet.PickupObjectId;

		}
		private static int ReaverletID;


		private static Hook BlankHook = new Hook(typeof(SilencerInstance).GetMethod("ProcessBlankModificationItemAdditionalEffects", BindingFlags.Instance | BindingFlags.NonPublic), typeof(ReaverAmmolet).GetMethod("BlankModHook", BindingFlags.Instance | BindingFlags.Public), typeof(SilencerInstance));

		public void BlankModHook(Action<SilencerInstance, BlankModificationItem, Vector2, PlayerController> orig, SilencerInstance silencer, BlankModificationItem bmi, Vector2 centerPoint, PlayerController user)
		{
			orig(silencer, bmi, centerPoint, user);
			try
			{
				if (user.HasPickupID(ReaverletID))
				{
					RoomHandler currentRoom = user.CurrentRoom;
					if (currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All))
					{
						//AkSoundEngine.PostEvent("Play_wpn_kthulu_soul_01", base.gameObject);
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
			//AkSoundEngine.PostEvent("Play_wpn_kthulu_soul_01", base.gameObject);
			target.ApplyEffect(Library.Detain, 1f, null);
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



namespace BunnyMod
{
	// Token: 0x02000050 RID: 80
	public static class Library
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0001426C File Offset: 0x0001246C


		public static GameActorSpeedEffect Detain = new GameActorSpeedEffect
		{
			AffectsPlayers = false,
			duration = 6f,
			AppliesTint = true,
			SpeedMultiplier = 0f,
			maxStackedDuration = 12f,
			TintColor = new Color(0.3f, 0f, 0.3f).WithAlpha(0.9f),
			effectIdentifier = "Detain"

		};
		public static GameActorFreezeEffect Shatter = new GameActorFreezeEffect
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
			duration = 9f,
		};
	}
}