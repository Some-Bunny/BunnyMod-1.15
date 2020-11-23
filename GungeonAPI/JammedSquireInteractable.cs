using System;
using System.Collections;
using System.Collections.Generic;
using GungeonAPI;
using UnityEngine;

namespace GungeonAPI
{
	// Token: 0x020000E6 RID: 230
	public class JammedSquireInteractable : SimpleInteractable, IPlayerInteractable
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0002CE58 File Offset: 0x0002B058
		private void Start()
		{
			this.talkPoint = base.transform.Find("talkpoint");
			this.m_isToggled = false;
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.black);
			this.m_canUse = true;
			base.spriteAnimator.Play("idle");
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002CEAC File Offset: 0x0002B0AC
		public void Interact(PlayerController interactor)
		{
			bool flag = TextBoxManager.HasTextBox(this.talkPoint);
			bool flag2 = !flag;
			if (flag2)
			{
				this.m_canUse = ((this.CanUse != null) ? this.CanUse(interactor, base.gameObject) : this.m_canUse);
				bool flag3 = !this.m_canUse;
				bool flag4 = flag3;
				if (flag4)
				{
					base.spriteAnimator.PlayForDuration("talk", 2f, "idle", false);
					TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, 2f, ". . .", interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
				}
				else
				{
					base.StartCoroutine(this.HandleConversation(interactor));
				}
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0002CF65 File Offset: 0x0002B165
		private IEnumerator HandleConversation(PlayerController interactor)
		{
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.black);
			base.spriteAnimator.PlayForDuration("talk_start", 1f, "talk", false);
			interactor.SetInputOverride("npcConversation");
			Pixelator.Instance.LerpToLetterbox(0.35f, 0.25f);
			yield return null;
			List<string> conversationToUse = JammedSquire.NoHarderLotJ ? this.conversation : this.conversation2;
			int num;
	    	for (int conversationIndex = 0; conversationIndex < conversationToUse.Count - 1; conversationIndex = num + 1)
			{
				Tools.Print<string>(string.Format("Index: {0}", conversationIndex), "FFFFFF", false);
				TextBoxManager.ClearTextBox(this.talkPoint);
				TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, -1f, conversationToUse[conversationIndex], interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, true, false);
				float timer = 0f;
				while (!BraveInput.GetInstanceForPlayer(interactor.PlayerIDX).ActiveActions.GetActionFromType(GungeonActions.GungeonActionType.Interact).WasPressed || timer < 0.4f)
				{
					timer += BraveTime.DeltaTime;
					yield return null;
				}
				num = conversationIndex;
			}
			this.m_allowMeToIntroduceMyself = false;
			TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, -1f, conversationToUse[conversationToUse.Count - 1], interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, true, false);
			string acceptanceTextToUse = JammedSquire.NoHarderLotJ ? this.acceptText : this.acceptText2;
			string declineTextToUse = JammedSquire.NoHarderLotJ ? this.declineText : this.declineText2;
			GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, acceptanceTextToUse, declineTextToUse);
			int selectedResponse = -1;
			while (!GameUIRoot.Instance.GetPlayerConversationResponse(out selectedResponse))
			{
				yield return null;
			}
			bool flag = selectedResponse == 0;
			if (flag)
			{
				TextBoxManager.ClearTextBox(this.talkPoint);
				base.spriteAnimator.PlayForDuration("do_effect", -1f, "talk", false);
				Action<PlayerController, GameObject> onAccept = this.OnAccept;
				if (onAccept != null)
				{
					onAccept(interactor, base.gameObject);
				}
				base.spriteAnimator.Play("talk");
				TextBoxManager.ShowTextBox(this.talkPoint.position, this.talkPoint, 1f, "So be it...", interactor.characterAudioSpeechTag, false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
				yield return new WaitForSeconds(1f);
			}
			else
			{
				Action<PlayerController, GameObject> onDecline = this.OnDecline;
				if (onDecline != null)
				{
					onDecline(interactor, base.gameObject);
				}
				TextBoxManager.ClearTextBox(this.talkPoint);
			}
			interactor.ClearInputOverride("npcConversation");
			Pixelator.Instance.LerpToLetterbox(1f, 0.25f);
			base.spriteAnimator.Play("idle");
			yield break;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002CF7B File Offset: 0x0002B17B
		public void OnEnteredRange(PlayerController interactor)
		{
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.white, 1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
			base.sprite.UpdateZDepth();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002CFA6 File Offset: 0x0002B1A6
		public void OnExitRange(PlayerController interactor)
		{
			SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.black, 1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
		public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped)
		{
			shouldBeFlipped = false;
			return string.Empty;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
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

		// Token: 0x060004EC RID: 1260 RVA: 0x0002D04C File Offset: 0x0002B24C
		public float GetOverrideMaxDistance()
		{
			return -1f;
		}

		// Token: 0x040001A1 RID: 417
		public bool m_allowMeToIntroduceMyself = true;
	}
}
