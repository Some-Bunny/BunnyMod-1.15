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
//thanks Hunter for the help!
namespace BunnyMod
{
	public class GunslayerShotgun : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Gunslayers Shotgun", "gunslayersshotgun");
			Game.Items.Rename("outdated_gun_mods:gunslayers_shotgun", "bny:gunslayers_shotgun");
			gun.gameObject.AddComponent<GunslayerShotgun>();
			gun.SetShortDescription("Super Shotgun");
			gun.SetLongDescription("The shotgun once wielded by the Gunslayer on his last clear of the Gungeon. Instead of using normal ammunition, it super-heats air canisters and fires them out.");
			gun.SetupSprite(null, "gunslayersshotgun_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 4);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			for (int i = 0; i < 10; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(125) as Gun, true, false);
			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 1.1f;
				projectileModule.angleVariance = 20f;
				projectileModule.numberOfShotsInClip = 4;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				PierceProjModifier pierce = projectile.gameObject.AddComponent<PierceProjModifier>();
				pierce.penetration = 0;
				projectile.baseData.range = 7f;
				pierce.penetratesBreakables = true;
				projectile.baseData.damage = 12f;
				projectile.AdditionalScaleMultiplier = 0.5f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				gun.DefaultModule.projectiles[0] = projectile;
				bool flag = projectileModule != gun.DefaultModule;
				if (flag)
				{
					projectileModule.ammoCost = 0;
				}
				projectile.transform.parent = gun.barrelOffset;
				projectile.SetProjectileSpriteRight("flakcannon_projectile_001", 14, 14, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(7), new int?(7), null, null, null);

			}
			gun.reloadTime = 1.7f;
			gun.SetBaseMaxAmmo(120);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(384) as Gun).muzzleFlashEffects;

			// Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
			gun.quality = PickupObject.ItemQuality.A;
			gun.encounterTrackable.EncounterGuid = "Cheer up Bunny ^ᴗ^ (i dont want to change this, at the very least not remove it)";
			//thanks Hunter, i really needed that, i wish you the best, cause you da best o7

			//This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
			//This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
			//projectile.baseData allows you to modify the base properties of your projectile module.
			//In our case, our gun uses modified projectiles from the ak-47.
			//Setting static values for a custom gun's projectile stats prevents them from scaling with player stats and bullet modifiers (damage, shotspeed, knockback)
			//You have to multiply the value of the original projectile you're using instead so they scale accordingly. For example if the projectile you're using as a base has 10 damage and you want it to be 6 you use this
			//In our case, our projectile has a base damage of 5.5, so we multiply it by 1.1 so it does 10% more damage from the ak-47.
			//This determines what sprite you want your projectile to use. Note this isn't necessary if you don't want to have a custom projectile sprite.
			//The x and y values determine the size of your custom projectile
			ETGMod.Databases.Items.Add(gun, null, "ANY");

		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.baseData.range = (UnityEngine.Random.Range(5f, 10f));
		}

		public override void OnPostFired(PlayerController player, Gun bruhgun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_golddoublebarrelshotgun_shot_01", gameObject);

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

		public override void OnReloadPressed(PlayerController player, Gun bruhgun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_shotgun_reload", gameObject);
			}
		}
	}

}