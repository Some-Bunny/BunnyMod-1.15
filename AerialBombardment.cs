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
	public class AerialBombardment : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Aerial Bombardment", "aerialbombardment");
			Game.Items.Rename("outdated_gun_mods:aerial_bombardment", "bny:aerial_bombardment");
			gun.gameObject.AddComponent<AerialBombardment>();
			gun.SetShortDescription("Unfortunate Gun");
			gun.SetLongDescription("A pocket device for calling in airstrikes. Pressing the button plays a cool riff!");
			gun.SetupSprite(null, "aerialbombardment_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 32);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 100);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 2);
			for (int i = 0; i < 9; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(38) as Gun, true, false);
			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 0.9f;
				projectileModule.angleVariance = 45f;
				projectileModule.numberOfShotsInClip = 1;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				PierceProjModifier pierce = projectile.gameObject.AddComponent<PierceProjModifier>();
				pierce.penetration = 10;
				projectile.baseData.range = (UnityEngine.Random.Range(3f, 9f));
				pierce.penetratesBreakables = true;
				projectile.baseData.damage = 0;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				gun.DefaultModule.projectiles[0] = projectile;
				bool flag = projectileModule != gun.DefaultModule;
				if (flag)
				{
					projectileModule.ammoCost = 0;
				}
				projectile.transform.parent = gun.barrelOffset;
			}
			gun.reloadTime = 1.2f;
			gun.SetBaseMaxAmmo(70);
			// Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
			gun.quality = PickupObject.ItemQuality.C;
			gun.encounterTrackable.EncounterGuid = "Cheer up Bunny ^ᴗ^";
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
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.sprite.renderer.enabled = false;
			projectile.baseData.range = (UnityEngine.Random.Range(3f, 9f));
			projectile.OnDestruction += this.BAUNAUUUUBAUNAUUUUUU;
		}
		private void BAUNAUUUUBAUNAUUUUUU(Projectile obj)
		{
			this.WEEEE(obj.specRigidbody.UnitCenter);
		}
		public void WEEEE(Vector3 position)
		{
			ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 3f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 5f,
			doExplosionRing = true,
			doDestroyProjectiles = false,
			doForce = false,
			debrisForce = 0f,
			preventPlayerForce = true,
			explosionDelay = 0.1f,
			usesComprehensiveDelay = false,
			doScreenShake = true,
			playDefaultSFX = true,
		};

		public override void OnPostFired(PlayerController player, Gun bruhgun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_Punchout_Punch_Hit_01", gameObject);

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
				AkSoundEngine.PostEvent("Play_OBJ_supplydrop_activate_01", gameObject);
			}
		}
	}
}