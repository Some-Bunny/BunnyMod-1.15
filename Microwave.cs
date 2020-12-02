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
	public class Microwave : GunBehaviour
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000037D8 File Offset: 0x000019D8
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Microwave", "microwave");
			Game.Items.Rename("outdated_gun_mods:microwave", "bny:microwave");
			gun.gameObject.AddComponent<Microwave>();
			GunExt.SetShortDescription(gun, "Cook for 10 Seconds");
			GunExt.SetLongDescription(gun, "A whole microwave thats been left carelessly laying around in the Gungeon. What monster would do this?");
			GunExt.SetupSprite(gun, null, "microwave_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 18);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, (int)0.01f);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 4);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(35) as Gun, true, false);
			//gun.gunSwitchGroup = (PickupObjectDatabase.GetById(377) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 10f;
			gun.DefaultModule.cooldownTime = .01f;
			gun.DefaultModule.numberOfShotsInClip = 1;
			gun.SetBaseMaxAmmo(60);
			gun.quality = PickupObject.ItemQuality.B;
			gun.DefaultModule.angleVariance = 7f;
			gun.DefaultModule.burstShotCount = 1;
			gun.encounterTrackable.EncounterGuid = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			MicrowaveModifier reaveonReloadModifier = gun.gameObject.AddComponent<MicrowaveModifier>();
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage = 0f;
			projectile.baseData.speed = 1f;
			projectile.baseData.range = 0f;
			projectile.transform.parent = gun.barrelOffset;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
		}
		private bool HasReloaded;


		public override void OnPostFired(PlayerController owner, Gun gun)
		{
			AkSoundEngine.PostEvent("Play_BOSS_Punchout_Punch_Hit_01", gameObject);
			gun.PreventNormalFireAudio = true;
		}

		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				HasReloaded = false;
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);
				//AkSoundEngine.PostEvent("Play_WPN_plasmacell_reload_01", base.gameObject);
			}
		}
	}
}
