using UnityEngine;
using System.Collections.Generic;

public class RandomizeChildOrder : MonoBehaviour
{
    public Transform parentTransform;

    void Start()
    {
        parentTransform = transform;

        List<Transform> children = new List<Transform>();
        foreach (Transform child in parentTransform)
        {
            children.Add(child);
        }

        Shuffle(children);

        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    private void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            Transform temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
