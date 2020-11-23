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
{
    public class SuperShield : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Over-Shield";
            string resourceName = "BunnyMod/Resources/supershield.png";
            GameObject obj = new GameObject(itemName);
			SuperShield counterChamber = obj.AddComponent<SuperShield>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Green Power";
            string longDesc = "Negates 2 hits. Recharges on entering a new floor.\n\nA downsized replica of the Super Shields used by military vessels. Although to actually generate one requires as much energy as one FTL jump, the elevators of the Gungeon are good enough to power one of a smaller scale.";
            counterChamber.SetupItem(shortDesc, longDesc, "bny");
            counterChamber.quality = PickupObject.ItemQuality.A;
        }
		protected override void Update()
		{
			bool flag = base.Owner;
			if (flag)
			{
				bool flag2 = (this.damageInstances <= 2);
				if (flag2)
				{
					this.EnableVFX(base.Owner);
				}
                bool dmg = (this.damageInstances == 2);
				if (dmg)
				{
					this.DisableVFX(base.Owner);
				}
			}
		}
		private void HandleEffect(HealthHaver source, HealthHaver.ModifyDamageEventArgs args)
		{
			bool flag = args == EventArgs.Empty;
			if (!flag)
			{
				bool flag2 = args.ModifiedDamage <= 0f;
				if (!flag2)
				{
					bool flag3 = !source.IsVulnerable;
					if (!flag3)
					{
						bool dmgcheck = damageInstances < 2;
						if (dmgcheck)
                        {
							bool flag4 = base.Owner;
							if (flag4)
							{
								AkSoundEngine.PostEvent("Play_BOSS_agunim_ribbons_01", gameObject);
								source.StartCoroutine("IncorporealityOnHit");
								source.TriggerInvulnerabilityPeriod(-1f);
								args.ModifiedDamage = 0f;
								this.damageInstances += 1;
							}
						}
					}
				}
			}
		}
		private void EnableVFX(PlayerController user)
		{
			Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
			outlineMaterial.SetColor("_OverrideColor", new Color(0f, 120f, 30f));
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00032B38 File Offset: 0x00030D38
		private void DisableVFX(PlayerController user)
		{
			Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
			outlineMaterial.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
		}
		private void OnNewFloor()
		{
			this.DisableVFX(base.Owner);
			PlayerController owner = base.Owner;
			this.damageInstances = 0;
		}
		public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
			bool flag = !this.m_pickedUpThisRun;
			bool flag2 = flag;
			if (flag2)
			{
				this.damageInstances = 0;
			}
			GameManager.Instance.OnNewLevelFullyLoaded += this.OnNewFloor;
			HealthHaver healthHaver = player.healthHaver;
            healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Combine(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));
            base.Pickup(player);
        }
		public override DebrisObject Drop(PlayerController player)
		{
            this.DisableVFX(base.Owner);
            {
				GameManager.Instance.OnNewLevelFullyLoaded -= this.OnNewFloor;
				HealthHaver healthHaver = player.healthHaver;
				healthHaver.ModifyDamage = (Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>)Delegate.Remove(healthHaver.ModifyDamage, new Action<HealthHaver, HealthHaver.ModifyDamageEventArgs>(this.HandleEffect));
				return base.Drop(player);
			}
		}
		private int damageInstances;
    }
}