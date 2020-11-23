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
    public class TableTechBomb : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Table Tech Bomb";
            string resourceName = "BunnyMod/Resources/tabletechbomb.png";
            GameObject obj = new GameObject(itemName);
            TableTechBomb minigunrounds = obj.AddComponent<TableTechBomb>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Reactive Flips";
            string longDesc = "This ancient technique allows the user to create bombs by flipping tables.\n\n Chapter 8-1 of the Table Sutra. Those who imbue reaction into their flips shall project it onto the flipped.";
            minigunrounds.SetupItem(shortDesc, longDesc, "bny");
            minigunrounds.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            minigunrounds.quality = PickupObject.ItemQuality.C;
            List<string> mandatoryConsoleIDs14 = new List<string>
            {
                "bny:table_tech_bomb"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "air_strike",
                "proximity_mine",
                "big_boy",
                "portable_turret",
                "blast_helmet",
                "hegemony_carbine",
                "hegemony_rifle",
                "rc_rocket"
            };
            CustomSynergies.Add("Hidden Tech Landmine", mandatoryConsoleIDs14, optionalConsoleIDs, true);
        }

        public override void Pickup(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
        private void HandleFlip(FlippableCover table)
        {
            bool flag2 = base.Owner.PlayerHasActiveSynergy("Hidden Tech Landmine");
            if (flag2)
            {
                SpawnObjectPlayerItem component = PickupObjectDatabase.GetById(66).GetComponent<SpawnObjectPlayerItem>();
                GameObject gameObject = component.objectToSpawn.gameObject;
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, table.sprite.WorldBottomCenter, Quaternion.identity);
                tk2dBaseSprite component2 = gameObject2.GetComponent<tk2dBaseSprite>();
                bool flag3 = component2;
                if (flag3)
                {
                    component2.PlaceAtPositionByAnchor(table.sprite.WorldBottomCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                }
            }
            else
            {
                int num = 108;
                SpawnObjectPlayerItem component = PickupObjectDatabase.GetById(num).GetComponent<SpawnObjectPlayerItem>();
                GameObject gameObject = component.objectToSpawn.gameObject;
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, table.sprite.WorldBottomCenter, Quaternion.identity);
                tk2dBaseSprite component2 = gameObject2.GetComponent<tk2dBaseSprite>();
                bool flag4 = component2;
                if (flag4)
                {
                    component2.PlaceAtPositionByAnchor(table.sprite.WorldBottomCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                }
            }
        }

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Remove(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}