using ItemAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BunnyMod
{
    class AstralCounterweight : PassiveItem
    {
        //idea by bunny based on https://bindingofisaacrebirth.gamepedia.com/Glyph_of_Balance
        public static void Init()
        {
            string itemName = "Astral Counterweight";
            string resourceName = "BunnyMod/Resources/astralcounterweight";
            GameObject obj = new GameObject(itemName);
            AstralCounterweight greandeParasite = obj.AddComponent<AstralCounterweight>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Equillibrium";
            string longDesc = "Masterfully forged in the Glaurung star system, this counterweight ensures balance in the soul.";
            greandeParasite.SetupItem(shortDesc, longDesc, "bny");
            greandeParasite.quality = PickupObject.ItemQuality.S;
        }

        protected override void Update()
        {
            PlayerController player = base.Owner;
            if (player != null && !cooldown)
            {
                List<PlayerStats.StatType> statTypes = new List<PlayerStats.StatType>(stats.Keys);
                if (this.previousStats.Values.Sum() == -balanceDivider)
                {
                    foreach (PlayerStats.StatType stat in statTypes)
                    {
                        this.previousStats[stat] = player.stats.GetStatValue(stat);
                        this.stats[stat] = player.stats.GetStatValue(stat);
                    }
                }
                else
                {
                    foreach (PlayerStats.StatType stat in statTypes)
                    {
                        this.stats[stat] = player.stats.GetStatValue(stat);
                    }

                    foreach (PlayerStats.StatType stat in statTypes)
                    {
                        if (this.previousStats[stat] != this.stats[stat])
                        {
                            cooldown = true;
                            this.CanBeDropped = false;
                            statsModified[stat] = true;
                            statsModifiedAmount[stat] = this.stats[stat] - this.previousStats[stat];
                        }
                    }
                    if (cooldown)
                    {
                        base.StartCoroutine(this.RecalculateStats());
                    }
                    else
                    {
                        foreach (PlayerStats.StatType stat in statTypes)
                        {
                            this.previousStats[stat] = this.stats[stat];
                        }
                    }
                }
            }
        }

        //pickup: get stats and baseStats and make the diff to balance

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            List<PlayerStats.StatType> statTypes = new List<PlayerStats.StatType>(stats.Keys);
            foreach (PlayerStats.StatType stat in statTypes)
            {
                this.previousStats[stat] = player.stats.GetStatValue(stat);
                this.stats[stat] = player.stats.GetStatValue(stat);
            }
            foreach (PlayerStats.StatType stat in statTypes)
            {
                if (this.stats[stat] != player.stats.GetBaseStatValue(stat))
                {
                    cooldown = true;
                    this.CanBeDropped = false;
                    statsModified[stat] = true;
                    statsModifiedAmount[stat] = this.stats[stat] - player.stats.GetBaseStatValue(stat);
                }
            }
            if (cooldown)
            {
                base.StartCoroutine(this.RecalculateStats());
            }
        }

        public override DebrisObject Drop(PlayerController player)
        {
            if (this.passiveStatModifiers != null && this.passiveStatModifiers.Count() > 0)
            {
                foreach (StatModifier statMod in this.passiveStatModifiers)
                {
                    this.RemovePassiveStatModifier(statMod);
                }
            }
            return base.Drop(player);
        }

        private IEnumerator RecalculateStats()
        {
            //do balance
            List<PlayerStats.StatType> statTypes = new List<PlayerStats.StatType>(stats.Keys);

            foreach (PlayerStats.StatType stat in statTypes)
            {
                if (statsModified[stat])
                {
                    //increasing accuracy make the gun less accurate, reload speed increase make it reload slower https://enterthegungeon.gamepedia.com/Stats#ReloadSpeed
                    if (stat == PlayerStats.StatType.Accuracy || stat == PlayerStats.StatType.ReloadSpeed)
                    {
                        this.AddPassiveStatModifier(stat, -statsModifiedAmount[stat]);
                        foreach (PlayerStats.StatType stat2 in statTypes)
                        {
                            if (stat2 == PlayerStats.StatType.Accuracy || stat2 == PlayerStats.StatType.ReloadSpeed)
                            {
                                this.AddPassiveStatModifier(stat2, (statsModifiedAmount[stat] / balanceDivider));
                            }
                            else
                            {
                                this.AddPassiveStatModifier(stat2, -(statsModifiedAmount[stat] / balanceDivider));
                            }
                        }
                    }
                    else
                    {
                        this.AddPassiveStatModifier(stat, -statsModifiedAmount[stat]);
                        foreach (PlayerStats.StatType stat2 in statTypes)
                        {
                            if (stat2 == PlayerStats.StatType.Accuracy || stat2 == PlayerStats.StatType.ReloadSpeed)
                            {
                                this.AddPassiveStatModifier(stat2, -(statsModifiedAmount[stat] / balanceDivider));
                            }
                            else
                            {
                                this.AddPassiveStatModifier(stat2, (statsModifiedAmount[stat] / balanceDivider));
                            }
                        }
                    }
                }
            }
            base.Owner.stats.RecalculateStats(base.Owner, true);
            yield return null;
            foreach (PlayerStats.StatType stat in statTypes)
            {
                this.previousStats[stat] = -1;
                this.stats[stat] = -1;
                this.statsModified[stat] = false;
                this.statsModifiedAmount[stat] = 0;
            }
            this.CanBeDropped = true;
            cooldown = false;
            yield break;
        }


        /*-----------------------------------------------------------------------------------------------*/

        //be cautious that the dictionnaries and the balance divider have the same number of stats
        private static readonly int balanceDivider = 10;

        [SerializeField]
        private bool cooldown = false;

        [SerializeField]
        private Dictionary<PlayerStats.StatType, float> previousStats { get; set; } = new Dictionary<PlayerStats.StatType, float>
        {
            {
                PlayerStats.StatType.MovementSpeed,
                -1
            },
            {
                PlayerStats.StatType.RateOfFire,
                -1
            },
            {
                PlayerStats.StatType.Coolness,
                -1
            },
            {
                PlayerStats.StatType.Damage,
                -1
            },
            {
                PlayerStats.StatType.ProjectileSpeed,
                -1
            },
            {
                PlayerStats.StatType.ReloadSpeed,
                -1
            },
            {
                PlayerStats.StatType.DamageToBosses,
                -1
            },
            {
                PlayerStats.StatType.ThrownGunDamage,
                -1
            },
            {
                PlayerStats.StatType.DodgeRollDamage,
                -1
            },
            {
                PlayerStats.StatType.Accuracy,
                -1
            },
        };

        [SerializeField]
        public Dictionary<PlayerStats.StatType, float> stats { get; set; } = new Dictionary<PlayerStats.StatType, float>
        {
            {
                PlayerStats.StatType.MovementSpeed,
                -1
            },
            {
                PlayerStats.StatType.RateOfFire,
                -1
            },
            {
                PlayerStats.StatType.Coolness,
                -1
            },
            {
                PlayerStats.StatType.Damage,
                -1
            },
            {
                PlayerStats.StatType.ProjectileSpeed,
                -1
            },
            {
                PlayerStats.StatType.ReloadSpeed,
                -1
            },
            {
                PlayerStats.StatType.DamageToBosses,
                -1
            },
            {
                PlayerStats.StatType.ThrownGunDamage,
                -1
            },
            {
                PlayerStats.StatType.DodgeRollDamage,
                -1
            },
            {
                PlayerStats.StatType.Accuracy,
                -1
            },
        };

        [SerializeField]
        public Dictionary<PlayerStats.StatType, bool> statsModified { get; set; } = new Dictionary<PlayerStats.StatType, bool>
        {
            {
                PlayerStats.StatType.MovementSpeed,
                false
            },
            {
                PlayerStats.StatType.RateOfFire,
                false
            },
            {
                PlayerStats.StatType.Coolness,
                false
            },
            {
                PlayerStats.StatType.Damage,
                false
            },
            {
                PlayerStats.StatType.ProjectileSpeed,
                false
            },
            {
                PlayerStats.StatType.ReloadSpeed,
                false
            },
            {
                PlayerStats.StatType.DamageToBosses,
                false
            },
            {
                PlayerStats.StatType.ThrownGunDamage,
                false
            },
            {
                PlayerStats.StatType.DodgeRollDamage,
                false
            },
            {
                PlayerStats.StatType.Accuracy,
                false
            },
        };

        [SerializeField]
        public Dictionary<PlayerStats.StatType, float> statsModifiedAmount { get; set; } = new Dictionary<PlayerStats.StatType, float>
        {
            {
                PlayerStats.StatType.MovementSpeed,
                0
            },
            {
                PlayerStats.StatType.RateOfFire,
                0
            },
            {
                PlayerStats.StatType.Coolness,
                0
            },
            {
                PlayerStats.StatType.Damage,
                0
            },
            {
                PlayerStats.StatType.ProjectileSpeed,
                0
            },
            {
                PlayerStats.StatType.ReloadSpeed,
                0
            },
            {
                PlayerStats.StatType.DamageToBosses,
                0
            },
            {
                PlayerStats.StatType.ThrownGunDamage,
                0
            },
            {
                PlayerStats.StatType.DodgeRollDamage,
                0
            },
            {
                PlayerStats.StatType.Accuracy,
                0
            },
        };

        //dico with all stats if u need it
        public Dictionary<PlayerStats.StatType, float> fullStats { get; set; } = new Dictionary<PlayerStats.StatType, float>
        {
            {
                PlayerStats.StatType.MovementSpeed,
                -1
            },
            {
                PlayerStats.StatType.RateOfFire,
                -1
            },
            {
                PlayerStats.StatType.Accuracy,
                -1
            },
            {
                PlayerStats.StatType.Health,
                -1
            },
            {
                PlayerStats.StatType.Coolness,
                -1
            },
            {
                PlayerStats.StatType.Damage,
                -1
            },
            {
                PlayerStats.StatType.ProjectileSpeed,
                -1
            },
            {
                PlayerStats.StatType.AdditionalGunCapacity,
                -1
            },
            {
                PlayerStats.StatType.AdditionalItemCapacity,
                -1
            },
            {
                PlayerStats.StatType.AmmoCapacityMultiplier,
                -1
            },
            {
                PlayerStats.StatType.ReloadSpeed,
                -1
            },
            {
                PlayerStats.StatType.AdditionalShotPiercing,
                -1
            },
            {
                PlayerStats.StatType.KnockbackMultiplier,
                -1
            },
            {
                PlayerStats.StatType.GlobalPriceMultiplier,
                -1
            },
            {
                PlayerStats.StatType.Curse,
                -1
            },
            {
                PlayerStats.StatType.PlayerBulletScale,
                -1
            },
            {
                PlayerStats.StatType.AdditionalClipCapacityMultiplier,
                -1
            },
            {
                PlayerStats.StatType.AdditionalShotBounces,
                -1
            },
            {
                PlayerStats.StatType.AdditionalBlanksPerFloor,
                -1
            },
            {
                PlayerStats.StatType.ShadowBulletChance,
                -1
            },
            {
                PlayerStats.StatType.ThrownGunDamage,
                -1
            },
            {
                PlayerStats.StatType.DodgeRollDamage,
                -1
            },
            {
                PlayerStats.StatType.DamageToBosses,
                -1
            },
            {
                PlayerStats.StatType.EnemyProjectileSpeedMultiplier,
                -1
            },
            {
                PlayerStats.StatType.ExtremeShadowBulletChance,
                -1
            },
            {
                PlayerStats.StatType.ChargeAmountMultiplier,
                -1
            },
            {
                PlayerStats.StatType.RangeMultiplier,
                -1
            },
            {
                PlayerStats.StatType.DodgeRollDistanceMultiplier,
                -1
            },
            {
                PlayerStats.StatType.DodgeRollSpeedMultiplier,
                -1
            },
            {
                PlayerStats.StatType.TarnisherClipCapacityMultiplier,
                -1
            },
            {
                PlayerStats.StatType.MoneyMultiplierFromEnemies,
                -1
            },
        };
    }
}