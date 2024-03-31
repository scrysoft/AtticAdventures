using AtticAdventures.Core;
using AtticAdventures.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace AtticAdventures
{
    public class EnemyChaseState : EnemyBaseState
    {
        readonly NavMeshAgent agent;
        readonly Transform player;

        public EnemyChaseState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            animator.CrossFade(RunHash, crossFadeDuration);
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}
