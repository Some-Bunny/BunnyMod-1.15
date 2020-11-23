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
    public class RTG : PassiveItem
    {

        public static void Init()
        {
            string itemName = "R.T.G";
            string resourceName = "BunnyMod/Resources/rtg.png";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<RTG>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "! BURY 4 KILOMETRES AWAY !";
            string longDesc = "An RTG used in humanities first ventures to Mars. Its heating properties even in the gungeon serve some use, though you probably shouldn't carry it around all the time.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Coolness, -1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, .15f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.C;
        }
        private void NukeBlastOnHit(PlayerController player)
        {
            //I dont know how, but SOMEHOW using Cigarettes with RTG instantly kills you if i try to have this Nuke Code
            //this.random = UnityEngine.Random.Range(0.00f, 1.00f);
            //if (random <= 0.04f)
            {
                Vector3 position = player.sprite.WorldCenter;
                this.Boom(position);
                AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
                GoopDefinition goopDefinition = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
                goopDefinition.baseColor32 = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
                goopDefinition.fireColor32 = new Color32(0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
                goopDefinition.UsesGreenFire = false;
                DeadlyDeadlyGoopManager goopManagerForGoopType = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDefinition);
                goopManagerForGoopType.TimedAddGoopCircle(projectile.sprite.WorldCenter, 10f, 0.1f, false);
                this.Nuke = assetBundle.LoadAsset<GameObject>("assets/data/vfx prefabs/impact vfx/vfx_explosion_nuke.prefab");
                GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Nuke);
                gameObject2.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(projectile.specRigidbody.UnitCenter, tk2dBaseSprite.Anchor.LowerCenter);
                gameObject2.transform.position = gameObject.transform.position.Quantize(0.0625f);
                gameObject2.GetComponent<tk2dBaseSprite>().UpdateZDepth();
                {
                    this.FlashHoldtime = 0.1f;
                    this.FlashFadetime = 0.5f;
                    Pixelator.Instance.FadeToColor(this.FlashFadetime, Color.white, true, this.FlashHoldtime);
                    StickyFrictionManager.Instance.RegisterCustomStickyFriction(0.15f, 1f, false, false); this.FlashHoldtime = 0.1f;
                }
            }
        }

        public void Boom(Vector3 position)
        {
            ExplosionData defaultSmallExplosionData = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData.effect;
            this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData.ignoreList;
            this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);
        }
        private ExplosionData smallPlayerSafeExplosion = new ExplosionData
        {
            damageRadius = 80f,
            damageToPlayer = 0f,
            doDamage = false,
            damage = 250f,
            doExplosionRing = false,
            doDestroyProjectiles = true,
            doForce = true,
            debrisForce = 100f,
            preventPlayerForce = false,
            explosionDelay = 0.25f,
            usesComprehensiveDelay = false,
            doScreenShake = true,
            playDefaultSFX = false
        };
        protected override void Update()
		{
			base.Update();
            List<AIActor> activeEnemies = base.Owner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            Vector2 centerPosition = base.Owner.CenterPosition;
            foreach (AIActor aiactor in activeEnemies)
            {
                BulletStatusEffectItem Firecomponent = PickupObjectDatabase.GetById(295).GetComponent<BulletStatusEffectItem>();
                GameActorFireEffect gameActorFire = Firecomponent.FireModifierEffect;
                bool flag3 = Vector2.Distance(aiactor.CenterPosition, centerPosition) < 4f && aiactor.healthHaver.GetMaxHealth() > 0f && aiactor != null && aiactor.specRigidbody != null && base.Owner != null;
                bool flag4 = flag3;
                if (flag4)
                {
                    {
                        aiactor.ApplyEffect(gameActorFire, 1f, null);
                    }
                }
            }

        }
		public override void Pickup(PlayerController player)
        {

            {
                GameManager.Instance.StartCoroutine(HotPotato());
            }
            base.Pickup(player);
        }
        private IEnumerator HotPotato()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(30, 120));
            AkSoundEngine.PostEvent("Play_ENM_hammer_target_01", gameObject);
            {
                yield return new WaitForSeconds(2);
                {
                    AkSoundEngine.PostEvent("Play_ENM_hammer_smash_01", gameObject);
                    base.Owner.DropPassiveItem(this);
                }
            }
        }
        public override DebrisObject Drop(PlayerController player)
        {
            return base.Drop(player);
        }
        private GameObject Nuke;
        public float FlashHoldtime;
        public float FlashFadetime;
    }
}



