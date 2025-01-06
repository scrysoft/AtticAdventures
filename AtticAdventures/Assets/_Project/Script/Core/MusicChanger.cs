using AtticAdventures.Audio;
using UnityEngine;

namespace AtticAdventures
{
    public class MusicChanger : MonoBehaviour
    {
        public void PlayMusic(int index)
        {
            AudioManager.instance.PlayMusic(index);
        }

        public void StopMusic(int index)
        {
            AudioManager.instance.StopMusic(index);
        }
    }
}
