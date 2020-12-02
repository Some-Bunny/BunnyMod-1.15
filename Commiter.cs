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
	public class Commiter : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Commiter", "commiter");
			Game.Items.Rename("outdated_gun_mods:commiter", "bny:commiter");
			gun.gameObject.AddComponent<Commiter>();
			GunExt.SetShortDescription(gun, "True Dedication");
			GunExt.SetLongDescription(gun, "Starts off weak, gains power through execution.\n\nYoung gundead use these guns to learn the true value of commiting to a weapon. Though to fully demonstrate this, these guns were literally attached to them to they couldn't get rid of them.");
			GunExt.SetupSprite(gun, null, "commiter_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 36);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 2);
            GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(53) as Gun, true, false);
            gun.gunSwitchGroup = (PickupObjectDatabase.GetById(53) as Gun).gunSwitchGroup;
            gun.barrelOffset.transform.localPosition = new Vector3(2.1875f, 0.625f, 0f);
            gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0f;
			gun.DefaultModule.cooldownTime = .4f;
			gun.DefaultModule.numberOfShotsInClip = 50;
			gun.SetBaseMaxAmmo(50);
			gun.quality = PickupObject.ItemQuality.A;
			gun.DefaultModule.angleVariance = 0f;
			gun.DefaultModule.burstShotCount = 1;
			gun.CanBeDropped = false;
			gun.encounterTrackable.EncounterGuid = "gungeoneer gungeonfar this is dsaasdasdads";
            gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(379) as Gun).muzzleFlashEffects;
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
            projectile.DefaultTintColor = new Color(2f, 0f, 2f).WithAlpha(0.4f);
            projectile.HasDefaultTint = true;
            projectile.baseData.damage = 1f;
			projectile.baseData.speed *= 0.9f;
			projectile.AdditionalScaleMultiplier = 1.1f;
			projectile.pierceMinorBreakables = true;
            projectile.shouldRotate = true;
			projectile.transform.parent = gun.barrelOffset;
            ETGMod.Databases.Items.Add(gun, null, "ANY");
			gun.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
		}

		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.baseData.damage = (1f *(1f + Killed));
			projectile.OnWillKillEnemy = (Action<Projectile, SpeculativeRigidbody>)Delegate.Combine(projectile.OnWillKillEnemy, new Action<Projectile, SpeculativeRigidbody>(this.OnKill));
		}
		private void OnKill(Projectile arg1, SpeculativeRigidbody arg2)
		{
			bool flag = !arg2.aiActor.healthHaver.IsDead;
			if (flag)
			{
                {
					this.Killed += 1;
				}
			}	
		}
		private float Killed;
	}
}
