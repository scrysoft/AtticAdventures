/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Climbing.Editor
{
    using Opsive.Shared.Editor.UIElements.Managers;
    using Opsive.UltimateCharacterController.Editor.Managers;

    /// <summary>
    /// Draws the inspector for the climbing add-on.
    /// </summary>
    [OrderedEditorItem("Climbing Pack", 3)]
    public class ClimbingAddOnInspector : AbilityAddOnInspector
    {
        public override string AddOnName { get { return "Climbing"; } }
        public override string AbilityNamespace { get { return "Opsive.UltimateCharacterController.AddOns.Climbing"; } }
        public override bool ShowFirstPersonAnimatorController { get { return false; } }
    }
}