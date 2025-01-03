using UnityEngine;
using System;
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
            if(instance == null)
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

        public void PlayMusic(string name)
        {
            Sound s = Array.Find(music, sound => sound.name == name);

            if (s == null) 
            {
                Debug.LogWarning("Sound: " + name + " not found in Music Sources.");
                return;
            }

            Debug.Log(s.source.clip.name);
            s.source.Play();
        }

        public void StopMusic(string name)
        {
            Sound s = Array.Find(music, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found in Music Sources.");
                return;
            }

            Debug.Log(s.source.clip.name);
            s.source.Stop();
        }

        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfx, sound => sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found in SFX Sources.");
                return;
            }

            s.source.Play();
        }

        private void SetupAudioSource(string name, Sound[] sounds, AudioMixerGroup output)
        {
            GameObject childObject = new GameObject(name);
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
