/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility.Editor
{
    using Opsive.Shared.Editor.UIElements.Managers;
    using Opsive.UltimateCharacterController.Editor.Managers;

    /// <summary>
    /// Draws the inspector for the agility add-on.
    /// </summary>
    [OrderedEditorItem("Agility Pack", 2)]
    public class AgilityAddOnInspector : AbilityAddOnInspector
    {
        public override string AddOnName { get { return "Agility"; } }
        public override string AbilityNamespace { get { return "Opsive.UltimateCharacterController.AddOns.Agility"; } }
        public override bool ShowFirstPersonAnimatorController { get { return false; } }
    }
}