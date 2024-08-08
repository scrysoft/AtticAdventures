using AtticAdventures.EventSystem;
using TMPro;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class Collectible : Entity
    {
        [SerializeField] int score = 10;
        [SerializeField] IntEventChannel scoreChannel;

        [SerializeField] float moveSpeed = 20f;

        private Transform playerTransform;
        private bool playerIsInRange = false;

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (playerIsInRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                scoreChannel.Invoke(score);
                Destroy(gameObject);
            }

            if (other.CompareTag("Magnet"))
            {
                bool isMagnetActive = other.GetComponent<Magnet>().GetActivity();
                if(isMagnetActive) playerIsInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Magnet"))
            {
                playerIsInRange = false;
            }
        }
    }
}
