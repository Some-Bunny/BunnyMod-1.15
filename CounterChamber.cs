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
    public class CounterChamber : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Counterproductive Chamber";
            string resourceName = "BunnyMod/Resources/counterproductivechamber";
            GameObject obj = new GameObject(itemName);
            CounterChamber counterChamber = obj.AddComponent<CounterChamber>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "It just works";
            string longDesc = "A lazy gungeoneer was once met with the dilemma of having to reload their chambers fully.\n\n" + "Their soultion?\n\n" + "'Half fill 'em clips!!\n\n" +
                "Even after their death, the eccentricity of the supposed genius lives on.";

            counterChamber.SetupItem(shortDesc, longDesc, "bny");
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, 0.5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.RateOfFire, .1f, StatModifier.ModifyMethod.ADDITIVE);
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.ProjectileSpeed, .2f, StatModifier.ModifyMethod.ADDITIVE);
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.AdditionalClipCapacityMultiplier, 0.5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            counterChamber.quality = PickupObject.ItemQuality.B;
            counterChamber.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            List<string> mandatoryConsoleIDs14 = new List<string>
            {
                "bny:counterproductive_chamber"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "oiled_cylinder",
                "sixth_chamber",
                "yellow_chamber"
            };
            CustomSynergies.Add("(6/2)+6 = Chamber has 9 Holes???", mandatoryConsoleIDs14, optionalConsoleIDs, true);
        }
        private void HandleGunReloaded(PlayerController player, Gun playerGun)
        {
            bool synergy = base.Owner.PlayerHasActiveSynergy("(6/2)+6 = Chamber has 9 Holes???");
            if (synergy)
            {
                bool flag = playerGun.ClipShotsRemaining == 0;
                if (flag)
                {
                    for (int counter = 0; counter < 3; counter++)
                    {
                        Projectile projectile = ((Gun)ETGMod.Databases.Items[221]).DefaultModule.projectiles[0];
                        Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                        Vector3 vector2 = player.specRigidbody.UnitCenter;
                        GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((player.CurrentGun == null) ? 1.2f : player.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                        Projectile component = gameObject.GetComponent<Projectile>();
                        HomingModifier homing = component.gameObject.AddComponent<HomingModifier>();
                        homing.HomingRadius = 120;
                        homing.AngularVelocity = 120;
                        if (flag)
                        {
                            component.Owner = player;
                            component.Shooter = player.specRigidbody;
                        }
                    }
                }

            }
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnReloadedGun += (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
        }

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnReloadedGun -= (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }

    }
}