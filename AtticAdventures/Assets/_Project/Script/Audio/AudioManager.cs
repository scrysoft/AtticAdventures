using UnityEngine;
using UnityEngine.Audio;

namespace AtticAdventures.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioMixerGroup musicOutput;
        [SerializeField] AudioMixerGroup sfxOutput;

        public Sound[] music;
        public Sound[] sfx;

        public static AudioManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            SetupAudioSource("MusicSources", music, musicOutput);
            SetupAudioSource("SFXSources", sfx, sfxOutput);
        }

        public void PlayMusic(int index)
        {
            if (index < 0 || index >= music.Length) return;
            Sound s = music[index];
            Debug.Log($"Playing Music Clip: {s.source.clip.name}");
            s.source.Play();
        }

        public void StopMusic(int index)
        {
            if (index < 0 || index >= music.Length) return;
            Sound s = music[index];
            Debug.Log($"Stopping Music Clip: {s.source.clip.name}");
            s.source.Stop();
        }

        public void PlaySFX(int index)
        {
            if (index < 0 || index >= sfx.Length) return;
            Sound s = sfx[index];
            Debug.Log($"Playing SFX Clip: {s.source.clip.name}");
            s.source.Play();
        }

        private void SetupAudioSource(string childName, Sound[] sounds, AudioMixerGroup output)
        {
            GameObject childObject = new GameObject(childName);
            childObject.transform.parent = transform;

            foreach (Sound s in sounds)
            {
                s.source = childObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = output;
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                s.source.pitch = s.pitch;
                s.source.spatialBlend = s.spacialBlend;
            }
        }
    }
}
