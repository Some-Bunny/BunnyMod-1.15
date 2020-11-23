using System;
using System.Collections.Generic;
using System.Linq;
using ItemAPI;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x02000007 RID: 7
	public static class NPCBuilder
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000037C8 File Offset: 0x000019C8
		public static tk2dSpriteAnimationClip AddAnimation(this GameObject obj, string name, string spriteDirectory, int fps, NPCBuilder.AnimationType type, DirectionalAnimation.DirectionType directionType = DirectionalAnimation.DirectionType.None, DirectionalAnimation.FlipType flipType = DirectionalAnimation.FlipType.None)
		{
			obj.AddComponent<tk2dSpriteAnimator>();
			AIAnimator aianimator = obj.GetComponent<AIAnimator>();
			bool flag = !aianimator;
			bool flag2 = flag;
			if (flag2)
			{
				aianimator = NPCBuilder.CreateNewAIAnimator(obj);
			}
			DirectionalAnimation directionalAnimation = aianimator.GetDirectionalAnimation(name, directionType, type);
			bool flag3 = directionalAnimation == null;
			bool flag4 = flag3;
			if (flag4)
			{
				directionalAnimation = new DirectionalAnimation
				{
					AnimNames = new string[0],
					Flipped = new DirectionalAnimation.FlipType[0],
					Type = directionType,
					Prefix = string.Empty
				};
			}
			directionalAnimation.AnimNames = directionalAnimation.AnimNames.Concat(new string[]
			{
				name
			}).ToArray<string>();
			directionalAnimation.Flipped = directionalAnimation.Flipped.Concat(new DirectionalAnimation.FlipType[]
			{
				flipType
			}).ToArray<DirectionalAnimation.FlipType>();
			aianimator.AssignDirectionalAnimation(name, directionalAnimation, type);
			return NPCBuilder.BuildAnimation(aianimator, name, spriteDirectory, fps);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000038AC File Offset: 0x00001AAC
		private static AIAnimator CreateNewAIAnimator(GameObject obj)
		{
			AIAnimator aianimator = obj.AddComponent<AIAnimator>();
			aianimator.FlightAnimation = NPCBuilder.CreateNewDirectionalAnimation();
			aianimator.HitAnimation = NPCBuilder.CreateNewDirectionalAnimation();
			aianimator.IdleAnimation = NPCBuilder.CreateNewDirectionalAnimation();
			aianimator.TalkAnimation = NPCBuilder.CreateNewDirectionalAnimation();
			aianimator.MoveAnimation = NPCBuilder.CreateNewDirectionalAnimation();
			aianimator.OtherAnimations = new List<AIAnimator.NamedDirectionalAnimation>();
			aianimator.IdleFidgetAnimations = new List<DirectionalAnimation>();
			aianimator.OtherVFX = new List<AIAnimator.NamedVFXPool>();
			return aianimator;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003920 File Offset: 0x00001B20
		private static DirectionalAnimation CreateNewDirectionalAnimation()
		{
			return new DirectionalAnimation
			{
				AnimNames = new string[0],
				Flipped = new DirectionalAnimation.FlipType[0],
				Type = DirectionalAnimation.DirectionType.None
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003958 File Offset: 0x00001B58
		public static tk2dSpriteAnimationClip BuildAnimation(AIAnimator aiAnimator, string name, string spriteDirectory, int fps)
		{
			tk2dSpriteCollectionData tk2dSpriteCollectionData = aiAnimator.GetComponent<tk2dSpriteCollectionData>();
			bool flag = !tk2dSpriteCollectionData;
			bool flag2 = flag;
			if (flag2)
			{
				tk2dSpriteCollectionData = SpriteBuilder.ConstructCollection(aiAnimator.gameObject, aiAnimator.name + "_collection");
			}
			string[] resourceNames = ResourceExtractor.GetResourceNames();
			List<int> list = new List<int>();
			for (int i = 0; i < resourceNames.Length; i++)
			{
				bool flag3 = resourceNames[i].StartsWith(spriteDirectory.Replace('/', '.'), StringComparison.OrdinalIgnoreCase);
				bool flag4 = flag3;
				if (flag4)
				{
					list.Add(SpriteBuilder.AddSpriteToCollection(resourceNames[i], tk2dSpriteCollectionData));
				}
			}
			bool flag5 = list.Count == 0;
			bool flag6 = flag5;
			if (flag6)
			{
				Tools.PrintError<string>("No sprites found for animation " + name, "FF0000");
			}
			tk2dSpriteAnimationClip tk2dSpriteAnimationClip = SpriteBuilder.AddAnimation(aiAnimator.spriteAnimator, tk2dSpriteCollectionData, list, name, tk2dSpriteAnimationClip.WrapMode.Loop);
			tk2dSpriteAnimationClip.fps = (float)fps;
			return tk2dSpriteAnimationClip;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003A40 File Offset: 0x00001C40
		public static DirectionalAnimation GetDirectionalAnimation(this AIAnimator aiAnimator, string name, DirectionalAnimation.DirectionType directionType, NPCBuilder.AnimationType type)
		{
			DirectionalAnimation directionalAnimation = null;
			switch (type)
			{
				case NPCBuilder.AnimationType.Move:
					directionalAnimation = aiAnimator.MoveAnimation;
					break;
				case NPCBuilder.AnimationType.Idle:
					directionalAnimation = aiAnimator.IdleAnimation;
					break;
				case NPCBuilder.AnimationType.Flight:
					directionalAnimation = aiAnimator.FlightAnimation;
					break;
				case NPCBuilder.AnimationType.Hit:
					directionalAnimation = aiAnimator.HitAnimation;
					break;
				case NPCBuilder.AnimationType.Talk:
					directionalAnimation = aiAnimator.TalkAnimation;
					break;
			}
			bool flag = directionalAnimation != null;
			bool flag2 = flag;
			DirectionalAnimation result;
			if (flag2)
			{
				result = directionalAnimation;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public static void AssignDirectionalAnimation(this AIAnimator aiAnimator, string name, DirectionalAnimation animation, NPCBuilder.AnimationType type)
		{
			switch (type)
			{
				case NPCBuilder.AnimationType.Move:
					aiAnimator.MoveAnimation = animation;
					break;
				case NPCBuilder.AnimationType.Idle:
					aiAnimator.IdleAnimation = animation;
					break;
				case NPCBuilder.AnimationType.Fidget:
					aiAnimator.IdleFidgetAnimations.Add(animation);
					break;
				case NPCBuilder.AnimationType.Flight:
					aiAnimator.FlightAnimation = animation;
					break;
				case NPCBuilder.AnimationType.Hit:
					aiAnimator.HitAnimation = animation;
					break;
				case NPCBuilder.AnimationType.Talk:
					aiAnimator.TalkAnimation = animation;
					break;
				default:
					aiAnimator.OtherAnimations.Add(new AIAnimator.NamedDirectionalAnimation
					{
						anim = animation,
						name = name
					});
					break;
			}
		}

		// Token: 0x0200012E RID: 302
		public enum AnimationType
		{
			// Token: 0x04000241 RID: 577
			Move,
			// Token: 0x04000242 RID: 578
			Idle,
			// Token: 0x04000243 RID: 579
			Fidget,
			// Token: 0x04000244 RID: 580
			Flight,
			// Token: 0x04000245 RID: 581
			Hit,
			// Token: 0x04000246 RID: 582
			Talk,
			// Token: 0x04000247 RID: 583
			Other
		}
	}
}
