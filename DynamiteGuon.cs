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
	internal class DynamiteGuon : IounStoneOrbitalItem
	{
		public static void Init()
		{
			string name = "Dynamite Guon Stone";
			string resourcePath = "BunnyMod/Resources/DynamiteGuon/dynamiteguonstone";
			GameObject gameObject = new GameObject();
			DynamiteGuon boomGuon = gameObject.AddComponent<DynamiteGuon>();
			ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
			string shortDesc = "Floating OSHA Violation";
			string longDesc = "A Guon Stone that has been wrapped in several sticks of dynamite. Boom!";
			boomGuon.SetupItem(shortDesc, longDesc, "bny");
			boomGuon.quality = PickupObject.ItemQuality.B;
			DynamiteGuon.BuildPrefab();
			boomGuon.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			boomGuon.OrbitalPrefab = DynamiteGuon.orbitalPrefab;
			boomGuon.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
		}

		public static void BuildPrefab()
		{
			bool flag = DynamiteGuon.orbitalPrefab != null;
			if (!flag)
			{
				GameObject gameObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/DynamiteGuon/dynamiteguonstonefloaty", null, true);
				gameObject.name = "Dynamite Guon Stone";
				SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(10, 10));
				speculativeRigidbody.CollideWithTileMap = false;
				speculativeRigidbody.CollideWithOthers = true;
				speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
				DynamiteGuon.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
				DynamiteGuon.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
				DynamiteGuon.orbitalPrefab.shouldRotate = false;
				DynamiteGuon.orbitalPrefab.orbitRadius = 4f;
				DynamiteGuon.orbitalPrefab.orbitDegreesPerSecond = 60f;
				DynamiteGuon.orbitalPrefab.SetOrbitalTier(0);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				FakePrefab.MarkAsFakePrefab(gameObject);
				gameObject.SetActive(false);
			}
		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			DynamiteGuon.guonHook = new Hook(typeof(PlayerOrbital).GetMethod("Initialize"), typeof(DynamiteGuon).GetMethod("GuonInit"));
			bool flag = player.gameObject.GetComponent<DynamiteGuon.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				player.gameObject.GetComponent<DynamiteGuon.BaBoom>().Destroy();
			}
			player.gameObject.AddComponent<DynamiteGuon.BaBoom>();
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
				bool flag3 = component != null && !(component.Owner is PlayerController);
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = !DynamiteGuon.onCooldown;
					bool flag6 = flag5;
					if (flag6)
                    {
						DynamiteGuon.onCooldown = true;
						GameManager.Instance.StartCoroutine(GuonGeon.StartCooldown(base.Owner));
						{
							this.Boom(myRigidbody.sprite.WorldCenter);
						}
					}
				}
			}
		}
		public static IEnumerator StartCooldown(PlayerController player)
		{
			yield return new WaitForSeconds(1f);
			DynamiteGuon.onCooldown = false;
			yield break;
		}
		private static bool onCooldown;


		// Token: 0x0600025B RID: 603 RVA: 0x000196DC File Offset: 0x000178DC
		public override DebrisObject Drop(PlayerController player)
		{
			player.GetComponent<DynamiteGuon.BaBoom>().Destroy();
			DynamiteGuon.guonHook.Dispose();
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
			bool flag = base.Owner && base.Owner.GetComponent<DynamiteGuon.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				base.Owner.GetComponent<DynamiteGuon.BaBoom>().Destroy();
			}
			bool flag4 = this.m_extantOrbital != null;
			bool flag5 = flag4;
			if (flag5)
			{
				SpeculativeRigidbody specRigidbody = this.m_extantOrbital.GetComponent<PlayerOrbital>().specRigidbody;
				specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.OnPreCollison));
			}
			PlayerController owner = base.Owner;
			owner.gameObject.AddComponent<DynamiteGuon.BaBoom>();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00019818 File Offset: 0x00017A18
		protected override void OnDestroy()
		{
			DynamiteGuon.guonHook.Dispose();
			bool flag = base.Owner && base.Owner.GetComponent<DynamiteGuon.BaBoom>() != null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				base.Owner.GetComponent<DynamiteGuon.BaBoom>().Destroy();
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
		public void Boom(Vector3 position)
		{
			ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 0.6f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 10f,
			doExplosionRing = true,
			doDestroyProjectiles = true,
			doForce = true,
			debrisForce = 5f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = false,
			playDefaultSFX = true,
		};
	}
}
