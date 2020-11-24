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
    public class EmpoweringCore : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Empowering Core";
            string resourceName = "BunnyMod/Resources/empoweringcore.png";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<EmpoweringCore>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Powe Eterna";
            string longDesc = "A pure energy core. No flaws, no diminishing returns. Just pure eternal power.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, 1.40f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            item.quality = PickupObject.ItemQuality.S;
            List<string> mandatoryConsoleIDs2 = new List<string>
            {
                "bny:empowering_core",
                "bny:broken_core"
            };
            CustomSynergies.Add("Ego Sum Aeternae", mandatoryConsoleIDs2, null, true);
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            bool flag2 = base.Owner.PlayerHasActiveSynergy("Ego Sum Aeternae");
            if (flag2)
            {
                if (enemyHealth.specRigidbody != null)
                {
                    bool y = enemyHealth.aiActor && UnityEngine.Random.value <= 0.0714f;
                    if (y)
                    {
                        Projectile projectile = ((Gun)ETGMod.Databases.Items[508]).DefaultModule.projectiles[0];
                        GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle)), true);
                        Projectile component = gameObject.GetComponent<Projectile>();
                        bool flag = component != null;
                        bool r = flag;
                        if (r)
                        {
                            component.Owner = base.Owner;
                            component.Shooter = base.Owner.specRigidbody;
                            component.baseData.speed = 100f;
                            component.baseData.damage = 40f;
                        }
                    }

                }
            }
        }
        public override void Pickup(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));

            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            return base.Drop(player);
        }
        //private float LaserTrue = 20f;
    }
}



