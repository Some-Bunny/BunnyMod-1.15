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
	public class CaptainsShotgun : GunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Captains Left Hand", "captainsshotgun");
			Game.Items.Rename("outdated_gun_mods:captains_left_hand", "bny:captains_left_hand");
			gun.gameObject.AddComponent<CaptainsShotgun>();
			gun.SetShortDescription("For Safe Travels");
			gun.SetLongDescription("A prosthetic vulcan shotgun once attached to an old ship captain. Although its capable of charging up to pin-point accuracy, you're not able to as it's not your hand.");
			gun.SetupSprite(null, "captainsshotgun_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 4);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			for (int i = 0; i < 8; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(59) as Gun, true, false);
				gun.gunSwitchGroup = (PickupObjectDatabase.GetById(57) as Gun).gunSwitchGroup;

			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 0.9f;
				projectileModule.angleVariance = 30f;
				projectileModule.numberOfShotsInClip = 8;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				projectileModule.projectiles[0] = projectile;
				projectile.baseData.damage = 6f;
				projectile.AdditionalScaleMultiplier = 1f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				projectile.SetProjectileSpriteRight("captainsshotgun_projectile_001", 11, 3, true, tk2dBaseSprite.Anchor.MiddleCenter, new int?(5), new int?(3), null, null, null);
				gun.DefaultModule.projectiles[0] = projectile;
				bool flag = projectileModule != gun.DefaultModule;
				if (flag)
				{
					projectileModule.ammoCost = 0;
				}
				projectile.transform.parent = gun.barrelOffset;
			}
			gun.reloadTime = 1.7f;
			gun.SetBaseMaxAmmo(160);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(57) as Gun).muzzleFlashEffects;
			gun.quality = PickupObject.ItemQuality.B;
			gun.encounterTrackable.EncounterGuid = "Cheer up Bunny ^ᴗ^ (i dont want to change this, at the very least not remove it) still doing this";
			//thanks Hunter, i really needed that, i wish you the best, cause you da best o7
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			List<string> mandatoryConsoleIDs15 = new List<string>
			{
				"bny:captains_left_hand"
			};
			List<string> optionalConsoleIDs = new List<string>
			{
				"robots_left_hand",
				"bionic_leg",
				"roll_bomb"
			};
			CustomSynergies.Add("Cybernetic Enchancements", mandatoryConsoleIDs15, optionalConsoleIDs, true);
			List<string> mandatoryConsoleIDs1 = new List<string>
			{
				"bny:captains_left_hand"
			};
			List<string> optionalConsoleID1s = new List<string>
			{
				"ibomb_companion_app",
				"mourning_star",
				"rocket_powered_bullets",
				"galactic_medal_of_valor",
				"laser_sight"
			};
			CustomSynergies.Add("Captains Right Hand", mandatoryConsoleIDs1, optionalConsoleID1s, true);
			CaptainsShotgun.CaptainsShotgunID = gun.PickupObjectId;
		}
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController player = projectile.Owner as PlayerController;
			this.Cybernetics(player);
			projectile.baseData.speed = 30f;
			projectile.baseData.range = 20f;
			projectile.baseData.force = 15f;
		}
		private bool HasReloaded;
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			bool flag = this.HasReloaded && gun.ClipShotsRemaining == 0;
			if (flag)
			{
				bool flagA = player.PlayerHasActiveSynergy("Captains Right Hand");
				if (flagA)
				{
					bool isInCombat = player.IsInCombat;
					if (isInCombat)
					{
						AkSoundEngine.PostEvent("Play_OBJ_supplydrop_activate_01", gameObject);
						GameManager.Instance.StartCoroutine(OrbitalStrike());
						this.HasReloaded = false;
					}
				}
			}
			base.OnReloadPressed(player, gun, bSOMETHING);
			this.Cybernetics(player);
		}
		private IEnumerator OrbitalStrike()
		{
			for (int counter = 0; counter < 3; counter++)
			{
				this.FireRocket();
				if (this.m_spawnedRockets > 0 && (BossKillCam.BossDeathCamRunning || GameManager.Instance.PreventPausing))
				{
					this.Cleanup();
				}
				yield return new WaitForSeconds(0.2f);
			}
			yield break;
		}
		private void OnDestroy()
		{
			this.Cleanup();
		}
		private void FireRocket()
		{
			var cm = UnityEngine.Object.Instantiate<GameObject>((GameObject)BraveResources.Load("Global Prefabs/_ChallengeManager", ".prefab"));
			this.Rocket = (cm.GetComponent<ChallengeManager>().PossibleChallenges.Where(c => c.challenge is SkyRocketChallengeModifier).First().challenge as SkyRocketChallengeModifier).Rocket;
			UnityEngine.Object.Destroy(cm);
			if (BossKillCam.BossDeathCamRunning)
			{
				return;
			}
			if (GameManager.Instance.PreventPausing)
			{
				return;
			}
			RoomHandler absoluteRoom = base.transform.position.GetAbsoluteRoom();
			AIActor randomActiveEnemy = absoluteRoom.GetRandomActiveEnemy(false);
			SkyRocket component = SpawnManager.SpawnProjectile(this.Rocket, Vector3.zero, Quaternion.identity, true).GetComponent<SkyRocket>();
			component.Target = randomActiveEnemy.specRigidbody;
			tk2dSprite componentInChildren = component.GetComponentInChildren<tk2dSprite>();
			component.transform.position = component.transform.position.WithY(component.transform.position.y - componentInChildren.transform.localPosition.y);
			this.m_spawnedRockets++;
		}
		public void Cleanup()
		{
			this.m_spawnedRockets = 0;
			SkyRocket[] array = UnityEngine.Object.FindObjectsOfType<SkyRocket>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					array[i].DieInAir();
				}
			}
		}
		public override void OnInitializedWithOwner(GameActor actor)
		{
			base.OnInitializedWithOwner(actor);
			PlayerController player = actor as PlayerController;
			this.Cybernetics(player);
		}
		private void Update()
		{
			bool flagQR = this.gun.CurrentOwner;
			if (flagQR)
			{
				bool flagRH = !this.gun.IsReloading && !this.HasReloaded;
				if (flagRH)
				{
					this.HasReloaded = true;
				}
			}
			PlayerController playerController = this.gun.CurrentOwner as PlayerController;
			this.currentItems = playerController.passiveItems.Count;
			this.currentGuns = playerController.inventory.AllGuns.Count;
			this.currentClip = this.gun.DefaultModule.numberOfShotsInClip;
			this.currentActives = playerController.activeItems.Count;
			bool flag = this.currentItems != this.lastItems || this.currentGuns != this.lastGuns || this.currentClip != this.lastClip || this.currentActives != this.lastActives;
			if (flag)
			{
				this.Cybernetics(playerController);
				this.lastGuns = this.currentGuns;
				this.lastItems = this.currentItems;
				this.lastClip = this.currentClip;
				this.lastActives = this.currentActives;
			}
			bool flag2 = playerController.PlayerHasActiveSynergy("Cybernetic Enchancements");
			if (flag2)
			{

				bool flag4 = !this.hadBionicsLastTimeChecked;
				if (flag4)
				{
					this.Cybernetics(playerController);
					this.hadBionicsLastTimeChecked = true;
				}
			}
			else
			{
				bool flag6 = this.hadBionicsLastTimeChecked;
				if (flag6)
				{
					this.Cybernetics(playerController);
					this.hadBionicsLastTimeChecked = false;
				}
			}
		}
		private void Cybernetics(PlayerController player)
		{
			int num = 30;
			bool flag = player.PlayerHasActiveSynergy("Cybernetic Enchancements");
			if (flag)
			{
				foreach (PassiveItem passiveItem in player.passiveItems)
				{
					bool flag2 = CaptainsShotgun.CybersItems.Contains(passiveItem.PickupObjectId);
					if (flag2)
					{
						num -= 15;
					}
				}
				foreach (Gun gun in player.inventory.AllGuns)
				{
					bool flag3 = CaptainsShotgun.CybersItems.Contains(gun.PickupObjectId);
					if (flag3)
					{
						num -= 15;
					}
				}
				foreach (PlayerItem playerItem in player.activeItems)
				{
					bool flag4 = CaptainsShotgun.CybersItems.Contains(playerItem.PickupObjectId);
					if (flag4)
					{
						num -= 15;
					}
				}
				foreach (ProjectileModule projectileModule in this.gun.Volley.projectiles)
				{
					projectileModule.angleVariance = num;
				}
			}
			else
			{
				foreach (ProjectileModule projectileModule in this.gun.Volley.projectiles)
				{
					projectileModule.angleVariance = 30f;
				}
			}
		}

		public static List<int> CybersItems = new List<int>
		{
			576,
			114,
			567
		};
		private bool hadBionicsLastTimeChecked;
		private int currentItems;
		private int lastItems;
		private int currentActives;
		private int lastActives;
		private int currentGuns;
		private int lastGuns;
		private int currentClip;
		private int lastClip;
		public GameObject Rocket;
		public float TimeBetweenRockets;
		private int m_spawnedRockets;
		public static int CaptainsShotgunID;

	}
}