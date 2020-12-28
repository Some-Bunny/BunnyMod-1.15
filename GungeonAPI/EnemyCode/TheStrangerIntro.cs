using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace BunnyMod
{

    [RequireComponent(typeof(GenericIntroDoer))]
    public class TheStrangerIntro : SpecificIntroDoer
    {

        public bool m_finished;

        // private bool m_initialized;        
        public AIActor m_AIActor;

        //public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators)
        //{
        //    GameManager.Instance.StartCoroutine(PlaySound());
        //}

        public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators)
        {
            GameManager.Instance.StartCoroutine(PlaySound());
        }

        private IEnumerator PlaySound()
        {
            yield return StartCoroutine(WaitForSecondsInvariant(3.2f));
            AkSoundEngine.PostEvent("Play_WPN_bsg_shot_01", base.aiActor.gameObject);
            yield break;
        }

        private IEnumerator WaitForSecondsInvariant(float time)
        {
            for (float elapsed = 0f; elapsed < time; elapsed += GameManager.INVARIANT_DELTA_TIME) { yield return null; }
            yield break;
        }
    }
}