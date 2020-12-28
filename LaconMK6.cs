using Gungeon;
using ItemAPI;
using UnityEngine;

namespace BunnyMod
{
    class Lacon6 : AdvancedGunBehavior
    {
        //this is basically all done by glaurung. I promise you he's a goddamn genuis
        public static void Add()
        {
            Gun gun = ETGMod.Databases.Items.NewGun("LaCon Mk.6", "lacon");
            Game.Items.Rename("outdated_gun_mods:lacon_mk.6", "bny:lacon_mk.6");
            gun.gameObject.AddComponent<Lacon6>();
            gun.SetShortDescription("Modular Perfection");
            gun.SetLongDescription("The ultimate Modular weapon. Filled to the brim with upgrades, this weapon will annhilate anything in its path.");
            gun.SetupSprite(null, "lacon_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 12);
            gun.SetAnimationFPS(gun.idleAnimation, 2);
            //20, 60, 40, 331, 121, 179, 10, 208, 107, 333, 196, 87, 100, 474, 595, 610, 
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(121) as Gun, true, false);
            gun.gunSwitchGroup = (PickupObjectDatabase.GetById(121) as Gun).gunSwitchGroup;
            gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(57) as Gun).muzzleFlashEffects;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Beam;
            gun.DefaultModule.ammoCost = 1;//dis work
            gun.DefaultModule.angleVariance = 0f;//dis doesn't seem ta work
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1f;
            gun.gunClass = GunClass.BEAM;
            gun.barrelOffset.transform.localPosition = new Vector3(1.875f, 0.4375f, 0f);

            gun.DefaultModule.cooldownTime = 0.01f;//dunno if it's useful, don't think so 
            gun.DefaultModule.numberOfShotsInClip = 30;
            gun.SetBaseMaxAmmo(300);

            //changing the projectile to a projectile from a non beam weapon create a null error and break the beam
            //Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(31) as Gun).DefaultModule.projectiles[0]);

            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;

            //those work
            projectile.baseData.damage = 100f;
            projectile.baseData.force *= 1f;
            projectile.FireApplyChance = 0;
            projectile.AppliesFire = false;
            projectile.baseData.speed *= 2.5f;
            projectile.baseData.range *= 2.25f;

            projectile.PenetratesInternalWalls = true;//doesn't seem to work


            projectile.AdditionalScaleMultiplier = 10f;//doesn't work on beam width apparently here
            projectile.AdjustPlayerProjectileTint(Color.magenta, 10, 1f); //doesn't change anything here

            //doesn't work here
            BounceProjModifier bounceMod = projectile.gameObject.GetOrAddComponent<BounceProjModifier>();
            bounceMod.numberOfBounces = 4;
            PierceProjModifier pierceMod = projectile.gameObject.GetOrAddComponent<PierceProjModifier>();
            pierceMod.penetratesBreakables = true;
            pierceMod.penetration = 5;


            gun.quality = PickupObject.ItemQuality.SPECIAL;

            //projectile.SetProjectileSpriteRight("build_projectile", 5, 5);

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }

        protected override void OnPickup(PlayerController player)
        {
            base.OnPickup(player);
            //player.GunChanged += this.OnGunChanged;
            player.PostProcessBeam += this.PostProcessBeam;
            player.GunChanged += this.OnGunChanged;
        }


        protected override void OnPostDrop(PlayerController player)
        {
            player.PostProcessBeam -= this.PostProcessBeam;
            player.GunChanged -= this.OnGunChanged;
            base.OnPostDrop(player);
        }

        private void OnGunChanged(Gun oldGun, Gun newGun, bool arg3)
        {

            if (this.gun && this.gun.CurrentOwner)
            {
                PlayerController player = this.gun.CurrentOwner as PlayerController;
                if (newGun == this.gun)
                {
                    player.PostProcessBeam += this.PostProcessBeam;
                }
                else
                {
                    player.PostProcessBeam -= this.PostProcessBeam;
                }
            }
        }
        /*
        protected override void Update()
        {
            PlayerController player = this.gun.CurrentOwner as PlayerController;
            if (gun.CurrentOwner)
            {
                bool check = player.HasPickupID(Game.Items["bny:lacon_upgrade_scrap"].PickupObjectId);
                if (check)
                {
                    AkSoundEngine.PostEvent("Play_OBJ_spears_clank_01", base.gameObject);
                    Upgrades++;
                    player.RemovePassiveItem(Lacon1Scrap.Scrap1ID);
                }
            }
           
        }
        private static int Upgrades = 0;
        */

        private void PostProcessBeam(BeamController beam)
        {
            beam.AdjustPlayerBeamTint(Color.magenta, 1); //works
            if (beam is BasicBeamController)
            {
                BasicBeamController basicBeamController = (beam as BasicBeamController);
                basicBeamController.penetration += 10; //it works 
                if (!basicBeamController.IsReflectedBeam)
                {
                    //basicBeamController.reflections = 5; //reflection = bounce and it works 
                    //create lag when hitting a broken lamp thingy on walls though for some reasons
                }
                basicBeamController.ProjectileScale = 1.5f;//it works !!!
                basicBeamController.PenetratesCover = true; //works to pass through tables
                basicBeamController.DamageModifier = 80f;
                //basicBeamController.homingRadius = 9999f;//work
                //basicBeamController.homingAngularVelocity = 9999f;//work
                basicBeamController.projectile.PenetratesInternalWalls = true;//don't work
            }
        }
    }
}
