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
	public class REXNeedler : AdvancedGunBehavior
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Robotic Injector", "directiveinject");
			Game.Items.Rename("outdated_gun_mods:robotic_injector", "bny:robotic_injector");
			gun.gameObject.AddComponent<REXNeedler>();
			GunExt.SetShortDescription(gun, "Directive: Inject");
			GunExt.SetLongDescription(gun, "An injector commonly used by hydroponic robots. Looks like its original carrier is off protocol.");
			GunExt.SetupSprite(gun, null, "directiveinject_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 36);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 7);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(26) as Gun, true, false);
			gun.gunHandedness = GunHandedness.HiddenOneHanded;
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(26) as Gun).gunSwitchGroup;
			gun.DefaultModule.burstShotCount = 3;
			gun.DefaultModule.burstCooldownTime = 0.0833f;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Burst;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0f;
			gun.DefaultModule.cooldownTime = .7f;
			gun.DefaultModule.numberOfShotsInClip = 300;
			gun.SetBaseMaxAmmo(300);
			gun.quality = PickupObject.ItemQuality.C;
			gun.DefaultModule.angleVariance = 0f;
			gun.encounterTrackable.EncounterGuid = "THE PLANT, THE PLANT, ITS THE PLANT";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.PoisonApplyChance = 1f;
			projectile.baseData.damage = 3f;
			projectile.baseData.speed = 20f;
			projectile.baseData.range *= 10f;
			projectile.baseData.force *= 0.3f;
			projectile.HasDefaultTint = true;
			projectile.SetProjectileSpriteRight("directiveinject_projectile_001", 11, 3, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(8), new int?(2), null, null, null);
			projectile.AdditionalScaleMultiplier = 1.1f;
			projectile.pierceMinorBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.damageTypes |= CoreDamageTypes.Poison;
			PoisonForDummiesLikeMe auegh = projectile.gameObject.AddComponent<PoisonForDummiesLikeMe>();
			auegh.procChance = 1;
			auegh.useSpecialTint = false;
			ETGMod.Databases.Items.Add(gun, null, "ANY");

		}
		public override void PostProcessProjectile(Projectile projectile)
		{

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
		public GunHandedness HiddenOneHanded;
	}
}
