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
	public class SausageRevolver : GunBehaviour
	{
		public static void Add()
		{
			Gun suasagerevolver = ETGMod.Databases.Items.NewGun("6 Chamber Sausage Shooter", "sausagerevolver");
			Game.Items.Rename("outdated_gun_mods:6_chamber_sausage_shooter", "bny:6_chamber_sausage_shooter");
			suasagerevolver.gameObject.AddComponent<SausageRevolver>();
			GunExt.SetShortDescription(suasagerevolver, "Made from questionable sources");
			GunExt.SetLongDescription(suasagerevolver, "A sausage revolver made mostly of sausage. It's quite disgusting to hold.");
			GunExt.SetupSprite(suasagerevolver, null, "sausagerevolver_idle_001", 19);
			GunExt.SetAnimationFPS(suasagerevolver, suasagerevolver.shootAnimation, 24);
			GunExt.SetAnimationFPS(suasagerevolver, suasagerevolver.reloadAnimation, 10);
			GunExt.SetAnimationFPS(suasagerevolver, suasagerevolver.idleAnimation, 3);
			GunExt.AddProjectileModuleFrom(suasagerevolver, "ak-47", true, false);
			suasagerevolver.DefaultModule.ammoCost = 1;
			suasagerevolver.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			suasagerevolver.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			suasagerevolver.reloadTime = .9f;
			suasagerevolver.DefaultModule.cooldownTime = .1f;
			suasagerevolver.DefaultModule.numberOfShotsInClip = 6;
			suasagerevolver.SetBaseMaxAmmo(360);
			suasagerevolver.quality = PickupObject.ItemQuality.C;
			suasagerevolver.DefaultModule.angleVariance = 10f;
			suasagerevolver.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
			suasagerevolver.encounterTrackable.EncounterGuid = "sausage_revolver";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(suasagerevolver.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			suasagerevolver.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage *= 1f;
			projectile.baseData.speed *= 1.1f;
			projectile.transform.parent = suasagerevolver.barrelOffset;
			projectile.SetProjectileSpriteRight("sausagerevolver_projectile_001", 9, 3, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(7), new int?(7), null, null, null);
			ETGMod.Databases.Items.Add(suasagerevolver, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun suasagerevolver)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_fish_impact_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun suasagerevolver, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_ENM_blobulord_reform_01", gameObject);
			}
		}
	}
}