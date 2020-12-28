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





namespace GungeonAPI
{
	// Token: 0x0200003F RID: 63
	public static class PleaseForgiveMe
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000128FC File Offset: 0x00010AFC
		public static void Add()
		{
			ShrineFactory shrineFactory = new ShrineFactory();
			{
				shrineFactory.name = "why";
				shrineFactory.modID = "bny";
				shrineFactory.text = "....";
				shrineFactory.spritePath = "BunnyMod/Resources/shrines/imsorryneighborino.png";
				shrineFactory.room = RoomFactory.BuildFromResource("BunnyMod/Resources/rooms/StrangerBossRoom.room").room;
				shrineFactory.acceptText = "...";
				shrineFactory.declineText = "Youre a monster Bunny.";
				shrineFactory.OnAccept = null;
				shrineFactory.OnDecline = null;
				shrineFactory.CanUse = new Func<PlayerController, GameObject, bool>(PleaseForgiveMe.CanUse);
				shrineFactory.offset = new Vector3(-19f, -19f, 0f);
				shrineFactory.talkPointOffset = new Vector3(0f, 3f, 0f);
				shrineFactory.isToggle = false;
				shrineFactory.isBreachShrine = false;
			}
			GameObject gameObject = shrineFactory.Build();

		}


		// Token: 0x060001C3 RID: 451 RVA: 0x000129D8 File Offset: 0x00010BD8
		public static bool CanUse(PlayerController player, GameObject shrine)
		{

			return false;

		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00012A0C File Offset: 0x00010C0C
		public static void Accept(PlayerController player, GameObject shrine)
		{
			
		}
	}
}
