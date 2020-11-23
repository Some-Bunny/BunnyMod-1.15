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
	internal class ArtifactOfHatred : IounStoneOrbitalItem
	{
		public static void Init()
		{
			string name = "Hatred";
			string resourcePath = "BunnyMod/Resources/Artifacts/hatred.png";
			GameObject gameObject = new GameObject();
			ArtifactOfHatred boomGuon = gameObject.AddComponent<ArtifactOfHatred>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "Watching over you.";
			string longDesc = "Spawns a Guon stone that hurts you if you shoot it.";
			boomGuon.SetupItem(shortDesc, longDesc, "bny");
			boomGuon.quality = PickupObject.ItemQuality.EXCLUDED;
			ArtifactOfHatred.BuildPrefab();
			boomGuon.OrbitalPrefab = ArtifactOfHatred.orbitalPrefab;
			boomGuon.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
		}

		public static void BuildPrefab()
		{
			bool flag = ArtifactOfHatred.orbitalPrefab != null;
			if (!flag)
			{
				GameObject gameObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/Artifacts/hatredfloaty.png", null, true);
				gameObject.name = "aasdadddasdassda";
				SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(10, 10));
				speculativeRigidbody.CollideWithTileMap = false;
				speculativeRigidbody.CollideWithOthers = true;
				speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
				ArtifactOfHatred.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
				ArtifactOfHatred.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
				ArtifactOfHatred.orbitalPrefab.shouldRotate = false;
				ArtifactOfHatred.orbitalPrefab.orbitRadius = 4.5f;
				ArtifactOfHatred.orbitalPrefab.orbitDegreesPerSecond = 45f;
				ArtifactOfHatred.orbitalPrefab.SetOrbitalTier(0);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				FakePrefab.MarkAsFakePrefab(gameObject);
				gameObject.SetActive(false);
			}
		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			ArtifactOfHatred.guonHook = new Hook(typeof(PlayerOrbital).GetMethod("Initialize"), typeof(ArtifactOfHatred).GetMethod("GuonInit"));
			bool flag = player.gameObject.GetComponent<ArtifactOfHatred.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				player.gameObject.GetComponent<ArtifactOfHatred.BaBoom>().Destroy();
			}
			player.gameObject.AddComponent<ArtifactOfHatred.BaBoom>();
			GameManager.Instance.OnNewLevelFullyLoaded += this.FixGuon;
			bool flag4 = this.m_extantOrbital != null;
			bool flag5 = flag4;
			if (flag5)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00019660 File Offset: 0x00017860
		private void OnPreCollison(SpeculativeRigidbody myRigidbody, PixelCollider myCollider, SpeculativeRigidbody other, PixelCollider otherCollider)
		{
			bool flag = base.Owner != null;
			bool flag2 = flag;
			if (flag2)
			{
				Projectile component = other.GetComponent<Projectile>();
				bool flag3 = component != null && (component.Owner is PlayerController);
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = !ArtifactOfHatred.onCooldown;
					bool flag6 = flag5;
					if (flag6)
                    {
						ArtifactOfHatred.onCooldown = true;
						GameManager.Instance.StartCoroutine(ArtifactOfHatred.StartCooldown(base.Owner));
						{
							PlayableCharacters characterIdentity = base.Owner.characterIdentity;
							bool flag7 = characterIdentity != PlayableCharacters.Robot;
							if (flag7)
							{
								AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", base.gameObject);
								base.Owner.healthHaver.ApplyHealing(-0.5f);
							}
							else
							{
								bool flag8 = characterIdentity == PlayableCharacters.Robot;
								if (flag8)
								{
									AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", base.gameObject);
									base.Owner.healthHaver.Armor = base.Owner.healthHaver.Armor - 1f;

								}
							}
						}
					}
				}
			}
		}
		public static IEnumerator StartCooldown(PlayerController player)
		{
			yield return new WaitForSeconds(1f);
			ArtifactOfHatred.onCooldown = false;
			yield break;
		}
		private static bool onCooldown;


		// Token: 0x0600025B RID: 603 RVA: 0x000196DC File Offset: 0x000178DC
		public override DebrisObject Drop(PlayerController player)
		{
			player.GetComponent<ArtifactOfHatred.BaBoom>().Destroy();
			ArtifactOfHatred.guonHook.Dispose();
			GameManager.Instance.OnNewLevelFullyLoaded -= this.FixGuon;
			bool flag = this.m_extantOrbital != null;
			bool flag2 = flag;
			if (flag2)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
			return base.Drop(player);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001976C File Offset: 0x0001796C
		private void FixGuon()
		{
			bool flag = base.Owner && base.Owner.GetComponent<ArtifactOfHatred.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				base.Owner.GetComponent<ArtifactOfHatred.BaBoom>().Destroy();
			}
			bool flag4 = this.m_extantOrbital != null;
			bool flag5 = flag4;
			if (flag5)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
			PlayerController owner = base.Owner;
			owner.gameObject.AddComponent<ArtifactOfHatred.BaBoom>();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00019818 File Offset: 0x00017A18
		protected override void OnDestroy()
		{
			ArtifactOfHatred.guonHook.Dispose();
			bool flag = base.Owner && base.Owner.GetComponent<ArtifactOfHatred.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				base.Owner.GetComponent<ArtifactOfHatred.BaBoom>().Destroy();
			}
			GameManager.Instance.OnNewLevelFullyLoaded -= this.FixGuon;
			base.OnDestroy();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B3AF File Offset: 0x000095AF
		public static void GuonInit(Action<PlayerOrbital, PlayerController> orig, PlayerOrbital self, PlayerController player)
		{
			orig(self, player);
		}

		// Token: 0x040000D5 RID: 213
		public static Hook guonHook;

		// Token: 0x040000D6 RID: 214
		public static PlayerOrbital orbitalPrefab;

		// Token: 0x0200009E RID: 158
		private class BaBoom : BraveBehaviour
		{
			// Token: 0x060003BD RID: 957 RVA: 0x0002320E File Offset: 0x0002140E
			private void Start()
			{
				this.owner = base.GetComponent<PlayerController>();
			}

			// Token: 0x060003BE RID: 958 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
			public void Destroy()
			{
				UnityEngine.Object.Destroy(this);
			}

			// Token: 0x04000215 RID: 533
			private PlayerController owner;
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

        public ItemQuality Spawnquality { get; internal set; }

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
	}
}
