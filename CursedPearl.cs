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
    public class CursedPearl : PlayerItem
    {
        public static void Init()
        {
            string itemName = "Cursed Pearl";
            string resourceName = "BunnyMod/Resources/cursepearl.png";
            GameObject obj = new GameObject(itemName);
            CursedPearl whisper = obj.AddComponent<CursedPearl>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Eye of the Jammed";
            string longDesc = "A fragile pearl with the souls of the Jammed stirring inside of it. The inside seems to be more reactive to higher levels of evil.";
            whisper.SetupItem(shortDesc, longDesc, "bny");
            whisper.SetCooldownType(ItemBuilder.CooldownType.Timed, 2f);
            whisper.consumable = true;
            whisper.quality = PickupObject.ItemQuality.EXCLUDED;
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }


        protected override void DoEffect(PlayerController user)
        {
            SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, base.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(base.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
            AkSoundEngine.PostEvent("Play_WPN_Life_Orb_Blast_01", base.gameObject);
            bool harderlotj = JammedSquire.NoHarderLotJ;
            if (harderlotj)
            {
                float num = 0f;
                num = (user.stats.GetStatValue(PlayerStats.StatType.Curse));
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 20f))
                {
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_jingle_01", base.gameObject);
                    int num3 = UnityEngine.Random.Range(0, 5);
                    bool flag3 = num3 == 0;
                    if (flag3)
                    {
                        ApplyStat(user, PlayerStats.StatType.KnockbackMultiplier, 1.2f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                    }
                    bool flag4 = num3 == 1;
                    if (flag4)
                    {
                        ApplyStat(user, PlayerStats.StatType.Damage, 1.1f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    bool a = num3 == 2;
                    if (a)
                    {
                        ApplyStat(user, PlayerStats.StatType.RateOfFire, 1.1f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    bool ea = num3 == 3;
                    if (ea)
                    {
                        ApplyStat(user, PlayerStats.StatType.ReloadSpeed, 0.9f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    bool ae = num3 == 4;
                    if (ae)
                    {
                        ApplyStat(user, PlayerStats.StatType.Accuracy, 0.9f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_jingle_01", base.gameObject);

                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 16f))
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.LastOwner);
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 66f))
                {
                    IntVector2 bestRewardLocation = user.CurrentRoom.GetBestRewardLocation(IntVector2.One * 3, RoomHandler.RewardLocationStyle.PlayerCenter, true);
                    Chest chest2 = GameManager.Instance.RewardManager.SpawnRewardChestAt(bestRewardLocation, -1f, PickupObject.ItemQuality.EXCLUDED);
                    chest2.RegisterChestOnMinimap(chest2.GetAbsoluteParentRoom());
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 16f))
                {
                    for (int counter = 0; counter < UnityEngine.Random.Range((1f * num), (4f * num)); counter++)
                    {
                        LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, base.LastOwner);
                    }
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 16f))
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(78).gameObject, base.LastOwner);
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 16f))
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(224).gameObject, base.LastOwner);
                }
            }
            else
            {
                ApplyStat(user, PlayerStats.StatType.Curse, 1f, StatModifier.ModifyMethod.ADDITIVE);
                float num = 0f;
                num = (user.stats.GetStatValue(PlayerStats.StatType.Curse));
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 12f))
                {
                    int num3 = UnityEngine.Random.Range(0, 5);
                    bool flag3 = num3 == 0;
                    if (flag3)
                    {
                        ApplyStat(user, PlayerStats.StatType.KnockbackMultiplier, 1.2f, StatModifier.ModifyMethod.MULTIPLICATIVE);
                    }
                    bool flag4 = num3 == 1;
                    if (flag4)
                    {
                        ApplyStat(user, PlayerStats.StatType.Damage, 1.1f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    bool a = num3 == 2;
                    if (a)
                    {
                        ApplyStat(user, PlayerStats.StatType.RateOfFire, 1.1f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    bool ea = num3 == 3;
                    if (ea)
                    {
                        ApplyStat(user, PlayerStats.StatType.ReloadSpeed, 0.9f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    bool ae = num3 == 4;
                    if (ae)
                    {
                        ApplyStat(user, PlayerStats.StatType.Accuracy, 0.9f, StatModifier.ModifyMethod.MULTIPLICATIVE);

                    }
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_jingle_01", base.gameObject);
                    //this.DefineEffects();
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 12f))
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.LastOwner);
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 45f))
                {
                    IntVector2 bestRewardLocation = user.CurrentRoom.GetBestRewardLocation(IntVector2.One * 3, RoomHandler.RewardLocationStyle.PlayerCenter, true);
                    Chest chest2 = GameManager.Instance.RewardManager.SpawnRewardChestAt(bestRewardLocation, -1f, PickupObject.ItemQuality.EXCLUDED);
                    chest2.RegisterChestOnMinimap(chest2.GetAbsoluteParentRoom());
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 12f))
                {
                    for (int counter = 0; counter < UnityEngine.Random.Range((1f * num), (6f * num)); counter++)
                    {
                        LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, base.LastOwner);
                    }
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 12f))
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(78).gameObject, base.LastOwner);
                }
                this.rng = UnityEngine.Random.Range(0.00f, 1.00f);
                if (this.rng <= (num / 12f))
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(224).gameObject, base.LastOwner);
                }
            }
        }
		public void DefineEffects()
		{
			this.statEffects.Add(new CursedPearl.Effect
			{
				statToEffect = PlayerStats.StatType.Accuracy,
				modifyMethod = StatModifier.ModifyMethod.MULTIPLICATIVE,
				amount = 0.9f
			});
            this.statEffects.Add(new CursedPearl.Effect
            {
                statToEffect = PlayerStats.StatType.Damage,
                modifyMethod = StatModifier.ModifyMethod.MULTIPLICATIVE,
                amount = 1.1f
            });
            this.statEffects.Add(new CursedPearl.Effect
            {
                statToEffect = PlayerStats.StatType.RateOfFire,
                modifyMethod = StatModifier.ModifyMethod.MULTIPLICATIVE,
                amount = 1.1f
            });
            this.statEffects.Add(new CursedPearl.Effect
            {
                statToEffect = PlayerStats.StatType.KnockbackMultiplier,
                modifyMethod = StatModifier.ModifyMethod.MULTIPLICATIVE,
                amount = 1.2f
            });
            this.statEffects.Add(new CursedPearl.Effect
            {
                statToEffect = PlayerStats.StatType.ReloadSpeed,
                modifyMethod = StatModifier.ModifyMethod.MULTIPLICATIVE,
                amount = 0.9f
            });
        }
		private float rng;
        private static float rnge;

        public List<CursedPearl.Effect> statEffects = new List<CursedPearl.Effect>();
        private void AddStat(PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod method = StatModifier.ModifyMethod.ADDITIVE)
        {
            StatModifier statModifier = new StatModifier
            {
                amount = amount,
                statToBoost = statType,
                modifyType = method
            };
            bool flag = this.passiveStatModifiers == null;
            if (flag)
            {
                this.passiveStatModifiers = new StatModifier[]
                {
                    statModifier
                };
            }
            else
            {
                this.passiveStatModifiers = this.passiveStatModifiers.Concat(new StatModifier[]
                {
                    statModifier
                }).ToArray<StatModifier>();
            }
        }
        private void ApplyStat(PlayerController player, PlayerStats.StatType statType, float amountToApply, StatModifier.ModifyMethod method)
        {
            player.stats.RecalculateStats(player, false, false);
            StatModifier statModifier = new StatModifier()
            {
                statToBoost = statType,
                amount = amountToApply,
                modifyType = method
            };
            player.ownerlessStatModifiers.Add(statModifier);
            player.stats.RecalculateStats(player, false, false);
        }
		public struct Effect
		{
			public PlayerStats.StatType statToEffect;

			public float amount;

			public StatModifier.ModifyMethod modifyMethod;

			public Action<CursedPearl.Effect, PlayerController> action;
		}


        public static void CursePearlDrops(Action<AIActor, Vector2> orig, AIActor self, Vector2 finalDamageDirection)
        {
            PlayerController player = (GameManager.Instance.PrimaryPlayer);
            orig(self, finalDamageDirection);
            bool harderlotj = JammedSquire.NoHarderLotJ;
            if (harderlotj)
            {
            }
            else
            {
                bool jamnation = self.IsBlackPhantom;
                if (!jamnation)
                {
                    bool FuckYouNoPearlYouGreedyFuck = player.HasPickupID(Game.Items["bny:cursed_pearl"].PickupObjectId);
                    if (!FuckYouNoPearlYouGreedyFuck)
                    {
                        float num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
                        CursedPearl.rnge = UnityEngine.Random.Range(0.000f, 1.000f);
                        if (CursedPearl.rnge <= (0.0166 * (1 + (num/3.5))))
                        {
                            SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, self.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(self.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
                            LootEngine.SpawnItem(ETGMod.Databases.Items["Cursed Pearl"].gameObject, self.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
                        }
                    }
                }
                else
                {
                    float num = (player.stats.GetStatValue(PlayerStats.StatType.Curse));
                    CursedPearl.rnge = UnityEngine.Random.Range(0.000f, 1.000f);
                    if (CursedPearl.rnge <= (0.0166 * (1 + (num / 14))))
                    {
                        SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, self.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(self.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        LootEngine.SpawnItem(ETGMod.Databases.Items["Cursed Pearl"].gameObject, self.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
                    }
                }
            }
        }
    }
}
