using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class BossChecker : MonoBehaviour
    {
        public BossResetter resetter;

        [SerializeField] MusicChanger musicChanger;

        public UnityEvent onFightBegin;

        public void PlayMusic(int index)
        {
            if (resetter.IsBossAlive())
            {
                musicChanger.PlayMusic(index);
                onFightBegin.Invoke();
            }
        }

        public void StopMusic(int index)
        {
            if (resetter.IsBossAlive())
            {
                musicChanger.StopMusic(index);
            }
        }
    }
}
