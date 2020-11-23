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
	public class IDPDFreakGun : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Mutated Rifle", "idpdfreak");
			Game.Items.Rename("outdated_gun_mods:mutated_rifle", "bny:mutated_rifle");
			gun.gameObject.AddComponent<IDPDFreakGun>();
			GunExt.SetShortDescription(gun, "L3+");
			GunExt.SetLongDescription(gun, "A rifle from a wasteland dimension so radioactive that the gun has started to mutate.\n\nFlashyn!");
			GunExt.SetupSprite(gun, null, "idpdfreak_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 16);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(2) as Gun, true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.barrelOffset.transform.localPosition = new Vector3(1f, 0.3125f, 0f);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0.6f;
			//gun.barrelOffset.transform.localPosition = new Vector3(1f, 0.3125f, 0f);
			gun.DefaultModule.cooldownTime = 0.01f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 60;
			gun.SetBaseMaxAmmo(900);
			gun.DefaultModule.angleVariance = 40f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(223) as Gun).muzzleFlashEffects;
			projectile.baseData.damage *= 1.1f;
			projectile.baseData.speed *= 1.2f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.SetProjectileSpriteRight("idpdfreak_projectile_001", 11, 4, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(2), null, null, null);
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			gun.quality = PickupObject.ItemQuality.B;
			gun.encounterTrackable.EncounterGuid = "BWAAAHAAALARRHHHHHH";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D693 File Offset: 0x0000B893
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_plasmarifle_impact_01", base.gameObject);
		}
	}
}
