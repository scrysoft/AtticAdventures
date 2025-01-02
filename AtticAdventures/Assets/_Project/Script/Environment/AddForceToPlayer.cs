using Opsive.UltimateCharacterController.Character;
using UnityEngine;

public class AddForceToPlayer : MonoBehaviour
{
    public Vector3 forceDirection = new Vector3(0, 10, 0);
    public float forceMagnitude = 500f;

    private UltimateCharacterLocomotion player;

    public void ApplyForce()
    {
        if(player == null)
        {
            player = FindAnyObjectByType<UltimateCharacterLocomotion>();   
        }

        Vector3 force = forceDirection.normalized * forceMagnitude;
        player.AddForce(force);
    }
}
