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
    public class BrokenLockpicker : PlayerItem
    {
        public static void Init()
        {
            string itemName = "Broken Lockpicker";
            string resourceName = "BunnyMod/Resources/lockpickbot";
            GameObject obj = new GameObject(itemName);
            BrokenLockpicker lockpicker = obj.AddComponent<BrokenLockpicker>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Tries its best";
            string longDesc = "A lockpicking machine that in it's prime would fire out nanobot swarms to open locks nearly instantly.\n\nHowever due to incredible clumsiness, this machine is broken and fails to interpret what a lock is.";
            lockpicker.SetupItem(shortDesc, longDesc, "bny");
            lockpicker.SetCooldownType(ItemBuilder.CooldownType.Damage, 1000f);
            lockpicker.consumable = false;
            lockpicker.quality = PickupObject.ItemQuality.B;
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Flynt, 1f);
            List<string> mandatoryConsoleIDs = new List<string>
            {
                "bny:broken_lockpicker",
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "akey47",
                "shelleton_key",
                "master_of_unlocking"
            };
            CustomSynergies.Add("Not like you'll need it...", mandatoryConsoleIDs, optionalConsoleIDs, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            bool flag1 = user.HasPickupID(95);
            bool flag2 = user.HasPickupID(166);
            bool flag3 = user.HasPickupID(140);
            if (flag1)
            {
                for (int counter = 0; counter < 3f; counter++)
                {
                    Projectile projectile = ((Gun)ETGMod.Databases.Items[39]).DefaultModule.projectiles[0];
                    Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
                    Vector3 vector2 = user.specRigidbody.UnitCenter;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, user.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
                    Projectile component = gameObject.GetComponent<Projectile>();
                    HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
                    homing.HomingRadius = 25f;
                    homing.AngularVelocity = 120;
                    {
                        component.Owner = user;
                        component.Shooter = user.specRigidbody;
                    }
                }
            }
            else
            if (flag2)
            {
                for (int counter = 0; counter < 3f; counter++)
                {
                    Projectile projectile = ((Gun)ETGMod.Databases.Items[39]).DefaultModule.projectiles[0];
                    Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
                    Vector3 vector2 = user.specRigidbody.UnitCenter;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, user.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
                    Projectile component = gameObject.GetComponent<Projectile>();
                    HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
                    homing.HomingRadius = 25f;
                    homing.AngularVelocity = 120;
                    {
                        component.Owner = user;
                        component.Shooter = user.specRigidbody;
                    }
                }

            }
            else
            if (flag3)
            {
                for (int counter = 0; counter < 3f; counter++)
                {
                    Projectile projectile = ((Gun)ETGMod.Databases.Items[39]).DefaultModule.projectiles[0];
                    Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
                    Vector3 vector2 = user.specRigidbody.UnitCenter;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, user.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
                    Projectile component = gameObject.GetComponent<Projectile>();
                    HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
                    homing.HomingRadius = 25f;
                    homing.AngularVelocity = 120;
                    {
                        component.Owner = user;
                        component.Shooter = user.specRigidbody;
                    }
                }

            }
            else
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[95]).DefaultModule.projectiles[0];
                Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
                Vector3 vector2 = user.specRigidbody.UnitCenter;
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, user.sprite.WorldCenter, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 359)));
                Projectile component = gameObject.GetComponent<Projectile>();
                {
                    component.Owner = user;
                    component.Shooter = user.specRigidbody;
                }
            }
        }

    }
}



