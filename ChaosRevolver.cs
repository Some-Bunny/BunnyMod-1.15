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
	public class ChaosRevolver : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun chaosgun = ETGMod.Databases.Items.NewGun("Chaos Revolver", "chaosrevolver");
			Game.Items.Rename("outdated_gun_mods:chaos_revolver", "bny:chaos_revolver");
			chaosgun.gameObject.AddComponent<ChaosRevolver>();
			GunExt.SetShortDescription(chaosgun, "Predictable Gun");
			GunExt.SetLongDescription(chaosgun, "A legendary gun that was sealed away because it was thought to speak ancient secrets, when in reality the Chamber was.\n\nWhile the gun was sealed away, the Chamber managed to escape, leaving the innocent weapon behind. You are lucky to resurface it.");
			GunExt.SetupSprite(chaosgun, null, "chaosrevolver_idle_001", 25);
			GunExt.SetAnimationFPS(chaosgun, chaosgun.shootAnimation, 14);
			GunExt.SetAnimationFPS(chaosgun, chaosgun.reloadAnimation, 8);
			GunExt.SetAnimationFPS(chaosgun, chaosgun.idleAnimation, 5);
			GunExt.AddProjectileModuleFrom(chaosgun, "ak-47", true, false);
			chaosgun.DefaultModule.ammoCost = 1;
			chaosgun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			chaosgun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			chaosgun.reloadTime = 1.8f;
			chaosgun.DefaultModule.cooldownTime = .2f;
			chaosgun.DefaultModule.numberOfShotsInClip = 6;
			chaosgun.SetBaseMaxAmmo(150);
			chaosgun.quality = PickupObject.ItemQuality.A;
			chaosgun.DefaultModule.angleVariance = 15f;
			chaosgun.DefaultModule.burstShotCount = 1;
			chaosgun.encounterTrackable.EncounterGuid = "chaosgun";
			ChaosRevolver.ChaosRevolverID = chaosgun.PickupObjectId;
			chaosgun.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(chaosgun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			chaosgun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 25f;
			projectile.baseData.speed *= 1.1f;
			projectile.transform.parent = chaosgun.barrelOffset;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 4;
			projectile.SetProjectileSpriteRight("chaosrevolver_projectile_001", 10, 10, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(7), new int?(7), null, null, null);
			ETGMod.Databases.Items.Add(chaosgun, null, "ANY");

		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = this.gun.CurrentOwner as PlayerController;
			bool flagA = player.PlayerHasActiveSynergy("Reunion");
			if (flagA)
			{
				projectile.baseData.speed /= 2;
				ChaosbowHandler chaosHandler = projectile.gameObject.AddComponent<ChaosbowHandler>();
				chaosHandler.projectileToSpawn = (PickupObjectDatabase.GetById(670) as Gun).DefaultModule.projectiles[0];
				HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
				homing.HomingRadius = 250f;
				homing.AngularVelocity = 5;
			}
			else
            {
				HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
				homing.HomingRadius = 2.5f;
				homing.AngularVelocity = 5000;
			}

		}

		public override void OnPostFired(PlayerController player, Gun chaosgun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_dragun_shot_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun chaosgun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", gameObject);
			}
		}
		private bool HasReloaded;
		public static int ChaosRevolverID;
	}
}

namespace BunnyMod
{
	// Token: 0x02000075 RID: 117
	public class ChaosbowHandler : MonoBehaviour
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000184B9 File Offset: 0x000166B9
		public ChaosbowHandler()
		{
			this.projectileToSpawn = null;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000184D5 File Offset: 0x000166D5
		private void Awake()
		{
			this.m_projectile = base.GetComponent<Projectile>();
			this.speculativeRigidBoy = base.GetComponent<SpeculativeRigidbody>();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000184F0 File Offset: 0x000166F0
		private void Update()
		{
			bool flag = this.m_projectile == null;
			if (flag)
			{
				this.m_projectile = base.GetComponent<Projectile>();
			}
			bool flag2 = this.speculativeRigidBoy == null;
			if (flag2)
			{
				this.speculativeRigidBoy = base.GetComponent<SpeculativeRigidbody>();
			}
			this.elapsed += BraveTime.DeltaTime;
			bool flag3 = this.elapsed > 0.05f;
			if (flag3)
			{
				this.spawnAngle = UnityEngine.Random.Range(-180f, 180f);
				this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
				this.elapsed = 0f;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000185BC File Offset: 0x000167BC
		private void SpawnProjectile(Projectile proj, Vector3 spawnPosition, float zRotation, SpeculativeRigidbody collidedRigidbody = null)
		{
			GameObject gameObject = SpawnManager.SpawnProjectile(proj.gameObject, spawnPosition, Quaternion.Euler(0f, 0f, zRotation), true);
			Projectile component = gameObject.GetComponent<Projectile>();
			bool flag = component;
			if (flag)
			{
				component.SpawnedFromOtherPlayerProjectile = true;
				PlayerController playerController = this.m_projectile.Owner as PlayerController;
				component.baseData.damage *= playerController.stats.GetStatValue(PlayerStats.StatType.Damage);
				component.baseData.speed *= .250f;
				playerController.DoPostProcessProjectile(component);
				PierceProjModifier spook = component.gameObject.AddComponent<PierceProjModifier>();
				spook.penetration = 2;
				component.AdditionalScaleMultiplier = 0.8f;
				component.baseData.range = 5f;
				HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
				homing.HomingRadius = 50f;
				homing.AngularVelocity = 10;
			}
		}

		// Token: 0x040000EA RID: 234
		private float spawnAngle = 90f;

		// Token: 0x040000EB RID: 235
		private Projectile m_projectile;

		// Token: 0x040000EC RID: 236
		private SpeculativeRigidbody speculativeRigidBoy;

		// Token: 0x040000ED RID: 237
		public Projectile projectileToSpawn;

		// Token: 0x040000EE RID: 238
		private float elapsed;
	}
}
