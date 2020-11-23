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
    public class StaticCharger : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Static Clips";
            string resourceName = "BunnyMod/Resources/staticclips";
            GameObject obj = new GameObject(itemName);
            StaticCharger staticCharger = obj.AddComponent<StaticCharger>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "OoOoOoh that tickles!";
            string longDesc = "An unusual pair of ammo clips that are designed to gather static energy created from reloading.\n\n" +
                "Despite how it may look like, a LOT of potential energy is wasted. Watch your ears.";
            staticCharger.SetupItem(shortDesc, longDesc, "bny");
            staticCharger.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, .90f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            staticCharger.quality = PickupObject.ItemQuality.B;
            staticCharger.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:static_clips",
            };
            List<string> optionalConsoleID1s = new List<string>
             {
                "shock_rounds",
                "battery_bullets",
                "shock_rifle",
                "thunderclap"
            };
            CustomSynergies.Add("Like putting a fork in a socket!", mandatoryConsoleIDs1, optionalConsoleID1s, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            base.Pickup(player);
            player.OnReloadedGun += (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
        }
        public override DebrisObject Drop(PlayerController player)
        {
            DebrisObject result = base.Drop(player);
            player.OnReloadedGun -= (Action<PlayerController, Gun>)Delegate.Remove(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
            return result;
        }
        private void HandleGunReloaded(PlayerController player, Gun playerGun)
        {
            bool flag5 = player.HasPickupID(298);
            bool flag2 = player.HasPickupID(410);
            bool flag3 = player.HasPickupID(13);
            bool flag4 = player.HasPickupID(153);
            if (flag5)
            {
                bool flag = playerGun.ClipShotsRemaining == 0 && (UnityEngine.Random.value <= 0.4f);
                if (flag)
                {
                    AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
                    for (int counter = 0; counter < 2; counter++)
                    {
                        Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
                        Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                        Vector3 vector2 = player.specRigidbody.UnitCenter;
                        GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                        Projectile component = gameObject.GetComponent<Projectile>();
                        HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                        homing.HomingRadius = 120;
                        homing.AngularVelocity = 120;
                        BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
                        bouncy.numberOfBounces = 1;
                        if (flag)
                        {
                            component.Owner = player;
                            component.Shooter = player.specRigidbody;
                        }

                    }
                }
            }
            if (flag2)
            {
                bool flag = playerGun.ClipShotsRemaining == 0 && (UnityEngine.Random.value <= 0.4f);
                if (flag)
                {
                    AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
                    for (int counter = 0; counter < 2; counter++)
                    {
                        Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
                        Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                        Vector3 vector2 = player.specRigidbody.UnitCenter;
                        GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                        Projectile component = gameObject.GetComponent<Projectile>();
                        HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                        homing.HomingRadius = 120;
                        homing.AngularVelocity = 120;
                        BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
                        bouncy.numberOfBounces = 1;
                        if (flag)
                        {
                            component.Owner = player;
                            component.Shooter = player.specRigidbody;
                        }

                    }
                }
            }
            if (flag3)
            {
                bool flag = playerGun.ClipShotsRemaining == 0 && (UnityEngine.Random.value <= 0.4f);
                if (flag)
                {
                    AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
                    for (int counter = 0; counter < 2; counter++)
                    {
                        Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
                        Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                        Vector3 vector2 = player.specRigidbody.UnitCenter;
                        GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                        Projectile component = gameObject.GetComponent<Projectile>();
                        HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                        homing.HomingRadius = 120;
                        homing.AngularVelocity = 120;
                        BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
                        bouncy.numberOfBounces = 1;
                        if (flag)
                        {
                            component.Owner = player;
                            component.Shooter = player.specRigidbody;
                        }

                    }
                }
            }
            if (flag4)
            {
                bool flag = playerGun.ClipShotsRemaining == 0 && (UnityEngine.Random.value <= 0.4f);
                if (flag)
                {
                    AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
                    for (int counter = 0; counter < 2; counter++)
                    {
                        Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
                        Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                        Vector3 vector2 = player.specRigidbody.UnitCenter;
                        GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                        Projectile component = gameObject.GetComponent<Projectile>();
                        HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                        homing.HomingRadius = 120;
                        homing.AngularVelocity = 120;
                        BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
                        bouncy.numberOfBounces = 1;
                        if (flag)
                        {
                            component.Owner = player;
                            component.Shooter = player.specRigidbody;
                        }

                    }
                }
            }
            else
            {
                bool flag = playerGun.ClipShotsRemaining == 0 && (UnityEngine.Random.value <= 0.6f);
                if (flag)
                {
                    Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
                    Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                    Vector3 vector2 = player.specRigidbody.UnitCenter;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 1.2f : player.CurrentGun.CurrentAngle), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    if (flag)
                    {
                        component.Owner = player;
                        component.Shooter = player.specRigidbody;
                    }
                    AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
                }
            }    
        }
        protected override void OnDestroy()
        {
            PlayerController owner = base.Owner;
            base.OnDestroy();
        }
    }
}