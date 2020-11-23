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
    public class TableTechReload : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Table Tech Reload";

            string resourceName = "BunnyMod/Resources/tabletechknife.png";

            GameObject obj = new GameObject(itemName);

            TableTechReload minigunrounds = obj.AddComponent<TableTechReload>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Fullfilling Flips";
            string longDesc = "This ancient technique allows the user to refill their weapon from table flipping.\n\nChapter 3, those who are fullfilled in their flip shall be refilled in kind.";

            minigunrounds.SetupItem(shortDesc, longDesc, "bny");
            minigunrounds.quality = PickupObject.ItemQuality.D;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            base.Pickup(player);
        }
        private void HandleFlip(FlippableCover table)
        {
            base.Owner.CurrentGun.ForceImmediateReload(true);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Remove(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            return base.Drop(player);
        }

    }
}