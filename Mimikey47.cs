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
	public class Mimikey47 : GunBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000D468 File Offset: 0x0000B668
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Mimikey47", "mimickey");
			Game.Items.Rename("outdated_gun_mods:mimikey47", "bny:mimikey47");
			gun.gameObject.AddComponent<Mimikey47>();
			GunExt.SetShortDescription(gun, "Relocked and Loaded");
			GunExt.SetLongDescription(gun, "Can't unlock chests.\n\nThe AKey-47 is a masterpiece if Mimicraft, perfectly combining the masterful gunplay of an AK-47 with the incredibly useless ability to break locks. An inscription along the frame rea- wait is that a mouth?");
			GunExt.SetupSprite(gun, null, "mimickey_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 32);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 7);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(338) as Gun, true, false);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(95) as Gun).gunSwitchGroup;
			gun.DefaultModule.ammoCost = 1;
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.reloadTime = 1.2f;
			gun.carryPixelOffset = new IntVector2((int)7f, (int)-0.6f);
			gun.DefaultModule.cooldownTime = 0.2f;
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.DefaultModule.numberOfShotsInClip = 40;
			gun.SetBaseMaxAmmo(600);
			gun.DefaultModule.angleVariance = 0f;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(338) as Gun).muzzleFlashEffects;
			projectile.baseData.damage = 10f;
			projectile.baseData.speed = 20f;
			projectile.pierceMinorBreakables = true;
			PierceProjModifier orAddComponent = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
			orAddComponent.penetratesBreakables = true;
			projectile.transform.parent = gun.barrelOffset;
			projectile.shouldRotate = true;
			projectile.SetProjectileSpriteRight("mimikey47_projectile_001", 18, 9, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(11), new int?(5), null, null, null);
			gun.quality = PickupObject.ItemQuality.S;
			gun.encounterTrackable.EncounterGuid = "MMMMYEEEESSSSSSSSSKEEEEEEEEEY";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			gun.AddToSubShop(ItemBuilder.ShopType.Flynt, 1f);

		}


		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = this.gun.CurrentOwner as PlayerController;
			bool isInCombat = player.IsInCombat;
			if (!isInCombat)
            {
				projectile.OnDestruction += OOf;
			}
		}
		public void OOf(Projectile projectile)
        {
			PlayerController player = this.gun.CurrentOwner as PlayerController;
			{
				IPlayerInteractable nearestInteractable = player.CurrentRoom.GetNearestInteractable(projectile.sprite.WorldCenter, 1f, player);
				if (nearestInteractable is InteractableLock || nearestInteractable is Chest || nearestInteractable is DungeonDoorController)
				{
					if (nearestInteractable is InteractableLock)
					{
						InteractableLock interactableLock = nearestInteractable as InteractableLock;
						if (interactableLock.lockMode == InteractableLock.InteractableLockMode.NORMAL)
						{

							{
								AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", base.gameObject);
								interactableLock.BreakLock();
							}
						}
						return;
					}
					if (nearestInteractable is DungeonDoorController)
					{
						DungeonDoorController dungeonDoorController = nearestInteractable as DungeonDoorController;
						if (dungeonDoorController != null && dungeonDoorController.Mode == DungeonDoorController.DungeonDoorMode.COMPLEX && dungeonDoorController.isLocked)
						{
							{
								AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", base.gameObject);
								dungeonDoorController.BreakLock();
							}
						}
					}
					else if (nearestInteractable is Chest)
					{
						Chest chest = nearestInteractable as Chest;
						if (chest.IsLocked)
						{
							if (!chest.IsLockBroken)
							{

								AkSoundEngine.PostEvent("Play_WPN_gun_empty_01", base.gameObject);
								chest.BreakLock();
								return;
							}
						}
					}
				}
			}	
		}
	}
}


