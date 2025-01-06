using UnityEngine;

namespace AtticAdventures
{
    public class DeathByBoss : MonoBehaviour
    {
        private BossResetter resetter;

        public void Reset()
        {
            resetter = FindObjectOfType<BossResetter>();

            if(resetter != null) resetter.ResetBosses();
        }
    }
}
