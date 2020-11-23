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
    public class TableTechKnife : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Table Tech Knife";

            string resourceName = "BunnyMod/Resources/tabletechknife.png";

            GameObject obj = new GameObject(itemName);

            TableTechKnife minigunrounds = obj.AddComponent<TableTechKnife>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Violent Flips";
            string longDesc = "This ancient technique allows the user to create daggers from table flipping.\n\nThis Chapter of the Table Sutra has been cut out, presumably with a dagger.";

            minigunrounds.SetupItem(shortDesc, longDesc, "bny");

            //minigunrounds.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            minigunrounds.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            base.Pickup(player);
        }
        private void HandleFlip(FlippableCover table)
        {
            this.m_extantEffect = this.CreateEffect(base.Owner, 1f, 1f);
        }
        private KnifeShieldEffect CreateEffect(PlayerController user, float radiusMultiplier = 1f, float rotationSpeedMultiplier = 1f)
        {
            GameObject obj = new GameObject();
            TableTechKnife fuck = obj.AddComponent<TableTechKnife>();
            KnifeShieldEffect knifeShieldEffect = PickupObjectDatabase.GetById(65).GetComponent<KnifeShieldEffect>();


            knifeShieldEffect.numKnives = fuck.numKnives;
            knifeShieldEffect.remainingHealth = fuck.knifeHealth;
            knifeShieldEffect.knifeDamage = fuck.knifeDamage;
            knifeShieldEffect.circleRadius = fuck.circleRadius * radiusMultiplier;
            knifeShieldEffect.rotationDegreesPerSecond = fuck.rotationDegreesPerSecond * rotationSpeedMultiplier;
            knifeShieldEffect.throwSpeed = fuck.throwSpeed;
            knifeShieldEffect.throwRange = fuck.throwRange;
            knifeShieldEffect.throwRadius = fuck.throwRadius;
            knifeShieldEffect.radiusChangeDistance = fuck.radiusChangeDistance;
            knifeShieldEffect.deathVFX = this.knifeDeathVFX;
            knifeShieldEffect.Initialize(user, this.knifePrefab);
            return knifeShieldEffect;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Remove(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            return base.Drop(player);
        }
        public GameObject knifePrefab;
        protected KnifeShieldEffect m_extantEffect;
        public GameObject knifeDeathVFX;
        public TableTechKnife()
        {
            this.numKnives = 1;
            this.knifeHealth = 0.5f;
            this.knifeDamage = 5f;
            this.circleRadius = 3f;
            this.rotationDegreesPerSecond = 360f;
            this.throwSpeed = 10f;
            this.throwRange = 25f;
            this.throwRadius = 3f;
            this.radiusChangeDistance = 3f;
        }
        public int numKnives;
        public float knifeHealth;
        public float knifeDamage;
        public float circleRadius;
        public float rotationDegreesPerSecond;

        [Header("Thrown Properties")]
        public float throwSpeed;
        public float throwRange;
        public float throwRadius;
        public float radiusChangeDistance;
    }
}