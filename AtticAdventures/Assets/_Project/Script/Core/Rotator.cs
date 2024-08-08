using UnityEngine;

namespace AtticAdventures.Core
{
    public class Rotator : MonoBehaviour
    {
        float rotationSpeed = 60.0f;

        void Update()
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
