using AtticAdventures.Core;
using UnityEngine;

namespace AtticAdventures.StateMachine
{
    public class DiveRollState : BaseState
    {
        public DiveRollState(PlayerController player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            animator.CrossFade(DiveRollHash, crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}
