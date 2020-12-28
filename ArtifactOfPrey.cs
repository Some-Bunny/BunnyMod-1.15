using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    public class ArtifactOfPrey : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Prey";
            string resourceName = "BunnyMod/Resources/Artifacts/prey";
            GameObject obj = new GameObject(itemName);
            ArtifactOfPrey greandeParasite = obj.AddComponent<ArtifactOfPrey>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Hunted.";
            string longDesc = "Every 10 minutes the Lord Of The Jammed spawns.";
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

		private void OnNewFloor()
        {
            this.jamtime += 1f;
            bool flag = this.jamtime == 1f;
            if (flag)
            {
                GameManager.Instance.StartCoroutine(Prey());
            }
        }
        private IEnumerator Prey()
        {
            yield return new WaitForSeconds(600f);
			{
				bool flag5 = base.Owner.HasPickupID(Game.Items["bny:prey"].PickupObjectId);
				if (flag5)
                {
					if (!GameManager.HasInstance || !GameManager.Instance.BestActivePlayer || GameManager.Instance.BestActivePlayer.CurrentRoom == null)
					{
						yield break;
					}
					GameObject superReaper = PrefabDatabase.Instance.SuperReaper;
					Vector2 vector = GameManager.Instance.BestActivePlayer.CurrentRoom.GetRandomVisibleClearSpot(2, 2).ToVector2();
					SpeculativeRigidbody component = superReaper.GetComponent<SpeculativeRigidbody>();
					if (component)
					{
						PixelCollider primaryPixelCollider = component.PrimaryPixelCollider;
						Vector2 a = PhysicsEngine.PixelToUnit(new IntVector2(primaryPixelCollider.ManualOffsetX, primaryPixelCollider.ManualOffsetY));
						Vector2 vector2 = PhysicsEngine.PixelToUnit(new IntVector2(primaryPixelCollider.ManualWidth, primaryPixelCollider.ManualHeight));
						Vector2 vector3 = new Vector2((float)Mathf.CeilToInt(vector2.x), (float)Mathf.CeilToInt(vector2.y));
						Vector2 b = new Vector2((vector3.x - vector2.x) / 2f, 0f).Quantize(0.0625f);
						vector -= a - b;
					}
					UnityEngine.Object.Instantiate<GameObject>(superReaper, vector.ToVector3ZUp(0f), Quaternion.identity);
					GameManager.Instance.StartCoroutine(Prey());
				}
				else
                {
					yield break;
                }

            }
        }

        public override void Pickup(PlayerController player)
        {
			GameManager.Instance.StartCoroutine(Prey());
			this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
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
        private float jamtime;
    }
}



