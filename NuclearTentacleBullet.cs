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
	// Token: 0x02000088 RID: 136
	public class Booletgobeeeg : MonoBehaviour
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0001C958 File Offset: 0x0001AB58
		private void Start()
		{
			this.m_projectile.baseData.damage *= 1f;
			this.m_projectile = base.GetComponent<Projectile>();
			this.m_projectile.OnPostUpdate += this.HandlePostUpdate;
			this.m_projectile.AdditionalScaleMultiplier *= 1f;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		private void HandlePostUpdate(Projectile proj)
		{
			bool flag = proj && proj.GetElapsedDistance() > 2f;
			if (flag)
			{
				proj.baseData.damage = 1f;
				proj.RuntimeUpdateScale(1.2f);
			}
		}

		// Token: 0x04000116 RID: 278
		private Projectile m_projectile;
	}
}
