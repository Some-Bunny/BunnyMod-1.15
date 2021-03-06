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
    public class GodLifesGift : PassiveItem
    {
        public static void Init()
        {
            string itemName = "The Flames' Gift";

            string resourceName = "BunnyMod/Resources/godoflifesgift";

            GameObject obj = new GameObject(itemName);

            GodLifesGift godLifesGift = obj.AddComponent<GodLifesGift>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "I am not the God Of Death...";
            string longDesc = "A contract from the Flames. Holding it grants you enough strength to be worthy of the job.\n\n" +
                "The Flames don't accept imperfection, and will bend time just to avoid failure.";

            godLifesGift.SetupItem(shortDesc, longDesc, "bny");
            godLifesGift.AddPassiveStatModifier(PlayerStats.StatType.Damage, .6f, StatModifier.ModifyMethod.ADDITIVE);
            godLifesGift.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, .7f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            godLifesGift.AddPassiveStatModifier(PlayerStats.StatType.ProjectileSpeed, .3f, StatModifier.ModifyMethod.ADDITIVE);
            godLifesGift.quality = PickupObject.ItemQuality.B;
            godLifesGift.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.OnHitByProjectile += this.OwnerHitByProjectile;
            //Tools.Print($"Player picked up {this.DisplayName}");
        }
        private void OwnerHitByProjectile(Projectile incomingProjectile, PlayerController arg2)
        {
            AIActor aIActor = (AIActor)incomingProjectile.Owner;
            PlayerController player = (GameManager.Instance.PrimaryPlayer);
            bool isInCombat = player.IsInCombat;
            if (isInCombat)
            {
                if (incomingProjectile.Owner != arg2)
                {
                    if (!incomingProjectile.Owner.healthHaver.IsBoss)
                    {
                        bool flag = ((double)player.healthHaver.GetCurrentHealth() == 0.5 && player.healthHaver.Armor == 0f) || player.healthHaver.GetCurrentHealth() == 0f && player.healthHaver.Armor == 1f;
                        if (!flag)
                        {
                            AkSoundEngine.PostEvent("Play_BOSS_lichB_intro_01", base.gameObject);
                            GameStatsManager.Instance.RegisterStatChange(TrackedStats.TIMES_HIT_WITH_THE_GRIPPY, 1f);
                            int num = 1;
                            if (num < 1)
                            {
                                num = 1;
                            }
                            List<RoomHandler> list = new List<RoomHandler>();
                            List<RoomHandler> list2 = new List<RoomHandler>();
                            //PlayerController player = base.Owner;
                            list.Add(player.CurrentRoom);
                            while (list.Count - 1 < num)
                            {
                                RoomHandler roomHandler = list[list.Count - 1];
                                list2.Clear();
                                foreach (RoomHandler roomHandler2 in roomHandler.connectedRooms)
                                {
                                    if (roomHandler2.hasEverBeenVisited && roomHandler2.distanceFromEntrance < roomHandler.distanceFromEntrance && !list.Contains(roomHandler2))
                                    {
                                        if (!roomHandler2.area.IsProceduralRoom || roomHandler2.area.proceduralCells == null)
                                        {
                                            list2.Add(roomHandler2);
                                        }
                                    }
                                }
                                if (list2.Count == 0)
                                {
                                    break;
                                }
                                list.Add(BraveUtility.RandomElement<RoomHandler>(list2));
                            }
                            if (list.Count > 1)
                            {
                                base.Owner.RespawnInPreviousRoom(false, PlayerController.EscapeSealedRoomStyle.GRIP_MASTER, true, list[list.Count - 1]);
                                for (int i = 1; i < list.Count - 1; i++)
                                {
                                    list[i].ResetPredefinedRoomLikeDarkSouls();
                                }
                            }
                            else
                            {
                                base.Owner.RespawnInPreviousRoom(false, PlayerController.EscapeSealedRoomStyle.GRIP_MASTER, true, null);
                            }
                            base.Owner.specRigidbody.Velocity = Vector2.zero;
                            base.Owner.knockbackDoer.TriggerTemporaryKnockbackInvulnerability(1f);
                        }
                    }
                }
            }
        }
        /*
        private void FailContract(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
        {
            PlayerController player = (GameManager.Instance.PrimaryPlayer);
            bool isInCombat = player.IsInCombat;
            if (isInCombat)
            {
                bool flag = ((double)player.healthHaver.GetCurrentHealth() == 0.5 && player.healthHaver.Armor == 0f) || player.healthHaver.GetCurrentHealth() == 0f && player.healthHaver.Armor == 1f;
                if (!flag)
                {
                    AkSoundEngine.PostEvent("Play_BOSS_lichB_intro_01", base.gameObject);
                    GameStatsManager.Instance.RegisterStatChange(TrackedStats.TIMES_HIT_WITH_THE_GRIPPY, 1f);
                    int num = 1;
                    if (num < 1)
                    {
                        num = 1;
                    }
                    List<RoomHandler> list = new List<RoomHandler>();
                    List<RoomHandler> list2 = new List<RoomHandler>();
                    //PlayerController player = base.Owner;
                    list.Add(player.CurrentRoom);
                    while (list.Count - 1 < num)
                    {
                        RoomHandler roomHandler = list[list.Count - 1];
                        list2.Clear();
                        foreach (RoomHandler roomHandler2 in roomHandler.connectedRooms)
                        {
                            if (roomHandler2.hasEverBeenVisited && roomHandler2.distanceFromEntrance < roomHandler.distanceFromEntrance && !list.Contains(roomHandler2))
                            {
                                if (!roomHandler2.area.IsProceduralRoom || roomHandler2.area.proceduralCells == null)
                                {
                                    list2.Add(roomHandler2);
                                }
                            }
                        }
                        if (list2.Count == 0)
                        {
                            break;
                        }
                        list.Add(BraveUtility.RandomElement<RoomHandler>(list2));
                    }
                    if (list.Count > 1)
                    {
                        base.Owner.RespawnInPreviousRoom(false, PlayerController.EscapeSealedRoomStyle.GRIP_MASTER, true, list[list.Count - 1]);
                        for (int i = 1; i < list.Count - 1; i++)
                        {
                            list[i].ResetPredefinedRoomLikeDarkSouls();
                        }
                    }
                    else
                    {
                        base.Owner.RespawnInPreviousRoom(false, PlayerController.EscapeSealedRoomStyle.GRIP_MASTER, true, null);
                    }
                    base.Owner.specRigidbody.Velocity = Vector2.zero;
                    base.Owner.knockbackDoer.TriggerTemporaryKnockbackInvulnerability(1f);
                }
            }            
        }
        */
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnHitByProjectile -= this.OwnerHitByProjectile;
            //Tools.Print($"Player dropped {this.DisplayName}");
            return base.Drop(player);
        }
    }
}