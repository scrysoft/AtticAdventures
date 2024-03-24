using UnityEngine;

namespace AtticAdventures.Environment
{
    public class PlatformCollisionHandler : MonoBehaviour
    {
        private Transform platform;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                // If the contact normal is pointing up
                ContactPoint contact = other.GetContact(0);

                if (contact.normal.y < 0.5f) return;

                platform = other.transform;
                transform.SetParent(platform);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(null);
                platform = null;
            }
        }
    }
}
