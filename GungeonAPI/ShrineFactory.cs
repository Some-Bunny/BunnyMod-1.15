using System;
using System.Collections.Generic;
using Dungeonator;
using ItemAPI;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x0200000C RID: 12
	public class ShrineFactory
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00005A60 File Offset: 0x00003C60
		public static void Init()
		{
			bool initialized = ShrineFactory.m_initialized;
			bool flag = !initialized;
			if (flag)
			{
				DungeonHooks.OnFoyerAwake += ShrineFactory.PlaceBnyBreachShrines;
				DungeonHooks.OnPreDungeonGeneration += delegate (LoopDungeonGenerator generator, Dungeon dungeon, DungeonFlow flow, int dungeonSeed)
				{
					bool flag2 = flow.name != "Foyer Flow" && !GameManager.IsReturningToFoyerWithPlayer;
					bool flag3 = flag2;
					if (flag3)
					{
						ShrineFactory.CleanupBreachShrines();
					}
				};
				ShrineFactory.m_initialized = true;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005ABC File Offset: 0x00003CBC
		public GameObject Build()
		{
			GameObject result;
			try
			{
				Texture2D textureFromResource = ResourceExtractor.GetTextureFromResource(this.spritePath);
				GameObject gameObject = SpriteBuilder.SpriteFromResource(this.spritePath, null, false);
				string text = (this.modID + ":" + this.name).ToLower().Replace(" ", "_");
				gameObject.name = text;
				tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
				component.IsPerpendicular = true;
				component.PlaceAtPositionByAnchor(this.offset, tk2dBaseSprite.Anchor.LowerCenter);
				Transform transform = new GameObject("talkpoint").transform;
				transform.position = gameObject.transform.position + this.talkPointOffset;
				transform.SetParent(gameObject.transform);
				bool flag = !this.usesCustomColliderOffsetAndSize;
				bool flag2 = flag;
				if (flag2)
				{
					IntVector2 intVector = new IntVector2(textureFromResource.width, textureFromResource.height);
					this.colliderOffset = new IntVector2(0, 0);
					this.colliderSize = new IntVector2(intVector.x, intVector.y / 2);
				}
				SpeculativeRigidbody speculativeRigidbody = component.SetUpSpeculativeRigidbody(this.colliderOffset, this.colliderSize);
				ShrineFactory.CustomShrineController customShrineController = gameObject.AddComponent<ShrineFactory.CustomShrineController>();
				customShrineController.ID = text;
				customShrineController.roomStyles = this.roomStyles;
				customShrineController.isBreachShrine = true;
				customShrineController.offset = this.offset;
				customShrineController.pixelColliders = speculativeRigidbody.specRigidbody.PixelColliders;
				customShrineController.factory = this;
				customShrineController.OnAccept = this.OnAccept;
				customShrineController.OnDecline = this.OnDecline;
				customShrineController.CanUse = this.CanUse;
				customShrineController.text = this.text;
				customShrineController.acceptText = this.acceptText;
				customShrineController.declineText = this.declineText;
				bool flag3 = this.interactableComponent == null;
				bool flag4 = flag3;
				if (flag4)
				{
					SimpleShrine simpleShrine = gameObject.AddComponent<SimpleShrine>();
					simpleShrine.isToggle = this.isToggle;
					simpleShrine.OnAccept = this.OnAccept;
					simpleShrine.OnDecline = this.OnDecline;
					simpleShrine.CanUse = this.CanUse;
					simpleShrine.text = this.text;
					simpleShrine.acceptText = this.acceptText;
					simpleShrine.declineText = this.declineText;
					simpleShrine.talkPoint = transform;
				}
				else
				{
					gameObject.AddComponent(this.interactableComponent);
				}
				gameObject.name = text;
				bool flag5 = !this.isBreachShrine;
				bool flag6 = flag5;
				if (flag6)
				{
					bool flag7 = !this.room;
					bool flag8 = flag7;
					if (flag8)
					{
						this.room = RoomFactory.CreateEmptyRoom(12, 12);
					}
					ShrineFactory.RegisterShrineRoom(gameObject, this.room, text, this.offset);
				}
				ShrineFactory.registeredShrines.Add(text, gameObject);
				FakePrefab.MarkAsFakePrefab(gameObject);
				Tools.Print<string>("Added shrine: " + text, "FFFFFF", false);
				result = gameObject;
			}
			catch (Exception e)
			{
				Tools.PrintException(e, "FF0000");
				result = null;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public static void RegisterShrineRoom(GameObject shrine, PrototypeDungeonRoom protoroom, string ID, Vector2 offset)
		{
			protoroom.category = PrototypeDungeonRoom.RoomCategory.NORMAL;
			DungeonPrerequisite[] array = new DungeonPrerequisite[0];
			Vector2 vector = new Vector2((float)(protoroom.Width / 2) + offset.x, (float)(protoroom.Height / 2) + offset.y);
			protoroom.placedObjectPositions.Add(vector);
			protoroom.placedObjects.Add(new PrototypePlacedObjectData
			{
				contentsBasePosition = vector,
				fieldData = new List<PrototypePlacedObjectFieldData>(),
				instancePrerequisites = array,
				linkedTriggerAreaIDs = new List<int>(),
				placeableContents = new DungeonPlaceable
				{
					width = 2,
					height = 2,
					respectsEncounterableDifferentiator = true,
					variantTiers = new List<DungeonPlaceableVariant>
					{
						new DungeonPlaceableVariant
						{
							percentChance = 1f,
							nonDatabasePlaceable = shrine,
							prerequisites = array,
							materialRequirements = new DungeonPlaceableRoomMaterialRequirement[0]
						}
					}
				}
			});
			RoomFactory.RoomData roomData = new RoomFactory.RoomData
			{
				room = protoroom,
				isSpecialRoom = true,
				category = "SPECIAL",
				specialSubCatergory = "UNSPECIFIED_SPECIAL"
			};
			RoomFactory.rooms.Add(ID, roomData);
			DungeonHandler.Register(roomData);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005F10 File Offset: 0x00004110
		public static void PlaceBnyBreachShrines()
		{
			ShrineFactory.CleanupBreachShrines();
			Tools.Print<string>("Placing breach shrines: ", "FFFFFF", false);
			foreach (GameObject gameObject in ShrineFactory.registeredShrines.Values)
			{
				try
				{
					ShrineFactory.CustomShrineController component = gameObject.GetComponent<ShrineFactory.CustomShrineController>();
					bool flag = !component.isBreachShrine;
					bool flag2 = !flag;
					if (flag2)
					{
						Tools.Print<string>("    " + gameObject.name, "FFFFFF", false);
						ShrineFactory.CustomShrineController component2 = UnityEngine.Object.Instantiate<GameObject>(gameObject).GetComponent<ShrineFactory.CustomShrineController>();
						component2.Copy(component);
						component2.gameObject.SetActive(true);
						component2.sprite.PlaceAtPositionByAnchor(component2.offset, tk2dBaseSprite.Anchor.LowerCenter);
						SpriteOutlineManager.AddOutlineToSprite(component2.sprite, Color.black);
						IPlayerInteractable component3 = component2.GetComponent<IPlayerInteractable>();
						bool flag3 = component3 is SimpleInteractable;
						bool flag4 = flag3;
						if (flag4)
						{
							((SimpleInteractable)component3).OnAccept = component2.OnAccept;
							((SimpleInteractable)component3).OnDecline = component2.OnDecline;
							((SimpleInteractable)component3).CanUse = component2.CanUse;
						}
						bool flag5 = !RoomHandler.unassignedInteractableObjects.Contains(component3);
						bool flag6 = flag5;
						if (flag6)
						{
							RoomHandler.unassignedInteractableObjects.Add(component3);
						}
					}
				}
				catch (Exception e)
				{
					Tools.PrintException(e, "FF0000");
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000060C4 File Offset: 0x000042C4
		private static void CleanupBreachShrines()
		{
			foreach (ShrineFactory.CustomShrineController customShrineController in UnityEngine.Object.FindObjectsOfType<ShrineFactory.CustomShrineController>())
			{
				bool flag = !FakePrefab.IsFakePrefab(customShrineController);
				bool flag2 = flag;
				if (flag2)
				{
					UnityEngine.Object.Destroy(customShrineController.gameObject);
				}
				else
				{
					customShrineController.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x04000018 RID: 24
		public string name;

		// Token: 0x04000019 RID: 25
		public string modID;

		// Token: 0x0400001A RID: 26
		public string spritePath;

		// Token: 0x0400001B RID: 27
		public string shadowSpritePath;

		// Token: 0x0400001C RID: 28
		public string text;

		// Token: 0x0400001D RID: 29
		public string acceptText;

		// Token: 0x0400001E RID: 30
		public string declineText;

		// Token: 0x0400001F RID: 31
		public Action<PlayerController, GameObject> OnAccept;

		// Token: 0x04000020 RID: 32
		public Action<PlayerController, GameObject> OnDecline;

		// Token: 0x04000021 RID: 33
		public Func<PlayerController, GameObject, bool> CanUse;

		// Token: 0x04000022 RID: 34
		public Vector3 talkPointOffset;

		// Token: 0x04000023 RID: 35
		public Vector3 offset = new Vector3(43.8f, 42.4f, 42.9f);

		// Token: 0x04000024 RID: 36
		public IntVector2 colliderOffset;

		// Token: 0x04000025 RID: 37
		public IntVector2 colliderSize;

		// Token: 0x04000026 RID: 38
		public bool isToggle;

		// Token: 0x04000027 RID: 39
		public bool usesCustomColliderOffsetAndSize;

		// Token: 0x04000028 RID: 40
		public Type interactableComponent = null;

		// Token: 0x04000029 RID: 41
		public bool isBreachShrine = false;

		// Token: 0x0400002A RID: 42
		public PrototypeDungeonRoom room;

		// Token: 0x0400002B RID: 43
		public Dictionary<string, int> roomStyles;

		// Token: 0x0400002C RID: 44
		public static Dictionary<string, GameObject> registeredShrines = new Dictionary<string, GameObject>();

		// Token: 0x0400002D RID: 45
		private static bool m_initialized;

		// Token: 0x02000131 RID: 305
		public class CustomShrineController : DungeonPlaceableBehaviour
		{
			// Token: 0x06000745 RID: 1861 RVA: 0x0003E69C File Offset: 0x0003C89C
			private void Start()
			{
				string text = base.name.Replace("(Clone)", "");
				bool flag = ShrineFactory.registeredShrines.ContainsKey(text);
				bool flag2 = flag;
				if (flag2)
				{
					this.Copy(ShrineFactory.registeredShrines[text].GetComponent<ShrineFactory.CustomShrineController>());
				}
				else
				{
					Tools.PrintError<string>("Was this shrine registered correctly?: " + text, "FF0000");
				}
				SimpleInteractable component = base.GetComponent<SimpleInteractable>();
				bool flag3 = !component;
				bool flag4 = !flag3;
				if (flag4)
				{
					component.OnAccept = this.OnAccept;
					component.OnDecline = this.OnDecline;
					component.CanUse = this.CanUse;
					component.text = this.text;
					component.acceptText = this.acceptText;
					component.declineText = this.declineText;
					Tools.Print<string>("Started shrine: " + text, "FFFFFF", false);
				}
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x0003E784 File Offset: 0x0003C984
			public void Copy(ShrineFactory.CustomShrineController other)
			{
				this.ID = other.ID;
				this.roomStyles = other.roomStyles;
				this.isBreachShrine = other.isBreachShrine;
				this.offset = other.offset;
				this.pixelColliders = other.pixelColliders;
				this.factory = other.factory;
				this.OnAccept = other.OnAccept;
				this.OnDecline = other.OnDecline;
				this.CanUse = other.CanUse;
				this.text = other.text;
				this.acceptText = other.acceptText;
				this.declineText = other.declineText;
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x0003E822 File Offset: 0x0003CA22
			public void ConfigureOnPlacement(RoomHandler room)
			{
				this.m_parentRoom = room;
				this.RegisterMinimapIcon();
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x0003E833 File Offset: 0x0003CA33
			public void RegisterMinimapIcon()
			{
				this.m_instanceMinimapIcon = Minimap.Instance.RegisterRoomIcon(this.m_parentRoom, (GameObject)BraveResources.Load("Global Prefabs/Minimap_Shrine_Icon", ".prefab"), false);
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x0003E864 File Offset: 0x0003CA64
			public void GetRidOfMinimapIcon()
			{
				bool flag = this.m_instanceMinimapIcon != null;
				bool flag2 = flag;
				if (flag2)
				{
					Minimap.Instance.DeregisterRoomIcon(this.m_parentRoom, this.m_instanceMinimapIcon);
					this.m_instanceMinimapIcon = null;
				}
			}

			// Token: 0x0400025E RID: 606
			public string ID;

			// Token: 0x0400025F RID: 607
			public bool isBreachShrine;

			// Token: 0x04000260 RID: 608
			public Vector3 offset;

			// Token: 0x04000261 RID: 609
			public List<PixelCollider> pixelColliders;

			// Token: 0x04000262 RID: 610
			public Dictionary<string, int> roomStyles;

			// Token: 0x04000263 RID: 611
			public ShrineFactory factory;

			// Token: 0x04000264 RID: 612
			public Action<PlayerController, GameObject> OnAccept;

			// Token: 0x04000265 RID: 613
			public Action<PlayerController, GameObject> OnDecline;

			// Token: 0x04000266 RID: 614
			public Func<PlayerController, GameObject, bool> CanUse;

			// Token: 0x04000267 RID: 615
			private RoomHandler m_parentRoom;

			// Token: 0x04000268 RID: 616
			private GameObject m_instanceMinimapIcon;

			// Token: 0x04000269 RID: 617
			public int numUses = 0;

			// Token: 0x0400026A RID: 618
			public string text;

			// Token: 0x0400026B RID: 619
			public string acceptText;

			// Token: 0x0400026C RID: 620
			public string declineText;
		}
	}
}
