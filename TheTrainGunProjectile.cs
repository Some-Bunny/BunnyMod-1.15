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
	public class RailsGunHandler : MonoBehaviour
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000184B9 File Offset: 0x000166B9
		public RailsGunHandler()
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
			bool flag3 = this.elapsed > 0.22f;
			if (flag3)
			{
				this.SpawnProjectile(this.projectileToSpawn, this.m_projectile.sprite.WorldCenter, this.m_projectile.transform.eulerAngles.z + this.spawnAngle, null);
				this.spawnAngle += UnityEngine.Random.Range(0f, 359f);
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
				component.baseData.speed *= .150f;
				component.baseData.damage = 4f;
				playerController.DoPostProcessProjectile(component);
				HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
				homing.HomingRadius = 25;
				homing.AngularVelocity = 12;
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