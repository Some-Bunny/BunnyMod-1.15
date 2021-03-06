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
    public class DeathsDebt : PlayerItem
    {

        public static void Init()
        {
            string itemName = "Deaths Debt";
            string resourceName = "BunnyMod/Resources/deathsdebt.png";
            GameObject obj = new GameObject(itemName);
            DeathsDebt lockpicker = obj.AddComponent<DeathsDebt>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Death Arrives In Different Ways...";
            string longDesc = "Allows the safety of your Vitality in exchange for the damage of your body, mind and soul. A human skull with runes carved onto it.\n\nAn uncomforting presence watches you...";
            lockpicker.SetupItem(shortDesc, longDesc, "bny");
            lockpicker.SetCooldownType(ItemBuilder.CooldownType.Timed, 3f);
            lockpicker.consumable = false;
            lockpicker.quality = PickupObject.ItemQuality.C;
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            DeathsDebt.spriteIDs = new int[DeathsDebt.spritePaths.Length];
            DeathsDebt.spriteIDs[0] = SpriteBuilder.AddSpriteToCollection(DeathsDebt.spritePaths[0], lockpicker.sprite.Collection);
            DeathsDebt.spriteIDs[1] = SpriteBuilder.AddSpriteToCollection(DeathsDebt.spritePaths[1], lockpicker.sprite.Collection);
        }
        public override void Pickup(PlayerController player)
        {
            this.DebtEnabled = false;
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
           if (DebtEnabled == false)
           {
                AkSoundEngine.PostEvent("Play_VO_lichA_chuckle_01", base.gameObject);
                this.id = DeathsDebt.spriteIDs[1];
                base.sprite.SetSprite(this.id);
                this.EnableVFX(base.LastOwner);
                user.OnHitByProjectile += this.OwnerHitByProjectile;
                HealthHaver healthHaver = base.LastOwner.healthHaver;
                healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Combine(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));
                this.DebtEnabled = true;

           }
           else           
           {
                this.id = DeathsDebt.spriteIDs[0];
                base.sprite.SetSprite(this.id);
                this.DisableVFX(base.LastOwner);
                user.OnHitByProjectile -= this.OwnerHitByProjectile;
                HealthHaver healthHaver = base.LastOwner.healthHaver;
                healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Remove(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));
                AkSoundEngine.PostEvent("play_boss_spacebaby_arrive_01", base.gameObject);
                this.DebtEnabled = false;
            }
        }
        private void OwnerHitByProjectile(Projectile incomingProjectile, PlayerController arg2)
        {
            if (incomingProjectile.Owner != arg2)
            {
                var target = incomingProjectile.Owner;
                target.PlayEffectOnActor(ResourceCache.Acquire("Global VFX/VFX_Curse") as GameObject, Vector3.zero, true, false, false);
                target.healthHaver.ApplyDamage(250, Vector2.zero, "eat lead nerd", CoreDamageTypes.None, DamageCategory.Unstoppable, true, null, false);
            }
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
                        bool flag4 = base.LastOwner;
                        if (flag4)
                        {
                            AkSoundEngine.PostEvent("Play_BOSS_spacebaby_charge_01", base.gameObject);
                            source.StartCoroutine("IncorporealityOnHit");
                            source.TriggerInvulnerabilityPeriod(-1f);
                            args.ModifiedDamage = 0f;
                            ApplyStat(base.LastOwner, PlayerStats.StatType.Damage, 0.96f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.ReloadSpeed, 1.04f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.RateOfFire, 1.04f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            //ApplyStat(base.LastOwner, PlayerStats.StatType.DodgeRollDistanceMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                            //ApplyStat(base.LastOwner, PlayerStats.StatType.DodgeRollSpeedMultiplier, UnityEngine.Random.Range(1.25f, 0.75f), StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.DamageToBosses, 0.96f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.MovementSpeed, 0.96f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.Accuracy, 1.06f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.KnockbackMultiplier, 0.96f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                            ApplyStat(base.LastOwner, PlayerStats.StatType.RangeMultiplier, 0.96f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                        }
                    }
                }
            }
        }
        private void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod modifyMethod)
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
        protected override void OnPreDrop(PlayerController user)
        {
            HealthHaver healthHaver = base.LastOwner.healthHaver;
            healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Remove(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));
            user.OnHitByProjectile -= this.OwnerHitByProjectile;
            this.DebtEnabled = false;
            this.DisableVFX(base.LastOwner);
            base.OnPreDrop(user);
        }
        private void EnableVFX(PlayerController user)
        {
            Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outlineMaterial.SetColor("_OverrideColor", new Color(16f, 2f, 2f));
        }

        // Token: 0x060005D4 RID: 1492 RVA: 0x00032B38 File Offset: 0x00030D38
        private void DisableVFX(PlayerController user)
        {
            Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outlineMaterial.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
        }
        private static int[] spriteIDs;
        private static readonly string[] spritePaths = new string[]
        {
            "BunnyMod/Resources/deathsdebt.png",
            "BunnyMod/Resources/deathsdebtenabled.png",
        };
        public bool DebtEnabled = false;
        private int id;

    }
}



