using AtticAdventures.Audio;
using UnityEngine;

namespace AtticAdventures
{
    public class MusicChanger : MonoBehaviour
    {
        public void PlayMusic(string name)
        {
            AudioManager.instance.PlayMusic(name);
            Debug.Log("Play Audio: " + name);
        }

        public void StopMusic(string name)
        {
            AudioManager.instance.StopMusic(name);
            Debug.Log("Stop Audio: " + name);
        }
    }
}
