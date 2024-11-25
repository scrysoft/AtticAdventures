using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Opsive.UltimateCharacterController.Character.Abilities;
using AtticAdventures.EventSystem;
using Opsive.UltimateCharacterController.Traits;

namespace AtticAdventures.UI
{
    [System.Serializable]
    public class DialogueEntry
    {
        public string name;
        [TextArea] public string text;
        public AudioClip audioClip;
        public float delay;
        public UnityEvent onDialogueStart;
        public UnityEvent onDialogueEnd;
    }

    public class InteractableDialogue : MonoBehaviour, IInteractableTarget
    {
        [SerializeField] private List<DialogueEntry> dialogues;
        [SerializeField] private StringEventChannel stringEventChannel;
        [SerializeField] private AudioSource audioSource;
        
        private CanvasGroup canvasGroup;
        private float fadeDuration = 0.2f;

        private Coroutine dialogueCoroutine;

        private bool dialogueIsRunning = false;

        public bool CanInteract(GameObject character, Interact interactAbility)
        {
            return !dialogueIsRunning;
        }

        public void Interact(GameObject character, Interact interactAbility)
        {
            StartDialogue();
        }

        public void StartDialogue()
        {
            canvasGroup = FindObjectOfType<DialogueUI>().GetCanvasGroup();

            if (dialogueCoroutine == null)
            {
                stringEventChannel?.Invoke("");
                dialogueCoroutine = StartCoroutine(PlayDialogueSequence());
            }
        }

        public void StopDialogue()
        {
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);
                dialogueCoroutine = null;
            }
        }

        private IEnumerator PlayDialogueSequence()
        {
            foreach (var dialogue in dialogues)
            {
                dialogueIsRunning = true;
                dialogue.onDialogueStart?.Invoke();

                yield return StartCoroutine(FadeCanvasGroup(1, fadeDuration));

                stringEventChannel?.Invoke($"{dialogue.name}: {dialogue.text}");

                if (dialogue.audioClip != null && audioSource != null)
                {
                    audioSource.clip = dialogue.audioClip;
                    audioSource.Play();

                    yield return new WaitForSeconds(audioSource.clip.length);
                }

                if (dialogue.delay > 0)
                {
                    yield return new WaitForSeconds(dialogue.delay);
                }

                yield return StartCoroutine(FadeCanvasGroup(0, fadeDuration));
                
                stringEventChannel?.Invoke("");
                dialogue.onDialogueEnd?.Invoke();
            }

            dialogueCoroutine = null;
            dialogueIsRunning = false;
        }

        private IEnumerator FadeCanvasGroup(float targetAlpha, float duration)
        {
            if (canvasGroup == null) yield break;

            float startAlpha = canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
        }
    }
}
