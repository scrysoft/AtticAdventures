/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Agility.Editor.Inspectors.Character.Abilities
{
	using Opsive.Shared.Editor.UIElements;
	using Opsive.Shared.Editor.UIElements.Controls;
	using Opsive.UltimateCharacterController.Editor.Controls.Types.AbilityDrawers;
	using Opsive.UltimateCharacterController.Editor.Utility;
	using System;
	using System.Reflection;
	using UnityEditor;
	using UnityEditor.Animations;
	using UnityEngine;
	using UnityEngine.UIElements;

	/// <summary>
	/// Implements AbilityDrawer for the LedgeStrafe ControlType.
	/// </summary>
	[ControlType(typeof(LedgeStrafe))]
	public class LedgeStrafeDrawer : AbilityDrawer
	{
		/// <summary>
		/// Returns the control that should be used for the specified ControlType.
		/// </summary>
		/// <param name="unityObject">A reference to the owning Unity Object.</param>
		/// <param name="target">The object that should have its fields displayed.</param>
		/// <param name="container">The container that the UIElements should be added to.</param>
		/// <param name="onChangeEvent">An event that is sent when the value changes. Returns false if the control cannot be changed.</param>
		/// <param name="onValidateChange">Event callback which validates if a field can be changed.</param>
		public override void CreateDrawer(UnityEngine.Object unityObject, object target, VisualElement container, Func<FieldInfo, object, bool> onValidateChange, Action<object> onChangeEvent)
		{
			FieldInspectorView.AddFields(unityObject, target, Opsive.Shared.Utility.MemberVisibility.Public, container, onChangeEvent, null, onValidateChange, false, null, true);
		}

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

				var baseStateMachine315408484 = animatorControllers[i].layers[0].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine315408484.stateMachines.Length; ++k) {
						if (baseStateMachine315408484.stateMachines[k].stateMachine.name == "Ledge Strafe") {
							baseStateMachine315408484.RemoveStateMachine(baseStateMachine315408484.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var ledgeStrafeIdleAnimationClip37700Path = AssetDatabase.GUIDToAssetPath("74100d50a4867ba4bbc2e29cb7155f66"); 
				var ledgeStrafeIdleAnimationClip37700 = AnimatorBuilder.GetAnimationClip(ledgeStrafeIdleAnimationClip37700Path, "LedgeStrafeIdle");
				var ledgeStrafeRightAnimationClip37708Path = AssetDatabase.GUIDToAssetPath("1f453c6b5910ecb46a0c7053d5cbc64e"); 
				var ledgeStrafeRightAnimationClip37708 = AnimatorBuilder.GetAnimationClip(ledgeStrafeRightAnimationClip37708Path, "LedgeStrafeRight");
				var ledgeStrafeLeftAnimationClip37710Path = AssetDatabase.GUIDToAssetPath("1f453c6b5910ecb46a0c7053d5cbc64e"); 
				var ledgeStrafeLeftAnimationClip37710 = AnimatorBuilder.GetAnimationClip(ledgeStrafeLeftAnimationClip37710Path, "LedgeStrafeLeft");

				// State Machine.
				var ledgeStrafeAnimatorStateMachine34268 = baseStateMachine315408484.AddStateMachine("Ledge Strafe", new Vector3(624f, 108f, 0f));

				// States.
				var ledgeIdleAnimatorState35166 = ledgeStrafeAnimatorStateMachine34268.AddState("Ledge Idle", new Vector3(384f, 0f, 0f));
				ledgeIdleAnimatorState35166.motion = ledgeStrafeIdleAnimationClip37700;
				ledgeIdleAnimatorState35166.cycleOffset = 0f;
				ledgeIdleAnimatorState35166.cycleOffsetParameterActive = false;
				ledgeIdleAnimatorState35166.iKOnFeet = true;
				ledgeIdleAnimatorState35166.mirror = false;
				ledgeIdleAnimatorState35166.mirrorParameterActive = false;
				ledgeIdleAnimatorState35166.speed = 1f;
				ledgeIdleAnimatorState35166.speedParameterActive = false;
				ledgeIdleAnimatorState35166.writeDefaultValues = true;

				var ledgeStrafeAnimatorState35168 = ledgeStrafeAnimatorStateMachine34268.AddState("Ledge Strafe", new Vector3(384f, 108f, 0f));
				var ledgeStrafeAnimatorState35168blendTreeBlendTree37706 = new BlendTree();
				AssetDatabase.AddObjectToAsset(ledgeStrafeAnimatorState35168blendTreeBlendTree37706, animatorControllers[i]);
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.hideFlags = HideFlags.HideInHierarchy;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.blendParameter = "HorizontalMovement";
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.blendParameterY = "HorizontalMovement";
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.blendType = BlendTreeType.Simple1D;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.maxThreshold = 1f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.minThreshold = -1f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.name = "Blend Tree";
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.useAutomaticThresholds = false;
				var ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0 =  new ChildMotion();
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.motion = ledgeStrafeRightAnimationClip37708;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.cycleOffset = 0f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.directBlendParameter = "HorizontalMovement";
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.mirror = false;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.position = new Vector2(0f, 0f);
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.threshold = -1f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0.timeScale = 2f;
				var ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1 =  new ChildMotion();
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.motion = ledgeStrafeIdleAnimationClip37700;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.cycleOffset = 0f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.directBlendParameter = "HorizontalMovement";
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.mirror = false;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.position = new Vector2(0f, 0f);
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.threshold = 0f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1.timeScale = 1f;
				var ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2 =  new ChildMotion();
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.motion = ledgeStrafeLeftAnimationClip37710;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.cycleOffset = 0f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.directBlendParameter = "HorizontalMovement";
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.mirror = false;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.position = new Vector2(0f, 0f);
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.threshold = 1f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2.timeScale = 2f;
				ledgeStrafeAnimatorState35168blendTreeBlendTree37706.children = new ChildMotion[] {
					ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child0,
					ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child1,
					ledgeStrafeAnimatorState35168blendTreeBlendTree37706Child2
				};
				ledgeStrafeAnimatorState35168.motion = ledgeStrafeAnimatorState35168blendTreeBlendTree37706;
				ledgeStrafeAnimatorState35168.cycleOffset = 0f;
				ledgeStrafeAnimatorState35168.cycleOffsetParameterActive = false;
				ledgeStrafeAnimatorState35168.iKOnFeet = true;
				ledgeStrafeAnimatorState35168.mirror = false;
				ledgeStrafeAnimatorState35168.mirrorParameterActive = false;
				ledgeStrafeAnimatorState35168.speed = 1f;
				ledgeStrafeAnimatorState35168.speedParameterActive = false;
				ledgeStrafeAnimatorState35168.writeDefaultValues = true;

				// State Machine Defaults.
				ledgeStrafeAnimatorStateMachine34268.anyStatePosition = new Vector3(50f, 20f, 0f);
				ledgeStrafeAnimatorStateMachine34268.defaultState = ledgeIdleAnimatorState35166;
				ledgeStrafeAnimatorStateMachine34268.entryPosition = new Vector3(50f, 120f, 0f);
				ledgeStrafeAnimatorStateMachine34268.exitPosition = new Vector3(800f, 120f, 0f);
				ledgeStrafeAnimatorStateMachine34268.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Transitions.
				var animatorStateTransition37696 = ledgeIdleAnimatorState35166.AddExitTransition();
				animatorStateTransition37696.canTransitionToSelf = true;
				animatorStateTransition37696.duration = 0.15f;
				animatorStateTransition37696.exitTime = 0.95f;
				animatorStateTransition37696.hasExitTime = false;
				animatorStateTransition37696.hasFixedDuration = true;
				animatorStateTransition37696.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37696.offset = 0f;
				animatorStateTransition37696.orderedInterruption = true;
				animatorStateTransition37696.isExit = true;
				animatorStateTransition37696.mute = false;
				animatorStateTransition37696.solo = false;
				animatorStateTransition37696.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				var animatorStateTransition37698 = ledgeIdleAnimatorState35166.AddTransition(ledgeStrafeAnimatorState35168);
				animatorStateTransition37698.canTransitionToSelf = true;
				animatorStateTransition37698.duration = 0.1f;
				animatorStateTransition37698.exitTime = 0.95f;
				animatorStateTransition37698.hasExitTime = false;
				animatorStateTransition37698.hasFixedDuration = true;
				animatorStateTransition37698.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37698.offset = 0.1f;
				animatorStateTransition37698.orderedInterruption = true;
				animatorStateTransition37698.isExit = false;
				animatorStateTransition37698.mute = false;
				animatorStateTransition37698.solo = false;
				animatorStateTransition37698.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition37698.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				var animatorStateTransition37702 = ledgeStrafeAnimatorState35168.AddExitTransition();
				animatorStateTransition37702.canTransitionToSelf = true;
				animatorStateTransition37702.duration = 0.15f;
				animatorStateTransition37702.exitTime = 0.95f;
				animatorStateTransition37702.hasExitTime = false;
				animatorStateTransition37702.hasFixedDuration = true;
				animatorStateTransition37702.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37702.offset = 0f;
				animatorStateTransition37702.orderedInterruption = true;
				animatorStateTransition37702.isExit = true;
				animatorStateTransition37702.mute = false;
				animatorStateTransition37702.solo = false;
				animatorStateTransition37702.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				var animatorStateTransition37704 = ledgeStrafeAnimatorState35168.AddTransition(ledgeIdleAnimatorState35166);
				animatorStateTransition37704.canTransitionToSelf = true;
				animatorStateTransition37704.duration = 0.25f;
				animatorStateTransition37704.exitTime = 0.91f;
				animatorStateTransition37704.hasExitTime = false;
				animatorStateTransition37704.hasFixedDuration = true;
				animatorStateTransition37704.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37704.offset = 0f;
				animatorStateTransition37704.orderedInterruption = true;
				animatorStateTransition37704.isExit = false;
				animatorStateTransition37704.mute = false;
				animatorStateTransition37704.solo = false;
				animatorStateTransition37704.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition37704.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				// State Machine Transitions.
				var animatorStateTransition34840 = baseStateMachine315408484.AddAnyStateTransition(ledgeIdleAnimatorState35166);
				animatorStateTransition34840.canTransitionToSelf = false;
				animatorStateTransition34840.duration = 0.15f;
				animatorStateTransition34840.exitTime = 0.75f;
				animatorStateTransition34840.hasExitTime = false;
				animatorStateTransition34840.hasFixedDuration = true;
				animatorStateTransition34840.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34840.offset = 0f;
				animatorStateTransition34840.orderedInterruption = true;
				animatorStateTransition34840.isExit = false;
				animatorStateTransition34840.mute = false;
				animatorStateTransition34840.solo = false;
				animatorStateTransition34840.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34840.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition34840.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition34842 = baseStateMachine315408484.AddAnyStateTransition(ledgeStrafeAnimatorState35168);
				animatorStateTransition34842.canTransitionToSelf = false;
				animatorStateTransition34842.duration = 0.15f;
				animatorStateTransition34842.exitTime = 0.75f;
				animatorStateTransition34842.hasExitTime = false;
				animatorStateTransition34842.hasFixedDuration = true;
				animatorStateTransition34842.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34842.offset = 0f;
				animatorStateTransition34842.orderedInterruption = true;
				animatorStateTransition34842.isExit = false;
				animatorStateTransition34842.mute = false;
				animatorStateTransition34842.solo = false;
				animatorStateTransition34842.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34842.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition34842.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				if (animatorControllers[i].layers.Length <= 5) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine484825858 = animatorControllers[i].layers[5].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine484825858.stateMachines.Length; ++k) {
						if (baseStateMachine484825858.stateMachines[k].stateMachine.name == "Ledge Strafe") {
							baseStateMachine484825858.RemoveStateMachine(baseStateMachine484825858.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var dualPistolIdleAnimationClip40866Path = AssetDatabase.GUIDToAssetPath("abd3b20228164e44b8fa1597ee6dfe31"); 
				var dualPistolIdleAnimationClip40866 = AnimatorBuilder.GetAnimationClip(dualPistolIdleAnimationClip40866Path, "DualPistolIdle");
				var shieldIdleAnimationClip40930Path = AssetDatabase.GUIDToAssetPath("17718399930c6ea4b9a734dbe54df24f"); 
				var shieldIdleAnimationClip40930 = AnimatorBuilder.GetAnimationClip(shieldIdleAnimationClip40930Path, "ShieldIdle");

				// State Machine.
				var ledgeStrafeAnimatorStateMachine40634 = baseStateMachine484825858.AddStateMachine("Ledge Strafe", new Vector3(852f, 12f, 0f));

				// States.
				var dualPistolAnimatorState40780 = ledgeStrafeAnimatorStateMachine40634.AddState("Dual Pistol", new Vector3(384f, 12f, 0f));
				dualPistolAnimatorState40780.motion = dualPistolIdleAnimationClip40866;
				dualPistolAnimatorState40780.cycleOffset = 0f;
				dualPistolAnimatorState40780.cycleOffsetParameterActive = false;
				dualPistolAnimatorState40780.iKOnFeet = false;
				dualPistolAnimatorState40780.mirror = false;
				dualPistolAnimatorState40780.mirrorParameterActive = false;
				dualPistolAnimatorState40780.speed = 1f;
				dualPistolAnimatorState40780.speedParameterActive = false;
				dualPistolAnimatorState40780.writeDefaultValues = true;

				var shieldAnimatorState40782 = ledgeStrafeAnimatorStateMachine40634.AddState("Shield", new Vector3(384f, 72f, 0f));
				shieldAnimatorState40782.motion = shieldIdleAnimationClip40930;
				shieldAnimatorState40782.cycleOffset = 0f;
				shieldAnimatorState40782.cycleOffsetParameterActive = false;
				shieldAnimatorState40782.iKOnFeet = false;
				shieldAnimatorState40782.mirror = false;
				shieldAnimatorState40782.mirrorParameterActive = false;
				shieldAnimatorState40782.speed = 1f;
				shieldAnimatorState40782.speedParameterActive = false;
				shieldAnimatorState40782.writeDefaultValues = true;

				// State Machine Defaults.
				ledgeStrafeAnimatorStateMachine40634.anyStatePosition = new Vector3(48f, 48f, 0f);
				ledgeStrafeAnimatorStateMachine40634.defaultState = dualPistolAnimatorState40780;
				ledgeStrafeAnimatorStateMachine40634.entryPosition = new Vector3(48f, 0f, 0f);
				ledgeStrafeAnimatorStateMachine40634.exitPosition = new Vector3(780f, 60f, 0f);
				ledgeStrafeAnimatorStateMachine40634.parentStateMachinePosition = new Vector3(756f, 0f, 0f);

				// State Transitions.
				var animatorStateTransition41000 = dualPistolAnimatorState40780.AddExitTransition();
				animatorStateTransition41000.canTransitionToSelf = true;
				animatorStateTransition41000.duration = 0.1f;
				animatorStateTransition41000.exitTime = 0f;
				animatorStateTransition41000.hasExitTime = false;
				animatorStateTransition41000.hasFixedDuration = true;
				animatorStateTransition41000.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41000.offset = 0f;
				animatorStateTransition41000.orderedInterruption = true;
				animatorStateTransition41000.isExit = true;
				animatorStateTransition41000.mute = false;
				animatorStateTransition41000.solo = false;
				animatorStateTransition41000.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				var animatorStateTransition41002 = shieldAnimatorState40782.AddExitTransition();
				animatorStateTransition41002.canTransitionToSelf = true;
				animatorStateTransition41002.duration = 0.1f;
				animatorStateTransition41002.exitTime = 0f;
				animatorStateTransition41002.hasExitTime = false;
				animatorStateTransition41002.hasFixedDuration = true;
				animatorStateTransition41002.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41002.offset = 0f;
				animatorStateTransition41002.orderedInterruption = true;
				animatorStateTransition41002.isExit = true;
				animatorStateTransition41002.mute = false;
				animatorStateTransition41002.solo = false;
				animatorStateTransition41002.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition40700 = baseStateMachine484825858.AddAnyStateTransition(dualPistolAnimatorState40780);
				animatorStateTransition40700.canTransitionToSelf = false;
				animatorStateTransition40700.duration = 0.15f;
				animatorStateTransition40700.exitTime = 0.75f;
				animatorStateTransition40700.hasExitTime = false;
				animatorStateTransition40700.hasFixedDuration = true;
				animatorStateTransition40700.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40700.offset = 0f;
				animatorStateTransition40700.orderedInterruption = true;
				animatorStateTransition40700.isExit = false;
				animatorStateTransition40700.mute = false;
				animatorStateTransition40700.solo = false;
				animatorStateTransition40700.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition40700.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot1ItemID");

				var animatorStateTransition40702 = baseStateMachine484825858.AddAnyStateTransition(shieldAnimatorState40782);
				animatorStateTransition40702.canTransitionToSelf = false;
				animatorStateTransition40702.duration = 0.15f;
				animatorStateTransition40702.exitTime = 0.75f;
				animatorStateTransition40702.hasExitTime = false;
				animatorStateTransition40702.hasFixedDuration = true;
				animatorStateTransition40702.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40702.offset = 0f;
				animatorStateTransition40702.orderedInterruption = true;
				animatorStateTransition40702.isExit = false;
				animatorStateTransition40702.mute = false;
				animatorStateTransition40702.solo = false;
				animatorStateTransition40702.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition40702.AddCondition(AnimatorConditionMode.Equals, 25f, "Slot0ItemID");

				if (animatorControllers[i].layers.Length <= 6) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine484828608 = animatorControllers[i].layers[6].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine484828608.stateMachines.Length; ++k) {
						if (baseStateMachine484828608.stateMachines[k].stateMachine.name == "Ledge Strafe") {
							baseStateMachine484828608.RemoveStateMachine(baseStateMachine484828608.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var swordIdleMovementAnimationClip41320Path = AssetDatabase.GUIDToAssetPath("7491132715cdf964889b5fc2729270e1"); 
				var swordIdleMovementAnimationClip41320 = AnimatorBuilder.GetAnimationClip(swordIdleMovementAnimationClip41320Path, "SwordIdleMovement");
				var knifeIdleMovementAnimationClip41422Path = AssetDatabase.GUIDToAssetPath("fa9162997c077584ebabf8919a1c1518"); 
				var knifeIdleMovementAnimationClip41422 = AnimatorBuilder.GetAnimationClip(knifeIdleMovementAnimationClip41422Path, "KnifeIdleMovement");

				// State Machine.
				var ledgeStrafeAnimatorStateMachine41080 = baseStateMachine484828608.AddStateMachine("Ledge Strafe", new Vector3(852f, 12f, 0f));

				// States.
				var swordAnimatorState41266 = ledgeStrafeAnimatorStateMachine41080.AddState("Sword", new Vector3(384f, 84f, 0f));
				swordAnimatorState41266.motion = swordIdleMovementAnimationClip41320;
				swordAnimatorState41266.cycleOffset = 0f;
				swordAnimatorState41266.cycleOffsetParameterActive = false;
				swordAnimatorState41266.iKOnFeet = false;
				swordAnimatorState41266.mirror = false;
				swordAnimatorState41266.mirrorParameterActive = false;
				swordAnimatorState41266.speed = 1f;
				swordAnimatorState41266.speedParameterActive = false;
				swordAnimatorState41266.writeDefaultValues = true;

				var knifeAnimatorState41264 = ledgeStrafeAnimatorStateMachine41080.AddState("Knife", new Vector3(384f, 24f, 0f));
				knifeAnimatorState41264.motion = knifeIdleMovementAnimationClip41422;
				knifeAnimatorState41264.cycleOffset = 0f;
				knifeAnimatorState41264.cycleOffsetParameterActive = false;
				knifeAnimatorState41264.iKOnFeet = false;
				knifeAnimatorState41264.mirror = false;
				knifeAnimatorState41264.mirrorParameterActive = false;
				knifeAnimatorState41264.speed = 1f;
				knifeAnimatorState41264.speedParameterActive = false;
				knifeAnimatorState41264.writeDefaultValues = true;

				var dualPistolAnimatorState41268 = ledgeStrafeAnimatorStateMachine41080.AddState("Dual Pistol", new Vector3(384f, -36f, 0f));
				dualPistolAnimatorState41268.motion = dualPistolIdleAnimationClip40866;
				dualPistolAnimatorState41268.cycleOffset = 0f;
				dualPistolAnimatorState41268.cycleOffsetParameterActive = false;
				dualPistolAnimatorState41268.iKOnFeet = false;
				dualPistolAnimatorState41268.mirror = false;
				dualPistolAnimatorState41268.mirrorParameterActive = false;
				dualPistolAnimatorState41268.speed = 1f;
				dualPistolAnimatorState41268.speedParameterActive = false;
				dualPistolAnimatorState41268.writeDefaultValues = true;

				// State Machine Defaults.
				ledgeStrafeAnimatorStateMachine41080.anyStatePosition = new Vector3(50f, 20f, 0f);
				ledgeStrafeAnimatorStateMachine41080.defaultState = swordAnimatorState41266;
				ledgeStrafeAnimatorStateMachine41080.entryPosition = new Vector3(48f, -24f, 0f);
				ledgeStrafeAnimatorStateMachine41080.exitPosition = new Vector3(780f, 24f, 0f);
				ledgeStrafeAnimatorStateMachine41080.parentStateMachinePosition = new Vector3(756f, -48f, 0f);

				// State Transitions.
				var animatorStateTransition41476 = swordAnimatorState41266.AddExitTransition();
				animatorStateTransition41476.canTransitionToSelf = true;
				animatorStateTransition41476.duration = 0.1f;
				animatorStateTransition41476.exitTime = 0f;
				animatorStateTransition41476.hasExitTime = false;
				animatorStateTransition41476.hasFixedDuration = true;
				animatorStateTransition41476.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41476.offset = 0f;
				animatorStateTransition41476.orderedInterruption = false;
				animatorStateTransition41476.isExit = true;
				animatorStateTransition41476.mute = false;
				animatorStateTransition41476.solo = false;
				animatorStateTransition41476.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				var animatorStateTransition41478 = knifeAnimatorState41264.AddExitTransition();
				animatorStateTransition41478.canTransitionToSelf = true;
				animatorStateTransition41478.duration = 0.1f;
				animatorStateTransition41478.exitTime = 0f;
				animatorStateTransition41478.hasExitTime = false;
				animatorStateTransition41478.hasFixedDuration = true;
				animatorStateTransition41478.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41478.offset = 0f;
				animatorStateTransition41478.orderedInterruption = true;
				animatorStateTransition41478.isExit = true;
				animatorStateTransition41478.mute = false;
				animatorStateTransition41478.solo = false;
				animatorStateTransition41478.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				var animatorStateTransition41480 = dualPistolAnimatorState41268.AddExitTransition();
				animatorStateTransition41480.canTransitionToSelf = true;
				animatorStateTransition41480.duration = 0.1f;
				animatorStateTransition41480.exitTime = 0f;
				animatorStateTransition41480.hasExitTime = false;
				animatorStateTransition41480.hasFixedDuration = true;
				animatorStateTransition41480.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41480.offset = 0f;
				animatorStateTransition41480.orderedInterruption = true;
				animatorStateTransition41480.isExit = true;
				animatorStateTransition41480.mute = false;
				animatorStateTransition41480.solo = false;
				animatorStateTransition41480.AddCondition(AnimatorConditionMode.NotEqual, 106f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition41152 = baseStateMachine484828608.AddAnyStateTransition(knifeAnimatorState41264);
				animatorStateTransition41152.canTransitionToSelf = false;
				animatorStateTransition41152.duration = 0.15f;
				animatorStateTransition41152.exitTime = 0.75f;
				animatorStateTransition41152.hasExitTime = false;
				animatorStateTransition41152.hasFixedDuration = true;
				animatorStateTransition41152.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41152.offset = 0f;
				animatorStateTransition41152.orderedInterruption = true;
				animatorStateTransition41152.isExit = false;
				animatorStateTransition41152.mute = false;
				animatorStateTransition41152.solo = false;
				animatorStateTransition41152.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition41152.AddCondition(AnimatorConditionMode.Equals, 23f, "Slot0ItemID");

				var animatorStateTransition41154 = baseStateMachine484828608.AddAnyStateTransition(swordAnimatorState41266);
				animatorStateTransition41154.canTransitionToSelf = false;
				animatorStateTransition41154.duration = 0.15f;
				animatorStateTransition41154.exitTime = 0.75f;
				animatorStateTransition41154.hasExitTime = false;
				animatorStateTransition41154.hasFixedDuration = true;
				animatorStateTransition41154.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41154.offset = 0f;
				animatorStateTransition41154.orderedInterruption = true;
				animatorStateTransition41154.isExit = false;
				animatorStateTransition41154.mute = false;
				animatorStateTransition41154.solo = false;
				animatorStateTransition41154.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition41154.AddCondition(AnimatorConditionMode.Equals, 22f, "Slot0ItemID");

				var animatorStateTransition41156 = baseStateMachine484828608.AddAnyStateTransition(dualPistolAnimatorState41268);
				animatorStateTransition41156.canTransitionToSelf = false;
				animatorStateTransition41156.duration = 0.15f;
				animatorStateTransition41156.exitTime = 0.75f;
				animatorStateTransition41156.hasExitTime = false;
				animatorStateTransition41156.hasFixedDuration = true;
				animatorStateTransition41156.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41156.offset = 0f;
				animatorStateTransition41156.orderedInterruption = true;
				animatorStateTransition41156.isExit = false;
				animatorStateTransition41156.mute = false;
				animatorStateTransition41156.solo = false;
				animatorStateTransition41156.AddCondition(AnimatorConditionMode.Equals, 106f, "AbilityIndex");
				animatorStateTransition41156.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
			}
		}
	}
}
