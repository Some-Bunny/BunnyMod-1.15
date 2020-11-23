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
    public class GungeonInvestment : PassiveItem
    {

        public static void Init()
        {
            string itemName = "The G.I.P";
            string resourceName = "BunnyMod/Resources/gungeonmonetaryprogram.png";
            GameObject obj = new GameObject(itemName);
            GungeonInvestment greandeParasite = obj.AddComponent<GungeonInvestment>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Invest, Adapt, Overcome";
            string longDesc = "This quick and easy investment program makes sure that every floor your balance grows. Though some money may be skimmed off the top.";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.quality = PickupObject.ItemQuality.C;
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:the_g.i.p"
            };
            List<string> optionalConsoleID1s = new List<string>
            {
                "gilded_hydra",
                "coin_crown",
                "gold_ammolet",
                "gilded_bullets",
                "gold_junk"
            };
            CustomSynergies.Add("$$ Cash Money $$", mandatoryConsoleIDs1, optionalConsoleID1s, true);
        }
        private void OnNewFloor()
        {
            //fuck you game
            PlayerController owner = base.Owner;
            bool flagA = owner.PlayerHasActiveSynergy("$$ Cash Money $$");
            if (flagA)
            {
                int num = 2 * (owner.carriedConsumables.Currency / 5);
                {
                    owner.carriedConsumables.Currency += num;
                }
            }
            else
            {
                int num = owner.carriedConsumables.Currency / 5;
                {
                    owner.carriedConsumables.Currency += num;
                }
            }
        }


        public override void Pickup(PlayerController player)
        {
            GameManager.Instance.OnNewLevelFullyLoaded += this.OnNewFloor;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            GameManager.Instance.OnNewLevelFullyLoaded -= this.OnNewFloor;
            return base.Drop(player);
        }
    }
}



