using UnityEngine;

public class ObjectPositionSetter : MonoBehaviour
{
    public CDPlayerRiddle randomBoolSetter;
    public Transform[] childObjects;

    private void Start()
    {
        if (randomBoolSetter == null || childObjects.Length == 0)
        {
            return;
        }

        SetChildPositions();
    }

    private void SetChildPositions()
    {
        if (randomBoolSetter.boolPairs.Count != childObjects.Length)
        {
            return;
        }


        for (int i = 0; i < randomBoolSetter.boolPairs.Count; i++)
        {
            if (randomBoolSetter.boolPairs[i].value)
            {
                Vector3 currentPosition = childObjects[i].localPosition;
                currentPosition.y -= 0.006f;
                childObjects[i].localPosition = currentPosition;
            }
        }
    }
}
