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
    public class ResurrectionBullets : PassiveItem
    {
        private float random;

        public static void Init()
        {
            string itemName = "Resurrection Bullets";
            string resourceName = "BunnyMod/Resources/resurrectionbullets";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<ResurrectionBullets>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Get living again, maggots!";
            string longDesc = "Bullets enchanted with the ability to change a gundead corpse so fast that they come back to life.\n\n" +
                "These bullets were originally used to find gundead suffering from Resurrectile Dysfunction, a lead-based disease which prevented gundead from coming back to life. Surprisingly common.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.RateOfFire, .1f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.C;
            item.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
            List<string> mandatoryConsoleIDs1 = new List<string>
            {
                "bny:resurrection_bullets",
                "zombie_bullets"
            };
            CustomSynergies.Add("Revitalization", mandatoryConsoleIDs1, null, true);
            List<string> mandatoryConsoleIDs2 = new List<string>
            {
                "bny:resurrection_bullets",
                "ghost_bullets"
            };
            CustomSynergies.Add("Souls", mandatoryConsoleIDs2, null, true);
        }
        private void OnEnemyDamaged(float damage, bool fatal, HealthHaver enemyHealth)
        {
            if (enemyHealth.specRigidbody != null)
            {
                bool flag = Owner.HasPickupID(528);
                if (flag)
                {
                    this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (random <= 0.2f)
                    {
                        if (enemyHealth.aiActor.EnemyGuid != "e21ac9492110493baef6df02a2682a0d")
                            if (enemyHealth.aiActor.EnemyGuid != "4db03291a12144d69fe940d5a01de376")
                            {
                                bool flag2 = enemyHealth.aiActor && fatal;
                                if (flag2)
                                {
                                    this.CommitAliveAgain(enemyHealth.sprite.WorldCenter);
                                }
                            }
                            else
                            {
                                AkSoundEngine.PostEvent("Play_ENM_Hurt", base.gameObject);
                            }
                    }
                }
                else
                {
                    this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (random <= 0.16f)
                    {
                        if (enemyHealth.aiActor.EnemyGuid != "e21ac9492110493baef6df02a2682a0d")
                            if (enemyHealth.aiActor.EnemyGuid != "4db03291a12144d69fe940d5a01de376")
                            {
                                bool flag3 = enemyHealth.aiActor && fatal;
                                if (flag3)
                                {
                                    this.CommitAliveAgain(enemyHealth.sprite.WorldCenter);
                                }
                            }
                            else
                            {
                                AkSoundEngine.PostEvent("Play_ENM_Hurt", base.gameObject);
                            }
                    }

                }
            }

        }
        public void CommitAliveAgain(Vector3 position)
        {
            bool flag4 = Owner.HasPickupID(172);
            if (flag4)
            {
                string guid;
                guid = "4db03291a12144d69fe940d5a01de376";
                PlayerController owner = base.Owner;
                AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                IntVector2? intVector = new IntVector2?(base.Owner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                aiactor.CanTargetEnemies = true;
                aiactor.CanTargetPlayers = false;
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                aiactor.gameObject.AddComponent<KillOnRoomClear>();
                aiactor.IsHarmlessEnemy = true;
                aiactor.IgnoreForRoomClear = true;
                aiactor.HandleReinforcementFallIntoRoom(0f);

            }
            else
            {
                string guid;
                guid = "e21ac9492110493baef6df02a2682a0d";
                PlayerController owner = base.Owner;
                AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                IntVector2? intVector = new IntVector2?(base.Owner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                aiactor.CanTargetEnemies = true;
                aiactor.CanTargetPlayers = false;
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                aiactor.gameObject.AddComponent<KillOnRoomClear>();
                aiactor.IsHarmlessEnemy = true;
                aiactor.IgnoreForRoomClear = true;
                aiactor.HandleReinforcementFallIntoRoom(0f);
            }
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
            player.OnAnyEnemyReceivedDamage += (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnAnyEnemyReceivedDamage -= (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(this.OnEnemyDamaged));
            Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}
