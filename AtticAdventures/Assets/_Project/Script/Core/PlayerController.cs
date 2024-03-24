using AtticAdventures.Input;
using AtticAdventures.StateMachine;
using AtticAdventures.Utilities;
using Cinemachine;
using KBCore.Refs;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVirtualCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float gravityMultiplier = 3f;

        [Header("DiveRoll")]
        [SerializeField] float diveRollForce = 10f;
        [SerializeField] float diveRollDuration = 1f;
        [SerializeField] float diveRollCooldown = 2f;


        private const float ZeroF = 0f;
        private Transform mainCamera;

        private float currentSpeed;
        private float velocity;
        private float jumpVelocity;
        private float diveRollVelocity = 1f;

        private Vector3 movement;

        private List<Timer> timers;

        // Jumping
        private CountdownTimer jumpTimer;
        private CountdownTimer jumpCooldownTimer;

        // DiveRoll
        CountdownTimer diveRollTimer;
        CountdownTimer diveRollCooldownTimer;

        private StateMachine.StateMachine stateMachine;

        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            mainCamera = Camera.main.transform;
            freeLookVirtualCam.Follow = transform;
            freeLookVirtualCam.LookAt = transform;
            freeLookVirtualCam.OnTargetObjectWarped(transform, transform.position - freeLookVirtualCam.transform.position - Vector3.forward);

            rb.freezeRotation = true;

            // Setup Timers
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);

            jumpTimer.OnTimerStart += () => jumpVelocity = jumpForce;
            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

            diveRollTimer = new CountdownTimer(diveRollDuration);
            diveRollCooldownTimer = new CountdownTimer(diveRollCooldown);
            diveRollTimer.OnTimerStart += () => diveRollVelocity = diveRollForce;
            diveRollTimer.OnTimerStop += () =>
            {
                diveRollVelocity = 1f;
                diveRollCooldownTimer.Start();
            };

            timers = new List<Timer>(4) { jumpTimer, jumpCooldownTimer, diveRollTimer, diveRollCooldownTimer };

            // State Machine
            stateMachine = new StateMachine.StateMachine();

            // Declare States
            var locomotionState = new LocomotionState(this, animator);
            var jumpState = new JumpState(this, animator);
            var diveRollState = new DiveRollState(this, animator);

            // Define Transitions
            At(locomotionState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
            At(locomotionState, diveRollState, new FuncPredicate(() => diveRollTimer.IsRunning));
            Any(locomotionState, new FuncPredicate(() => groundChecker.IsGrounded && !jumpTimer.IsRunning && !diveRollTimer.IsRunning));

            // Set initial state
            stateMachine.SetState(locomotionState);
        }

        private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        private void Start()
        {
            input.EnablePlayerActions();
        }

        private void OnEnable()
        {
            input.Jump += OnJump;
            input.DiveRoll += OnDiveRoll;
        }

        private void OnDisable()
        {
            input.Jump -= OnJump;
            input.DiveRoll -= OnDiveRoll;
        }

        private void OnJump(bool performed)
        {
            if(performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpTimer.Start();
            }else if (!performed && jumpTimer.IsRunning)
            {
                jumpTimer.Stop();
            }
        }

        private void OnDiveRoll()
        {
            if(!diveRollTimer.IsRunning && !diveRollCooldownTimer.IsRunning )
            {
                diveRollTimer.Start();
            }
        }

        private void Update()
        {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            stateMachine.Update();

            HandleTimers();

            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed);
        }

        private void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void HandleJump()
        {
            // If not jumping and grounded keep velocity at zero
            if(!jumpTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpVelocity = ZeroF;
                jumpTimer.Stop();
                return;
            }

            // If jumping or falling calculate velocity
            if (!jumpTimer.IsRunning)
            {
                // Gravity takes over
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            // Apply velocity
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        public void HandleMovement()
        {
            // Rotates movement direction to match camera rotation
            Vector3 adjustedDirection = Quaternion.AngleAxis(mainCamera.eulerAngles.y, Vector3.up) * movement;

            if(adjustedDirection.magnitude > ZeroF)
            {
                // Adjust rotation to match movement direction
                HandleRotation(adjustedDirection);
                
                // Move the player
                HandleHorizontalMovement(adjustedDirection);

                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);

                // Reset horizontal velocity for a snappy stop
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }

        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            Vector3 velocity = adjustedDirection * (moveSpeed * diveRollVelocity * Time.fixedDeltaTime);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        private void HandleRotation(Vector3 adjustedDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }

        private void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}
