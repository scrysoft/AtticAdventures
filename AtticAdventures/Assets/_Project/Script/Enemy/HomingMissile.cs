using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 20f;
    public float initialSpeed = 40f;
    public float accelerationTime = 1f;
    public float rotateSpeed = 200f;
    public float lifeTime = 10f;
    public float homingAccuracy = 1f;
    public GameObject explosionPrefab;
    public GameObject otherCollisionPrefab;
    public Vector3 targetOffset = Vector3.zero;
    public float fallSpeed = 50f;

    private Transform target;
    private Rigidbody rb;
    private float currentSpeed;
    private float accelerationTimer;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialSpeed;
        accelerationTimer = accelerationTime;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Kein Objekt mit dem Tag 'Player' gefunden!");
        }

        Invoke("StartFalling", lifeTime);
    }

    void FixedUpdate()
    {
        if (!isFalling)
        {
            if (accelerationTimer > 0f)
            {
                accelerationTimer -= Time.deltaTime;
            }
            else
            {
                currentSpeed = speed;
            }

            if (target != null)
            {
                Vector3 adjustedTargetPosition = target.position + targetOffset;
                Vector3 direction = (adjustedTargetPosition - transform.position).normalized;

                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime * homingAccuracy);

                rb.velocity = transform.forward * currentSpeed;
            }
            else
            {
                rb.velocity = transform.forward * currentSpeed;
            }
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, -fallSpeed, rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
        else
        {
            if (otherCollisionPrefab != null)
            {
                Quaternion upwardRotation = Quaternion.Euler(90f, 0f, 0f);
                Instantiate(otherCollisionPrefab, transform.position, upwardRotation);
            }

            Explode();
        }
    }

    public void Explode()
    {
        if (explosionPrefab != null)
        {
            Opsive.Shared.Game.ObjectPoolBase.Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    private void StartFalling()
    {
        isFalling = true;
    }
}
