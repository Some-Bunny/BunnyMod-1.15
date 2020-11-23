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
	public class PocketPistol : GunBehaviour
	{
		public static void Add()
		{
			Gun pocketpistol = ETGMod.Databases.Items.NewGun("Pocket Pistol", "pocketpistol");
			Game.Items.Rename("outdated_gun_mods:pocket_pistol", "bny:pocket_pistol");
			pocketpistol.gameObject.AddComponent<PocketPistol>();
			GunExt.SetShortDescription(pocketpistol, "Happy to see me?");
			GunExt.SetLongDescription(pocketpistol, "An incredibly small firearm. How do bullets even fit in there?");
			GunExt.SetupSprite(pocketpistol, null, "pocketpistol_idle_001", 19);
			GunExt.SetAnimationFPS(pocketpistol, pocketpistol.shootAnimation, 15);
			GunExt.SetAnimationFPS(pocketpistol, pocketpistol.reloadAnimation, 7);
			GunExt.SetAnimationFPS(pocketpistol, pocketpistol.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(pocketpistol, "magnum", true, false);
			pocketpistol.gunSwitchGroup = (PickupObjectDatabase.GetById(79) as Gun).gunSwitchGroup;
			pocketpistol.DefaultModule.ammoCost = 1;
			pocketpistol.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			pocketpistol.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			pocketpistol.reloadTime = 0.9f;
			pocketpistol.barrelOffset.transform.localPosition = new Vector3(.5f, 0.25f, 0f);
			pocketpistol.DefaultModule.cooldownTime = .3f;
			pocketpistol.DefaultModule.numberOfShotsInClip = 6;
			pocketpistol.SetBaseMaxAmmo(327);
			pocketpistol.quality = PickupObject.ItemQuality.D;
			pocketpistol.DefaultModule.angleVariance = 4f;
			pocketpistol.AddPassiveStatModifier(PlayerStats.StatType.AmmoCapacityMultiplier, .1f, StatModifier.ModifyMethod.ADDITIVE);
			pocketpistol.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
			pocketpistol.encounterTrackable.EncounterGuid = "The Small Gun Hell Yeah Brother";
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(pocketpistol.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			pocketpistol.DefaultModule.projectiles[0] = projectile;
			projectile.shouldRotate = true;
			projectile.baseData.damage = 7.5f;
			projectile.baseData.speed *= 1.5f;
			projectile.AdditionalScaleMultiplier = 0.7f;
			projectile.transform.parent = pocketpistol.barrelOffset;
			ETGMod.Databases.Items.Add(pocketpistol, null, "ANY");
			List<string> mandatoryConsoleIDs1 = new List<string>
			{
				"bny:pocket_pistol"
			};
			List<string> optionalConsoleID1s = new List<string>
			{
				"stout_bullets",
				"fat_bullets",
				"+1_bullets",
				"flak_bullets"
			};
			CustomSynergies.Add("How do these even fit in?", mandatoryConsoleIDs1, optionalConsoleID1s, true);
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = this.gun.CurrentOwner as PlayerController;
			bool flagA = player.PlayerHasActiveSynergy("How do these even fit in?");
			if (flagA)
			{
				projectile.baseData.damage *= 1.2f;
				projectile.AdditionalScaleMultiplier *= 2f;
			}
		}
		private bool HasReloaded;

		protected void Update()
		{
			bool flag = this.gun.CurrentOwner;
			if (flag)
			{
				bool flag2 = !this.gun.IsReloading && !this.HasReloaded;
				if (flag2)
				{
					this.HasReloaded = true;
				}
			}
		}
	}
}