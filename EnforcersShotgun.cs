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
	public class EnforcersLaw : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Enforcers Law", "enforcersgun");
			Game.Items.Rename("outdated_gun_mods:enforcers_law", "bny:enforcers_law");
			gun.gameObject.AddComponent<EnforcersLaw>();
			gun.SetShortDescription("Enforce Of Nature!");
			gun.SetLongDescription("A common, yet effective shotgun type used by Enforcers to supress riots, or whole alien populations. Usually accompanied by riot shields.");
			gun.SetupSprite(null, "enforcersgun_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 5);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			for (int i = 0; i < 6; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(35) as Gun, true, false);
			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.Burst;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 0.9f;
				projectileModule.angleVariance = 10f;
				projectileModule.numberOfShotsInClip = 6;
				projectileModule.burstShotCount = 2;
				projectileModule.burstCooldownTime = 0.15f;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				projectileModule.projectiles[0] = projectile;
				PierceProjModifier pierce = projectile.gameObject.AddComponent<PierceProjModifier>();
				pierce.penetration = 5;
				projectile.baseData.range = 9f;
				pierce.penetratesBreakables = true;
				projectile.baseData.damage = 5f;
				projectile.AdditionalScaleMultiplier = 0.5f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				bool flag = projectileModule != gun.DefaultModule;
				if (flag)
				{
					projectileModule.ammoCost = 0;
				}
				projectile.transform.parent = gun.barrelOffset;
			}
			gun.carryPixelOffset += new IntVector2((int)5f, (int)-1f);
			gun.reloadTime = 2.3f;
			gun.SetBaseMaxAmmo(180);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(93) as Gun).muzzleFlashEffects;
			gun.quality = PickupObject.ItemQuality.B;
			gun.encounterTrackable.EncounterGuid = "WEE WOO WEE WOO WEE WOO WEE WOO";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			List<string> mandatoryConsoleIDs1 = new List<string>
			{
				"bny:enforcers_law"
			};
			List<string> optionalConsoleID1s = new List<string>
			{
				"betrayers_shield",
				"chaff_grenade",
				"laser_sight",
				"shield_of_the_maiden",
				"armor_of_thorns",
				"heavy_boots",
				"gunboots",
				"blast_helmet",
				"galactic_medal_of_valor",
				"full_metal_jacket",
				"crisis_stone"
			};
			CustomSynergies.Add("Protect And Serve", mandatoryConsoleIDs1, optionalConsoleID1s, true);
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			//projectile.baseData.damage = 5f;
			//projectile.AdditionalScaleMultiplier = 0.8f;
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
				bool flag = this.HasReloaded && gun.ClipShotsRemaining == 0;
				if (flag)
				{
					bool flag2 = !EnforcersLaw.onCooldown;
					if (flag2)
                    {
						bool flagA = player.PlayerHasActiveSynergy("Protect And Serve");
						if (flagA)
						{
							Gun gun = PickupObjectDatabase.GetById(380) as Gun;
							Gun currentGun = player.CurrentGun;
							GameObject gameObject = gun.ObjectToInstantiateOnReload.gameObject;
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, player.sprite.WorldCenter, Quaternion.identity);
							SingleSpawnableGunPlacedObject @interface = gameObject2.GetInterface<SingleSpawnableGunPlacedObject>();
							BreakableShieldController component = gameObject2.GetComponent<BreakableShieldController>();
							bool flag3 = gameObject2;
							if (flag3)
							{
								@interface.Initialize(currentGun);
								component.Initialize(currentGun);
								EnforcersLaw.onCooldown = true;
								GameManager.Instance.StartCoroutine(EnforcersLaw.StartCooldown());
							}
						}
					}	
				}
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_shotgun_reload", gameObject);
			}
		}
		private static IEnumerator StartCooldown()
		{
			yield return new WaitForSeconds(10f);
			EnforcersLaw.onCooldown = false;
			yield break;
		}
		private static bool onCooldown;
	}
}