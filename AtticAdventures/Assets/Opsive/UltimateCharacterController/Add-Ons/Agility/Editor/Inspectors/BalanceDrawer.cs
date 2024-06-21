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
	/// Implements AbilityDrawer for the Balance ControlType.
	/// </summary>
	[ControlType(typeof(Balance))]
	public class BalanceDrawer : AbilityDrawer
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

				var baseStateMachine481083488 = animatorControllers[i].layers[0].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine481083488.stateMachines.Length; ++k) {
						if (baseStateMachine481083488.stateMachines[k].stateMachine.name == "Balance") {
							baseStateMachine481083488.RemoveStateMachine(baseStateMachine481083488.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var balanceBwdAnimationClip37602Path = AssetDatabase.GUIDToAssetPath("1eb167ae0309ef345acbb8e77a657f82"); 
				var balanceBwdAnimationClip37602 = AnimatorBuilder.GetAnimationClip(balanceBwdAnimationClip37602Path, "BalanceBwd");
				var balanceIdleRightAnimationClip37604Path = AssetDatabase.GUIDToAssetPath("a433ee9521fa9d741b694e52eb438d29"); 
				var balanceIdleRightAnimationClip37604 = AnimatorBuilder.GetAnimationClip(balanceIdleRightAnimationClip37604Path, "BalanceIdleRight");
				var balanceFwdAnimationClip37606Path = AssetDatabase.GUIDToAssetPath("47a0520f151e5c643aa6ef9c68654a51"); 
				var balanceFwdAnimationClip37606 = AnimatorBuilder.GetAnimationClip(balanceFwdAnimationClip37606Path, "BalanceFwd");
				var balanceIdleLeftAnimationClip37612Path = AssetDatabase.GUIDToAssetPath("a433ee9521fa9d741b694e52eb438d29"); 
				var balanceIdleLeftAnimationClip37612 = AnimatorBuilder.GetAnimationClip(balanceIdleLeftAnimationClip37612Path, "BalanceIdleLeft");

				// State Machine.
				var balanceAnimatorStateMachine34264 = baseStateMachine481083488.AddStateMachine("Balance", new Vector3(624f, 12f, 0f));

				// States.
				var balanceMovementAnimatorState35126 = balanceAnimatorStateMachine34264.AddState("Balance Movement", new Vector3(432f, 96f, 0f));
				var balanceMovementAnimatorState35126blendTreeBlendTree37600 = new BlendTree();
				AssetDatabase.AddObjectToAsset(balanceMovementAnimatorState35126blendTreeBlendTree37600, animatorControllers[i]);
				balanceMovementAnimatorState35126blendTreeBlendTree37600.hideFlags = HideFlags.HideInHierarchy;
				balanceMovementAnimatorState35126blendTreeBlendTree37600.blendParameter = "ForwardMovement";
				balanceMovementAnimatorState35126blendTreeBlendTree37600.blendParameterY = "ForwardMovement";
				balanceMovementAnimatorState35126blendTreeBlendTree37600.blendType = BlendTreeType.Simple1D;
				balanceMovementAnimatorState35126blendTreeBlendTree37600.maxThreshold = 1f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600.minThreshold = -1f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600.name = "Blend Tree";
				balanceMovementAnimatorState35126blendTreeBlendTree37600.useAutomaticThresholds = false;
				var balanceMovementAnimatorState35126blendTreeBlendTree37600Child0 =  new ChildMotion();
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.motion = balanceBwdAnimationClip37602;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.cycleOffset = 0f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.directBlendParameter = "HorizontalMovement";
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.mirror = false;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.position = new Vector2(0f, -1f);
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.threshold = -1f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child0.timeScale = 1.4f;
				var balanceMovementAnimatorState35126blendTreeBlendTree37600Child1 =  new ChildMotion();
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.motion = balanceIdleRightAnimationClip37604;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.cycleOffset = 0f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.directBlendParameter = "HorizontalMovement";
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.mirror = false;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.position = new Vector2(0f, 0f);
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.threshold = 0f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child1.timeScale = 1f;
				var balanceMovementAnimatorState35126blendTreeBlendTree37600Child2 =  new ChildMotion();
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.motion = balanceFwdAnimationClip37606;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.cycleOffset = 0f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.directBlendParameter = "HorizontalMovement";
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.mirror = false;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.position = new Vector2(0f, 1f);
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.threshold = 1f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600Child2.timeScale = 1.4f;
				balanceMovementAnimatorState35126blendTreeBlendTree37600.children = new ChildMotion[] {
					balanceMovementAnimatorState35126blendTreeBlendTree37600Child0,
					balanceMovementAnimatorState35126blendTreeBlendTree37600Child1,
					balanceMovementAnimatorState35126blendTreeBlendTree37600Child2
				};
				balanceMovementAnimatorState35126.motion = balanceMovementAnimatorState35126blendTreeBlendTree37600;
				balanceMovementAnimatorState35126.cycleOffset = 0f;
				balanceMovementAnimatorState35126.cycleOffsetParameterActive = false;
				balanceMovementAnimatorState35126.iKOnFeet = true;
				balanceMovementAnimatorState35126.mirror = false;
				balanceMovementAnimatorState35126.mirrorParameterActive = false;
				balanceMovementAnimatorState35126.speed = 1f;
				balanceMovementAnimatorState35126.speedParameterActive = false;
				balanceMovementAnimatorState35126.writeDefaultValues = true;

				var balanceIdleLeftAnimatorState37592 = balanceAnimatorStateMachine34264.AddState("Balance Idle Left", new Vector3(564f, -24f, 0f));
				balanceIdleLeftAnimatorState37592.motion = balanceIdleLeftAnimationClip37612;
				balanceIdleLeftAnimatorState37592.cycleOffset = 0f;
				balanceIdleLeftAnimatorState37592.cycleOffsetParameterActive = false;
				balanceIdleLeftAnimatorState37592.iKOnFeet = true;
				balanceIdleLeftAnimatorState37592.mirror = false;
				balanceIdleLeftAnimatorState37592.mirrorParameterActive = false;
				balanceIdleLeftAnimatorState37592.speed = 1f;
				balanceIdleLeftAnimatorState37592.speedParameterActive = false;
				balanceIdleLeftAnimatorState37592.writeDefaultValues = true;

				var balanceIdleRightAnimatorState35128 = balanceAnimatorStateMachine34264.AddState("Balance Idle Right", new Vector3(312f, -24f, 0f));
				balanceIdleRightAnimatorState35128.motion = balanceIdleRightAnimationClip37604;
				balanceIdleRightAnimatorState35128.cycleOffset = 0f;
				balanceIdleRightAnimatorState35128.cycleOffsetParameterActive = false;
				balanceIdleRightAnimatorState35128.iKOnFeet = true;
				balanceIdleRightAnimatorState35128.mirror = false;
				balanceIdleRightAnimatorState35128.mirrorParameterActive = false;
				balanceIdleRightAnimatorState35128.speed = 1f;
				balanceIdleRightAnimatorState35128.speedParameterActive = false;
				balanceIdleRightAnimatorState35128.writeDefaultValues = true;

				// State Machine Defaults.
				balanceAnimatorStateMachine34264.anyStatePosition = new Vector3(50f, 20f, 0f);
				balanceAnimatorStateMachine34264.defaultState = balanceMovementAnimatorState35126;
				balanceAnimatorStateMachine34264.entryPosition = new Vector3(50f, 120f, 0f);
				balanceAnimatorStateMachine34264.exitPosition = new Vector3(800f, 120f, 0f);
				balanceAnimatorStateMachine34264.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Transitions.
				var animatorStateTransition37594 = balanceMovementAnimatorState35126.AddExitTransition();
				animatorStateTransition37594.canTransitionToSelf = true;
				animatorStateTransition37594.duration = 0.15f;
				animatorStateTransition37594.exitTime = 0.9f;
				animatorStateTransition37594.hasExitTime = false;
				animatorStateTransition37594.hasFixedDuration = true;
				animatorStateTransition37594.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37594.offset = 0f;
				animatorStateTransition37594.orderedInterruption = true;
				animatorStateTransition37594.isExit = true;
				animatorStateTransition37594.mute = false;
				animatorStateTransition37594.solo = false;
				animatorStateTransition37594.AddCondition(AnimatorConditionMode.NotEqual, 107f, "AbilityIndex");

				var animatorStateTransition37596 = balanceMovementAnimatorState35126.AddTransition(balanceIdleRightAnimatorState35128);
				animatorStateTransition37596.canTransitionToSelf = true;
				animatorStateTransition37596.duration = 0.3f;
				animatorStateTransition37596.exitTime = 0.91f;
				animatorStateTransition37596.hasExitTime = false;
				animatorStateTransition37596.hasFixedDuration = true;
				animatorStateTransition37596.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37596.offset = 0f;
				animatorStateTransition37596.orderedInterruption = true;
				animatorStateTransition37596.isExit = false;
				animatorStateTransition37596.mute = false;
				animatorStateTransition37596.solo = false;
				animatorStateTransition37596.AddCondition(AnimatorConditionMode.Equals, 107f, "AbilityIndex");
				animatorStateTransition37596.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");
				animatorStateTransition37596.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");

				var animatorStateTransition37598 = balanceMovementAnimatorState35126.AddTransition(balanceIdleLeftAnimatorState37592);
				animatorStateTransition37598.canTransitionToSelf = true;
				animatorStateTransition37598.duration = 0.3f;
				animatorStateTransition37598.exitTime = 0.91f;
				animatorStateTransition37598.hasExitTime = false;
				animatorStateTransition37598.hasFixedDuration = true;
				animatorStateTransition37598.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37598.offset = 0f;
				animatorStateTransition37598.orderedInterruption = true;
				animatorStateTransition37598.isExit = false;
				animatorStateTransition37598.mute = false;
				animatorStateTransition37598.solo = false;
				animatorStateTransition37598.AddCondition(AnimatorConditionMode.Equals, 107f, "AbilityIndex");
				animatorStateTransition37598.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");
				animatorStateTransition37598.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition37608 = balanceIdleLeftAnimatorState37592.AddExitTransition();
				animatorStateTransition37608.canTransitionToSelf = true;
				animatorStateTransition37608.duration = 0.15f;
				animatorStateTransition37608.exitTime = 0.95f;
				animatorStateTransition37608.hasExitTime = false;
				animatorStateTransition37608.hasFixedDuration = true;
				animatorStateTransition37608.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37608.offset = 0f;
				animatorStateTransition37608.orderedInterruption = true;
				animatorStateTransition37608.isExit = true;
				animatorStateTransition37608.mute = false;
				animatorStateTransition37608.solo = false;
				animatorStateTransition37608.AddCondition(AnimatorConditionMode.NotEqual, 107f, "AbilityIndex");

				var animatorStateTransition37610 = balanceIdleLeftAnimatorState37592.AddTransition(balanceMovementAnimatorState35126);
				animatorStateTransition37610.canTransitionToSelf = true;
				animatorStateTransition37610.duration = 0.2f;
				animatorStateTransition37610.exitTime = 0.95f;
				animatorStateTransition37610.hasExitTime = false;
				animatorStateTransition37610.hasFixedDuration = true;
				animatorStateTransition37610.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37610.offset = 0.6f;
				animatorStateTransition37610.orderedInterruption = true;
				animatorStateTransition37610.isExit = false;
				animatorStateTransition37610.mute = false;
				animatorStateTransition37610.solo = false;
				animatorStateTransition37610.AddCondition(AnimatorConditionMode.Equals, 107f, "AbilityIndex");
				animatorStateTransition37610.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				var animatorStateTransition37614 = balanceIdleRightAnimatorState35128.AddExitTransition();
				animatorStateTransition37614.canTransitionToSelf = true;
				animatorStateTransition37614.duration = 0.15f;
				animatorStateTransition37614.exitTime = 0.95f;
				animatorStateTransition37614.hasExitTime = false;
				animatorStateTransition37614.hasFixedDuration = true;
				animatorStateTransition37614.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37614.offset = 0f;
				animatorStateTransition37614.orderedInterruption = true;
				animatorStateTransition37614.isExit = true;
				animatorStateTransition37614.mute = false;
				animatorStateTransition37614.solo = false;
				animatorStateTransition37614.AddCondition(AnimatorConditionMode.NotEqual, 107f, "AbilityIndex");

				var animatorStateTransition37616 = balanceIdleRightAnimatorState35128.AddTransition(balanceMovementAnimatorState35126);
				animatorStateTransition37616.canTransitionToSelf = true;
				animatorStateTransition37616.duration = 0.2f;
				animatorStateTransition37616.exitTime = 0.95f;
				animatorStateTransition37616.hasExitTime = false;
				animatorStateTransition37616.hasFixedDuration = true;
				animatorStateTransition37616.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37616.offset = 0.1f;
				animatorStateTransition37616.orderedInterruption = true;
				animatorStateTransition37616.isExit = false;
				animatorStateTransition37616.mute = false;
				animatorStateTransition37616.solo = false;
				animatorStateTransition37616.AddCondition(AnimatorConditionMode.Equals, 107f, "AbilityIndex");
				animatorStateTransition37616.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				// State Machine Transitions.
				var animatorStateTransition34800 = baseStateMachine481083488.AddAnyStateTransition(balanceMovementAnimatorState35126);
				animatorStateTransition34800.canTransitionToSelf = false;
				animatorStateTransition34800.duration = 0.15f;
				animatorStateTransition34800.exitTime = 0.75f;
				animatorStateTransition34800.hasExitTime = false;
				animatorStateTransition34800.hasFixedDuration = true;
				animatorStateTransition34800.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34800.offset = 0f;
				animatorStateTransition34800.orderedInterruption = true;
				animatorStateTransition34800.isExit = false;
				animatorStateTransition34800.mute = false;
				animatorStateTransition34800.solo = false;
				animatorStateTransition34800.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34800.AddCondition(AnimatorConditionMode.Equals, 107f, "AbilityIndex");
				animatorStateTransition34800.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				var animatorStateTransition34802 = baseStateMachine481083488.AddAnyStateTransition(balanceIdleRightAnimatorState35128);
				animatorStateTransition34802.canTransitionToSelf = false;
				animatorStateTransition34802.duration = 0.15f;
				animatorStateTransition34802.exitTime = 0.75f;
				animatorStateTransition34802.hasExitTime = false;
				animatorStateTransition34802.hasFixedDuration = true;
				animatorStateTransition34802.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34802.offset = 0f;
				animatorStateTransition34802.orderedInterruption = true;
				animatorStateTransition34802.isExit = false;
				animatorStateTransition34802.mute = false;
				animatorStateTransition34802.solo = false;
				animatorStateTransition34802.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34802.AddCondition(AnimatorConditionMode.Equals, 107f, "AbilityIndex");
				animatorStateTransition34802.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");
			}
		}
	}
}
