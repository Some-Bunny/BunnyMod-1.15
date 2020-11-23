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
    public class Keylocator : PlayerItem
    {

        public static void Init()
        {
            string itemName = "Kelocator";
            string resourceName = "BunnyMod/Resources/keylocator";
            GameObject obj = new GameObject(itemName);
            Keylocator whisper = obj.AddComponent<Keylocator>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Remember to keep your receipt!";
            string longDesc = "Introducing the new Kelocator! Are you tired of having spare, useless keys in your pocket? Now you can transport them into the future! Simply deposit your key into the slot, and press the button. Voila! Your key will be with you, in the future!\n\nTimeBanks.Inc does not claim any responsibility for accidental replication or deletion of your keys.";
            whisper.SetupItem(shortDesc, longDesc, "bny");
            whisper.SetCooldownType(ItemBuilder.CooldownType.Timed, 5f);
            whisper.consumable = false;
            whisper.quality = PickupObject.ItemQuality.C;
            whisper.AddToSubShop(ItemBuilder.ShopType.Flynt, 1f);
        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void DoEffect(PlayerController user)
        {
            string header = "";
            string text = "";
            user.carriedConsumables.KeyBullets--;
            LootEngine.TryGivePrefabToPlayer(ETGMod.Databases.Items["Key Receipt"].gameObject, user, true);
            header = "Your Key is now in transit.";
            text = "Expect a short wait time.";
            this.Notify(header, text);
        }
        public override bool CanBeUsed(PlayerController user)
        {
            return user.carriedConsumables.KeyBullets > 0;
        }
        private void Notify(string header, string text)
        {
            tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
            int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/keylocator");
            GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, null, spriteIdByName, UINotificationController.NotificationColor.GOLD, false, true);
        }
    }
}

namespace BunnyMod
{
    public class Keyceipt : PassiveItem
    {
        private float random;

        public static void Init()
        {
            string itemName = "Key Receipt";
            string resourceName = "BunnyMod/Resources/keyceipt";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<Keyceipt>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Keep it!.";
            string longDesc = "A receipt for your key re-transportation services. Don't lose it, as this should only take 5 minutes average!";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "bny");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            this.CanBeDropped = false;
            base.Pickup(player);
            {
                GameManager.Instance.StartCoroutine(KeyTimer());
            }
        }
        private IEnumerator KeyTimer()
        {
            yield return new WaitForSeconds(300f);
            {

                this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                if (random <= 0.95f)
                {
                    this.random = UnityEngine.Random.Range(0.0f, 1.0f);
                    if (random <= 0.8f)
                    {
                        string header = "";
                        string text = "";
                        AkSoundEngine.PostEvent("Play_OBJ_goldkey_pickup_01", base.gameObject);
                        LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.Owner);
                        base.Owner.DropPassiveItem(this);
                        header = "Your Key has arrived.";
                        text = "We hope our service was of use.";
                        this.Notify(header, text);
                    }
                    else
                    {
                        string header = "";
                        string text = "";
                        AkSoundEngine.PostEvent("Play_OBJ_goldkey_pickup_01", base.gameObject);
                        LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.Owner);
                        LootEngine.GivePrefabToPlayer(PickupObjectDatabase.GetById(67).gameObject, base.Owner);
                        base.Owner.DropPassiveItem(this);
                        header = "Your Key has duplicated.";
                        text = "Hand over extras to authorities.";
                        this.Notify(header, text);
                    }
                }
                else
                {
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", base.gameObject);
                    string header = "";
                    string text = "";
                    base.Owner.DropPassiveItem(this);
                    header = "Your Key got lost.";
                    text = "We apologize for the error.";
                    this.Notify(header, text);
                }
            }
        }
        public override DebrisObject Drop(PlayerController player)
        {
            DebrisObject debrisObject = base.Drop(player);
            Keyceipt component = debrisObject.GetComponent<Keyceipt>();
            component.Break();
            return debrisObject;
        }
        public void Break()
        {
            this.m_pickedUp = true;
            UnityEngine.Object.Destroy(base.gameObject, 1f);
        }
        private void Notify(string header, string text)
        {
            tk2dSpriteCollectionData encounterIconCollection = AmmonomiconController.Instance.EncounterIconCollection;
            int spriteIdByName = encounterIconCollection.GetSpriteIdByName("BunnyMod/Resources/keyceipt");
            GameUIRoot.Instance.notificationController.DoCustomNotification(header, text, null, spriteIdByName, UINotificationController.NotificationColor.GOLD, false, true);
        }
    }
}
