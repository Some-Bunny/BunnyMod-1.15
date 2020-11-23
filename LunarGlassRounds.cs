using System;
using System.Collections.Generic;
using UnityEngine;
using ItemAPI;
using Dungeonator;


namespace BunnyMod
{
    public class LunarGlassRounds : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Lunar Glass Rounds";
            string resourceName = "BunnyMod/Resources/lunarglassrounds.png";
            GameObject obj = new GameObject(itemName);
            LunarGlassRounds Module = obj.AddComponent<LunarGlassRounds>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Frail, and Soft.";
            string longDesc = "Rounds embedded with Lunar Glass.\n\nA piece of glass formed at the core of a broken moon, it's unusual properties extend from time manipulation to turning other things into glass.";
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
            float num = 0.0833f;
            bool flag = UnityEngine.Random.value < num;
            if (flag)
            {
                AkSoundEngine.PostEvent("Play_OBJ_glass_shatter_01", base.gameObject);
                enemy.aiActor.ApplyEffect(this.GlassEffect, 1f, null);
            }
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
        public GameActorFreezeEffect GlassEffect = new GameActorFreezeEffect
        {
            TintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(1f),
            DeathTintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(1f),
            AppliesTint = true,
            AppliesDeathTint = true,
            effectIdentifier = "Shatter",
            FreezeAmount = 100f,
            UnfreezeDamagePercent = 0f,
            crystalNum = 0,
            crystalRot = 0,
            crystalVariation = new Vector2(0.05f, 0.05f),
            debrisMinForce = 5,
            debrisMaxForce = 5,
            debrisAngleVariance = 15f,
            PlaysVFXOnActor = true,
            duration = 7f,
            OverheadVFX = ShatterEffect.ShatterVFXObject,
        };

    }
}



