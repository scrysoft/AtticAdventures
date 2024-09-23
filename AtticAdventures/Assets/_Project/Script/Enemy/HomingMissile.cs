using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 20f;           // Normale Geschwindigkeit der Rakete
    public float initialSpeed = 40f;    // Anfangsgeschwindigkeit der Rakete
    public float accelerationTime = 1f; // Zeit, nach der die Rakete von der Anfangsgeschwindigkeit auf die normale Geschwindigkeit wechselt
    public float rotateSpeed = 200f;    // Rotationsgeschwindigkeit der Rakete
    public float lifeTime = 10f;        // Lebensdauer der Rakete in Sekunden
    public float homingAccuracy = 1f;   // Genauigkeit des Ziels, 1f für direktes Verfolgen
    public GameObject explosionPrefab;  // Prefab für die Explosion
    public Vector3 targetOffset = Vector3.zero; // Offset zur Spielerposition
    public float fallSpeed = 50f;       // Zusätzliche Sinkgeschwindigkeit beim Absturz

    private Transform target; // Das Ziel der Rakete wird automatisch gesetzt
    private Rigidbody rb;
    private float currentSpeed;    // Variable zur Speicherung der aktuellen Geschwindigkeit
    private float accelerationTimer; // Timer für die Geschwindigkeitsanpassung
    private bool isFalling = false;  // Gibt an, ob die Rakete zu Boden stürzt

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Setze die Anfangsgeschwindigkeit und starte den Timer
        currentSpeed = initialSpeed;
        accelerationTimer = accelerationTime;

        // Suche nach einem Objekt mit dem Tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Wenn ein Player gefunden wird, setze es als Ziel
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Kein Objekt mit dem Tag 'Player' gefunden!");
        }

        // Zerstöre die Rakete nach Ablauf der Lebenszeit und starte den Sturz
        Invoke("StartFalling", lifeTime);
    }

    void FixedUpdate()
    {
        if (!isFalling)
        {
            // Geschwindigkeitsanpassung: Nach einer gewissen Zeit zur normalen Geschwindigkeit wechseln
            if (accelerationTimer > 0f)
            {
                accelerationTimer -= Time.deltaTime;
            }
            else
            {
                currentSpeed = speed; // Wechsel zur normalen Geschwindigkeit nach Ablauf des Timers
            }

            if (target != null)
            {
                // Berechne die Richtung zum Ziel mit Offset
                Vector3 adjustedTargetPosition = target.position + targetOffset; // Offset zur Spielerposition hinzufügen
                Vector3 direction = (adjustedTargetPosition - transform.position).normalized;

                // Berechne die Rotation in Richtung des Ziels
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                // Dreht die Rakete allmählich in Richtung des Ziels
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime * homingAccuracy);

                // Bewegt die Rakete vorwärts
                rb.velocity = transform.forward * currentSpeed;
            }
            else
            {
                // Optional: Verhalten wenn kein Ziel vorhanden ist
                rb.velocity = transform.forward * currentSpeed; // Fliegt geradeaus weiter
            }
        }
        else
        {
            // Bei Sturz: Behalte Vorwärtsgeschwindigkeit bei, aber sinke schnell nach unten
            rb.velocity = new Vector3(rb.velocity.x, -fallSpeed, rb.velocity.z); // Fallgeschwindigkeit nach unten
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null || isFalling)
        Explode();
    }

    public void Explode()
    {
        // Explosion wird an der Position der Rakete gespawnt
        if (explosionPrefab != null)
        {
            Opsive.Shared.Game.ObjectPoolBase.Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        // Zerstöre die Rakete nach der Explosion
        Destroy(gameObject);
    }

    private void StartFalling()
    {
        isFalling = true;  // Verhindert, dass die Rakete weiter das Ziel verfolgt
        // Geschwindigkeit wird nicht auf null gesetzt, die Rakete behält ihre Vorwärtsgeschwindigkeit bei
    }
}
