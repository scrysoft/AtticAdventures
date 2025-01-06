using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace AtticAdventures.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup musicOutput;
        [SerializeField] private AudioMixerGroup sfxOutput;
        public float fadeDuration = 1f;
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
            Sound newSound = music[index];
            Sound currentlyPlaying = GetCurrentlyPlayingMusic();
            if (currentlyPlaying != null && currentlyPlaying != newSound)
            {
                StartCoroutine(FadeOutAndIn(currentlyPlaying, newSound, fadeDuration));
            }
            else
            {
                StartCoroutine(FadeIn(newSound, fadeDuration));
            }
        }

        public void StopMusic(int index)
        {
            if (index < 0 || index >= music.Length) return;
            Sound s = music[index];
            if (!s.source.isPlaying || s.source.volume <= 0f) return;
            StartCoroutine(FadeOut(s, fadeDuration));
        }

        public void PlaySFX(int index)
        {
            if (index < 0 || index >= sfx.Length) return;
            Sound s = sfx[index];
            s.source.volume = s.targetVolume;
            s.source.Play();
        }

        private IEnumerator FadeOutAndIn(Sound oldSound, Sound newSound, float duration)
        {
            yield return StartCoroutine(FadeOut(oldSound, duration));
            yield return StartCoroutine(FadeIn(newSound, duration));
        }

        private IEnumerator FadeOut(Sound s, float duration)
        {
            float startVolume = s.source.volume;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                s.source.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
                yield return null;
            }
            s.source.volume = 0f;
            s.source.Stop();
        }

        private IEnumerator FadeIn(Sound s, float duration)
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
            }
            float startVolume = s.source.volume;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                s.source.volume = Mathf.Lerp(startVolume, s.targetVolume, elapsed / duration);
                yield return null;
            }
            s.source.volume = s.targetVolume;
        }

        private Sound GetCurrentlyPlayingMusic()
        {
            foreach (Sound m in music)
            {
                if (m.source.isPlaying && m.source.volume > 0f) return m;
            }
            return null;
        }

        private void SetupAudioSource(string childName, Sound[] sounds, AudioMixerGroup output)
        {
            GameObject childObject = new GameObject(childName);
            childObject.transform.SetParent(transform);
            foreach (Sound s in sounds)
            {
                s.source = childObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = output;
                s.source.clip = s.clip;
                s.source.volume = 0f;
                s.source.loop = s.loop;
                s.source.pitch = s.pitch;
                s.source.spatialBlend = s.spacialBlend;
                s.targetVolume = s.volume;
            }
        }
    }
}
