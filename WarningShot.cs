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
	public class WarningShot : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Warning Shot", "warningshot");
			Game.Items.Rename("outdated_gun_mods:warning_shot", "bny:warning_shot");
			gun.gameObject.AddComponent<WarningShot>();
			GunExt.SetShortDescription(gun, "Calm Before The Storm");
			GunExt.SetLongDescription(gun, "An intimidating gun once wielded by a non-intimidating gunslinger. Legends say those who heard the whistle of the gun were doomed.");
			GunExt.SetupSprite(gun, null, "warningshot_idle_001", 19);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 14);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, "magnum", true, false);	

			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(38) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.6f;
			gun.DefaultModule.cooldownTime = 1.8f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(100);
			gun.quality = PickupObject.ItemQuality.B;
			gun.DefaultModule.angleVariance = 3f;
			gun.encounterTrackable.EncounterGuid = "1. Last. Warning.";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage = 60f;
			projectile.baseData.speed *= 1.75f;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 2;
			projectile.pierceMinorBreakables = true;
			projectile.SetProjectileSpriteRight("warningshot_projectile_001", 20, 10, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(16), new int?(8), null, null, null);
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			WarningShot.Warner = gun;
			List<string> mandatoryConsoleIDs = new List<string>
			{
				"bny:warning_shot"
			};
			List<string> optionalConsoleIDs = new List<string>
			{
				"hip_holster",
				"angry_bullets",
				"bloody_9mm",
				"easy_reload_bullets",
				"vorpal_bullets",
				"crisis_stone",
				"omega_bullets",
			};
			CustomSynergies.Add("Fear The Finale", mandatoryConsoleIDs, optionalConsoleIDs, true);
			List<string> mandatoryConsoleIDs1 = new List<string>
			{
				"bny:warning_shot"
			};
			List<string> optionalConsoleIDs1 = new List<string>
			{
				"blast_helmet",
				"melted_rock",
				"explosive_rounds",
				"rpg",
				"lil_bomber",
				"grenade_launcher",
				"bomb"
			};
			CustomSynergies.Add("Pattersons Warning", mandatoryConsoleIDs1, optionalConsoleIDs1, true);
		}
		private static Gun Warner;
		private bool HasReloaded;

		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController playerController = this.gun.CurrentOwner as PlayerController;
            {
				bool flag2 = this.Index == 1 || this.Index > 1;
				if (flag2)
				{
					projectile.sprite.renderer.enabled = true;
					projectile.baseData.damage = 50f * playerController.stats.GetStatValue(PlayerStats.StatType.Damage);
					projectile.baseData.range = 1000f;
					bool flagA = playerController.PlayerHasActiveSynergy("Pattersons Warning");
					if (flagA)
					{
						projectile.OnHitEnemy += (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.baboomer));
					}
				}
				bool flag3 = this.Index != this.LoadedShot && this.Index < 1;
				if (flag3)
				{
					//gun.ammo += 1;
					projectile.hitEffects.suppressMidairDeathVfx = true;
					projectile.baseData.range = 1E-05f;
					projectile.baseData.damage = 0f;
					projectile.sprite.renderer.enabled = false;
					bool flagA = playerController.PlayerHasActiveSynergy("Fear The Finale");
					if (flagA)
					{
						RoomHandler currentRoom = playerController.CurrentRoom;
						bool a = currentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.All);
						if (a)
						{
							foreach (AIActor aiactor in currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
							{
								bool r = aiactor.behaviorSpeculator != null;
								if (r)
								{
									aiactor.behaviorSpeculator.FleePlayerData = WarningShot.fleeData;
									FleePlayerData fleePlayerData = new FleePlayerData();
									GameManager.Instance.StartCoroutine(WarningShot.scare(aiactor));
								}
							}
						}
					}
				}
			}
		}
		private static IEnumerator scare(AIActor aiactor)
		{
			yield return new WaitForSeconds(2.5f);
			aiactor.behaviorSpeculator.FleePlayerData = null;
			yield break;
		}
		protected void Update()
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			WarningShot.fleeData = new FleePlayerData();
			WarningShot.fleeData.Player = player;
			WarningShot.fleeData.StartDistance = 100f;
			this.gun.PreventNormalFireAudio = true;
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
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			this.Index += 1;
			gun.PreventNormalFireAudio = true;
			bool flag2 = this.Index == this.LoadedShot +1 || this.Index > 1;
			if (flag2)
			{
				AkSoundEngine.PostEvent("Play_WPN_golddoublebarrelshotgun_shot_01", base.gameObject);
			}
			bool flag3 = this.Index == 1;
			if (flag3)
			{
				gun.ammo += 1;
				player.CurrentGun.ForceImmediateReload(true);
				AkSoundEngine.PostEvent("Play_OBJ_bloodybullet_proc_01", base.gameObject);

			}
		}
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			bool flag = gun.IsReloading && this.HasReloaded;
			if (flag)
			{
				this.Index = 0;
				this.HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
			}
		}
		private void baboomer(Projectile arg1, SpeculativeRigidbody arg2, bool arg3)
		{
			this.Boom(arg1.sprite.WorldCenter);
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
			damageRadius = 3.5f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 10f,
			doExplosionRing = false,
			doDestroyProjectiles = false,
			doForce = false,
			debrisForce = 0f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = false,
			playDefaultSFX = true,
		};
		private int Index = 0;

		// Token: 0x0400015E RID: 350
		private int LoadedShot = WarningShot.Warner.ClipCapacity ;
		private static FleePlayerData fleeData;
	}
}