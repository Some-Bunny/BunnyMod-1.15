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

    public class GestureOfTheJammed : PassiveItem
    {
        public static void Init()
        {
            string name = "Gesture Of The Jammed";
            string resourcePath = "BunnyMod/Resources/gesutreofthejammed.png";
            GameObject gameObject = new GameObject();
            GestureOfTheJammed GunSoulBox = gameObject.AddComponent<GestureOfTheJammed>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject, true);
            string shortDesc = "Eternal Damnation";
            string longDesc = "Forbidden sorrows of the Jammed given shape, form. For it is not enough to be in agony alone, one must spread it to others like a sickness.";
            GunSoulBox.SetupItem(shortDesc, longDesc, "bny");
            GunSoulBox.quality = PickupObject.ItemQuality.D;
            GunSoulBox.AddPassiveStatModifier(PlayerStats.StatType.Curse, 1f, StatModifier.ModifyMethod.ADDITIVE);
            GunSoulBox.AddPassiveStatModifier(PlayerStats.StatType.AmmoCapacityMultiplier, .2f, StatModifier.ModifyMethod.ADDITIVE);
            GunSoulBox.AddPassiveStatModifier(PlayerStats.StatType.AdditionalClipCapacityMultiplier, .5f, StatModifier.ModifyMethod.ADDITIVE);
            GunSoulBox.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);


        }
        private float random;


        // Token: 0x06000060 RID: 96 RVA: 0x0000534D File Offset: 0x0000354D
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
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag = enemyHealth.aiActor && fatal && enemyHealth.aiActor.IsBlackPhantom;
                if (flag)
                {
                    this.m_owner.CurrentRoom.ApplyActionToNearbyEnemies(enemyHealth.aiActor.CenterPosition, 4.5f, new Action<AIActor, float>(this.ProcessEnemy));
                    PlayerController player = (GameManager.Instance.PrimaryPlayer);
                    float num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
                    this.random = UnityEngine.Random.Range(0.000f, 1.000f);
                    if (this.random <= (0.03 * (1 + (num / 7))))
                    {
                        SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, enemyHealth.aiActor.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(enemyHealth.aiActor.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        LootEngine.SpawnItem(ETGMod.Databases.Items["Cursed Pearl"].gameObject, enemyHealth.aiActor.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
                    }
                }
            }
        }
        private void ProcessEnemy(AIActor target, float distance)
        {
            bool jamnation = target.IsBlackPhantom;
            if (!jamnation)
            {
                target.PlayEffectOnActor(ResourceCache.Acquire("Global VFX/VFX_Curse") as GameObject, Vector3.zero, true, false, false);
                target.BecomeBlackPhantom();
            }
        }
    }
}



