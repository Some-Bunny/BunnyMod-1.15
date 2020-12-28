using System;
using System.Collections.Generic;
using ItemAPI;
using Dungeonator;
using UnityEngine;


using Gungeon;
namespace BunnyMod
{
	// Token: 0x020000AF RID: 175
	internal class SynergyFormInitialiser
	{
		public static void AddSynergyForms()
		{
			AdvancedTransformGunSynergyProcessor advancedTransformGunSynergyProcessor = (PickupObjectDatabase.GetById(ChaosRevolver.ChaosRevolverID) as Gun).gameObject.AddComponent<AdvancedTransformGunSynergyProcessor>();
			advancedTransformGunSynergyProcessor.NonSynergyGunId = ChaosRevolver.ChaosRevolverID;
			advancedTransformGunSynergyProcessor.SynergyGunId = ChaosRevolverSynergyForme.ChaosRevolverSynergyFormeID;
			advancedTransformGunSynergyProcessor.SynergyToCheck = "Reunion";
			AdvancedDualWieldSynergyProcessor advancedDualWieldSynergyProcessor = (PickupObjectDatabase.GetById(Death.DeathID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			advancedDualWieldSynergyProcessor.PartnerGunID = Taxes.TaxesID;
			advancedDualWieldSynergyProcessor.SynergyNameToCheck = "Death & Taxes";
			AdvancedDualWieldSynergyProcessor advancedDualWieldSynergyProcessor1 = (PickupObjectDatabase.GetById(Taxes.TaxesID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			advancedDualWieldSynergyProcessor1.PartnerGunID = Death.DeathID;
			advancedDualWieldSynergyProcessor1.SynergyNameToCheck = "Death & Taxes";
			AdvancedDualWieldSynergyProcessor gunther = (PickupObjectDatabase.GetById(Gunthemimic.GunthemimicID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			gunther.PartnerGunID = 338;
			gunther.SynergyNameToCheck = "Imposter Syndrome";
			AdvancedDualWieldSynergyProcessor gunther1 = (PickupObjectDatabase.GetById(338) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			gunther1.PartnerGunID = Gunthemimic.GunthemimicID;
			gunther1.SynergyNameToCheck = "Imposter Syndrome";
			AdvancedDualWieldSynergyProcessor akey = (PickupObjectDatabase.GetById(Mimikey47.MimikeyID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			akey.PartnerGunID = 95;
			akey.SynergyNameToCheck = "One Locks, Other Unlocks";
			AdvancedDualWieldSynergyProcessor akey1 = (PickupObjectDatabase.GetById(95) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			akey1.PartnerGunID = Mimikey47.MimikeyID;
			akey1.SynergyNameToCheck = "One Locks, Other Unlocks";
			AdvancedDualWieldSynergyProcessor casey = (PickupObjectDatabase.GetById(Casemimic.CasemimicID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			casey.PartnerGunID = 541;
			casey.SynergyNameToCheck = "Suspicion On #ff0000";
			AdvancedDualWieldSynergyProcessor casey1 = (PickupObjectDatabase.GetById(541) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			casey1.PartnerGunID = Casemimic.CasemimicID;
			casey1.SynergyNameToCheck = "Suspicion On #ff0000";
			AdvancedDualWieldSynergyProcessor blasphemy = (PickupObjectDatabase.GetById(ABlasphemimic.BlashempmicID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			blasphemy.PartnerGunID = 417;
			blasphemy.SynergyNameToCheck = "Double-Edged Sword";
			AdvancedDualWieldSynergyProcessor blasphemy1 = (PickupObjectDatabase.GetById(417) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			blasphemy1.PartnerGunID = ABlasphemimic.BlashempmicID;
			blasphemy1.SynergyNameToCheck = "Double-Edged Sword";
			AdvancedDualWieldSynergyProcessor chamber = (PickupObjectDatabase.GetById(ChambemimicGun.ChambemimicID) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
            chamber.PartnerGunID = 647;
			chamber.SynergyNameToCheck = "Russian Roulette";
			AdvancedDualWieldSynergyProcessor chamber1 = (PickupObjectDatabase.GetById(647) as Gun).gameObject.AddComponent<AdvancedDualWieldSynergyProcessor>();
			chamber1.PartnerGunID = ChambemimicGun.ChambemimicID;
			chamber1.SynergyNameToCheck = "Russian Roulette";
		}
	}
}
