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
	public class MatterTranslocator : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun flakcannon = ETGMod.Databases.Items.NewGun("Matter Translocator", "mattertranslocator");
			Game.Items.Rename("outdated_gun_mods:matter_translocator", "bny:matter_translocator");
			flakcannon.gameObject.AddComponent<MatterTranslocator>();
			GunExt.SetShortDescription(flakcannon, "Tele-Tech");
			GunExt.SetLongDescription(flakcannon, "The most stable and well-calibrated portable teleporter available for use. As with all things, it's weaponized.");
			GunExt.SetupSprite(flakcannon, null, "mattertranslocator_idle_001", 11);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.shootAnimation, 15);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.reloadAnimation, 12);
			GunExt.SetAnimationFPS(flakcannon, flakcannon.idleAnimation, 5);
			GunExt.AddProjectileModuleFrom(flakcannon, "magnum", true, false);
			flakcannon.barrelOffset.transform.localPosition = new Vector3(2.3125f, 0.625f, 0f);
			flakcannon.DefaultModule.ammoCost = 1;
			flakcannon.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			flakcannon.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			flakcannon.reloadTime = 3f;
			flakcannon.DefaultModule.cooldownTime = .1f;
			flakcannon.DefaultModule.numberOfShotsInClip = 1;
			flakcannon.SetBaseMaxAmmo(30);
			flakcannon.quality = PickupObject.ItemQuality.A;
			flakcannon.DefaultModule.angleVariance = 0f;
			flakcannon.DefaultModule.burstShotCount = 1;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(flakcannon.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			flakcannon.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 60f;
			projectile.baseData.speed *= .35f;
			projectile.AdditionalScaleMultiplier = 0.85f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.baseData.range = 9f;
			projectile.SetProjectileSpriteRight("mattertranslocator_projectile_001", 22, 22, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(30), new int?(30), null, null, null);
			BounceProjModifier bouncy = projectile.gameObject.AddComponent<BounceProjModifier>();
			bouncy.numberOfBounces = 100;
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 100;
			flakcannon.encounterTrackable.EncounterGuid = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
			ETGMod.Databases.Items.Add(flakcannon, null, "ANY");
			flakcannon.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			flakcannon.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);

		}

		public override void OnPostFired(PlayerController player, Gun flakcannon)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_BOSS_RatMech_Cannon_01", base.gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun flakcannon, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_plasmacell_reload_01", gameObject);
			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnDestruction += this.Warp;
		}

		private void Warp(Projectile projectile)
		{
			PlayerController player = (GameManager.Instance.PrimaryPlayer);
			GameObject silencerVFX = (GameObject)ResourceCache.Acquire("Global VFX/BlankVFX_Ghost");
			RoomHandler currentRoom = player.CurrentRoom;
			if (currentRoom != null)
			{
				Vector2 playerPosition = this.gun.CurrentOwner.transform.position;
				Vector2 projPosition = projectile.transform.position;
				CellData cellAim = currentRoom.GetNearestCellToPosition(projPosition);
				CellData cellAimLeft = currentRoom.GetNearestCellToPosition(projPosition + Vector2.left);
				CellData cellAimRight = currentRoom.GetNearestCellToPosition(projPosition + Vector2.right);
				CellData cellAimUp = currentRoom.GetNearestCellToPosition(projPosition + Vector2.up);
				CellData cellAimDown = currentRoom.GetNearestCellToPosition(projPosition + Vector2.down);
				if (player.IsValidPlayerPosition(projPosition) && !cellAim.isNextToWall && !cellAimLeft.isNextToWall && !cellAimRight.isNextToWall && !cellAimUp.isNextToWall && !cellAimDown.isNextToWall)
				{
					GameManager.Instance.StartCoroutine(this.Warplol(player, playerPosition, projPosition));
				}
			}
			AkSoundEngine.PostEvent("Play_OBJ_lightning_flash_01", base.gameObject);
			for (int counter = 0; counter < 12; counter++)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				Projectile projectile1 = ((Gun)ETGMod.Databases.Items[390]).DefaultModule.projectiles[0];
				GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, 30f * counter));
				Projectile component = gameObject.GetComponent<Projectile>();
				bool flag = component != null;
				bool flag2 = flag;
				if (flag2)
				{
					component.SpawnedFromOtherPlayerProjectile = true;
					component.Shooter = this.gun.CurrentOwner.specRigidbody;
					component.Owner = playerController;
					component.Shooter = playerController.specRigidbody;
					component.baseData.speed = 30f;
					component.AdditionalScaleMultiplier = 0.25f;
					component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
					component.ignoreDamageCaps = true;
					component.baseData.damage *= 0.5f;

				}
			}
		}
		private IEnumerator Warplol(PlayerController player, Vector2 playerPosition, Vector2 projPosition)
		{
			StartCoroutine(HandleShield(player));
			//AkSoundEngine.PostEvent("Play_OBJ_teleport_depart_01", gameObject);
			GameObject obj = new GameObject();
			HealthHaver fuck = obj.AddComponent<HealthHaver>();
			yield return new WaitForSeconds(0.1f);
			//Vector2 vector2 = projectile.sprite.WorldCenter;
			(player as PlayerController).WarpToPoint(projPosition);
			yield return new WaitForSeconds(0.12f);
			SpawnManager.SpawnVFX((PickupObjectDatabase.GetById(573) as ChestTeleporterItem).TeleportVFX, player.sprite.WorldCenter.ToVector3ZisY(0f), Quaternion.identity).GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor(player.sprite.WorldCenter.ToVector3ZisY(0f), tk2dBaseSprite.Anchor.MiddleCenter);

			yield break;
		}
		float m_activeDuration = 1f;
		float duration = 1f;
		private IEnumerator HandleShield(PlayerController user)
		{
			//IsCurrentlyActive = true;
			//float m_activeElapsed = 0f;
			m_activeDuration = this.duration;
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
				AkSoundEngine.PostEvent("Play_OBJ_metalskin_end_01", base.gameObject);
			}
			yield break;
		}
	}
}