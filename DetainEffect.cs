using System;
using System.Collections.Generic;
using System.Linq;
using ItemAPI;
using UnityEngine;


namespace BunnyMod
{
	// Token: 0x02000037 RID: 55
	public class GameActorDetainEffect : GameActorEffect
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00010F54 File Offset: 0x0000F154
		public GameActorDetainEffect(PlayerController owner)
		{
			this.AffectsPlayers = false;
			this.duration = 10f;
			this.AppliesTint = true;
			this.TintColor = new Color(0.5f, 0f, 0.5f, Mathf.Clamp01(this.duration));
			this.Owner = owner;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public bool ShouldVanishOnDeath(GameActor actor)
		{
			return (!actor.healthHaver || !actor.healthHaver.IsBoss) && (!(actor is AIActor) || !(actor as AIActor).IsSignatureEnemy);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00011000 File Offset: 0x0000F200
		public override void OnEffectApplied(GameActor actor, RuntimeGameActorEffectData effectData, float partialAmount = 1f)
		{
			bool flag = !actor.healthHaver.IsBoss && !actor.healthHaver.IsDead;
			if (flag)
			{
				AIActor aiActor = actor.aiActor;
				this.prev = aiActor.CanTargetPlayers;
				aiActor.CanTargetPlayers = false;
				aiActor.MovementSpeed = 0f;
				base.OnEffectApplied(actor, effectData, partialAmount);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00011064 File Offset: 0x0000F264
		public override void OnEffectRemoved(GameActor actor, RuntimeGameActorEffectData effectData)
		{
			AIActor aiActor = actor.aiActor;
			aiActor.MovementSpeed = aiActor.BaseMovementSpeed;
			aiActor.CanTargetPlayers = this.prev;
			base.OnEffectRemoved(actor, effectData);
		}

		// Token: 0x04000097 RID: 151
		private bool prev = true;

		// Token: 0x04000098 RID: 152
		public PlayerController Owner;
	}
}


