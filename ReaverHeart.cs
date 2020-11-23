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
using MonoMod;



namespace BunnyMod
{
	public class ReaverHeart : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun flakcannon = ETGMod.Databases.Items.NewGun("Reaver Heart", "reaverheart");
			Game.Items.Rename("outdated_gun_mods:reaver_heart", "bny:reaver_heart");
			flakcannon.gameObject.AddComponent<ReaverHeart>();
			GunExt.SetShortDescription(flakcannon, "A Glacier Eventually...");
			GunExt.SetLongDescription(flakcannon, "The still-beating heart of a Void Reaver, an ancient guardian of a time-looping planet. It slowly pulsates with energy funneled straight from the Void.");
			GunExt.SetupSprite(flakcannon, null, "reaverheart_idle_001", 11);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.shootAnimation, 35);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.reloadAnimation, 1);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.idleAnimation, 2);
			GunExt.AddProjectileModuleFrom(flakcannon, PickupObjectDatabase.GetById(223) as Gun, true, false);
			flakcannon.DefaultModule.ammoCost = 1;
			flakcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			flakcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			flakcannon.reloadTime = 2f;
			flakcannon.barrelOffset.transform.localPosition = new Vector3(.375f, 0.375f, 0f);
			flakcannon.DefaultModule.cooldownTime = 1.1f;
			flakcannon.DefaultModule.numberOfShotsInClip = 5;
			flakcannon.SetBaseMaxAmmo(200);
			flakcannon.quality = PickupObject.ItemQuality.C;
			flakcannon.DefaultModule.angleVariance = 0f;
			flakcannon.DefaultModule.burstShotCount = 1;
			flakcannon.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
			flakcannon.AddToSubShop(ItemBuilder.ShopType.OldRed, 1f);
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(flakcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			flakcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 3f;
			projectile.baseData.speed *= 0.1f;
			projectile.AdditionalScaleMultiplier = 0.1f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.baseData.range = 0.01f;
			projectile.sprite.renderer.enabled = false;
			ExplosiveModifier explosiveModifier = projectile.gameObject.AddComponent<ExplosiveModifier>();
			explosiveModifier.doExplosion = true;
			explosiveModifier.explosionData = ReaverHeart.ReaverExplosion;
			flakcannon.encounterTrackable.EncounterGuid = "Void reavers be like 'waaaaaaAAAAAAAAAAAAAA- BWOOM WAUU WAUU'";
			ETGMod.Databases.Items.Add(flakcannon, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun flakcannon)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_wall_slam_01", gameObject);
		}
		private bool HasReloaded;

		protected void Update()
		{
			if (gun.CurrentOwner)
			{

				if (!gun.PreventNormalFireAudio)
				{
					this.gun.PreventNormalFireAudio = true;
				}
				if (!gun.IsReloading && !HasReloaded)
				{
					this.HasReloaded = true;
				}
			}
		}

		public override void OnReloadPressed(PlayerController player, Gun flakcannon, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_OBJ_blackhole_close_01", gameObject);

			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnDestruction += this.ReaverBeat;
		}
		private void ReaverBeat(Projectile projectile)
		{
			this.Boom(projectile.sprite.WorldCenter);
			bool flag = projectile != null;
			if (flag)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Vector2 a = playerController.sprite.WorldCenter - projectile.sprite.WorldCenter;
				playerController.knockbackDoer.ApplyKnockback(-a, 0f, false);
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[180]).DefaultModule.projectiles[0];
				ExplosiveModifier component = projectile.GetComponent<ExplosiveModifier>();
				ExplosionData explosionData = component.explosionData;
				component.doDistortionWave = true;
				component.explosionData.damage = 0f;
				Exploder.Explode(projectile.sprite.WorldCenter, explosionData, Vector2.zero, null, false, CoreDamageTypes.None, false);
				playerController.ForceBlank(0f, 0.5f, false, true, new Vector2?(projectile.sprite.WorldCenter), false, 300f);
			}

		}
		private static ExplosionData smallExplosion = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
		public void Boom(Vector3 position)
		{
			ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}

		// Token: 0x040000D7 RID: 215
		private static ExplosionData ReaverExplosion = new ExplosionData
		{
			effect = ReaverHeart.smallExplosion.effect,
			ignoreList = ReaverHeart.smallExplosion.ignoreList,
			ss = ReaverHeart.smallExplosion.ss,
			damageRadius = 0f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 0f,
			doDestroyProjectiles = false,
			doForce = true,
			debrisForce = 0f,
			preventPlayerForce = true,
			explosionDelay = 0.1f,
			usesComprehensiveDelay = false,
			doScreenShake = true,
			playDefaultSFX = false
		};
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 15f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 10f,
			doExplosionRing = false,
			doDestroyProjectiles = false,
			doForce = true,
			debrisForce = 2f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = false,
			playDefaultSFX = false,
		};

	}
}