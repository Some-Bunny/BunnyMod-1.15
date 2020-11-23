using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ItemAPI
{
	// Token: 0x0200000A RID: 10
	public static class SpriteBuilder
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003E4C File Offset: 0x0000204C
		public static GameObject SpriteFromFile(string spriteName, GameObject obj = null)
		{
			string fileName = spriteName.Replace(".png", "");
			Texture2D textureFromFile = ResourceExtractor.GetTextureFromFile(fileName, ".png");
			bool flag = textureFromFile == null;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = SpriteBuilder.SpriteFromTexture(textureFromFile, spriteName, obj);
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003E94 File Offset: 0x00002094
		public static GameObject SpriteFromResource(string spriteName, GameObject obj = null, bool v = false)
		{
			string str = (!spriteName.EndsWith(".png")) ? ".png" : "";
			string text = spriteName + str;
			Texture2D textureFromResource = ResourceExtractor.GetTextureFromResource(text);
			bool flag = textureFromResource == null;
			GameObject result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = SpriteBuilder.SpriteFromTexture(textureFromResource, text, obj);
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003EEC File Offset: 0x000020EC
		public static GameObject SpriteFromTexture(Texture2D texture, string spriteName, GameObject obj = null)
		{
			bool flag = obj == null;
			if (flag)
			{
				obj = new GameObject();
			}
			tk2dSprite tk2dSprite = obj.AddComponent<tk2dSprite>();
			int newSpriteId = SpriteBuilder.AddSpriteToCollection(spriteName, SpriteBuilder.itemCollection);
			tk2dSprite.SetSprite(SpriteBuilder.itemCollection, newSpriteId);
			tk2dSprite.SortingOrder = 0;
			tk2dSprite.IsPerpendicular = true;
			obj.GetComponent<BraveBehaviour>().sprite = tk2dSprite;
			return obj;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003F50 File Offset: 0x00002150
		public static int AddSpriteToCollection(string resourcePath, tk2dSpriteCollectionData collection)
		{
			string str = (!resourcePath.EndsWith(".png")) ? ".png" : "";
			resourcePath += str;
			Texture2D textureFromResource = ResourceExtractor.GetTextureFromResource(resourcePath);
			tk2dSpriteDefinition tk2dSpriteDefinition = SpriteBuilder.ConstructDefinition(textureFromResource);
			tk2dSpriteDefinition.name = textureFromResource.name;
			return SpriteBuilder.AddSpriteToCollection(tk2dSpriteDefinition, collection);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003FA8 File Offset: 0x000021A8
		public static int AddSpriteToCollection(tk2dSpriteDefinition spriteDefinition, tk2dSpriteCollectionData collection)
		{
			tk2dSpriteDefinition[] spriteDefinitions = collection.spriteDefinitions;
			tk2dSpriteDefinition[] array = spriteDefinitions.Concat(new tk2dSpriteDefinition[]
			{
				spriteDefinition
			}).ToArray<tk2dSpriteDefinition>();
			collection.spriteDefinitions = array;
			FieldInfo field = typeof(tk2dSpriteCollectionData).GetField("spriteNameLookupDict", BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(collection, null);
			collection.InitDictionary();
			return array.Length - 1;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000400C File Offset: 0x0000220C
		public static int AddToAmmonomicon(tk2dSpriteDefinition spriteDefinition)
		{
			return SpriteBuilder.AddSpriteToCollection(spriteDefinition, SpriteBuilder.ammonomiconCollection);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000402C File Offset: 0x0000222C
		public static tk2dSpriteAnimationClip AddAnimation(tk2dSpriteAnimator animator, tk2dSpriteCollectionData collection, List<int> spriteIDs, string clipName, tk2dSpriteAnimationClip.WrapMode wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop)
		{
			bool flag = animator.Library == null;
			if (flag)
			{
				animator.Library = animator.gameObject.AddComponent<tk2dSpriteAnimation>();
				animator.Library.clips = new tk2dSpriteAnimationClip[0];
				animator.Library.enabled = true;
			}
			List<tk2dSpriteAnimationFrame> list = new List<tk2dSpriteAnimationFrame>();
			for (int i = 0; i < spriteIDs.Count; i++)
			{
				tk2dSpriteDefinition tk2dSpriteDefinition = collection.spriteDefinitions[spriteIDs[i]];
				bool valid = tk2dSpriteDefinition.Valid;
				if (valid)
				{
					list.Add(new tk2dSpriteAnimationFrame
					{
						spriteCollection = collection,
						spriteId = spriteIDs[i]
					});
				}
			}
			tk2dSpriteAnimationClip tk2dSpriteAnimationClip = new tk2dSpriteAnimationClip
			{
				name = clipName,
				fps = 15f,
				wrapMode = wrapMode
			};
			Array.Resize<tk2dSpriteAnimationClip>(ref animator.Library.clips, animator.Library.clips.Length + 1);
			animator.Library.clips[animator.Library.clips.Length - 1] = tk2dSpriteAnimationClip;
			tk2dSpriteAnimationClip.frames = list.ToArray();
			return tk2dSpriteAnimationClip;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000414C File Offset: 0x0000234C
		public static SpeculativeRigidbody SetUpSpeculativeRigidbody(this tk2dSprite sprite, IntVector2 offset, IntVector2 dimensions)
		{
			SpeculativeRigidbody orAddComponent = sprite.gameObject.GetOrAddComponent<SpeculativeRigidbody>();
			PixelCollider pixelCollider = new PixelCollider();
			pixelCollider.ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual;
			pixelCollider.CollisionLayer = CollisionLayer.EnemyCollider;
			pixelCollider.ManualWidth = dimensions.x;
			pixelCollider.ManualHeight = dimensions.y;
			pixelCollider.ManualOffsetX = offset.x;
			pixelCollider.ManualOffsetY = offset.y;
			orAddComponent.PixelColliders = new List<PixelCollider>
			{
				pixelCollider
			};
			return orAddComponent;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000041C4 File Offset: 0x000023C4
		public static tk2dSpriteDefinition ConstructDefinition(Texture2D texture)
		{
			RuntimeAtlasSegment runtimeAtlasSegment = ETGMod.Assets.Packer.Pack(texture, false);
			Material material = new Material(ShaderCache.Acquire(PlayerController.DefaultShaderName));
			material.mainTexture = runtimeAtlasSegment.texture;
			int width = texture.width;
			int height = texture.height;
			float num = 0f;
			float num2 = 0f;
			float num3 = (float)width / 16f;
			float num4 = (float)height / 16f;
			tk2dSpriteDefinition tk2dSpriteDefinition = new tk2dSpriteDefinition
			{
				normals = new Vector3[]
				{
					new Vector3(0f, 0f, -1f),
					new Vector3(0f, 0f, -1f),
					new Vector3(0f, 0f, -1f),
					new Vector3(0f, 0f, -1f)
				},
				tangents = new Vector4[]
				{
					new Vector4(1f, 0f, 0f, 1f),
					new Vector4(1f, 0f, 0f, 1f),
					new Vector4(1f, 0f, 0f, 1f),
					new Vector4(1f, 0f, 0f, 1f)
				},
				texelSize = new Vector2(0.0625f, 0.0625f),
				extractRegion = false,
				regionX = 0,
				regionY = 0,
				regionW = 0,
				regionH = 0,
				flipped = tk2dSpriteDefinition.FlipMode.None,
				complexGeometry = false,
				physicsEngine = tk2dSpriteDefinition.PhysicsEngine.Physics3D,
				colliderType = tk2dSpriteDefinition.ColliderType.None,
				collisionLayer = CollisionLayer.HighObstacle,
				position0 = new Vector3(num, num2, 0f),
				position1 = new Vector3(num + num3, num2, 0f),
				position2 = new Vector3(num, num2 + num4, 0f),
				position3 = new Vector3(num + num3, num2 + num4, 0f),
				material = material,
				materialInst = material,
				materialId = 0,
				uvs = runtimeAtlasSegment.uvs,
				boundsDataCenter = new Vector3(num3 / 2f, num4 / 2f, 0f),
				boundsDataExtents = new Vector3(num3, num4, 0f),
				untrimmedBoundsDataCenter = new Vector3(num3 / 2f, num4 / 2f, 0f),
				untrimmedBoundsDataExtents = new Vector3(num3, num4, 0f)
			};
			tk2dSpriteDefinition.name = texture.name;
			return tk2dSpriteDefinition;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000044AC File Offset: 0x000026AC
		public static tk2dSpriteCollectionData ConstructCollection(GameObject obj, string name)
		{
			tk2dSpriteCollectionData tk2dSpriteCollectionData = obj.AddComponent<tk2dSpriteCollectionData>();
			UnityEngine.Object.DontDestroyOnLoad(tk2dSpriteCollectionData);
			tk2dSpriteCollectionData.assetName = name;
			tk2dSpriteCollectionData.spriteCollectionGUID = name;
			tk2dSpriteCollectionData.spriteCollectionName = name;
			tk2dSpriteCollectionData.spriteDefinitions = new tk2dSpriteDefinition[0];
			return tk2dSpriteCollectionData;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000044F0 File Offset: 0x000026F0
		public static T CopyFrom<T>(this Component comp, T other) where T : Component
		{
			Type type = comp.GetType();
			bool flag = type != other.GetType();
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				PropertyInfo[] properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					bool canWrite = propertyInfo.CanWrite;
					if (canWrite)
					{
						try
						{
							propertyInfo.SetValue(comp, propertyInfo.GetValue(other, null), null);
						}
						catch
						{
						}
					}
				}
				FieldInfo[] fields = type.GetFields();
				foreach (FieldInfo fieldInfo in fields)
				{
					fieldInfo.SetValue(comp, fieldInfo.GetValue(other));
				}
				result = (comp as T);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000045E4 File Offset: 0x000027E4
		public static void SetColor(this tk2dSprite sprite, Color color)
		{
			sprite.renderer.material.SetColor("_OverrideColor", color);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004600 File Offset: 0x00002800
		public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
		{
			return go.AddComponent<T>().CopyFrom(toAdd);
		}

		// Token: 0x0400000C RID: 12
		public static tk2dSpriteCollectionData itemCollection = PickupObjectDatabase.GetByEncounterName("singularity").sprite.Collection;

		// Token: 0x0400000D RID: 13
		private static tk2dSpriteCollectionData ammonomiconCollection = AmmonomiconController.ForceInstance.EncounterIconCollection;

		// Token: 0x0400000E RID: 14
		public static tk2dSprite baseSprite = PickupObjectDatabase.GetByEncounterName("singularity").GetComponent<tk2dSprite>();
	}
}
