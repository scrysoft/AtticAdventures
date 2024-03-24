using UnityEngine;

namespace AtticAdventures.Core
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistance = 0.08f;
        [SerializeField] LayerMask groundLayers;

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            IsGrounded = Physics.SphereCast(transform.position + new Vector3(0f, 0.1f, 0f), groundDistance, Vector3.down, out _, groundDistance, groundLayers);
        }
    }
}
