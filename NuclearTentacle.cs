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
	public class NuclearTentacle : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun flakcannon = ETGMod.Databases.Items.NewGun("Nuclear Tentacle", "nucleartentacle");
			Game.Items.Rename("outdated_gun_mods:nuclear_tentacle", "bny:nuclear_tentacle");
			flakcannon.gameObject.AddComponent<NuclearTentacle>();
			GunExt.SetShortDescription(flakcannon, "RWAAAAAAAAAAAAAAA!");
			GunExt.SetLongDescription(flakcannon, "A big, radioactive tentacle that spews large energy balls, what's not to love?");
			GunExt.SetupSprite(flakcannon, null, "nucleartentacle_idle_001", 21);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.shootAnimation, 16);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.reloadAnimation, 8);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.idleAnimation, 3);
			GunExt.AddProjectileModuleFrom(flakcannon, "magnum", true, false);
			flakcannon.DefaultModule.ammoCost = 1;
			flakcannon.barrelOffset.transform.localPosition = new Vector3(2.125f, 0.6825f, 0f);
			flakcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			flakcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			flakcannon.reloadTime = 1.8f;
			flakcannon.DefaultModule.cooldownTime = .4f;
			flakcannon.DefaultModule.numberOfShotsInClip = 5;
			flakcannon.SetBaseMaxAmmo(90);
			flakcannon.quality = PickupObject.ItemQuality.A;
			flakcannon.DefaultModule.angleVariance = 0f;
			flakcannon.DefaultModule.burstShotCount = 1;
			flakcannon.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(flakcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			flakcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 40f;
			projectile.baseData.speed *= 1.2f;
			projectile.AdditionalScaleMultiplier = 1f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			Booletgobeeeg booletgobeeeg = projectile.gameObject.AddComponent<Booletgobeeeg>();
			projectile.SetProjectileSpriteRight("nucleartentacle_projectile_001", 32, 32, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(22), new int?(22), null, null, null);
			flakcannon.encounterTrackable.EncounterGuid = "tentales gun, yeahhhhhhhhhhhhhhhhhhhhhh!";
			ETGMod.Databases.Items.Add(flakcannon, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun flakcannon)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_Rat_Cheese_Burst_01", gameObject);
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
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_VO_bombshee_death_01", gameObject);
			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnDestruction += this.soundeffectmoment;
		}
		private void soundeffectmoment(Projectile projectile)
		{
			this.Boom(projectile.sprite.WorldCenter);
			AkSoundEngine.PostEvent("Play_OBJ_blackhole_close_01", gameObject);
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
			damageRadius = 5f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 20f,
			doExplosionRing = true,
			doDestroyProjectiles = false,
			doForce = true,
			debrisForce = 20f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = false,
			playDefaultSFX = true,
		};
	}
}