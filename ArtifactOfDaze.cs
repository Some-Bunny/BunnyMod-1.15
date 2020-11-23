using System;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ArtifactOfDaze : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Daze";
            string resourceName = "BunnyMod/Resources/Artifacts/daze";
            GameObject obj = new GameObject(itemName);
            ArtifactOfDaze greandeParasite = obj.AddComponent<ArtifactOfDaze>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "I can't see!";
            string longDesc = "Temporarily blinded upon entering a room.";
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

		private void Blind()
        {
            AkSoundEngine.PostEvent("Play_OBJ_chaff_blast_01", base.gameObject);
            this.FlashHoldtime = 0.5f;
            this.FlashFadetime = 3f;
            Pixelator.Instance.FadeToColor(this.FlashFadetime, Color.black, true, this.FlashHoldtime);
            StickyFrictionManager.Instance.RegisterCustomStickyFriction(0.15f, 3f, false, false); this.FlashHoldtime = 0.5f;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnEnteredCombat += (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.Blind));
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



