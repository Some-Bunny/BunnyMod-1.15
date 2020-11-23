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

public class MicrowaveModifier : MonoBehaviour
{
	// Token: 0x0600700B RID: 28683 RVA: 0x002B7FC9 File Offset: 0x002B61C9
	public MicrowaveModifier()
	{
		this.AuraRadius = 5f;
		this.DamagePerSecond = 11f;
		this.RadiusSynergyRadius = 5f;
	}

	// Token: 0x0600700C RID: 28684 RVA: 0x002B7FF4 File Offset: 0x002B61F4
	private void Awake()
	{
		this.m_gun = base.GetComponent<Gun>();
		Gun gun = this.m_gun;
		gun.OnDropped = (Action)Delegate.Combine(gun.OnDropped, new Action(this.OnDropped));
		PlayerController playerController = this.m_gun.CurrentOwner as PlayerController;
		if (playerController)
		{
			playerController.inventory.OnGunChanged += this.OnGunChanged;
		}
	}

	// Token: 0x0600700D RID: 28685 RVA: 0x002B8068 File Offset: 0x002B6268
	private void Update()
	{
		if (this.m_gun.IsReloading && this.m_gun.CurrentOwner is PlayerController)
		{
			this.DoAura();
			if (this.IgnitesEnemies || this.DoRadialIndicatorAnyway)
			{
				this.HandleRadialIndicator();
			}
            else
            {
				this.HandleRadialIndicator();
			}
		}
		else
		{
			this.UnhandleRadialIndicator();
		}
	}

	// Token: 0x0600700E RID: 28686 RVA: 0x002B80C8 File Offset: 0x002B62C8
	private void HandleRadialIndicator()
	{
		if (!this.m_radialIndicatorActive)
		{
			this.m_radialIndicatorActive = true;
			this.m_radialIndicator = ((GameObject)UnityEngine.Object.Instantiate(ResourceCache.Acquire("Global VFX/HeatIndicator"), this.m_gun.CurrentOwner.CenterPosition.ToVector3ZisY(0f), Quaternion.identity, this.m_gun.CurrentOwner.transform)).GetComponent<HeatIndicatorController>();
			if (!this.IgnitesEnemies)
			{
				this.m_radialIndicator.CurrentColor = Color.red.WithAlpha(20f);
				this.m_radialIndicator.IsFire = true;
			}
            else
            {
				this.m_radialIndicator.CurrentColor = Color.red.WithAlpha(20f);
			}
		}
	}

	// Token: 0x0600700F RID: 28687 RVA: 0x002B816B File Offset: 0x002B636B
	private void UnhandleRadialIndicator()
	{
		if (this.m_radialIndicatorActive)
		{
			this.m_radialIndicatorActive = false;
			if (this.m_radialIndicator)
			{
				this.m_radialIndicator.EndEffect();
			}
			this.m_radialIndicator = null;
		}
	}

	// Token: 0x06007010 RID: 28688 RVA: 0x002B81A4 File Offset: 0x002B63A4
	protected virtual void DoAura()
	{
		bool didDamageEnemies = false;
		PlayerController playerController = this.m_gun.CurrentOwner as PlayerController;
		if (this.AuraAction == null)
		{
			this.AuraAction = delegate (AIActor actor, float dist)
			{
				float num2 = this.DamagePerSecond * BraveTime.DeltaTime;
				if (this.IgnitesEnemies || num2 > 0f)
				{
					didDamageEnemies = true;
				}
				if (this.IgnitesEnemies)
				{
					actor.ApplyEffect(this.IgniteEffect, 1f, null);
				}
				actor.healthHaver.ApplyDamage(num2, Vector2.zero, "Aura", this.damageTypes, DamageCategory.Normal, false, null, false);
			};
		}
		if (playerController != null && playerController.CurrentRoom != null)
		{
			float num = this.AuraRadius;
			if (this.HasRadiusSynergy && playerController.HasActiveBonusSynergy(this.RadiusSynergy, false))
			{
				num = this.RadiusSynergyRadius;
			}
			if (this.m_radialIndicator)
			{
				this.m_radialIndicator.CurrentRadius = num;
			}
			playerController.CurrentRoom.ApplyActionToNearbyEnemies(playerController.CenterPosition, num, this.AuraAction);
		}
		if (didDamageEnemies)
		{
			playerController.DidUnstealthyAction();
		}
	}

	// Token: 0x06007011 RID: 28689 RVA: 0x002B827C File Offset: 0x002B647C
	private void OnDropped()
	{
		this.UnhandleRadialIndicator();
		PlayerController playerController = this.m_gun.CurrentOwner as PlayerController;
		if (playerController)
		{
			playerController.inventory.OnGunChanged -= this.OnGunChanged;
		}
	}

	// Token: 0x06007012 RID: 28690 RVA: 0x002B82C2 File Offset: 0x002B64C2
	private void OnGunChanged(Gun previous, Gun current, Gun previoussecondary, Gun currentsecondary, bool newgun)
	{
		if (current != this && currentsecondary != this)
		{
			this.UnhandleRadialIndicator();
		}
	}

	// Token: 0x06007013 RID: 28691 RVA: 0x002B82E4 File Offset: 0x002B64E4
	private void OnDestroy()
	{
		Gun gun = this.m_gun;
		gun.OnDropped = (Action)Delegate.Remove(gun.OnDropped, new Action(this.OnDropped));
		PlayerController playerController = this.m_gun.CurrentOwner as PlayerController;
		if (playerController)
		{
			playerController.inventory.OnGunChanged -= this.OnGunChanged;
		}
	}

	// Token: 0x04006F52 RID: 28498
	public float AuraRadius;

	// Token: 0x04006F53 RID: 28499
	public CoreDamageTypes damageTypes;

	// Token: 0x04006F54 RID: 28500
	public float DamagePerSecond;

	// Token: 0x04006F55 RID: 28501
	public bool IgnitesEnemies;

	// Token: 0x04006F56 RID: 28502
	public GameActorFireEffect IgniteEffect;

	// Token: 0x04006F57 RID: 28503
	public bool DoRadialIndicatorAnyway;

	// Token: 0x04006F58 RID: 28504
	public bool HasRadiusSynergy;

	// Token: 0x04006F59 RID: 28505
	[LongNumericEnum]
	public CustomSynergyType RadiusSynergy;

	// Token: 0x04006F5A RID: 28506
	public float RadiusSynergyRadius;

	// Token: 0x04006F5B RID: 28507
	private Gun m_gun;

	// Token: 0x04006F5C RID: 28508
	private Action<AIActor, float> AuraAction;

	// Token: 0x04006F5D RID: 28509
	private bool m_radialIndicatorActive;

	// Token: 0x04006F5E RID: 28510
	private HeatIndicatorController m_radialIndicator;
}