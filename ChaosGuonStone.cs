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
    internal class ChaosGuonStone : IounStoneOrbitalItem
    {
        public static void Init()
        {
            string name = "Chaos Guon Stone";
            string resourcePath = "BunnyMod/Resources/MagnetGuonStone/magneticguonstone";
            GameObject gameObject = new GameObject();
            ChaosGuonStone chaosGuon = gameObject.AddComponent<ChaosGuonStone>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
            string shortDesc = "Unspecified Orbit";
            string longDesc = "A guon stone that does not adhere to normal guon stone principles. It decides it's own rotation and orbit every floor, just because it can.";
            chaosGuon.SetupItem(shortDesc, longDesc, "bny");
            chaosGuon.quality = PickupObject.ItemQuality.C;
            ChaosGuonStone.BuildPrefab();
            chaosGuon.OrbitalPrefab = ChaosGuonStone.orbitalPrefab;
            chaosGuon.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
        }

        public static void BuildPrefab()
        {
            bool flag = ChaosGuonStone.orbitalPrefab != null;
            if (!flag)
            {
                GameObject gameObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/MagnetGuonStone/magnetguonstonefloaty", null, true);
                gameObject.name = "Chaos Guon Orbital";
                SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(10, 10));
                speculativeRigidbody.CollideWithTileMap = false;
                speculativeRigidbody.CollideWithOthers = true;
                speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
                ChaosGuonStone.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
                ChaosGuonStone.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
                ChaosGuonStone.orbitalPrefab.shouldRotate = false;
                ChaosGuonStone.orbitalPrefab.orbitRadius = UnityEngine.Random.Range(5.5f, 1.5f);
                ChaosGuonStone.orbitalPrefab.orbitDegreesPerSecond = UnityEngine.Random.Range(120f, 20f);
                ChaosGuonStone.orbitalPrefab.SetOrbitalTier(0);
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                FakePrefab.MarkAsFakePrefab(gameObject);
                gameObject.SetActive(false);
            }
        }
        public override void Pickup(PlayerController player)
        {
            foreach (IPlayerOrbital fuck in player.orbitals)
            {
                PlayerOrbital m_extantOrbital = (PlayerOrbital)fuck;
                m_extantOrbital.orbitDegreesPerSecond = UnityEngine.Random.Range(120f, 20f);
                m_extantOrbital.orbitRadius = UnityEngine.Random.Range(7f, 2f);
            }
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Combine(player.OnNewFloorLoaded, new Action<PlayerController>(this.HandleNewFloor));
            base.Pickup(player);
        }

        private void HandleNewFloor(PlayerController obj)
        {
            foreach (IPlayerOrbital fuck in obj.orbitals)
            {
                PlayerOrbital m_extantOrbital = (PlayerOrbital)fuck;
                m_extantOrbital.orbitDegreesPerSecond = UnityEngine.Random.Range(120f, 20f);
                m_extantOrbital.orbitRadius = UnityEngine.Random.Range(7f, 2f);
            }
        }

        public override DebrisObject Drop(PlayerController player)
        {
            ChaosGuonStone.speedUp = false;
            return base.Drop(player);
        }

        protected override void OnDestroy()
        {
            ChaosGuonStone.speedUp = false;
            base.OnDestroy();
        }
        public static bool speedUp = false;
        public static PlayerOrbital orbitalPrefab;
        public List<IPlayerOrbital> orbitals = new List<IPlayerOrbital>();
    }
}
