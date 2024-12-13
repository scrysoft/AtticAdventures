using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class FullHeal : MonoBehaviour
    {
        UltimateCharacterLocomotion characterLocomotion;
        CharacterAttributeManager characterAttributeManager;

        public void HealCharacter()
        {
            characterLocomotion = FindAnyObjectByType<UltimateCharacterLocomotion>();
            characterAttributeManager = characterLocomotion.GetComponent<CharacterAttributeManager>();
            Attribute health = characterAttributeManager.GetAttribute("Health");

            if (characterAttributeManager != null)
            {
                health.Value = health.MaxValue;
            }
        }
    }
}
