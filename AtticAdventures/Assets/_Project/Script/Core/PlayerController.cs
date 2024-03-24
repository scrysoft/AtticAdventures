using AtticAdventures.Input;
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
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;


        private const float ZeroF = 0f;
        private Transform mainCamera;

        private float currentSpeed;
        private float velocity;
        private float jumpVelocity;

        private Vector3 movement;

        private List<Timer> timers;
        private CountdownTimer jumpTimer;
        private CountdownTimer jumpCooldownTimer;

        // Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            mainCamera = Camera.main.transform;
            freeLookVirtualCam.Follow = transform;
            freeLookVirtualCam.LookAt = transform;
            freeLookVirtualCam.OnTargetObjectWarped(transform, transform.position - freeLookVirtualCam.transform.position - Vector3.forward);

            rb.freezeRotation = true;

            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<Timer>(2) { jumpTimer, jumpCooldownTimer };

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
        }

        private void Start()
        {
            input.EnablePlayerActions();
        }

        private void OnEnable()
        {
            input.Jump += OnJump;
        }

        private void OnDisable()
        {
            input.Jump -= OnJump;
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

        private void Update()
        {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);

            HandleTimers();

            UpdateAnimator();
        }

        private void FixedUpdate()
        {
            HandleJump();
            HandleMovement();
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

        private void HandleJump()
        {
            // If not jumping and grounded keep velocity at zero
            if(!jumpTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpVelocity = ZeroF;
                jumpTimer.Stop();
                return;
            }

            // If jumping or falling calculate velocity
            if (jumpTimer.IsRunning)
            {
                // Progress point for initial burst of velocity
                float launchPoint = 0.9f;

                if(jumpTimer.Progress > launchPoint)
                {
                    // Calculate the velocity required to reach the jump height
                    jumpVelocity = Mathf.Sqrt(2 * jumpMaxHeight * Mathf.Abs(Physics.gravity.y));
                }
                else
                {
                    // Gradually apply less velocity as the jump progresses
                    jumpVelocity += (1 - jumpTimer.Progress) * jumpForce * Time.fixedDeltaTime;
                }
            }
            else
            {
                // Gravity takes over
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            // Apply velocity
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        private void HandleMovement()
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
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
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
