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
	// Token: 0x02000075 RID: 117
	public class MimicBlasphemyHandler : MonoBehaviour
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000184B9 File Offset: 0x000166B9
		public MimicBlasphemyHandler()
		{
			this.projectileToSpawn = null;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000184D5 File Offset: 0x000166D5
		private void Awake()
		{
			this.m_projectile = base.GetComponent<Projectile>();
			this.speculativeRigidBoy = base.GetComponent<SpeculativeRigidbody>();
		}
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
			bool flag3 = this.elapsed > 0.95f;
			if (flag3)
			{
				for (int counter = 0; counter < 3; counter++)
				{
					this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
					this.spawnAngle += 120f;
				}
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
				component.baseData.speed *= playerController.stats.GetStatValue(PlayerStats.StatType.ProjectileSpeed);
				PierceProjModifier spook = component.gameObject.AddComponent<PierceProjModifier>();
				component.AdditionalScaleMultiplier = 0.7f;
				spook.penetration = 1;
				HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
				homing.HomingRadius = 25;
				homing.AngularVelocity = 60;
				playerController.DoPostProcessProjectile(component);
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