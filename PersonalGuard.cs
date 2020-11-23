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
    public class PersonalGuard : PlayerItem
    {

        public float Random { get; private set; }
        public static void Init()
        {
            string itemName = "Personal Guard";
            string resourceName = "BunnyMod/Resources/personalguard";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<PersonalGuard>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Reformed Knights";
            string longDesc = "An unlikely union between gungeoneer and gun knights.\n\n Use to call in extra support. Watch for friendly fire.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 850f);
            item.consumable = false;
            item.quality = PickupObject.ItemQuality.C;
            item.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:personal_guard"
            };
            List<string> optionalConsoleID1s = new List<string>
            {
                "shotgun_full_of_hate",
                "fightsabre",
                "skull_spitter",
                "vorpal_gun",
                "ghost_bullets",
                "shadow_bullets",
                "vorpal_bullets"
            };
            CustomSynergies.Add("Protector From The Curtain", mandatoryConsoleIDs1, optionalConsoleID1s, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            bool flag1 = user.HasPickupID(143);
            bool flag2 = user.HasPickupID(345);
            bool flag3 = user.HasPickupID(45);
            bool flag4 = user.HasPickupID(537);
            bool flag5 = user.HasPickupID(172);
            bool flag6 = user.HasPickupID(352);
            bool flag7 = user.HasPickupID(640);
            if (flag1)
            {
                this.SpawnSpectralLad(user);
            }
            else
            if (flag2)
            {
                this.SpawnSpectralLad(user);
            }
            else
            if (flag3)
            {
                this.SpawnSpectralLad(user);
            }
            else
            if (flag4)
            {
                this.SpawnSpectralLad(user);
            }
            else
            if (flag5)
            {
                this.SpawnSpectralLad(user);
            }
            else
            if (flag6)
            {
                this.SpawnSpectralLad(user);
            }
            else
            if (flag7)
            {
                this.SpawnSpectralLad(user);
            }
            else
            {
                string guid;
                guid = "ec8ea75b557d4e7b8ceeaacdf6f8238c";
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
        private void SpawnSpectralLad(PlayerController user)
        {
            string guid;
            guid = "383175a55879441d90933b5c4e60cf6f";
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
}
