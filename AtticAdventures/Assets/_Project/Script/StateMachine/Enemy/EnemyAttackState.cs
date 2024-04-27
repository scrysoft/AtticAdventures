using AtticAdventures.Core;
using UnityEngine;
using UnityEngine.AI;

namespace AtticAdventures.StateMachine
{
    public class EnemyAttackState : EnemyBaseState
    {
        readonly NavMeshAgent agent;
        readonly Transform player;
        private float baseSpeed;

        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            baseSpeed = agent.speed;
            agent.speed = 0f;
            animator.CrossFade(AttackHash, crossFadeDuration);
        }

        public override void OnExit()
        {
            agent.speed = baseSpeed;
        }

        public override void Update()
        {
            Vector3 directionToPlayer = player.position - agent.transform.position;

            Vector3 targetPosition = player.position - directionToPlayer.normalized;

            agent.SetDestination(targetPosition);

            enemy.Attack();
        }
    }
}