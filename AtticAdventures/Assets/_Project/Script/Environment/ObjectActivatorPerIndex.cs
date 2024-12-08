using System.Collections.Generic;
using UnityEngine;

public class ObjectActivatorPerIndex : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;

    public void ActivateObject(int index)
    {
        if (index < 0 || index >= objects.Count)
        {
            return;
        }

        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].SetActive(i == index);
        }
    }

    public void DeactivateObject(int index)
    {
        if (index < 0 || index >= objects.Count)
        {
            return;
        }

        objects[index].SetActive(false);
    }
}
