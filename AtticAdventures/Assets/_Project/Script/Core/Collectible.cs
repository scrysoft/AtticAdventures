using AtticAdventures.EventSystem;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class Collectible : Entity
    {
        [SerializeField] int score = 10;
        [SerializeField] IntEventChannel scoreChannel;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                scoreChannel.Invoke(score);
                Destroy(gameObject);
            }
        }
    }
}
