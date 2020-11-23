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
    public class DGrenade : PlayerItem
    {

        public static void Init()
        {
            string itemName = "D-Grenade";
            string resourceName = "BunnyMod/Resources/DGrenade";
            GameObject obj = new GameObject(itemName);
            DGrenade lockpicker = obj.AddComponent<DGrenade>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Pull the Pin...";
            string longDesc = "An incredibly powerful grenade made by Daisuke before slipping into DnD madness. Only use it if you are extremely daring, because once the pin is pulled, the consequences are on YOU.";
            lockpicker.SetupItem(shortDesc, longDesc, "bny");
            lockpicker.SetCooldownType(ItemBuilder.CooldownType.Timed, 1f);
            lockpicker.consumable = true;
            lockpicker.quality = PickupObject.ItemQuality.B;
            lockpicker.AddToSubShop(ItemBuilder.ShopType.Flynt, 1f);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            Projectile projectile = ((Gun)ETGMod.Databases.Items[19]).DefaultModule.projectiles[0];
            Vector3 vector = user.unadjustedAimPoint - user.LockedApproximateSpriteCenter;
            Vector3 vector2 = user.specRigidbody.UnitCenter;
            GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.LastOwner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.LastOwner.CurrentGun == null) ? 1.2f : base.LastOwner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-5, 5)), true);
            Projectile component = gameObject.GetComponent<Projectile>();
            {
                component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 50f);
                component.Owner = user;
                component.Shooter = user.specRigidbody;
                component.baseData.speed = 1.4f;
                component.baseData.range = UnityEngine.Random.Range(1f, 4f);
                component.OnDestruction += this.fuckShitUp;
                component.Owner = user;
                component.Shooter = user.specRigidbody;
            }

        }
        private void fuckShitUp(Projectile projectile)
        {
            string header = "";
            string text = "";
            int num3 = UnityEngine.Random.Range(0, 19);
            bool flag3 = num3 == 0;
            if (flag3)
            {
                header = "You got nothing!";
                text = "Get dunked on!";
            }
            bool flag4 = num3 == 1;
            if (flag4)
            {
                AkSoundEngine.PostEvent("Play_OBJ_chaff_blast_01", base.gameObject);
                this.FlashHoldtime = 3f;
                this.FlashFadetime = 4f;
                Pixelator.Instance.FadeToColor(this.FlashFadetime, Color.white, true, this.FlashHoldtime);
                StickyFrictionManager.Instance.RegisterCustomStickyFriction(0.15f, 4f, false, false); this.FlashHoldtime = 3f;
            }
            bool flag5 = num3 == 2;
            if (flag5)
            {
                header = "Your bullets bounce!";
                text = "";
                ApplyStat(base.LastOwner, PlayerStats.StatType.AdditionalShotBounces, 2f, StatModifier.ModifyMethod.ADDITIVE);
            }
            bool flag6 = num3 == 3;
            if (flag6)
            {
                header = "You turn to Glass.";
                text = "";
                ApplyStat(base.LastOwner, PlayerStats.StatType.Damage, 2f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(base.LastOwner, PlayerStats.StatType.Health, -5f, StatModifier.ModifyMethod.ADDITIVE);
            }
            bool flag7 = num3 == 4;
            if (flag7)
            {
                header = "The Game.";
                text = "You just lost it.";
            }
            bool flag9 = num3 == 5;
            if (flag9)
            {
                ApplyStat(base.LastOwner, PlayerStats.StatType.AdditionalItemCapacity, 1f, StatModifier.ModifyMethod.ADDITIVE);
                for (int counter = 0; counter < UnityEngine.Random.Range(1f, 9f); counter++)
                {
                    PickupObject var = Gungeon.Game.Items["bny:ancient_whisper"];
                    LootEngine.GivePrefabToPlayer(var.gameObject, base.LastOwner);
                }
            }
            bool flag10 = num3 == 6;
            if (flag10)
            {
                header = "You fool!";
                text = "";
                ApplyStat(base.LastOwner, PlayerStats.StatType.Curse, 10f, StatModifier.ModifyMethod.ADDITIVE);
            }
            bool flag11 = num3 == 7;
            if (flag11)
            {
                header = "Try again!";
                text = "";
                for (int counter = 0; counter < UnityEngine.Random.Range(2f, 4f); counter++)
                {
                    PickupObject var = Gungeon.Game.Items["bny:d-grenade"];
                    LootEngine.GivePrefabToPlayer(var.gameObject, base.LastOwner);
                }
            }
            bool flag12 = num3 == 8;
            if (flag12)
            {
                header = "mmm yees";
                text = "junk";
                for (int counter = 0; counter < UnityEngine.Random.Range(1f, 7f); counter++)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(127).gameObject, base.LastOwner);
                }
            }
            bool flag13 = num3 == 9;
            if (flag13)
            {
                header = "Unlocks!";
                text = "";
                for (int counter = 0; counter < UnityEngine.Random.Range(1f, 3f); counter++)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.LastOwner);
                }
            }
            bool flag14 = num3 == 10;
            if (flag14)
            {
                GameManager.Instance.LoadCustomLevel("tt_bullethell");
            }
            bool flag15 = num3 == 11;
            if (flag15)
            {
                GameManager.Instance.LoadCustomLevel("tt_castle");
            }
            bool flag16 = num3 == 12;
            if (flag16)
            {
                IntVector2 randomVisibleClearSpot6 = base.LastOwner.CurrentRoom.GetRandomVisibleClearSpot(1, 1);
                GameManager.Instance.RewardManager.SpawnRewardChestAt(randomVisibleClearSpot6, -1f, PickupObject.ItemQuality.EXCLUDED);
            }
            bool flag17 = num3 == 13;
            if (flag17)
            {
                Vector3 position = projectile.sprite.WorldCenter;
                this.Boom(position);
                AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
                GoopDefinition goopDefinition = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
                goopDefinition.baseColor32 = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
                goopDefinition.fireColor32 = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
                goopDefinition.UsesGreenFire = false;
                DeadlyDeadlyGoopManager goopManagerForGoopType = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDefinition);
                goopManagerForGoopType.TimedAddGoopCircle(projectile.sprite.WorldCenter, 10f, 0.1f, false);
                this.Nuke = assetBundle.LoadAsset<GameObject>("assets/data/vfx prefabs/impact vfx/vfx_explosion_nuke.prefab");
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Nuke);
                gameObject2.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(projectile.specRigidbody.UnitCenter, tk2dBaseSprite.Anchor.LowerCenter);
                gameObject2.transform.position = gameObject.transform.position.Quantize(0.0625f);
                gameObject2.GetComponent<tk2dBaseSprite>().UpdateZDepth();
                {
                    this.FlashHoldtime = 0.1f;
                    this.FlashFadetime = 0.5f;
                    Pixelator.Instance.FadeToColor(this.FlashFadetime, Color.white, true, this.FlashHoldtime);
                    StickyFrictionManager.Instance.RegisterCustomStickyFriction(0.15f, 1f, false, false); this.FlashHoldtime = 0.1f;
                }
            }
            bool flag18 = num3 == 14;
            if (flag18)
            {
                header = "Oops! Inflation!";
                text = "";
                ApplyStat(base.LastOwner, PlayerStats.StatType.MoneyMultiplierFromEnemies, 5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(base.LastOwner, PlayerStats.StatType.GlobalPriceMultiplier, 5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            }
            bool flag19 = num3 == 15;
            if (flag19)
            {
                header = "Brrrap Papap Pop!";
                text = "Pop! Brrrrrap!";
                ApplyStat(base.LastOwner, PlayerStats.StatType.ShadowBulletChance, 10f, StatModifier.ModifyMethod.ADDITIVE);
            }
            bool flag20 = num3 == 16;
            if (flag20)
            {
                header = "Money!";
                text = "";
                for (int counter = 0; counter < UnityEngine.Random.Range(24f, 96f); counter++)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, base.LastOwner);
                }
            }
            bool flag21 = num3 == 17;
            if (flag21)
            {
                for (int counter = 0; counter < UnityEngine.Random.Range(1f, 5f); counter++)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(224).gameObject, base.LastOwner);
                }
            }
            bool flag22 = num3 == 18;
            if (flag22)
            {
                header = "You can take your time...";
                text = "";
                ApplyStat(base.LastOwner, PlayerStats.StatType.ProjectileSpeed, .3f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                ApplyStat(base.LastOwner, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, .3f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            }
            bool flag23 = num3 == 19;
            if (flag23)
            {
                LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(137).gameObject, base.LastOwner);
            }
            this.Notify(header, text);
        }
        private void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod modifyMethod)
        {
            player.stats.RecalculateStats(player, false, false);
            StatModifier statModifier = new StatModifier()
            {
                statToBoost = statType,
                amount = amountToApply,
                modifyType = modifyMethod
            };
            player.ownerlessStatModifiers.Add(statModifier);
            player.stats.RecalculateStats(player, false, false);
        }
        private void Notify(string header, string text)
        {
            tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
            int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/DGrenade");
            GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, null, spriteIdByName, UINotificationController.NotificationColor.SILVER, false, true);
        }
        public void Boom(Vector3 position)
        {
            ExplosionData defaultSmallExplosionData = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData.effect;
            this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData.ignoreList;
            this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
        }

        // Token: 0x04000172 RID: 370
        private ExplosionData smallPlayerSafeExplosion = new ExplosionData
        {
            damageRadius = 80f,
            // (:
            damageToPlayer = 2f,
            doDamage = true,
            damage = 1050f,
            doExplosionRing = false,
            doDestroyProjectiles = true,
            doForce = true,
            debrisForce = 100f,
            preventPlayerForce = false,
            explosionDelay = 0.25f,
            usesComprehensiveDelay = false,
            doScreenShake = true,
            playDefaultSFX = false
        };
        private GameObject Nuke;
        public float FlashHoldtime;
        public float FlashFadetime;
    }
}



