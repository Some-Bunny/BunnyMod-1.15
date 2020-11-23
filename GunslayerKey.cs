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
    public class SlayerKey : PlayerItem
    {
        private float random;

        public float Random { get; private set; }
        public static void Init()
        {
            string itemName = "Gunslayer Key";
            string resourceName = "BunnyMod/Resources/slayerkey";
            GameObject obj = new GameObject(itemName);
            SlayerKey slayerKey = obj.AddComponent<SlayerKey>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Clip and Bear";
            string longDesc = "A key made of bodies of powerful gundead. It is said that this key once belonged to the Gunslayer, who used it to combat great beasts for fun. To taunt them, he'd permanantly leave behind a piece of his arsenal.\n\n Noone knows where the Gunslayer is now, as he disappeared without a trace.";
            slayerKey.SetupItem(shortDesc, longDesc, "bny");
            slayerKey.SetCooldownType(ItemBuilder.CooldownType.Damage, 1250f);
            slayerKey.consumable = false;
            slayerKey.quality = PickupObject.ItemQuality.A;
            slayerKey.AddToSubShop(ItemBuilder.ShopType.Flynt, 1f);
        }
        public override void Pickup(PlayerController owner)
        {
            AkSoundEngine.PostEvent("Play_OBJ_goldkey_pickup_01", base.gameObject);
            base.Pickup(owner);
        }
        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_OBJ_goldenlock_open_01", base.gameObject);
            {
                this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                if (random <= 0.83f)
                {
                    for (int counter = 0; counter < 2; counter++)
                    {
                        int num3 = UnityEngine.Random.Range(0, 6);
                        bool flag3 = num3 == 0;
                        if (flag3)
                        {
                            string guid;
                            guid = "cd4a4b7f612a4ba9a720b9f97c52f38c";
                            PlayerController owner = base.LastOwner;
                            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                            IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            aiactor.CanTargetEnemies = false;
                            aiactor.CanTargetPlayers = true;
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                            aiactor.IsHarmlessEnemy = false;
                            aiactor.IgnoreForRoomClear = false;
                            aiactor.HandleReinforcementFallIntoRoom(0f);
                        }
                        bool flag4 = num3 == 1;
                        if (flag4)
                        {
                            string guid;
                            guid = "98ea2fe181ab4323ab6e9981955a9bca";
                            PlayerController owner = base.LastOwner;
                            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                            IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            aiactor.CanTargetEnemies = false;
                            aiactor.CanTargetPlayers = true;
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                            aiactor.IsHarmlessEnemy = false;
                            aiactor.IgnoreForRoomClear = false;
                            aiactor.HandleReinforcementFallIntoRoom(0f);
                        }
                        bool flag5 = num3 == 2;
                        if (flag5)
                        {
                            string guid;
                            guid = "383175a55879441d90933b5c4e60cf6f";
                            PlayerController owner = base.LastOwner;
                            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                            IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            aiactor.CanTargetEnemies = false;
                            aiactor.CanTargetPlayers = true;
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                            aiactor.IsHarmlessEnemy = false;
                            aiactor.IgnoreForRoomClear = false;
                            aiactor.HandleReinforcementFallIntoRoom(0f);
                        }
                        bool flag6 = num3 == 3;
                        if (flag6)
                        {
                            string guid;
                            guid = "d5a7b95774cd41f080e517bea07bf495";
                            PlayerController owner = base.LastOwner;
                            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                            IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            aiactor.CanTargetEnemies = false;
                            aiactor.CanTargetPlayers = true;
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                            aiactor.IsHarmlessEnemy = false;
                            aiactor.IgnoreForRoomClear = false;
                            aiactor.HandleReinforcementFallIntoRoom(0f);
                        }
                        bool flag7 = num3 == 4;
                        if (flag7)
                        {
                            string guid;
                            guid = "1bc2a07ef87741be90c37096910843ab";
                            PlayerController owner = base.LastOwner;
                            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                            IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            aiactor.CanTargetEnemies = false;
                            aiactor.CanTargetPlayers = true;
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                            aiactor.IsHarmlessEnemy = false;
                            aiactor.IgnoreForRoomClear = false;
                            aiactor.HandleReinforcementFallIntoRoom(0f);
                        }
                        bool flag8 = num3 == 5;
                        if (flag8)
                        {
                            string guid;
                            guid = "21dd14e5ca2a4a388adab5b11b69a1e1";
                            PlayerController owner = base.LastOwner;
                            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                            IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            aiactor.CanTargetEnemies = false;
                            aiactor.CanTargetPlayers = true;
                            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                            aiactor.IsHarmlessEnemy = false;
                            aiactor.IgnoreForRoomClear = false;
                            aiactor.HandleReinforcementFallIntoRoom(0f);
                        }
                    }
                }
                else
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, user);
                    IntVector2 bestRewardLocation = user.CurrentRoom.GetBestRewardLocation(IntVector2.One * 3, RoomHandler.RewardLocationStyle.PlayerCenter, true);
                    Chest chest2 = GameManager.Instance.RewardManager.SpawnRewardChestAt(bestRewardLocation, -1f, PickupObject.ItemQuality.EXCLUDED);
                    chest2.RegisterChestOnMinimap(chest2.GetAbsoluteParentRoom());
                }         
            }
        }
    }
}


