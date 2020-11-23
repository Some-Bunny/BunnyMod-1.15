using System;
using System.Reflection;
using UnityEngine;

namespace ItemAPI
{
	// Token: 0x02000014 RID: 20
	public class AdvancedGunBehavior : MonoBehaviour
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00007A00 File Offset: 0x00005C00
		protected virtual void Update()
		{
			bool flag = this.Player != null;
			if (flag)
			{
				this.lastPlayer = this.Player;
				bool flag2 = !this.everPickedUpByPlayer;
				if (flag2)
				{
					this.everPickedUpByPlayer = true;
				}
			}
			bool flag3 = this.Player != null && !this.pickedUpLast;
			if (flag3)
			{
				this.OnPickup(this.Player);
				this.pickedUpLast = true;
			}
			bool flag4 = this.Player == null && this.pickedUpLast;
			if (flag4)
			{
				bool flag5 = this.lastPlayer != null;
				if (flag5)
				{
					this.OnPostDrop(this.lastPlayer);
					this.lastPlayer = null;
				}
				this.pickedUpLast = false;
			}
			bool flag6 = this.gun != null && !this.gun.IsReloading && !this.hasReloaded;
			if (flag6)
			{
				this.hasReloaded = true;
			}
			this.gun.PreventNormalFireAudio = this.preventNormalFireAudio;
			this.gun.OverrideNormalFireAudioEvent = this.overrrideNormalFireAudio;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007B1C File Offset: 0x00005D1C
		public virtual void Start()
		{
			this.gun = base.GetComponent<Gun>();
			Gun gun = this.gun;
			gun.OnInitializedWithOwner = (Action<GameActor>)Delegate.Combine(gun.OnInitializedWithOwner, new Action<GameActor>(this.OnInitializedWithOwner));
			Gun gun2 = this.gun;
			gun2.PostProcessProjectile = (Action<Projectile>)Delegate.Combine(gun2.PostProcessProjectile, new Action<Projectile>(this.PostProcessProjectile));
			Gun gun3 = this.gun;
			gun3.PostProcessVolley = (Action<ProjectileVolleyData>)Delegate.Combine(gun3.PostProcessVolley, new Action<ProjectileVolleyData>(this.PostProcessVolley));
			Gun gun4 = this.gun;
			gun4.OnDropped = (Action)Delegate.Combine(gun4.OnDropped, new Action(this.OnDropped));
			Gun gun5 = this.gun;
			gun5.OnAutoReload = (Action<PlayerController, Gun>)Delegate.Combine(gun5.OnAutoReload, new Action<PlayerController, Gun>(this.OnAutoReload));
			Gun gun6 = this.gun;
			gun6.OnReloadPressed = (Action<PlayerController, Gun, bool>)Delegate.Combine(gun6.OnReloadPressed, new Action<PlayerController, Gun, bool>(this.OnReloadPressed));
			Gun gun7 = this.gun;
			gun7.OnFinishAttack = (Action<PlayerController, Gun>)Delegate.Combine(gun7.OnFinishAttack, new Action<PlayerController, Gun>(this.OnFinishAttack));
			Gun gun8 = this.gun;
			gun8.OnPostFired = (Action<PlayerController, Gun>)Delegate.Combine(gun8.OnPostFired, new Action<PlayerController, Gun>(this.OnPostFired));
			Gun gun9 = this.gun;
			gun9.OnAmmoChanged = (Action<PlayerController, Gun>)Delegate.Combine(gun9.OnAmmoChanged, new Action<PlayerController, Gun>(this.OnAmmoChanged));
			Gun gun10 = this.gun;
			gun10.OnBurstContinued = (Action<PlayerController, Gun>)Delegate.Combine(gun10.OnBurstContinued, new Action<PlayerController, Gun>(this.OnBurstContinued));
			Gun gun11 = this.gun;
			gun11.OnPreFireProjectileModifier = (Func<Gun, Projectile, ProjectileModule, Projectile>)Delegate.Combine(gun11.OnPreFireProjectileModifier, new Func<Gun, Projectile, ProjectileModule, Projectile>(this.OnPreFireProjectileModifier));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007CEE File Offset: 0x00005EEE
		public virtual void OnInitializedWithOwner(GameActor actor)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007CF1 File Offset: 0x00005EF1
		public virtual void PostProcessProjectile(Projectile projectile)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public virtual void PostProcessVolley(ProjectileVolleyData volley)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007CF7 File Offset: 0x00005EF7
		public virtual void OnDropped()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007CFA File Offset: 0x00005EFA
		public virtual void OnAutoReload(PlayerController player, Gun gun)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007D00 File Offset: 0x00005F00
		public virtual void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			bool flag = this.hasReloaded;
			if (flag)
			{
				this.OnReload(player, gun);
				this.hasReloaded = false;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007D2C File Offset: 0x00005F2C
		public virtual void OnReload(PlayerController player, Gun gun)
		{
			bool flag = this.preventNormalReloadAudio;
			if (flag)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				bool flag2 = !string.IsNullOrEmpty(this.overrideNormalReloadAudio);
				if (flag2)
				{
					AkSoundEngine.PostEvent(this.overrideNormalReloadAudio, base.gameObject);
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007D7D File Offset: 0x00005F7D
		public virtual void OnFinishAttack(PlayerController player, Gun gun)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007D80 File Offset: 0x00005F80
		public virtual void OnPostFired(PlayerController player, Gun gun)
		{
			bool isHeroSword = gun.IsHeroSword;
			if (isHeroSword)
			{
				bool flag = (float)AdvancedGunBehavior.heroSwordCooldown.GetValue(gun) == 0.5f;
				if (flag)
				{
					this.OnHeroSwordCooldownStarted(player, gun);
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public virtual void OnHeroSwordCooldownStarted(PlayerController player, Gun gun)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00007DC3 File Offset: 0x00005FC3
		public virtual void OnAmmoChanged(PlayerController player, Gun gun)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007DC6 File Offset: 0x00005FC6
		public virtual void OnBurstContinued(PlayerController player, Gun gun)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007DCC File Offset: 0x00005FCC
		public virtual Projectile OnPreFireProjectileModifier(Gun gun, Projectile projectile, ProjectileModule mod)
		{
			return projectile;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007E05 File Offset: 0x00006005
		protected virtual void OnPickup(PlayerController player)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007E08 File Offset: 0x00006008
		protected virtual void OnPostDrop(PlayerController player)
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007E0C File Offset: 0x0000600C
		public bool PickedUp
		{
			get
			{
				return this.gun.CurrentOwner != null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00007E30 File Offset: 0x00006030
		public PlayerController Player
		{
			get
			{
				bool flag = this.gun.CurrentOwner is PlayerController;
				PlayerController result;
				if (flag)
				{
					result = (this.gun.CurrentOwner as PlayerController);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00007E70 File Offset: 0x00006070
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00007EB0 File Offset: 0x000060B0
		public float HeroSwordCooldown
		{
			get
			{
				bool flag = this.gun != null;
				float result;
				if (flag)
				{
					result = (float)AdvancedGunBehavior.heroSwordCooldown.GetValue(this.gun);
				}
				else
				{
					result = -1f;
				}
				return result;
			}
			set
			{
				bool flag = this.gun != null;
				if (flag)
				{
					AdvancedGunBehavior.heroSwordCooldown.SetValue(this.gun, value);
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00007EE8 File Offset: 0x000060E8
		public GameActor Owner
		{
			get
			{
				return this.gun.CurrentOwner;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00007F08 File Offset: 0x00006108
		public bool OwnerIsPlayer
		{
			get
			{
				return this.Player != null;
			}
		}

		// Token: 0x04000048 RID: 72
		private bool pickedUpLast = false;

		// Token: 0x04000049 RID: 73
		private PlayerController lastPlayer = null;

		// Token: 0x0400004A RID: 74
		public bool everPickedUpByPlayer = false;

		// Token: 0x0400004B RID: 75
		protected Gun gun;

		// Token: 0x0400004C RID: 76
		private bool hasReloaded = true;

		// Token: 0x0400004D RID: 77
		protected bool preventNormalFireAudio;

		// Token: 0x0400004E RID: 78
		protected bool preventNormalReloadAudio;

		// Token: 0x0400004F RID: 79
		protected string overrrideNormalFireAudio;

		// Token: 0x04000050 RID: 80
		protected string overrideNormalReloadAudio;

		// Token: 0x04000051 RID: 81
		private static FieldInfo heroSwordCooldown = typeof(Gun).GetField("HeroSwordCooldown", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
