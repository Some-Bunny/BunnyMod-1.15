using System;

namespace GungeonAPI
{
	// Token: 0x02000013 RID: 19
	public static class GungeonAP
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000059ED File Offset: 0x00003BED
		public static void Init()
		{
			Tools.Init();
			StaticReferences.Init();
			ShrineFakePrefabHooks.Init();
			ShrineFactory.Init();
		}
	}
}

