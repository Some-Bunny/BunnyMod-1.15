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
using System.Timers;



namespace BunnyMod
{
    public class BloodyTrigger : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Bloody Trigger";
            string resourceName = "BunnyMod/Resources/bloodytrigger";
            GameObject obj = new GameObject(itemName);
            BloodyTrigger bloodyTrigger = obj.AddComponent<BloodyTrigger>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Bloodlust";
            string longDesc = "Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! Shoot! ";
            bloodyTrigger.SetupItem(shortDesc, longDesc, "bny");
            bloodyTrigger.quality = PickupObject.ItemQuality.A;
            bloodyTrigger.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            bloodyTrigger.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            List<string> mandatoryConsoleIDs2 = new List<string>
            {
                "bny:bloody_trigger",
                "lichy_trigger_finger"
            };
            CustomSynergies.Add("So it benefits you...", mandatoryConsoleIDs2, null, true);
        }
        private float yees;

        private void HandleTriedAttack(PlayerController obj)
        {
            GameManager.Instance.StartCoroutine(death());
        }
        private IEnumerator death()
        {
            this.yees += 1f;
            yield return new WaitForSeconds(0.25f);
            {
                this.yees -= 1f;
                yield break;
            }
        }
        protected override void Update()
        {
            this.RemoveStat(PlayerStats.StatType.Damage);
            bool flagA = base.Owner.PlayerHasActiveSynergy("So it benefits you...");
            if (flagA)
            {
                this.AddStat(PlayerStats.StatType.Damage, (0.033f * yees), StatModifier.ModifyMethod.ADDITIVE);
                base.Owner.stats.RecalculateStats(base.Owner, true, true);
            }
            else
            {

                this.AddStat(PlayerStats.StatType.Damage, (0.025f * yees), StatModifier.ModifyMethod.ADDITIVE);
                base.Owner.stats.RecalculateStats(base.Owner, true, true);
            }
        }


        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.OnTriedToInitiateAttack += this.HandleTriedAttack;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnTriedToInitiateAttack -= this.HandleTriedAttack;
            return base.Drop(player);
        }
        private void AddStat(PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod method = StatModifier.ModifyMethod.ADDITIVE)
        {
            StatModifier statModifier = new StatModifier
            {
                amount = amount,
                statToBoost = statType,
                modifyType = method
            };
            bool flag = this.passiveStatModifiers == null;
            if (flag)
            {
                this.passiveStatModifiers = new StatModifier[]
                {
                    statModifier
                };
            }
            else
            {
                this.passiveStatModifiers = this.passiveStatModifiers.Concat(new StatModifier[]
                {
                    statModifier
                }).ToArray<StatModifier>();
            }
        }
        private void RemoveStat(PlayerStats.StatType statType)
        {
            List<StatModifier> list = new List<StatModifier>();
            for (int i = 0; i < this.passiveStatModifiers.Length; i++)
            {
                bool flag = this.passiveStatModifiers[i].statToBoost != statType;
                if (flag)
                {
                    list.Add(this.passiveStatModifiers[i]);
                }
            }
            this.passiveStatModifiers = list.ToArray();
        }
    }
}