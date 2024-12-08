using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class ObjectListDeactivator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> objectsToDeactivate = new List<GameObject>();

        public void DeactivateAllObjects()
        {
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
