using UnityEngine;

namespace AtticAdventures
{
    public class ChipFuseActivator : MonoBehaviour
    {
        [SerializeField] GameObject fuse;


        public void ToggleFuse(bool value)
        {
            fuse.SetActive(value);
        }
    }
}
