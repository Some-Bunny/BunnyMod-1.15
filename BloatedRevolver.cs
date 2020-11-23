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
	public class BloatedRevolver : GunBehaviour
	{
		public static void Add()
		{
			Gun boomrevolver = ETGMod.Databases.Items.NewGun("Bloated Revolver", "bloatedrevolver");
			Game.Items.Rename("outdated_gun_mods:bloated_revolver", "bny:bloated_revolver");
			boomrevolver.gameObject.AddComponent<BloatedRevolver>();
			GunExt.SetShortDescription(boomrevolver, "A bit too full.");
			GunExt.SetLongDescription(boomrevolver, "A normal revolver that's been packed with so much gunpowder that it explodes if theres nothing left to shoot. Only the forces of the Gungeon prevent the self-destruction of the weapon.");
			GunExt.SetupSprite(boomrevolver, null, "bloatedrevolver_idle_001", 19);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.shootAnimation, 24);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.reloadAnimation, 10);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.idleAnimation, 3);
			GunExt.AddProjectileModuleFrom(boomrevolver, "magnum", true, false);
			boomrevolver.gunSwitchGroup = (PickupObjectDatabase.GetById(38) as Gun).gunSwitchGroup;
			boomrevolver.DefaultModule.ammoCost = 1;
			boomrevolver.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			boomrevolver.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			boomrevolver.reloadTime = 1.2f;
			boomrevolver.DefaultModule.cooldownTime = .1f;
			boomrevolver.DefaultModule.numberOfShotsInClip = 7;
			boomrevolver.SetBaseMaxAmmo(350);
			boomrevolver.quality = PickupObject.ItemQuality.D;
			boomrevolver.DefaultModule.angleVariance = 5f;
			boomrevolver.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			boomrevolver.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			boomrevolver.encounterTrackable.EncounterGuid = "bloated_revolveryeaahhhhhhh";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(boomrevolver.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			boomrevolver.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage *= .75f;
			projectile.baseData.speed *= 0.9f;
			projectile.transform.parent = boomrevolver.barrelOffset;
			ETGMod.Databases.Items.Add(boomrevolver, null, "ANY");
		}

		private bool HasReloaded;

		protected void Update()
		{
			bool flag = this.gun.CurrentOwner;
			if (flag)
			{
				bool flag2 = !this.gun.IsReloading && !this.HasReloaded;
				if (flag2)
				{
					this.HasReloaded = true;
				}
			}
		}

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			bool flag = this.HasReloaded && gun.ClipShotsRemaining == 0;
			if (flag)
			{
				this.HasReloaded = false;
				this.Boom(player.sprite.WorldCenter);
			}
			base.OnReloadPressed(player, gun, bSOMETHING);
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
			damageRadius = 1f,
			damageToPlayer = 1f,
			doDamage = true,
			damage = 10f,
			doExplosionRing = true,
			doDestroyProjectiles = false,
			doForce = false,
			debrisForce = 0f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = false,
			playDefaultSFX = true,
		};
	}
}