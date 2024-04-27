using UnityEngine;

namespace AtticAdventures.Core
{
    [RequireComponent(typeof(Animator))]
    public class LookAtController : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float headWeight;
        [SerializeField] float bodyWeight;
        [SerializeField] Animator animator;
        [SerializeField] float minLookDistance = 10f;

        private bool activated = false;
        private Vector3 baseTargetPosition;

        private void Start()
        {
            animator = GetComponent<Animator>();
            if(target != null) baseTargetPosition = transform.position;
        }

        public void ActivateLookAt(Transform target)
        {
            this.target.position = target.position;
            activated = true;
        }

        public void Deactivate()
        {
            target.position = baseTargetPosition;
            activated = false;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (target != null && activated)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance <= minLookDistance)
                {
                    float weight = 1f - (distance / minLookDistance); // Gewicht basierend auf Entfernung berechnen
                    animator.SetLookAtPosition(target.position);
                    animator.SetLookAtWeight(weight, bodyWeight, headWeight);
                }
                else
                {
                    animator.SetLookAtWeight(0);
                }
            }
        }
    }
}
