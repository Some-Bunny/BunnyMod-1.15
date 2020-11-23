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
    public class SoulInAJar : PlayerItem
    {

        public static void Init()
        {
            string itemName = "Soul In A Jar";
            string resourceName = "BunnyMod/Resources/soulinajar";
            GameObject obj = new GameObject(itemName);
            SoulInAJar lockpicker = obj.AddComponent<SoulInAJar>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Little Fella'";
            string longDesc = "A tiny little gunslinger soul in a jar. Fly my pretty!";
            lockpicker.SetupItem(shortDesc, longDesc, "bny");
            lockpicker.SetCooldownType(ItemBuilder.CooldownType.Damage, 250f);
            lockpicker.consumable = false;
            lockpicker.quality = PickupObject.ItemQuality.D;
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:soul_in_a_jar"
            };
            List<string> optionalConsoleID1s = new List<string>
            {
                "shotgun_full_of_hate",
                "ghost_bullets",
                "skull_spitter",
                "bullet_idol",
                "clear_guon_stone",
                "gun_soul",
                "sixth_chamber",
                "cursed_bullets"

            };
            CustomSynergies.Add("Go! Go! Go!", mandatoryConsoleIDs1, optionalConsoleID1s, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_OBJ_bottle_cork_01", base.gameObject);
            bool synergy = user.PlayerHasActiveSynergy("Go! Go! Go!");
            if (synergy)
            {
                for (int counter = 0; counter < 3; counter++)
                {
                    Projectile projectile = ((Gun)ETGMod.Databases.Items[198]).DefaultModule.projectiles[0];
                    Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
                    Vector3 vector2 = user.specRigidbody.UnitCenter;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, user.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.LastOwner.CurrentGun == null) ? 1.2f : base.LastOwner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    component.baseData.range = 50f;

                    {
                        component.Owner = user;
                        component.Shooter = user.specRigidbody;
                    }
                }
            }
            else
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[198]).DefaultModule.projectiles[0];
                Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
                Vector3 vector2 = user.specRigidbody.UnitCenter;
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, user.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.LastOwner.CurrentGun == null) ? 1.2f : base.LastOwner.CurrentGun.CurrentAngle)), true);
                Projectile component = gameObject.GetComponent<Projectile>();
                component.baseData.range = 50f;

                {
                    component.Owner = user;
                    component.Shooter = user.specRigidbody;
                }
            }
        }
    }
}



