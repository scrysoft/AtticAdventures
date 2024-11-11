using UnityEngine;

namespace AtticAdventures.Core
{
    [RequireComponent(typeof(Animator))]
    public class LookAtController : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float headWeight;
        [SerializeField] float bodyWeight;
        [SerializeField] float leftArmWeight;
        [SerializeField] float rightArmWeight;
        [SerializeField] Animator animator;
        [SerializeField] float minLookDistance = 10f;

        [SerializeField] bool activated = false;
        private Vector3 baseTargetPosition;

        private void Start()
        {
            animator = GetComponent<Animator>();
            if (target != null) baseTargetPosition = target.position;
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
                    float weight = 1f - (distance / minLookDistance);
                    animator.SetLookAtPosition(target.position);
                    animator.SetLookAtWeight(weight, bodyWeight, headWeight);

                    // Set IK weights for hands
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftArmWeight * weight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftArmWeight * weight);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightArmWeight * weight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightArmWeight * weight);

                    // Keep hands at specific positions if weights are set
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, animator.GetIKPosition(AvatarIKGoal.LeftHand));
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, animator.GetIKRotation(AvatarIKGoal.LeftHand));
                    animator.SetIKPosition(AvatarIKGoal.RightHand, animator.GetIKPosition(AvatarIKGoal.RightHand));
                    animator.SetIKRotation(AvatarIKGoal.RightHand, animator.GetIKRotation(AvatarIKGoal.RightHand));
                }
                else
                {
                    animator.SetLookAtWeight(0);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                }
            }
        }
    }
}
