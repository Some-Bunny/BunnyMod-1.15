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
    public class GunslayerHelmet : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Gunslayers Helmet";
            string resourceName = "BunnyMod/Resources/gunslayershelmet";
            GameObject obj = new GameObject(itemName);
            GunslayerHelmet gunslayerHelmet = obj.AddComponent<GunslayerHelmet>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "A haunting memory";
            string longDesc = "The helmet of the Gunslayer, discarded on his 700th clear of the Gungeon.\n\n" +
                "Although the Gunslayer is gone, the memory of his crusade still lives in the warning tales of the Gundead.";
            gunslayerHelmet.SetupItem(shortDesc, longDesc, "bny");
            gunslayerHelmet.AddPassiveStatModifier(PlayerStats.StatType.Coolness, 1f, StatModifier.ModifyMethod.ADDITIVE);
            gunslayerHelmet.AddPassiveStatModifier(PlayerStats.StatType.GlobalPriceMultiplier, .95f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            gunslayerHelmet.quality = PickupObject.ItemQuality.A;
            gunslayerHelmet.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
        }
        private void Ohfuckitstheslayer()
        {
            RoomHandler currentRoom = Owner.CurrentRoom;
            bool flag2 = currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All);
            if (flag2)
            {
                foreach (AIActor aiactor in currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                {
                    bool flag3 = aiactor.behaviorSpeculator != null;
                    if (flag3)
                    {
                        aiactor.behaviorSpeculator.FleePlayerData = GunslayerHelmet.fleeData;
                        FleePlayerData fleePlayerData = new FleePlayerData();
                        GameManager.Instance.StartCoroutine(GunslayerHelmet.ohshitthatsnottheslayer(aiactor));
                    }
                }

            }
        }
        private static IEnumerator ohshitthatsnottheslayer(AIActor aiactor)
        {
            yield return new WaitForSeconds(10f);
            aiactor.behaviorSpeculator.FleePlayerData = null;
            yield break;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnEnteredCombat += (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.Ohfuckitstheslayer));
            GunslayerHelmet.fleeData = new FleePlayerData();
            GunslayerHelmet.fleeData.Player = player;
            GunslayerHelmet.fleeData.StartDistance = 100f;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnEnteredCombat -= (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.Ohfuckitstheslayer));
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
        private static FleePlayerData fleeData;
    }
}
