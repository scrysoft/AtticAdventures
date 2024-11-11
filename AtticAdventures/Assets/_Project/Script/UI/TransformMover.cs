using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMover : MonoBehaviour
{
    [System.Serializable]
    public class PositionAndDelay
    {
        public Transform position; // Die Zielposition
        public float delay;        // Die Zeit, die an dieser Position gewartet wird
    }

    public List<PositionAndDelay> pointsAndDelays;
    public Transform target; // Das Transform, das bewegt wird
    public float moveSpeed = 1f;
    public float easeFactor = 2f;

    private void Start()
    {
        if (target != null && pointsAndDelays.Count > 0)
        {
            StartCoroutine(MoveAlongPoints());
        }
    }

    private IEnumerator MoveAlongPoints()
    {
        while (true) // Endlos-Schleife für die Wiederholung
        {
            foreach (PositionAndDelay point in pointsAndDelays)
            {
                if (point.position == null)
                {
                    Debug.LogWarning("Eine Position in pointsAndDelays ist null. Überspringen...");
                    continue; // Überspringt null Positionen
                }

                yield return StartCoroutine(MoveToPosition(point.position.position, point.delay));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 destination, float waitTime)
    {
        Vector3 startPosition = target.position;
        float distance = Vector3.Distance(startPosition, destination);

        // Sicherheits-Check: Wenn die Start- und Zielposition gleich sind, sofort zum Ziel wechseln
        if (distance == 0)
        {
            target.position = destination;
            yield return new WaitForSeconds(waitTime);
            yield break;
        }

        float time = 0f;

        while (time < 1f)
        {
            time = Mathf.Clamp01(time + Time.deltaTime * moveSpeed / distance); // Grenzwertprüfung für time
            float easedTime = EaseInOut(time);

            if (float.IsNaN(easedTime)) // Sicherheits-Check für easedTime
            {
                Debug.LogError("easedTime ist NaN, Abbruch der Bewegung.");
                yield break;
            }

            target.position = Vector3.Lerp(startPosition, destination, easedTime);
            yield return null;
        }

        target.position = destination;
        yield return new WaitForSeconds(waitTime);
    }

    private float EaseInOut(float t)
    {
        return t < 0.5f ? Mathf.Pow(t * 2f, easeFactor) / 2f : 1f - Mathf.Pow(2f - t * 2f, easeFactor) / 2f;
    }
}
