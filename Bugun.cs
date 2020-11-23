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
	public class Bugun : GunBehaviour
	{
		public static void ThisIsBasicallyCelsRNGUNButTakenToASillyLevel()
		{

			Gun randomGun = PickupObjectDatabase.GetRandomGun();
			Bugun.bugun.muzzleFlashEffects = randomGun.muzzleFlashEffects;
			Bugun.bugun.gunSwitchGroup = randomGun.gunSwitchGroup;
			//GunExt.AddProjectileModuleFrom(randomGun, PickupObjectDatabase.GetRandomGun(), true, false);
			Material material = bugun.sprite.renderer.material;
			float fuckedupness = UnityEngine.Random.Range(0.07f, 0.35f);
			material.shader = ShaderCache.Acquire("Brave/Internal/Glitch");
			material.SetFloat("_GlitchInterval", fuckedupness);
			material.SetFloat("_DispProbability", fuckedupness);
			material.SetFloat("_DispIntensity", fuckedupness);
			material.SetFloat("_ColorProbability", fuckedupness);
			material.SetFloat("_ColorIntensity", fuckedupness);
		}
		public static void Add()
		{

			Gun boomrevolver = ETGMod.Databases.Items.NewGun("Bugun", "bugun");
			Game.Items.Rename("outdated_gun_mods:bugun", "bny:bugun");
			boomrevolver.gameObject.AddComponent<Bugun>();
			GunExt.SetShortDescription(boomrevolver, "Oh god what the hell?");
			GunExt.SetLongDescription(boomrevolver, "A gun so detached from this reality that it has merged with another dimension. ");
			GunExt.SetupSprite(boomrevolver, null, "bugun_idle_001", 19);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.shootAnimation, 15);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.reloadAnimation, 5);
			GunExt.SetAnimationFPS(boomrevolver, boomrevolver.idleAnimation, 10);
			GunExt.AddProjectileModuleFrom(boomrevolver, "ak-47", true, false);
			Bugun.bugun = boomrevolver;
			//boomrevolver.gunSwitchGroup = (PickupObjectDatabase.GetRandomGun() as Gun).gunSwitchGroup;
			//boomrevolver.muzzleFlashEffects = (PickupObjectDatabase.GetRandomGun() as Gun).muzzleFlashEffects;
			boomrevolver.DefaultModule.ammoCost = 1;
			boomrevolver.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			boomrevolver.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			boomrevolver.reloadTime = 1.2f;
			boomrevolver.DefaultModule.cooldownTime = .07f;	
			boomrevolver.DefaultModule.numberOfShotsInClip = 10;
			boomrevolver.SetBaseMaxAmmo(450);
			boomrevolver.quality = PickupObject.ItemQuality.C;
			boomrevolver.DefaultModule.angleVariance = 5f;
			boomrevolver.sprite.usesOverrideMaterial = true;
			boomrevolver.encounterTrackable.EncounterGuid = "NullReferenceException: Object Reference not set to an instance of an object.";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(boomrevolver.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			boomrevolver.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage = 11f;
			projectile.baseData.speed *= 1f;
			projectile.transform.parent = boomrevolver.barrelOffset;
			ETGMod.Databases.Items.Add(boomrevolver, null, "ANY");
			Bugun.ThisIsBasicallyCelsRNGUNButTakenToASillyLevel();
		}

		private bool HasReloaded;
		public override void OnPostFired(PlayerController owner, Gun boomrevolver)
		{
			int barrel = UnityEngine.Random.Range(-3, 3);
			int barrel2 = UnityEngine.Random.Range(-3, 3);
			int carry = UnityEngine.Random.Range(-30, 30);
			int carry2 = UnityEngine.Random.Range(-30, 30);
			boomrevolver.barrelOffset.transform.localPosition = new Vector3(barrel, barrel2, 0f);
			boomrevolver.carryPixelOffset = new IntVector2(carry, carry2);
			Bugun.ThisIsBasicallyCelsRNGUNButTakenToASillyLevel();
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			float size = UnityEngine.Random.Range(0.7f, 6f);
			float speed = UnityEngine.Random.Range(0.4f, 3f);
			projectile.baseData.speed *= speed;
			projectile.AdditionalScaleMultiplier *= size;
		}
		protected void Update()
		{
			bool flag = this.gun.CurrentOwner;
			if (flag)
			{
				bool flag2 = !this.gun.IsReloading && !this.HasReloaded;
				if (flag2)
				{
					Bugun.ThisIsBasicallyCelsRNGUNButTakenToASillyLevel();
					this.HasReloaded = true;
				}
			}
		}

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			bool flag = this.HasReloaded;
			if (flag)
			{
				this.HasReloaded = false;
			}
			base.OnReloadPressed(player, gun, bSOMETHING);
		}
		private static Gun bugun;

	}
}