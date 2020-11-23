using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace ItemAPI
{
	// Token: 0x02000015 RID: 21
	public static class GunTools
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00007A00 File Offset: 0x00005C00
		public static tk2dSpriteDefinition CopyDefinitionFrom(this tk2dSpriteDefinition other)
		{
			tk2dSpriteDefinition tk2dSpriteDefinition = new tk2dSpriteDefinition
			{

				boundsDataCenter = new Vector3
				{
					x = other.boundsDataCenter.x,
					y = other.boundsDataCenter.y,
					z = other.boundsDataCenter.z
				},
				boundsDataExtents = new Vector3
				{
					x = other.boundsDataExtents.x,
					y = other.boundsDataExtents.y,
					z = other.boundsDataExtents.z
				},
				colliderConvex = other.colliderConvex,
				colliderSmoothSphereCollisions = other.colliderSmoothSphereCollisions,
				colliderType = other.colliderType,
				colliderVertices = other.colliderVertices,
				collisionLayer = other.collisionLayer,
				complexGeometry = other.complexGeometry,
				extractRegion = other.extractRegion,
				flipped = other.flipped,
				indices = other.indices,
				material = new Material(other.material),
				materialId = other.materialId,
				materialInst = new Material(other.materialInst),
				metadata = other.metadata,
				name = other.name,
				normals = other.normals,
				physicsEngine = other.physicsEngine,
				position0 = new Vector3
				{
					x = other.position0.x,
					y = other.position0.y,
					z = other.position0.z
				},
				position1 = new Vector3
				{
					x = other.position1.x,
					y = other.position1.y,
					z = other.position1.z
				},
				position2 = new Vector3
				{
					x = other.position2.x,
					y = other.position2.y,
					z = other.position2.z
				},
				position3 = new Vector3
				{
					x = other.position3.x,
					y = other.position3.y,
					z = other.position3.z
				},
				regionH = other.regionH,
				regionW = other.regionW,
				regionX = other.regionX,
				regionY = other.regionY,
				tangents = other.tangents,
				texelSize = new Vector2
				{
					x = other.texelSize.x,
					y = other.texelSize.y
				},
				untrimmedBoundsDataCenter = new Vector3
				{
					x = other.untrimmedBoundsDataCenter.x,
					y = other.untrimmedBoundsDataCenter.y,
					z = other.untrimmedBoundsDataCenter.z
				},
				untrimmedBoundsDataExtents = new Vector3
				{
					x = other.untrimmedBoundsDataExtents.x,
					y = other.untrimmedBoundsDataExtents.y,
					z = other.untrimmedBoundsDataExtents.z
				}
			};
			List<Vector2> list = new List<Vector2>();
			foreach (Vector2 vector in other.uvs)
			{
				list.Add(new Vector2
				{
					x = vector.x,
					y = vector.y
				});
			}
			tk2dSpriteDefinition.uvs = list.ToArray();
			List<Vector3> list2 = new List<Vector3>();
			foreach (Vector3 vector2 in other.colliderVertices)
			{
				list2.Add(new Vector3
				{
					x = vector2.x,
					y = vector2.y,
					z = vector2.z
				});
			}
			tk2dSpriteDefinition.colliderVertices = list2.ToArray();
			return tk2dSpriteDefinition;
		}
		public static void AddPassiveStatModifier(this Gun gun, PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod modifyMethod)
		{
			gun.passiveStatModifiers = gun.passiveStatModifiers.Concat(new StatModifier[]
			{
				new StatModifier
				{
					statToBoost = statType,
					amount = amount,
					modifyType = modifyMethod
				}
			}).ToArray<StatModifier>();
		}
		// Token: 0x06000097 RID: 151 RVA: 0x00007E60 File Offset: 0x00006060
		public static void SetProjectileSpriteRight(this Projectile proj, string name, int pixelWidth, int pixelHeight, bool lightened = true, tk2dBaseSprite.Anchor anchor = tk2dBaseSprite.Anchor.LowerLeft, int? overrideColliderPixelWidth = null, int? overrideColliderPixelHeight = null, int? overrideColliderOffsetX = null, int? overrideColliderOffsetY = null, Projectile overrideProjectileToCopyFrom = null)
		{
			try
			{
				ETGMod.GetAnySprite(proj).spriteId = ETGMod.Databases.Items.ProjectileCollection.inst.GetSpriteIdByName(name);
				tk2dSpriteDefinition tk2dSpriteDefinition = GunTools.SetupDefinitionForProjectileSprite(name, ETGMod.GetAnySprite(proj).spriteId, pixelWidth, pixelHeight, lightened, overrideColliderPixelWidth, overrideColliderPixelHeight, overrideColliderOffsetX, overrideColliderOffsetY, overrideProjectileToCopyFrom);
				tk2dSpriteDefinition.ConstructOffsetsFromAnchor(anchor, tk2dSpriteDefinition.position3);
			}
			catch (Exception ex)
			{
				ETGModConsole.Log("Ooops! Seems like something got very, Very, VERY wrong. Here's the exception:", false);
				ETGModConsole.Log(ex.ToString(), false);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007EF8 File Offset: 0x000060F8
		public static void MakeOffset(this tk2dSpriteDefinition def, Vector2 offset)
		{
			float x = offset.x;
			float y = offset.y;
			def.position0 += new Vector3(x, y, 0f);
			def.position1 += new Vector3(x, y, 0f);
			def.position2 += new Vector3(x, y, 0f);
			def.position3 += new Vector3(x, y, 0f);
			def.boundsDataCenter += new Vector3(x, y, 0f);
			def.boundsDataExtents += new Vector3(x, y, 0f);
			def.untrimmedBoundsDataCenter += new Vector3(x, y, 0f);
			def.untrimmedBoundsDataExtents += new Vector3(x, y, 0f);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007FFC File Offset: 0x000061FC
		public static void ConstructOffsetsFromAnchor(this tk2dSpriteDefinition def, tk2dBaseSprite.Anchor anchor, Vector2 scale)
		{
			float x = 0f;
			bool flag = anchor == tk2dBaseSprite.Anchor.LowerCenter || anchor == tk2dBaseSprite.Anchor.MiddleCenter || anchor == tk2dBaseSprite.Anchor.UpperCenter;
			if (flag)
			{
				x = -(scale.x / 2f);
			}
			else
			{
				bool flag2 = anchor == tk2dBaseSprite.Anchor.LowerRight || anchor == tk2dBaseSprite.Anchor.MiddleRight || anchor == tk2dBaseSprite.Anchor.UpperRight;
				if (flag2)
				{
					x = -scale.x;
				}
			}
			float y = 0f;
			bool flag3 = anchor == tk2dBaseSprite.Anchor.MiddleLeft || anchor == tk2dBaseSprite.Anchor.MiddleCenter || anchor == tk2dBaseSprite.Anchor.MiddleLeft;
			if (flag3)
			{
				y = -(scale.y / 2f);
			}
			else
			{
				bool flag4 = anchor == tk2dBaseSprite.Anchor.UpperLeft || anchor == tk2dBaseSprite.Anchor.UpperCenter || anchor == tk2dBaseSprite.Anchor.UpperRight;
				if (flag4)
				{
					y = -scale.y;
				}
			}
			def.MakeOffset(new Vector2(x, y));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000080AC File Offset: 0x000062AC
		public static tk2dSpriteDefinition SetupDefinitionForProjectileSprite(string name, int id, int pixelWidth, int pixelHeight, bool lightened = true, int? overrideColliderPixelWidth = null, int? overrideColliderPixelHeight = null, int? overrideColliderOffsetX = null, int? overrideColliderOffsetY = null, Projectile overrideProjectileToCopyFrom = null)
		{
			bool flag = overrideColliderPixelWidth == null;
			if (flag)
			{
				overrideColliderPixelWidth = new int?(pixelWidth);
			}
			bool flag2 = overrideColliderPixelHeight == null;
			if (flag2)
			{
				overrideColliderPixelHeight = new int?(pixelHeight);
			}
			bool flag3 = overrideColliderOffsetX == null;
			if (flag3)
			{
				overrideColliderOffsetX = new int?(0);
			}
			bool flag4 = overrideColliderOffsetY == null;
			if (flag4)
			{
				overrideColliderOffsetY = new int?(0);
			}
			float num = 14f;
			float num2 = 16f;
			float num3 = (float)pixelWidth / num;
			float num4 = (float)pixelHeight / num;
			float x = (float)overrideColliderPixelWidth.Value / num2;
			float y = (float)overrideColliderPixelHeight.Value / num2;
			float x2 = (float)overrideColliderOffsetX.Value / num2;
			float y2 = (float)overrideColliderOffsetY.Value / num2;
			tk2dSpriteDefinition tk2dSpriteDefinition = ETGMod.Databases.Items.ProjectileCollection.inst.spriteDefinitions[ETGMod.GetAnySprite(overrideProjectileToCopyFrom ?? (PickupObjectDatabase.GetById(lightened ? 47 : 12) as Gun).DefaultModule.projectiles[0]).spriteId].CopyDefinitionFrom();
			tk2dSpriteDefinition.boundsDataCenter = new Vector3(num3 / 2f, num4 / 2f, 0f);
			tk2dSpriteDefinition.boundsDataExtents = new Vector3(num3, num4, 0f);
			tk2dSpriteDefinition.untrimmedBoundsDataCenter = new Vector3(num3 / 2f, num4 / 2f, 0f);
			tk2dSpriteDefinition.untrimmedBoundsDataExtents = new Vector3(num3, num4, 0f);
			tk2dSpriteDefinition.texelSize = new Vector2(0.0625f, 0.0625f);
			tk2dSpriteDefinition.position0 = new Vector3(0f, 0f, 0f);
			tk2dSpriteDefinition.position1 = new Vector3(0f + num3, 0f, 0f);
			tk2dSpriteDefinition.position2 = new Vector3(0f, 0f + num4, 0f);
			tk2dSpriteDefinition.position3 = new Vector3(0f + num3, 0f + num4, 0f);
			tk2dSpriteDefinition.colliderVertices[0].x = x2;
			tk2dSpriteDefinition.colliderVertices[0].y = y2;
			tk2dSpriteDefinition.colliderVertices[1].x = x;
			tk2dSpriteDefinition.colliderVertices[1].y = y;
			tk2dSpriteDefinition.name = name;
			ETGMod.Databases.Items.ProjectileCollection.inst.spriteDefinitions[id] = tk2dSpriteDefinition;
			return tk2dSpriteDefinition;
		}
	}
}

