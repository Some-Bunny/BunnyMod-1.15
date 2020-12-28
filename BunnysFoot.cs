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
    public class BunnysFoot : PassiveItem
    {

        public static void Init()
        {
            string itemName = "Bunnys Foot";
            string resourceName = "BunnyMod/Resources/bunnysfoot.png";
            GameObject obj = new GameObject(itemName);
            BunnysFoot counterChamber = obj.AddComponent<BunnysFoot>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Give it back!";
            string longDesc = "The foot of a bunny fitted onto a chain.\n\nUnlike the more common rabbits foot, a bunnys effect on luck is 100x more potent.\n\nI still want it back though.";
            counterChamber.SetupItem(shortDesc, longDesc, "bny");
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.MoneyMultiplierFromEnemies, 1.25f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.ExtremeShadowBulletChance, 5f, StatModifier.ModifyMethod.ADDITIVE);
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.ShadowBulletChance, 5f, StatModifier.ModifyMethod.ADDITIVE);
            counterChamber.AddPassiveStatModifier(PlayerStats.StatType.Coolness, 3f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            counterChamber.quality = PickupObject.ItemQuality.S;
        }
        private void LuckOTheBunnyShoppe(PlayerController player, ShopItemController shop)
        {

            {
                int num3 = UnityEngine.Random.Range(0, 5);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, player);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(120).gameObject, player);
                }
                bool aa = num3 == 3;
                if (aa)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(127).gameObject, player);
                }
                bool snote = num3 == 4;
                if (snote)
                {
                    for (int counter = 0; counter < UnityEngine.Random.Range(7f, 23f); counter++)
                    {
                        LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, player);
                    }
                }
            }
        }
        public static void LootPlus(Action<Chest, PlayerController> orig, Chest self, PlayerController player)
        {
            orig(self, player);
            {
                {
                    bool flag = player.HasPickupID(Game.Items["bny:bunnys_foot"].PickupObjectId);
                    if (flag)
                    {
                        int numure = UnityEngine.Random.Range(0, 4);
                        bool fuckye = numure == 0 | numure == 1 | numure == 2;
                        if (fuckye)
                        {
                            int num3 = UnityEngine.Random.Range(0, 3);
                            bool flag3 = num3 == 0;
                            if (flag3)
                            {
                                LootEngine.SpawnItem(PickupObjectDatabase.GetById(224).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
                            }
                            bool flag4 = num3 == 1;
                            if (flag4)
                            {
                                LootEngine.SpawnItem(PickupObjectDatabase.GetById(67).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
                            }
                            bool flag6 = num3 == 2;
                            if (flag6)
                            {
                                LootEngine.SpawnItem(PickupObjectDatabase.GetById(78).gameObject, self.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
                            }
                        }
                        bool fuckye1 = numure == 3;
                        if (fuckye1)
                        {
                            GameObject obj = new GameObject();
                            BunnysFoot fuck = obj.AddComponent<BunnysFoot>();
                            foreach (PickupObject pickup in self.contents)
                            {
                                GameManager.Instance.RewardManager.SpawnTotallyRandomItem(self.specRigidbody.UnitCenter, pickup.quality);
                            }
                        }
                    }
                }
            }
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemy)
        {
            bool flag = fatal && enemy.aiActor;
            if (flag)
            {

                {
                    int numure = UnityEngine.Random.Range(0, 4);
                    bool fuckye = numure == 0 | numure == 1 | numure == 2;
                    if (fuckye)
                    {
                        bool flag3 = this.mimicGuids.Contains(enemy.aiActor.EnemyGuid);
                        if (flag3)
                        {
                            int id = BraveUtility.RandomElement<int>(BunnysFoot.Lootdrops);
                            LootEngine.SpawnItem(PickupObjectDatabase.GetById(id).gameObject, enemy.specRigidbody.UnitCenter, Vector2.down, .7f, false, true, false);
                        }
                    }
                    bool fuckye1 = numure == 3;
                    if (fuckye1)
                    {
                        PickupObject.ItemQuality itemQuality = PickupObject.ItemQuality.D;
                        bool flag3 = enemy.aiActor.EnemyGuid == "2ebf8ef6728648089babb507dec4edb7";
                        if (flag3)
                        {
                            itemQuality = PickupObject.ItemQuality.D;
                            this.SpawnBonusItem(enemy, itemQuality);
                        }
                        else
                        {
                            bool flag4 = enemy.aiActor.EnemyGuid == "d8d651e3484f471ba8a2daa4bf535ce6";
                            if (flag4)
                            {
                                itemQuality = PickupObject.ItemQuality.C;
                                this.SpawnBonusItem(enemy, itemQuality);
                            }
                            else
                            {
                                bool flag5 = enemy.aiActor.EnemyGuid == "abfb454340294a0992f4173d6e5898a8";
                                if (flag5)
                                {
                                    itemQuality = PickupObject.ItemQuality.B;
                                    this.SpawnBonusItem(enemy, itemQuality);
                                }
                                else
                                {
                                    bool flag6 = enemy.aiActor.EnemyGuid == "d8fd592b184b4ac9a3be217bc70912a2";
                                    if (flag6)
                                    {
                                        itemQuality = PickupObject.ItemQuality.A;
                                        this.SpawnBonusItem(enemy, itemQuality);
                                    }
                                    else
                                    {
                                        bool flag7 = enemy.aiActor.EnemyGuid == "6450d20137994881aff0ddd13e3d40c8";
                                        if (flag7)
                                        {
                                            itemQuality = PickupObject.ItemQuality.S;
                                            this.SpawnBonusItem(enemy, itemQuality);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void SpawnBonusItem(HealthHaver enemy, PickupObject.ItemQuality itemQuality)
        {
            GameManager.Instance.RewardManager.SpawnTotallyRandomItem(enemy.specRigidbody.UnitCenter, itemQuality, itemQuality);
        }
        public override void Pickup(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            player.OnItemPurchased += this.LuckOTheBunnyShoppe;
            base.Pickup(player);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Remove(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            player.OnItemPurchased -= this.LuckOTheBunnyShoppe;
            return base.Drop(player);
        }
        private List<string> mimicGuids = new List<string>
        {
            "2ebf8ef6728648089babb507dec4edb7",
            "d8d651e3484f471ba8a2daa4bf535ce6",
            "abfb454340294a0992f4173d6e5898a8",
            "d8fd592b184b4ac9a3be217bc70912a2",
            "ac9d345575444c9a8d11b799e8719be0",
            "6450d20137994881aff0ddd13e3d40c8",
            "479556d05c7c44f3b6abb3b2067fc778"
        };
        public static List<int> Lootdrops = new List<int>
        {
            73,
            85,
            120,
            67,
            224,
            600,
            78
        };
        public PickupObject.ItemQuality Spawnquality;
        public PassiveItem target;
    }
}



