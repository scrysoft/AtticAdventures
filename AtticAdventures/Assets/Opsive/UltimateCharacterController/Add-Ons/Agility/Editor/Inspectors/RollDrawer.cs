/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility.Editor.Inspectors.Character.Abilities
{
    using Opsive.Shared.Editor.UIElements.Controls;
	using Opsive.UltimateCharacterController.Editor.Controls.Types.AbilityDrawers;
	using Opsive.UltimateCharacterController.Editor.Utility;
    using UnityEditor;
	using UnityEditor.Animations;
	using UnityEngine;

    /// <summary>
	/// Implements AbilityDrawer for the Roll ControlType.
    /// </summary>
    [ControlType(typeof(Roll))]
	public class RollDrawer : AbilityDrawer
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
				if (animatorControllers[i].layers.Length <= 0) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine366788710 = animatorControllers[i].layers[0].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine366788710.stateMachines.Length; ++k) {
						if (baseStateMachine366788710.stateMachines[k].stateMachine.name == "Roll") {
							baseStateMachine366788710.RemoveStateMachine(baseStateMachine366788710.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var aimRollStrafeLeftAnimationClip37714Path = AssetDatabase.GUIDToAssetPath("ebc53bbaf0a0c1a4cb304d6fcf6d59e0"); 
				var aimRollStrafeLeftAnimationClip37714 = AnimatorBuilder.GetAnimationClip(aimRollStrafeLeftAnimationClip37714Path, "AimRollStrafeLeft");
				var rollStrafeLeftAnimationClip37718Path = AssetDatabase.GUIDToAssetPath("b8637c33bea0507438d6ac9899a42eba"); 
				var rollStrafeLeftAnimationClip37718 = AnimatorBuilder.GetAnimationClip(rollStrafeLeftAnimationClip37718Path, "RollStrafeLeft");
				var rollStrafeRightAnimationClip37722Path = AssetDatabase.GUIDToAssetPath("b8637c33bea0507438d6ac9899a42eba"); 
				var rollStrafeRightAnimationClip37722 = AnimatorBuilder.GetAnimationClip(rollStrafeRightAnimationClip37722Path, "RollStrafeRight");
				var rollAnimationClip37726Path = AssetDatabase.GUIDToAssetPath("80a3c683dd5529f45a234b67ef06e9f9"); 
				var rollAnimationClip37726 = AnimatorBuilder.GetAnimationClip(rollAnimationClip37726Path, "Roll");
				var rollWalkAnimationClip37730Path = AssetDatabase.GUIDToAssetPath("b8637c33bea0507438d6ac9899a42eba"); 
				var rollWalkAnimationClip37730 = AnimatorBuilder.GetAnimationClip(rollWalkAnimationClip37730Path, "RollWalk");
				var rollRunAnimationClip37734Path = AssetDatabase.GUIDToAssetPath("a6ca56273daad044499776330617bbe0"); 
				var rollRunAnimationClip37734 = AnimatorBuilder.GetAnimationClip(rollRunAnimationClip37734Path, "RollRun");
				var aimRollStrafeRightAnimationClip37738Path = AssetDatabase.GUIDToAssetPath("2086f17275bfe1a428abc9e4fd79b9d0"); 
				var aimRollStrafeRightAnimationClip37738 = AnimatorBuilder.GetAnimationClip(aimRollStrafeRightAnimationClip37738Path, "AimRollStrafeRight");
				var rollFallingAnimationClip37742Path = AssetDatabase.GUIDToAssetPath("f58548f03b2a12d4784d3d02009eac46"); 
				var rollFallingAnimationClip37742 = AnimatorBuilder.GetAnimationClip(rollFallingAnimationClip37742Path, "RollFalling");

				// State Machine.
				var rollAnimatorStateMachine34270 = baseStateMachine366788710.AddStateMachine("Roll", new Vector3(624f, 156f, 0f));

				// States.
				var aimRollLeftAnimatorState35170 = rollAnimatorStateMachine34270.AddState("Aim Roll Left", new Vector3(384f, -132f, 0f));
				aimRollLeftAnimatorState35170.motion = aimRollStrafeLeftAnimationClip37714;
				aimRollLeftAnimatorState35170.cycleOffset = 0f;
				aimRollLeftAnimatorState35170.cycleOffsetParameterActive = false;
				aimRollLeftAnimatorState35170.iKOnFeet = false;
				aimRollLeftAnimatorState35170.mirror = false;
				aimRollLeftAnimatorState35170.mirrorParameterActive = false;
				aimRollLeftAnimatorState35170.speed = 2.75f;
				aimRollLeftAnimatorState35170.speedParameterActive = false;
				aimRollLeftAnimatorState35170.writeDefaultValues = true;

				var rollLeftAnimatorState35172 = rollAnimatorStateMachine34270.AddState("Roll Left", new Vector3(384f, -192f, 0f));
				rollLeftAnimatorState35172.motion = rollStrafeLeftAnimationClip37718;
				rollLeftAnimatorState35172.cycleOffset = 0f;
				rollLeftAnimatorState35172.cycleOffsetParameterActive = false;
				rollLeftAnimatorState35172.iKOnFeet = false;
				rollLeftAnimatorState35172.mirror = false;
				rollLeftAnimatorState35172.mirrorParameterActive = false;
				rollLeftAnimatorState35172.speed = 2.75f;
				rollLeftAnimatorState35172.speedParameterActive = false;
				rollLeftAnimatorState35172.writeDefaultValues = true;

				var rollRightAnimatorState35174 = rollAnimatorStateMachine34270.AddState("Roll Right", new Vector3(384f, -72f, 0f));
				rollRightAnimatorState35174.motion = rollStrafeRightAnimationClip37722;
				rollRightAnimatorState35174.cycleOffset = 0f;
				rollRightAnimatorState35174.cycleOffsetParameterActive = false;
				rollRightAnimatorState35174.iKOnFeet = false;
				rollRightAnimatorState35174.mirror = false;
				rollRightAnimatorState35174.mirrorParameterActive = false;
				rollRightAnimatorState35174.speed = 2.75f;
				rollRightAnimatorState35174.speedParameterActive = false;
				rollRightAnimatorState35174.writeDefaultValues = true;

				var rollAnimatorState35176 = rollAnimatorStateMachine34270.AddState("Roll", new Vector3(384f, 48f, 0f));
				rollAnimatorState35176.motion = rollAnimationClip37726;
				rollAnimatorState35176.cycleOffset = 0f;
				rollAnimatorState35176.cycleOffsetParameterActive = false;
				rollAnimatorState35176.iKOnFeet = false;
				rollAnimatorState35176.mirror = false;
				rollAnimatorState35176.mirrorParameterActive = false;
				rollAnimatorState35176.speed = 2.75f;
				rollAnimatorState35176.speedParameterActive = false;
				rollAnimatorState35176.writeDefaultValues = true;

				var rollWalkAnimatorState35178 = rollAnimatorStateMachine34270.AddState("Roll Walk", new Vector3(384f, 108f, 0f));
				rollWalkAnimatorState35178.motion = rollWalkAnimationClip37730;
				rollWalkAnimatorState35178.cycleOffset = 0f;
				rollWalkAnimatorState35178.cycleOffsetParameterActive = false;
				rollWalkAnimatorState35178.iKOnFeet = false;
				rollWalkAnimatorState35178.mirror = false;
				rollWalkAnimatorState35178.mirrorParameterActive = false;
				rollWalkAnimatorState35178.speed = 2.75f;
				rollWalkAnimatorState35178.speedParameterActive = false;
				rollWalkAnimatorState35178.writeDefaultValues = true;

				var rollRunAnimatorState35180 = rollAnimatorStateMachine34270.AddState("Roll Run", new Vector3(384f, 168f, 0f));
				rollRunAnimatorState35180.motion = rollRunAnimationClip37734;
				rollRunAnimatorState35180.cycleOffset = 0f;
				rollRunAnimatorState35180.cycleOffsetParameterActive = false;
				rollRunAnimatorState35180.iKOnFeet = false;
				rollRunAnimatorState35180.mirror = false;
				rollRunAnimatorState35180.mirrorParameterActive = false;
				rollRunAnimatorState35180.speed = 2.75f;
				rollRunAnimatorState35180.speedParameterActive = false;
				rollRunAnimatorState35180.writeDefaultValues = true;

				var aimRollRightAnimatorState35182 = rollAnimatorStateMachine34270.AddState("Aim Roll Right", new Vector3(384f, -12f, 0f));
				aimRollRightAnimatorState35182.motion = aimRollStrafeRightAnimationClip37738;
				aimRollRightAnimatorState35182.cycleOffset = 0f;
				aimRollRightAnimatorState35182.cycleOffsetParameterActive = false;
				aimRollRightAnimatorState35182.iKOnFeet = false;
				aimRollRightAnimatorState35182.mirror = false;
				aimRollRightAnimatorState35182.mirrorParameterActive = false;
				aimRollRightAnimatorState35182.speed = 2.75f;
				aimRollRightAnimatorState35182.speedParameterActive = false;
				aimRollRightAnimatorState35182.writeDefaultValues = true;

				var fallingRollAnimatorState35184 = rollAnimatorStateMachine34270.AddState("Falling Roll", new Vector3(384f, 228f, 0f));
				fallingRollAnimatorState35184.motion = rollFallingAnimationClip37742;
				fallingRollAnimatorState35184.cycleOffset = 0f;
				fallingRollAnimatorState35184.cycleOffsetParameterActive = false;
				fallingRollAnimatorState35184.iKOnFeet = false;
				fallingRollAnimatorState35184.mirror = false;
				fallingRollAnimatorState35184.mirrorParameterActive = false;
				fallingRollAnimatorState35184.speed = 2.75f;
				fallingRollAnimatorState35184.speedParameterActive = false;
				fallingRollAnimatorState35184.writeDefaultValues = true;

				// State Machine Defaults.
				rollAnimatorStateMachine34270.anyStatePosition = new Vector3(50f, 20f, 0f);
				rollAnimatorStateMachine34270.defaultState = rollAnimatorState35176;
				rollAnimatorStateMachine34270.entryPosition = new Vector3(60f, -36f, 0f);
				rollAnimatorStateMachine34270.exitPosition = new Vector3(756f, 24f, 0f);
				rollAnimatorStateMachine34270.parentStateMachinePosition = new Vector3(732f, -48f, 0f);

				// State Transitions.
				var animatorStateTransition37712 = aimRollLeftAnimatorState35170.AddExitTransition();
				animatorStateTransition37712.canTransitionToSelf = true;
				animatorStateTransition37712.duration = 0.15f;
				animatorStateTransition37712.exitTime = 0.9158775f;
				animatorStateTransition37712.hasExitTime = false;
				animatorStateTransition37712.hasFixedDuration = true;
				animatorStateTransition37712.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37712.offset = 0f;
				animatorStateTransition37712.orderedInterruption = true;
				animatorStateTransition37712.isExit = true;
				animatorStateTransition37712.mute = false;
				animatorStateTransition37712.solo = false;
				animatorStateTransition37712.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37716 = rollLeftAnimatorState35172.AddExitTransition();
				animatorStateTransition37716.canTransitionToSelf = true;
				animatorStateTransition37716.duration = 0.15f;
				animatorStateTransition37716.exitTime = 0.9158775f;
				animatorStateTransition37716.hasExitTime = false;
				animatorStateTransition37716.hasFixedDuration = true;
				animatorStateTransition37716.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37716.offset = 0f;
				animatorStateTransition37716.orderedInterruption = true;
				animatorStateTransition37716.isExit = true;
				animatorStateTransition37716.mute = false;
				animatorStateTransition37716.solo = false;
				animatorStateTransition37716.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37720 = rollRightAnimatorState35174.AddExitTransition();
				animatorStateTransition37720.canTransitionToSelf = true;
				animatorStateTransition37720.duration = 0.15f;
				animatorStateTransition37720.exitTime = 0.9158775f;
				animatorStateTransition37720.hasExitTime = false;
				animatorStateTransition37720.hasFixedDuration = true;
				animatorStateTransition37720.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37720.offset = 0f;
				animatorStateTransition37720.orderedInterruption = true;
				animatorStateTransition37720.isExit = true;
				animatorStateTransition37720.mute = false;
				animatorStateTransition37720.solo = false;
				animatorStateTransition37720.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37724 = rollAnimatorState35176.AddExitTransition();
				animatorStateTransition37724.canTransitionToSelf = true;
				animatorStateTransition37724.duration = 0.15f;
				animatorStateTransition37724.exitTime = 0.9158775f;
				animatorStateTransition37724.hasExitTime = false;
				animatorStateTransition37724.hasFixedDuration = true;
				animatorStateTransition37724.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37724.offset = 0f;
				animatorStateTransition37724.orderedInterruption = true;
				animatorStateTransition37724.isExit = true;
				animatorStateTransition37724.mute = false;
				animatorStateTransition37724.solo = false;
				animatorStateTransition37724.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37728 = rollWalkAnimatorState35178.AddExitTransition();
				animatorStateTransition37728.canTransitionToSelf = true;
				animatorStateTransition37728.duration = 0.15f;
				animatorStateTransition37728.exitTime = 0.9158775f;
				animatorStateTransition37728.hasExitTime = false;
				animatorStateTransition37728.hasFixedDuration = true;
				animatorStateTransition37728.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37728.offset = 0f;
				animatorStateTransition37728.orderedInterruption = true;
				animatorStateTransition37728.isExit = true;
				animatorStateTransition37728.mute = false;
				animatorStateTransition37728.solo = false;
				animatorStateTransition37728.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37732 = rollRunAnimatorState35180.AddExitTransition();
				animatorStateTransition37732.canTransitionToSelf = true;
				animatorStateTransition37732.duration = 0.15f;
				animatorStateTransition37732.exitTime = 0.9158775f;
				animatorStateTransition37732.hasExitTime = false;
				animatorStateTransition37732.hasFixedDuration = true;
				animatorStateTransition37732.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37732.offset = 0f;
				animatorStateTransition37732.orderedInterruption = true;
				animatorStateTransition37732.isExit = true;
				animatorStateTransition37732.mute = false;
				animatorStateTransition37732.solo = false;
				animatorStateTransition37732.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37736 = aimRollRightAnimatorState35182.AddExitTransition();
				animatorStateTransition37736.canTransitionToSelf = true;
				animatorStateTransition37736.duration = 0.15f;
				animatorStateTransition37736.exitTime = 0.9158775f;
				animatorStateTransition37736.hasExitTime = false;
				animatorStateTransition37736.hasFixedDuration = true;
				animatorStateTransition37736.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37736.offset = 0f;
				animatorStateTransition37736.orderedInterruption = true;
				animatorStateTransition37736.isExit = true;
				animatorStateTransition37736.mute = false;
				animatorStateTransition37736.solo = false;
				animatorStateTransition37736.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition37740 = fallingRollAnimatorState35184.AddExitTransition();
				animatorStateTransition37740.canTransitionToSelf = true;
				animatorStateTransition37740.duration = 0.15f;
				animatorStateTransition37740.exitTime = 0.9158775f;
				animatorStateTransition37740.hasExitTime = false;
				animatorStateTransition37740.hasFixedDuration = true;
				animatorStateTransition37740.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37740.offset = 0f;
				animatorStateTransition37740.orderedInterruption = true;
				animatorStateTransition37740.isExit = true;
				animatorStateTransition37740.mute = false;
				animatorStateTransition37740.solo = false;
				animatorStateTransition37740.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition34844 = baseStateMachine366788710.AddAnyStateTransition(aimRollLeftAnimatorState35170);
				animatorStateTransition34844.canTransitionToSelf = false;
				animatorStateTransition34844.duration = 0.05f;
				animatorStateTransition34844.exitTime = 0.75f;
				animatorStateTransition34844.hasExitTime = false;
				animatorStateTransition34844.hasFixedDuration = true;
				animatorStateTransition34844.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34844.offset = 0.1f;
				animatorStateTransition34844.orderedInterruption = true;
				animatorStateTransition34844.isExit = false;
				animatorStateTransition34844.mute = false;
				animatorStateTransition34844.solo = false;
				animatorStateTransition34844.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34844.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34844.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34844.AddCondition(AnimatorConditionMode.If, 0f, "Aiming");

				var animatorStateTransition34846 = baseStateMachine366788710.AddAnyStateTransition(rollLeftAnimatorState35172);
				animatorStateTransition34846.canTransitionToSelf = false;
				animatorStateTransition34846.duration = 0.05f;
				animatorStateTransition34846.exitTime = 0.75f;
				animatorStateTransition34846.hasExitTime = false;
				animatorStateTransition34846.hasFixedDuration = true;
				animatorStateTransition34846.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34846.offset = 0.1f;
				animatorStateTransition34846.orderedInterruption = true;
				animatorStateTransition34846.isExit = false;
				animatorStateTransition34846.mute = false;
				animatorStateTransition34846.solo = false;
				animatorStateTransition34846.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34846.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34846.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34846.AddCondition(AnimatorConditionMode.IfNot, 0f, "Aiming");

				var animatorStateTransition34848 = baseStateMachine366788710.AddAnyStateTransition(rollRightAnimatorState35174);
				animatorStateTransition34848.canTransitionToSelf = false;
				animatorStateTransition34848.duration = 0.05f;
				animatorStateTransition34848.exitTime = 0.75f;
				animatorStateTransition34848.hasExitTime = false;
				animatorStateTransition34848.hasFixedDuration = true;
				animatorStateTransition34848.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34848.offset = 0.1f;
				animatorStateTransition34848.orderedInterruption = true;
				animatorStateTransition34848.isExit = false;
				animatorStateTransition34848.mute = false;
				animatorStateTransition34848.solo = false;
				animatorStateTransition34848.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34848.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34848.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34848.AddCondition(AnimatorConditionMode.IfNot, 0f, "Aiming");

				var animatorStateTransition34850 = baseStateMachine366788710.AddAnyStateTransition(rollAnimatorState35176);
				animatorStateTransition34850.canTransitionToSelf = false;
				animatorStateTransition34850.duration = 0.05f;
				animatorStateTransition34850.exitTime = 0.75f;
				animatorStateTransition34850.hasExitTime = false;
				animatorStateTransition34850.hasFixedDuration = true;
				animatorStateTransition34850.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34850.offset = 0.1f;
				animatorStateTransition34850.orderedInterruption = true;
				animatorStateTransition34850.isExit = false;
				animatorStateTransition34850.mute = false;
				animatorStateTransition34850.solo = false;
				animatorStateTransition34850.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34850.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34850.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition34850.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition34852 = baseStateMachine366788710.AddAnyStateTransition(rollWalkAnimatorState35178);
				animatorStateTransition34852.canTransitionToSelf = false;
				animatorStateTransition34852.duration = 0.05f;
				animatorStateTransition34852.exitTime = 0.75f;
				animatorStateTransition34852.hasExitTime = false;
				animatorStateTransition34852.hasFixedDuration = true;
				animatorStateTransition34852.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34852.offset = 0.1f;
				animatorStateTransition34852.orderedInterruption = true;
				animatorStateTransition34852.isExit = false;
				animatorStateTransition34852.mute = false;
				animatorStateTransition34852.solo = false;
				animatorStateTransition34852.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34852.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34852.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition34852.AddCondition(AnimatorConditionMode.If, 0f, "Moving");
				animatorStateTransition34852.AddCondition(AnimatorConditionMode.Greater, 0.5f, "Speed");
				animatorStateTransition34852.AddCondition(AnimatorConditionMode.Less, 1.5f, "Speed");

				var animatorStateTransition34854 = baseStateMachine366788710.AddAnyStateTransition(rollRunAnimatorState35180);
				animatorStateTransition34854.canTransitionToSelf = false;
				animatorStateTransition34854.duration = 0.05f;
				animatorStateTransition34854.exitTime = 0.75f;
				animatorStateTransition34854.hasExitTime = false;
				animatorStateTransition34854.hasFixedDuration = true;
				animatorStateTransition34854.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34854.offset = 0.1f;
				animatorStateTransition34854.orderedInterruption = true;
				animatorStateTransition34854.isExit = false;
				animatorStateTransition34854.mute = false;
				animatorStateTransition34854.solo = false;
				animatorStateTransition34854.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34854.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34854.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition34854.AddCondition(AnimatorConditionMode.If, 0f, "Moving");
				animatorStateTransition34854.AddCondition(AnimatorConditionMode.Greater, 1f, "Speed");

				var animatorStateTransition34856 = baseStateMachine366788710.AddAnyStateTransition(aimRollRightAnimatorState35182);
				animatorStateTransition34856.canTransitionToSelf = false;
				animatorStateTransition34856.duration = 0.05f;
				animatorStateTransition34856.exitTime = 0.75f;
				animatorStateTransition34856.hasExitTime = false;
				animatorStateTransition34856.hasFixedDuration = true;
				animatorStateTransition34856.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34856.offset = 0.1f;
				animatorStateTransition34856.orderedInterruption = true;
				animatorStateTransition34856.isExit = false;
				animatorStateTransition34856.mute = false;
				animatorStateTransition34856.solo = false;
				animatorStateTransition34856.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34856.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34856.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34856.AddCondition(AnimatorConditionMode.If, 0f, "Aiming");

				var animatorStateTransition34858 = baseStateMachine366788710.AddAnyStateTransition(fallingRollAnimatorState35184);
				animatorStateTransition34858.canTransitionToSelf = false;
				animatorStateTransition34858.duration = 0.05f;
				animatorStateTransition34858.exitTime = 0.75f;
				animatorStateTransition34858.hasExitTime = false;
				animatorStateTransition34858.hasFixedDuration = true;
				animatorStateTransition34858.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34858.offset = 0.1f;
				animatorStateTransition34858.orderedInterruption = true;
				animatorStateTransition34858.isExit = false;
				animatorStateTransition34858.mute = false;
				animatorStateTransition34858.solo = false;
				animatorStateTransition34858.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34858.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition34858.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				if (animatorControllers[i].layers.Length <= 5) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine310762244 = animatorControllers[i].layers[5].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine310762244.stateMachines.Length; ++k) {
						if (baseStateMachine310762244.stateMachines[k].stateMachine.name == "Roll") {
							baseStateMachine310762244.RemoveStateMachine(baseStateMachine310762244.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var assaultRifleRollIdleAnimationClip41006Path = AssetDatabase.GUIDToAssetPath("39544c3509240c542b07576a5af1c13c"); 
				var assaultRifleRollIdleAnimationClip41006 = AnimatorBuilder.GetAnimationClip(assaultRifleRollIdleAnimationClip41006Path, "AssaultRifleRollIdle");
				var pistolRollIdleAnimationClip41010Path = AssetDatabase.GUIDToAssetPath("09e38275d81602b489835c6374477408"); 
				var pistolRollIdleAnimationClip41010 = AnimatorBuilder.GetAnimationClip(pistolRollIdleAnimationClip41010Path, "PistolRollIdle");
				var dualPistolRollIdleAnimationClip41014Path = AssetDatabase.GUIDToAssetPath("d828fedde99b6024a8e7e26e17e45386"); 
				var dualPistolRollIdleAnimationClip41014 = AnimatorBuilder.GetAnimationClip(dualPistolRollIdleAnimationClip41014Path, "DualPistolRollIdle");
				var rocketLauncherRollIdleAnimationClip41018Path = AssetDatabase.GUIDToAssetPath("4b2e64e4268ee634a8f5ef226eea2fa7"); 
				var rocketLauncherRollIdleAnimationClip41018 = AnimatorBuilder.GetAnimationClip(rocketLauncherRollIdleAnimationClip41018Path, "RocketLauncherRollIdle");
				var shotgunRollIdleAnimationClip41022Path = AssetDatabase.GUIDToAssetPath("ecea2486194b00448941b70a63f54011"); 
				var shotgunRollIdleAnimationClip41022 = AnimatorBuilder.GetAnimationClip(shotgunRollIdleAnimationClip41022Path, "ShotgunRollIdle");
				var sniperRifleRollIdleAnimationClip41026Path = AssetDatabase.GUIDToAssetPath("39b068db4042ab343a202f0407939fed"); 
				var sniperRifleRollIdleAnimationClip41026 = AnimatorBuilder.GetAnimationClip(sniperRifleRollIdleAnimationClip41026Path, "SniperRifleRollIdle");
				var shieldRollIdleAnimationClip41030Path = AssetDatabase.GUIDToAssetPath("a0f52d3dc5cea6046b13848f8edd2f5c"); 
				var shieldRollIdleAnimationClip41030 = AnimatorBuilder.GetAnimationClip(shieldRollIdleAnimationClip41030Path, "ShieldRollIdle");
				var bowRollIdleAnimationClip41034Path = AssetDatabase.GUIDToAssetPath("a073f0ad43f21244aaca022aef7f88ee"); 
				var bowRollIdleAnimationClip41034 = AnimatorBuilder.GetAnimationClip(bowRollIdleAnimationClip41034Path, "BowRollIdle");

				// State Machine.
				var rollAnimatorStateMachine40636 = baseStateMachine310762244.AddStateMachine("Roll", new Vector3(852f, 108f, 0f));

				// States.
				var assaultRifleAnimatorState40790 = rollAnimatorStateMachine40636.AddState("Assault Rifle", new Vector3(384f, -48f, 0f));
				assaultRifleAnimatorState40790.motion = assaultRifleRollIdleAnimationClip41006;
				assaultRifleAnimatorState40790.cycleOffset = 0f;
				assaultRifleAnimatorState40790.cycleOffsetParameterActive = false;
				assaultRifleAnimatorState40790.iKOnFeet = false;
				assaultRifleAnimatorState40790.mirror = false;
				assaultRifleAnimatorState40790.mirrorParameterActive = false;
				assaultRifleAnimatorState40790.speed = 1f;
				assaultRifleAnimatorState40790.speedParameterActive = false;
				assaultRifleAnimatorState40790.writeDefaultValues = true;

				var pistolAnimatorState40788 = rollAnimatorStateMachine40636.AddState("Pistol", new Vector3(384f, 132f, 0f));
				pistolAnimatorState40788.motion = pistolRollIdleAnimationClip41010;
				pistolAnimatorState40788.cycleOffset = 0f;
				pistolAnimatorState40788.cycleOffsetParameterActive = false;
				pistolAnimatorState40788.iKOnFeet = false;
				pistolAnimatorState40788.mirror = false;
				pistolAnimatorState40788.mirrorParameterActive = false;
				pistolAnimatorState40788.speed = 1f;
				pistolAnimatorState40788.speedParameterActive = false;
				pistolAnimatorState40788.writeDefaultValues = true;

				var dualPistolAnimatorState40786 = rollAnimatorStateMachine40636.AddState("Dual Pistol", new Vector3(384f, 72f, 0f));
				dualPistolAnimatorState40786.motion = dualPistolRollIdleAnimationClip41014;
				dualPistolAnimatorState40786.cycleOffset = 0f;
				dualPistolAnimatorState40786.cycleOffsetParameterActive = false;
				dualPistolAnimatorState40786.iKOnFeet = false;
				dualPistolAnimatorState40786.mirror = false;
				dualPistolAnimatorState40786.mirrorParameterActive = false;
				dualPistolAnimatorState40786.speed = 1f;
				dualPistolAnimatorState40786.speedParameterActive = false;
				dualPistolAnimatorState40786.writeDefaultValues = true;

				var rocketLauncherAnimatorState40798 = rollAnimatorStateMachine40636.AddState("Rocket Launcher", new Vector3(384f, 192f, 0f));
				rocketLauncherAnimatorState40798.motion = rocketLauncherRollIdleAnimationClip41018;
				rocketLauncherAnimatorState40798.cycleOffset = 0f;
				rocketLauncherAnimatorState40798.cycleOffsetParameterActive = false;
				rocketLauncherAnimatorState40798.iKOnFeet = false;
				rocketLauncherAnimatorState40798.mirror = false;
				rocketLauncherAnimatorState40798.mirrorParameterActive = false;
				rocketLauncherAnimatorState40798.speed = 1f;
				rocketLauncherAnimatorState40798.speedParameterActive = false;
				rocketLauncherAnimatorState40798.writeDefaultValues = true;

				var shotgunAnimatorState40796 = rollAnimatorStateMachine40636.AddState("Shotgun", new Vector3(384f, 312f, 0f));
				shotgunAnimatorState40796.motion = shotgunRollIdleAnimationClip41022;
				shotgunAnimatorState40796.cycleOffset = 0f;
				shotgunAnimatorState40796.cycleOffsetParameterActive = false;
				shotgunAnimatorState40796.iKOnFeet = false;
				shotgunAnimatorState40796.mirror = false;
				shotgunAnimatorState40796.mirrorParameterActive = false;
				shotgunAnimatorState40796.speed = 1f;
				shotgunAnimatorState40796.speedParameterActive = false;
				shotgunAnimatorState40796.writeDefaultValues = true;

				var sniperRifleAnimatorState40794 = rollAnimatorStateMachine40636.AddState("Sniper Rifle", new Vector3(384f, 372f, 0f));
				sniperRifleAnimatorState40794.motion = sniperRifleRollIdleAnimationClip41026;
				sniperRifleAnimatorState40794.cycleOffset = 0f;
				sniperRifleAnimatorState40794.cycleOffsetParameterActive = false;
				sniperRifleAnimatorState40794.iKOnFeet = false;
				sniperRifleAnimatorState40794.mirror = false;
				sniperRifleAnimatorState40794.mirrorParameterActive = false;
				sniperRifleAnimatorState40794.speed = 1f;
				sniperRifleAnimatorState40794.speedParameterActive = false;
				sniperRifleAnimatorState40794.writeDefaultValues = true;

				var shieldAnimatorState40784 = rollAnimatorStateMachine40636.AddState("Shield", new Vector3(384f, 252f, 0f));
				shieldAnimatorState40784.motion = shieldRollIdleAnimationClip41030;
				shieldAnimatorState40784.cycleOffset = 0f;
				shieldAnimatorState40784.cycleOffsetParameterActive = false;
				shieldAnimatorState40784.iKOnFeet = false;
				shieldAnimatorState40784.mirror = false;
				shieldAnimatorState40784.mirrorParameterActive = false;
				shieldAnimatorState40784.speed = 1f;
				shieldAnimatorState40784.speedParameterActive = false;
				shieldAnimatorState40784.writeDefaultValues = true;

				var bowAnimatorState40792 = rollAnimatorStateMachine40636.AddState("Bow", new Vector3(384f, 12f, 0f));
				bowAnimatorState40792.motion = bowRollIdleAnimationClip41034;
				bowAnimatorState40792.cycleOffset = 0f;
				bowAnimatorState40792.cycleOffsetParameterActive = false;
				bowAnimatorState40792.iKOnFeet = false;
				bowAnimatorState40792.mirror = false;
				bowAnimatorState40792.mirrorParameterActive = false;
				bowAnimatorState40792.speed = 1f;
				bowAnimatorState40792.speedParameterActive = false;
				bowAnimatorState40792.writeDefaultValues = true;

				// State Machine Defaults.
				rollAnimatorStateMachine40636.anyStatePosition = new Vector3(36f, 156f, 0f);
				rollAnimatorStateMachine40636.defaultState = assaultRifleAnimatorState40790;
				rollAnimatorStateMachine40636.entryPosition = new Vector3(36f, 96f, 0f);
				rollAnimatorStateMachine40636.exitPosition = new Vector3(768f, 168f, 0f);
				rollAnimatorStateMachine40636.parentStateMachinePosition = new Vector3(744f, 84f, 0f);

				// State Transitions.
				var animatorStateTransition41004 = assaultRifleAnimatorState40790.AddExitTransition();
				animatorStateTransition41004.canTransitionToSelf = true;
				animatorStateTransition41004.duration = 0.15f;
				animatorStateTransition41004.exitTime = 0f;
				animatorStateTransition41004.hasExitTime = false;
				animatorStateTransition41004.hasFixedDuration = true;
				animatorStateTransition41004.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41004.offset = 0f;
				animatorStateTransition41004.orderedInterruption = true;
				animatorStateTransition41004.isExit = true;
				animatorStateTransition41004.mute = false;
				animatorStateTransition41004.solo = false;
				animatorStateTransition41004.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41008 = pistolAnimatorState40788.AddExitTransition();
				animatorStateTransition41008.canTransitionToSelf = true;
				animatorStateTransition41008.duration = 0.15f;
				animatorStateTransition41008.exitTime = 0f;
				animatorStateTransition41008.hasExitTime = false;
				animatorStateTransition41008.hasFixedDuration = true;
				animatorStateTransition41008.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41008.offset = 0f;
				animatorStateTransition41008.orderedInterruption = true;
				animatorStateTransition41008.isExit = true;
				animatorStateTransition41008.mute = false;
				animatorStateTransition41008.solo = false;
				animatorStateTransition41008.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41012 = dualPistolAnimatorState40786.AddExitTransition();
				animatorStateTransition41012.canTransitionToSelf = true;
				animatorStateTransition41012.duration = 0.15f;
				animatorStateTransition41012.exitTime = 0f;
				animatorStateTransition41012.hasExitTime = false;
				animatorStateTransition41012.hasFixedDuration = true;
				animatorStateTransition41012.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41012.offset = 0f;
				animatorStateTransition41012.orderedInterruption = true;
				animatorStateTransition41012.isExit = true;
				animatorStateTransition41012.mute = false;
				animatorStateTransition41012.solo = false;
				animatorStateTransition41012.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41016 = rocketLauncherAnimatorState40798.AddExitTransition();
				animatorStateTransition41016.canTransitionToSelf = true;
				animatorStateTransition41016.duration = 0.15f;
				animatorStateTransition41016.exitTime = 0f;
				animatorStateTransition41016.hasExitTime = false;
				animatorStateTransition41016.hasFixedDuration = true;
				animatorStateTransition41016.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41016.offset = 0f;
				animatorStateTransition41016.orderedInterruption = true;
				animatorStateTransition41016.isExit = true;
				animatorStateTransition41016.mute = false;
				animatorStateTransition41016.solo = false;
				animatorStateTransition41016.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41020 = shotgunAnimatorState40796.AddExitTransition();
				animatorStateTransition41020.canTransitionToSelf = true;
				animatorStateTransition41020.duration = 0.15f;
				animatorStateTransition41020.exitTime = 0f;
				animatorStateTransition41020.hasExitTime = false;
				animatorStateTransition41020.hasFixedDuration = true;
				animatorStateTransition41020.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41020.offset = 0f;
				animatorStateTransition41020.orderedInterruption = true;
				animatorStateTransition41020.isExit = true;
				animatorStateTransition41020.mute = false;
				animatorStateTransition41020.solo = false;
				animatorStateTransition41020.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41024 = sniperRifleAnimatorState40794.AddExitTransition();
				animatorStateTransition41024.canTransitionToSelf = true;
				animatorStateTransition41024.duration = 0.15f;
				animatorStateTransition41024.exitTime = 0f;
				animatorStateTransition41024.hasExitTime = false;
				animatorStateTransition41024.hasFixedDuration = true;
				animatorStateTransition41024.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41024.offset = 0f;
				animatorStateTransition41024.orderedInterruption = true;
				animatorStateTransition41024.isExit = true;
				animatorStateTransition41024.mute = false;
				animatorStateTransition41024.solo = false;
				animatorStateTransition41024.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41028 = shieldAnimatorState40784.AddExitTransition();
				animatorStateTransition41028.canTransitionToSelf = true;
				animatorStateTransition41028.duration = 0.15f;
				animatorStateTransition41028.exitTime = 0f;
				animatorStateTransition41028.hasExitTime = false;
				animatorStateTransition41028.hasFixedDuration = true;
				animatorStateTransition41028.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41028.offset = 0f;
				animatorStateTransition41028.orderedInterruption = true;
				animatorStateTransition41028.isExit = true;
				animatorStateTransition41028.mute = false;
				animatorStateTransition41028.solo = false;
				animatorStateTransition41028.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41032 = bowAnimatorState40792.AddExitTransition();
				animatorStateTransition41032.canTransitionToSelf = true;
				animatorStateTransition41032.duration = 0.15f;
				animatorStateTransition41032.exitTime = 0f;
				animatorStateTransition41032.hasExitTime = false;
				animatorStateTransition41032.hasFixedDuration = true;
				animatorStateTransition41032.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41032.offset = 0f;
				animatorStateTransition41032.orderedInterruption = true;
				animatorStateTransition41032.isExit = true;
				animatorStateTransition41032.mute = false;
				animatorStateTransition41032.solo = false;
				animatorStateTransition41032.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition40704 = baseStateMachine310762244.AddAnyStateTransition(shieldAnimatorState40784);
				animatorStateTransition40704.canTransitionToSelf = false;
				animatorStateTransition40704.duration = 0.05f;
				animatorStateTransition40704.exitTime = 0.75f;
				animatorStateTransition40704.hasExitTime = false;
				animatorStateTransition40704.hasFixedDuration = true;
				animatorStateTransition40704.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40704.offset = 0f;
				animatorStateTransition40704.orderedInterruption = true;
				animatorStateTransition40704.isExit = false;
				animatorStateTransition40704.mute = false;
				animatorStateTransition40704.solo = false;
				animatorStateTransition40704.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40704.AddCondition(AnimatorConditionMode.Equals, 25f, "Slot1ItemID");

				var animatorStateTransition40706 = baseStateMachine310762244.AddAnyStateTransition(dualPistolAnimatorState40786);
				animatorStateTransition40706.canTransitionToSelf = false;
				animatorStateTransition40706.duration = 0.05f;
				animatorStateTransition40706.exitTime = 0.75f;
				animatorStateTransition40706.hasExitTime = false;
				animatorStateTransition40706.hasFixedDuration = true;
				animatorStateTransition40706.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40706.offset = 0f;
				animatorStateTransition40706.orderedInterruption = true;
				animatorStateTransition40706.isExit = false;
				animatorStateTransition40706.mute = false;
				animatorStateTransition40706.solo = false;
				animatorStateTransition40706.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40706.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition40706.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot1ItemID");

				var animatorStateTransition40708 = baseStateMachine310762244.AddAnyStateTransition(pistolAnimatorState40788);
				animatorStateTransition40708.canTransitionToSelf = false;
				animatorStateTransition40708.duration = 0.05f;
				animatorStateTransition40708.exitTime = 0.75f;
				animatorStateTransition40708.hasExitTime = false;
				animatorStateTransition40708.hasFixedDuration = true;
				animatorStateTransition40708.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40708.offset = 0f;
				animatorStateTransition40708.orderedInterruption = true;
				animatorStateTransition40708.isExit = false;
				animatorStateTransition40708.mute = false;
				animatorStateTransition40708.solo = false;
				animatorStateTransition40708.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40708.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition40708.AddCondition(AnimatorConditionMode.NotEqual, 2f, "Slot1ItemID");

				var animatorStateTransition40710 = baseStateMachine310762244.AddAnyStateTransition(assaultRifleAnimatorState40790);
				animatorStateTransition40710.canTransitionToSelf = false;
				animatorStateTransition40710.duration = 0.05f;
				animatorStateTransition40710.exitTime = 0.75f;
				animatorStateTransition40710.hasExitTime = false;
				animatorStateTransition40710.hasFixedDuration = true;
				animatorStateTransition40710.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40710.offset = 0f;
				animatorStateTransition40710.orderedInterruption = true;
				animatorStateTransition40710.isExit = false;
				animatorStateTransition40710.mute = false;
				animatorStateTransition40710.solo = false;
				animatorStateTransition40710.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40710.AddCondition(AnimatorConditionMode.Equals, 1f, "Slot0ItemID");

				var animatorStateTransition40712 = baseStateMachine310762244.AddAnyStateTransition(bowAnimatorState40792);
				animatorStateTransition40712.canTransitionToSelf = false;
				animatorStateTransition40712.duration = 0.05f;
				animatorStateTransition40712.exitTime = 0.75f;
				animatorStateTransition40712.hasExitTime = false;
				animatorStateTransition40712.hasFixedDuration = true;
				animatorStateTransition40712.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40712.offset = 0f;
				animatorStateTransition40712.orderedInterruption = true;
				animatorStateTransition40712.isExit = false;
				animatorStateTransition40712.mute = false;
				animatorStateTransition40712.solo = false;
				animatorStateTransition40712.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40712.AddCondition(AnimatorConditionMode.Equals, 4f, "Slot1ItemID");

				var animatorStateTransition40714 = baseStateMachine310762244.AddAnyStateTransition(sniperRifleAnimatorState40794);
				animatorStateTransition40714.canTransitionToSelf = false;
				animatorStateTransition40714.duration = 0.05f;
				animatorStateTransition40714.exitTime = 0.75f;
				animatorStateTransition40714.hasExitTime = false;
				animatorStateTransition40714.hasFixedDuration = true;
				animatorStateTransition40714.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40714.offset = 0f;
				animatorStateTransition40714.orderedInterruption = true;
				animatorStateTransition40714.isExit = false;
				animatorStateTransition40714.mute = false;
				animatorStateTransition40714.solo = false;
				animatorStateTransition40714.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40714.AddCondition(AnimatorConditionMode.Equals, 5f, "Slot0ItemID");

				var animatorStateTransition40716 = baseStateMachine310762244.AddAnyStateTransition(shotgunAnimatorState40796);
				animatorStateTransition40716.canTransitionToSelf = false;
				animatorStateTransition40716.duration = 0.05f;
				animatorStateTransition40716.exitTime = 0.75f;
				animatorStateTransition40716.hasExitTime = false;
				animatorStateTransition40716.hasFixedDuration = true;
				animatorStateTransition40716.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40716.offset = 0f;
				animatorStateTransition40716.orderedInterruption = true;
				animatorStateTransition40716.isExit = false;
				animatorStateTransition40716.mute = false;
				animatorStateTransition40716.solo = false;
				animatorStateTransition40716.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40716.AddCondition(AnimatorConditionMode.Equals, 3f, "Slot0ItemID");

				var animatorStateTransition40718 = baseStateMachine310762244.AddAnyStateTransition(rocketLauncherAnimatorState40798);
				animatorStateTransition40718.canTransitionToSelf = false;
				animatorStateTransition40718.duration = 0.05f;
				animatorStateTransition40718.exitTime = 0.75f;
				animatorStateTransition40718.hasExitTime = false;
				animatorStateTransition40718.hasFixedDuration = true;
				animatorStateTransition40718.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40718.offset = 0f;
				animatorStateTransition40718.orderedInterruption = true;
				animatorStateTransition40718.isExit = false;
				animatorStateTransition40718.mute = false;
				animatorStateTransition40718.solo = false;
				animatorStateTransition40718.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition40718.AddCondition(AnimatorConditionMode.Equals, 6f, "Slot0ItemID");

				if (animatorControllers[i].layers.Length <= 6) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine270919106 = animatorControllers[i].layers[6].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine270919106.stateMachines.Length; ++k) {
						if (baseStateMachine270919106.stateMachines[k].stateMachine.name == "Roll") {
							baseStateMachine270919106.RemoveStateMachine(baseStateMachine270919106.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var swordRollIdleAnimationClip41496Path = AssetDatabase.GUIDToAssetPath("0e24b81f894df92458c8e8a5588622de"); 
				var swordRollIdleAnimationClip41496 = AnimatorBuilder.GetAnimationClip(swordRollIdleAnimationClip41496Path, "SwordRollIdle");
				var katanaRollIdleAnimationClip41500Path = AssetDatabase.GUIDToAssetPath("e9734da6e3b579041b65e31fa14305f9"); 
				var katanaRollIdleAnimationClip41500 = AnimatorBuilder.GetAnimationClip(katanaRollIdleAnimationClip41500Path, "KatanaRollIdle");
				var knifeRollIdleAnimationClip41506Path = AssetDatabase.GUIDToAssetPath("c7bccd5ba3087d24ab9b21627c71d781"); 
				var knifeRollIdleAnimationClip41506 = AnimatorBuilder.GetAnimationClip(knifeRollIdleAnimationClip41506Path, "KnifeRollIdle");

				// State Machine.
				var rollAnimatorStateMachine41082 = baseStateMachine270919106.AddStateMachine("Roll", new Vector3(852f, 60f, 0f));

				// States.
				var assaultRifleAnimatorState41286 = rollAnimatorStateMachine41082.AddState("Assault Rifle", new Vector3(384f, -228f, 0f));
				assaultRifleAnimatorState41286.motion = assaultRifleRollIdleAnimationClip41006;
				assaultRifleAnimatorState41286.cycleOffset = 0f;
				assaultRifleAnimatorState41286.cycleOffsetParameterActive = false;
				assaultRifleAnimatorState41286.iKOnFeet = false;
				assaultRifleAnimatorState41286.mirror = false;
				assaultRifleAnimatorState41286.mirrorParameterActive = false;
				assaultRifleAnimatorState41286.speed = 1f;
				assaultRifleAnimatorState41286.speedParameterActive = false;
				assaultRifleAnimatorState41286.writeDefaultValues = true;

				var sniperRifleAnimatorState41288 = rollAnimatorStateMachine41082.AddState("Sniper Rifle", new Vector3(384f, 252f, 0f));
				sniperRifleAnimatorState41288.motion = sniperRifleRollIdleAnimationClip41026;
				sniperRifleAnimatorState41288.cycleOffset = 0f;
				sniperRifleAnimatorState41288.cycleOffsetParameterActive = false;
				sniperRifleAnimatorState41288.iKOnFeet = false;
				sniperRifleAnimatorState41288.mirror = false;
				sniperRifleAnimatorState41288.mirrorParameterActive = false;
				sniperRifleAnimatorState41288.speed = 1f;
				sniperRifleAnimatorState41288.speedParameterActive = false;
				sniperRifleAnimatorState41288.writeDefaultValues = true;

				var shotgunAnimatorState41280 = rollAnimatorStateMachine41082.AddState("Shotgun", new Vector3(384f, 192f, 0f));
				shotgunAnimatorState41280.motion = shotgunRollIdleAnimationClip41022;
				shotgunAnimatorState41280.cycleOffset = 0f;
				shotgunAnimatorState41280.cycleOffsetParameterActive = false;
				shotgunAnimatorState41280.iKOnFeet = false;
				shotgunAnimatorState41280.mirror = false;
				shotgunAnimatorState41280.mirrorParameterActive = false;
				shotgunAnimatorState41280.speed = 1f;
				shotgunAnimatorState41280.speedParameterActive = false;
				shotgunAnimatorState41280.writeDefaultValues = true;

				var rocketLauncherAnimatorState41284 = rollAnimatorStateMachine41082.AddState("Rocket Launcher", new Vector3(384f, 132f, 0f));
				rocketLauncherAnimatorState41284.motion = rocketLauncherRollIdleAnimationClip41018;
				rocketLauncherAnimatorState41284.cycleOffset = 0f;
				rocketLauncherAnimatorState41284.cycleOffsetParameterActive = false;
				rocketLauncherAnimatorState41284.iKOnFeet = false;
				rocketLauncherAnimatorState41284.mirror = false;
				rocketLauncherAnimatorState41284.mirrorParameterActive = false;
				rocketLauncherAnimatorState41284.speed = 1f;
				rocketLauncherAnimatorState41284.speedParameterActive = false;
				rocketLauncherAnimatorState41284.writeDefaultValues = true;

				var dualPistolAnimatorState41278 = rollAnimatorStateMachine41082.AddState("Dual Pistol", new Vector3(384f, -108f, 0f));
				dualPistolAnimatorState41278.motion = dualPistolRollIdleAnimationClip41014;
				dualPistolAnimatorState41278.cycleOffset = 0f;
				dualPistolAnimatorState41278.cycleOffsetParameterActive = false;
				dualPistolAnimatorState41278.iKOnFeet = false;
				dualPistolAnimatorState41278.mirror = false;
				dualPistolAnimatorState41278.mirrorParameterActive = false;
				dualPistolAnimatorState41278.speed = 1f;
				dualPistolAnimatorState41278.speedParameterActive = false;
				dualPistolAnimatorState41278.writeDefaultValues = true;

				var pistolAnimatorState41282 = rollAnimatorStateMachine41082.AddState("Pistol", new Vector3(384f, 72f, 0f));
				pistolAnimatorState41282.motion = pistolRollIdleAnimationClip41010;
				pistolAnimatorState41282.cycleOffset = 0f;
				pistolAnimatorState41282.cycleOffsetParameterActive = false;
				pistolAnimatorState41282.iKOnFeet = false;
				pistolAnimatorState41282.mirror = false;
				pistolAnimatorState41282.mirrorParameterActive = false;
				pistolAnimatorState41282.speed = 1f;
				pistolAnimatorState41282.speedParameterActive = false;
				pistolAnimatorState41282.writeDefaultValues = true;

				var swordAnimatorState41270 = rollAnimatorStateMachine41082.AddState("Sword", new Vector3(384f, 312f, 0f));
				swordAnimatorState41270.motion = swordRollIdleAnimationClip41496;
				swordAnimatorState41270.cycleOffset = 0f;
				swordAnimatorState41270.cycleOffsetParameterActive = false;
				swordAnimatorState41270.iKOnFeet = false;
				swordAnimatorState41270.mirror = false;
				swordAnimatorState41270.mirrorParameterActive = false;
				swordAnimatorState41270.speed = 1f;
				swordAnimatorState41270.speedParameterActive = false;
				swordAnimatorState41270.writeDefaultValues = true;

				var katanaAnimatorState41274 = rollAnimatorStateMachine41082.AddState("Katana", new Vector3(384f, -48f, 0f));
				katanaAnimatorState41274.motion = katanaRollIdleAnimationClip41500;
				katanaAnimatorState41274.cycleOffset = 0f;
				katanaAnimatorState41274.cycleOffsetParameterActive = false;
				katanaAnimatorState41274.iKOnFeet = false;
				katanaAnimatorState41274.mirror = false;
				katanaAnimatorState41274.mirrorParameterActive = false;
				katanaAnimatorState41274.speed = 1f;
				katanaAnimatorState41274.speedParameterActive = false;
				katanaAnimatorState41274.writeDefaultValues = true;

				var bowAnimatorState41276 = rollAnimatorStateMachine41082.AddState("Bow", new Vector3(384f, -168f, 0f));
				bowAnimatorState41276.motion = bowRollIdleAnimationClip41034;
				bowAnimatorState41276.cycleOffset = 0f;
				bowAnimatorState41276.cycleOffsetParameterActive = false;
				bowAnimatorState41276.iKOnFeet = false;
				bowAnimatorState41276.mirror = false;
				bowAnimatorState41276.mirrorParameterActive = false;
				bowAnimatorState41276.speed = 1f;
				bowAnimatorState41276.speedParameterActive = false;
				bowAnimatorState41276.writeDefaultValues = true;

				var knifeAnimatorState41272 = rollAnimatorStateMachine41082.AddState("Knife", new Vector3(384f, 12f, 0f));
				knifeAnimatorState41272.motion = knifeRollIdleAnimationClip41506;
				knifeAnimatorState41272.cycleOffset = 0f;
				knifeAnimatorState41272.cycleOffsetParameterActive = false;
				knifeAnimatorState41272.iKOnFeet = false;
				knifeAnimatorState41272.mirror = false;
				knifeAnimatorState41272.mirrorParameterActive = false;
				knifeAnimatorState41272.speed = 1f;
				knifeAnimatorState41272.speedParameterActive = false;
				knifeAnimatorState41272.writeDefaultValues = true;

				// State Machine Defaults.
				rollAnimatorStateMachine41082.anyStatePosition = new Vector3(48f, 48f, 0f);
				rollAnimatorStateMachine41082.defaultState = assaultRifleAnimatorState41286;
				rollAnimatorStateMachine41082.entryPosition = new Vector3(48f, 0f, 0f);
				rollAnimatorStateMachine41082.exitPosition = new Vector3(768f, 36f, 0f);
				rollAnimatorStateMachine41082.parentStateMachinePosition = new Vector3(744f, -48f, 0f);

				// State Transitions.
				var animatorStateTransition41482 = assaultRifleAnimatorState41286.AddExitTransition();
				animatorStateTransition41482.canTransitionToSelf = true;
				animatorStateTransition41482.duration = 0.15f;
				animatorStateTransition41482.exitTime = 0f;
				animatorStateTransition41482.hasExitTime = false;
				animatorStateTransition41482.hasFixedDuration = true;
				animatorStateTransition41482.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41482.offset = 0f;
				animatorStateTransition41482.orderedInterruption = true;
				animatorStateTransition41482.isExit = true;
				animatorStateTransition41482.mute = false;
				animatorStateTransition41482.solo = false;
				animatorStateTransition41482.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41484 = sniperRifleAnimatorState41288.AddExitTransition();
				animatorStateTransition41484.canTransitionToSelf = true;
				animatorStateTransition41484.duration = 0.15f;
				animatorStateTransition41484.exitTime = 0f;
				animatorStateTransition41484.hasExitTime = false;
				animatorStateTransition41484.hasFixedDuration = true;
				animatorStateTransition41484.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41484.offset = 0f;
				animatorStateTransition41484.orderedInterruption = true;
				animatorStateTransition41484.isExit = true;
				animatorStateTransition41484.mute = false;
				animatorStateTransition41484.solo = false;
				animatorStateTransition41484.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41486 = shotgunAnimatorState41280.AddExitTransition();
				animatorStateTransition41486.canTransitionToSelf = true;
				animatorStateTransition41486.duration = 0.15f;
				animatorStateTransition41486.exitTime = 0f;
				animatorStateTransition41486.hasExitTime = false;
				animatorStateTransition41486.hasFixedDuration = true;
				animatorStateTransition41486.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41486.offset = 0f;
				animatorStateTransition41486.orderedInterruption = true;
				animatorStateTransition41486.isExit = true;
				animatorStateTransition41486.mute = false;
				animatorStateTransition41486.solo = false;
				animatorStateTransition41486.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41488 = rocketLauncherAnimatorState41284.AddExitTransition();
				animatorStateTransition41488.canTransitionToSelf = true;
				animatorStateTransition41488.duration = 0.15f;
				animatorStateTransition41488.exitTime = 0f;
				animatorStateTransition41488.hasExitTime = false;
				animatorStateTransition41488.hasFixedDuration = true;
				animatorStateTransition41488.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41488.offset = 0f;
				animatorStateTransition41488.orderedInterruption = true;
				animatorStateTransition41488.isExit = true;
				animatorStateTransition41488.mute = false;
				animatorStateTransition41488.solo = false;
				animatorStateTransition41488.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41490 = dualPistolAnimatorState41278.AddExitTransition();
				animatorStateTransition41490.canTransitionToSelf = true;
				animatorStateTransition41490.duration = 0.15f;
				animatorStateTransition41490.exitTime = 0f;
				animatorStateTransition41490.hasExitTime = false;
				animatorStateTransition41490.hasFixedDuration = true;
				animatorStateTransition41490.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41490.offset = 0f;
				animatorStateTransition41490.orderedInterruption = true;
				animatorStateTransition41490.isExit = true;
				animatorStateTransition41490.mute = false;
				animatorStateTransition41490.solo = false;
				animatorStateTransition41490.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41492 = pistolAnimatorState41282.AddExitTransition();
				animatorStateTransition41492.canTransitionToSelf = true;
				animatorStateTransition41492.duration = 0.15f;
				animatorStateTransition41492.exitTime = 0f;
				animatorStateTransition41492.hasExitTime = false;
				animatorStateTransition41492.hasFixedDuration = true;
				animatorStateTransition41492.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41492.offset = 0f;
				animatorStateTransition41492.orderedInterruption = true;
				animatorStateTransition41492.isExit = true;
				animatorStateTransition41492.mute = false;
				animatorStateTransition41492.solo = false;
				animatorStateTransition41492.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41494 = swordAnimatorState41270.AddExitTransition();
				animatorStateTransition41494.canTransitionToSelf = true;
				animatorStateTransition41494.duration = 0.15f;
				animatorStateTransition41494.exitTime = 0f;
				animatorStateTransition41494.hasExitTime = false;
				animatorStateTransition41494.hasFixedDuration = true;
				animatorStateTransition41494.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41494.offset = 0f;
				animatorStateTransition41494.orderedInterruption = false;
				animatorStateTransition41494.isExit = true;
				animatorStateTransition41494.mute = false;
				animatorStateTransition41494.solo = false;
				animatorStateTransition41494.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41498 = katanaAnimatorState41274.AddExitTransition();
				animatorStateTransition41498.canTransitionToSelf = true;
				animatorStateTransition41498.duration = 15f;
				animatorStateTransition41498.exitTime = 0f;
				animatorStateTransition41498.hasExitTime = false;
				animatorStateTransition41498.hasFixedDuration = true;
				animatorStateTransition41498.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41498.offset = 0f;
				animatorStateTransition41498.orderedInterruption = true;
				animatorStateTransition41498.isExit = true;
				animatorStateTransition41498.mute = false;
				animatorStateTransition41498.solo = false;
				animatorStateTransition41498.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41502 = bowAnimatorState41276.AddExitTransition();
				animatorStateTransition41502.canTransitionToSelf = true;
				animatorStateTransition41502.duration = 0.15f;
				animatorStateTransition41502.exitTime = 0f;
				animatorStateTransition41502.hasExitTime = false;
				animatorStateTransition41502.hasFixedDuration = true;
				animatorStateTransition41502.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41502.offset = 0f;
				animatorStateTransition41502.orderedInterruption = true;
				animatorStateTransition41502.isExit = true;
				animatorStateTransition41502.mute = false;
				animatorStateTransition41502.solo = false;
				animatorStateTransition41502.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				var animatorStateTransition41504 = knifeAnimatorState41272.AddExitTransition();
				animatorStateTransition41504.canTransitionToSelf = true;
				animatorStateTransition41504.duration = 0.15f;
				animatorStateTransition41504.exitTime = 0f;
				animatorStateTransition41504.hasExitTime = false;
				animatorStateTransition41504.hasFixedDuration = true;
				animatorStateTransition41504.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41504.offset = 0f;
				animatorStateTransition41504.orderedInterruption = true;
				animatorStateTransition41504.isExit = true;
				animatorStateTransition41504.mute = false;
				animatorStateTransition41504.solo = false;
				animatorStateTransition41504.AddCondition(AnimatorConditionMode.NotEqual, 102f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition41158 = baseStateMachine270919106.AddAnyStateTransition(swordAnimatorState41270);
				animatorStateTransition41158.canTransitionToSelf = false;
				animatorStateTransition41158.duration = 0.05f;
				animatorStateTransition41158.exitTime = 0.75f;
				animatorStateTransition41158.hasExitTime = false;
				animatorStateTransition41158.hasFixedDuration = true;
				animatorStateTransition41158.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41158.offset = 0f;
				animatorStateTransition41158.orderedInterruption = true;
				animatorStateTransition41158.isExit = false;
				animatorStateTransition41158.mute = false;
				animatorStateTransition41158.solo = false;
				animatorStateTransition41158.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41158.AddCondition(AnimatorConditionMode.Equals, 22f, "Slot0ItemID");

				var animatorStateTransition41160 = baseStateMachine270919106.AddAnyStateTransition(knifeAnimatorState41272);
				animatorStateTransition41160.canTransitionToSelf = false;
				animatorStateTransition41160.duration = 0.05f;
				animatorStateTransition41160.exitTime = 0.75f;
				animatorStateTransition41160.hasExitTime = false;
				animatorStateTransition41160.hasFixedDuration = true;
				animatorStateTransition41160.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41160.offset = 0f;
				animatorStateTransition41160.orderedInterruption = true;
				animatorStateTransition41160.isExit = false;
				animatorStateTransition41160.mute = false;
				animatorStateTransition41160.solo = false;
				animatorStateTransition41160.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41160.AddCondition(AnimatorConditionMode.Equals, 23f, "Slot0ItemID");

				var animatorStateTransition41162 = baseStateMachine270919106.AddAnyStateTransition(katanaAnimatorState41274);
				animatorStateTransition41162.canTransitionToSelf = false;
				animatorStateTransition41162.duration = 0.05f;
				animatorStateTransition41162.exitTime = 0.75f;
				animatorStateTransition41162.hasExitTime = false;
				animatorStateTransition41162.hasFixedDuration = true;
				animatorStateTransition41162.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41162.offset = 0f;
				animatorStateTransition41162.orderedInterruption = true;
				animatorStateTransition41162.isExit = false;
				animatorStateTransition41162.mute = false;
				animatorStateTransition41162.solo = false;
				animatorStateTransition41162.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41162.AddCondition(AnimatorConditionMode.Equals, 24f, "Slot0ItemID");

				var animatorStateTransition41164 = baseStateMachine270919106.AddAnyStateTransition(bowAnimatorState41276);
				animatorStateTransition41164.canTransitionToSelf = false;
				animatorStateTransition41164.duration = 0.05f;
				animatorStateTransition41164.exitTime = 0.75f;
				animatorStateTransition41164.hasExitTime = false;
				animatorStateTransition41164.hasFixedDuration = true;
				animatorStateTransition41164.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41164.offset = 0f;
				animatorStateTransition41164.orderedInterruption = true;
				animatorStateTransition41164.isExit = false;
				animatorStateTransition41164.mute = false;
				animatorStateTransition41164.solo = false;
				animatorStateTransition41164.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41164.AddCondition(AnimatorConditionMode.Equals, 4f, "Slot1ItemID");

				var animatorStateTransition41166 = baseStateMachine270919106.AddAnyStateTransition(dualPistolAnimatorState41278);
				animatorStateTransition41166.canTransitionToSelf = false;
				animatorStateTransition41166.duration = 0.05f;
				animatorStateTransition41166.exitTime = 0.75f;
				animatorStateTransition41166.hasExitTime = false;
				animatorStateTransition41166.hasFixedDuration = true;
				animatorStateTransition41166.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41166.offset = 0f;
				animatorStateTransition41166.orderedInterruption = true;
				animatorStateTransition41166.isExit = false;
				animatorStateTransition41166.mute = false;
				animatorStateTransition41166.solo = false;
				animatorStateTransition41166.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41166.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition41166.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot1ItemID");

				var animatorStateTransition41168 = baseStateMachine270919106.AddAnyStateTransition(shotgunAnimatorState41280);
				animatorStateTransition41168.canTransitionToSelf = false;
				animatorStateTransition41168.duration = 0.05f;
				animatorStateTransition41168.exitTime = 0.75f;
				animatorStateTransition41168.hasExitTime = false;
				animatorStateTransition41168.hasFixedDuration = true;
				animatorStateTransition41168.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41168.offset = 0f;
				animatorStateTransition41168.orderedInterruption = true;
				animatorStateTransition41168.isExit = false;
				animatorStateTransition41168.mute = false;
				animatorStateTransition41168.solo = false;
				animatorStateTransition41168.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41168.AddCondition(AnimatorConditionMode.Equals, 3f, "Slot0ItemID");

				var animatorStateTransition41170 = baseStateMachine270919106.AddAnyStateTransition(pistolAnimatorState41282);
				animatorStateTransition41170.canTransitionToSelf = false;
				animatorStateTransition41170.duration = 0.05f;
				animatorStateTransition41170.exitTime = 0.75f;
				animatorStateTransition41170.hasExitTime = false;
				animatorStateTransition41170.hasFixedDuration = true;
				animatorStateTransition41170.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41170.offset = 0f;
				animatorStateTransition41170.orderedInterruption = true;
				animatorStateTransition41170.isExit = false;
				animatorStateTransition41170.mute = false;
				animatorStateTransition41170.solo = false;
				animatorStateTransition41170.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41170.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition41170.AddCondition(AnimatorConditionMode.NotEqual, 2f, "Slot1ItemID");

				var animatorStateTransition41172 = baseStateMachine270919106.AddAnyStateTransition(rocketLauncherAnimatorState41284);
				animatorStateTransition41172.canTransitionToSelf = false;
				animatorStateTransition41172.duration = 0.05f;
				animatorStateTransition41172.exitTime = 0.75f;
				animatorStateTransition41172.hasExitTime = false;
				animatorStateTransition41172.hasFixedDuration = true;
				animatorStateTransition41172.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41172.offset = 0f;
				animatorStateTransition41172.orderedInterruption = true;
				animatorStateTransition41172.isExit = false;
				animatorStateTransition41172.mute = false;
				animatorStateTransition41172.solo = false;
				animatorStateTransition41172.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41172.AddCondition(AnimatorConditionMode.Equals, 6f, "Slot0ItemID");

				var animatorStateTransition41174 = baseStateMachine270919106.AddAnyStateTransition(assaultRifleAnimatorState41286);
				animatorStateTransition41174.canTransitionToSelf = false;
				animatorStateTransition41174.duration = 0.05f;
				animatorStateTransition41174.exitTime = 0.75f;
				animatorStateTransition41174.hasExitTime = false;
				animatorStateTransition41174.hasFixedDuration = true;
				animatorStateTransition41174.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41174.offset = 0f;
				animatorStateTransition41174.orderedInterruption = true;
				animatorStateTransition41174.isExit = false;
				animatorStateTransition41174.mute = false;
				animatorStateTransition41174.solo = false;
				animatorStateTransition41174.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41174.AddCondition(AnimatorConditionMode.Equals, 1f, "Slot0ItemID");

				var animatorStateTransition41176 = baseStateMachine270919106.AddAnyStateTransition(sniperRifleAnimatorState41288);
				animatorStateTransition41176.canTransitionToSelf = false;
				animatorStateTransition41176.duration = 0.05f;
				animatorStateTransition41176.exitTime = 0.75f;
				animatorStateTransition41176.hasExitTime = false;
				animatorStateTransition41176.hasFixedDuration = true;
				animatorStateTransition41176.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41176.offset = 0f;
				animatorStateTransition41176.orderedInterruption = true;
				animatorStateTransition41176.isExit = false;
				animatorStateTransition41176.mute = false;
				animatorStateTransition41176.solo = false;
				animatorStateTransition41176.AddCondition(AnimatorConditionMode.Equals, 102f, "AbilityIndex");
				animatorStateTransition41176.AddCondition(AnimatorConditionMode.Equals, 5f, "Slot0ItemID");
			}
		}
	}
}
