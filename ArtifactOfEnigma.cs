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
	internal class ArtifactOfEnigma : PassiveItem
	{
		public static void Init()
		{
			string name = "Enigma";
			string resourcePath = "BunnyMod/Resources/Artifacts/enigma.png";
			GameObject gameObject = new GameObject();
			ArtifactOfEnigma boomGuon = gameObject.AddComponent<ArtifactOfEnigma>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "Active Gambler";
			string longDesc = "Your active item changes on use or after 30 seconds. Timer starts on pickup.";
			boomGuon.SetupItem(shortDesc, longDesc, "bny");
			boomGuon.quality = PickupObject.ItemQuality.EXCLUDED;
			boomGuon.Spawnquality = (PickupObject.ItemQuality)UnityEngine.Random.Range(1, 6);
			boomGuon.target = LootEngine.GetItemOfTypeAndQuality<PlayerItem>(boomGuon.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
		}
		public GameObject instanceJetpack = null;

		private void SwitchActives(PlayerController usingPlayer, PlayerItem usedItem)
		{
			{
				ChangeSpiceWeight();
				GameManager.Instance.StartCoroutine(CoolDown(usingPlayer));
			}
		}
		private void StopthefuckingJetpack(PlayerController usingPlayer)
		{
			this.instanceJetpackSprite = this.instanceJetpack.GetComponent<tk2dSprite>();
			usingPlayer.SetIsFlying(false, "jetpack", true, false);
			usingPlayer.DeregisterAttachedObject(this.instanceJetpack, true);
			this.instanceJetpackSprite = null;
			usingPlayer.stats.RecalculateStats(usingPlayer, false, false);
		}

		public tk2dSprite instanceJetpackSprite;
		public static float ChangeSpiceWeight()
		{
			return SpiceItem.ONE_SPICE_WEIGHT;
		}
		private IEnumerator CoolDown(PlayerController usingPlayer)
		{
			yield return new WaitForSeconds(2f);
			{
				usingPlayer.activeItems.Clear();
				yield return new WaitForSeconds(1f);
				{
					base.Owner.spiceCount = -1;
					ChangeSpiceWeight();
					this.Spawnquality = (PickupObject.ItemQuality)UnityEngine.Random.Range(1, 6);
					this.target = LootEngine.GetItemOfTypeAndQuality<PlayerItem>(this.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
					LootEngine.TryGivePrefabToPlayer(this.target.gameObject, base.Owner, true);
				}
			}
			yield break;
		}
		private IEnumerator ProperActiveReset()
		{
			ChangeSpiceWeight();
			yield return new WaitForSeconds(30f);
			{
				base.Owner.activeItems.Clear();
				{
					base.Owner.spiceCount = -1;
					this.Spawnquality = (PickupObject.ItemQuality)UnityEngine.Random.Range(1, 6);
					this.target = LootEngine.GetItemOfTypeAndQuality<PlayerItem>(this.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
					LootEngine.TryGivePrefabToPlayer(this.target.gameObject, base.Owner, true);
				}
				GameManager.Instance.StartCoroutine(ProperActiveReset());
			}
			yield break;
		}
		public override void Pickup(PlayerController player)
		{
			GameManager.Instance.StartCoroutine(ProperActiveReset());
			player.OnUsedPlayerItem += this.SwitchActives;
			this.CanBeDropped = false;
			base.Pickup(player);
		}


		public override DebrisObject Drop(PlayerController player)
		{

			return base.Drop(player);
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
		static ArtifactOfEnigma()
		{
            SpiceItem.ONE_SPICE_WEIGHT = 0.1f;
		}
		public PickupObject.ItemQuality Spawnquality;
		public PlayerItem target;
	}
}

