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
    public class ArtifactOfSacrifice : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Sacrifice";
            string resourceName = "BunnyMod/Resources/Artifacts/sacrifice";
            GameObject obj = new GameObject(itemName);
			ArtifactOfSacrifice greandeParasite = obj.AddComponent<ArtifactOfSacrifice>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "The Hunt";
            string longDesc = "Chests/Mimics and broken Chests no longer drop loot. Enemies can now drop items.";
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



		public static void DenyDrops(Action<Chest> orig, Chest self)
		{
			GameObject obj = new GameObject();
			ArtifactOfSacrifice fuck = obj.AddComponent<ArtifactOfSacrifice>();
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			bool flag = player.HasPickupID(Game.Items["bny:sacrifice"].PickupObjectId);
			if (flag)
            {
				fuck.GetRidOfBowler();
				if (self.ChestIdentifier == Chest.SpecialChestIdentifier.SECRET_RAINBOW)
				{
					self.RevealSecretRainbow();
				}
				if (self.ChestIdentifier == Chest.SpecialChestIdentifier.SECRET_RAINBOW || self.IsRainbowChest || self.breakAnimName.Contains("redgold") || self.breakAnimName.Contains("black"))
				{
					GameStatsManager.Instance.SetFlag(GungeonFlags.ITEMSPECIFIC_GOLD_JUNK, true);
				}
				self.spriteAnimator.Play(string.IsNullOrEmpty(fuck.overrideBreakAnimName) ? fuck.breakAnimName : fuck.overrideBreakAnimName);
				self.specRigidbody.enabled = false;
				self.IsBroken = true;
				IntVector2 intVector = self.specRigidbody.UnitBottomLeft.ToIntVector2(VectorConversions.Floor);
				IntVector2 intVector2 = self.specRigidbody.UnitTopRight.ToIntVector2(VectorConversions.Floor);
				for (int i = intVector.x; i <= intVector2.x; i++)
				{
					for (int j = intVector.y; j <= intVector2.y; j++)
					{
						GameManager.Instance.Dungeon.data[new IntVector2(i, j)].isOccupied = false;
					}
				}
				if (self.LockAnimator != null && self.LockAnimator)
				{
					UnityEngine.Object.Destroy(self.LockAnimator.gameObject);
				}
				Transform transform = self.transform.Find("Shadow");
				if (transform != null)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
				
				for (int k = 0; k < GameManager.Instance.AllPlayers.Length; k++)
				{
					if (GameManager.Instance.AllPlayers[k].OnChestBroken != null)
					{
						GameManager.Instance.AllPlayers[k].OnChestBroken(GameManager.Instance.AllPlayers[k], self);
					}
				}

			}
			else
			{
				bool harderlotj = GungeonAPI.JammedSquire.NoHarderLotJ;
				if (harderlotj)
				{
					orig(self);
				}
				else
                {
					float num = 0f;
					num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					orig(self);
					fuck.random = UnityEngine.Random.Range(0.0f, 1.0f);
					if (fuck.random <= (num / 10))
					{
						LootEngine.SpawnItem(PickupObjectDatabase.GetById(127).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
					}
				}
			}
		}
		private void GetRidOfBowler()
		{
			if (this.m_bowlerInstance)
			{
				LootEngine.DoDefaultPurplePoof(this.m_bowlerInstance.GetComponent<tk2dBaseSprite>().WorldCenter, false);
				UnityEngine.Object.Destroy(this.m_bowlerInstance);
				this.m_bowlerInstance = null;
				AkSoundEngine.PostEvent("Stop_SND_OBJ", base.gameObject);
			}
		}
		public static void DenyDropsMimic(Action<Chest, PlayerController, int> orig, Chest self, PlayerController player, int tierShift = 0)
		{
			bool flag = player.HasPickupID(Game.Items["bny:sacrifice"].PickupObjectId);
			if (flag)
            {

            }
			else
			{
				bool harderlotj = GungeonAPI.JammedSquire.NoHarderLotJ;
				if (harderlotj)
				{
					orig(self, player, tierShift = 0);
				}
				else
				{

					float num = 0f;
					orig(self, player, tierShift = 0);
					num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
					ArtifactOfSacrifice.randomcurse = UnityEngine.Random.Range(0.0f, 1.0f);
					if (ArtifactOfSacrifice.randomcurse <= (num / 18) + (1 * (num / 10)))
					{
						int num3 = UnityEngine.Random.Range(0, 3);
						bool flag3 = num3 == 0;
						if (flag3)
						{
							LootEngine.SpawnItem(PickupObjectDatabase.GetById(224).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
						}
						bool flag4 = num3 == 1;
						if (flag4)
						{
							LootEngine.SpawnItem(PickupObjectDatabase.GetById(67).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
						}
						bool flag6 = num3 == 2;
						if (flag6)
						{
							LootEngine.SpawnItem(PickupObjectDatabase.GetById(78).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
						}

					}
					ArtifactOfSacrifice.chanceagain = UnityEngine.Random.Range(0.0f, 1.0f);
					if (ArtifactOfSacrifice.randomcurse <= (num / 13f) * (1.01 * (num / 13)))
					{
						bool items = !GameStatsManager.Instance.IsRainbowRun && self && self.contents != null;
						if (items)
						{
							foreach (PickupObject pickupObject in self.contents)
							{
								GameManager.Instance.RewardManager.SpawnTotallyRandomItem(player.specRigidbody.UnitCenter, pickupObject.quality, pickupObject.quality);
							}
						}
					}
				}
			}
		
		}
		private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
		{
			if (enemyHealth.specRigidbody != null)
			{
				bool flag2 = enemyHealth.aiActor && fatal;
				if (flag2)
				{
                    if (ItemChanceBoost == true)
                    {
                        this.random = UnityEngine.Random.Range(0.00f, 1.00f);
                        if (random <= 0.08f)
						{
							this.RollRnG();
							this.Spawnquality = (PickupObject.ItemQuality)(RnG);
                            this.target = LootEngine.GetItemOfTypeAndQuality<PickupObject>(this.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
                            LootEngine.SpawnItem(this.target.gameObject, enemyHealth.specRigidbody.UnitCenter, Vector2.down, 0f, false, true, false);
                            ItemChanceBoost = false;
                        }
                    }
                    else //if (ItemChanceBoost == false)
                    {
                        this.random = UnityEngine.Random.Range(0.00f, 1.00f);
                        if (random <= 0.025f)
                        {
							this.RollRnG();
                            this.Spawnquality = (PickupObject.ItemQuality)(RnG);
                            this.target = LootEngine.GetItemOfTypeAndQuality<PickupObject>(this.Spawnquality, GameManager.Instance.RewardManager.ItemsLootTable, false);
                            LootEngine.SpawnItem(this.target.gameObject, enemyHealth.specRigidbody.UnitCenter, Vector2.down, 0f, false, true, false);
                        }
                    }
					{
						if (GunChanceBoost == true)
						{
							this.random = UnityEngine.Random.Range(0.00f, 1.00f);
							if (random <= 0.08f)
							{
								this.RollRnG();
								this.Spawnquality = (PickupObject.ItemQuality)(RnG);
								this.target1 = LootEngine.GetItemOfTypeAndQuality<PickupObject>(this.Spawnquality, GameManager.Instance.RewardManager.GunsLootTable, false);
								LootEngine.SpawnItem(this.target1.gameObject, enemyHealth.specRigidbody.UnitCenter, Vector2.down, 0f, false, true, false);
								GunChanceBoost = false;
							}
						}
						else// if (GunChanceBoost == false)
						{
							this.random = UnityEngine.Random.Range(0.00f, 1.00f);
							if (random <= 0.025f)
							{
								this.RollRnG();
								this.Spawnquality = (PickupObject.ItemQuality)(RnG);
								this.target1 = LootEngine.GetItemOfTypeAndQuality<PickupObject>(this.Spawnquality, GameManager.Instance.RewardManager.GunsLootTable, false);
								LootEngine.SpawnItem(this.target1.gameObject, enemyHealth.specRigidbody.UnitCenter, Vector2.down, 0f, false, true, false);
							}
						}
					}
				}
			}
		}
		private void RollRnG()
        {
			int num3 = UnityEngine.Random.Range(0, 19);
			bool DTier = num3 == 0 | num3 == 1 | num3 == 2 | num3 == 3;
			if (DTier)
			{
				this.RnG = 1;
			}
			bool CTier = num3 == 4| num3 == 5 | num3 == 6 | num3 == 7 | num3 == 8 | num3 == 9 | num3 == 10;
			if (CTier)
			{
				this.RnG = 2;
			}
			bool BTier = num3 == 11| num3 == 12 | num3 == 13 | num3 == 14 | num3 == 15;
			if (BTier)
			{
				this.RnG = 3;
			}
			bool ATier = num3 == 16 | num3 == 17 | num3 == 18;
			if (ATier)
			{
				this.RnG = 4;
			}
			bool STier = num3 == 19;
			if (STier)
			{
				this.RnG = 5;
			}
		}
		private void OnNewFloor()
		{
			GunChanceBoost = true;
			ItemChanceBoost = true;
		}
		public override void Pickup(PlayerController player)
		{
			GameManager.Instance.OnNewLevelFullyLoaded += this.OnNewFloor;
			player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
			this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
		{
			GameManager.Instance.OnNewLevelFullyLoaded -= this.OnNewFloor;
			player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
			Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
		//private static int kills = 0;
		private bool GunChanceBoost = true;
		private bool ItemChanceBoost = true;
		private GameObject m_bowlerInstance;
		public string overrideBreakAnimName;
		public string breakAnimName;
		private float random;
		private float RnG;
		public PickupObject.ItemQuality Spawnquality;
		public PickupObject target;
		public PickupObject target1;
		private static float randomcurse;
		private static float chanceagain;
	}
}



