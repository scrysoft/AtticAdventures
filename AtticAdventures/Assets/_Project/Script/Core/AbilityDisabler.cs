using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using UnityEngine;

namespace AtticAdventures
{
    public class AbilityDisabler : MonoBehaviour
    {
        [SerializeField] UltimateCharacterLocomotion characterController;

        public void DisableAbility(int index)
        {
            if (characterController == null) return;

            ItemAbility itemAbility = (ItemAbility)characterController.ItemAbilities.GetValue(index);

            itemAbility.Enabled = false;
        }

        public void EnableAbility(int index)
        {
            if (characterController == null) return;

            ItemAbility itemAbility = (ItemAbility)characterController.ItemAbilities.GetValue(index);

            itemAbility.Enabled = true;
        }
    }
}
