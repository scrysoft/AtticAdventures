/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility
{
    using Opsive.Shared.StateSystem;
    using System;
    using UnityEngine;

    // See Opsive.UltimateCharacterController.StateSystem.AOTLinker for an explanation of this class.
    public class AOTLinker : MonoBehaviour
    {
        public void Linker()
        {
#pragma warning disable 0219
            var allowedMovementGenericDelegate = new Preset.GenericDelegate<Hang.AllowedMovement>();
            var allowedMovementFuncDelegate = new Func<Hang.AllowedMovement>(() => { return 0; });
            var allowedMovementActionDelegate = new Action<Hang.AllowedMovement>((Hang.AllowedMovement value) => { });
            var dodgeDirectionGenericDelegate = new Preset.GenericDelegate<Dodge.Direction>();
            var dodgeDirectionFuncDelegate = new Func<Dodge.Direction>(() => { return 0; });
            var dodgeDirectionActionDelegate = new Action<Dodge.Direction>((Dodge.Direction value) => { });
#pragma warning restore 0219
        }
    }
}
