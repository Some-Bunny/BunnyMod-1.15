using System;
using System.Collections.Generic;
using Gungeon;
using GungeonAPI;
using UnityEngine;


namespace GungeonAPI
{
	// Token: 0x0200000E RID: 14
	public static class ArtifactMonger
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000480C File Offset: 0x00002A0C
		public static void Add()
		{
			ShrineFactory ShrineFactory = new ShrineFactory
			{
				name = "Artifact Monger",
				modID = "BunnyMod",
				spritePath = "BunnyMod/Resources/Artifacts/ArtifactNPC/Idle/artifactmonger_idle_001.png",
				shadowSpritePath = "BunnyMod/Resources/Artifacts/ArtifactNPC/artifactmongerplaceshadow_001.png",
				acceptText = "...Sure?",
				declineText = "No thanks.",
				OnAccept = new Action<PlayerController, GameObject>(ArtifactMonger.Accept),
				OnDecline = null,
				CanUse = new Func<PlayerController, GameObject, bool>(ArtifactMonger.CanUse),
				offset = new Vector3(38.125f, 27f, 26.5f),
				talkPointOffset = new Vector3(3f, 3f, 0f),
				isToggle = false,
				isBreachShrine = true,
				interactableComponent = typeof(ArtifactMongerInteractible)
			};
			GameObject gameObject = ShrineFactory.Build();
			gameObject.AddAnimation("idle", "BunnyMod/Resources/Artifacts/ArtifactNPC/Idle/", 2, NPCBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk", "BunnyMod/Resources/Artifacts/ArtifactNPC/Talk/", 6, NPCBuilder.AnimationType.Talk, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk_start", "BunnyMod/Resources/Artifacts/ArtifactNPC/TalkStart/", 6, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("do_effect", "BunnyMod/Resources/Artifacts/ArtifactNPC/DoEffect/", 5, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			ArtifactMongerInteractible component = gameObject.GetComponent<ArtifactMongerInteractible>();
			component.conversation = new List<string>
			{
				"helo!",
				"i com from favaway pwanet!",
				"soooooo fav awayyyyy!!",
				"and breng pwetty wocks!",
				"but te wocks hurt...",
				"...",
				"...do u want one? tu help me?"
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
			ArtifactMonger.HandleLoadout(player);
		}
		public static void HandleLoadout(PlayerController player)
		{
			ArtifactMonger.Char = UnityEngine.Random.Range(1, 12);
			switch (ArtifactMonger.Char)
			{
				case 1:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Attraction"].gameObject, player, true);
					break;
				case 2:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Revenge"].gameObject, player, true);
					break;
				case 3:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Glass"].gameObject, player, true);
					break;
				case 4:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Avarice"].gameObject, player, true);
					break;
				case 5:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Daze"].gameObject, player, true);
					break;
				case 6:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Prey"].gameObject, player, true);
					break;
				case 7:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Megalomania"].gameObject, player, true);
					break;
				case 8:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fodder"].gameObject, player, true);
					break;
				case 9:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Bolster"].gameObject, player, true);
					break;
				case 10:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Enigma"].gameObject, player, true);
					break;
				case 11:
					LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Sacrifice"].gameObject, player, true);
					break;
			}
			ArtifactMonger.storedPlayer = player;

		}



		// Token: 0x04000015 RID: 21
		private static PlayerController storedPlayer;
		public static int Char = 0;
    }
}






