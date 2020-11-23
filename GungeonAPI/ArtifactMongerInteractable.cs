using System;
using System.Collections;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x0200000F RID: 15
	public class ArtifactMongerInteractible : SimpleInteractable, IPlayerInteractable
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004A64 File Offset: 0x00002C64
		public void Start()
		{
			this.talkPoint = base.transform.Find("talkpoint");
			this.m_isToggled = false;
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.black);
			this.m_canUse = true;
			base.spriteAnimator.Play("idle");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public void Interact(PlayerController interactor)
		{
			bool flag = TextBoxManager.HasTextBox(this.talkPoint);
			bool flag2 = !flag;
			if (flag2)
			{
				this.m_canUse = ((this.CanUse != null) ? this.CanUse(interactor, base.gameObject) : this.m_canUse);
				bool flag3 = this.m_canUse;
				bool flag4 = flag3;
				if (flag4)
				{
					bool flagYEE = this.counterfoirfuckssakeWORK == 1f;
					if (flagYEE)
					{
						TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, 2f, ":DD", interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
						base.spriteAnimator.PlayForDuration("talk", 2f, "idle", false);
					}
					else
					{
						base.StartCoroutine(this.HandleConversation(interactor));
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004B71 File Offset: 0x00002D71
		private IEnumerator HandleConversation(PlayerController interactor)
		{
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.black);
			base.spriteAnimator.PlayForDuration("talk_start", 1f, "talk", false);
			interactor.SetInputOverride("npcConversation");
			Pixelator.Instance.LerpToLetterbox(0.35f, 0.25f);
			yield return null;
			int num;
			for (int conversationIndex = this.m_allowMeToIntroduceMyself ? 0 : (this.conversation.Count - 1); conversationIndex < this.conversation.Count - 1; conversationIndex = num + 1)
			{
				Tools.Print<string>(string.Format("Index: {0}", conversationIndex), "FFFFFF", false);
				TextBoxManager.ClearTextBox(this.talkPoint);
				TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, -1f, this.conversation[conversationIndex], interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, true, false);
				float timer = 0f;
				while (!BraveInput.GetInstanceForPlayer(interactor.PlayerIDX).ActiveActions.GetActionFromType(GungeonActions.GungeonActionType.Interact).WasPressed || timer < 0.4f)
				{
					timer += BraveTime.DeltaTime;
					yield return null;
				}
				num = conversationIndex;
			}
			this.m_allowMeToIntroduceMyself = false;
			TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, -1f, this.conversation[this.conversation.Count - 1], interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, true, false);
			GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, this.acceptText, this.declineText);
			int selectedResponse = -1;
			while (!GameUIRoot.Instance.GetPlayerConversationResponse(out selectedResponse))
			{
				yield return null;
			}
			bool flag = selectedResponse == 0;
			bool flag2 = flag;
			if (flag2)
			{
				TextBoxManager.ClearTextBox(this.talkPoint);
				base.spriteAnimator.PlayForDuration("do_effect", 2f, "talk", false);
				while (base.spriteAnimator.CurrentFrame < 9)
				{
					yield return null;
				}
				Action<PlayerController, GameObject> onAccept = this.OnAccept;
				bool flag3 = onAccept != null;
				if (flag3)
				{
					onAccept(interactor, base.gameObject);
					this.counterfoirfuckssakeWORK += 1f;
				}
				base.spriteAnimator.Play("talk");
				TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, 1f, "thenk uuuuuu!", interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
				yield return new WaitForSeconds(1f);
				onAccept = null;
			}
			else
			{
				Action<PlayerController, GameObject> onDecline = this.OnDecline;
				bool flag4 = onDecline != null;
				if (flag4)
				{
					onDecline(interactor, base.gameObject);
				}
				TextBoxManager.ClearTextBox(this.talkPoint);
				onDecline = null;
			}
			interactor.ClearInputOverride("npcConversation");
			Pixelator.Instance.LerpToLetterbox(1f, 0.25f);
			base.spriteAnimator.Play("idle");
			yield break;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004B87 File Offset: 0x00002D87
		public void OnEnteredRange(PlayerController interactor)
		{
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.white, 1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
			base.sprite.UpdateZDepth();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004BB2 File Offset: 0x00002DB2
		public void OnExitRange(PlayerController interactor)
		{
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.black, 1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped)
		{
			shouldBeFlipped = false;
			return string.Empty;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public float GetDistanceToPoint(Vector2 point)
		{
			bool flag = base.sprite == null;
			bool flag2 = flag;
			float result;
			if (flag2)
			{
				result = 100f;
			}
			else
			{
				Vector3 v = BraveMathCollege.ClosestPointOnRectangle(point, base.specRigidbody.UnitBottomLeft, base.specRigidbody.UnitDimensions);
				result = Vector2.Distance(point, v) / 1.5f;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004C58 File Offset: 0x00002E58
		public float GetOverrideMaxDistance()
		{
			return -1f;
		}

		// Token: 0x0400000F RID: 15
		private bool m_allowMeToIntroduceMyself = true;
		private float counterfoirfuckssakeWORK = 0;

	}
}
