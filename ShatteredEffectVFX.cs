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
	public class ShatterEffect
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0001BB3C File Offset: 0x00019D3C
		public static void Initialise()
		{
			ShatterEffect.ShatterVFXObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/EffectIcons/shattered_debuff_icon.png", new GameObject("ShatterIcon"), true);
			ShatterEffect.ShatterVFXObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(ShatterEffect.ShatterVFXObject);
			UnityEngine.Object.DontDestroyOnLoad(ShatterEffect.ShatterVFXObject);
			tk2dSpriteAnimator tk2dSpriteAnimator = ShatterEffect.ShatterVFXObject.AddComponent<tk2dSpriteAnimator>();
			tk2dSpriteAnimator.Library = ShatterEffect.ShatterVFXObject.AddComponent<tk2dSpriteAnimation>();
			tk2dSpriteAnimator.Library.clips = new tk2dSpriteAnimationClip[0];
			tk2dSpriteAnimationClip tk2dSpriteAnimationClip = new tk2dSpriteAnimationClip
			{
				name = "ShatterIcon",
				fps = 1f,
				frames = new tk2dSpriteAnimationFrame[0]
			};
			foreach (string resourcePath in ShatterEffect.LockdownPaths)
			{
				int spriteId = SpriteBuilder.AddSpriteToCollection(resourcePath, ShatterEffect.ShatterVFXObject.GetComponent<tk2dBaseSprite>().Collection);
				tk2dSpriteAnimationFrame tk2dSpriteAnimationFrame = new tk2dSpriteAnimationFrame
				{
					spriteId = spriteId,
					spriteCollection = ShatterEffect.ShatterVFXObject.GetComponent<tk2dBaseSprite>().Collection
				};
				tk2dSpriteAnimationClip.frames = tk2dSpriteAnimationClip.frames.Concat(new tk2dSpriteAnimationFrame[]
				{
					tk2dSpriteAnimationFrame
				}).ToArray<tk2dSpriteAnimationFrame>();
			}
			tk2dSpriteAnimator.Library.clips = tk2dSpriteAnimator.Library.clips.Concat(new tk2dSpriteAnimationClip[]
			{
				tk2dSpriteAnimationClip
			}).ToArray<tk2dSpriteAnimationClip>();
			tk2dSpriteAnimator.playAutomatically = true;
			tk2dSpriteAnimator.DefaultClipId = tk2dSpriteAnimator.GetClipIdByName("ShatterIcon");
		}

		// Token: 0x040000FE RID: 254
		public static List<string> LockdownPaths = new List<string>
		{
			"BunnyMod/Resources/EffectIcons/shattered_debuff_icon.png"
		};

		// Token: 0x040000FF RID: 255
		public static GameObject ShatterVFXObject;
	}
}



