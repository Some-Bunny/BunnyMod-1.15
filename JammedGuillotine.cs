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
    public class JammedGuillotine : PlayerItem
    {

        public static void Init()
        {
            string itemName = "Jammed Guillotine";
            string resourceName = "BunnyMod/Resources/jammedguillotine.png";
            GameObject obj = new GameObject(itemName);
            JammedGuillotine glassSyringe = obj.AddComponent<JammedGuillotine>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "The Executioner";
            string longDesc = "Those weakened in combat suffer consequences.\n\nAn ancient guillotine used by gundead to perform ritualistic executions. Though obsolete, the souls of those wronged by it still empower it to seek revenge.";
            ItemBuilder.SetupItem(glassSyringe, shortDesc, longDesc, "bny");
            ItemBuilder.SetCooldownType(glassSyringe, ItemBuilder.CooldownType.PerRoom, 1);
            glassSyringe.AddPassiveStatModifier(PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);
            glassSyringe.consumable = false;
            glassSyringe.quality = PickupObject.ItemQuality.A;
            List<string> mandatoryConsoleIDs = new List<string>
            {
                "bny:jammed_guillotine"
            };
            List<string> optionalConsoleIDs = new List<string>
            {
                "bny:guillotine_rounds",
                "gunslingers_ashes",
                "huntsman",
                "skull_spitter"
            };
            CustomSynergies.Add("Judge, Jury and...", mandatoryConsoleIDs, optionalConsoleIDs, true);
            List<string> mandatoryConsole1IDs = new List<string>
            {
                "bny:jammed_guillotine"
            };
            List<string> optionalConsole1IDs = new List<string>
            {
                "melted_rock",
                "pitchfork",
                "cursed_bullets",
                "melted_rock",
                "shotgun_full_of_hate"
            };
            CustomSynergies.Add("Lost Humanity", mandatoryConsole1IDs, optionalConsole1IDs, true);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }
        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_OBJ_lock_pick_01", base.gameObject);
            foreach (AIActor aiactor in user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
            {
                bool flag = aiactor != null;
                if (flag)
                {
                    float currentHealthPercentage = aiactor.healthHaver.GetCurrentHealthPercentage();
                    bool flaga3 = currentHealthPercentage <= 0.3f;
                    bool isBoss = aiactor.healthHaver.IsBoss;
                    bool flag4 = flaga3 && !isBoss;
                    if (flag4)
                    {
                        aiactor.healthHaver.ApplyDamage(100000f, Vector2.zero, "Telefrag", CoreDamageTypes.Void, DamageCategory.Normal, true, null, false);
                        bool synergy = user.PlayerHasActiveSynergy("Judge, Jury and...");
                        if (synergy)
                        {
                            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
                            GoopDefinition goopDef = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
                            DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDef).TimedAddGoopCircle(aiactor.sprite.WorldBottomCenter, 2.5f, 1.4f, false);
                        }
                    }
                    else
                    {
                        aiactor.healthHaver.ApplyDamage(10f, Vector2.zero, "Telefrag", CoreDamageTypes.Void, DamageCategory.Normal, true, null, false);
                        bool synergy = user.PlayerHasActiveSynergy("Lost Humanity");
                        if (synergy)
                        {
                            Vector3 position = aiactor.sprite.WorldCenter;
                            GameObject gameObject = SpawnManager.SpawnProjectile((PickupObjectDatabase.GetById(45) as Gun).DefaultModule.projectiles[0].gameObject, position, Quaternion.Euler(0f, 0f, BraveMathCollege.Atan2Degrees(user.sprite.WorldCenter - aiactor.sprite.WorldCenter)), true);
                            Projectile component = gameObject.GetComponent<Projectile>();
                            bool flag12 = component != null;
                            bool flag2 = flag12;
                            if (flag2)
                            {
                                PierceProjModifier spook = component.gameObject.AddComponent<PierceProjModifier>();
                                spook.penetration = 10;
                                component.SpawnedFromOtherPlayerProjectile = true;
                                component.Shooter = user.specRigidbody;
                                component.Owner = user;
                                component.baseData.speed = 4f;
                                component.baseData.damage = 10f;
                                component.AdditionalScaleMultiplier = 0.7f;
                                component.ignoreDamageCaps = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
