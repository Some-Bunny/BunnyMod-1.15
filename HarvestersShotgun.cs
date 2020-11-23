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
using System.Timers;




namespace BunnyMod
{
	public class HarvestersShotgun : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Harvesters Shotgun", "harvestershotgun");
			Game.Items.Rename("outdated_gun_mods:harvesters_shotgun", "bny:harvesters_shotgun");
			gun.gameObject.AddComponent<HarvestersShotgun>();
			GunExt.SetShortDescription(gun, "Reap What You Slay.");
			GunExt.SetLongDescription(gun, "An antique shotgun used by long-gone Shot-Gunreapers for the job of transporting souls to Bullet Hell. Demoralizing, but pays well.\n\nAnother night...");
			GunExt.SetupSprite(gun, null, "harvestershotgun_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 15);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 6);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			for (int i = 0; i < 4; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(1) as Gun, true, false);
				gun.gunSwitchGroup = (PickupObjectDatabase.GetById(1) as Gun).gunSwitchGroup;

			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 0.4f;
				projectileModule.angleVariance = 8f;
				projectileModule.numberOfShotsInClip = 6;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				projectileModule.projectiles[0] = projectile;
				projectile.baseData.damage = 6.25f;
				projectile.AdditionalScaleMultiplier = 1f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				//projectile.SetProjectileSpriteRight("captainsshotgun_projectile_001", 11, 3, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(3), null, null, null);
				gun.DefaultModule.projectiles[0] = projectile;
				bool flag = projectileModule != gun.DefaultModule;
				if (flag)
				{
					projectileModule.ammoCost = 0;
				}
				projectile.transform.parent = gun.barrelOffset;
			}
			gun.reloadTime = 1.6f;
			gun.SetBaseMaxAmmo(300);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(93) as Gun).muzzleFlashEffects;
			gun.quality = PickupObject.ItemQuality.B;
			gun.carryPixelOffset = new IntVector2((int)6f, (int)0f);
			gun.encounterTrackable.EncounterGuid = "EXECUTED WITH IMPUNITY";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}



        public override void PostProcessProjectile(Projectile projectile)
        {
            projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.OnProjectileHitEnemy));
        }

        public void OnProjectileHitEnemy(Projectile proj, SpeculativeRigidbody enemy, bool fatal)
        {
            if (enemy != null)
            {
                AIActor aiActor = enemy.aiActor;
                if (aiActor != null && this.gun && this.gun.CurrentOwner && kills <= 0 && fatal)
                {
                    killStreakTimer = new System.Timers.Timer(250);
                    killStreakTimer.Elapsed += OnTimedEvent;
                    killStreakTimer.Start();
                    oldKills = kills;
                    kills++;
                }
                else if (fatal)
                {
                    oldKills = kills;
                    kills++;
                }

            }
        }


        private static void OnTimedEvent(System.Object source, System.Timers.ElapsedEventArgs e)
        {
            if (oldKills < kills)
            {
                //Tools.Print($"Killstreak {kills}", "FFFFFF", true);
                numberOfTimesKillsEqhalsOldKills = 0;
                oldKills = kills;
            }
            else if (kills == oldKills && numberOfTimesKillsEqhalsOldKills <= 16)
            {
                numberOfTimesKillsEqhalsOldKills++;
            }
            else
            {
                //Tools.Print($"Killstreak ended", "FFFFFF", true);
                oldKills = 0;
                kills = 0;
                numberOfTimesKillsEqhalsOldKills = 0;
                killStreakTimer.Stop();
            }
        }

        protected void Update()
        {
            if (kills <= 0 && firstKillStreakBoostActive)
            {
                AkSoundEngine.PostEvent("Play_WPN_Life_Orb_Blast_01", gameObject);
                firstKillStreakBoostActive = false;
                //Tools.Print($"Killstreak Remove Boost", "FFFFFF", true);
                //gun.AddCurrentGunStatModifier(PlayerStats.StatType.Damage, -0.5f, StatModifier.ModifyMethod.ADDITIVE);
                PlayerController owner = (gun.CurrentOwner as PlayerController);
                //owner.stats.RecalculateStats(owner, false);

            }
            else if (kills >= 4 && !firstKillStreakBoostActive)
            {
                PlayerController player = this.gun.CurrentOwner as PlayerController;
                for (int counter = 0; counter < 5; counter++)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, player);
                }
                firstKillStreakBoostActive = true;
                PlayerController owner = (gun.CurrentOwner as PlayerController);
                owner.stats.RecalculateStats(owner, false);
            }
            else if (kills >= 9 && !secondKillStreakBoostActive)
            {
                PlayerController player = this.gun.CurrentOwner as PlayerController;
                for (int counter = 0; counter < 10; counter++)
                {
                    LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(68).gameObject, player);
                }
                secondKillStreakBoostActive = true;
                PlayerController owner = (gun.CurrentOwner as PlayerController);
                owner.stats.RecalculateStats(owner, false);
            }
            else if (kills >= 19 && !thrindKillStreakBoostActive)
            {
                PlayerController player = this.gun.CurrentOwner as PlayerController;
                int num3 = UnityEngine.Random.Range(0, 3);
                bool flag3 = num3 == 0;
                if (flag3)
                {
                    LootEngine.SpawnItem(PickupObjectDatabase.GetById(224).gameObject, player.specRigidbody.UnitCenter, Vector2.up, 0f, false, true, false);
                }
                bool flag4 = num3 == 1;
                if (flag4)
                {
                    LootEngine.SpawnItem(PickupObjectDatabase.GetById(67).gameObject, player.specRigidbody.UnitCenter, Vector2.up, 0f, false, true, false);
                }
                bool flag6 = num3 == 2;
                if (flag6)
                {
                    LootEngine.SpawnItem(PickupObjectDatabase.GetById(78).gameObject, player.specRigidbody.UnitCenter, Vector2.up, 0f, false, true, false);
                }
                thrindKillStreakBoostActive = true;
                PlayerController owner = (gun.CurrentOwner as PlayerController);
                owner.stats.RecalculateStats(owner, false);
            }
            Material outlineMaterial = SpriteOutlineManager.GetOutlineMaterial(gun.sprite);
            outlineMaterial.SetColor("_OverrideColor", new Color(10.5f * kills, 9.4f * kills, 8.6f * kills));
            //this.Update();
        }

        private static int oldKills = 0;
        private static int kills = 0;
        private static int numberOfTimesKillsEqhalsOldKills = 0;
        private static Timer killStreakTimer;
        private bool firstKillStreakBoostActive = false;
        private bool secondKillStreakBoostActive = false;
        private bool thrindKillStreakBoostActive = false;
    }
}
