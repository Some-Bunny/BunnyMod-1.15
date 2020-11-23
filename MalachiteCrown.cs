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
using DaikonForge.Tween.Components;

namespace BunnyMod
{
    public class MalachiteCrown : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Malachite Thorns";
            string resourceName = "BunnyMod/Resources/malachitecrown";
            GameObject obj = new GameObject(itemName);
            MalachiteCrown crownOfBlood = obj.AddComponent<MalachiteCrown>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Aspect of Corruption";
            string longDesc = "A large pair of mineral thorns that sprouted in the most extreme conditions. They will ensure their survival no matter what.";
            crownOfBlood.SetupItem(shortDesc, longDesc, "bny");
            crownOfBlood.AddPassiveStatModifier(PlayerStats.StatType.MovementSpeed, .8f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            crownOfBlood.AddPassiveStatModifier(PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);
            crownOfBlood.AddPassiveStatModifier(PlayerStats.StatType.DamageToBosses, 1.3f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            crownOfBlood.AddPassiveStatModifier(PlayerStats.StatType.Damage, 1.3f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            crownOfBlood.AddPassiveStatModifier(PlayerStats.StatType.Health, 1f, StatModifier.ModifyMethod.ADDITIVE);
            crownOfBlood.AddPassiveStatModifier(PlayerStats.StatType.KnockbackMultiplier, 1.5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            crownOfBlood.quality = PickupObject.ItemQuality.S;
            crownOfBlood.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            crownOfBlood.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
            List<string> mandatoryConsoleIDs15 = new List<string>
            {
                "bny:malachite_thorns"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "monster_blood",
                "plunger",
                "poxcannon",
                "irradiated_lead",
                "poison_vial",
                "uranium_ammolet"
            };
            CustomSynergies.Add("Champion Type", mandatoryConsoleIDs15, optionalConsoleIDs, true);
        }
        private void OnDealtDamage(PlayerController usingPlayer, float amount, bool fatal, HealthHaver targetr)
        {
            bool flag = !MalachiteCrown.onCooldown;
            bool flag2 = flag;
            if (flag2)
            {
                bool flagA = usingPlayer.PlayerHasActiveSynergy("Champion Type");
                if (flagA)
                {
                    MalachiteCrown.onCooldown = true;
                    GameManager.Instance.StartCoroutine(MalachiteCrown.StartCooldown());
                    float value = UnityEngine.Random.value;
                    bool flag3 = value < 0.25f;
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_mineflayer_bong_01", gameObject);
                        this.CorruptionBombs2(usingPlayer);
                    }
                }
                else
                {
                    MalachiteCrown.onCooldown = true;
                    GameManager.Instance.StartCoroutine(MalachiteCrown.StartCooldown());
                    float value = UnityEngine.Random.value;
                    bool flag3 = value < 0.33f;
                    bool flag4 = flag3;
                    if (flag4)
                    {
                        AkSoundEngine.PostEvent("Play_BOSS_mineflayer_bong_01", gameObject);
                        this.CorruptionBombs(usingPlayer);
                    }
                }
            }
        }
        private static IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(1.5f);
            MalachiteCrown.onCooldown = false;
            yield break;
        }
        
        private void CorruptionBombs2(PlayerController player)
        {
            Projectile projectile = ((Gun)ETGMod.Databases.Items[19]).DefaultModule.projectiles[0];
            GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 270f), true);
            GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 342f), true);
            GameObject gameObject3 = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 54f), true);
            GameObject gameObject4 = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 126f), true);
            GameObject gameObject5 = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 198f), true);
            Projectile component = gameObject.GetComponent<Projectile>();
            Projectile component2 = gameObject2.GetComponent<Projectile>();
            Projectile component3 = gameObject3.GetComponent<Projectile>();
            Projectile component4 = gameObject4.GetComponent<Projectile>();
            Projectile component5 = gameObject5.GetComponent<Projectile>();
            bool flag = component != null;
            bool flag2 = flag;
            if (flag2)
            {
                component.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component.Shooter = base.Owner.specRigidbody;
                component.baseData.speed = 1.7f;
                component.baseData.range = 7f;
                component.OnDestruction += this.HellaPosion;
            }
            bool flag3 = component2 != null;
            bool flag4 = flag3;
            if (flag4)
            {
                component2.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component2.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component2.Shooter = base.Owner.specRigidbody;
                component.baseData.speed = 1.7f;
                component2.baseData.range = 7f;
                component2.OnDestruction += this.HellaPosion;
            }
            bool flag5 = component3 != null;
            bool flag6 = flag5;
            if (flag6)
            {
                component3.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component3.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component3.Shooter = base.Owner.specRigidbody;
                component.baseData.speed = 1.7f;
                component3.baseData.range = 7f;
                component3.OnDestruction += this.HellaPosion;
            }
            bool aaa = component4 != null;
            bool aa = aaa;
            if (aa)
            {
                component4.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component4.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component4.Shooter = base.Owner.specRigidbody;
                component.baseData.speed = 1.7f;
                component4.baseData.range = 7f;
                component4.OnDestruction += this.HellaPosion;
            }
            bool youre = component5 != null;
            bool moom = youre;
            if (moom)
            {
                component5.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component5.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component5.Shooter = base.Owner.specRigidbody;
                component.baseData.speed = 1.7f;
                component5.baseData.range = 7f;
                component5.OnDestruction += this.HellaPosion;
            }
        }
        private void CorruptionBombs(PlayerController player)
        {
            Projectile projectile = ((Gun)ETGMod.Databases.Items[19]).DefaultModule.projectiles[0];
            GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 330f), true);
            GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 90f), true);
            GameObject gameObject3 = SpawnManager.SpawnProjectile(projectile.gameObject, base.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (base.Owner.CurrentGun == null) ? 0f : 210f), true);
            Projectile component = gameObject.GetComponent<Projectile>();
            Projectile component2 = gameObject2.GetComponent<Projectile>();
            Projectile component3 = gameObject3.GetComponent<Projectile>();
            bool flag = component != null;
            bool flag2 = flag;
            if (flag2)
            {
                component.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component.Shooter = base.Owner.specRigidbody;
                component.baseData.speed = 1f;
                component.baseData.range = 5f;
                component.OnDestruction += this.HellaPosion;
            }
            bool flag3 = component2 != null;
            bool flag4 = flag3;
            if (flag4)
            {
                component2.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component2.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component2.Shooter = base.Owner.specRigidbody;
                component2.baseData.speed = 1f;
                component2.baseData.range = 5f;
                component2.OnDestruction += this.HellaPosion;
            }
            bool flag5 = component3 != null;
            bool flag6 = flag5;
            if (flag6)
            {
                component3.Owner = base.Owner;
                PierceProjModifier pierceProjModifier = component3.gameObject.AddComponent<PierceProjModifier>();
                pierceProjModifier.penetration = 10;
                component3.Shooter = base.Owner.specRigidbody;
                component3.baseData.speed = 1f;
                component3.baseData.range = 5f;
                component3.OnDestruction += this.HellaPosion;
            }
        }
        private void HellaPosion(Projectile arg1)
        {
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            GoopDefinition goopDef = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/poison goop.asset");
            DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDef).TimedAddGoopCircle(arg1.sprite.WorldBottomCenter, 4f, 1f, false);
        }


        public override void Pickup(PlayerController player)
        {
            this.EnableVFX(player);
            AkSoundEngine.PostEvent("Play_BOSS_mineflayer_bellshot_01", gameObject);
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnDealtDamageContext += this.OnDealtDamage;
            this.m_PoisonImmunity = new DamageTypeModifier();
            this.m_PoisonImmunity.damageMultiplier = 0f;
            this.m_PoisonImmunity.damageType = CoreDamageTypes.Poison;
            player.healthHaver.damageTypeModifiers.Add(this.m_PoisonImmunity);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnDealtDamageContext -= this.OnDealtDamage;
            this.DisableVFX(player);
            return base.Drop(player);
        }
        private void EnableVFX(PlayerController user)
        {
            Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outlineMaterial.SetColor("_OverrideColor", new Color(0f, 150f, 0f));
        }

        // Token: 0x060005D4 RID: 1492 RVA: 0x00032B38 File Offset: 0x00030D38
        private void DisableVFX(PlayerController user)
        {
            Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outlineMaterial.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
        }
        private static bool onCooldown;
        private DamageTypeModifier m_PoisonImmunity;

    }
}