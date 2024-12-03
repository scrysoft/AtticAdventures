using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public TankMovement tankMovement;
    public RotateAndShoot rotateAndShoot;
    public Canvas canvas;

    private bool isCanvasActivatedEntirely = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankMovement.SetPlayerInZone(true, other.transform);
            rotateAndShoot.StartRotationAndShoot(true, other.transform);

            if (isCanvasActivatedEntirely)
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankMovement.SetPlayerInZone(false, null);
            rotateAndShoot.StartRotationAndShoot(false, null);

            if (isCanvasActivatedEntirely)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    public void DeactivateCanvasEntirely()
    {
        isCanvasActivatedEntirely = false;
    }
}
