using System;
using System.Collections.Generic;
using ItemAPI;


namespace BunnyMod
{
	// Token: 0x020000AF RID: 175
	internal class SynergyFormInitialiser
	{
		// Token: 0x0600039C RID: 924 RVA: 0x00022AF0 File Offset: 0x00020CF0
		public static void AddSynergyForms()
		{
			AdvancedTransformGunSynergyProcessor advancedTransformGunSynergyProcessor = (PickupObjectDatabase.GetById(ChaosRevolver.ChaosRevolverID) as Gun).gameObject.AddComponent<AdvancedTransformGunSynergyProcessor>();
			advancedTransformGunSynergyProcessor.NonSynergyGunId = ChaosRevolver.ChaosRevolverID;
			advancedTransformGunSynergyProcessor.SynergyGunId = ChaosRevolverSynergyForme.ChaosRevolverSynergyFormeID;
			advancedTransformGunSynergyProcessor.SynergyToCheck = "Reunion";
		}
	}
}
