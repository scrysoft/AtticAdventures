using UnityEngine;

namespace AtticAdventures
{
    public class HitFlashList : MonoBehaviour
    {
        public HitFlash[] hitFlashComponents;

        public void TriggerHitFlashes()
        {
            if (hitFlashComponents == null) return;

            foreach (HitFlash hitFlash in hitFlashComponents)
            {
                hitFlash.TriggerFlash();
            }
        }
    }
}
