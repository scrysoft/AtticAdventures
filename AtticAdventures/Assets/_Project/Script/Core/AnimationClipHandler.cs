using UnityEngine;
using UnityEngine.AI;

namespace AtticAdventures
{
    public class AnimationClipHandler : MonoBehaviour
    {
        [SerializeField] Animation animationComponent;
        [SerializeField] NavMeshAgent navMeshAgent;

        [SerializeField] string idleName = "A_idle";
        [SerializeField] string deathName = "B_dead";
        [SerializeField] string hitName = "A_hit";
        [SerializeField] string walkName = "A_walk";

        [SerializeField] float walkAnimationSpeed = 1.5f; // Adjust this value to control the speed

        private void Start()
        {
            if (animationComponent == null) return;
            PlayIdleAnimation();
        }

        private void Update()
        {
            if (navMeshAgent == null || animationComponent == null) return;

            if (navMeshAgent.velocity.magnitude > 0.1f && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                PlayWalkAnimation();
            }
        }

        public void PlayWalkAnimation()
        {
            if (animationComponent == null || animationComponent.IsPlaying(walkName)) return;

            animationComponent[walkName].speed = walkAnimationSpeed;

            animationComponent.Play(walkName);
        }

        public void PlayIdleAnimation()
        {
            if (animationComponent == null || animationComponent.IsPlaying(idleName)) return;
            animationComponent.Play(idleName);
        }

        public void PlayerDeathAnimation()
        {
            if (animationComponent == null) return;
            animationComponent.Play(deathName);
        }

        public void PlayHitAnimation()
        {
            if (animationComponent == null) return;
            animationComponent.Play(hitName);
        }
    }
}
