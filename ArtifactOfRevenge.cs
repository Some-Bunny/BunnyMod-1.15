using System;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ArtifactOfRevenge : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Revenge";
            string resourceName = "BunnyMod/Resources/Artifacts/revenge";
            GameObject obj = new GameObject(itemName);
            ArtifactOfRevenge greandeParasite = obj.AddComponent<ArtifactOfRevenge>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Literally a Spite ripoff.";
            string longDesc = "Enemies drop bombs on death.";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.quality = PickupObject.ItemQuality.EXCLUDED;
        }
		protected override void Update()
		{
			bool flag = base.Owner;
			if (flag)
			{
				this.CalculateStats(base.Owner);
			}
		}

		private int currentItems;

		private int lastItems;

		private int currentGuns;

		private int lastGuns;

		// Token: 0x06000355 RID: 853 RVA: 0x0001F480 File Offset: 0x0001D680
		private void CalculateStats(PlayerController player)
		{
			this.currentItems = player.passiveItems.Count;
			this.currentGuns = player.inventory.AllGuns.Count;
			bool flag = this.currentItems != this.lastItems;
			bool flag2 = this.currentGuns != this.lastGuns;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				this.RemoveStat(PlayerStats.StatType.Curse);
				{
					bool flag5 = base.Owner.HasPickupID(Game.Items["bny:megalomania"].PickupObjectId);
					if (flag5)
					{
						this.AddStat(PlayerStats.StatType.Curse, -.2f, StatModifier.ModifyMethod.ADDITIVE);
					}
					this.lastItems = this.currentItems;
					this.lastGuns = this.currentGuns;
					player.stats.RecalculateStats(player, true, false);
				}

			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001F684 File Offset: 0x0001D884
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
		private void EatPen(float damage, bool fatal, HealthHaver enemyHealth)
        {
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			if (enemyHealth.specRigidbody != null)
            {
                bool flag = enemyHealth.aiActor && fatal;
                if (flag)
				{
					float num = 2;
					bool sacrificeisabastard = player.HasPickupID(Game.Items["bny:sacrifice"].PickupObjectId);
					if (sacrificeisabastard)
					{
						num /= 2;
					}
					for (int counter = 0; counter < num; counter++)
                    {
                        this.Revenge(enemyHealth.sprite.WorldCenter);
                    }
                }
            }
        }

        public void Revenge(Vector3 position)
        {
            Projectile projectile = ((Gun)ETGMod.Databases.Items[19]).DefaultModule.projectiles[0];
            GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 359)));
            Projectile component2 = gameObject.GetComponent<Projectile>();
            ExplosiveModifier boomer = projectile.GetComponent<ExplosiveModifier>();
            boomer.explosionData.damageToPlayer = 1;
            boomer.explosionData.damage = 1;
            {
				component2.HasDefaultTint = true;
				component2.DefaultTintColor = new Color(10f, 0f, 0f).WithAlpha(1f);
				component2.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyCollider));
                component2.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyHitBox));
                component2.AdjustPlayerProjectileTint(new Color((float)(40), (float)(40), 0f), 5, 0f);
                component2.baseData.speed = UnityEngine.Random.Range(4f, 8f);
                component2.baseData.range = UnityEngine.Random.Range(3f, 8f);
                component2.baseData.damage = .1f;
                component2.AdditionalScaleMultiplier = 1.2f;
            }
        }

        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
            player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.EatPen));
        }
        public override DebrisObject Drop(PlayerController player)
        {
			player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Remove(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.EatPen));

            return base.Drop(player);
        }
    }
}



