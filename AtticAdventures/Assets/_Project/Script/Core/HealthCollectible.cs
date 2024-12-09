using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures.Core
{
    public class HealthCollectible : Entity
    {
        [SerializeField] int amount = 10;

        [SerializeField] float moveSpeed = 20f;

        private Transform playerTransform;
        private CharacterAttributeManager characterAttributeManager;
        private Attribute health;
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

            characterAttributeManager = playerTransform.GetComponent<CharacterAttributeManager>();
            health = characterAttributeManager.GetAttribute("Health");
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
                if(health.Value != health.MaxValue)
                {
                    health.Value += amount;
                }

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