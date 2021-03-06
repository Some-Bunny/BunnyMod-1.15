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
    public class DeathMark : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Death Mark";
            string resourceName = "BunnyMod/Resources/death_mark";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<DeathMark>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "You're out of time.";
            string longDesc = "The curse has consumed you. Do not expect to live.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            item.quality = PickupObject.ItemQuality.SPECIAL;
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, 0.5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, .1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.MovementSpeed, .75f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DamageToBosses, .5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Health, -20f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 3f, StatModifier.ModifyMethod.ADDITIVE);
        }
        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
        }
    }
}

namespace BunnyMod
{
    public class DamocleanClip : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Damoclean Clip";
            string resourceName = "BunnyMod/Resources/damoclean_clip";
            GameObject obj = new GameObject(itemName);
            DamocleanClip damocleanClip = obj.AddComponent<DamocleanClip>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Impending doom lingers...";
            string longDesc = "A cursed ammo clip, filled with old clockworks. It seems that there are entryways for blood to enter into the clockworks, and that it's stuck to you.";
            damocleanClip.SetupItem(shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(damocleanClip, PlayerStats.StatType.Damage, 1.35f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(damocleanClip, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, -.1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(damocleanClip, PlayerStats.StatType.DamageToBosses, 1.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(damocleanClip, PlayerStats.StatType.ReloadSpeed, .5f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            ItemBuilder.AddPassiveStatModifier(damocleanClip, PlayerStats.StatType.Curse, 1f, StatModifier.ModifyMethod.ADDITIVE);
            damocleanClip.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            damocleanClip.quality = PickupObject.ItemQuality.D;
        }

        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            player.healthHaver.OnDamaged += new HealthHaver.OnDamagedEvent(this.TimeIsTicking);
            base.Pickup(player);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            AkSoundEngine.PostEvent("Play_ENM_darken_world_01", gameObject);
            DebrisObject debrisObject = base.Drop(player);
            DamocleanClip component = debrisObject.GetComponent<DamocleanClip>();
            player.healthHaver.OnDamaged -= new HealthHaver.OnDamagedEvent(this.TimeIsTicking);
            component.m_pickedUpThisRun = true;
            component.Break();
            return debrisObject;
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }
        private void TimeIsTicking(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
        {
            this.Countdown += 1f;
            bool flag = this.Countdown == 1f;
            if (flag)
            {
                string header = "";
                string text = "";
                header = "The blood flows in...";
                text = "The cogs start turning.";
                this.Notify(header, text);
                GameManager.Instance.StartCoroutine(TheCountdown());
            }
        }
        private IEnumerator TheCountdown()
        {
            yield return new WaitForSeconds(900f);
            {
                LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Death Mark"].gameObject, base.Owner, true);
                base.Owner.DropPassiveItem(this);
            }
            yield break;
        }
        private void Notify(string header, string text)
        {
            tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
            int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/death_mark");
            GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, null, spriteIdByName, UINotificationController.NotificationColor.SILVER, false, true);
        }
        private float Countdown;
    }
}