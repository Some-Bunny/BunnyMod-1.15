using System;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x02000006 RID: 6
	public static class HitboxMonitor
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000036B8 File Offset: 0x000018B8
		public static void DisplayHitbox(SpeculativeRigidbody speculativeRigidbody)
		{
			PixelCollider hitboxPixelCollider = speculativeRigidbody.HitboxPixelCollider;
			Tools.Log<string>("Collider Found...");
			bool flag = !speculativeRigidbody.gameObject.GetComponent<HitboxMonitor.HitBoxDisplay>();
			if (flag)
			{
				speculativeRigidbody.gameObject.AddComponent<HitboxMonitor.HitBoxDisplay>();
			}
			Tools.Log<string>("Displaying...");
			HitboxMonitor.LogHitboxInfo(hitboxPixelCollider);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003710 File Offset: 0x00001910
		public static void DeleteHitboxDisplays()
		{
			foreach (HitboxMonitor.HitBoxDisplay obj in UnityEngine.Object.FindObjectsOfType<HitboxMonitor.HitBoxDisplay>())
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003740 File Offset: 0x00001940
		private static void LogHitboxInfo(PixelCollider collider)
		{
			Tools.Print<string>(string.Format("Dimensions: ({0},{1})", collider.Dimensions.x, collider.Dimensions.y), "FFFFFF", false);
			Tools.Print<string>(string.Format("Offset: ({0},{1})", collider.Offset.x, collider.Offset.y), "FFFFFF", false);
		}

		// Token: 0x0400000C RID: 12
		private static float pixelsPerUnit = 16f;

		// Token: 0x0200012D RID: 301
		public class HitBoxDisplay : BraveBehaviour
		{
			// Token: 0x06000740 RID: 1856 RVA: 0x0003E4E6 File Offset: 0x0003C6E6
			private void Start()
			{
				this.CreateDisplay();
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
			public void CreateDisplay()
			{
				this.collider = base.specRigidbody.HitboxPixelCollider;
				string name = "HitboxDisplay";
				bool flag = this.hitboxDisplay == null;
				if (flag)
				{
					this.hitboxDisplay = GameObject.CreatePrimitive(PrimitiveType.Cube);
				}
				this.hitboxDisplay.GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f, 0.75f);
				this.hitboxDisplay.name = name;
				this.hitboxDisplay.transform.SetParent(base.specRigidbody.transform);
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x0003E58C File Offset: 0x0003C78C
			private void FixedUpdate()
			{
				this.hitboxDisplay.transform.localScale = new Vector3((float)this.collider.Dimensions.x / HitboxMonitor.pixelsPerUnit, (float)this.collider.Dimensions.y / HitboxMonitor.pixelsPerUnit, 1f);
				Vector3 vector = new Vector3((float)this.collider.Offset.x + (float)this.collider.Dimensions.x * 0.5f, (float)this.collider.Offset.y + (float)this.collider.Dimensions.y * 0.5f, -HitboxMonitor.pixelsPerUnit);
				vector /= HitboxMonitor.pixelsPerUnit;
				this.hitboxDisplay.transform.localPosition = vector;
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x0003E660 File Offset: 0x0003C860
			protected override void OnDestroy()
			{
				bool flag = this.hitboxDisplay;
				if (flag)
				{
					UnityEngine.Object.DestroyImmediate(this.hitboxDisplay);
				}
			}

			// Token: 0x0400023E RID: 574
			private GameObject hitboxDisplay = null;

			// Token: 0x0400023F RID: 575
			private PixelCollider collider;
		}
	}
}
