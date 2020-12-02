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
    public class Dejammer : PlayerItem
    {

        public static void Init()
        {
            string itemName = "De-Jammer";
            string resourceName = "BunnyMod/Resources/dejammer";
            GameObject obj = new GameObject(itemName);
            Dejammer lockpicker = obj.AddComponent<Dejammer>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "I'll be undamned!";
            string longDesc = "A strange device that purifies enemies. Nothing much to it. Press a button and you're done.";
            lockpicker.SetupItem(shortDesc, longDesc, "bny");
            lockpicker.SetCooldownType(ItemBuilder.CooldownType.PerRoom, 5f);
            lockpicker.consumable = false;
            lockpicker.quality = PickupObject.ItemQuality.B;
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }
        public override bool CanBeUsed(PlayerController user)
        {
            List<AIActor> activeEnemies = user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (activeEnemies != null)
            {
                int count = activeEnemies.Count;
                for (int i = 0; i < count; i++)
                {
                    if (activeEnemies[i] && activeEnemies[i].HasBeenEngaged && activeEnemies[i].healthHaver && activeEnemies[i].IsNormalEnemy && !activeEnemies[i].healthHaver.IsDead && !activeEnemies[i].healthHaver.IsBoss && !activeEnemies[i].IsTransmogrified && activeEnemies[i].IsBlackPhantom)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_WPN_blasterbow_shot_01", gameObject);

            RoomHandler currentRoom = user.CurrentRoom;
            bool flag2 = currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (flag2)
            {
                foreach (AIActor aiactor in currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                {
                    bool flag3 = aiactor.behaviorSpeculator != null;
                    if (flag3)
                    {
                        bool isBlackPhantom = aiactor.IsBlackPhantom;
                        if (isBlackPhantom)
                        {
                            aiactor.UnbecomeBlackPhantom();
                        }
                    }
                }
            }
        }
    }
}



