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
        private float baseSpeed;

        public EnemyChaseState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            baseSpeed = agent.speed;
            agent.speed = 4.0f;
            animator.CrossFade(RunHash, crossFadeDuration);
        }

        public override void OnExit()
        {
            agent.speed = baseSpeed;
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}
