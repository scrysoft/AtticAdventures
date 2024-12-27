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
        public enum DialogueMode { Sequential, Random }
        public enum DialogueProgressMode { Automatic, Manual }

        [SerializeField] private DialogueMode dialogueMode = DialogueMode.Sequential;
        [SerializeField] private DialogueProgressMode progressMode = DialogueProgressMode.Automatic;
        [SerializeField] private List<DialogueEntry> dialogues;
        [SerializeField] private StringEventChannel stringEventChannel;
        [SerializeField] private AudioSource audioSource;

        private DialogueUI dialogueUI;
        private Coroutine dialogueCoroutine;
        private bool dialogueIsRunning = false;

        private int currentDialogueIndex = -1;
        private int lastRandomIndex = -1;

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
            dialogueUI = FindObjectOfType<DialogueUI>();

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

        public void ContinueDialogue()
        {
            if (!dialogueIsRunning || progressMode != DialogueProgressMode.Manual)
            {
                return;
            }

            if (dialogueCoroutine == null)
            {
                dialogueCoroutine = StartCoroutine(PlayDialogueSequence());
            }
        }

        private IEnumerator PlayDialogueSequence()
        {
            dialogueIsRunning = true;

            if (dialogueMode == DialogueMode.Random)
            {
                int randomIndex = GetNextRandomIndex();
                yield return PlayDialogueAtIndex(randomIndex);
            }
            else if (dialogueMode == DialogueMode.Sequential)
            {
                while (true)
                {
                    var dialogueIndex = GetNextDialogueIndex();
                    yield return PlayDialogueAtIndex(dialogueIndex);

                    if (progressMode == DialogueProgressMode.Manual)
                    {
                        // Warten, bis ContinueDialogue aufgerufen wird
                        dialogueCoroutine = null;
                        yield break;
                    }

                    if (dialogueIndex == dialogues.Count - 1)
                    {
                        break;
                    }
                }
            }

            dialogueCoroutine = null;
            dialogueIsRunning = false;
        }

        public void SetDialogueIsRunning(bool value)
        {
            dialogueIsRunning = value;

            if(value == false)
            {
                dialogueCoroutine = null;
                currentDialogueIndex = -1;
    }
        }

        private int GetNextDialogueIndex()
        {
            if (dialogues.Count == 1)
            {
                return 0;
            }

            if (dialogueMode == DialogueMode.Sequential)
            {
                currentDialogueIndex = (currentDialogueIndex + 1) % dialogues.Count;
                return currentDialogueIndex;
            }

            return -1;
        }

        private int GetNextRandomIndex()
        {
            if (dialogues.Count == 1)
            {
                return 0;
            }

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, dialogues.Count);
            } while (randomIndex == lastRandomIndex);

            lastRandomIndex = randomIndex;
            return randomIndex;
        }

        private IEnumerator PlayDialogueAtIndex(int index)
        {
            if(dialogueUI == null)
            {
                dialogueUI = FindObjectOfType<DialogueUI>();
            }

            var dialogue = dialogues[index];

            dialogue.onDialogueStart?.Invoke();

            yield return dialogueUI.FadeCanvasGroup(1);

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

            yield return dialogueUI.FadeCanvasGroup(0);

            stringEventChannel?.Invoke("");

            dialogue.onDialogueEnd?.Invoke();
        }

        public void PlaySpecificDialogue(int index)
        {
            if (index < 0 || index >= dialogues.Count)
            {
                return;
            }

            if (dialogueCoroutine == null)
            {
                dialogueCoroutine = StartCoroutine(PlayDialogueAtIndex(index));
            }
        }
    }
}
