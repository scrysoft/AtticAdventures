/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Editor.Inspectors.Character.Abilities
{
	using Opsive.Shared.Editor.UIElements.Controls;
	using Opsive.UltimateCharacterController.Editor.Controls.Types.AbilityDrawers;
	using Opsive.UltimateCharacterController.Editor.Utility;
	using UnityEditor;
	using UnityEditor.Animations;
	using UnityEngine;

	/// <summary>
	/// Draws a custom inspector for the ShortClimb Ability.
	/// </summary>
	[ControlType(typeof(Opsive.UltimateCharacterController.AddOns.Climbing.ShortClimb))]
	public class ShortClimbDrawer : DetectObjectAbilityBaseDrawer
	{
		// ------------------------------------------- Start Generated Code -------------------------------------------
		// ------- Do NOT make any changes below. Changes will be removed when the animator is generated again. -------
		// ------------------------------------------------------------------------------------------------------------

		/// <summary>
		/// Returns true if the ability can build to the animator.
		/// </summary>
		public override bool CanBuildAnimator { get { return true; } }

		/// <summary>
		/// An editor only method which can add the abilities states/transitions to the animator.
		/// </summary>
		/// <param name="animatorControllers">The Animator Controllers to add the states to.</param>
		/// <param name="firstPersonAnimatorControllers">The first person Animator Controllers to add the states to.</param>
		public override void BuildAnimator(AnimatorController[] animatorControllers, AnimatorController[] firstPersonAnimatorControllers)
		{
			for (int i = 0; i < animatorControllers.Length; ++i) {
				if (animatorControllers[i].layers.Length <= 12) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine232221840 = animatorControllers[i].layers[12].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine232221840.stateMachines.Length; ++k) {
						if (baseStateMachine232221840.stateMachines[k].stateMachine.name == "Short Climb") {
							baseStateMachine232221840.RemoveStateMachine(baseStateMachine232221840.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var shortClimbHighAnimationClip33824Path = AssetDatabase.GUIDToAssetPath("cbafffb180174b7458c03e78b0df003b"); 
				var shortClimbHighAnimationClip33824 = AnimatorBuilder.GetAnimationClip(shortClimbHighAnimationClip33824Path, "ShortClimbHigh");
				var shortClimbMediumAnimationClip33828Path = AssetDatabase.GUIDToAssetPath("a01e757ff5caca3499d3c62debb46065"); 
				var shortClimbMediumAnimationClip33828 = AnimatorBuilder.GetAnimationClip(shortClimbMediumAnimationClip33828Path, "ShortClimbMedium");
				var shortClimbLowAnimationClip33832Path = AssetDatabase.GUIDToAssetPath("9c684263e6e77bc49a3378644c1f57bf"); 
				var shortClimbLowAnimationClip33832 = AnimatorBuilder.GetAnimationClip(shortClimbLowAnimationClip33832Path, "ShortClimbLow");

				// State Machine.
				var shortClimbAnimatorStateMachine31912 = baseStateMachine232221840.AddStateMachine("Short Climb", new Vector3(650f, 440f, 0f));

				// States.
				var shortClimbHighAnimatorState32296 = shortClimbAnimatorStateMachine31912.AddState("Short Climb High", new Vector3(380f, -50f, 0f));
				shortClimbHighAnimatorState32296.motion = shortClimbHighAnimationClip33824;
				shortClimbHighAnimatorState32296.cycleOffset = 0f;
				shortClimbHighAnimatorState32296.cycleOffsetParameterActive = false;
				shortClimbHighAnimatorState32296.iKOnFeet = false;
				shortClimbHighAnimatorState32296.mirror = false;
				shortClimbHighAnimatorState32296.mirrorParameterActive = false;
				shortClimbHighAnimatorState32296.speed = 1f;
				shortClimbHighAnimatorState32296.speedParameterActive = false;
				shortClimbHighAnimatorState32296.writeDefaultValues = true;

				var shortClimbMediumAnimatorState32298 = shortClimbAnimatorStateMachine31912.AddState("Short Climb Medium", new Vector3(380f, 10f, 0f));
				shortClimbMediumAnimatorState32298.motion = shortClimbMediumAnimationClip33828;
				shortClimbMediumAnimatorState32298.cycleOffset = 0f;
				shortClimbMediumAnimatorState32298.cycleOffsetParameterActive = false;
				shortClimbMediumAnimatorState32298.iKOnFeet = false;
				shortClimbMediumAnimatorState32298.mirror = false;
				shortClimbMediumAnimatorState32298.mirrorParameterActive = false;
				shortClimbMediumAnimatorState32298.speed = 1f;
				shortClimbMediumAnimatorState32298.speedParameterActive = false;
				shortClimbMediumAnimatorState32298.writeDefaultValues = true;

				var shortClimbLowAnimatorState32300 = shortClimbAnimatorStateMachine31912.AddState("Short Climb Low", new Vector3(380f, 70f, 0f));
				shortClimbLowAnimatorState32300.motion = shortClimbLowAnimationClip33832;
				shortClimbLowAnimatorState32300.cycleOffset = 0f;
				shortClimbLowAnimatorState32300.cycleOffsetParameterActive = false;
				shortClimbLowAnimatorState32300.iKOnFeet = false;
				shortClimbLowAnimatorState32300.mirror = false;
				shortClimbLowAnimatorState32300.mirrorParameterActive = false;
				shortClimbLowAnimatorState32300.speed = 1f;
				shortClimbLowAnimatorState32300.speedParameterActive = false;
				shortClimbLowAnimatorState32300.writeDefaultValues = true;

				// State Machine Defaults.
				shortClimbAnimatorStateMachine31912.anyStatePosition = new Vector3(50f, 20f, 0f);
				shortClimbAnimatorStateMachine31912.defaultState = shortClimbHighAnimatorState32296;
				shortClimbAnimatorStateMachine31912.entryPosition = new Vector3(50f, 120f, 0f);
				shortClimbAnimatorStateMachine31912.exitPosition = new Vector3(800f, 120f, 0f);
				shortClimbAnimatorStateMachine31912.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Transitions.
				var animatorStateTransition33822 = shortClimbHighAnimatorState32296.AddExitTransition();
				animatorStateTransition33822.canTransitionToSelf = true;
				animatorStateTransition33822.duration = 0.15f;
				animatorStateTransition33822.exitTime = 0.8064516f;
				animatorStateTransition33822.hasExitTime = false;
				animatorStateTransition33822.hasFixedDuration = true;
				animatorStateTransition33822.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33822.offset = 0f;
				animatorStateTransition33822.orderedInterruption = true;
				animatorStateTransition33822.isExit = true;
				animatorStateTransition33822.mute = false;
				animatorStateTransition33822.solo = false;
				animatorStateTransition33822.AddCondition(AnimatorConditionMode.NotEqual, 502f, "AbilityIndex");

				var animatorStateTransition33826 = shortClimbMediumAnimatorState32298.AddExitTransition();
				animatorStateTransition33826.canTransitionToSelf = true;
				animatorStateTransition33826.duration = 0.15f;
				animatorStateTransition33826.exitTime = 0.8064516f;
				animatorStateTransition33826.hasExitTime = false;
				animatorStateTransition33826.hasFixedDuration = true;
				animatorStateTransition33826.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33826.offset = 0f;
				animatorStateTransition33826.orderedInterruption = true;
				animatorStateTransition33826.isExit = true;
				animatorStateTransition33826.mute = false;
				animatorStateTransition33826.solo = false;
				animatorStateTransition33826.AddCondition(AnimatorConditionMode.NotEqual, 502f, "AbilityIndex");

				var animatorStateTransition33830 = shortClimbLowAnimatorState32300.AddExitTransition();
				animatorStateTransition33830.canTransitionToSelf = true;
				animatorStateTransition33830.duration = 0.15f;
				animatorStateTransition33830.exitTime = 0.8064516f;
				animatorStateTransition33830.hasExitTime = false;
				animatorStateTransition33830.hasFixedDuration = true;
				animatorStateTransition33830.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33830.offset = 0f;
				animatorStateTransition33830.orderedInterruption = true;
				animatorStateTransition33830.isExit = true;
				animatorStateTransition33830.mute = false;
				animatorStateTransition33830.solo = false;
				animatorStateTransition33830.AddCondition(AnimatorConditionMode.NotEqual, 502f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition32120 = baseStateMachine232221840.AddAnyStateTransition(shortClimbHighAnimatorState32296);
				animatorStateTransition32120.canTransitionToSelf = true;
				animatorStateTransition32120.duration = 0.15f;
				animatorStateTransition32120.exitTime = 0.75f;
				animatorStateTransition32120.hasExitTime = false;
				animatorStateTransition32120.hasFixedDuration = true;
				animatorStateTransition32120.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32120.offset = 0f;
				animatorStateTransition32120.orderedInterruption = true;
				animatorStateTransition32120.isExit = false;
				animatorStateTransition32120.mute = false;
				animatorStateTransition32120.solo = false;
				animatorStateTransition32120.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32120.AddCondition(AnimatorConditionMode.Equals, 502f, "AbilityIndex");
				animatorStateTransition32120.AddCondition(AnimatorConditionMode.Greater, 1.4f, "AbilityFloatData");

				var animatorStateTransition32122 = baseStateMachine232221840.AddAnyStateTransition(shortClimbMediumAnimatorState32298);
				animatorStateTransition32122.canTransitionToSelf = true;
				animatorStateTransition32122.duration = 0.15f;
				animatorStateTransition32122.exitTime = 0.75f;
				animatorStateTransition32122.hasExitTime = false;
				animatorStateTransition32122.hasFixedDuration = true;
				animatorStateTransition32122.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32122.offset = 0f;
				animatorStateTransition32122.orderedInterruption = true;
				animatorStateTransition32122.isExit = false;
				animatorStateTransition32122.mute = false;
				animatorStateTransition32122.solo = false;
				animatorStateTransition32122.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32122.AddCondition(AnimatorConditionMode.Equals, 502f, "AbilityIndex");
				animatorStateTransition32122.AddCondition(AnimatorConditionMode.Greater, 0.9f, "AbilityFloatData");
				animatorStateTransition32122.AddCondition(AnimatorConditionMode.Less, 1.1f, "AbilityFloatData");

				var animatorStateTransition32124 = baseStateMachine232221840.AddAnyStateTransition(shortClimbLowAnimatorState32300);
				animatorStateTransition32124.canTransitionToSelf = true;
				animatorStateTransition32124.duration = 0.15f;
				animatorStateTransition32124.exitTime = 0.75f;
				animatorStateTransition32124.hasExitTime = false;
				animatorStateTransition32124.hasFixedDuration = true;
				animatorStateTransition32124.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32124.offset = 0f;
				animatorStateTransition32124.orderedInterruption = true;
				animatorStateTransition32124.isExit = false;
				animatorStateTransition32124.mute = false;
				animatorStateTransition32124.solo = false;
				animatorStateTransition32124.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32124.AddCondition(AnimatorConditionMode.Equals, 502f, "AbilityIndex");
				animatorStateTransition32124.AddCondition(AnimatorConditionMode.Less, 0.9f, "AbilityFloatData");
			}
		}
	}
}
