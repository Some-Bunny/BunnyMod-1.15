using System;
using ItemAPI;
using MonoMod.RuntimeDetour;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x0200000E RID: 14
	public static class ShrineFakePrefabHooks
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00006300 File Offset: 0x00004500
		public static void Init()
		{
			try
			{
				Hook hook = new Hook(typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[]
				{
					typeof(UnityEngine.Object),
					typeof(Transform),
					typeof(bool)
				}), typeof(ShrineFakePrefabHooks).GetMethod("InstantiateOPI"));
				Hook hook2 = new Hook(typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[]
				{
					typeof(UnityEngine.Object),
					typeof(Transform)
				}), typeof(ShrineFakePrefabHooks).GetMethod("InstantiateOP"));
				Hook hook3 = new Hook(typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[]
				{
					typeof(UnityEngine.Object)
				}), typeof(ShrineFakePrefabHooks).GetMethod("InstantiateO"));
				Hook hook4 = new Hook(typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[]
				{
					typeof(UnityEngine.Object),
					typeof(Vector3),
					typeof(Quaternion)
				}), typeof(ShrineFakePrefabHooks).GetMethod("InstantiateOPR"));
				Hook hook5 = new Hook(typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[]
				{
					typeof(UnityEngine.Object),
					typeof(Vector3),
					typeof(Quaternion),
					typeof(Transform)
				}), typeof(ShrineFakePrefabHooks).GetMethod("InstantiateOPRP"));
			}
			catch (Exception e)
			{
				Tools.PrintException(e, "FF0000");
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000064F0 File Offset: 0x000046F0
		public static T InstantiateGeneric<T>(UnityEngine.Object original) where T : UnityEngine.Object
		{
			return (T)((object)FakePrefab.Instantiate(original, UnityEngine.Object.Instantiate(original)));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006514 File Offset: 0x00004714
		public static UnityEngine.Object InstantiateOPI(Func<UnityEngine.Object, Transform, bool, UnityEngine.Object> orig, UnityEngine.Object original, Transform parent, bool instantiateInWorldSpace)
		{
			return FakePrefab.Instantiate(original, orig(original, parent, instantiateInWorldSpace));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006538 File Offset: 0x00004738
		public static UnityEngine.Object InstantiateOP(Func<UnityEngine.Object, Transform, UnityEngine.Object> orig, UnityEngine.Object original, Transform parent)
		{
			return FakePrefab.Instantiate(original, orig(original, parent));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006558 File Offset: 0x00004758
		public static UnityEngine.Object InstantiateO(Func<UnityEngine.Object, UnityEngine.Object> orig, UnityEngine.Object original)
		{
			return FakePrefab.Instantiate(original, orig(original));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006578 File Offset: 0x00004778
		public static UnityEngine.Object InstantiateOPR(Func<UnityEngine.Object, Vector3, Quaternion, UnityEngine.Object> orig, UnityEngine.Object original, Vector3 position, Quaternion rotation)
		{
			return FakePrefab.Instantiate(original, orig(original, position, rotation));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000659C File Offset: 0x0000479C
		public static UnityEngine.Object InstantiateOPRP(Func<UnityEngine.Object, Vector3, Quaternion, Transform, UnityEngine.Object> orig, UnityEngine.Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			return FakePrefab.Instantiate(original, orig(original, position, rotation, parent));
		}

		// Token: 0x02000133 RID: 307
		// (Invoke) Token: 0x0600074F RID: 1871
		public delegate TResult Func<T1, T2, T3, T4, T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
	}
}
