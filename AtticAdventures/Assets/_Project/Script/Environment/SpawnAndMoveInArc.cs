using UnityEngine;
using System.Collections;
using Opsive.UltimateCharacterController.Character;

public class SpawnAndMoveInArc : MonoBehaviour
{
    public GameObject prefab;
    public Transform startPunkt;
    public Transform endPunkt;
    public float bewegungsDauer = 2f;
    public float hoeheUeberBoden = 2f;
    public Vector3 spawnOffset;

    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = new Vector3(35f, 35f, 35f);

    public Vector3 startEulerRotation = Vector3.zero;
    public Vector3 endEulerRotation = new Vector3(0f, 180f, 0f);

    public void Animate()
    {
        StartCoroutine(SpawnUndBewegen());
    }

    private IEnumerator SpawnUndBewegen()
    {
        startPunkt = FindAnyObjectByType<UltimateCharacterLocomotion>().transform;

        Vector3 startPos = startPunkt.position + spawnOffset;

        GameObject instanz = Instantiate(prefab, startPos, Quaternion.identity);

        Quaternion startRot = Quaternion.Euler(startEulerRotation);
        Quaternion endRot = Quaternion.Euler(endEulerRotation);

        instanz.transform.localScale = startScale;
        instanz.transform.rotation = startRot;

        Vector3 mittelPunkt = (startPos + endPunkt.position) / 2f;
        mittelPunkt.y += hoeheUeberBoden;

        float zeit = 0f;

        while (zeit < bewegungsDauer)
        {
            zeit += Time.deltaTime;
            float t = zeit / bewegungsDauer;
            float easedT = t * t * (3f - 2f * t);

            Vector3 aktuellePosition =
                Mathf.Pow(1f - easedT, 2) * startPos +
                2f * (1f - easedT) * easedT * mittelPunkt +
                Mathf.Pow(easedT, 2) * endPunkt.position;
            instanz.transform.position = aktuellePosition;

            Vector3 aktuelleScale = Vector3.Lerp(startScale, endScale, easedT);
            instanz.transform.localScale = aktuelleScale;

            Quaternion aktuelleRotation = Quaternion.Slerp(startRot, endRot, easedT);
            instanz.transform.rotation = aktuelleRotation;

            yield return null;
        }

        instanz.transform.position = endPunkt.position;
        instanz.transform.localScale = endScale;
        instanz.transform.rotation = endRot;

        Destroy(instanz);
    }
}
