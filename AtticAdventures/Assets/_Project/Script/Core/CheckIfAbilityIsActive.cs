using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities;
using UnityEngine;
using UnityEngine.Events;

namespace AtticAdventures
{
    public class CheckIfAbilityIsActive : MonoBehaviour
    {
        [SerializeField] UltimateCharacterLocomotion characterController;
        public int abilityIndex = 0;

        public UnityEvent isActive;
        public UnityEvent isNotActive;

        private void Update()
        {
            if (characterController == null) return;

            Ability ability = (Ability)characterController.Abilities.GetValue(abilityIndex);


            if(ability.IsActive)
            {
                Debug.Log(ability + " is Active");
                isActive.Invoke();
            }
            else
            {
                Debug.Log(ability + " is not Active");
                isNotActive.Invoke();
            }
        }
    }
}
