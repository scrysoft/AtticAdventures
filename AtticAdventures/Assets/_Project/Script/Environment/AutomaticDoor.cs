using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public AnimationCurve easeCurve; // Kann in der UI gesetzt werden, für eine benutzerdefinierte Ease-in/Ease-out-Kurve
    public float duration = 2.0f; // Dauer der Rotation

    // Öffentliche Funktion, um die Rotation zu starten
    public void RotateOverTime()
    {
        StartCoroutine(RotateCoroutine());
    }

    // Coroutine für die Rotation
    private IEnumerator RotateCoroutine()
    {
        float timeElapsed = 0f;
        Quaternion startRotation = transform.localRotation; // Startrotation (local)
        Quaternion targetRotation = Quaternion.Euler(-90f, 0f, 0f) * startRotation; // Zielrotation (local), -90 Grad auf der X-Achse

        while (timeElapsed < duration)
        {
            // Nutzt die Animationskurve oder Mathf.SmoothStep für Ease-in/Ease-out
            float t = timeElapsed / duration;
            float easedT = easeCurve.Evaluate(t); // Wert von 0 bis 1 nach der Kurve

            // Interpoliert zwischen Start- und Zielrotation (local)
            transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, easedT);

            timeElapsed += Time.deltaTime;
            yield return null; // Warten bis zum nächsten Frame
        }

        // Stellt sicher, dass die Rotation am Ende exakt das Ziel erreicht
        transform.localRotation = targetRotation;
    }
}
