using System;
using System.Collections.Generic;
using UnityEngine;
using ItemAPI;

namespace BunnyMod
{
    public class GreandeParasite : PassiveItem
    {

        public ItemQuality Spawnquality { get; internal set; }

        public static void Init()
        {
            string itemName = "Pinhead Parasite";
            string resourceName = "BunnyMod/Resources/pinheadparasite";
            GameObject obj = new GameObject(itemName);
            GreandeParasite greandeParasite = obj.AddComponent<GreandeParasite>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Spiteful";
            string longDesc = "Little known fact is that pinheads are infact parasites that grow in gundead.\n\n" +
                "This little bugger lays eggs in your bullets, for you to fire them into gundead. Oh the horror!";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.quality = PickupObject.ItemQuality.A;
            greandeParasite.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            greandeParasite.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            List<string> mandatoryConsoleIDs = new List<string>
            {
                "bny:pinhead_parasite"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "poison_vial",
                "irradiated_lead",
                "gundromeda_strain",
                "monster_blood",
                "plague_pistol",
                "plunger"
            };
            CustomSynergies.Add("Bio-Warfare", mandatoryConsoleIDs, optionalConsoleIDs, true);
            List<string> mandatoryConsoleIDs2 = new List<string>
            {
                "bny:pinhead_parasite",
                "bullet_bore"
            };
            CustomSynergies.Add("Evolved", mandatoryConsoleIDs2, null, true);
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag = enemyHealth.aiActor && fatal;
                if (flag)
                {
                    bool flag1 = Owner.HasPickupID(205);
                    bool flag2 = Owner.HasPickupID(204);
                    bool flag3 = Owner.HasPickupID(159);
                    bool flag4 = Owner.HasPickupID(313);
                    bool flag5 = Owner.HasPickupID(207);
                    bool flag6 = Owner.HasPickupID(208);
                    if (flag1)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                    else
                    if (flag2)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                    else
                    if (flag3)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                    else
                    if (flag4)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                    else
                    if (flag5)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                    else
                    if (flag6)
                    {
                        for (int counter = 0; counter < 3; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                    else
                    {
                        for (int counter = 0; counter < 2; counter++)
                        {
                            this.Spited(enemyHealth.sprite.WorldCenter);
                        }
                    }
                }
            }
        }

        public void Spited(Vector3 position)
        {
            bool flag4 = Owner.HasPickupID(362);
            if (flag4)
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[362]).DefaultModule.projectiles[0];
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 359)));
                Projectile component2 = gameObject.GetComponent<Projectile>();
                PierceProjModifier spook = component2.gameObject.AddComponent<PierceProjModifier>();
                spook.penetration = 5;
                {
                    component2.Owner = Owner;
                    component2.Shooter = Owner.specRigidbody;
                }
            }
            else
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[19]).DefaultModule.projectiles[0];
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 359)));
                Projectile component2 = gameObject.GetComponent<Projectile>();
                PierceProjModifier spook = component2.gameObject.AddComponent<PierceProjModifier>();
                spook.penetration = 1;
                {
                    component2.Owner = Owner;
                    component2.Shooter = Owner.specRigidbody;
                }
            }
        }


        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}



