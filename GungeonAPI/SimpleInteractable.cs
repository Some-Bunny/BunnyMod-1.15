using System;
using System.Collections.Generic;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x0200001A RID: 26
	public abstract class SimpleInteractable : BraveBehaviour
	{
		// Token: 0x0400004F RID: 79
		public Action<PlayerController, GameObject> OnAccept;

		// Token: 0x04000050 RID: 80
		public Action<PlayerController, GameObject> OnDecline;

		// Token: 0x04000051 RID: 81
		public List<string> conversation;

		public List<string> conversationB;

		// Token: 0x04000052 RID: 82
		public Func<PlayerController, GameObject, bool> CanUse;

		// Token: 0x04000053 RID: 83
		public Transform talkPoint;

		// Token: 0x04000054 RID: 84
		public string text;

		// Token: 0x04000055 RID: 85
		public string acceptText;

		// Token: 0x04000037 RID: 55
		public string acceptTextB;

		// Token: 0x04000038 RID: 56
		public string declineText;

		// Token: 0x04000039 RID: 57
		public string declineTextB;

		// Token: 0x04000057 RID: 87
		public bool isToggle;

		// Token: 0x04000058 RID: 88
		protected bool m_isToggled;

		// Token: 0x04000059 RID: 89
		protected bool m_canUse = true;

	}
}
