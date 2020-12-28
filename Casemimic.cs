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
	public class Casemimic : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Casemimic", "casemimic");
			Game.Items.Rename("outdated_gun_mods:casemimic", "bny:casemimic");
			gun.gameObject.AddComponent<RogueLegendary>();
			GunExt.SetShortDescription(gun, "Batting .50");
			GunExt.SetLongDescription(gun, "Infinite ammo. Does not reveal secret walls. A standard baseball bat, modified to hit bullets instead of balls. Launches enemy projectiles back with a vengeance!\n\nNothi- Hey hold on a second.");
			GunExt.SetupSprite(gun, null, "casemimic_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 30);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 100);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(404) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(541) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.barrelOffset.transform.localPosition = new Vector3(1f, 0.3125f, 0f);
			gun.carryPixelOffset += new IntVector2((int)12f, (int)0f);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 0f;
			gun.DefaultModule.cooldownTime = 0.1f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 6;
			gun.SetBaseMaxAmmo(300);
			gun.DefaultModule.angleVariance = 10f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.baseData.damage *= 1.2f;
			projectile.baseData.speed *= 1f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			gun.quality = PickupObject.ItemQuality.D;
			gun.encounterTrackable.EncounterGuid = "you absolute fool, you were PRANKED!!! by the CASEY MIMIC auegh";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			Casemimic.CasemimicID = gun.PickupObjectId;

		}
		public static int CasemimicID;

		protected void Update()
		{
			PlayerController player = this.gun.CurrentOwner as PlayerController;
			bool flag4 =  !player;
			if (flag4)
			{
				Vector3 vector = this.gun.sprite.WorldBottomLeft.ToVector3ZisY(0f);
				Vector3 vector2 = this.gun.sprite.WorldTopRight.ToVector3ZisY(0f);
				float num = (vector2.y - vector.y) * (vector2.x - vector.x);
				float num2 = 25f * num;
				int num3 = Mathf.CeilToInt(Mathf.Max(1f, num2 * BraveTime.DeltaTime));
				int num4 = num3;
				Vector3 minPosition = vector;
				Vector3 maxPosition = vector2;
				Vector3 direction = Vector3.up / 2f;
				float angleVariance = 120f;
				float magnitudeVariance = 0.2f;
				float? startLifetime = new float?(UnityEngine.Random.Range(0.8f, 1.25f));
				GlobalSparksDoer.DoRandomParticleBurst(num4, minPosition, maxPosition, direction, angleVariance, magnitudeVariance, null, startLifetime, null, GlobalSparksDoer.SparksType.BLACK_PHANTOM_SMOKE);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000D693 File Offset: 0x0000B893
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			gun.PreventNormalFireAudio = false;
		}
	}
}
