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
	/// Implements AbilityDrawer for the Hang ControlType.
	/// </summary>
	[ControlType(typeof(Hang))]
	public class HangDrawer : DetectObjectAbilityBaseDrawer
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

				var baseStateMachine345826936 = animatorControllers[i].layers[12].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine345826936.stateMachines.Length; ++k) {
						if (baseStateMachine345826936.stateMachines[k].stateMachine.name == "Hang") {
							baseStateMachine345826936.RemoveStateMachine(baseStateMachine345826936.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var hangMovementAnimationClip43510Path = AssetDatabase.GUIDToAssetPath("fd8bcd7a5867d3c489f6e8c561390f5c"); 
				var hangMovementAnimationClip43510 = AnimatorBuilder.GetAnimationClip(hangMovementAnimationClip43510Path, "HangMovement");
				var hangIdleAnimationClip43512Path = AssetDatabase.GUIDToAssetPath("bddb2b4a0e5f12f49b71618b412ff5aa"); 
				var hangIdleAnimationClip43512 = AnimatorBuilder.GetAnimationClip(hangIdleAnimationClip43512Path, "HangIdle");
				var hangStartAnimationClip43516Path = AssetDatabase.GUIDToAssetPath("053d29fbe247cbc4f98bd80634cdad46"); 
				var hangStartAnimationClip43516 = AnimatorBuilder.GetAnimationClip(hangStartAnimationClip43516Path, "HangStart");
				var transferRightAnimationClip43522Path = AssetDatabase.GUIDToAssetPath("04c74e7c306a6cf48813b0ffc2e992db"); 
				var transferRightAnimationClip43522 = AnimatorBuilder.GetAnimationClip(transferRightAnimationClip43522Path, "TransferRight");
				var transferLeftAnimationClip43528Path = AssetDatabase.GUIDToAssetPath("77339d9bf70da1441b2e6b4de0bbc3d8"); 
				var transferLeftAnimationClip43528 = AnimatorBuilder.GetAnimationClip(transferLeftAnimationClip43528Path, "TransferLeft");
				var transferUpAnimationClip43534Path = AssetDatabase.GUIDToAssetPath("9409cd14629cef747a96eac1d5fa22f7"); 
				var transferUpAnimationClip43534 = AnimatorBuilder.GetAnimationClip(transferUpAnimationClip43534Path, "TransferUp");
				var dropStartAnimationClip43538Path = AssetDatabase.GUIDToAssetPath("0b38d3330a39f684ab19e45f90a6eead"); 
				var dropStartAnimationClip43538 = AnimatorBuilder.GetAnimationClip(dropStartAnimationClip43538Path, "DropStart");
				var pullUpAnimationClip43542Path = AssetDatabase.GUIDToAssetPath("c7d5ba824b936f649bbb2747a4a3fc4a"); 
				var pullUpAnimationClip43542 = AnimatorBuilder.GetAnimationClip(pullUpAnimationClip43542Path, "PullUp");
				var transferDownAnimationClip43548Path = AssetDatabase.GUIDToAssetPath("2ee34eba64def1b44a6062121131cc13"); 
				var transferDownAnimationClip43548 = AnimatorBuilder.GetAnimationClip(transferDownAnimationClip43548Path, "TransferDown");
				var hangJumpStartAnimationClip43552Path = AssetDatabase.GUIDToAssetPath("053d29fbe247cbc4f98bd80634cdad46"); 
				var hangJumpStartAnimationClip43552 = AnimatorBuilder.GetAnimationClip(hangJumpStartAnimationClip43552Path, "HangJumpStart");
				var dropStartLedgeStrafeAnimationClip43556Path = AssetDatabase.GUIDToAssetPath("02e3ce0a2c624334d9af402815fa5f5f"); 
				var dropStartLedgeStrafeAnimationClip43556 = AnimatorBuilder.GetAnimationClip(dropStartLedgeStrafeAnimationClip43556Path, "DropStartLedgeStrafe");

				// State Machine.
				var hangAnimatorStateMachine41920 = baseStateMachine345826936.AddStateMachine("Hang", new Vector3(624f, 204f, 0f));

				// States.
				var hangAnimatorState43484 = hangAnimatorStateMachine41920.AddState("Hang", new Vector3(384f, -84f, 0f));
				var hangAnimatorState43484blendTreeBlendTree43508 = new BlendTree();
				AssetDatabase.AddObjectToAsset(hangAnimatorState43484blendTreeBlendTree43508, animatorControllers[i]);
				hangAnimatorState43484blendTreeBlendTree43508.hideFlags = HideFlags.HideInHierarchy;
				hangAnimatorState43484blendTreeBlendTree43508.blendParameter = "HorizontalMovement";
				hangAnimatorState43484blendTreeBlendTree43508.blendParameterY = "HorizontalMovement";
				hangAnimatorState43484blendTreeBlendTree43508.blendType = BlendTreeType.Simple1D;
				hangAnimatorState43484blendTreeBlendTree43508.maxThreshold = 1f;
				hangAnimatorState43484blendTreeBlendTree43508.minThreshold = -1f;
				hangAnimatorState43484blendTreeBlendTree43508.name = "Blend Tree";
				hangAnimatorState43484blendTreeBlendTree43508.useAutomaticThresholds = false;
				var hangAnimatorState43484blendTreeBlendTree43508Child0 =  new ChildMotion();
				hangAnimatorState43484blendTreeBlendTree43508Child0.motion = hangMovementAnimationClip43510;
				hangAnimatorState43484blendTreeBlendTree43508Child0.cycleOffset = 0f;
				hangAnimatorState43484blendTreeBlendTree43508Child0.directBlendParameter = "HorizontalMovement";
				hangAnimatorState43484blendTreeBlendTree43508Child0.mirror = false;
				hangAnimatorState43484blendTreeBlendTree43508Child0.position = new Vector2(0f, 0f);
				hangAnimatorState43484blendTreeBlendTree43508Child0.threshold = -1f;
				hangAnimatorState43484blendTreeBlendTree43508Child0.timeScale = 1.4f;
				var hangAnimatorState43484blendTreeBlendTree43508Child1 =  new ChildMotion();
				hangAnimatorState43484blendTreeBlendTree43508Child1.motion = hangIdleAnimationClip43512;
				hangAnimatorState43484blendTreeBlendTree43508Child1.cycleOffset = 0f;
				hangAnimatorState43484blendTreeBlendTree43508Child1.directBlendParameter = "HorizontalMovement";
				hangAnimatorState43484blendTreeBlendTree43508Child1.mirror = false;
				hangAnimatorState43484blendTreeBlendTree43508Child1.position = new Vector2(0f, 0f);
				hangAnimatorState43484blendTreeBlendTree43508Child1.threshold = 0f;
				hangAnimatorState43484blendTreeBlendTree43508Child1.timeScale = 1f;
				var hangAnimatorState43484blendTreeBlendTree43508Child2 =  new ChildMotion();
				hangAnimatorState43484blendTreeBlendTree43508Child2.motion = hangMovementAnimationClip43510;
				hangAnimatorState43484blendTreeBlendTree43508Child2.cycleOffset = 0f;
				hangAnimatorState43484blendTreeBlendTree43508Child2.directBlendParameter = "HorizontalMovement";
				hangAnimatorState43484blendTreeBlendTree43508Child2.mirror = false;
				hangAnimatorState43484blendTreeBlendTree43508Child2.position = new Vector2(0f, 0f);
				hangAnimatorState43484blendTreeBlendTree43508Child2.threshold = 1f;
				hangAnimatorState43484blendTreeBlendTree43508Child2.timeScale = -1.4f;
				hangAnimatorState43484blendTreeBlendTree43508.children = new ChildMotion[] {
					hangAnimatorState43484blendTreeBlendTree43508Child0,
					hangAnimatorState43484blendTreeBlendTree43508Child1,
					hangAnimatorState43484blendTreeBlendTree43508Child2
				};
				hangAnimatorState43484.motion = hangAnimatorState43484blendTreeBlendTree43508;
				hangAnimatorState43484.cycleOffset = 0f;
				hangAnimatorState43484.cycleOffsetParameterActive = false;
				hangAnimatorState43484.iKOnFeet = false;
				hangAnimatorState43484.mirror = false;
				hangAnimatorState43484.mirrorParameterActive = false;
				hangAnimatorState43484.speed = 1f;
				hangAnimatorState43484.speedParameterActive = false;
				hangAnimatorState43484.writeDefaultValues = false;

				var hangStartAnimatorState42252 = hangAnimatorStateMachine41920.AddState("Hang Start", new Vector3(384f, -192f, 0f));
				hangStartAnimatorState42252.motion = hangStartAnimationClip43516;
				hangStartAnimatorState42252.cycleOffset = 0f;
				hangStartAnimatorState42252.cycleOffsetParameterActive = false;
				hangStartAnimatorState42252.iKOnFeet = false;
				hangStartAnimatorState42252.mirror = false;
				hangStartAnimatorState42252.mirrorParameterActive = false;
				hangStartAnimatorState42252.speed = 2f;
				hangStartAnimatorState42252.speedParameterActive = false;
				hangStartAnimatorState42252.writeDefaultValues = false;

				var transferRightAnimatorState43486 = hangAnimatorStateMachine41920.AddState("Transfer Right", new Vector3(588f, 12f, 0f));
				transferRightAnimatorState43486.motion = transferRightAnimationClip43522;
				transferRightAnimatorState43486.cycleOffset = 0f;
				transferRightAnimatorState43486.cycleOffsetParameterActive = false;
				transferRightAnimatorState43486.iKOnFeet = true;
				transferRightAnimatorState43486.mirror = false;
				transferRightAnimatorState43486.mirrorParameterActive = false;
				transferRightAnimatorState43486.speed = 1.4f;
				transferRightAnimatorState43486.speedParameterActive = false;
				transferRightAnimatorState43486.writeDefaultValues = true;

				var transferLeftAnimatorState43488 = hangAnimatorStateMachine41920.AddState("Transfer Left", new Vector3(180f, 12f, 0f));
				transferLeftAnimatorState43488.motion = transferLeftAnimationClip43528;
				transferLeftAnimatorState43488.cycleOffset = 0f;
				transferLeftAnimatorState43488.cycleOffsetParameterActive = false;
				transferLeftAnimatorState43488.iKOnFeet = true;
				transferLeftAnimatorState43488.mirror = false;
				transferLeftAnimatorState43488.mirrorParameterActive = false;
				transferLeftAnimatorState43488.speed = 1.4f;
				transferLeftAnimatorState43488.speedParameterActive = false;
				transferLeftAnimatorState43488.writeDefaultValues = true;

				var transferUpAnimatorState43490 = hangAnimatorStateMachine41920.AddState("Transfer Up", new Vector3(264f, 84f, 0f));
				transferUpAnimatorState43490.motion = transferUpAnimationClip43534;
				transferUpAnimatorState43490.cycleOffset = 0f;
				transferUpAnimatorState43490.cycleOffsetParameterActive = false;
				transferUpAnimatorState43490.iKOnFeet = true;
				transferUpAnimatorState43490.mirror = false;
				transferUpAnimatorState43490.mirrorParameterActive = false;
				transferUpAnimatorState43490.speed = 1.4f;
				transferUpAnimatorState43490.speedParameterActive = false;
				transferUpAnimatorState43490.writeDefaultValues = true;

				var dropStartAnimatorState42246 = hangAnimatorStateMachine41920.AddState("Drop Start", new Vector3(132f, -192f, 0f));
				dropStartAnimatorState42246.motion = dropStartAnimationClip43538;
				dropStartAnimatorState42246.cycleOffset = 0f;
				dropStartAnimatorState42246.cycleOffsetParameterActive = false;
				dropStartAnimatorState42246.iKOnFeet = true;
				dropStartAnimatorState42246.mirror = false;
				dropStartAnimatorState42246.mirrorParameterActive = false;
				dropStartAnimatorState42246.speed = 1.3f;
				dropStartAnimatorState42246.speedParameterActive = false;
				dropStartAnimatorState42246.writeDefaultValues = true;

				var pullUpAnimatorState43492 = hangAnimatorStateMachine41920.AddState("Pull Up", new Vector3(384f, 168f, 0f));
				pullUpAnimatorState43492.motion = pullUpAnimationClip43542;
				pullUpAnimatorState43492.cycleOffset = 0f;
				pullUpAnimatorState43492.cycleOffsetParameter = "HorizontalMovement";
				pullUpAnimatorState43492.cycleOffsetParameterActive = false;
				pullUpAnimatorState43492.iKOnFeet = true;
				pullUpAnimatorState43492.mirror = false;
				pullUpAnimatorState43492.mirrorParameterActive = false;
				pullUpAnimatorState43492.speed = 1.4f;
				pullUpAnimatorState43492.speedParameterActive = false;
				pullUpAnimatorState43492.writeDefaultValues = true;

				var transferDownAnimatorState43494 = hangAnimatorStateMachine41920.AddState("Transfer Down", new Vector3(504f, 84f, 0f));
				transferDownAnimatorState43494.motion = transferDownAnimationClip43548;
				transferDownAnimatorState43494.cycleOffset = 0f;
				transferDownAnimatorState43494.cycleOffsetParameterActive = false;
				transferDownAnimatorState43494.iKOnFeet = true;
				transferDownAnimatorState43494.mirror = false;
				transferDownAnimatorState43494.mirrorParameterActive = false;
				transferDownAnimatorState43494.speed = 1.4f;
				transferDownAnimatorState43494.speedParameterActive = false;
				transferDownAnimatorState43494.writeDefaultValues = true;

				var hangJumpStartAnimatorState42248 = hangAnimatorStateMachine41920.AddState("Hang Jump Start", new Vector3(624f, -192f, 0f));
				hangJumpStartAnimatorState42248.motion = hangJumpStartAnimationClip43552;
				hangJumpStartAnimatorState42248.cycleOffset = 0f;
				hangJumpStartAnimatorState42248.cycleOffsetParameterActive = false;
				hangJumpStartAnimatorState42248.iKOnFeet = false;
				hangJumpStartAnimatorState42248.mirror = false;
				hangJumpStartAnimatorState42248.mirrorParameterActive = false;
				hangJumpStartAnimatorState42248.speed = 1f;
				hangJumpStartAnimatorState42248.speedParameterActive = false;
				hangJumpStartAnimatorState42248.writeDefaultValues = false;

				var dropStartfromLedgeStrafeAnimatorState42250 = hangAnimatorStateMachine41920.AddState("Drop Start from Ledge Strafe", new Vector3(132f, -264f, 0f));
				dropStartfromLedgeStrafeAnimatorState42250.motion = dropStartLedgeStrafeAnimationClip43556;
				dropStartfromLedgeStrafeAnimatorState42250.cycleOffset = 0f;
				dropStartfromLedgeStrafeAnimatorState42250.cycleOffsetParameterActive = false;
				dropStartfromLedgeStrafeAnimatorState42250.iKOnFeet = true;
				dropStartfromLedgeStrafeAnimatorState42250.mirror = false;
				dropStartfromLedgeStrafeAnimatorState42250.mirrorParameterActive = false;
				dropStartfromLedgeStrafeAnimatorState42250.speed = 1.3f;
				dropStartfromLedgeStrafeAnimatorState42250.speedParameterActive = false;
				dropStartfromLedgeStrafeAnimatorState42250.writeDefaultValues = true;

				// State Machine Defaults.
				hangAnimatorStateMachine41920.anyStatePosition = new Vector3(-84f, -72f, 0f);
				hangAnimatorStateMachine41920.defaultState = hangAnimatorState43484;
				hangAnimatorStateMachine41920.entryPosition = new Vector3(-84f, -156f, 0f);
				hangAnimatorStateMachine41920.exitPosition = new Vector3(876f, -84f, 0f);
				hangAnimatorStateMachine41920.parentStateMachinePosition = new Vector3(852f, -168f, 0f);

				// State Transitions.
				var animatorStateTransition43496 = hangAnimatorState43484.AddExitTransition();
				animatorStateTransition43496.canTransitionToSelf = true;
				animatorStateTransition43496.duration = 0.15f;
				animatorStateTransition43496.exitTime = 0.91f;
				animatorStateTransition43496.hasExitTime = false;
				animatorStateTransition43496.hasFixedDuration = true;
				animatorStateTransition43496.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43496.offset = 0f;
				animatorStateTransition43496.orderedInterruption = true;
				animatorStateTransition43496.isExit = true;
				animatorStateTransition43496.mute = false;
				animatorStateTransition43496.solo = false;
				animatorStateTransition43496.AddCondition(AnimatorConditionMode.NotEqual, 104f, "AbilityIndex");

				var animatorStateTransition43498 = hangAnimatorState43484.AddTransition(transferRightAnimatorState43486);
				animatorStateTransition43498.canTransitionToSelf = true;
				animatorStateTransition43498.duration = 0.15f;
				animatorStateTransition43498.exitTime = 0.91f;
				animatorStateTransition43498.hasExitTime = false;
				animatorStateTransition43498.hasFixedDuration = true;
				animatorStateTransition43498.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43498.offset = 0f;
				animatorStateTransition43498.orderedInterruption = true;
				animatorStateTransition43498.isExit = false;
				animatorStateTransition43498.mute = false;
				animatorStateTransition43498.solo = false;
				animatorStateTransition43498.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition43498.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");

				var animatorStateTransition43500 = hangAnimatorState43484.AddTransition(transferUpAnimatorState43490);
				animatorStateTransition43500.canTransitionToSelf = true;
				animatorStateTransition43500.duration = 0.1f;
				animatorStateTransition43500.exitTime = 0.91f;
				animatorStateTransition43500.hasExitTime = false;
				animatorStateTransition43500.hasFixedDuration = true;
				animatorStateTransition43500.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43500.offset = 0f;
				animatorStateTransition43500.orderedInterruption = true;
				animatorStateTransition43500.isExit = false;
				animatorStateTransition43500.mute = false;
				animatorStateTransition43500.solo = false;
				animatorStateTransition43500.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition43500.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition43502 = hangAnimatorState43484.AddTransition(transferLeftAnimatorState43488);
				animatorStateTransition43502.canTransitionToSelf = true;
				animatorStateTransition43502.duration = 0.15f;
				animatorStateTransition43502.exitTime = 0.91f;
				animatorStateTransition43502.hasExitTime = false;
				animatorStateTransition43502.hasFixedDuration = true;
				animatorStateTransition43502.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43502.offset = 0f;
				animatorStateTransition43502.orderedInterruption = true;
				animatorStateTransition43502.isExit = false;
				animatorStateTransition43502.mute = false;
				animatorStateTransition43502.solo = false;
				animatorStateTransition43502.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition43502.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition43504 = hangAnimatorState43484.AddTransition(pullUpAnimatorState43492);
				animatorStateTransition43504.canTransitionToSelf = true;
				animatorStateTransition43504.duration = 0f;
				animatorStateTransition43504.exitTime = 0.91f;
				animatorStateTransition43504.hasExitTime = false;
				animatorStateTransition43504.hasFixedDuration = false;
				animatorStateTransition43504.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43504.offset = 0f;
				animatorStateTransition43504.orderedInterruption = true;
				animatorStateTransition43504.isExit = false;
				animatorStateTransition43504.mute = false;
				animatorStateTransition43504.solo = false;
				animatorStateTransition43504.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition43504.AddCondition(AnimatorConditionMode.Equals, 7f, "AbilityIntData");

				var animatorStateTransition43506 = hangAnimatorState43484.AddTransition(transferDownAnimatorState43494);
				animatorStateTransition43506.canTransitionToSelf = true;
				animatorStateTransition43506.duration = 0.15f;
				animatorStateTransition43506.exitTime = 0.91f;
				animatorStateTransition43506.hasExitTime = false;
				animatorStateTransition43506.hasFixedDuration = true;
				animatorStateTransition43506.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43506.offset = 0f;
				animatorStateTransition43506.orderedInterruption = true;
				animatorStateTransition43506.isExit = false;
				animatorStateTransition43506.mute = false;
				animatorStateTransition43506.solo = false;
				animatorStateTransition43506.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition43506.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");

				var animatorStateTransition43514 = hangStartAnimatorState42252.AddTransition(hangAnimatorState43484);
				animatorStateTransition43514.canTransitionToSelf = true;
				animatorStateTransition43514.duration = 0.05f;
				animatorStateTransition43514.exitTime = 0.95f;
				animatorStateTransition43514.hasExitTime = true;
				animatorStateTransition43514.hasFixedDuration = true;
				animatorStateTransition43514.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43514.offset = 0f;
				animatorStateTransition43514.orderedInterruption = true;
				animatorStateTransition43514.isExit = false;
				animatorStateTransition43514.mute = false;
				animatorStateTransition43514.solo = false;

				var animatorStateTransition43518 = transferRightAnimatorState43486.AddTransition(hangAnimatorState43484);
				animatorStateTransition43518.canTransitionToSelf = true;
				animatorStateTransition43518.duration = 0.15f;
				animatorStateTransition43518.exitTime = 0.95f;
				animatorStateTransition43518.hasExitTime = true;
				animatorStateTransition43518.hasFixedDuration = true;
				animatorStateTransition43518.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43518.offset = 0f;
				animatorStateTransition43518.orderedInterruption = true;
				animatorStateTransition43518.isExit = false;
				animatorStateTransition43518.mute = false;
				animatorStateTransition43518.solo = false;

				var animatorStateTransition43520 = transferRightAnimatorState43486.AddExitTransition();
				animatorStateTransition43520.canTransitionToSelf = true;
				animatorStateTransition43520.duration = 0.15f;
				animatorStateTransition43520.exitTime = 0.9076923f;
				animatorStateTransition43520.hasExitTime = false;
				animatorStateTransition43520.hasFixedDuration = true;
				animatorStateTransition43520.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43520.offset = 0f;
				animatorStateTransition43520.orderedInterruption = true;
				animatorStateTransition43520.isExit = true;
				animatorStateTransition43520.mute = false;
				animatorStateTransition43520.solo = false;
				animatorStateTransition43520.AddCondition(AnimatorConditionMode.NotEqual, 104f, "AbilityIndex");

				var animatorStateTransition43524 = transferLeftAnimatorState43488.AddTransition(hangAnimatorState43484);
				animatorStateTransition43524.canTransitionToSelf = true;
				animatorStateTransition43524.duration = 0.15f;
				animatorStateTransition43524.exitTime = 0.95f;
				animatorStateTransition43524.hasExitTime = true;
				animatorStateTransition43524.hasFixedDuration = true;
				animatorStateTransition43524.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43524.offset = 0f;
				animatorStateTransition43524.orderedInterruption = true;
				animatorStateTransition43524.isExit = false;
				animatorStateTransition43524.mute = false;
				animatorStateTransition43524.solo = false;

				var animatorStateTransition43526 = transferLeftAnimatorState43488.AddExitTransition();
				animatorStateTransition43526.canTransitionToSelf = true;
				animatorStateTransition43526.duration = 0.15f;
				animatorStateTransition43526.exitTime = 0.9076923f;
				animatorStateTransition43526.hasExitTime = false;
				animatorStateTransition43526.hasFixedDuration = true;
				animatorStateTransition43526.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43526.offset = 0f;
				animatorStateTransition43526.orderedInterruption = true;
				animatorStateTransition43526.isExit = true;
				animatorStateTransition43526.mute = false;
				animatorStateTransition43526.solo = false;
				animatorStateTransition43526.AddCondition(AnimatorConditionMode.NotEqual, 104f, "AbilityIndex");

				var animatorStateTransition43530 = transferUpAnimatorState43490.AddTransition(hangAnimatorState43484);
				animatorStateTransition43530.canTransitionToSelf = true;
				animatorStateTransition43530.duration = 0.15f;
				animatorStateTransition43530.exitTime = 0.95f;
				animatorStateTransition43530.hasExitTime = true;
				animatorStateTransition43530.hasFixedDuration = true;
				animatorStateTransition43530.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43530.offset = 0f;
				animatorStateTransition43530.orderedInterruption = true;
				animatorStateTransition43530.isExit = false;
				animatorStateTransition43530.mute = false;
				animatorStateTransition43530.solo = false;

				var animatorStateTransition43532 = transferUpAnimatorState43490.AddExitTransition();
				animatorStateTransition43532.canTransitionToSelf = true;
				animatorStateTransition43532.duration = 0.15f;
				animatorStateTransition43532.exitTime = 0.8823529f;
				animatorStateTransition43532.hasExitTime = false;
				animatorStateTransition43532.hasFixedDuration = true;
				animatorStateTransition43532.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43532.offset = 0f;
				animatorStateTransition43532.orderedInterruption = true;
				animatorStateTransition43532.isExit = true;
				animatorStateTransition43532.mute = false;
				animatorStateTransition43532.solo = false;
				animatorStateTransition43532.AddCondition(AnimatorConditionMode.NotEqual, 104f, "AbilityIndex");

				var animatorStateTransition43536 = dropStartAnimatorState42246.AddTransition(hangAnimatorState43484);
				animatorStateTransition43536.canTransitionToSelf = true;
				animatorStateTransition43536.duration = 0.15f;
				animatorStateTransition43536.exitTime = 0.99f;
				animatorStateTransition43536.hasExitTime = true;
				animatorStateTransition43536.hasFixedDuration = true;
				animatorStateTransition43536.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43536.offset = 0f;
				animatorStateTransition43536.orderedInterruption = true;
				animatorStateTransition43536.isExit = false;
				animatorStateTransition43536.mute = false;
				animatorStateTransition43536.solo = false;

				var animatorStateTransition43540 = pullUpAnimatorState43492.AddExitTransition();
				animatorStateTransition43540.canTransitionToSelf = true;
				animatorStateTransition43540.duration = 0.25f;
				animatorStateTransition43540.exitTime = 0.95f;
				animatorStateTransition43540.hasExitTime = false;
				animatorStateTransition43540.hasFixedDuration = true;
				animatorStateTransition43540.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43540.offset = 0f;
				animatorStateTransition43540.orderedInterruption = true;
				animatorStateTransition43540.isExit = true;
				animatorStateTransition43540.mute = false;
				animatorStateTransition43540.solo = false;
				animatorStateTransition43540.AddCondition(AnimatorConditionMode.NotEqual, 104f, "AbilityIndex");

				var animatorStateTransition43544 = transferDownAnimatorState43494.AddTransition(hangAnimatorState43484);
				animatorStateTransition43544.canTransitionToSelf = true;
				animatorStateTransition43544.duration = 0.15f;
				animatorStateTransition43544.exitTime = 0.95f;
				animatorStateTransition43544.hasExitTime = true;
				animatorStateTransition43544.hasFixedDuration = true;
				animatorStateTransition43544.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43544.offset = 0f;
				animatorStateTransition43544.orderedInterruption = true;
				animatorStateTransition43544.isExit = false;
				animatorStateTransition43544.mute = false;
				animatorStateTransition43544.solo = false;

				var animatorStateTransition43546 = transferDownAnimatorState43494.AddExitTransition();
				animatorStateTransition43546.canTransitionToSelf = true;
				animatorStateTransition43546.duration = 0.15f;
				animatorStateTransition43546.exitTime = 0.8823529f;
				animatorStateTransition43546.hasExitTime = false;
				animatorStateTransition43546.hasFixedDuration = true;
				animatorStateTransition43546.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43546.offset = 0f;
				animatorStateTransition43546.orderedInterruption = true;
				animatorStateTransition43546.isExit = true;
				animatorStateTransition43546.mute = false;
				animatorStateTransition43546.solo = false;
				animatorStateTransition43546.AddCondition(AnimatorConditionMode.NotEqual, 104f, "AbilityIndex");

				var animatorStateTransition43550 = hangJumpStartAnimatorState42248.AddTransition(hangAnimatorState43484);
				animatorStateTransition43550.canTransitionToSelf = true;
				animatorStateTransition43550.duration = 0.05f;
				animatorStateTransition43550.exitTime = 0.95f;
				animatorStateTransition43550.hasExitTime = true;
				animatorStateTransition43550.hasFixedDuration = true;
				animatorStateTransition43550.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43550.offset = 0f;
				animatorStateTransition43550.orderedInterruption = true;
				animatorStateTransition43550.isExit = false;
				animatorStateTransition43550.mute = false;
				animatorStateTransition43550.solo = false;

				var animatorStateTransition43554 = dropStartfromLedgeStrafeAnimatorState42250.AddTransition(hangAnimatorState43484);
				animatorStateTransition43554.canTransitionToSelf = true;
				animatorStateTransition43554.duration = 0.15f;
				animatorStateTransition43554.exitTime = 0.99f;
				animatorStateTransition43554.hasExitTime = true;
				animatorStateTransition43554.hasFixedDuration = true;
				animatorStateTransition43554.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43554.offset = 0f;
				animatorStateTransition43554.orderedInterruption = true;
				animatorStateTransition43554.isExit = false;
				animatorStateTransition43554.mute = false;
				animatorStateTransition43554.solo = false;

				// State Machine Transitions.
				var animatorStateTransition42112 = baseStateMachine345826936.AddAnyStateTransition(dropStartAnimatorState42246);
				animatorStateTransition42112.canTransitionToSelf = false;
				animatorStateTransition42112.duration = 0.15f;
				animatorStateTransition42112.exitTime = 0.75f;
				animatorStateTransition42112.hasExitTime = false;
				animatorStateTransition42112.hasFixedDuration = true;
				animatorStateTransition42112.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition42112.offset = 0f;
				animatorStateTransition42112.orderedInterruption = true;
				animatorStateTransition42112.isExit = false;
				animatorStateTransition42112.mute = false;
				animatorStateTransition42112.solo = false;
				animatorStateTransition42112.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition42112.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition42112.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition42112.AddCondition(AnimatorConditionMode.Less, 0.1f, "AbilityFloatData");

				var animatorStateTransition42114 = baseStateMachine345826936.AddAnyStateTransition(hangJumpStartAnimatorState42248);
				animatorStateTransition42114.canTransitionToSelf = false;
				animatorStateTransition42114.duration = 0.05f;
				animatorStateTransition42114.exitTime = 0.75f;
				animatorStateTransition42114.hasExitTime = false;
				animatorStateTransition42114.hasFixedDuration = true;
				animatorStateTransition42114.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition42114.offset = 0f;
				animatorStateTransition42114.orderedInterruption = true;
				animatorStateTransition42114.isExit = false;
				animatorStateTransition42114.mute = false;
				animatorStateTransition42114.solo = false;
				animatorStateTransition42114.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition42114.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition42114.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition42116 = baseStateMachine345826936.AddAnyStateTransition(dropStartfromLedgeStrafeAnimatorState42250);
				animatorStateTransition42116.canTransitionToSelf = false;
				animatorStateTransition42116.duration = 0.15f;
				animatorStateTransition42116.exitTime = 0.75f;
				animatorStateTransition42116.hasExitTime = false;
				animatorStateTransition42116.hasFixedDuration = true;
				animatorStateTransition42116.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition42116.offset = 0f;
				animatorStateTransition42116.orderedInterruption = true;
				animatorStateTransition42116.isExit = false;
				animatorStateTransition42116.mute = false;
				animatorStateTransition42116.solo = false;
				animatorStateTransition42116.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition42116.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition42116.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition42116.AddCondition(AnimatorConditionMode.Greater, 0.9f, "AbilityFloatData");
				animatorStateTransition42116.AddCondition(AnimatorConditionMode.Less, 1.1f, "AbilityFloatData");

				var animatorStateTransition42118 = baseStateMachine345826936.AddAnyStateTransition(hangStartAnimatorState42252);
				animatorStateTransition42118.canTransitionToSelf = false;
				animatorStateTransition42118.duration = 0.2f;
				animatorStateTransition42118.exitTime = 0.75f;
				animatorStateTransition42118.hasExitTime = false;
				animatorStateTransition42118.hasFixedDuration = true;
				animatorStateTransition42118.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition42118.offset = 0f;
				animatorStateTransition42118.orderedInterruption = true;
				animatorStateTransition42118.isExit = false;
				animatorStateTransition42118.mute = false;
				animatorStateTransition42118.solo = false;
				animatorStateTransition42118.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition42118.AddCondition(AnimatorConditionMode.Equals, 104f, "AbilityIndex");
				animatorStateTransition42118.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
			}
		}
	}
}
