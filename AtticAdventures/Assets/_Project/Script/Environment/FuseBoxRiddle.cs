using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuseBoxRiddle : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectBoolPair
    {
        public GameObject gameObject;
        public bool state;
        public Material material;

        public GameObjectBoolPair(GameObject gameObject, bool state, Material material)
        {
            this.gameObject = gameObject;
            this.state = state;
            this.material = material;
        }
    }

    public List<GameObjectBoolPair> objectBoolPairs = new List<GameObjectBoolPair>();

    public UnityEvent onIndex0Activated;
    public UnityEvent onIndex1Activated;
    public UnityEvent onIndex2Activated;

    public Color emissiveColor = Color.green;
    public float emissiveIntensity = 5f;

    private void Start()
    {
        foreach (GameObjectBoolPair pair in objectBoolPairs)
        {
            if (pair.material != null)
            {
                ResetEmissiveMaterial(pair.material);
            }
        }
    }

    public void SetState(int index)
    {
        if (index >= 0 && index < objectBoolPairs.Count)
        {
            objectBoolPairs[index].state = true;
            objectBoolPairs[index].gameObject.SetActive(true);

            SetEmissiveMaterial(objectBoolPairs[index].material);

            TriggerEvent(index);
        }
    }

    private void SetEmissiveMaterial(Material material)
    {
        if (material != null)
        {
            material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
        }
    }

    private void ResetEmissiveMaterial(Material material)
    {
        if (material != null)
        {
            material.SetColor("_EmissiveColor", Color.black * emissiveIntensity);
        }
    }

    private void TriggerEvent(int index)
    {
        switch (index)
        {
            case 0:
                onIndex0Activated?.Invoke();
                break;
            case 1:
                onIndex1Activated?.Invoke();
                break;
            case 2:
                onIndex2Activated?.Invoke();
                break;
            default:
                break;
        }
    }

    [ContextMenu("Test Index 0")]
    public void TestIndex0()
    {
        SetState(0);
    }

    [ContextMenu("Test Index 1")]
    public void TestIndex1()
    {
        SetState(1);
    }

    [ContextMenu("Test Index 2")]
    public void TestIndex2()
    {
        SetState(2);
    }
}
