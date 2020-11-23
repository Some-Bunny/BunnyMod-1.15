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
	public class MithrixHammer : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Hammer Of The Moon", "lunarhammer");
			Game.Items.Rename("outdated_gun_mods:hammer_of_the_moon", "bny:hammer_of_the_moon");
			gun.gameObject.AddComponent<MithrixHammer>();
			GunExt.SetShortDescription(gun, "Commencement");
			GunExt.SetLongDescription(gun, "You're Gonna Need A Bigger Hammer.");
			GunExt.SetupSprite(gun, null, "lunarhammer_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 8);
			GunExt.SetAnimationFPS(gun, gun.chargeAnimation, 7);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(481) as Gun, true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 3.5f;
			gun.DefaultModule.cooldownTime = 1f;
			gun.carryPixelOffset = new IntVector2((int)-2f, (int)-1f);
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.DefaultModule.preventFiringDuringCharge = true;
			gun.SetBaseMaxAmmo(40);
			gun.InfiniteAmmo = true;
			gun.quality = PickupObject.ItemQuality.S;
			gun.encounterTrackable.EncounterGuid = "The Big Ukulele";
			ProjectileModule.ChargeProjectile item = new ProjectileModule.ChargeProjectile();
			gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile>
			{
				item
			};
			gun.DefaultModule.chargeProjectiles[0].ChargeTime = 1.25f;
			gun.DefaultModule.chargeProjectiles[0].UsedProperties = ((Gun)ETGMod.Databases.Items[481]).DefaultModule.chargeProjectiles[0].UsedProperties;
			gun.DefaultModule.chargeProjectiles[0].VfxPool = ((Gun)ETGMod.Databases.Items[481]).DefaultModule.chargeProjectiles[0].VfxPool;
			gun.DefaultModule.chargeProjectiles[0].VfxPool.type = ((Gun)ETGMod.Databases.Items[481]).DefaultModule.chargeProjectiles[0].VfxPool.type;
			MithRixOnReloadModifier mithRixOnReloadModifier = gun.gameObject.AddComponent<MithRixOnReloadModifier>();
			ProjectileModule.ChargeProjectile chargeProjectile = gun.DefaultModule.chargeProjectiles[0];
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(((Gun)ETGMod.Databases.Items[481]).DefaultModule.chargeProjectiles[0].Projectile);
			chargeProjectile.Projectile = projectile;
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
			gun.GetComponent<tk2dSpriteAnimator>().GetClipByName(gun.chargeAnimation).loopStart = 5;
			projectile.transform.parent = gun.barrelOffset;
			projectile.baseData.damage = 0f;
			projectile.baseData.range = 0f;
			projectile.baseData.force = 350f;
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			gun.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}
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
		private bool HasReloaded;

		public override void OnPostFired(PlayerController owner, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			gun.carryPixelOffset = new IntVector2((int)-2f, (int)-1f);
			projectile.baseData.range = 0f;
			this.Blam(projectile.sprite.WorldCenter);
			projectile.OnDestruction += Shatter;
		}
		public void Shatter(Projectile projectile)
		{
			PlayerController playerController1 = this.gun.CurrentOwner as PlayerController;
			bool isInCombat = playerController1.IsInCombat;
			if (isInCombat)
			{
				foreach (AIActor aiactor in playerController1.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
				{
					bool flag = aiactor != null;
					if (flag)
					{
						Vector3 position = aiactor.sprite.WorldCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile((PickupObjectDatabase.GetById(670) as Gun).DefaultModule.projectiles[0].gameObject, position, Quaternion.Euler(0f, 0f, BraveMathCollege.Atan2Degrees(playerController1.sprite.WorldCenter - aiactor.sprite.WorldCenter)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						bool flag12 = component != null;
						bool flag2 = flag12;
						if (flag2)
						{
							component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 0f);
							component.SpawnedFromOtherPlayerProjectile = true;
							component.Shooter = this.gun.CurrentOwner.specRigidbody;
							component.Owner = playerController1;
							component.Shooter = playerController1.specRigidbody;
							component.sprite.renderer.enabled = false;
							component.baseData.speed = 4f;
							component.baseData.damage = 1f;
							component.AdditionalScaleMultiplier = 0.7f;
							component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
							component.ignoreDamageCaps = true;
							component.FreezeApplyChance = 1f;
							component.AppliesFreeze = true;
							component.freezeEffect = MithrixHammer.ShatteredEffect;
						}
					}
				}
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
			damageRadius = 10f,
			damageToPlayer = 0f,
			doDamage = true,
			damage = 60f,
			doExplosionRing = false,
			doDestroyProjectiles = false,
			doForce = true,
			debrisForce = 100f,
			preventPlayerForce = true,
			explosionDelay = 0f,
			usesComprehensiveDelay = false,
			doScreenShake = true,
			playDefaultSFX = false,
		};
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			base.OnReloadPressed(player, gun, bSOMETHING);
			bool flag = gun.ClipCapacity == gun.ClipShotsRemaining || gun.CurrentAmmo == gun.ClipShotsRemaining;
			bool needler = this.HasReloaded && gun.ClipShotsRemaining == 0;
			if (needler)
			{
				base.StartCoroutine(this.StartNeedling(player));
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
			}

			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
			}
		}
        public override void OnFinishAttack(PlayerController player, Gun gun)
		{
			base.OnFinishAttack(player, gun);
        }

        private IEnumerator StartNeedling(PlayerController player)
		{
			{
				gun.carryPixelOffset = new IntVector2((int)-30f, (int)-1f);
				AIActor aIActor = new AIActor();
				for (int counter = 0; counter < 12; counter++)
				{
					yield return new WaitForSeconds(.1f);
					{
						gun.carryPixelOffset -= new IntVector2((int)-1.25f, (int)-0.3f);
						Projectile projectile = ((Gun)ETGMod.Databases.Items[357]).DefaultModule.projectiles[0];
						Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
						Vector3 vector2 = player.specRigidbody.UnitCenter;
						GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((player.CurrentGun == null) ? 1.2f : player.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-2.5f, 2.5f)), true);
						Projectile component = gameObject.GetComponent<Projectile>();
						HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
						homing.HomingRadius = 300;
						homing.AngularVelocity = 10;
						component.baseData.damage = 3.75f;
						component.Owner = player;
						component.Shooter = player.specRigidbody;
						component.shouldRotate = true;
					}
				}
				for (int counter = 0; counter < 8; counter++)
				{
					yield return new WaitForSeconds(.1f);
					gun.carryPixelOffset -= new IntVector2((int)-2.5f, (int)-.66f);
				}
				yield return new WaitForSeconds(.1f);
				this.Reposition();
			}
			yield break;
		}
		private void Reposition()
        {
			gun.carryPixelOffset = new IntVector2((int)-2f, (int)-1f);
		}

		private static GameActorFreezeEffect ShatteredEffect = new GameActorFreezeEffect
		{
			TintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(1f),
			DeathTintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(1f),
			AppliesTint = true,
			AppliesDeathTint = true,
			effectIdentifier = "Shatter",
			FreezeAmount = 150f,
			UnfreezeDamagePercent = 0f,
			crystalNum = 0,
			crystalRot = 0,
			crystalVariation = new Vector2(0.05f, 0.05f),
			debrisMinForce = 5,
			debrisMaxForce = 5,
			debrisAngleVariance = 15f,
			PlaysVFXOnActor = true,
			OverheadVFX = ShatterEffect.ShatterVFXObject,
		};

		public static GameObject ShatterVFXObject = SpriteBuilder.SpriteFromResource("BunnyMod/Resources/EffectIcons/shattered_debuff_icon.png", new GameObject("Shatter"), true);
	}
}
