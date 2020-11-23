using System;
using System.Collections.Generic;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x0200000D RID: 13
	public class ShrineFakePrefab : Component
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000615C File Offset: 0x0000435C
		public static bool IsFakePrefab(UnityEngine.Object o)
		{
			bool flag = o is GameObject;
			bool result;
			if (flag)
			{
				result = ShrineFakePrefab.ExistingFakePrefabs.Contains((GameObject)o);
			}
			else
			{
				bool flag2 = o is Component;
				result = (flag2 && ShrineFakePrefab.ExistingFakePrefabs.Contains(((Component)o).gameObject));
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000061B6 File Offset: 0x000043B6
		public static void MarkAsFakePrefab(GameObject obj)
		{
			ShrineFakePrefab.ExistingFakePrefabs.Add(obj);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000061C8 File Offset: 0x000043C8
		public static GameObject Clone(GameObject obj)
		{
			bool flag = ShrineFakePrefab.IsFakePrefab(obj);
			bool activeSelf = obj.activeSelf;
			bool flag2 = activeSelf;
			if (flag2)
			{
				obj.SetActive(false);
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(obj);
			bool flag3 = activeSelf;
			if (flag3)
			{
				obj.SetActive(true);
			}
			foreach (object obj2 in gameObject.GetComponentInChildren<Transform>())
			{
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			ShrineFakePrefab.ExistingFakePrefabs.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00006270 File Offset: 0x00004470
		public static UnityEngine.Object Instantiate(UnityEngine.Object o, UnityEngine.Object new_o)
		{
			bool flag = o is GameObject && ShrineFakePrefab.ExistingFakePrefabs.Contains((GameObject)o);
			if (flag)
			{
				((GameObject)new_o).SetActive(true);
			}
			else
			{
				bool flag2 = o is Component && ShrineFakePrefab.ExistingFakePrefabs.Contains(((Component)o).gameObject);
				if (flag2)
				{
					((Component)new_o).gameObject.SetActive(true);
				}
			}
			return new_o;
		}

		// Token: 0x0400002E RID: 46
		internal static HashSet<GameObject> ExistingFakePrefabs = new HashSet<GameObject>();
	}
}
