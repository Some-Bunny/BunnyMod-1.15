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
	public class OppressorsCrossbow : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun flakcannon = ETGMod.Databases.Items.NewGun("Oppressors Crossbow", "oppressorbow");
			Game.Items.Rename("outdated_gun_mods:oppressors_crossbow", "bny:oppressors_crossbow");
			flakcannon.gameObject.AddComponent<OppressorsCrossbow>();
			GunExt.SetShortDescription(flakcannon, "You're Gonna Need A Guitar.");
			GunExt.SetLongDescription(flakcannon, "The crossbow of the Oppressor, a merciless cosmic demigod who slayed any living creature deemed unworthy by it. Their reign of terror ended long ago, after a long battle with a mortal being who stole the crossbow mid-battle and used it to their advantage.\n\nSome say the rage of the Oppressor still lingers in it.");
			GunExt.SetupSprite(flakcannon, null, "oppressorbow_idle_001", 11);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.shootAnimation, 15);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.reloadAnimation, 12);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(flakcannon, "magnum", true, false);
			flakcannon.DefaultModule.ammoCost = 1;
			flakcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			flakcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			flakcannon.reloadTime = 2.5f;
			flakcannon.DefaultModule.cooldownTime = .5f;
			flakcannon.DefaultModule.numberOfShotsInClip = 1;
			flakcannon.SetBaseMaxAmmo(50);
			flakcannon.quality = PickupObject.ItemQuality.A;
			flakcannon.DefaultModule.angleVariance = 2f;
			flakcannon.DefaultModule.burstShotCount = 1;
			flakcannon.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(flakcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			flakcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 3f;
			projectile.baseData.speed *= 1.3f;
			projectile.AdditionalScaleMultiplier = .75f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 10;
			OpressorsCrossbowHandler crossbowHandler = projectile.gameObject.AddComponent<OpressorsCrossbowHandler>();
			crossbowHandler.projectileToSpawn = (PickupObjectDatabase.GetById(223) as Gun).DefaultModule.projectiles[0];
			projectile.SetProjectileSpriteRight("oppressorbow_projectile_001", 22, 11, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(11), new int?(5), null, null, null);
			flakcannon.encounterTrackable.EncounterGuid = "You're Gonna Need A Bigger Ukulele";
			ETGMod.Databases.Items.Add(flakcannon, null, "ANY");
		}

		public override void OnPostFired(PlayerController player, Gun flakcannon)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_blasterbow_shot_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun flakcannon, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_duelingpistol_reload_01", gameObject);
			}
		}
	}
}

//it just works
namespace BunnyMod
{
	// Token: 0x02000075 RID: 117
	public class OpressorsCrossbowHandler : MonoBehaviour
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000184B9 File Offset: 0x000166B9
		public OpressorsCrossbowHandler()
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
			bool flag3 = this.elapsed > 0.016f;
			if (flag3)
			{
				this.spawnAngle = 150f;
				this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
				this.spawnAngle = 210f;
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
				component.baseData.speed *= .300f;
				playerController.DoPostProcessProjectile(component);
				PierceProjModifier spook = component.gameObject.AddComponent<PierceProjModifier>();
				spook.penetration = 2;
				component.AdditionalScaleMultiplier = 0.8f;
				component.baseData.range = 3f;
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