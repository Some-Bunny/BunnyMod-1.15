using System;
using Gungeon;
using UnityEngine;

namespace BunnyMod
{
	// Token: 0x02000098 RID: 152
	public class ExtremelySimpleFireBulletBehaviour : MonoBehaviour
	{
		// Token: 0x0600034A RID: 842 RVA: 0x000200D8 File Offset: 0x0001E2D8
		public ExtremelySimpleFireBulletBehaviour()
		{
			this.tintColour = Color.red;
			this.useSpecialTint = true;
			this.procChance = 1;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00020128 File Offset: 0x0001E328
		private void Start()
		{
			this.m_projectile = base.GetComponent<Projectile>();
			bool flag = this.useSpecialTint;
			if (flag)
			{
				this.m_projectile.AdjustPlayerProjectileTint(this.tintColour, 2, 0f);
			}
			Projectile projectile = this.m_projectile;
			projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.OnHitEnemy));
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00020190 File Offset: 0x0001E390
		private void OnHitEnemy(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
		{
			bool flag = UnityEngine.Random.value <= (float)this.procChance;
			if (flag)
			{
				enemy.gameActor.ApplyEffect(this.fireEffect, 1f, null);
			}
		}

		// Token: 0x04000132 RID: 306
		private GameActorFireEffect fireEffect = Game.Items["hot_lead"].GetComponent<BulletStatusEffectItem>().FireModifierEffect;

		// Token: 0x04000133 RID: 307
		private Projectile m_projectile;

		// Token: 0x04000134 RID: 308
		public Color tintColour;

		// Token: 0x04000135 RID: 309
		public bool useSpecialTint;

		// Token: 0x04000136 RID: 310
		public int procChance;
	}
}