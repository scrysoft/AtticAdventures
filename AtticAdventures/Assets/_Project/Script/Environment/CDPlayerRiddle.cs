using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CDPlayerRiddle : MonoBehaviour
{
    public Color green;
    public Color red;

    public List<Pair> boolPairs;
    public UnityEvent onAllCorrect;

    private HashSet<int> correctGuesses = new HashSet<int>();
    private Coroutine resetCoroutine;

    private bool gameComplete = false;

    private void Awake()
    {
        SetRandomBooleans();
        ResetAllMaterials();
    }

    private void SetRandomBooleans()
    {
        foreach (var pair in boolPairs)
        {
            pair.value = false;
        }

        int count = 3;
        while (count > 0)
        {
            int randomIndex = Random.Range(0, boolPairs.Count);
            if (!boolPairs[randomIndex].value)
            {
                boolPairs[randomIndex].value = true;
                count--;
            }
        }
    }

    public void CheckBoolAtIndex(int index)
    {
        if (gameComplete) return;

        if (index < 0 || index >= boolPairs.Count)
        {
            return;
        }

        bool isTrue = boolPairs[index].value;

        if (boolPairs[index].gameObject != null)
        {
            if (isTrue)
            {
                SetEmissionColor(boolPairs[index].gameObject, green);
                correctGuesses.Add(index);

                if (correctGuesses.Count == 3)
                {
                    onAllCorrect?.Invoke();
                    gameComplete = true;
                    ResetAllMaterials();
                }
            }
            else
            {
                if (resetCoroutine != null)
                {
                    StopCoroutine(resetCoroutine);
                }
                resetCoroutine = StartCoroutine(ResetMaterials(boolPairs[index].gameObject));
            }
        }
    }

    private IEnumerator ResetMaterials(GameObject incorrectObject)
    {
        SetEmissionColor(incorrectObject, red);
        yield return new WaitForSeconds(1f);
        ResetAllMaterials();
        correctGuesses.Clear();
    }

    private void ResetAllMaterials()
    {
        foreach (var pair in boolPairs)
        {
            if (pair.gameObject != null)
            {
                SetEmissionColor(pair.gameObject, Color.black);
            }
        }
    }

    private void SetEmissionColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            Material material = renderer.material;
            material.SetColor("_EmissiveColor", color);
        }
    }

    [System.Serializable]
    public class Pair
    {
        public GameObject gameObject;
        public bool value;
    }
}
