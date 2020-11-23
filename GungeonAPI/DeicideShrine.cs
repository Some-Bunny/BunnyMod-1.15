using System;
using System.Collections.Generic;
using Gungeon;
using GungeonAPI;
using UnityEngine;


namespace GungeonAPI
{
	// Token: 0x0200000E RID: 14
	public static class DeicideShrine
	{
		// Token: 0x06000059 RID: 89 RVA: 0x0000480C File Offset: 0x00002A0C
		public static void Add()
		{
			ShrineFactory ShrineFactory = new ShrineFactory
			{
				name = "Deicide Shrine",
				modID = "BunnyMod",
				spritePath = "BunnyMod/Resources/DeicideShrine/dcdshrine_idle_001.png",
				shadowSpritePath = "BunnyMod/Resources/DeicideShrine/dcdshrine_shadow.png",
				acceptText = "I challenge fate. (Gives all artifacts)",
				declineText = "I don't challenge fate.",
				OnAccept = new Action<PlayerController, GameObject>(DeicideShrine.Accept),
				OnDecline = null,
				CanUse = new Func<PlayerController, GameObject, bool>(DeicideShrine.CanUse),
				offset = new Vector3(12.6875f, 21.9375f, 22.4375f),
				talkPointOffset = new Vector3(3f, 3f, 0f),
				isToggle = false,
				isBreachShrine = true,
				interactableComponent = typeof(DeicideShrineInteractible)
			};
			GameObject gameObject = ShrineFactory.Build();
			gameObject.AddAnimation("idle", "BunnyMod/Resources/DeicideShrine/", 2, NPCBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk", "BunnyMod/Resources/DeicideShrine/", 3, NPCBuilder.AnimationType.Talk, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("talk_start", "BunnyMod/Resources/DeicideShrine/", 4, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			gameObject.AddAnimation("do_effect", "BunnyMod/Resources/DeicideShrine/", 5, NPCBuilder.AnimationType.Other, DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType.None);
			DeicideShrineInteractible component = gameObject.GetComponent<DeicideShrineInteractible>();
			component.conversation = new List<string>
			{
				"Put yourself to the ultimate test?"
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
			npc.GetComponent<tk2dSpriteAnimator>().PlayForDuration("do_effect", 1f, "idle", false);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Attraction"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Revenge"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Glass"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Avarice"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Daze"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Prey"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Megalomania"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fodder"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Bolster"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Enigma"].gameObject, player, true);
			LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Sacrifice"].gameObject, player, true);

		}
	}
}






