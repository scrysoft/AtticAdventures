using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;
using AtticAdventures.StateMachine;
using AtticAdventures.Utilities;

namespace AtticAdventures.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public class Enemy : Entity
    {
        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Self] PlayerDetector playerDetector;
        [SerializeField, Child] Animator animator;

        [SerializeField] float wanderRadius = 10f;
        [SerializeField] float timeBetweenAttacks = 1f;

        private StateMachine.StateMachine stateMachine;

        private CountdownTimer attackTimer;

        private void OnValidate() => this.ValidateRefs();

        private void Start()
        {
            attackTimer = new CountdownTimer(timeBetweenAttacks);

            stateMachine = new StateMachine.StateMachine();

            var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius);
            var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.Player);
            var attackState = new EnemyAttackState(this, animator, agent, playerDetector.Player);

            At(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
            At(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
            At(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
            At(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));

            stateMachine.SetState(wanderState);
        }

        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        private void Update()
        {
            stateMachine.Update();
            attackTimer.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        public void Attack()
        {
            if (attackTimer.IsRunning) return;

            attackTimer.Start();
        }
    }
}
