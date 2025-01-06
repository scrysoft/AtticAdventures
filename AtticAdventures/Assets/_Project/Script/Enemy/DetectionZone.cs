using AtticAdventures;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public TankMovement tankMovement;
    public RotateAndShoot rotateAndShoot;
    public Canvas canvas;
    public Health health;
    public BossResetter resetter;

    [SerializeField] MusicChanger musicChanger;

    private bool isCanvasActivatedEntirely = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankMovement.SetPlayerInZone(true, other.transform);
            rotateAndShoot.StartRotationAndShoot(true, other.transform);
            health.Invincible = false;

            if (resetter.IsTankAlive())
            {
                musicChanger.StopMusic(1);
                musicChanger.PlayMusic(3);
            }

            if (isCanvasActivatedEntirely)
            {
                canvas.GetComponent<CanvasGroup>().alpha = 1f;            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankMovement.SetPlayerInZone(false, null);
            rotateAndShoot.StartRotationAndShoot(false, null);
            health.Invincible = true;

            if (resetter.IsTankAlive())
            {
                musicChanger.StopMusic(3);
                musicChanger.PlayMusic(1);
            }

            if (isCanvasActivatedEntirely)
            {
                canvas.GetComponent<CanvasGroup>().alpha = 0f;
            }
        }
    }

    public void DeactivateCanvasEntirely()
    {
        isCanvasActivatedEntirely = false;
    }
}
