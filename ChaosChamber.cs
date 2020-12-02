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



namespace BunnyMod
{
    public class ChaosChamber : PassiveItem
    {

        public PlayerStats.StatType Damage { get; private set; }

        public static void Init()
        {
            string itemName = "Chaos Chamber";
            string resourceName = "BunnyMod/Resources/chaoschamber";
            GameObject obj = new GameObject(itemName);
            ChaosChamber chaosChamber = obj.AddComponent<ChaosChamber>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Unpredictable Chamber";
            string longDesc = "\n\nOn smelling blood, it will devour anything it can see, with a distaste for the Jammed.\n\nThe legendary chamber of a gun that was made by accident when a material forged with the concepts of reality was accidentally added to a custom design revolver. Legends say that while the gun was hidden, the chamber escaped.\n\n";
            chaosChamber.SetupItem(shortDesc, longDesc, "bny");
            chaosChamber.AddPassiveStatModifier(PlayerStats.StatType.Damage, .20f, StatModifier.ModifyMethod.ADDITIVE);
            chaosChamber.AddPassiveStatModifier(PlayerStats.StatType.RateOfFire, .15f, StatModifier.ModifyMethod.ADDITIVE);
            chaosChamber.AddPassiveStatModifier(PlayerStats.StatType.ReloadSpeed, -0.2f, StatModifier.ModifyMethod.ADDITIVE);
            chaosChamber.AddPassiveStatModifier(PlayerStats.StatType.Accuracy, .9f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            chaosChamber.quality = PickupObject.ItemQuality.A;
            chaosChamber.AddToSubShop(ItemBuilder.ShopType.Trorc, 1f);
            chaosChamber.AddToSubShop(ItemBuilder.ShopType.Cursula, 1f);
        }

        public override void Pickup(PlayerController player)
        {
            player.OnReloadedGun += (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.somethingrelatedtojammed));
            player.healthHaver.OnDamaged += new HealthHaver.OnDamagedEvent(this.oof);
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
        public override DebrisObject Drop(PlayerController player)
        {
            player.OnReloadedGun -= (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.somethingrelatedtojammed));
            player.healthHaver.OnDamaged -= new HealthHaver.OnDamagedEvent(this.oof);
            DebrisObject result = base.Drop(player);
            return result;
        }
        private void somethingrelatedtojammed(PlayerController player, Gun playerGun)
        {
            bool flag = playerGun.ClipShotsRemaining == 0;
            if (flag)
            {
                List<AIActor> activeEnemies = player.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                if (activeEnemies != null)
                {
                    int count = activeEnemies.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (activeEnemies[i] && activeEnemies[i].HasBeenEngaged && activeEnemies[i].healthHaver && activeEnemies[i].IsNormalEnemy && !activeEnemies[i].healthHaver.IsDead && !activeEnemies[i].healthHaver.IsBoss && !activeEnemies[i].IsTransmogrified && activeEnemies[i].IsBlackPhantom)
                        {
                            GameManager.Instance.StartCoroutine(this.Heatfuck(player));
                        }
                    }
                }
            }
        }
        private IEnumerator Heatfuck(PlayerController user)
        {
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            GoopDefinition goopDefinition = assetBundle.LoadAsset<GoopDefinition>("assets/data/goops/napalmgoopquickignite.asset");
            DeadlyDeadlyGoopManager goopManagerForGoopType = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopDefinition);
            goopManagerForGoopType.TimedAddGoopCircle(base.Owner.sprite.WorldCenter, 18f, 0.8f, false);
            yield return new WaitForSeconds(1f);
            DeadlyDeadlyGoopManager.DelayedClearGoopsInRadius(base.Owner.sprite.WorldCenter, 25f);
            yield break;
        }
        private void oof(float resultValue, float maxValue, CoreDamageTypes damageTypes, DamageCategory damageCategory, Vector2 damageDirection)
        {
            bool flag3 = this.m_owner.CurrentRoom != null;
            if (flag3)
            {
                AkSoundEngine.PostEvent("Play_ENM_kali_burst_01", base.gameObject);
                this.m_owner.CurrentRoom.ApplyActionToNearbyEnemies(this.m_owner.CenterPosition, 20f, new Action<AIActor, float>(this.ProcessEnemy));
            }
        }

		private void ProcessEnemy(AIActor target, float distance)
		{
            {
                GameManager.Instance.Dungeon.StartCoroutine(this.HandleEnemySuck(target));
                target.EraseFromExistence(true);
            }
		}
        private IEnumerator HandleEnemySuck(AIActor target)
        {
            Transform copySprite = this.CreateEmptySprite(target);
            Vector3 startPosition = copySprite.transform.position;
            float elapsed = 0f;
            float duration = 0.78f;
            while (elapsed < duration)
            {
                elapsed += BraveTime.DeltaTime;
                bool flag3 = this.m_owner.CurrentGun && copySprite;
                if (flag3)
                {
                    Vector3 position = this.m_owner.CurrentGun.PrimaryHandAttachPoint.position;
                    float t = elapsed / duration * (elapsed / duration);
                    copySprite.position = Vector3.Lerp(startPosition, position, t);
                    copySprite.rotation = Quaternion.Euler(0f, 0f, 360f * BraveTime.DeltaTime) * copySprite.rotation;
                    copySprite.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.1f, 0.1f, 0.1f), t);
                    position = default(Vector3);
                }
                yield return null;
            }
            bool flag4 = copySprite;
            if (flag4)
            {
                UnityEngine.Object.Destroy(copySprite.gameObject);
            }
            yield break;
        }
        private Transform CreateEmptySprite(AIActor target)
        {
            GameObject gameObject = new GameObject("suck image");
            gameObject.layer = target.gameObject.layer;
            tk2dSprite tk2dSprite = gameObject.AddComponent<tk2dSprite>();
            gameObject.transform.parent = SpawnManager.Instance.VFX;
            tk2dSprite.SetSprite(target.sprite.Collection, target.sprite.spriteId);
            tk2dSprite.transform.position = target.sprite.transform.position;
            GameObject gameObject2 = new GameObject("image parent");
            gameObject2.transform.position = tk2dSprite.WorldCenter;
            tk2dSprite.transform.parent = gameObject2.transform;
            bool flag = target.optionalPalette != null;
            if (flag)
            {
                tk2dSprite.renderer.material.SetTexture("_PaletteTex", target.optionalPalette);
            }
            return gameObject2.transform;
        }
        public AIActor AIActor;
        public PlayerController player;
        public Gun gun;
    }
}



