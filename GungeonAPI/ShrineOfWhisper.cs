using System;
using System.Collections.Generic;
using Gungeon;
using GungeonAPI;
using UnityEngine;


namespace GungeonAPI
{
	// Token: 0x0200000E RID: 14
	public static class WhisperShrine
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000480C File Offset: 0x00002A0C
		public static void Add()
		{
			ShrineFactory ShrineFactory = new ShrineFactory
			{
				name = "Shrine Of The Whisper",
				modID = "BunnyMod",
				spritePath = "BunnyMod/Resources/ShrineOfWhisper/ancientwhisper_idle_001.png",
				shadowSpritePath = "BunnyMod/Resources/ShrineOfWhisper/ancientwhispershadow_001",
				acceptText = "I challenge the Shrine Of The Whisper. (Lag Warning)",
				declineText = "I don't challenge the Shrine Of The Whisper.",
				OnAccept = new Action<PlayerController, GameObject>(WhisperShrine.Accept),
				OnDecline = null,
				CanUse = new Func<PlayerController, GameObject, bool>(WhisperShrine.CanUse),
				offset = new Vector3(103.9375f, 15.5f, 16f),
				talkPointOffset = new Vector3(3f, 3f, 0f),
				isToggle = false,
				isBreachShrine = true,
				interactableComponent = typeof(WhisperShrineInteractible)
			};
			GameObject gameObject = ShrineFactory.Build();
			gameObject.AddAnimation("idle", "BunnyMod/Resources/ShrineOfWhisper/", 2, NPCBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk", "BunnyMod/Resources/ShrineOfWhisper/", 6, NPCBuilder.AnimationType.Talk, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk_start", "BunnyMod/Resources/ShrineOfWhisper/", 6, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("do_effect", "BunnyMod/Resources/ShrineOfWhisper/", 5, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			WhisperShrineInteractible component = gameObject.GetComponent<WhisperShrineInteractible>();
			component.conversation = new List<string>
			{
				"Challenge the Shrine Of The Whisper?"
			};
			gameObject.SetActive(false);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0003ACFC File Offset: 0x00038EFC

		private static bool CanUse(PlayerController player, GameObject npc)
		{
			return true;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0003AD10 File Offset: 0x00038F10
		public static void Accept(PlayerController player, GameObject npc)
		{
			npc.GetComponent<tk2dSpriteAnimator>().PlayForDuration("do_effect", 2f, "idle", false);
			WhisperShrine.HandleLoadout(player);
		}
		public static void HandleLoadout(PlayerController player)
		{
			for (int i = 0; i < 4; i++)
            {
				for (int iA = 0; iA < 5; iA++)
				{
					for (int i2 = 0; i2 < 5; i2++)
					{
						WhisperShrine.DestroyYourStats(player);
					}
				}
			}
		}
		public static void DestroyYourStats(PlayerController player)
		{
			ApplyStat(player, PlayerStats.StatType.Damage, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.MoneyMultiplierFromEnemies, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.ReloadSpeed, UnityEngine.Random.Range(1.4f, 0.5f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.RateOfFire, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.DodgeRollDistanceMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.DodgeRollSpeedMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.DamageToBosses, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.AmmoCapacityMultiplier, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.AdditionalClipCapacityMultiplier, UnityEngine.Random.Range(1.5f, 0.6f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
			ApplyStat(player, PlayerStats.StatType.GlobalPriceMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
		}
		private static void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod modifyMethod)
		{
			//player.stats.RecalculateStats(player, false, false);
			StatModifier statModifier = new StatModifier()
			{
				statToBoost = statType,
				amount = amountToApply,
				modifyType = modifyMethod
			};
			player.ownerlessStatModifiers.Add(statModifier);
			player.stats.RecalculateStats(player, false, false);
		}
	}
}






