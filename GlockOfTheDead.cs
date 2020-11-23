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
    public class GlockOfTheDead : PlayerItem
    {
        public float Random { get; private set; }
        public static void Init()
        {
            string itemName = "Glock Of The Dead";
            string resourceName = "BunnyMod/Resources/glockofthedead";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<GlockOfTheDead>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Rise from the grave!";
            string longDesc = "A Glock looted from the body of a gungeoneer, whos dead body still shambled around the Gungeon long after its death.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 15f);
            item.consumable = false;
            item.quality = PickupObject.ItemQuality.B;
            item.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:glock_of_the_dead",
                "zombie_bullets"
            };
            CustomSynergies.Add("Horde Of Many", mandatoryConsoleIDs1, null, true);
            List<string> mandatoryConsoleIDs2 = new List<string>
            {
                "bny:glock_of_the_dead",
            };
            List<string> optionalConsoleID2s = new List<string>
            {
                "fossilized_gun",
                "shelleton_key",
                "skull_spitter"
            };
            CustomSynergies.Add("Back in the Bones", mandatoryConsoleIDs2, optionalConsoleID2s, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }
        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_BOSS_agunim_volley_01", base.gameObject);
            {
                bool flag1 = user.HasPickupID(166);
                bool flag2 = user.HasPickupID(196);
                bool flag3 = user.HasPickupID(45);
                if (flag1)
                {
                    string guid;
                    guid = "336190e29e8a4f75ab7486595b700d4a";
                    PlayerController owner = base.LastOwner;
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                    IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                    AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                    aiactor.CanTargetEnemies = true;
                    aiactor.CanTargetPlayers = false;
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                    aiactor.gameObject.AddComponent<KillOnRoomClear>();
                    aiactor.IsHarmlessEnemy = true;
                    aiactor.IgnoreForRoomClear = true;
                    aiactor.HandleReinforcementFallIntoRoom(0f);
                }
                else
                if (flag2)
                {
                    string guid;
                    guid = "336190e29e8a4f75ab7486595b700d4a";
                    PlayerController owner = base.LastOwner;
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                    IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                    AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                    aiactor.CanTargetEnemies = true;
                    aiactor.CanTargetPlayers = false;
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                    aiactor.gameObject.AddComponent<KillOnRoomClear>();
                    aiactor.IsHarmlessEnemy = true;
                    aiactor.IgnoreForRoomClear = true;
                    aiactor.HandleReinforcementFallIntoRoom(0f);

                }
                else
                if (flag3)
                {
                    string guid;
                    guid = "336190e29e8a4f75ab7486595b700d4a";
                    PlayerController owner = base.LastOwner;
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                    IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                    AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                    aiactor.CanTargetEnemies = true;
                    aiactor.CanTargetPlayers = false;
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                    aiactor.gameObject.AddComponent<KillOnRoomClear>();
                    aiactor.IsHarmlessEnemy = true;
                    aiactor.IgnoreForRoomClear = true;
                    aiactor.HandleReinforcementFallIntoRoom(0f);

                }
                else
                {
                    string guid;
                    guid = "e21ac9492110493baef6df02a2682a0d";
                    PlayerController owner = base.LastOwner;
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                    IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                    AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                    aiactor.CanTargetEnemies = true;
                    aiactor.CanTargetPlayers = false;
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                    aiactor.gameObject.AddComponent<KillOnRoomClear>();
                    aiactor.IsHarmlessEnemy = true;
                    aiactor.IgnoreForRoomClear = true;
                    aiactor.HandleReinforcementFallIntoRoom(0f);
                }
            }
            {
                bool flag1 = user.HasPickupID(528);
                if (flag1)
                {
                    for (int counter = 0; counter < 2; counter++)
                    {
                        GameManager.Instance.StartCoroutine(Summon2ndSpent());
                    }
                }
                else
                {
                    GameManager.Instance.StartCoroutine(Summon2ndSpent());
                }
            }
        }
        private IEnumerator Summon2ndSpent()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.25f));
            {
                string guid;
                guid = "e21ac9492110493baef6df02a2682a0d";
                PlayerController owner = base.LastOwner;
                AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                IntVector2? intVector = new IntVector2?(base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                aiactor.CanTargetEnemies = true;
                aiactor.CanTargetPlayers = false;
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                aiactor.gameObject.AddComponent<KillOnRoomClear>();
                aiactor.IsHarmlessEnemy = true;
                aiactor.IgnoreForRoomClear = true;
                aiactor.HandleReinforcementFallIntoRoom(0f);
            }
            yield break;
        }
    }
}
