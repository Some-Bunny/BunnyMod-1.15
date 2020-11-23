using System;
using Dungeonator;
using UnityEngine;


namespace BunnyMod
{
	// Token: 0x02000094 RID: 148
	public class AdvancedTransformGunSynergyProcessor : MonoBehaviour
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0001FB62 File Offset: 0x0001DD62
		public AdvancedTransformGunSynergyProcessor()
		{
			this.NonSynergyGunId = -1;
			this.SynergyGunId = -1;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001FB7A File Offset: 0x0001DD7A
		public void Awake()
		{
			this.m_gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		public void Update()
		{
			bool flag = Dungeon.IsGenerating || Dungeon.ShouldAttemptToLoadFromMidgameSave;
			if (!flag)
			{
				bool flag2 = this.m_gun && this.m_gun.CurrentOwner is PlayerController;
				if (flag2)
				{
					PlayerController player = this.m_gun.CurrentOwner as PlayerController;
					bool flag3 = !this.m_gun.enabled;
					if (flag3)
					{
						return;
					}
					bool flag4 = player.PlayerHasActiveSynergy(this.SynergyToCheck) && !this.m_transformed;
					if (flag4)
					{
						this.m_transformed = true;
						this.m_gun.TransformToTargetGun(PickupObjectDatabase.GetById(this.SynergyGunId) as Gun);
						bool shouldResetAmmoAfterTransformation = this.ShouldResetAmmoAfterTransformation;
						if (shouldResetAmmoAfterTransformation)
						{
							this.m_gun.ammo = this.ResetAmmoCount;
						}
					}
					else
					{
						bool flag5 = !player.PlayerHasActiveSynergy(this.SynergyToCheck) && this.m_transformed;
						if (flag5)
						{
							this.m_transformed = false;
							this.m_gun.TransformToTargetGun(PickupObjectDatabase.GetById(this.NonSynergyGunId) as Gun);
							bool shouldResetAmmoAfterTransformation2 = this.ShouldResetAmmoAfterTransformation;
							if (shouldResetAmmoAfterTransformation2)
							{
								this.m_gun.ammo = this.ResetAmmoCount;
							}
						}
					}
				}
				else
				{
					bool flag6 = this.m_gun && !this.m_gun.CurrentOwner && this.m_transformed;
					if (flag6)
					{
						this.m_transformed = false;
						this.m_gun.TransformToTargetGun(PickupObjectDatabase.GetById(this.NonSynergyGunId) as Gun);
						bool shouldResetAmmoAfterTransformation3 = this.ShouldResetAmmoAfterTransformation;
						if (shouldResetAmmoAfterTransformation3)
						{
							this.m_gun.ammo = this.ResetAmmoCount;
						}
					}
				}
				this.ShouldResetAmmoAfterTransformation = false;
			}
		}
		public string SynergyToCheck;

		// Token: 0x04000122 RID: 290
		public int NonSynergyGunId;

		// Token: 0x04000123 RID: 291
		public int SynergyGunId;

		// Token: 0x04000124 RID: 292
		private Gun m_gun;

		// Token: 0x04000125 RID: 293
		private bool m_transformed;

		// Token: 0x04000126 RID: 294
		public bool ShouldResetAmmoAfterTransformation;

		// Token: 0x04000127 RID: 295
		public int ResetAmmoCount;
	}
}
