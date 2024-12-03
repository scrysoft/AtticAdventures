using System.Collections.Generic;
using UnityEngine;

public class OwlCollectibleTracker : MonoBehaviour
{
    public static OwlCollectibleTracker Instance { get; private set; }

    [SerializeField]
    private List<GameObject> owls = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DisableAllOwls();
    }

    private void DisableAllOwls()
    {
        foreach (var owl in owls)
        {
            if (owl != null)
                owl.SetActive(false);
        }
    }

    public void ActivateOwl(int index)
    {
        if (index < 0 || index >= owls.Count)
        {
            return;
        }

        if (owls[index] != null)
        {
            owls[index].SetActive(true);
        }
    }
}
