using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    // Speed parameter name in the animator
    public string speedParameterName = "Speed";
    private Transform player;

    // Hit animations for different directions
    public string hitFrontAnimation1 = "HitFront1";
    public string hitFrontAnimation2 = "HitFront2";
    public string hitBackAnimation = "HitBack";
    public string hitLeftAnimation = "HitLeft";
    public string hitRightAnimation = "HitRight";

    // Attack animations
    public string[] leftHandAttacks = { "L1", "L2", "L3" };
    public string[] rightHandAttacks = { "R1", "R2", "R3" };

    private int attackIndex = 0;

    // Spawn points for the fists
    public Transform leftHandSpawn;
    public Transform rightHandSpawn;

    // Prefab to spawn (e.g., a hit effect or projectile)
    public GameObject hitEffectPrefab;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the boss is stunned
        bool isStunned = animator.GetBool("IsStunned");

        if (isStunned)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
        else
        {
            agent.isStopped = false;

            // Get the velocity of the agent and convert it to the local space of the agent
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            // Calculate the forward speed (movement in the z-direction)
            float forwardSpeed = localVelocity.z;

            // Pass the forward speed to the animator
            animator.SetFloat(speedParameterName, forwardSpeed);
        }
    }

    // Function to be called when the character is hit
    public void OnHit()
    {
        Vector3 attackerPosition = player.position;
        Vector3 hitDirection = (attackerPosition - transform.position).normalized;
        Vector3 localHitDirection = transform.InverseTransformDirection(hitDirection);
        float angle = Mathf.Atan2(localHitDirection.x, localHitDirection.z) * Mathf.Rad2Deg;

        if (angle > -45 && angle < 45)
        {
            int randomFront = Random.Range(0, 2);
            if (randomFront == 0)
                animator.Play(hitFrontAnimation1);
            else
                animator.Play(hitFrontAnimation2);
        }
        else if (angle >= 45 && angle < 135)
            animator.Play(hitRightAnimation);
        else if (angle <= -45 && angle > -135)
            animator.Play(hitLeftAnimation);
        else
            animator.Play(hitBackAnimation);
    }

    // Function to perform an attack
    public void Attack()
    {
        if (attackIndex % 2 == 0)
        {
            string leftHandAttack = leftHandAttacks[attackIndex / 2];
            animator.Play(leftHandAttack);
        }
        else
        {
            string rightHandAttack = rightHandAttacks[attackIndex / 2];
            animator.Play(rightHandAttack);
        }

        attackIndex = (attackIndex + 1) % 6;
    }

    // Function to spawn the hit effect - this will be called from the animation event
    public void SpawnHitEffect(string hand)
    {
        if (hitEffectPrefab == null) return;

        Transform spawnPoint = hand == "left" ? leftHandSpawn : rightHandSpawn;

        if (spawnPoint != null)
        {
            Opsive.Shared.Game.ObjectPoolBase.Instantiate(hitEffectPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
