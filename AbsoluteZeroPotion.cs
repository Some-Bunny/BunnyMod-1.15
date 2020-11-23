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
    // Token: 0x02000016 RID: 22
    internal class AbsoluteZeroPotion : PlayerItem
    {
        // Token: 0x06000085 RID: 133 RVA: 0x00005A10 File Offset: 0x00003C10
        public static void Init()
        {
            string name = "Absolute-Zero Drink";
            string resourcePath = "BunnyMod/Resources/absolutezeropotion";
            GameObject gameObject = new GameObject(name);
            AbsoluteZeroPotion absoluteZeroPotion = gameObject.AddComponent<AbsoluteZeroPotion>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, gameObject);
            string shortDesc = "Incredibly cold.";
            string longDesc = "This bottle of drinkable coolant was made for surviving in extremely hot enviroments. People who have drank it describe it as 'being incredibly cool'.";
            absoluteZeroPotion.SetupItem(shortDesc, longDesc, "bny");
            absoluteZeroPotion.SetCooldownType(ItemBuilder.CooldownType.Damage, 1800f);
            absoluteZeroPotion.consumable = false;
            absoluteZeroPotion.quality = PickupObject.ItemQuality.B;
        }

        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_OBJ_power_up_01", base.gameObject);
            this.StartEffect(user);
            base.StartCoroutine(ItemBuilder.HandleDuration(this, this.duration, user, new Action<PlayerController>(this.EndEffect)));
        }

        // Token: 0x06000087 RID: 135 RVA: 0x00005AB8 File Offset: 0x00003CB8
        private void StartEffect(PlayerController user)
        {
            float amount = 25f;
            this.cool = this.AddPassiveStatModifier(PlayerStats.StatType.Coolness, amount, StatModifier.ModifyMethod.ADDITIVE);
            user.stats.RecalculateStats(user, true, true);
        }

        // Token: 0x06000088 RID: 136 RVA: 0x00005AEC File Offset: 0x00003CEC
        private void EndEffect(PlayerController user)
        {
            bool flag = this.cool == null;
            if (!flag)
            {
                this.RemovePassiveStatModifier(this.cool);
                user.stats.RecalculateStats(user, true, true);
            }
        }

        // Token: 0x06000089 RID: 137 RVA: 0x00005B25 File Offset: 0x00003D25
        protected override void OnPreDrop(PlayerController user)
        {
            base.OnPreDrop(user);
            this.EndEffect(user);
        }

        // Token: 0x0600008A RID: 138 RVA: 0x00005B38 File Offset: 0x00003D38
        public override bool CanBeUsed(PlayerController user)
        {
            return base.CanBeUsed(user);
        }

        private float duration = 30f;

        private StatModifier cool;
    }
}

//speen