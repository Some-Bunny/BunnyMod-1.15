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
    internal class GuonPebble : IounStoneOrbitalItem
    {
        public static void Init()
        {
            string name = "Guon Pebble";
            string resourcePath = "BunnyMod/Resources/GuonPebble/guonpebble";
            GameObject gameObject = new GameObject();
            GuonPebble pebbleGuon = gameObject.AddComponent<GuonPebble>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
            string shortDesc = "smol";
            string longDesc = "A piece of the Guon Boulder that was lost due to the fact that the boulder was made by someone else. I just stole a piece.";
            pebbleGuon.SetupItem(shortDesc, longDesc, "bny");
            pebbleGuon.quality = PickupObject.ItemQuality.D;
            GuonPebble.BuildPrefab();
            pebbleGuon.OrbitalPrefab = GuonPebble.orbitalPrefab;
            pebbleGuon.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
        }

        public static void BuildPrefab()
        {
            bool flag = GuonPebble.orbitalPrefab != null;
            if (!flag)
            {
                GameObject gameObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/GuonPebble/guonpebblefloaty", null, true);
                gameObject.name = "its a rock! yeah";
                SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(10, 10));
                speculativeRigidbody.CollideWithTileMap = false;
                speculativeRigidbody.CollideWithOthers = true;
                speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
                GuonPebble.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
                GuonPebble.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
                GuonPebble.orbitalPrefab.shouldRotate = false;
                GuonPebble.orbitalPrefab.orbitRadius = 3.5f;
                GuonPebble.orbitalPrefab.orbitDegreesPerSecond = -30f;
                GuonPebble.orbitalPrefab.SetOrbitalTier(0);
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                FakePrefab.MarkAsFakePrefab(gameObject);
                gameObject.SetActive(false);
            }
        }
        public override void Pickup(PlayerController player)
        {
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Combine(player.OnNewFloorLoaded, new Action<PlayerController>(this.HandleNewFloor));
            base.Pickup(player);
        }

        private void HandleNewFloor(PlayerController obj)
        {

        }

        public override DebrisObject Drop(PlayerController player)
        {
            GuonPebble.speedUp = false;
            return base.Drop(player);
        }

        protected override void OnDestroy()
        {
            GuonPebble.speedUp = false;
            base.OnDestroy();
        }
        public static bool speedUp = false;
        public static PlayerOrbital orbitalPrefab;
        public List<IPlayerOrbital> orbitals = new List<IPlayerOrbital>();
    }
}
