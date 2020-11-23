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
	// Token: 0x0200002D RID: 45
	public class Gunthemimic : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Gunthemimic", "Gunthemimic");
			Game.Items.Rename("outdated_gun_mods:gunthemimic", "bny:gunthemimic");
			gun.gameObject.AddComponent<Gunthemimic>();
			GunExt.SetShortDescription(gun, "Jealous Weapon");
			GunExt.SetLongDescription(gun, "Infinite ammo. Does not reveal secret walls. Grows more powerful with each room cleared?\n\nThis totally trustful gun has gained sentience. As with all intelligent weapons, he should be treated with res- hold on, what?");
			GunExt.SetupSprite(gun, null, "Gunthemimic_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 32);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 10);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 2);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(338) as Gun, true, false);
			gun.DefaultModule.ammoCost = 1;
			//gun.barrelOffset.transform.localPosition = new Vector3(1f, 0.3125f, 0f);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0.9f;
			gun.DefaultModule.cooldownTime = 0.125f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 20;
			gun.SetBaseMaxAmmo(700);
			gun.DefaultModule.angleVariance = 7f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(333) as Gun).muzzleFlashEffects;
			projectile.baseData.damage = 11f;
			projectile.baseData.speed *= 0.9f;
			projectile.pierceMinorBreakables = true;
			HomingModifier homing = projectile.gameObject.AddComponent<HomingModifier>();
			homing.HomingRadius = 25f;
			homing.AngularVelocity = 5;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			gun.quality = PickupObject.ItemQuality.S;
			gun.encounterTrackable.EncounterGuid = "NOM NOM NOM NOM NOM NOM NOM NOM NOM NOM NOM  BLALALALALALALAAA";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D693 File Offset: 0x0000B893
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_mailbox_impact_01", base.gameObject);
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.baseData.speed = 24f;
			base.StartCoroutine(this.Speed(projectile));
		}
		public IEnumerator Speed(Projectile projectile)
		{
			bool flag = this.gun.CurrentOwner;
			bool flag3 = flag;
			if (flag3)
			{
				for (int counter = 0; counter < 5; counter++)
                {
					yield return new WaitForSeconds(0.1f);
					{
						projectile.baseData.speed *= .5f;
						projectile.UpdateSpeed();
					}
				}
				yield return new WaitForSeconds(0.2f);
				projectile.baseData.speed = 24f;
				projectile.UpdateSpeed();
				base.StartCoroutine(this.Speed(projectile));
			}
			yield break;
		}
		private void LateUpdate()
		{
			bool flag = this.gun && this.gun.IsReloading && this.gun.CurrentOwner is PlayerController;
			if (flag)
			{
				bool Nom = gun.ClipShotsRemaining < gun.ClipCapacity / 2;
				if (Nom)
                {
					PlayerController playerController = this.gun.CurrentOwner as PlayerController;
					bool flag2 = playerController.CurrentRoom != null;
					if (flag2)
					{
						for (int counter = 0; counter < 1; counter++)
						{

							playerController.CurrentRoom.ApplyActionToNearbyEnemies(playerController.CenterPosition, 4f, new Action<AIActor, float>(this.DEVOUR));
						}

					}
				}
			}
		}
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_BOSS_doormimic_lick_02", base.gameObject);

			}
		}
		private bool HasReloaded;

		private void DEVOUR(AIActor target, float distance)
		{
			{
				GameManager.Instance.Dungeon.StartCoroutine(this.HandleEnemySuck(target));
				target.EraseFromExistence(true);
			}
		}
		private IEnumerator HandleEnemySuck(AIActor target)
		{
			PlayerController playerController = this.gun.CurrentOwner as PlayerController;
			Transform copySprite = this.CreateEmptySprite(target);
			Vector3 startPosition = copySprite.transform.position;
			float elapsed = 0f;
			float duration = 0.3f;
			while (elapsed < duration)
			{
				elapsed += BraveTime.DeltaTime;
				bool TRESS = playerController.CurrentGun && copySprite;
				if (TRESS)
				{
					Vector3 position = playerController.CurrentGun.PrimaryHandAttachPoint.position;
					float t = elapsed / duration * (elapsed / duration);
					copySprite.position = Vector3.Lerp(startPosition, position, t);
					copySprite.rotation = Quaternion.Euler(0f, 0f, 360f * BraveTime.DeltaTime) * copySprite.rotation;
					copySprite.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.1f, 0.1f, 0.1f), t);
					position = default(Vector3);
				}
				yield return null;
			}
			bool flag4 = copySprite;
			if (flag4)
			{
				UnityEngine.Object.Destroy(copySprite.gameObject);
			}
			bool flag3 = this.gun;
			if (flag3)
			{
				this.gun.GainAmmo(2);
			}
			yield break;
		}
		private Transform CreateEmptySprite(AIActor target)
		{
			GameObject gameObject = new GameObject("suck image");
			gameObject.layer = target.gameObject.layer;
			tk2dSprite tk2dSprite = gameObject.AddComponent<tk2dSprite>();
			gameObject.transform.parent = SpawnManager.Instance.VFX;
			tk2dSprite.SetSprite(target.sprite.Collection, target.sprite.spriteId);
			tk2dSprite.transform.position = target.sprite.transform.position;
			GameObject gameObject2 = new GameObject("image parent");
			gameObject2.transform.position = tk2dSprite.WorldCenter;
			tk2dSprite.transform.parent = gameObject2.transform;
			bool flag = target.optionalPalette != null;
			if (flag)
			{
				tk2dSprite.renderer.material.SetTexture("_PaletteTex", target.optionalPalette);
			}
			return gameObject2.transform;
		}
	}
}


