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



namespace BunnyMod
{
    public class ClayCordStatue : CompanionItem
    {
		public static GameObject prefab;

		public static void ClayBuildPrefab()
		{
			bool flag = ClayCordStatue.prefab != null || CompanionBuilder.companionDictionary.ContainsKey(ClayCordStatue.guidclay);
			if (!flag)
			{
				ClayCordStatue.prefab = CompanionBuilder.BuildPrefab("claystatue", ClayCordStatue.guidclay, "BunnyMod/Resources/claycordstatue_idle_001.png", new IntVector2(1, 0), new IntVector2(9, 9));
				CompanionController companionController = ClayCordStatue.prefab.AddComponent<CompanionController>();
				companionController.aiActor.MovementSpeed = 0f;
				ClayCordStatue.prefab.AddAnimation("idle_right", "BunnyMod/Resources/claycordstatue_idle_001.png", 1, CompanionBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal, DirectionalAnimation.FlipType.None);
				ClayCordStatue.prefab.AddAnimation("idle_left", "BunnyMod/Resources/claycordstatue_idle_001.png", 1, CompanionBuilder.AnimationType.Idle, DirectionalAnimation.DirectionType.TwoWayHorizontal, DirectionalAnimation.FlipType.None);
				companionController.CanInterceptBullets = true;
				companionController.aiActor.healthHaver.PreventAllDamage = false;
				companionController.aiActor.specRigidbody.CollideWithOthers = true;
				companionController.aiActor.specRigidbody.CollideWithTileMap = false;
				companionController.aiActor.healthHaver.ForceSetCurrentHealth(30f);
				companionController.aiActor.healthHaver.SetHealthMaximum(30f, null, false);
				companionController.aiActor.specRigidbody.PixelColliders.Clear();
				companionController.aiActor.specRigidbody.PixelColliders.Add(new PixelCollider
				{
					ColliderGenerationMode = PixelCollider.PixelColliderGeneration.Manual,
					CollisionLayer = CollisionLayer.BulletBlocker,
					IsTrigger = false,
					BagleUseFirstFrameOnly = false,
					SpecifyBagelFrame = string.Empty,
					BagelColliderNumber = 0,
					ManualOffsetX = 0,
					ManualOffsetY = 0,
					ManualWidth = 16,
					ManualHeight = 16,
					ManualDiameter = 0,
					ManualLeftX = 0,
					ManualLeftY = 0,
					ManualRightX = 0,
					ManualRightY = 0
				});
			}
		}
		public static string guidclay = "asdopkijdsaoikj;adsoasdoiasoiadsoiaoioasde";
	}
}
