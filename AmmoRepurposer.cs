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
    public class AmmoRepurposer : PassiveItem
    {
        private PlayerController m_buffedTarget;
        private StatModifier m_temporaryModifier;
        private bool onCooldown;

        public AmmoRepurposer()
        {
        }

        public static void Init()
        {
            string itemName = "Ammo Repurposer";
            string resourceName = "BunnyMod/Resources/ammorepurposer";
            GameObject obj = new GameObject(itemName);
            AmmoRepurposer ammoRepurposer = obj.AddComponent<AmmoRepurposer>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Shell Space";
            string longDesc = "This unusual box is actually a wormhole to another dimension, where an even more unusual cosmic horror resides. \n\n" +
                "If it smells blood on a casing, it will occasionally rip a small dimensional tear inside of it, somehow working like a small pocket.\n\n" + "The horrors name is George.";
            ammoRepurposer.SetupItem(shortDesc, longDesc, "bny");
            ammoRepurposer.quality = PickupObject.ItemQuality.C;
            ammoRepurposer.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            ammoRepurposer.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            List<string> mandatoryConsoleIDs = new List<string>
            {
                "bny:ammo_repurposer"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "black_hole_gun",
                "singularity",
                "abyssal_tentacle",
                "yellow_chamber"
            };
            CustomSynergies.Add("Oh, the George!", mandatoryConsoleIDs, optionalConsoleIDs, true);
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:ammo_repurposer"
            };
            List<string> optionalConsoleIDs2 = new List<string>
            {
                "drum_clip",
                "ammo_belt",
                "utility_belt"
            };
            CustomSynergies.Add("Awaken the Beast", mandatoryConsoleIDs1, optionalConsoleIDs2, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.PostProcessProjectile += this.PostProcessProjectile;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.PostProcessProjectile -= this.PostProcessProjectile;
            return base.Drop(player);

        }
        public void PostProcessProjectile(Projectile sourceProjectile, float effectChanceScalar)
        {
            bool flag10 = Owner.HasPickupID(280);
            bool flag20 = Owner.HasPickupID(134);
            bool flag30 = Owner.HasPickupID(131);
            if (flag10)
            {
                bool flag = UnityEngine.Random.value <= 0.02f;
                if (flag && !this.onCooldown)
                {
                    this.onCooldown = true;
                    sourceProjectile.OnHitEnemy += RepurposeShells;
                    GameManager.Instance.StartCoroutine(StartCooldown());

                }

            }
            else
            if (flag20)
            {
                bool flag = UnityEngine.Random.value <= 0.02f;
                if (flag && !this.onCooldown)
                {
                    this.onCooldown = true;
                    sourceProjectile.OnHitEnemy += RepurposeShells;
                    GameManager.Instance.StartCoroutine(StartCooldown());

                }

            }
            else
            if (flag30)
            {
                bool flag = UnityEngine.Random.value <= 0.02f;
                if (flag && !this.onCooldown)
                {
                    this.onCooldown = true;
                    sourceProjectile.OnHitEnemy += RepurposeShells;
                    GameManager.Instance.StartCoroutine(StartCooldown());

                }

            }
            else
            {
                bool flag = UnityEngine.Random.value <= 0.0133f;
                if (flag && !this.onCooldown)
                {
                    this.onCooldown = true;
                    sourceProjectile.OnHitEnemy += RepurposeShells;
                    GameManager.Instance.StartCoroutine(StartCooldown());

                }
            }
            bool flag1 = Owner.HasPickupID(169);
            bool flag2 = Owner.HasPickupID(155);
            bool flag3 = Owner.HasPickupID(474);
            bool flag4 = Owner.HasPickupID(570);
            if (flag1)
            {
                bool flag = UnityEngine.Random.value <= 0.03f;
                if (flag)
                {
                    this.SynergyOhTheGeorge(sprite.WorldCenter);
                }
            }
            else
            if (flag2)
            {
                bool flag = UnityEngine.Random.value <= 0.03f;
                if (flag)
                {
                    this.SynergyOhTheGeorge(sprite.WorldCenter);
                }
            }
            else
            if (flag3)
            {
                bool flag = UnityEngine.Random.value <= 0.03f;
                if (flag)
                {
                    this.SynergyOhTheGeorge(sprite.WorldCenter);
                }
            }
            else
            if (flag4)
            {
                bool flag = UnityEngine.Random.value <= 0.03f;
                if (flag)
                {
                    this.SynergyOhTheGeorge(sprite.WorldCenter);
                }
            }

        }
        private void RepurposeShells(Projectile projectile, SpeculativeRigidbody pop, bool bol)
        {
            PlayerController user = base.Owner;
            this.m_buffedTarget = user;
            this.m_temporaryModifier = new StatModifier();
            this.m_temporaryModifier.statToBoost = PlayerStats.StatType.AmmoCapacityMultiplier;
            this.m_temporaryModifier.amount = .01f;
            this.m_temporaryModifier.modifyType = StatModifier.ModifyMethod.ADDITIVE;
            user.ownerlessStatModifiers.Add(this.m_temporaryModifier);
            user.stats.RecalculateStats(user, false, false);

        }
        private void SynergyOhTheGeorge(Vector3 position)
        {
            for (int counter = 0; counter < 8; counter++)
            {

                Projectile projectile = ((Gun)ETGMod.Databases.Items[357]).DefaultModule.projectiles[0];
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-45, 45)), true);
                Projectile component = gameObject.GetComponent<Projectile>();
                bool flag = component != null;
                bool flag2 = flag;
                if (flag2)
                {
                    component.Owner = base.Owner;
                    component.Shooter = base.Owner.specRigidbody;
                    component.baseData.speed = 10f;
                }
            }

        }

        private IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(1f);
            this.onCooldown = false;
            yield break;
        }

    }
}
