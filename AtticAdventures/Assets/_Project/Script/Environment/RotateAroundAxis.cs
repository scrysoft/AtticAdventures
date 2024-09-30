using UnityEngine;

public class RotateAroundAxis : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Die Achse, um die rotiert wird (standardmäßig Y-Achse)
    public float rotationSpeed = 90f; // Geschwindigkeit der Rotation in Grad pro Sekunde
    private bool isRotating = false; // Kontrolliert, ob die Rotation aktiv ist

    private void Start()
    {
        isRotating = true; // Standardmäßig startet die Rotation
    }

    void Update()
    {
        if (isRotating)
        {
            // Rotiert das Objekt um die definierte Achse mit der festgelegten Geschwindigkeit
            transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // Startet die Rotation
    public void StartRotation()
    {
        isRotating = true;
    }

    // Stoppt die Rotation
    public void StopRotation()
    {
        isRotating = false;
    }

    // Zeichnet ein Gizmo im Editor
    private void OnDrawGizmos()
    {
        // Setze die Gizmo-Farbe
        Gizmos.color = Color.red;

        // Zeichne eine Linie, die die Rotationsachse darstellt
        Gizmos.DrawLine(transform.position, transform.position + rotationAxis.normalized * 2f);

        // Zeichne einen kleinen Würfel an der Position des Objekts zur besseren Sichtbarkeit
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.2f);
    }
}
