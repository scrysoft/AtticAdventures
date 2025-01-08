using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class FullHeal : MonoBehaviour
    {
        UltimateCharacterLocomotion characterLocomotion;
        CharacterAttributeManager characterAttributeManager;

        [SerializeField] GameObject healEffect;

        public void HealCharacter()
        {
            characterLocomotion = FindAnyObjectByType<UltimateCharacterLocomotion>();
            characterAttributeManager = characterLocomotion.GetComponent<CharacterAttributeManager>();
            Attribute health = characterAttributeManager.GetAttribute("Health");

            if (characterAttributeManager != null)
            {
                health.Value = health.MaxValue;
                Instantiate(healEffect, characterLocomotion.transform.position, Quaternion.identity);
            }
        }
    }
}
