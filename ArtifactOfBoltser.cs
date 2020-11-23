using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ArtifactOfBolster : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Bolster";
            string resourceName = "BunnyMod/Resources/Artifacts/bolster";
            GameObject obj = new GameObject(itemName);
			ArtifactOfBolster greandeParasite = obj.AddComponent<ArtifactOfBolster>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Enraged";
            string longDesc = "Killing an enemy temporarily increases enemy projectile speed.";
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

		private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
		{
			if (enemyHealth.specRigidbody != null)
			{
				bool flag = enemyHealth.aiActor && fatal;
				if (flag)
				{
					GameManager.Instance.StartCoroutine(Bolster());
				}
			}
		}
		private IEnumerator Bolster()
		{
			{
				this.AddStat(PlayerStats.StatType.EnemyProjectileSpeedMultiplier, .25f, StatModifier.ModifyMethod.ADDITIVE);
				base.Owner.stats.RecalculateStats(base.Owner, true, true);
                yield return new WaitForSeconds(3f);
                {
					this.AddStat(PlayerStats.StatType.EnemyProjectileSpeedMultiplier, -.25f, StatModifier.ModifyMethod.ADDITIVE);
					base.Owner.stats.RecalculateStats(base.Owner, true, true);
				}
			}
			yield break;
		}


		public override void Pickup(PlayerController player)
        {
			player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
			this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
        public float FlashHoldtime;
        public float FlashFadetime;
    }
}



