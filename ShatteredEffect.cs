using System;
using System.Collections.Generic;
using System.Linq;
using ItemAPI;
using UnityEngine;


namespace BunnyMod
{
	public class ShatteredEffect : GameActorEffect
	{
			// Token: 0x06004CA4 RID: 19620 RVA: 0x00199080 File Offset: 0x00197280
		public ShatteredEffect()
		{
			TintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(0.9f);
			DeathTintColor = new Color(0f, 0.1f, 0.3f).WithAlpha(0.9f);
			this.FreezeAmount = 125f;
			this.UnfreezeDamagePercent = 0f;
			this.crystalNum = 0;
			this.crystalRot = 0;
			this.crystalVariation = new Vector2(0.05f, 0.05f);
			this.debrisMinForce = 5;
			this.debrisMaxForce = 5;
			this.debrisAngleVariance = 15f;
			this.OverheadVFX = ShatterEffect.ShatterVFXObject;
		}

        public ShatteredEffect(object player)
        {
            this.player = player;
        }

        // Token: 0x06004CA5 RID: 19621 RVA: 0x001990E8 File Offset: 0x001972E8
        public bool ShouldVanishOnDeath(GameActor actor)
		{
			return (!actor.healthHaver || !actor.healthHaver.IsBoss) && (!(actor is AIActor) || !(actor as AIActor).IsSignatureEnemy);
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00007B97 File Offset: 0x00005D97
		public override void ApplyTint(GameActor actor)
		{
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00199138 File Offset: 0x00197338
		public override void OnEffectApplied(GameActor actor, RuntimeGameActorEffectData effectData, float partialAmount = 1f)
		{
			effectData.MovementModifier = delegate (ref Vector2 volundaryVel, ref Vector2 involuntaryVel)
			{
				float d = Mathf.Clamp01((100f - actor.FreezeAmount) / 100f);
				volundaryVel *= d;
			};
			actor.MovementModifiers += effectData.MovementModifier;
			effectData.OnActorPreDeath = delegate (Vector2 dir)
			{
				if (actor.IsFrozen)
				{
					this.DestroyCrystals(effectData, !actor.IsFalling);
					AkSoundEngine.PostEvent("Play_OBJ_crystal_shatter_01", GameManager.Instance.PrimaryPlayer.gameObject);
					actor.FreezeAmount = 0f;
					if (this.ShouldVanishOnDeath(actor))
					{
						if (actor is AIActor)
						{
							(actor as AIActor).ForceDeath(dir, false);
						}
						UnityEngine.Object.Destroy(actor.gameObject);
					}
				}
			};
			actor.healthHaver.OnPreDeath += effectData.OnActorPreDeath;
			actor.FreezeAmount += this.FreezeAmount * partialAmount;
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x001991DC File Offset: 0x001973DC
		public override void OnDarkSoulsAccumulate(GameActor actor, RuntimeGameActorEffectData effectData, float partialAmount = 1f, Projectile sourceProjectile = null)
		{
			if (!effectData.actor.IsFrozen)
			{
				actor.FreezeAmount += this.FreezeAmount * partialAmount;
				if (actor.healthHaver.IsBoss)
				{
					actor.FreezeAmount = Mathf.Min(actor.FreezeAmount, 75f);
				}
			}
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x00199234 File Offset: 0x00197434
		public override void EffectTick(GameActor actor, RuntimeGameActorEffectData effectData)
		{
			if (actor.FreezeAmount > 0f)
			{
				actor.FreezeAmount = Mathf.Max(0f, actor.FreezeAmount - BraveTime.DeltaTime * actor.FreezeDispelFactor);
				if (!actor.IsFrozen)
				{
					if (actor.FreezeAmount > 100f && actor.healthHaver.IsAlive)
					{
						actor.FreezeAmount = 100f;
						if (this.FreezeCrystals.Count > 0)
						{
							if (effectData.vfxObjects == null)
							{
								effectData.vfxObjects = new List<global::Tuple<GameObject, float>>();
							}
							int num = this.crystalNum;
							if (effectData.actor && effectData.actor.specRigidbody && effectData.actor.specRigidbody.HitboxPixelCollider != null)
							{
								float num2 = effectData.actor.specRigidbody.HitboxPixelCollider.UnitDimensions.x * effectData.actor.specRigidbody.HitboxPixelCollider.UnitDimensions.y;
								num = Mathf.Max(this.crystalNum, (int)((float)this.crystalNum * (0.5f + num2 / 4f)));
							}
							for (int i = 0; i < num; i++)
							{
								GameObject prefab = BraveUtility.RandomElement<GameObject>(this.FreezeCrystals);
								Vector2 vector = actor.specRigidbody.HitboxPixelCollider.UnitCenter;
								Vector2 vector2 = BraveUtility.RandomVector2(-this.crystalVariation, this.crystalVariation);
								vector += vector2;
								float num3 = BraveMathCollege.QuantizeFloat(vector2.ToAngle(), 360f / (float)this.crystalRot);
								Quaternion rotation = Quaternion.Euler(0f, 0f, num3);
								GameObject gameObject = SpawnManager.SpawnVFX(prefab, vector, rotation, true);
								gameObject.transform.parent = actor.transform;
								tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
								if (component)
								{
									actor.sprite.AttachRenderer(component);
									component.HeightOffGround = 0.1f;
								}
								if (effectData.actor && effectData.actor.specRigidbody && effectData.actor.specRigidbody.HitboxPixelCollider != null)
								{
									Vector2 unitCenter = effectData.actor.specRigidbody.HitboxPixelCollider.UnitCenter;
									float num4 = (float)i * (360f / (float)num);
									Vector2 normalized = BraveMathCollege.DegreesToVector(num4, 1f).normalized;
									normalized.x *= effectData.actor.specRigidbody.HitboxPixelCollider.UnitDimensions.x / 2f;
									normalized.y *= effectData.actor.specRigidbody.HitboxPixelCollider.UnitDimensions.y / 2f;
									float magnitude = normalized.magnitude;
									Vector2 vector3 = unitCenter + normalized;
									vector3 += (unitCenter - vector3).normalized * (magnitude * UnityEngine.Random.Range(0.15f, 0.85f));
									gameObject.transform.position = vector3.ToVector3ZUp(0f);
									gameObject.transform.rotation = Quaternion.Euler(0f, 0f, num4);
								}
								effectData.vfxObjects.Add(global::Tuple.Create<GameObject, float>(gameObject, num3));
							}
						}
						if (this.ShouldVanishOnDeath(actor))
						{
							actor.StealthDeath = true;
						}
						if (actor.behaviorSpeculator)
						{
							if (actor.behaviorSpeculator.IsInterruptable)
							{
								actor.behaviorSpeculator.InterruptAndDisable();
							}
							else
							{
								actor.behaviorSpeculator.enabled = false;
							}
						}
						if (actor is AIActor)
						{
							AIActor aiactor = actor as AIActor;
							aiactor.ClearPath();
							aiactor.BehaviorOverridesVelocity = false;
						}
						actor.IsFrozen = true;
					}
				}
				else if (actor.IsFrozen)
				{
					if (actor.FreezeAmount <= 0f)
					{
						return;
					}
					if (actor.IsFalling)
					{
						if (effectData.vfxObjects != null && effectData.vfxObjects.Count > 0)
						{
							this.DestroyCrystals(effectData, false);
						}
						if (actor.aiAnimator)
						{
							actor.aiAnimator.FpsScale = 1f;
						}
					}
				}
			}
			if (!actor.healthHaver.IsDead)
			{
				float num5 = (!actor.healthHaver.IsBoss) ? 100f : 75f;
				float num6 = (!actor.IsFrozen) ? Mathf.Clamp01((100f - actor.FreezeAmount) / 100f) : 0f;
				float num7 = (!actor.IsFrozen) ? Mathf.Clamp01(actor.FreezeAmount / num5) : 1f;
				if (actor.aiAnimator)
				{
					actor.aiAnimator.FpsScale = ((!actor.IsFalling) ? num6 : 1f);
				}
				if (actor.aiShooter)
				{
					actor.aiShooter.AimTimeScale = num6;
				}
				if (actor.behaviorSpeculator)
				{
					actor.behaviorSpeculator.CooldownScale = num6;
				}
				if (actor.bulletBank)
				{
					actor.bulletBank.TimeScale = num6;
				}
				if (this.AppliesTint)
				{
					float num8 = actor.FreezeAmount / actor.FreezeDispelFactor;
					Color overrideColor = this.TintColor;
					if (num8 < 0.1f)
					{
						overrideColor = Color.black;
					}
					else if (num8 < 0.2f)
					{
						overrideColor = Color.white;
					}
					overrideColor.a *= num7;
					actor.RegisterOverrideColor(overrideColor, this.effectIdentifier);
				}
			}
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00199814 File Offset: 0x00197A14
		public override void OnEffectRemoved(GameActor actor, RuntimeGameActorEffectData effectData)
		{
			if (actor.IsFrozen)
			{
				actor.FreezeAmount = 0f;
				float resistanceForEffectType = actor.GetResistanceForEffectType(this.resistanceType);
				float damage = Mathf.Max(0f, actor.healthHaver.GetMaxHealth() * this.UnfreezeDamagePercent * (1f - resistanceForEffectType));
				actor.healthHaver.ApplyDamage(damage, Vector2.zero, "Freezer Burn", CoreDamageTypes.Ice, DamageCategory.DamageOverTime, true, null, false);
				this.DestroyCrystals(effectData, !actor.healthHaver.IsDead);
				if (this.AppliesTint)
				{
					actor.DeregisterOverrideColor(this.effectIdentifier);
				}
				if (actor.behaviorSpeculator)
				{
					actor.behaviorSpeculator.enabled = true;
				}
				if (this.ShouldVanishOnDeath(actor))
				{
					actor.StealthDeath = false;
				}
				actor.IsFrozen = false;
			}
			actor.MovementModifiers -= effectData.MovementModifier;
			actor.healthHaver.OnPreDeath -= effectData.OnActorPreDeath;
			if (actor.aiAnimator)
			{
				actor.aiAnimator.FpsScale = 1f;
			}
			if (actor.aiShooter)
			{
				actor.aiShooter.AimTimeScale = 1f;
			}
			if (actor.behaviorSpeculator)
			{
				actor.behaviorSpeculator.CooldownScale = 1f;
			}
			if (actor.bulletBank)
			{
				actor.bulletBank.TimeScale = 1f;
			}
			tk2dSpriteAnimator spriteAnimator = actor.spriteAnimator;
			if (spriteAnimator && actor.aiAnimator && spriteAnimator.CurrentClip != null && !spriteAnimator.IsPlaying(spriteAnimator.CurrentClip))
			{
				actor.aiAnimator.PlayUntilFinished(actor.spriteAnimator.CurrentClip.name, false, null, -1f, true);
			}
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x001999E5 File Offset: 0x00197BE5
		public override bool IsFinished(GameActor actor, RuntimeGameActorEffectData effectData, float elapsedTime)
		{
			return actor.FreezeAmount <= 0f;
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x001999F8 File Offset: 0x00197BF8
		private void DestroyCrystals(RuntimeGameActorEffectData effectData, bool playVfxExplosion = true)
		{
			if (effectData.vfxObjects == null || effectData.vfxObjects.Count == 0)
			{
				return;
			}
			Vector2 vector = Vector2.zero;
			GameActor actor = effectData.actor;
			if (actor)
			{
				vector = ((!actor.specRigidbody) ? actor.sprite.WorldCenter : actor.specRigidbody.HitboxPixelCollider.UnitCenter);
			}
			else
			{
				int num = 0;
				for (int i = 0; i < effectData.vfxObjects.Count; i++)
				{
					if (effectData.vfxObjects[i].First)
					{
						vector += effectData.vfxObjects[i].First.transform.position.XY();
						num++;
					}
				}
				if (num == 0)
				{
					return;
				}
				vector /= (float)num;
			}
			if (playVfxExplosion && this.vfxExplosion)
			{
				GameObject gameObject = SpawnManager.SpawnVFX(this.vfxExplosion, vector, Quaternion.identity);
				tk2dSprite component = gameObject.GetComponent<tk2dSprite>();
				if (actor && component)
				{
					actor.sprite.AttachRenderer(component);
					component.HeightOffGround = 0.1f;
					component.UpdateZDepth();
				}
			}
			for (int j = 0; j < effectData.vfxObjects.Count; j++)
			{
				GameObject first = effectData.vfxObjects[j].First;
				if (first)
				{
					first.transform.parent = SpawnManager.Instance.VFX;
					DebrisObject orAddComponent = first.GetOrAddComponent<DebrisObject>();
					if (actor)
					{
						actor.sprite.AttachRenderer(orAddComponent.sprite);
					}
					orAddComponent.sprite.IsPerpendicular = true;
					orAddComponent.DontSetLayer = true;
					orAddComponent.gameObject.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
					orAddComponent.angularVelocity = Mathf.Sign(UnityEngine.Random.value - 0.5f) * 125f;
					orAddComponent.angularVelocityVariance = 60f;
					orAddComponent.decayOnBounce = 0.5f;
					orAddComponent.bounceCount = 1;
					orAddComponent.canRotate = true;
					float num2 = effectData.vfxObjects[j].Second + UnityEngine.Random.Range(-this.debrisAngleVariance, this.debrisAngleVariance);
					if (orAddComponent.name.Contains("tilt", true))
					{
						num2 += 45f;
					}
					Vector2 vector2 = BraveMathCollege.DegreesToVector(num2, 1f) * (float)UnityEngine.Random.Range(this.debrisMinForce, this.debrisMaxForce);
					Vector3 startingForce = new Vector3(vector2.x, (vector2.y >= 0f) ? 0f : vector2.y, (vector2.y <= 0f) ? 0f : vector2.y);
					float startingHeight = (!actor) ? 0.75f : (first.transform.position.y - actor.specRigidbody.HitboxPixelCollider.UnitBottom);
					if (orAddComponent.minorBreakable)
					{
						orAddComponent.minorBreakable.enabled = true;
					}
					orAddComponent.Trigger(startingForce, startingHeight, 1f);
				}
			}
			effectData.vfxObjects.Clear();
		}

		// Token: 0x040042AE RID: 17070
		public const float BossMinResistance = 0.1f;

		// Token: 0x040042AF RID: 17071
		public const float BossMaxResistance = .3f;

		// Token: 0x040042B0 RID: 17072
		public const float BossResistanceDelta = 0.01f;

		// Token: 0x040042B1 RID: 17073
		public const float BossMaxFreezeAmount = 10f;

		// Token: 0x040042B2 RID: 17074
		public float FreezeAmount;

		// Token: 0x040042B3 RID: 17075
		public float UnfreezeDamagePercent;

		// Token: 0x040042B4 RID: 17076
		public List<GameObject> FreezeCrystals;

		// Token: 0x040042B5 RID: 17077
		[NonSerialized]
		public int crystalNum;

		// Token: 0x040042B6 RID: 17078
		public int crystalRot;

		// Token: 0x040042B7 RID: 17079
		public Vector2 crystalVariation;

		// Token: 0x040042B8 RID: 17080
		public int debrisMinForce;

		// Token: 0x040042B9 RID: 17081
		public int debrisMaxForce;

		// Token: 0x040042BA RID: 17082
		public float debrisAngleVariance;

		// Token: 0x040042BB RID: 17083
		public GameObject vfxExplosion;
        private object player;
    }
}



