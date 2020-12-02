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
	public class Valkyrie : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Valkyrie", "valkyrie");
			Game.Items.Rename("outdated_gun_mods:valkyrie", "bny:valkyrie");
			gun.gameObject.AddComponent<Valkyrie>();
			gun.SetShortDescription("Lay Waste To All");
			gun.SetLongDescription("Through the bullets and the missiles we carry on.\n\nDestroy those who stand before you.");
			gun.SetupSprite(null, "valkyrie_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 22);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 5);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			for (int i = 0; i < 3; i++)
			{
				GunExt.AddProjectileModuleFrom(gun, "rpg", true, false);
			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.Burst;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 0.5f;
				projectileModule.angleVariance = 25f;
				projectileModule.numberOfShotsInClip = 4;
				projectileModule.burstShotCount = 4;
				projectileModule.burstCooldownTime = 0.6f;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				projectileModule.projectiles[0] = projectile;
				PierceProjModifier pierce = projectile.gameObject.AddComponent<PierceProjModifier>();
				projectile.baseData.range = 1000f;
				pierce.penetratesBreakables = true;
				projectile.baseData.damage = 30f;
				projectile.AdditionalScaleMultiplier = 1f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				bool flag = projectileModule != gun.DefaultModule;
				if (flag)
				{
					projectileModule.ammoCost = 0;
				}
				projectile.transform.parent = gun.barrelOffset;
			}
			gun.carryPixelOffset += new IntVector2((int)5f, (int)2f);
			gun.reloadTime = 2.7f;
			gun.SetBaseMaxAmmo(100);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(384) as Gun).muzzleFlashEffects;
			gun.quality = PickupObject.ItemQuality.S;
			gun.encounterTrackable.EncounterGuid = "FIREEEEEE BRING FIIIIIIRE";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			List<string> mandatoryConsoleIDs = new List<string>
			{
				"bny:valkyrie"
			};
			List<string> optionalConsoleIDs = new List<string>
			{
				"napalm_strike",
				"ibomb_companion_app",
				"roll_bomb",
				"air_strike"
			};
			CustomSynergies.Add("Armageddon", mandatoryConsoleIDs, optionalConsoleIDs, true);
			List<string> mandatoryConsoleIDs1 = new List<string>
			{
				"bny:valkyrie"
			};
			List<string> optionalConsoleIDs1 = new List<string>
			{
				"blast_helmet",
				"melted_rock",
				"explosive_rounds",
				"cluster_mine"
			};
			CustomSynergies.Add("Boiling Veins", mandatoryConsoleIDs1, optionalConsoleIDs1, true);
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			bool flagA = player.PlayerHasActiveSynergy("Armageddon");
			if (flagA)
			{
				HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
				homing.HomingRadius = 250f;
				homing.AngularVelocity = 250f;
			}
			projectile.OnDestruction += this.kerboomer;
		}

		private void kerboomer(Projectile projectile)
		{
			this.Blam(projectile.sprite.WorldCenter);

			//AkSoundEngine.PostEvent("Play_OBJ_nuke_blast_01", gameObject);
			//Exploder.DoDefaultExplosion(projectile.specRigidbody.UnitTopCenter, default(Vector2), null, false, CoreDamageTypes.None, true);
		}

		public override void OnPostFired(PlayerController player, Gun bruhgun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_comm4nd0_shot_01", gameObject);

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
					bool flagA = player.PlayerHasActiveSynergy("Boiling Veins");
					if (flagA)
					{
						for (int counter = 0; counter < 8; counter++)
						{
							Projectile projectile = ((Gun)ETGMod.Databases.Items[16]).DefaultModule.projectiles[0];
							Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
							Vector3 vector2 = player.specRigidbody.UnitCenter;
							GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((player.CurrentGun == null) ? 1.2f : player.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-30, 30)), true);
							Projectile component = gameObject.GetComponent<Projectile>();
							if (flag)
							{
								component.Owner = player;
								component.Shooter = player.specRigidbody;
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
		public void Blam(Vector3 position)
		{
			AkSoundEngine.PostEvent("Play_OBJ_nuke_blast_01", gameObject);
			ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 4f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 40f,
			doExplosionRing = true,
			doDestroyProjectiles = false,
			doForce = true,
			debrisForce = 0f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = false,
			playDefaultSFX = true,
		};
	}
}	