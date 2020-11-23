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


//T1 Modules
namespace BunnyMod
{
    public class ModuleDamage : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Damage Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/weaponmodulardamage.png";
            GameObject obj = new GameObject(itemName);
            ModuleDamage Module = obj.AddComponent<ModuleDamage>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Damage Up.";
            string longDesc = "Increases damage by 25%";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Damage, .25f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class ModuleClipSize : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Clip Size Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/weaponmodularclipsize.png";
            GameObject obj = new GameObject(itemName);
            ModuleClipSize Module = obj.AddComponent<ModuleClipSize>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Clip Size Up.";
            string longDesc = "Increases clip size by 25%";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.AdditionalClipCapacityMultiplier, .25f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class ModuleFireRate : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Fire Rate Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/weaponmodularfirerate.png";
            GameObject obj = new GameObject(itemName);
            ModuleFireRate Module = obj.AddComponent<ModuleFireRate>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Fire Rate Up.";
            string longDesc = "Increases fire rate by 30%";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.RateOfFire, .30f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class ModuleReload : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Reload Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/weaponmodularreload.png";
            GameObject obj = new GameObject(itemName);
            ModuleReload Module = obj.AddComponent<ModuleReload>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Reload Time Down.";
            string longDesc = "Reduces reload time by 20%";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.ReloadSpeed, .80f, StatModifier.ModifyMethod.MULTIPLICATIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T2ModuleYV : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Splitter Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t2weaponmodularyv.png";
            GameObject obj = new GameObject(itemName);
            T2ModuleYV Module = obj.AddComponent<T2ModuleYV>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Chance for extra.";
            string longDesc = "Gives a chance to fire extra bullets.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.ShadowBulletChance, 10f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}

//T2 Modules
namespace BunnyMod
{
    public class T2ModulePierce : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Piercer Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t2weaponmodularpierce.png";
            GameObject obj = new GameObject(itemName);
            T2ModulePierce Module = obj.AddComponent<T2ModulePierce>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Piercing.";
            string longDesc = "Makes projectiles pierce.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.AdditionalShotPiercing, 1f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T2ModuleBounce : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Bouncer Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t2weaponmodularbounce.png";
            GameObject obj = new GameObject(itemName);
            T2ModuleBounce Module = obj.AddComponent<T2ModuleBounce>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Bouncing.";
            string longDesc = "Makes projectiles bounce.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.AdditionalShotBounces, 1f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T2ModuleEjector : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Ejector Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t2weaponmodularejector.png";
            GameObject obj = new GameObject(itemName);
            T2ModuleEjector Module = obj.AddComponent<T2ModuleEjector>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Discharge.";
            string longDesc = "Shoots a burst of bullets on reload.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
        }
        private void HandleGunReloaded(PlayerController player, Gun playerGun)
        {
            RadialBurstInterface radialBurstSettings = this.RadialBurstSettings;
            bool flag = playerGun.ClipShotsRemaining == 0;
            if (flag)
            {
                this.RadialBurstSettings.DoBurst(player, null, null);
            }
        }
        public override void Pickup(PlayerController player)
        {
            player.OnReloadedGun += (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnReloadedGun -= (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
            return base.Drop(player);
        }
        public RadialBurstInterface RadialBurstSettings;
    }
}
namespace BunnyMod
{
    public class T2ModuleCloak : PassiveItem
    {
        private float random;

        public static void Init()
        {
            string itemName = "Cloak Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t2weaponmodularcloak.png";
            GameObject obj = new GameObject(itemName);
            T2ModuleCloak Module = obj.AddComponent<T2ModuleCloak>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Into Darkness.";
            string longDesc = "Chance to enter stealth on enemy kill.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnKilledEnemy += this.StealthEffect;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        private void StealthEffect(PlayerController player)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.25f)
            {
                PlayerController owner = base.Owner;
                this.BreakStealth(owner);
                owner.OnItemStolen += this.BreakStealthOnSteal;
                owner.ChangeSpecialShaderFlag(1, 1f);
                owner.healthHaver.OnDamaged += this.OnDamaged;
                owner.SetIsStealthed(true, "table");
                owner.SetCapableOfStealing(true, "table", null);
                GameManager.Instance.StartCoroutine(this.Unstealthy());
            }
        }

        // Token: 0x060005D6 RID: 1494 RVA: 0x00038919 File Offset: 0x00036B19
        private IEnumerator Unstealthy()
        {
            PlayerController player = base.Owner;
            yield return new WaitForSeconds(0.15f);
            player.OnDidUnstealthyAction += this.BreakStealth;
            yield break;
        }

        // Token: 0x060005D7 RID: 1495 RVA: 0x00038928 File Offset: 0x00036B28
        private void BreakStealth(PlayerController player)
        {
            player.ChangeSpecialShaderFlag(1, 0f);
            player.OnItemStolen -= this.BreakStealthOnSteal;
            player.SetIsStealthed(false, "table");
            player.healthHaver.OnDamaged -= this.OnDamaged;
            player.SetCapableOfStealing(false, "table", null);
            player.OnDidUnstealthyAction -= this.BreakStealth;
            AkSoundEngine.PostEvent("Play_ENM_wizardred_appear_01", base.gameObject);
        }
        private void OnDamaged(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
        {
            PlayerController owner = base.Owner;
            this.BreakStealth(owner);
        }

        // Token: 0x060005D9 RID: 1497 RVA: 0x000389D5 File Offset: 0x00036BD5
        private void BreakStealthOnSteal(PlayerController arg1, ShopItemController arg2)
        {
            this.BreakStealth(arg1);
        }

        // Token: 0x060005DA RID: 1498 RVA: 0x000389E0 File Offset: 0x00036BE0
        protected override void OnDestroy()
        {
            PlayerController owner = base.Owner;
            bool flag = base.Owner;
            if (flag)
            {
                owner.OnReceivedDamage -= this.StealthEffect;
            }
            base.OnDestroy();
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T2ModuleHoming : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Homing Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t2weaponmodularhoming.png";
            GameObject obj = new GameObject(itemName);
            T2ModuleHoming Module = obj.AddComponent<T2ModuleHoming>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Tracking shot.";
            string longDesc = "Bullets will home in one enemies.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
        }
        public override void Pickup(PlayerController player)
        {
            this.homingRadius = 4f;
            this.homingAngularVelocity = 200f;
            this.CanBeDropped = false;
            player.PostProcessProjectile += this.PostProcessProjectile;
            player.PostProcessBeamChanceTick += this.PostProcessBeamChanceTick;
            base.Pickup(player);
        }
        private void PostProcessProjectile(Projectile obj, float effectChanceScalar)
        {
            if (UnityEngine.Random.value > this.ActivationChance * effectChanceScalar)
            {
                {
                    obj.baseData.damage *= this.SynergyDamageMultiplier;
                    obj.RuntimeUpdateScale(this.SynergyDamageMultiplier);
                }
                return;
            }
            HomingModifier homingModifier = obj.gameObject.GetComponent<HomingModifier>();
            if (homingModifier == null)
            {
                homingModifier = obj.gameObject.AddComponent<HomingModifier>();
                homingModifier.HomingRadius = 0f;
                homingModifier.AngularVelocity = 0f;
            }
            homingModifier.HomingRadius += this.homingRadius;
            homingModifier.AngularVelocity += this.homingAngularVelocity;
        }
        private void PostProcessBeamChanceTick(BeamController beam)
        {
            if (UnityEngine.Random.value > this.ActivationChance)
            {
                return;
            }
            beam.ChanceBasedHomingRadius += this.homingRadius;
            beam.ChanceBasedHomingAngularVelocity += this.homingAngularVelocity;
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
        public float ActivationChance = 1f;

        // Token: 0x040076B8 RID: 30392
        public float homingRadius = 5f;

        // Token: 0x040076B9 RID: 30393
        public float homingAngularVelocity = 360f;

        public bool SynergyIncreasesDamageIfNotActive;


        // Token: 0x040076BC RID: 30396
        public float SynergyDamageMultiplier = 1.5f;
        protected PlayerController m_player;

    }
}

//T3 Modules
namespace BunnyMod
{
    public class T3ModuleColossus : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Colossus Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t3weaponmodularmassiveslowdps.png";
            GameObject obj = new GameObject(itemName);
            T3ModuleColossus Module = obj.AddComponent<T3ModuleColossus>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Massive.";
            string longDesc = "Increases damage and size but decreases speed.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Damage, .25f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.PlayerBulletScale, 1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.ProjectileSpeed, .75f, StatModifier.ModifyMethod.MULTIPLICATIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T3ModuleRocket : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Rocket Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t3weaponmodularrocket.png";
            GameObject obj = new GameObject(itemName);
            T3ModuleRocket Module = obj.AddComponent<T3ModuleRocket>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Velocity.";
            string longDesc = "Increases bullet speed and knockback. Slightly increases fire rate.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.KnockbackMultiplier, 3f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.ProjectileSpeed, 3f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.RateOfFire, .25f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T3ModuleInaccurate : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Inaccurate Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t3weaponmodularinaccuratebutdps.png";
            GameObject obj = new GameObject(itemName);
            T3ModuleInaccurate Module = obj.AddComponent<T3ModuleInaccurate>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Tradeoff.";
            string longDesc = "Greatly increases damage. Decreases accuracy.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Damage, .75f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.DamageToBosses, .25f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Accuracy, 2.5f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T3ModuleOverload: PassiveItem
    {
        public static void Init()
        {
            string itemName = "Overloader Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t3weaponmodularoverloader.png";
            GameObject obj = new GameObject(itemName);
            T3ModuleOverload Module = obj.AddComponent<T3ModuleOverload>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Shocker.";
            string longDesc = "On reload blasts ricochecting lightning in random directions.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
        }
        private void HandleGunReloaded(PlayerController player, Gun playerGun)
        {
            bool flag = playerGun.ClipShotsRemaining == 0;
            if (flag)
            {
                AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
                for (int counter = 0; counter < 3; counter++)
                {
                    Projectile projectile = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
                    Vector3 vector = player.unadjustedAimPoint - player.LockedApproximateSpriteCenter;
                    Vector3 vector2 = player.specRigidbody.UnitCenter;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, ((base.Owner.CurrentGun == null) ? 1.2f : base.Owner.CurrentGun.CurrentAngle) + UnityEngine.Random.Range(-180, 180)), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    BounceProjModifier bouncy = component.gameObject.AddComponent<BounceProjModifier>();
                    bouncy.numberOfBounces = 2;
                    if (flag)
                    {
                        component.Owner = player;
                        component.Shooter = player.specRigidbody;
                    }
                }
            }
        }

        public override void Pickup(PlayerController player)
        {
            player.OnReloadedGun += (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnReloadedGun -= (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.HandleGunReloaded));
            return base.Drop(player);
        }
    }
}
namespace BunnyMod
{
    public class T3ModuleReactive : PassiveItem
    {
        //this is an incredibly lazy module even by my standards
        public static void Init()
        {
            string itemName = "Reactive Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/t3weaponmodularBOOM.png";
            GameObject obj = new GameObject(itemName);
            T3ModuleReactive Module = obj.AddComponent<T3ModuleReactive>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Explosive.";
            string longDesc = "Dead enemies explode.";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag = enemyHealth.aiActor && fatal;
                if (flag)
                {
                    this.Boom(enemyHealth.sprite.WorldCenter);
                }
            }

        }
        public void Boom(Vector3 position)
        {
            ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
            this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
            this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
        }


        private ExplosionData smallPlayerSafeExplosion = new ExplosionData
        {
            damageRadius = 4f,
            damageToPlayer = 0f,
            doDamage = true,
            damage = 15f,
            doExplosionRing = true,
            doDestroyProjectiles = false,
            doForce = true,
            debrisForce = 6f,
            preventPlayerForce = true,
            explosionDelay = 0.1f,
            usesComprehensiveDelay = false,
            doScreenShake = false,
            playDefaultSFX = true,
        };


        public override void Pickup(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            return base.Drop(player);
        }
    }
}


//Corrupt Modules
namespace BunnyMod
{
    public class CorruptModuleSensor : PassiveItem
    {
        private float random;

        public static void Init()
        {
            string itemName = "Corrupt Sensor Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/corruptmodulesensorfailure.png";
            GameObject obj = new GameObject(itemName);
            CorruptModuleSensor Module = obj.AddComponent<CorruptModuleSensor>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "ERROR: SENSOR FAILURE.";
            string longDesc = "> \n\n>DETECTED FAILURE IN PROJECTILE SENSOR\n\n>FAILURE TO DETECT ENEMY PROJECTILE SPEED\n\n>SEEK CAUTION UNTIL RESTORATION COMPLETE";
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, .35f, StatModifier.ModifyMethod.ADDITIVE);
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        private void HandleRoomCleared(PlayerController obj)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.13f)
            {
                AkSoundEngine.PostEvent("Play_BOSS_agunim_deflect_01", gameObject);
                this.DecorruptModule();
            }
        }

        private void DecorruptModule()
        {
            {
                int num3 = UnityEngine.Random.Range(0, 5);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Damage Module"].gameObject, base.Owner, true);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Clip Size Module"].gameObject, base.Owner, true);
                }
                bool flag5 = num3 == 2;
                if (flag5)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Reload Module"].gameObject, base.Owner, true);
                }
                bool flag6 = num3 == 3;
                if (flag6)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fire Rate Module"].gameObject, base.Owner, true);
                }
                bool flag7 = num3 == 4;
                if (flag7)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Splitter Module"].gameObject, base.Owner, true);
                }
            }
            base.Owner.DropPassiveItem(this);
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            AkSoundEngine.PostEvent("Play_BOSS_agunim_ribbons_01", gameObject);
            player.OnRoomClearEvent += this.HandleRoomCleared;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnRoomClearEvent -= this.HandleRoomCleared;
            DebrisObject debrisObject = base.Drop(player);
            CorruptModuleSensor component = debrisObject.GetComponent<CorruptModuleSensor>();
            component.Break();
            return debrisObject;
        }
    }
}
namespace BunnyMod
{
    public class CorruptModuleAccuracy : PassiveItem
    {
        private float random;

        public static void Init()
        {
            string itemName = "Corrupt Accuracy Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/corruptmoduleaccuracyfailure.png";
            GameObject obj = new GameObject(itemName);
            CorruptModuleAccuracy Module = obj.AddComponent<CorruptModuleAccuracy>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "ERROR: AIM FAILURE.";
            string longDesc = "> \n\n>DETECTED FAILURE IN ACCURACY MODULE\n\n>FAILURE TO CALIBRATE WEAPON\n\n>SEEK CAUTION UNTIL RESTORATION COMPLETE";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Accuracy, 3.5f, StatModifier.ModifyMethod.ADDITIVE);
        }
        private void HandleRoomCleared(PlayerController obj)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.13f)
            {
                AkSoundEngine.PostEvent("Play_BOSS_agunim_deflect_01", gameObject);
                this.DecorruptModule();
            }
        }
        private void DecorruptModule()
        {
            {
                int num3 = UnityEngine.Random.Range(0, 5);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Damage Module"].gameObject, base.Owner, true);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Clip Size Module"].gameObject, base.Owner, true);
                }
                bool flag5 = num3 == 2;
                if (flag5)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Reload Module"].gameObject, base.Owner, true);
                }
                bool flag6 = num3 == 3;
                if (flag6)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fire Rate Module"].gameObject, base.Owner, true);
                }
                bool flag7 = num3 == 4;
                if (flag7)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Splitter Module"].gameObject, base.Owner, true);
                }
            }
            base.Owner.DropPassiveItem(this);
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }
        public override void Pickup(PlayerController player)
        {
            AkSoundEngine.PostEvent("Play_BOSS_agunim_ribbons_01", gameObject);
            player.OnRoomClearEvent += this.HandleRoomCleared;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnRoomClearEvent -= this.HandleRoomCleared;
            DebrisObject debrisObject = base.Drop(player);
            CorruptModuleAccuracy component = debrisObject.GetComponent<CorruptModuleAccuracy>();
            component.Break();
            return debrisObject;
        }
    }
}
namespace BunnyMod
{
    public class CorruptModuleCoolant : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Corrupt Cooling Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/corruptmodulecoolingfailure.png";
            GameObject obj = new GameObject(itemName);
            CorruptModuleCoolant Module = obj.AddComponent<CorruptModuleCoolant>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "ERROR: COOLING FAILURE.";
            string longDesc = "> \n\n>DETECTED CORRUPT COOLING MODULE\n\n>FAILURE IN COOLING UNITS\n\n>SEEK CAUTION UNTIL RESTORATION COMPLETE";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Coolness, -10f, StatModifier.ModifyMethod.ADDITIVE);
        }
        private void HandleRoomCleared(PlayerController obj)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.13f)
            {
                AkSoundEngine.PostEvent("Play_BOSS_agunim_deflect_01", gameObject);
                this.DecorruptModule();
            }
        }
        private void DecorruptModule()
        {
            {
                int num3 = UnityEngine.Random.Range(0, 5);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Damage Module"].gameObject, base.Owner, true);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Clip Size Module"].gameObject, base.Owner, true);
                }
                bool flag5 = num3 == 2;
                if (flag5)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Reload Module"].gameObject, base.Owner, true);
                }
                bool flag6 = num3 == 3;
                if (flag6)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fire Rate Module"].gameObject, base.Owner, true);
                }
                bool flag7 = num3 == 4;
                if (flag7)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Splitter Module"].gameObject, base.Owner, true);
                }
            }
            base.Owner.DropPassiveItem(this);
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            AkSoundEngine.PostEvent("Play_BOSS_agunim_ribbons_01", gameObject);
            player.OnRoomClearEvent += this.HandleRoomCleared;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnRoomClearEvent -= this.HandleRoomCleared;
            DebrisObject debrisObject = base.Drop(player);
            CorruptModuleCoolant component = debrisObject.GetComponent<CorruptModuleCoolant>();
            component.Break();
            return debrisObject;
        }
        private float random;
    }
}
namespace BunnyMod
{
    public class CorruptModuleLoose: PassiveItem
    {
        public static void Init()
        {
            string itemName = "Corrupt Fitting Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/corruptmoduleloosefit.png";
            GameObject obj = new GameObject(itemName);
            CorruptModuleLoose Module = obj.AddComponent<CorruptModuleLoose>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "ERROR: STORAGE FAILURE.";
            string longDesc = "> \n\n>DETECTED CORRUPT SYSTEM MODULE\n\n>FAILURE IN STORAGE UNITS\n\n>SEEK CAUTION UNTIL RESTORATION COMPLETE";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.AdditionalClipCapacityMultiplier, .5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.ReloadSpeed, 1.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
        }
        private void HandleRoomCleared(PlayerController obj)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.13f)
            {
                AkSoundEngine.PostEvent("Play_BOSS_agunim_deflect_01", gameObject);
                this.DecorruptModule();
            }
        }
        private void DecorruptModule()
        {
            {
                int num3 = UnityEngine.Random.Range(0, 5);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Damage Module"].gameObject, base.Owner, true);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Clip Size Module"].gameObject, base.Owner, true);
                }
                bool flag5 = num3 == 2;
                if (flag5)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Reload Module"].gameObject, base.Owner, true);
                }
                bool flag6 = num3 == 3;
                if (flag6)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fire Rate Module"].gameObject, base.Owner, true);
                }
                bool flag7 = num3 == 4;
                if (flag7)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Splitter Module"].gameObject, base.Owner, true);
                }
            }
            base.Owner.DropPassiveItem(this);
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            AkSoundEngine.PostEvent("Play_BOSS_agunim_ribbons_01", gameObject);
            player.OnRoomClearEvent += this.HandleRoomCleared;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnRoomClearEvent -= this.HandleRoomCleared;
            DebrisObject debrisObject = base.Drop(player);
            CorruptModuleLoose component = debrisObject.GetComponent<CorruptModuleLoose>();
            component.Break();
            return debrisObject;
        }
        private float random;
    }
}
namespace BunnyMod
{
    public class CorruptModuleDamage : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Corrupt Damage Module";
            string resourceName = "BunnyMod/Resources/WeaponModules/corruptmoduledamage.png";
            GameObject obj = new GameObject(itemName);
            CorruptModuleDamage Module = obj.AddComponent<CorruptModuleDamage>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "ERROR: ATTACK FAILURE.";
            string longDesc = "> \n\n>DETECTED CORRUPT ATTACK MODULE\n\n>FAILURE IN WEAPON FIRING SYSTEM\n\n>SEEK CAUTION UNTIL RESTORATION COMPLETE";
            Module.SetupItem(shortDesc, longDesc, "bny");
            Module.quality = PickupObject.ItemQuality.EXCLUDED;
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.Damage, .8f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(Module, PlayerStats.StatType.ProjectileSpeed, .8f, StatModifier.ModifyMethod.MULTIPLICATIVE);
        }
        private void HandleRoomCleared(PlayerController obj)
        {
            this.random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= 0.13f)
            {
                AkSoundEngine.PostEvent("Play_BOSS_agunim_deflect_01", gameObject);
                this.DecorruptModule();
            }
        }
        private void DecorruptModule()
        {
            {
                int num3 = UnityEngine.Random.Range(0, 5);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Damage Module"].gameObject, base.Owner, true);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Clip Size Module"].gameObject, base.Owner, true);
                }
                bool flag5 = num3 == 2;
                if (flag5)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Reload Module"].gameObject, base.Owner, true);
                }
                bool flag6 = num3 == 3;
                if (flag6)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Fire Rate Module"].gameObject, base.Owner, true);
                }
                bool flag7 = num3 == 4;
                if (flag7)
                {
                    LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Splitter Module"].gameObject, base.Owner, true);
                }
            }
            base.Owner.DropPassiveItem(this);
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            AkSoundEngine.PostEvent("Play_BOSS_agunim_ribbons_01", gameObject);
            player.OnRoomClearEvent += this.HandleRoomCleared;
            this.CanBeDropped = false;
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnRoomClearEvent -= this.HandleRoomCleared;
            DebrisObject debrisObject = base.Drop(player);
            CorruptModuleDamage component = debrisObject.GetComponent<CorruptModuleDamage>();
            component.Break();
            return debrisObject;
        }
        private float random;
    }
}

