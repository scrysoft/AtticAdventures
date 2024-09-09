using AtticAdventures.EventSystem;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class Collectible : Entity
    {
        [SerializeField] int score = 10;
        [SerializeField] IntEventChannel scoreChannel;

        [SerializeField] float moveSpeed = 20f;

        private Transform playerTransform;
        [SerializeField] Vector3 offSet = new Vector3(0, 1f, 0);

        private bool playerIsInRange = false;

        [SerializeField] bool initallyActive = true;

        public void SetActivity(bool value)
        {
            initallyActive = value;
        }

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (playerIsInRange && initallyActive)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + offSet, moveSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && initallyActive)
            {
                scoreChannel.Invoke(score);
                Destroy(gameObject);
            }

            if (other.CompareTag("Magnet"))
            {
                bool isMagnetActive = other.GetComponent<Magnet>().GetActivity();
                if (isMagnetActive) playerIsInRange = true;
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