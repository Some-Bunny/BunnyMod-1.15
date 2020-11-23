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
	public class RogueLegendary : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Rogue Legendary", "roguelegendary");
			Game.Items.Rename("outdated_gun_mods:rogue_legendary", "bny:rogue_legendary");
			gun.gameObject.AddComponent<RogueLegendary>();
			GunExt.SetShortDescription(gun, "Overhanded And Efficient");
			GunExt.SetLongDescription(gun, "The Pilot got pissed off at everyone complaining about his pistol, so he custom built this weapon out of two-dozen Rogue Specials.");
			GunExt.SetupSprite(gun, null, "roguelegendary_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 60);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 3);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(89) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(89) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.barrelOffset.transform.localPosition = new Vector3(1f, 0.3125f, 0f);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0.9f;
			gun.DefaultModule.cooldownTime = 0.1f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 30;
			gun.SetBaseMaxAmmo(600);
			gun.DefaultModule.angleVariance = 20f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(89) as Gun).muzzleFlashEffects;
			projectile.baseData.damage *= 2.5f;
			projectile.baseData.speed *= 1f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			gun.quality = PickupObject.ItemQuality.S;
			gun.encounterTrackable.EncounterGuid = "They say you're legendary? Im legendary-er!!!";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D693 File Offset: 0x0000B893
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = false;
		}
	}
}
