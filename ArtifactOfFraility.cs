using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;
using Dungeonator;

namespace BunnyMod
{
    public class ArtifactOfFraility : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Frailty";
            string resourceName = "BunnyMod/Resources/Artifacts/frailty.png";
            GameObject obj = new GameObject(itemName);
			ArtifactOfFraility greandeParasite = obj.AddComponent<ArtifactOfFraility>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Wither";
            string longDesc = "Damage is near-permanent.";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.quality = PickupObject.ItemQuality.EXCLUDED;
        }
		protected override void Update()
		{
			bool flag = base.Owner;
			if (flag)
			{
				PlayerController player = (GameManager.Instance.PrimaryPlayer);
				this.CalculateStats(base.Owner);
				int healthInt = Convert.ToInt32(player.healthHaver.GetCurrentHealth() * 2);
				int armorInt = Convert.ToInt32(player.healthHaver.Armor);
				int total = healthInt + armorInt;
				if (total == DeathMark+1 || total == DeathMark +2)
                {
					this.EnableVFX(player);
				}
				else
                {
					this.DisableVFX(player);
				}
			}

		}
		private void EnableVFX(PlayerController user)
		{
			Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
			outlineMaterial.SetColor("_OverrideColor", new Color(19f, 2f, 19f));
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00032B38 File Offset: 0x00030D38
		private void DisableVFX(PlayerController user)
		{
			Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
			outlineMaterial.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
		}
		private void OopsDamage(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			this.DeathMark += 1f;
			int healthInt = Convert.ToInt32(player.healthHaver.GetCurrentHealth() * 2);
			int armorInt = Convert.ToInt32(player.healthHaver.Armor);
			int total = healthInt + armorInt;
			if (total <= DeathMark || total == DeathMark)
			{
				int num = 2;
				if (num >= 0)
				{
					player.healthHaver.ApplyDamage(total, Vector2.zero, StringTableManager.GetEnemiesString("#SMOKING", -1), CoreDamageTypes.None, DamageCategory.Normal, true, null, false);
					num--;
				}

				//base.Owner.healthHaver.Die(Vector2.zero);
			}
		}
		private float DeathMark;


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


		private void OnNewFloor()
		{
			this.DeathMark = 0f;

		}
		public override void Pickup(PlayerController player)
		{
			GameManager.Instance.OnNewLevelFullyLoaded += this.OnNewFloor;
			player.healthHaver.OnDamaged += new HealthHaver.OnDamagedEvent(this.OopsDamage);
			this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
		{
			GameManager.Instance.OnNewLevelFullyLoaded -= this.OnNewFloor;
			player.healthHaver.OnDamaged -= new HealthHaver.OnDamagedEvent(this.OopsDamage);
			Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
		//private static int kills = 0;
		public string overrideBreakAnimName;
		public string breakAnimName;
		public PickupObject.ItemQuality Spawnquality;
		public PickupObject target;
		public PickupObject target1;
	}
}



