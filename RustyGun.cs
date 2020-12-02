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
	public class BrokenGunParts : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Broken Gun", "brokengun");
			Game.Items.Rename("outdated_gun_mods:broken_gun", "bny:broken_gun");
			gun.gameObject.AddComponent<BrokenGunParts>();
			GunExt.SetShortDescription(gun, "Uh Oh.");
			GunExt.SetLongDescription(gun, "A gun that has fallen apart completely. The parts seem to be compatible with other guns though, so I guess its got that going for it.");
			GunExt.SetupSprite(gun, null, "brokengun_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 16);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 2);
			GunExt.AddProjectileModuleFrom(gun, "magnum", true, false);
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0f;
			gun.DefaultModule.angleVariance = 0f;
			gun.DefaultModule.cooldownTime = 0f;
			gun.DefaultModule.numberOfShotsInClip = 0;
			gun.SetBaseMaxAmmo(0);
			gun.ammo = 0;
			gun.AddPassiveStatModifier(PlayerStats.StatType.Damage, .1f, StatModifier.ModifyMethod.ADDITIVE);
			gun.AddPassiveStatModifier(PlayerStats.StatType.ProjectileSpeed, .2f, StatModifier.ModifyMethod.ADDITIVE);
			gun.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, .875f, StatModifier.ModifyMethod.MULTIPLICATIVE);
			gun.AddPassiveStatModifier(PlayerStats.StatType.RangeMultiplier, 2f, StatModifier.ModifyMethod.MULTIPLICATIVE);
			gun.InfiniteAmmo = false;
			gun.quality = PickupObject.ItemQuality.C;
			gun.encounterTrackable.EncounterGuid = "pARtapartspartsppartshahgGAHAHAHAHAAHhPARTSPATRp[stypoapatrs.";
			gun.sprite.IsPerpendicular = true;
			gun.gunClass = GunClass.PISTOL;
			gun.CanGainAmmo = false;
			gun.IgnoredByRat = true;
			gun.encounterTrackable.m_doNotificationOnEncounter = false;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 0f;
			projectile.baseData.speed *= 1f;
			projectile.baseData.force *= 0f;
			projectile.baseData.range *= 0f;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A99A File Offset: 0x00008B9A
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = true;
			AkSoundEngine.PostEvent("Play_WPN_magnum_shot_01", base.gameObject);
		}
	}
}
