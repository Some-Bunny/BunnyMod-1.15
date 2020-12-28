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
	// Token: 0x0200002D RID: 45
	public class SteveStaff : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Stevarian Staff", "stevestaff");
			Game.Items.Rename("outdated_gun_mods:stevarian_staff", "bny:stevarian_staff");
			var behav = gun.gameObject.AddComponent<SteveStaff>();
			//behav.preventNormalReloadAudio = true;
			//behav.overrideNormalReloadAudio = "Play_BOSS_doormimic_appear_01";
			GunExt.SetShortDescription(gun, "Guard");
			GunExt.SetLongDescription(gun, "A large staff wielded by Warden Gunjurers. Though simple in projectile, the blast is enough to take anyone by surprise.");
			GunExt.SetupSprite(gun, null, "stevestaff_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 36);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.chargeAnimation, 9);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 5);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(593) as Gun, true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 2.5f;
			gun.DefaultModule.cooldownTime = 0.6f;
			gun.DefaultModule.numberOfShotsInClip = 9;
			gun.SetBaseMaxAmmo(90);
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).frames[0].eventAudio = "Play_wpn_voidcannon_shot_01";
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.shootAnimation).frames[0].triggerEvent = true;
			gun.barrelOffset.transform.localPosition = new Vector3(1.0f, 1.625f, 0f);
			gun.carryPixelOffset += new IntVector2((int)7f, (int)-2f);
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).loopStart = 2;
			gun.encounterTrackable.EncounterGuid = "IT'S FUCKING STEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEVE";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(593) as Gun).DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 15f;
			projectile.baseData.speed *= 0.75f;
			projectile.baseData.force *= 1f;
			projectile.baseData.range = 3.5f;
			projectile.AdditionalScaleMultiplier = 1.1f;
			projectile.transform.parent = gun.barrelOffset;
			projectile.AdditionalScaleMultiplier *= 0.5f;
			Projectile projectile2 = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(593) as Gun).DefaultModule.projectiles[0]);
			projectile2.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile2.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile2);
			gun.DefaultModule.projectiles[0] = projectile2;
			//gun.gunHandedness = GunHandedness.HiddenOneHanded;
			projectile2.baseData.damage = 25f;
			projectile2.baseData.speed *= 1.0f;
			projectile2.baseData.force *= 1f;
			projectile2.baseData.range = 7f;
			projectile2.AdditionalScaleMultiplier *= 0.7f;
			projectile2.transform.parent = gun.barrelOffset;
			Projectile projectile3 = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(593) as Gun).DefaultModule.projectiles[0]);
			projectile3.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile3.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile3);
			gun.DefaultModule.projectiles[0] = projectile3;
			projectile3.baseData.damage = 50f;
			projectile3.baseData.speed *= 1.5f;
			projectile3.baseData.force *= 1f;
			projectile3.baseData.range = 100f;
			projectile3.AdditionalScaleMultiplier *= 1f;
			projectile3.transform.parent = gun.barrelOffset;
			ProjectileModule.ChargeProjectile item = new ProjectileModule.ChargeProjectile
			{
				Projectile = projectile,
				ChargeTime = 0f
			};
			ProjectileModule.ChargeProjectile item2 = new ProjectileModule.ChargeProjectile
			{
				Projectile = projectile2,
				ChargeTime = 0.75f
			};
			ProjectileModule.ChargeProjectile item3 = new ProjectileModule.ChargeProjectile
			{
				Projectile = projectile3,
				ChargeTime = 1.5f
			};
			gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile>
			{
				item,
				item2,
				item3
			};

			gun.quality = PickupObject.ItemQuality.A;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			//gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			List<string> mandatoryConsoleIDs7 = new List<string>
			{
				"bny:stevarian_staff",
				"evolver"
			};
			CustomSynergies.Add("Staff Of Life", mandatoryConsoleIDs7, null, true);
		}

		private bool HasReloaded;

		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			if (player.carriedConsumables.Currency > 240 || player.carriedConsumables.Currency == 240)
			{
				projectile.baseData.damage *= 3;
			}
			bool flagA = player.PlayerHasActiveSynergy("Staff Of Life");
			if (flagA)
			{
				projectile.OnWillKillEnemy = (Action<Projectile, SpeculativeRigidbody>)Delegate.Combine(projectile.OnWillKillEnemy, new Action<Projectile, SpeculativeRigidbody>(this.OnKill));
			}
			projectile.OnDestruction += this.kerboomer;
		}
		private void OnKill(Projectile arg1, SpeculativeRigidbody arg2)
		{
			PlayerController owner = this.gun.CurrentOwner as PlayerController;
			bool flag = !arg2.aiActor.healthHaver.IsDead;
			if (flag)
			{
				{
					RoomHandler absoluteRoom = base.transform.position.GetAbsoluteRoom();
					List<AIActor> list = new List<AIActor>();
					bool flag6 = absoluteRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null;
					if (flag6)
					{
						foreach (AIActor item in absoluteRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear))
						{
							list.Add(item);
						}
					}
					bool flag7 = list.Count > 1;
					if (flag7)
                    {
						bool fuckyou = (arg2.aiActor.EnemyGuid == "01972dee89fc4404a5c408d50007dad5" && arg2.aiActor.IsHarmlessEnemy);
						if (!fuckyou)
						{
							string guid;
							guid = "01972dee89fc4404a5c408d50007dad5";
							AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
							IntVector2? intVector = new IntVector2?(owner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
							AIActor bastardman = AIActor.Spawn(orLoadByGuid.aiActor, arg2.sprite.WorldCenter, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Spawn, true);
							bastardman.CanTargetEnemies = true;
							bastardman.CanTargetPlayers = false;
							PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(bastardman.specRigidbody, null, false);
							bastardman.gameObject.AddComponent<KillOnRoomClear>();
							bastardman.IsHarmlessEnemy = true;
							bastardman.IgnoreForRoomClear = true;
							bastardman.HandleReinforcementFallIntoRoom(0f);
						}
					}

				}
			}
        }

        public Vector3 projectilePos;


		private void kerboomer(Projectile projectile)
		{
			this.Blam(projectile.sprite.WorldCenter);
		}
		public void Blam(Vector3 position)
		{
			//AkSoundEngine.PostEvent("Play_OBJ_nuke_blast_01", gameObject);
			ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 2f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 20f,
			doExplosionRing = false,
			doDestroyProjectiles = true,
			doForce = true,
			debrisForce = 500f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = true,
			playDefaultSFX = true,
		};

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
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_BOSS_doormimic_appear_01", base.gameObject);
			}
		}
	}
}
