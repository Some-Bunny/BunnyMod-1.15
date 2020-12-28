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

            string resourceName = "BunnyMod/Resources/actualtabletechknife.png";

            GameObject obj = new GameObject(itemName);

            TableTechKnife minigunrounds = obj.AddComponent<TableTechKnife>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Violent Flips";
            string longDesc = "This ancient technique allows the user to create daggers from table flipping.\n\nThis Chapter of the Table Sutra has been cut out, presumably with a dagger.";

            minigunrounds.SetupItem(shortDesc, longDesc, "bny");

            //minigunrounds.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            minigunrounds.quality = PickupObject.ItemQuality.B;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnTableFlipCompleted = (Action<FlippableCover>)Delegate.Combine(player.OnTableFlipCompleted, new Action<FlippableCover>(this.HandleFlip));
            base.Pickup(player);
        }
        private void HandleFlip(FlippableCover table)
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			bool flag = this.shield != null && this.shield.gameObject;
			if (flag)
			{
				this.shield.ThrowShield();
			}
			this.shield = this.CreateEffect(player, 4, 2f);
		}
		private KnifeShieldEffect CreateEffect(PlayerController user, int numKnives, float radiusMultiplier = 2f)
		{
			KnifeShieldItem knifeShieldItem = (KnifeShieldItem)PickupObjectDatabase.GetById(65);
			KnifeShieldEffect knifeShieldEffect = new GameObject("knife shield effect")
			{
				transform =
				{
					position = user.LockedApproximateSpriteCenter,
					parent = user.transform
				}
			}.AddComponent<KnifeShieldEffect>();
			knifeShieldEffect.numKnives = numKnives;
			knifeShieldEffect.remainingHealth = knifeShieldItem.knifeHealth;
			knifeShieldEffect.knifeDamage = knifeShieldItem.knifeDamage;
			knifeShieldEffect.circleRadius = radiusMultiplier;
			knifeShieldEffect.rotationDegreesPerSecond = knifeShieldItem.rotationDegreesPerSecond;
			knifeShieldEffect.throwSpeed = knifeShieldItem.throwSpeed;
			knifeShieldEffect.throwRange = knifeShieldItem.throwRange;
			knifeShieldEffect.throwRadius = knifeShieldItem.throwRadius;
			knifeShieldEffect.radiusChangeDistance = knifeShieldItem.radiusChangeDistance;
			knifeShieldEffect.deathVFX = knifeShieldItem.knifeDeathVFX;
			this.shieldPrefab = knifeShieldItem.knifePrefab;
			knifeShieldEffect.Initialize(user, this.shieldPrefab);
			return knifeShieldEffect;
		}

		// Token: 0x040000A1 RID: 161


		// Token: 0x040000A2 RID: 162
		private GameObject shieldPrefab;

		// Token: 0x040000A3 RID: 163
		public int Kills;

		// Token: 0x040000A4 RID: 164
		public KnifeShieldEffect shield;
	}
}