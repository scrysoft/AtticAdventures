using UnityEngine;

namespace AtticAdventures
{
    public class OwlMessenger : MonoBehaviour
    {
        public void OwlCollected(int index)
        {
            OwlCollectibleTracker.Instance.ActivateOwl(index);
        }
    }
}
