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
    public class ModuleAmmoEater : PlayerItem
    {

        public static void Init()
        {
            string itemName = "Modular Printer";
            string resourceName = "BunnyMod/Resources/weaponmodularammoeater.png";
            GameObject obj = new GameObject(itemName);
            ModuleAmmoEater lockpicker = obj.AddComponent<ModuleAmmoEater>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Prints Modules, Sort Of.";
            string longDesc = "A machine that slowly prints corrupt Modules. These can be slowly reconverted to normal ones, however their damaged software will affect you.";
            lockpicker.SetupItem(shortDesc, longDesc, "bny");
            lockpicker.SetCooldownType(ItemBuilder.CooldownType.PerRoom, 6f);
			lockpicker.AddPassiveStatModifier(PlayerStats.StatType.AdditionalItemCapacity, 1f, StatModifier.ModifyMethod.ADDITIVE);
			lockpicker.consumable = false;
            lockpicker.quality = PickupObject.ItemQuality.EXCLUDED;
			ModuleAmmoEater.spriteIDs = new int[ModuleAmmoEater.spritePaths.Length];
			ModuleAmmoEater.spriteIDs[0] = SpriteBuilder.AddSpriteToCollection(ModuleAmmoEater.spritePaths[0], lockpicker.sprite.Collection);
			ModuleAmmoEater.spriteIDs[1] = SpriteBuilder.AddSpriteToCollection(ModuleAmmoEater.spritePaths[1], lockpicker.sprite.Collection);
		}
        public override void Pickup(PlayerController player)
		{
			base.Pickup(player);
        }
		protected override void DoEffect(PlayerController user)
		{
			int num3 = UnityEngine.Random.Range(0, 5);
			bool flag3 = num3 == 0;
			if (flag3)
			{
				LootEngine.SpawnItem(ETGMod.Databases.Items["Corrupt Sensor Module"].gameObject, user.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
			}
			bool flag4 = num3 == 1;
			if (flag4)
			{
				LootEngine.SpawnItem(ETGMod.Databases.Items["Corrupt Accuracy Module"].gameObject, user.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
			}
			bool flag5 = num3 == 2;
			if (flag5)
			{
				LootEngine.SpawnItem(ETGMod.Databases.Items["Corrupt Cooling Module"].gameObject, user.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
			}
			bool flag6 = num3 == 3;
			if (flag6)
			{
				LootEngine.SpawnItem(ETGMod.Databases.Items["Corrupt Fitting Module"].gameObject, user.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
			}
			bool flag7 = num3 == 4;
			if (flag7)
			{
				LootEngine.SpawnItem(ETGMod.Databases.Items["Corrupt Damage Module"].gameObject, user.specRigidbody.UnitCenter, Vector2.zero, 1f, false, true, false);
			}
		}

		private static int[] spriteIDs;
		


		// Token: 0x04000087 RID: 135
		private static readonly string[] spritePaths = new string[]
		{
			"BunnyMod/Resources/weaponmodularammoeater.png",
			"BunnyMod/Resources/weaponmodularammoeaterfull.png"
		};
	}
}



