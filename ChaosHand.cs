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
using System.Xml.Serialization;
using InControl;

namespace BunnyMod
{
	public class ChaosHand : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun chaoshand = ETGMod.Databases.Items.NewGun("Hand Of ChaosGod", "chaoshand");
			Game.Items.Rename("outdated_gun_mods:hand_of_chaosgod", "bny:hand_of_chaosgod");
			chaoshand.gameObject.AddComponent<ChaosHand>();
			GunExt.SetShortDescription(chaoshand, "Soul Puller");
			GunExt.SetLongDescription(chaoshand, "The replica hand of the ChaosGod, with just as much soul-tearing power.");
			GunExt.SetupSprite(chaoshand, null, "chaoshand_idle_001", 11);
			GunExt.SetAnimationFPS(chaoshand, chaoshand.shootAnimation, 15);
			GunExt.SetAnimationFPS(chaoshand, chaoshand.reloadAnimation, 12);
			GunExt.SetAnimationFPS(chaoshand, chaoshand.idleAnimation, 4);
			GunExt.AddProjectileModuleFrom(chaoshand, PickupObjectDatabase.GetById(223) as Gun, true, false);
			chaoshand.gunHandedness = GunHandedness.HiddenOneHanded;
			chaoshand.DefaultModule.ammoCost = 1;
			chaoshand.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			chaoshand.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			chaoshand.reloadTime = 1.8f;
			chaoshand.DefaultModule.cooldownTime = .4f;
			chaoshand.DefaultModule.numberOfShotsInClip = 5;
			chaoshand.SetBaseMaxAmmo(60);
			chaoshand.quality = PickupObject.ItemQuality.A;
			chaoshand.DefaultModule.angleVariance = 10f;
			chaoshand.DefaultModule.burstShotCount = 1;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(chaoshand.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			chaoshand.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 25f;
			projectile.baseData.speed *= 1.5f;
			projectile.AdditionalScaleMultiplier = 1f;
			projectile.shouldRotate = true;
			projectile.pierceMinorBreakables = true;
			projectile.baseData.range = .1f;
			chaoshand.encounterTrackable.EncounterGuid = "iuhxzdcuhdxzsuaadassadsasaassdasasasdsdsdahuzashduzlkmkj,lmkjolkjmkijnkjnhdjnhhkjashduhsaduahusai";
			ETGMod.Databases.Items.Add(chaoshand, null, "ANY");
			chaoshand.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
			chaoshand.AddToSubShop(ItemBuilder.ShopType.Goopton, 1f);
		}

		public override void OnPostFired(PlayerController player, Gun chaoshand)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_wpn_kthulu_soul_01", gameObject);
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

		public override void OnReloadPressed(PlayerController player, Gun chaoshand, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_ENM_Grip_Master_Swipe_01", base.gameObject);
			}
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			projectile.OnDestruction += SoulTear1;
		}

        private void SoulTear1(Projectile projectile)
		{

			PlayerController playerController1 = this.gun.CurrentOwner as PlayerController;
			bool isInCombat = playerController1.IsInCombat;
			if (isInCombat)
            {
				foreach (AIActor aiactor in playerController1.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
				{
					bool flag = aiactor != null;
					if (flag)
					{
                        {
							Vector3 position = aiactor.sprite.WorldCenter;
							GameObject gameObject = SpawnManager.SpawnProjectile((PickupObjectDatabase.GetById(670) as Gun).DefaultModule.projectiles[0].gameObject, position, Quaternion.Euler(0f, 0f, BraveMathCollege.Atan2Degrees(playerController1.sprite.WorldCenter - aiactor.sprite.WorldCenter)), true);
							Projectile component = gameObject.GetComponent<Projectile>();
							bool flag12 = component != null;
							bool flag2 = flag12;
							if (flag2)
							{
								PierceProjModifier spook = component.gameObject.AddComponent<PierceProjModifier>();
								spook.penetration = 10;
								component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.blue.a / 50f), 50, 0f);
								component.SpawnedFromOtherPlayerProjectile = true;
								component.Shooter = this.gun.CurrentOwner.specRigidbody;
								component.Owner = playerController1;
								component.Shooter = playerController1.specRigidbody;
								component.baseData.speed = 4f;
								component.baseData.damage = 30f;
								component.AdditionalScaleMultiplier = 0.7f;
								component.SetOwnerSafe(this.gun.CurrentOwner, "Player");
								component.ignoreDamageCaps = true;
								base.StartCoroutine(this.Speed(component));
							}
						}
					}
				}
			}
		}
		public IEnumerator Speed(Projectile component)
		{
			bool flag = this.gun.CurrentOwner;
			bool flag3 = flag;
			if (flag3)
			{
				yield return new WaitForSeconds(0.2f);
				bool flag2 = this.gun.IsReloading;
				bool flag4 = !flag2;
				if (flag4)
				{
					base.StartCoroutine(this.Speed(component));
				}
				else
				{
					component.AdjustPlayerProjectileTint(Color.blue.WithAlpha(Color.yellow.a / 25f), 50, 0f);
					component.baseData.speed = -16f;
					component.UpdateSpeed();
				}
			}
			yield break;
		}
	}
}