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
	public class BigNukeGun : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Nuclear Armageddon", "nucleararmageddon");
			Game.Items.Rename("outdated_gun_mods:nuclear_armageddon", "bny:nuclear_armageddon");
			gun.gameObject.AddComponent<BigNukeGun>();
			GunExt.SetShortDescription(gun, "End of the Gungeon");
			GunExt.SetLongDescription(gun, "An incredibly powerful nuclear weapon that was thrown into the Gungeon after soldiers testing it reported it nearly killing them.\n\nOnce it's fired, you've made your decision.");
			GunExt.SetupSprite(gun, null, "nucleararmageddon_idle_001", 26);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 40);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 8);
			GunExt.AddProjectileModuleFrom(gun, "rpg", true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 12f;
			gun.DefaultModule.cooldownTime = 2f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(5);
			gun.quality = PickupObject.ItemQuality.S;
			gun.encounterTrackable.EncounterGuid = "a gigiantic ass nuclear weapon gun holy shit why isnt this illegal on every single planet oh ghd oh fuck";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage *= 1.00f;
			projectile.baseData.speed *= 0.7f;
			projectile.baseData.force = 20f;
			projectile.transform.parent = gun.barrelOffset;
			projectile.SetProjectileSpriteRight("nucleararmageddon_projectile_001", 15, 8, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(7), new int?(7), null, null, null);
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);

		}

		public override void OnPostFired(PlayerController player, Gun gun)
		{
			AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
			AkSoundEngine.PostEvent("Play_ENM_kali_blast_01", gameObject);
			gun.PreventNormalFireAudio = true;
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

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				AkSoundEngine.PostEvent("Play_WPN_plasmacell_reload_01", base.gameObject);
			}
		}

		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController playerController = this.gun.CurrentOwner as PlayerController;
			Vector2 direction = Quaternion.Euler(0f, 0f, playerController.CurrentGun.CurrentAngle) * -Vector2.right;
			playerController.knockbackDoer.ApplyKnockback(direction, 20f, false);
            bool flag = playerController == null;
			bool flag2 = flag;
			bool flag3 = flag2;
			if (flag3)
			{
				this.gun.ammo = this.gun.GetBaseMaxAmmo();
			}
			this.gun.DefaultModule.ammoCost = 1;
			projectile.baseData.range = 250f;
            projectile.OnDestruction += this.BFN;
		}
		private void BFN(Projectile projectile)
		{
			Vector2 worldCenter = projectile.sprite.WorldCenter;
			Vector3 position = projectile.sprite.WorldCenter;
			Projectile projectile1 = ((Gun)ETGMod.Databases.Items[481]).DefaultModule.chargeProjectiles[0].Projectile;
			GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, projectile.sprite.WorldCenter, Quaternion.Euler(0f, 0f, 0f));
			bool flag = projectile1 != null;
			bool flag2 = flag;
			if (flag2)
			{
				projectile1.baseData.damage = 50f;
				projectile1.baseData.speed = 0f;
				projectile1.AdditionalScaleMultiplier = 2.5f;
			}
			this.Boom(position);
			AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
			GoopDefinition goopDefinition = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
			goopDefinition.baseColor32 = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			goopDefinition.fireColor32 = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			goopDefinition.UsesGreenFire = false;
			DeadlyDeadlyGoopManager goopManagerForGoopType = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDefinition);
			goopManagerForGoopType.TimedAddGoopCircle(projectile.sprite.WorldCenter, 10f, 0.1f, false);
			this.Nuke = assetBundle.LoadAsset<GameObject>("assets/data/vfx prefabs/impact vfx/vfx_explosion_nuke.prefab");
			//this.Nuke = assetBundle.LoadAsset<GameObject>("assets/data/vfx prefabs/impact vfx/vfx_explosion_big_new.prefab");

			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Nuke);
			gameObject2.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(projectile.specRigidbody.UnitCenter, tk2dBaseSprite.Anchor.LowerCenter);
			gameObject2.transform.position = gameObject.transform.position.Quantize(0.0625f);
			gameObject2.GetComponent<tk2dBaseSprite>().UpdateZDepth();
			{
				this.FlashHoldtime = 0.1f;
				this.FlashFadetime = 0.5f;
				Pixelator.Instance.FadeToColor(this.FlashFadetime, Color.white, true, this.FlashHoldtime);
				StickyFrictionManager.Instance.RegisterCustomStickyFriction(0.15f, 1f, false, false); this.FlashHoldtime = 0.1f;
			}
		}


		// Token: 0x06000134 RID: 308 RVA: 0x0000D448 File Offset: 0x0000B648
		public void Boom(Vector3 position)
		{
			ExplosionData defaultSmallExplosionData = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
			this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData.effect;
			this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData.ignoreList;
			this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData.ss;
			Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
		}

		// Token: 0x04000172 RID: 370
		private ExplosionData smallPlayerSafeExplosion = new ExplosionData
		{
			damageRadius = 80f,
			// (:
			damageToPlayer = 2f,
			doDamage = true,
			damage = 1050f,
			doExplosionRing = false,
			doDestroyProjectiles = true,
			doForce = true,
			debrisForce = 100f,
			preventPlayerForce = false,
			explosionDelay = 0.25f,
			usesComprehensiveDelay = false,
			doScreenShake = true,
			playDefaultSFX = false
		};

		// Token: 0x04000173 RID: 371
		public float ActivationChance;

		// Token: 0x04000174 RID: 372
		public bool TriggersRadialBulletBurst;

		// Token: 0x04000175 RID: 373
		[ShowInInspectorIf("TriggersRadialBulletBurst", false)]
		public RadialBurstInterface RadialBurstSettings;

		// Token: 0x04000176 RID: 374


		// Token: 0x04000182 RID: 386
		private GameObject Nuke;

		public float FlashHoldtime;

		// Token: 0x04007BA7 RID: 31655
		public float FlashFadetime;
	}
}

				//AssetBundle assetBundle1 = ResourceManager.LoadAssetBundle("shared_auto_001");
				//this.Nuke = assetBundle1.LoadAsset<GameObject>("assets/data/vfx prefabs/vfx_explosion_firework.prefab");
				//GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.Nuke);
				//gameObject1.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(projectile.specRigidbody.UnitCenter, tk2dBaseSprite.Anchor.LowerCenter);
				//gameObject1.transform.position = gameObject.transform.position.Quantize(0.0625f);
				//gameObject1.GetComponent<tk2dBaseSprite>().UpdateZDepth();
				//im saving this for later
