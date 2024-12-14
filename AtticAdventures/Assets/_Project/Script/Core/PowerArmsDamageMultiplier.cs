
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Items.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class PowerArmsDamageMultiplier : MonoBehaviour
    {
        public List<MeleeAction> actions;
        CharacterIK characterIK;

        private void OnEnable()
        {
            foreach (var action in actions) 
            {
                action.ImpactModuleGroup.GetModuleAt(0).Enabled = false;
                action.ImpactModuleGroup.GetModuleAt(1).Enabled = true;     
            }
        }
    }
}
