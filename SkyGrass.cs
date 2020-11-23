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
    public class SkyGrass : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Sky Grass";
            string resourceName = "BunnyMod/Resources/skygrass";
            GameObject obj = new GameObject(itemName);
            SkyGrass skyGrass = obj.AddComponent<SkyGrass>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Grown from Purple Rain";
            string longDesc = "An unusally soft patch of grass grown on a meadow on a faraway planet.\n\n" +
                "Padding your shoes with it makes you really comfortable. The spilt lifeforce of the great creatures that once inhabited the Meadow still lives on. ";
            skyGrass.SetupItem(shortDesc, longDesc, "bny");
            skyGrass.AddPassiveStatModifier(PlayerStats.StatType.MovementSpeed, 1.15f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            skyGrass.quality = PickupObject.ItemQuality.C;
            skyGrass.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            List<string> mandatoryConsoleIDs15 = new List<string>
            {
                "bny:sky_grass"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "shotgun_full_of_love",
                "crescent_crossbow",
                "abyssal_tentacle",
                "hyper_light_blaster",
                "life_orb",
                "magic_sweet",
                "gundromeda_strain",
                "bloody_eye",
                "rolling_eye",
                "heart_purse",
                "wingman",
                "charming_rounds",
                "magic_bullets"
            };
            CustomSynergies.Add("Purple Rain", mandatoryConsoleIDs15, optionalConsoleIDs, true);
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag = enemyHealth.aiActor && fatal && UnityEngine.Random.value <= 0.1f;
                if (flag)
                {
                    bool flag2 = base.Owner.PlayerHasActiveSynergy("Purple Rain");
                    if (flag2)
                    {
                        this.Boom2(enemyHealth.sprite.WorldCenter);
                    }
                    else
                    {
                        this.Boom(enemyHealth.sprite.WorldCenter);
                    }
                }
            }

        }
        public void Boom(Vector3 position)
        {
            AkSoundEngine.PostEvent("Play_WPN_Life_Orb_Blast_01", base.gameObject);
            ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
            this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
            this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
        }
        public void Boom2(Vector3 position)
        {
            AkSoundEngine.PostEvent("Play_WPN_Life_Orb_Blast_01", base.gameObject);
            ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion1.effect = defaultSmallExplosionData2.effect;
            this.smallPlayerSafeExplosion1.ignoreList = defaultSmallExplosionData2.ignoreList;
            this.smallPlayerSafeExplosion1.ss = defaultSmallExplosionData2.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion1, Vector2.zero, null, false, CoreDamageTypes.None, false);
        }

        private ExplosionData smallPlayerSafeExplosion = new ExplosionData
        {
            damageRadius = 4f,
            damageToPlayer = 0f,
            doDamage = true,
            damage = 10f,
            doExplosionRing = true,
            doDestroyProjectiles = false,
            doForce = true,
            debrisForce = 20f,
            preventPlayerForce = true,
            explosionDelay = 0.1f,
            usesComprehensiveDelay = false,
            doScreenShake = false,
            playDefaultSFX = false,
        };
        private ExplosionData smallPlayerSafeExplosion1 = new ExplosionData
        {
            damageRadius = 8f,
            damageToPlayer = 0f,
            doDamage = true,
            damage = 20f,
            doExplosionRing = true,
            doDestroyProjectiles = false,
            doForce = true,
            debrisForce = 25f,
            preventPlayerForce = true,
            explosionDelay = 0.1f,
            usesComprehensiveDelay = false,
            doScreenShake = true,
            playDefaultSFX = false,
        };

        public Vector3 position { get; private set; }

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
