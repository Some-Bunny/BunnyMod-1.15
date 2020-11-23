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
	public class ChaosMalice : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Chaos Malice", "chaosmalice");
			Game.Items.Rename("outdated_gun_mods:chaos_malice", "bny:chaos_malice");
			gun.gameObject.AddComponent<ChaosMalice>();
			GunExt.SetShortDescription(gun, "Mutated");
			GunExt.SetLongDescription(gun, "A horrific failure at an attempt to recreate a weapon. It painfully calls out to its bretheren...");
			GunExt.SetupSprite(gun, null, "chaosmalice_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 2);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(670) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(539) as Gun).gunSwitchGroup;
			gun.carryPixelOffset = new IntVector2((int)5f, (int)-1f);
			gun.gunHandedness = GunHandedness.HiddenOneHanded;
			//gun.InfiniteAmmo = false;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0f;
			gun.DefaultModule.cooldownTime = .1f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(400);
			gun.quality = PickupObject.ItemQuality.EXCLUDED;
			gun.DefaultModule.angleVariance = 13f;
			gun.encounterTrackable.EncounterGuid = "https://youtu.be/snshwCV06Dw";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 10;
			gun.DefaultModule.projectiles[0] = projectile;
			//projectile.sprite.renderer.enabled = false;
			projectile.AdditionalScaleMultiplier = .3f;
			projectile.baseData.damage = 16f;
			projectile.baseData.speed *= 1.2f;
			projectile.baseData.range = 100f;
			projectile.AdditionalScaleMultiplier = 0.7f;
			projectile.pierceMinorBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			ChaosMalice.MaliceID = gun.PickupObjectId;
		}
		public override void OnPostFired(PlayerController player, Gun staffyeah)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_PET_junk_slash_01", gameObject);
		}
		private bool HasReloaded;

		protected void Update()
		{
			if (gun.CurrentOwner)
			{

				if (!gun.PreventNormalFireAudio)
				{
					this.gun.PreventNormalFireAudio = true;
				}
				if (!gun.IsReloading && !HasReloaded)
				{
					this.HasReloaded = true;
				}
			}
		}
		public override void OnReloadPressed(PlayerController player, Gun staffyeah, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
			}
		}

		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnWillKillEnemy = (Action<Projectile, SpeculativeRigidbody>)Delegate.Combine(projectile.OnWillKillEnemy, new Action<Projectile, SpeculativeRigidbody>(this.OnKill));
		}
		private void OnKill(Projectile arg1, SpeculativeRigidbody arg2)
		{
			bool flag = !arg2.aiActor.healthHaver.IsDead;
			if (flag)
			{
				int num3 = UnityEngine.Random.Range(0, 4);
				bool flag3 = num3 == 0;
				if (flag3)
                {
					AkSoundEngine.PostEvent("Play_OBJ_chestwarp_use_01", gameObject);
					PlayerController player = (GameManager.Instance.PrimaryPlayer);
					string guid;
					guid = "ChaosBeing";
					PlayerController owner = player;
					AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
					IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
					AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
					aiactor.CanTargetEnemies = false;
					aiactor.CanTargetPlayers = true;
					PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
					aiactor.IsHarmlessEnemy = false;
					aiactor.IgnoreForRoomClear = true;
					aiactor.HandleReinforcementFallIntoRoom(-1f);
					SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, aiactor.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(aiactor.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);
				}		
			}
		}
		
		public static int MaliceID;
		public GunHandedness HiddenOneHanded;
	}
}
