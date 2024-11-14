using UnityEngine;

public class RandomChildActivator : MonoBehaviour
{
    void Start()
    {
        int childCount = transform.childCount;
        if (childCount == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, childCount);
        Transform randomChild = transform.GetChild(randomIndex);

        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        randomChild.gameObject.SetActive(true);
    }
}
