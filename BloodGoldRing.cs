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
    public class BloodGoldRing: PassiveItem
    {
        public static void Init()
        {
            string name = "Blood-Gold Ring";
            string resourcePath = "BunnyMod/Resources/bloodgoldring.png";
            GameObject gameObject = new GameObject();
            BloodGoldRing bloodring = gameObject.AddComponent<BloodGoldRing>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject, true);
            string shortDesc = "Bloody Treasure";
            string longDesc = "A ring with a blood diamond embedded on top.\n\nOn the inside, 'Those who seek wealth, must abandon their most valuable posession.' is engraved.";
            bloodring.SetupItem(shortDesc, longDesc, "bny");
            bloodring.quality = PickupObject.ItemQuality.C;
            bloodring.AddPassiveStatModifier(PlayerStats.StatType.Curse, 0.5f, StatModifier.ModifyMethod.ADDITIVE);
            bloodring.AddPassiveStatModifier(PlayerStats.StatType.DamageToBosses, .125f, StatModifier.ModifyMethod.ADDITIVE);
            bloodring.AddPassiveStatModifier(PlayerStats.StatType.MoneyMultiplierFromEnemies, .1f, StatModifier.ModifyMethod.ADDITIVE);
            bloodring.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);

            BloodGoldRing.ringofrain = bloodring.PickupObjectId;
        }

        public override void Pickup(PlayerController player)
        {
            HealthHaver healthHaver = player.healthHaver;
            healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Combine(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));           // player.healthHaver.OnPreDeath += this.HandlePreDeath;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            HealthHaver healthHaver = player.healthHaver;
            healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Remove(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));            //player.healthHaver.OnPreDeath -= this.HandlePreDeath;
            return base.Drop(player);
        }
        
        private void HandleEffect(HealthHaver source, HealthHaver.ModifyDamageEventArgs args)
        {
            bool flag = args == EventArgs.Empty;
            if (!flag)
            {
                bool flag2 = args.ModifiedDamage <= 0f;
                if (!flag2)
                {
                    bool flag3 = !source.IsVulnerable;
                    if (!flag3)
                    {
                        bool flag4 = base.Owner;
                        if (flag4)
                        {
                            PlayerController player = base.Owner as PlayerController;
                            bool actuallygonnafuckingdie = ((double)player.healthHaver.GetCurrentHealth() == 0.5 && player.healthHaver.Armor == 0f) || player.healthHaver.GetCurrentHealth() == 0f && player.healthHaver.Armor == 1f;
                            if (actuallygonnafuckingdie)
                            {
                                source.StartCoroutine("IncorporealityOnHit");
                                source.TriggerInvulnerabilityPeriod(-1f);
                                args.ModifiedDamage = 0f;
                                AkSoundEngine.PostEvent("Play_ENM_darken_world_01", gameObject);
                                float num = 0f;
                                num = (player.stats.GetStatValue(PlayerStats.StatType.Health));
                                ApplyStat(player, PlayerStats.StatType.Health, (-num) + 1, StatModifier.ModifyMethod.ADDITIVE);
                                ApplyStat(player, PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);
                                player.healthHaver.FullHeal();
                                player.IsOnFire = false;
                                if (player.characterIdentity == PlayableCharacters.Robot)
                                {
                                    player.healthHaver.Armor = 3f;
                                }
                                base.StartCoroutine(this.Weeee());
                            }
                        }
                    }
                }
            }
        }
        private static void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod modifyMethod)
        {
            player.stats.RecalculateStats(player, false, false);
            StatModifier statModifier = new StatModifier()
            {
                statToBoost = statType,
                amount = amountToApply,
                modifyType = modifyMethod
            };
            player.ownerlessStatModifiers.Add(statModifier);
            player.stats.RecalculateStats(player, false, false);
        }
        private IEnumerator Weeee()
        {
            yield return new WaitForSeconds(0.1f);
            {
                PlayerController player = base.Owner as PlayerController;
                player.IsOnFire = false;
                player.RemovePassiveItem(BloodGoldRing.ringofrain);
                LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.Owner);
                LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.Owner);
                IntVector2 bestRewardLocation = player.CurrentRoom.GetBestRewardLocation(player.CurrentRoom.GetRandomVisibleClearSpot(1, 1));
                //Chest chest1 = GameManager.Instance.RewardManager.SpawnRewardChestAt(bestRewardLocation, -4f, PickupObject.ItemQuality.EXCLUDED);
                Chest chest2 = GameManager.Instance.RewardManager.SpawnRewardChestAt(bestRewardLocation, 4f, PickupObject.ItemQuality.EXCLUDED);
                //chest1.RegisterChestOnMinimap(chest1.GetAbsoluteParentRoom());
                chest2.RegisterChestOnMinimap(chest2.GetAbsoluteParentRoom());
            }
        }
        public ExtraLifeItem.ExtraLifeMode extraLifeMode;
        public static int ringofrain;

    }
}