
using KBCore.Refs;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class IKMovement : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [Header("IK Settings")]
        [Range(0f, 1f)]
        [SerializeField] float distanceToGround = 0f;
        [SerializeField] LayerMask layerMask;

        private void OnAnimatorIK(int layerIndex)
        {
            if (animator)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

                // LeftFoot
                RaycastHit hit;
                Ray ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

                if (Physics.Raycast(ray, out hit, distanceToGround + 1f, layerMask))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        Vector3 footPosition = hit.point;
                        footPosition.y += distanceToGround;
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                    }
                }

                // RightFoot
                ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

                if (Physics.Raycast(ray, out hit, distanceToGround + 1f, layerMask))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        Vector3 footPosition = hit.point;
                        footPosition.y += distanceToGround;
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                    }
                }
            }
        }
    }
}
