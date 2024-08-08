using UnityEngine;

namespace AtticAdventures.Core
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] float radius = 4f;
        [SerializeField] bool isActive = true;

        private void Start() => UpdateScale();

        public void SetScale(float value)
        {
            radius = value;
            UpdateScale();
        }

        public void SetActivity(bool value)
        {
            isActive = value;
        }

        public bool GetActivity()
        {
            return isActive;
        }

        private void UpdateScale()
        {
            transform.localScale = new Vector3(radius, radius, radius);
        }
    }
}
