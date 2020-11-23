using System;
using Gungeon;
using UnityEngine;

namespace BunnyMod
{
	// Token: 0x02000082 RID: 130
	public class ExtremelySimpleFireThing : MonoBehaviour
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0001BE5C File Offset: 0x0001A05C
		public ExtremelySimpleFireThing()
		{
			this.tintColour = Color.red;
			this.useSpecialTint = true;
			this.procChance = 1;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001BEAC File Offset: 0x0001A0AC
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

		// Token: 0x060002ED RID: 749 RVA: 0x0001BF14 File Offset: 0x0001A114
		private void OnHitEnemy(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
		{
			bool flag = UnityEngine.Random.value <= (float)this.procChance;
			if (flag)
			{
				enemy.gameActor.ApplyEffect(this.burnEffect, 3f, null);
			}
		}

		// Token: 0x0400010A RID: 266
		private GameActorHealthEffect burnEffect = Game.Items["hot_lead"].GetComponent<BulletStatusEffectItem>().HealthModifierEffect;

		// Token: 0x0400010B RID: 267
		private Projectile m_projectile;

		// Token: 0x0400010C RID: 268
		public Color tintColour;

		// Token: 0x0400010D RID: 269
		public bool useSpecialTint;

		// Token: 0x0400010E RID: 270
		public int procChance;
	}
}
