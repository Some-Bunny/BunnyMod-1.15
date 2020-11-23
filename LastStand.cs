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
	public class LastStand : AdvancedGunBehavior
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Last Stand", "laststand");
			Game.Items.Rename("outdated_gun_mods:last_stand", "bny:last_stand");
			gun.gameObject.AddComponent<LastStand>();
			GunExt.SetShortDescription(gun, "The Danger Zone");
			GunExt.SetLongDescription(gun, "Masterfully cobbled together by a struggling crew, this gun was made to hold off powerful enemies when at the brink of death.");
			GunExt.SetupSprite(gun, null, "laststand_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 36);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 7);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(38) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(38) as Gun).gunSwitchGroup;
			gun.DefaultModule.burstShotCount = 2;
			gun.DefaultModule.burstCooldownTime = 0.1f;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Burst;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.8f;
			gun.DefaultModule.cooldownTime = .25f;
			gun.DefaultModule.numberOfShotsInClip = 20;
			gun.SetBaseMaxAmmo(500);
			gun.quality = PickupObject.ItemQuality.B;
			gun.DefaultModule.angleVariance = 0f;
			gun.encounterTrackable.EncounterGuid = "*Screams in Hacked Lv.1 Oxygen System on Flagship*";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 5f;
			projectile.baseData.speed = 25f;
			projectile.baseData.range *= 10f;
			projectile.baseData.force *= 0.3f;
			projectile.HasDefaultTint = true;
			projectile.AdditionalScaleMultiplier = 1.1f;
			projectile.pierceMinorBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");

		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = projectile.Owner as PlayerController;
			bool flag = ((double)player.healthHaver.GetCurrentHealth() == 0.5 && player.healthHaver.Armor == 0f) || player.healthHaver.GetCurrentHealth() == 0f && player.healthHaver.Armor == 1f;
			if (flag)
			{
				projectile.HasDefaultTint = true;
				projectile.DefaultTintColor = new Color(7f, 0f, 0f).WithAlpha(0.5f);
				projectile.baseData.damage *= 4f;
            }
		}
	

		private bool HasReloaded;


		public override void OnReloadPressed(PlayerController player, Gun staffyeah, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_yarirocketlauncher_reload_01", base.gameObject);
			}
		}
	}
}
