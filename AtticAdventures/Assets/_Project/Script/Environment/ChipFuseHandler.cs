using UnityEngine;

namespace AtticAdventures
{
    public class ChipFuseHandler : MonoBehaviour
    {
        public void ToggleFuse(bool value)
        {
            ChipFuseActivator chipFuseActivator = FindAnyObjectByType<ChipFuseActivator>();

            if (chipFuseActivator != null) 
            {
                chipFuseActivator.ToggleFuse(value);
            }
        }
    }
}
