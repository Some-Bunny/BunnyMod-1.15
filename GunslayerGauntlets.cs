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
	public class GunslayerGauntlets : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Gunslayers Gauntlet", "gauntlets");
			Game.Items.Rename("outdated_gun_mods:gunslayers_gauntlet", "bny:gunslayers_gauntlet");
			gun.gameObject.AddComponent<GunslayerGauntlets>();
			GunExt.SetShortDescription(gun, "Rip And Tear");
			GunExt.SetLongDescription(gun, "The Gauntlet of the Gunslayer. In most circumstances, these would only be used to bear the burden of the Slayers weapons, but if needed to...");
			GunExt.SetupSprite(gun, null, "gauntlets_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 36);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 20);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 2);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(539) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(539) as Gun).gunSwitchGroup;
			gun.carryPixelOffset = new IntVector2((int)11f, (int)-1f);
			gun.gunHandedness = GunHandedness.HiddenOneHanded;
			gun.InfiniteAmmo = true;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0f;
			gun.DefaultModule.cooldownTime = .4f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(50);
			gun.quality = PickupObject.ItemQuality.C;
			gun.DefaultModule.angleVariance = 0f;
			gun.encounterTrackable.EncounterGuid = "RIP AND TEAR UNTIL IT IS DONE. *BAW NAW NANANANAU NANANAU BANAW NANANAUUU*";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			PierceProjModifier spook = projectile.gameObject.AddComponent<PierceProjModifier>();
			spook.penetration = 10;
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.sprite.renderer.enabled = false;
			projectile.AdditionalScaleMultiplier = 3f;
			projectile.baseData.damage = 20f;
			projectile.baseData.speed *= 3f;
			projectile.baseData.range = 2f;
			projectile.AdditionalScaleMultiplier = 0.7f;
			projectile.pierceMinorBreakables = true;
			projectile.shouldRotate = true;
			projectile.AppliesStun = true;
			projectile.AppliedStunDuration = 1.2f;
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			gun.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);

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
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
			Vector2 direction = Quaternion.Euler(0f, 0f, playerController.CurrentGun.CurrentAngle) * -Vector2.right;
			playerController.knockbackDoer.ApplyKnockback(direction, -8f, false);
			projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.HandleHit));
		}
		private void FireBlob(Projectile projectile)
        {
			AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
			GoopDefinition goopDef = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
			DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDef).TimedAddGoopCircle(projectile.sprite.WorldBottomCenter, 1.5f, 1.4f, false);
		}
		private void HandleHit(Projectile arg1, SpeculativeRigidbody arg2, bool arg3)
		{
			float currentHealthPercentage1 = arg2.healthHaver.GetCurrentHealthPercentage();
			bool flag3 = currentHealthPercentage1 <= 0.9f;
			if (flag3)
			{
				arg2.healthHaver.ApplyDamage(20f, Vector2.zero, "Telefrag", CoreDamageTypes.Void, DamageCategory.Normal, true, null, false);
			}
			float currentHealthPercentage = arg2.healthHaver.GetCurrentHealthPercentage();
			bool flaga3 = currentHealthPercentage <= 0.3f;
			bool isBoss = arg2.healthHaver.IsBoss;
			bool flag4 = flaga3 && !isBoss;
			if (flag4)
			{
				AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
				GoopDefinition goopDef = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
				DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDef).TimedAddGoopCircle(arg2.sprite.WorldBottomCenter, 1.5f, 1.4f, false);
				this.Execute(arg2);
			}
		}
		private void Execute(SpeculativeRigidbody AIactor)
		{
			AkSoundEngine.PostEvent("Play_BOSS_wall_slam_01", gameObject);
			AIactor.healthHaver.ApplyDamage(100000f, Vector2.zero, "Telefrag", CoreDamageTypes.Void, DamageCategory.Normal, true, null, false);
		}
		public GunHandedness HiddenOneHanded;
	}
}
