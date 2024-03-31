using AtticAdventures.Core;
using UnityEngine;

namespace AtticAdventures.StateMachine
{
    public class AttackState : BaseState
    {
        public AttackState(PlayerController player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            animator.CrossFade(AttackHash, crossFadeDuration);
            player.Attack();
        }

        public override void FixedUpdate()
        {
            player.HandleJump();
            player.HandleMovement();
        }
    }
}
