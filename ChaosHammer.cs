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
    public class ChaosHammer : PlayerItem
    {

        public PlayerStats.StatType Damage { get; private set; }

        public static void Init()
        {
            string itemName = "Chaos Hammer";
            string resourceName = "BunnyMod/Resources/chaoshammer.png";
            GameObject obj = new GameObject(itemName);
            ChaosHammer chaosChamber = obj.AddComponent<ChaosHammer>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Chaotic Kick-Back";
            string longDesc = "The missing Hammer of the Chaos Revolver. Each and every strike of the hammer is powerful and should not be underestimated.";
            chaosChamber.SetupItem(shortDesc, longDesc, "bny");
            chaosChamber.AddPassiveStatModifier(PlayerStats.StatType.RateOfFire, .30f, StatModifier.ModifyMethod.ADDITIVE);
            chaosChamber.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, 1.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            chaosChamber.quality = PickupObject.ItemQuality.B;
            ItemBuilder.SetCooldownType(chaosChamber, ItemBuilder.CooldownType.Timed, 12f);
            chaosChamber.consumable = false;
            chaosChamber.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            //player.OnReloadedGun += (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.somethingrelatedtojammed));
            base.Pickup(player);
        }


        protected override void DoEffect(PlayerController user)
        {
            PlayerController playerController = base.LastOwner as PlayerController;
            playerController.StartCoroutine(this.HandleDash(playerController));
        }
        public IEnumerator HandleDash(PlayerController target)
		{
            StartCoroutine(HandleShield(target));
            AkSoundEngine.PostEvent("Play_WPN_woodbeam_impact_01", base.gameObject);
           // AkSoundEngine.PostEvent("Play_BOSS_wall_slam_01", base.gameObject);
            //BunnyModule.Log("snot bun");
            target.SetIsFlying(true, "chaos", true, false);
            float duration = 0.025f;
            float elapsed = -BraveTime.DeltaTime;
            float angle = target.CurrentGun.CurrentAngle;
            float adjSpeed = 250f;
           // target.ReceivesTouchDamage = false;
            while (elapsed < duration)
            {
                elapsed += BraveTime.DeltaTime;
                target.specRigidbody.Velocity = BraveMathCollege.DegreesToVector(angle, 1f).normalized * adjSpeed;
                yield return null;
            }
            target.SetIsFlying(false, "chaos", true, false);
            //target.ReceivesTouchDamage = true;
            this.DoMicroBlank(target.sprite.WorldCenter, 0f);
            yield break;
        }
        private void DoMicroBlank(Vector2 center, float knockbackForce = 30f)
        {
            PlayerController owner = base.LastOwner;
            GameObject silencerVFX = (GameObject)ResourceCache.Acquire("Global VFX/BlankVFX_Ghost");
            AkSoundEngine.PostEvent("Play_OBJ_silenceblank_small_01", base.gameObject);
            GameObject gameObject = new GameObject("silencer");
            SilencerInstance silencerInstance = gameObject.AddComponent<SilencerInstance>();
            float additionalTimeAtMaxRadius = 0.25f;
            silencerInstance.TriggerSilencer(center, 25f, 5f, silencerVFX, 0f, 3f, 3f, 3f, 250f, 5f, additionalTimeAtMaxRadius, owner, false, false);
        }
        float m_invactiveDuration = 0.2f;
        float duration = 0.2f;
        private IEnumerator HandleShield(PlayerController user)
        {
            //IsCurrentlyActive = true;
            //float m_activeElapsed = 0f;
            m_invactiveDuration = this.duration;
            SpeculativeRigidbody specRigidbody = user.specRigidbody;
            user.healthHaver.IsVulnerable = false;
            float elapsed = 0f;
            while (elapsed < this.duration)
            {
                elapsed += BraveTime.DeltaTime;
                user.healthHaver.IsVulnerable = false;
                yield return null;
            }
            if (user)
            {
                user.healthHaver.IsVulnerable = true;
                user.ClearOverrideShader();
                SpeculativeRigidbody specRigidbody2 = user.specRigidbody;
                //IsCurrentlyActive = false;
            }
            if (this)
            {
                //AkSoundEngine.PostEvent("Play_OBJ_metalskin_end_01", base.gameObject);
            }
            PlayerController player = base.LastOwner as PlayerController;
            bool flagA = player.PlayerHasActiveSynergy("Reunion");
            if (flagA)
            {
                this.DoMicroBlank(user.sprite.WorldCenter, 0f);
            }
            yield break;
        }
        public AIActor AIActor;
        public PlayerController player;
        public Gun gun;
    }
}



