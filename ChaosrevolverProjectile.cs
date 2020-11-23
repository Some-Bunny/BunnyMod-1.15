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



namespace BunnyMod
{// Token: 0x02000009 RID: 9
	internal class ChaosRevolverProjectile : MonoBehaviour
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00003998 File Offset: 0x00001B98
		public void Start()
		{
			this.boxprojectile = base.GetComponent<Projectile>();
			this.player = (this.boxprojectile.Owner as PlayerController);
			Projectile projectile = this.boxprojectile;
			this.boxprojectile.sprite.GetSpriteIdByName("boxgun_projectile_001");
		}
		// Token: 0x04000009 RID: 9
		private Projectile boxprojectile;
		// Token: 0x0400000A RID: 10
		private PlayerController player;
	}
}