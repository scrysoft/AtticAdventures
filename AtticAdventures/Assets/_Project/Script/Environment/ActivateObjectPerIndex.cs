
using UnityEngine;

namespace AtticAdventures
{
    public class ActivateObjectPerIndex : MonoBehaviour
    {
        private ObjectActivatorPerIndex objectActivatorPerIndex;

        void Start()
        {
            objectActivatorPerIndex = FindObjectOfType<ObjectActivatorPerIndex>();
        }

        public void ActivatePerIndex(int index)
        {
            objectActivatorPerIndex.ActivateObject(index);
        }
    }
}
