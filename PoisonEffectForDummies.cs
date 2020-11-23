using System;
using System.Collections.Generic;
using System.Linq;
using ItemAPI;
using UnityEngine;
using Gungeon;


namespace BunnyMod
{
	// Token: 0x02000099 RID: 153
	public class PoisonForDummiesLikeMe : MonoBehaviour
	{
		// Token: 0x0600034D RID: 845 RVA: 0x000201CD File Offset: 0x0001E3CD
		public PoisonForDummiesLikeMe()
		{
			this.tintColour = Color.green;
			this.useSpecialTint = true;
			this.procChance = 1;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000201F0 File Offset: 0x0001E3F0
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

		// Token: 0x0600034F RID: 847 RVA: 0x00020258 File Offset: 0x0001E458
		private void OnHitEnemy(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
		{
			bool flag = enemy && enemy.gameActor && enemy.healthHaver;
			if (flag)
			{
				bool flag2 = UnityEngine.Random.value <= (float)this.procChance;
				if (flag2)
				{
					GameActorHealthEffect healthModifierEffect = Game.Items["irradiated_lead"].GetComponent<BulletStatusEffectItem>().HealthModifierEffect;
					GameActorHealthEffect gameActorHealthEffect = new GameActorHealthEffect
					{
						duration = healthModifierEffect.duration,
						DamagePerSecondToEnemies = healthModifierEffect.DamagePerSecondToEnemies,
						TintColor = this.tintColour,
						DeathTintColor = this.tintColour,
						effectIdentifier = healthModifierEffect.effectIdentifier,
						AppliesTint = true,
						AppliesDeathTint = true,
						resistanceType = EffectResistanceType.Poison,
						OverheadVFX = healthModifierEffect.OverheadVFX,
						AffectsEnemies = true,
						AffectsPlayers = false,
						AppliesOutlineTint = false,
						ignitesGoops = false,
						OutlineTintColor = this.tintColour,
						PlaysVFXOnActor = false
					};
					bool flag3 = this.duration > 0;
					if (flag3)
					{
						gameActorHealthEffect.duration = (float)this.duration;
					}
					enemy.gameActor.ApplyEffect(gameActorHealthEffect, 1f, null);
				}
			}
			else
			{
				ETGModConsole.Log("Target could not be poisoned", false);
			}
		}

		// Token: 0x04000137 RID: 311
		private Projectile m_projectile;

		// Token: 0x04000138 RID: 312
		public Color tintColour;

		// Token: 0x04000139 RID: 313
		public bool useSpecialTint;

		// Token: 0x0400013A RID: 314
		public int procChance;

		// Token: 0x0400013B RID: 315
		public int duration;
	}
}


