using UnityEngine;
using UnityEngine.Audio;

namespace AtticAdventures.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [HideInInspector]
        public AudioMixerGroup output;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        [Range(0f, 1f)]
        public float spacialBlend;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}
