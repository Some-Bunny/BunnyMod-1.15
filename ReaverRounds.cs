using System;
using System.Collections.Generic;
using UnityEngine;
using ItemAPI;
using Dungeonator;


namespace BunnyMod
{
    public class ReaverRounds : PassiveItem
    {
        public static void Init()
        {
            // i dont fucking know what to write. fuck AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            string itemName = "Reaver Rounds";
            string resourceName = "BunnyMod/Resources/reaverrounds.png";
            GameObject obj = new GameObject(itemName);
            ReaverRounds Module = obj.AddComponent<ReaverRounds>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Secure. Detain. Protect.";
            string longDesc = "Rounds with little pellets made of Void Reavers.\n\nThe unusual properties of these rounds cause enemies to lock in place..";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.A;
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            player.PostProcessProjectile += this.ShatterChance;
            base.Pickup(player);
        }
        private void ShatterChance(Projectile projectile, float eff)
        {
            projectile.OnHitEnemy = new Action<Projectile, SpeculativeRigidbody, bool>(this.Proj);

        }
        public void Proj(Projectile bullet, SpeculativeRigidbody enemy, bool what)
        {
            float num = 0.06f;
            bool flag = UnityEngine.Random.value < num;
            if (flag)
            {
                AkSoundEngine.PostEvent("Play_wpn_kthulu_soul_01", base.gameObject);
                enemy.aiActor.ApplyEffect(this.DetainEffect, 1f, null);
            }
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
        public GameActorSpeedEffect DetainEffect = new GameActorSpeedEffect
        {
            AffectsPlayers = false,
            duration = 6f,
            AppliesTint = true,
            SpeedMultiplier = 0f,
            maxStackedDuration = 12f,
            TintColor = new Color(0.3f, 0f, 0.3f).WithAlpha(0.9f)
        };

    }
}



