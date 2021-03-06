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
    public class ChaosGodsWrath : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Effigy Of ChaosGod";
            string resourceName = "BunnyMod/Resources/effigyofchaosgod";
            GameObject obj = new GameObject(itemName);
            ChaosGodsWrath chaosGodsWrath = obj.AddComponent<ChaosGodsWrath>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Sealed Guardian";
            string longDesc = "A guarding effigy of ChaosGod himself.\n\nProtects the carrier of the effigy in dangerous situations.";
            chaosGodsWrath.SetupItem(shortDesc, longDesc, "bny");
            chaosGodsWrath.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            chaosGodsWrath.quality = PickupObject.ItemQuality.A;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            AkSoundEngine.PostEvent("Play_VO_lichA_chuckle_01", base.gameObject);
            player.healthHaver.OnDamaged += new HealthHaver.OnDamagedEvent(this.ChaosHole);
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.healthHaver.OnDamaged -= new HealthHaver.OnDamagedEvent(this.ChaosHole);
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            return base.Drop(player);

        }
        private void ChaosHole(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
        {
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items["black_hole_gun"]).DefaultModule.projectiles[0];
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : base.Owner.CurrentGun.CurrentAngle), true);
                Projectile component = gameObject.GetComponent<Projectile>();
                bool flag = component != null;
                bool flag2 = flag;
                if (flag2)
                {
                    component.Owner = base.Owner;
                    component.Shooter = base.Owner.specRigidbody;
                    component.baseData.speed = 4f;
                    component.baseData.damage = 0.1f;
                }
            }
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            bool flag1 = base.Owner.HasPickupID(Game.Items["bny:cool_robes"].PickupObjectId);
            if (flag1)
            {
                if (enemyHealth.specRigidbody != null)
                {
                    bool flag = enemyHealth.aiActor && fatal;
                    if (flag)
                    {
                        bool flagA = !ChaosGodsWrath.onCooldown;
                        bool flag2 = flagA;
                        if (flag2)
                        {
                            ChaosGodsWrath.onCooldown = true;
                            GameManager.Instance.StartCoroutine(ChaosGodsWrath.StartCooldown());
                            this.activateSlow(base.Owner);
                        }
                    }
                }
            }
        }

        protected void activateSlow(PlayerController user)
        {
            new RadialSlowInterface
            {
                DoesSepia = true,
                RadialSlowHoldTime = 1f,
                RadialSlowTimeModifier = 0.1f
            }.DoRadialSlow(user.specRigidbody.UnitCenter, user.CurrentRoom);
        }
        private static IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(3f);
            ChaosGodsWrath.onCooldown = false;
            yield break;
        }
        public PlayerController player;
        private static bool onCooldown;
    }
}