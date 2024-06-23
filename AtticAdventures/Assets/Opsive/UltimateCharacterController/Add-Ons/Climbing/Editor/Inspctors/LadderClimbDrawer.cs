
namespace Opsive.UltimateCharacterController.Editor.Inspectors.Character.Abilities
{
	using Opsive.Shared.Editor.UIElements.Controls;
	using Opsive.UltimateCharacterController.Editor.Controls.Types.AbilityDrawers;
	using Opsive.UltimateCharacterController.Editor.Utility;
	using UnityEditor;
	using UnityEditor.Animations;
	using UnityEngine;

	/// <summary>
	/// Draws a custom inspector for the LadderClimb Ability.
	/// </summary>
	[ControlType(typeof(Opsive.UltimateCharacterController.AddOns.Climbing.LadderClimb))]
	public class LadderClimbDrawer : DetectObjectAbilityBaseDrawer
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

				var baseStateMachine207580318 = animatorControllers[i].layers[12].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine207580318.stateMachines.Length; ++k) {
						if (baseStateMachine207580318.stateMachines[k].stateMachine.name == "Ladder Climb") {
							baseStateMachine207580318.RemoveStateMachine(baseStateMachine207580318.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var climbLadderTopDismountRightAnimationClip33698Path = AssetDatabase.GUIDToAssetPath("8020ee5e313d0c54a96a32402cf7ae46"); 
				var climbLadderTopDismountRightAnimationClip33698 = AnimatorBuilder.GetAnimationClip(climbLadderTopDismountRightAnimationClip33698Path, "ClimbLadderTopDismountRight");
				var climbLadderBottomMountAnimationClip33706Path = AssetDatabase.GUIDToAssetPath("1ee2a6fa93329904f8841315613890a8"); 
				var climbLadderBottomMountAnimationClip33706 = AnimatorBuilder.GetAnimationClip(climbLadderBottomMountAnimationClip33706Path, "ClimbLadderBottomMount");
				var climbLadderBottomDismountLeftAnimationClip33712Path = AssetDatabase.GUIDToAssetPath("25470ca977145354d91c2d4904bd434b"); 
				var climbLadderBottomDismountLeftAnimationClip33712 = AnimatorBuilder.GetAnimationClip(climbLadderBottomDismountLeftAnimationClip33712Path, "ClimbLadderBottomDismountLeft");
				var climbLadderTopDismountLeftAnimationClip33716Path = AssetDatabase.GUIDToAssetPath("c178f786b45ce024581dba11faff2e93"); 
				var climbLadderTopDismountLeftAnimationClip33716 = AnimatorBuilder.GetAnimationClip(climbLadderTopDismountLeftAnimationClip33716Path, "ClimbLadderTopDismountLeft");
				var climbLadderTopMountAnimationClip33724Path = AssetDatabase.GUIDToAssetPath("8901ddb7fcee9e747b0d35d6e830b768"); 
				var climbLadderTopMountAnimationClip33724 = AnimatorBuilder.GetAnimationClip(climbLadderTopMountAnimationClip33724Path, "ClimbLadderTopMount");
				var climbLadderBottomDismountRightAnimationClip33728Path = AssetDatabase.GUIDToAssetPath("eac22f07651e97c43acb1a41461da263"); 
				var climbLadderBottomDismountRightAnimationClip33728 = AnimatorBuilder.GetAnimationClip(climbLadderBottomDismountRightAnimationClip33728Path, "ClimbLadderBottomDismountRight");
				var climbLadderLeftUpAnimationClip33748Path = AssetDatabase.GUIDToAssetPath("88298cd410899c945952b24872b24320"); 
				var climbLadderLeftUpAnimationClip33748 = AnimatorBuilder.GetAnimationClip(climbLadderLeftUpAnimationClip33748Path, "ClimbLadderLeftUp");
				var climbLadderRightUpAnimationClip33760Path = AssetDatabase.GUIDToAssetPath("88298cd410899c945952b24872b24320"); 
				var climbLadderRightUpAnimationClip33760 = AnimatorBuilder.GetAnimationClip(climbLadderRightUpAnimationClip33760Path, "ClimbLadderRightUp");
				var climbLadderLeftDownAnimationClip33772Path = AssetDatabase.GUIDToAssetPath("88298cd410899c945952b24872b24320"); 
				var climbLadderLeftDownAnimationClip33772 = AnimatorBuilder.GetAnimationClip(climbLadderLeftDownAnimationClip33772Path, "ClimbLadderLeftDown");
				var climbLadderRightDownAnimationClip33784Path = AssetDatabase.GUIDToAssetPath("88298cd410899c945952b24872b24320"); 
				var climbLadderRightDownAnimationClip33784 = AnimatorBuilder.GetAnimationClip(climbLadderRightDownAnimationClip33784Path, "ClimbLadderRightDown");
				var climbLadderIdleLeftAnimationClip33796Path = AssetDatabase.GUIDToAssetPath("35f6345e0926afc4494f8225ad65b06a"); 
				var climbLadderIdleLeftAnimationClip33796 = AnimatorBuilder.GetAnimationClip(climbLadderIdleLeftAnimationClip33796Path, "ClimbLadderIdleLeft");
				var climbLadderIdleRightAnimationClip33808Path = AssetDatabase.GUIDToAssetPath("35f6345e0926afc4494f8225ad65b06a"); 
				var climbLadderIdleRightAnimationClip33808 = AnimatorBuilder.GetAnimationClip(climbLadderIdleRightAnimationClip33808Path, "ClimbLadderIdleRight");
				var hangtoLadderClimbLeftAnimationClip33814Path = AssetDatabase.GUIDToAssetPath("8193daa5e5a44654285c8c3eddf1fed3"); 
				var hangtoLadderClimbLeftAnimationClip33814 = AnimatorBuilder.GetAnimationClip(hangtoLadderClimbLeftAnimationClip33814Path, "HangtoLadderClimbLeft");
				var hangtoLadderClimbRightAnimationClip33820Path = AssetDatabase.GUIDToAssetPath("8193daa5e5a44654285c8c3eddf1fed3"); 
				var hangtoLadderClimbRightAnimationClip33820 = AnimatorBuilder.GetAnimationClip(hangtoLadderClimbRightAnimationClip33820Path, "HangtoLadderClimbRight");

				// State Machine.
				var ladderClimbAnimatorStateMachine31910 = baseStateMachine207580318.AddStateMachine("Ladder Climb", new Vector3(650f, 380f, 0f));

				// States.
				var topDismountRightAnimatorState33682 = ladderClimbAnimatorStateMachine31910.AddState("Top Dismount Right", new Vector3(730f, -160f, 0f));
				topDismountRightAnimatorState33682.motion = climbLadderTopDismountRightAnimationClip33698;
				topDismountRightAnimatorState33682.cycleOffset = 0f;
				topDismountRightAnimatorState33682.cycleOffsetParameterActive = false;
				topDismountRightAnimatorState33682.iKOnFeet = false;
				topDismountRightAnimatorState33682.mirror = false;
				topDismountRightAnimatorState33682.mirrorParameterActive = false;
				topDismountRightAnimatorState33682.speed = 1f;
				topDismountRightAnimatorState33682.speedParameterActive = false;
				topDismountRightAnimatorState33682.writeDefaultValues = true;

				var bottomMountAnimatorState32292 = ladderClimbAnimatorStateMachine31910.AddState("Bottom Mount", new Vector3(390f, 220f, 0f));
				bottomMountAnimatorState32292.motion = climbLadderBottomMountAnimationClip33706;
				bottomMountAnimatorState32292.cycleOffset = 0f;
				bottomMountAnimatorState32292.cycleOffsetParameterActive = false;
				bottomMountAnimatorState32292.iKOnFeet = true;
				bottomMountAnimatorState32292.mirror = false;
				bottomMountAnimatorState32292.mirrorParameterActive = false;
				bottomMountAnimatorState32292.speed = 1f;
				bottomMountAnimatorState32292.speedParameterActive = false;
				bottomMountAnimatorState32292.writeDefaultValues = true;

				var bottomDismountRightAnimatorState33684 = ladderClimbAnimatorStateMachine31910.AddState("Bottom Dismount Right", new Vector3(730f, 280f, 0f));
				bottomDismountRightAnimatorState33684.motion = climbLadderBottomDismountLeftAnimationClip33712;
				bottomDismountRightAnimatorState33684.cycleOffset = 0f;
				bottomDismountRightAnimatorState33684.cycleOffsetParameterActive = false;
				bottomDismountRightAnimatorState33684.iKOnFeet = true;
				bottomDismountRightAnimatorState33684.mirror = false;
				bottomDismountRightAnimatorState33684.mirrorParameterActive = false;
				bottomDismountRightAnimatorState33684.speed = 1f;
				bottomDismountRightAnimatorState33684.speedParameterActive = false;
				bottomDismountRightAnimatorState33684.writeDefaultValues = true;

				var topDismountLeftAnimatorState33686 = ladderClimbAnimatorStateMachine31910.AddState("Top Dismount Left", new Vector3(730f, -100f, 0f));
				topDismountLeftAnimatorState33686.motion = climbLadderTopDismountLeftAnimationClip33716;
				topDismountLeftAnimatorState33686.cycleOffset = 0f;
				topDismountLeftAnimatorState33686.cycleOffsetParameterActive = false;
				topDismountLeftAnimatorState33686.iKOnFeet = false;
				topDismountLeftAnimatorState33686.mirror = false;
				topDismountLeftAnimatorState33686.mirrorParameterActive = false;
				topDismountLeftAnimatorState33686.speed = 1f;
				topDismountLeftAnimatorState33686.speedParameterActive = false;
				topDismountLeftAnimatorState33686.writeDefaultValues = true;

				var topMountAnimatorState32290 = ladderClimbAnimatorStateMachine31910.AddState("Top Mount", new Vector3(390f, -90f, 0f));
				topMountAnimatorState32290.motion = climbLadderTopMountAnimationClip33724;
				topMountAnimatorState32290.cycleOffset = 0f;
				topMountAnimatorState32290.cycleOffsetParameterActive = false;
				topMountAnimatorState32290.iKOnFeet = true;
				topMountAnimatorState32290.mirror = false;
				topMountAnimatorState32290.mirrorParameterActive = false;
				topMountAnimatorState32290.speed = 1f;
				topMountAnimatorState32290.speedParameterActive = false;
				topMountAnimatorState32290.writeDefaultValues = true;

				var bottomDismountLeftAnimatorState33688 = ladderClimbAnimatorStateMachine31910.AddState("Bottom Dismount Left", new Vector3(730f, 210f, 0f));
				bottomDismountLeftAnimatorState33688.motion = climbLadderBottomDismountRightAnimationClip33728;
				bottomDismountLeftAnimatorState33688.cycleOffset = 0f;
				bottomDismountLeftAnimatorState33688.cycleOffsetParameterActive = false;
				bottomDismountLeftAnimatorState33688.iKOnFeet = true;
				bottomDismountLeftAnimatorState33688.mirror = false;
				bottomDismountLeftAnimatorState33688.mirrorParameterActive = false;
				bottomDismountLeftAnimatorState33688.speed = 1f;
				bottomDismountLeftAnimatorState33688.speedParameterActive = false;
				bottomDismountLeftAnimatorState33688.writeDefaultValues = true;

				// State Machine.
				var climbAnimatorStateMachine33690 = ladderClimbAnimatorStateMachine31910.AddStateMachine("Climb", new Vector3(570f, 60f, 0f));

				// States.
				var climbLeftUpAnimatorState33730 = climbAnimatorStateMachine33690.AddState("Climb Left Up", new Vector3(290f, -180f, 0f));
				climbLeftUpAnimatorState33730.motion = climbLadderLeftUpAnimationClip33748;
				climbLeftUpAnimatorState33730.cycleOffset = 0f;
				climbLeftUpAnimatorState33730.cycleOffsetParameterActive = false;
				climbLeftUpAnimatorState33730.iKOnFeet = true;
				climbLeftUpAnimatorState33730.mirror = false;
				climbLeftUpAnimatorState33730.mirrorParameterActive = false;
				climbLeftUpAnimatorState33730.speed = 1f;
				climbLeftUpAnimatorState33730.speedParameterActive = false;
				climbLeftUpAnimatorState33730.writeDefaultValues = true;

				var climbRightUpAnimatorState33732 = climbAnimatorStateMachine33690.AddState("Climb Right Up", new Vector3(610f, -180f, 0f));
				climbRightUpAnimatorState33732.motion = climbLadderRightUpAnimationClip33760;
				climbRightUpAnimatorState33732.cycleOffset = 0f;
				climbRightUpAnimatorState33732.cycleOffsetParameterActive = false;
				climbRightUpAnimatorState33732.iKOnFeet = true;
				climbRightUpAnimatorState33732.mirror = false;
				climbRightUpAnimatorState33732.mirrorParameterActive = false;
				climbRightUpAnimatorState33732.speed = 1f;
				climbRightUpAnimatorState33732.speedParameterActive = false;
				climbRightUpAnimatorState33732.writeDefaultValues = true;

				var climbLeftDownAnimatorState33734 = climbAnimatorStateMachine33690.AddState("Climb Left Down", new Vector3(290f, 140f, 0f));
				climbLeftDownAnimatorState33734.motion = climbLadderLeftDownAnimationClip33772;
				climbLeftDownAnimatorState33734.cycleOffset = 0f;
				climbLeftDownAnimatorState33734.cycleOffsetParameterActive = false;
				climbLeftDownAnimatorState33734.iKOnFeet = true;
				climbLeftDownAnimatorState33734.mirror = false;
				climbLeftDownAnimatorState33734.mirrorParameterActive = false;
				climbLeftDownAnimatorState33734.speed = 1f;
				climbLeftDownAnimatorState33734.speedParameterActive = false;
				climbLeftDownAnimatorState33734.writeDefaultValues = true;

				var climbRightDownAnimatorState33736 = climbAnimatorStateMachine33690.AddState("Climb Right Down", new Vector3(610f, 140f, 0f));
				climbRightDownAnimatorState33736.motion = climbLadderRightDownAnimationClip33784;
				climbRightDownAnimatorState33736.cycleOffset = 0f;
				climbRightDownAnimatorState33736.cycleOffsetParameterActive = false;
				climbRightDownAnimatorState33736.iKOnFeet = true;
				climbRightDownAnimatorState33736.mirror = false;
				climbRightDownAnimatorState33736.mirrorParameterActive = false;
				climbRightDownAnimatorState33736.speed = 1f;
				climbRightDownAnimatorState33736.speedParameterActive = false;
				climbRightDownAnimatorState33736.writeDefaultValues = true;

				var climbLeftIdleAnimatorState33708 = climbAnimatorStateMachine33690.AddState("Climb Left Idle", new Vector3(310f, -20f, 0f));
				climbLeftIdleAnimatorState33708.motion = climbLadderIdleLeftAnimationClip33796;
				climbLeftIdleAnimatorState33708.cycleOffset = 0f;
				climbLeftIdleAnimatorState33708.cycleOffsetParameterActive = false;
				climbLeftIdleAnimatorState33708.iKOnFeet = true;
				climbLeftIdleAnimatorState33708.mirror = false;
				climbLeftIdleAnimatorState33708.mirrorParameterActive = false;
				climbLeftIdleAnimatorState33708.speed = 1f;
				climbLeftIdleAnimatorState33708.speedParameterActive = false;
				climbLeftIdleAnimatorState33708.writeDefaultValues = true;

				var climbRightIdleAnimatorState32294 = climbAnimatorStateMachine33690.AddState("Climb Right Idle", new Vector3(530f, -20f, 0f));
				climbRightIdleAnimatorState32294.motion = climbLadderIdleRightAnimationClip33808;
				climbRightIdleAnimatorState32294.cycleOffset = 0f;
				climbRightIdleAnimatorState32294.cycleOffsetParameterActive = false;
				climbRightIdleAnimatorState32294.iKOnFeet = true;
				climbRightIdleAnimatorState32294.mirror = false;
				climbRightIdleAnimatorState32294.mirrorParameterActive = false;
				climbRightIdleAnimatorState32294.speed = 1f;
				climbRightIdleAnimatorState32294.speedParameterActive = false;
				climbRightIdleAnimatorState32294.writeDefaultValues = true;

				// State Machine Defaults.
				climbAnimatorStateMachine33690.anyStatePosition = new Vector3(-80f, -80f, 0f);
				climbAnimatorStateMachine33690.defaultState = climbLeftUpAnimatorState33730;
				climbAnimatorStateMachine33690.entryPosition = new Vector3(-80f, -40f, 0f);
				climbAnimatorStateMachine33690.exitPosition = new Vector3(1080f, 20f, 0f);
				climbAnimatorStateMachine33690.parentStateMachinePosition = new Vector3(1060f, -50f, 0f);

				// State Machine.
				var hangtoLadderClimbAnimatorStateMachine33692 = ladderClimbAnimatorStateMachine31910.AddStateMachine("Hang to Ladder Climb", new Vector3(1014.525f, 318.5602f, 0f));

				// States.
				var hangtoLadderClimbLeftAnimatorState32324 = hangtoLadderClimbAnimatorStateMachine33692.AddState("Hang to Ladder Climb Left", new Vector3(410f, -10f, 0f));
				hangtoLadderClimbLeftAnimatorState32324.motion = hangtoLadderClimbLeftAnimationClip33814;
				hangtoLadderClimbLeftAnimatorState32324.cycleOffset = 0f;
				hangtoLadderClimbLeftAnimatorState32324.cycleOffsetParameterActive = false;
				hangtoLadderClimbLeftAnimatorState32324.iKOnFeet = false;
				hangtoLadderClimbLeftAnimatorState32324.mirror = false;
				hangtoLadderClimbLeftAnimatorState32324.mirrorParameterActive = false;
				hangtoLadderClimbLeftAnimatorState32324.speed = 1f;
				hangtoLadderClimbLeftAnimatorState32324.speedParameterActive = false;
				hangtoLadderClimbLeftAnimatorState32324.writeDefaultValues = true;

				var hangtoLadderClimbRightAnimatorState32326 = hangtoLadderClimbAnimatorStateMachine33692.AddState("Hang to Ladder Climb Right", new Vector3(410f, 60f, 0f));
				hangtoLadderClimbRightAnimatorState32326.motion = hangtoLadderClimbRightAnimationClip33820;
				hangtoLadderClimbRightAnimatorState32326.cycleOffset = 0f;
				hangtoLadderClimbRightAnimatorState32326.cycleOffsetParameterActive = false;
				hangtoLadderClimbRightAnimatorState32326.iKOnFeet = false;
				hangtoLadderClimbRightAnimatorState32326.mirror = false;
				hangtoLadderClimbRightAnimatorState32326.mirrorParameterActive = false;
				hangtoLadderClimbRightAnimatorState32326.speed = 1f;
				hangtoLadderClimbRightAnimatorState32326.speedParameterActive = false;
				hangtoLadderClimbRightAnimatorState32326.writeDefaultValues = true;

				// State Machine Defaults.
				hangtoLadderClimbAnimatorStateMachine33692.anyStatePosition = new Vector3(50f, 20f, 0f);
				hangtoLadderClimbAnimatorStateMachine33692.defaultState = hangtoLadderClimbLeftAnimatorState32324;
				hangtoLadderClimbAnimatorStateMachine33692.entryPosition = new Vector3(50f, 120f, 0f);
				hangtoLadderClimbAnimatorStateMachine33692.exitPosition = new Vector3(800f, 120f, 0f);
				hangtoLadderClimbAnimatorStateMachine33692.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Machine Defaults.
				ladderClimbAnimatorStateMachine31910.anyStatePosition = new Vector3(50f, 20f, 0f);
				ladderClimbAnimatorStateMachine31910.defaultState = bottomMountAnimatorState32292;
				ladderClimbAnimatorStateMachine31910.entryPosition = new Vector3(50f, 120f, 0f);
				ladderClimbAnimatorStateMachine31910.exitPosition = new Vector3(1120f, 110f, 0f);
				ladderClimbAnimatorStateMachine31910.parentStateMachinePosition = new Vector3(1120f, 10f, 0f);

				// State Transitions.
				var animatorStateTransition33696 = topDismountRightAnimatorState33682.AddExitTransition();
				animatorStateTransition33696.canTransitionToSelf = true;
				animatorStateTransition33696.duration = 0.25f;
				animatorStateTransition33696.exitTime = 0.75f;
				animatorStateTransition33696.hasExitTime = false;
				animatorStateTransition33696.hasFixedDuration = true;
				animatorStateTransition33696.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33696.offset = 0f;
				animatorStateTransition33696.orderedInterruption = true;
				animatorStateTransition33696.isExit = true;
				animatorStateTransition33696.mute = false;
				animatorStateTransition33696.solo = false;
				animatorStateTransition33696.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33700 = bottomMountAnimatorState32292.AddTransition(climbLeftIdleAnimatorState33708);
				animatorStateTransition33700.canTransitionToSelf = true;
				animatorStateTransition33700.duration = 0f;
				animatorStateTransition33700.exitTime = 1f;
				animatorStateTransition33700.hasExitTime = false;
				animatorStateTransition33700.hasFixedDuration = true;
				animatorStateTransition33700.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33700.offset = 0f;
				animatorStateTransition33700.orderedInterruption = true;
				animatorStateTransition33700.isExit = false;
				animatorStateTransition33700.mute = false;
				animatorStateTransition33700.solo = false;
				animatorStateTransition33700.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33700.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33702 = bottomMountAnimatorState32292.AddTransition(bottomDismountLeftAnimatorState33688);
				animatorStateTransition33702.canTransitionToSelf = true;
				animatorStateTransition33702.duration = 0f;
				animatorStateTransition33702.exitTime = 1.05f;
				animatorStateTransition33702.hasExitTime = true;
				animatorStateTransition33702.hasFixedDuration = true;
				animatorStateTransition33702.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33702.offset = 0f;
				animatorStateTransition33702.orderedInterruption = true;
				animatorStateTransition33702.isExit = false;
				animatorStateTransition33702.mute = false;
				animatorStateTransition33702.solo = false;
				animatorStateTransition33702.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33702.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");

				var animatorStateTransition33704 = bottomMountAnimatorState32292.AddExitTransition();
				animatorStateTransition33704.canTransitionToSelf = true;
				animatorStateTransition33704.duration = 0f;
				animatorStateTransition33704.exitTime = 1.05f;
				animatorStateTransition33704.hasExitTime = true;
				animatorStateTransition33704.hasFixedDuration = true;
				animatorStateTransition33704.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33704.offset = 0f;
				animatorStateTransition33704.orderedInterruption = true;
				animatorStateTransition33704.isExit = true;
				animatorStateTransition33704.mute = false;
				animatorStateTransition33704.solo = false;
				animatorStateTransition33704.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33710 = bottomDismountRightAnimatorState33684.AddExitTransition();
				animatorStateTransition33710.canTransitionToSelf = true;
				animatorStateTransition33710.duration = 0.1f;
				animatorStateTransition33710.exitTime = 1.1f;
				animatorStateTransition33710.hasExitTime = false;
				animatorStateTransition33710.hasFixedDuration = true;
				animatorStateTransition33710.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33710.offset = 0f;
				animatorStateTransition33710.orderedInterruption = true;
				animatorStateTransition33710.isExit = true;
				animatorStateTransition33710.mute = false;
				animatorStateTransition33710.solo = false;
				animatorStateTransition33710.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33714 = topDismountLeftAnimatorState33686.AddExitTransition();
				animatorStateTransition33714.canTransitionToSelf = true;
				animatorStateTransition33714.duration = 0.25f;
				animatorStateTransition33714.exitTime = 0.75f;
				animatorStateTransition33714.hasExitTime = false;
				animatorStateTransition33714.hasFixedDuration = true;
				animatorStateTransition33714.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33714.offset = 0f;
				animatorStateTransition33714.orderedInterruption = true;
				animatorStateTransition33714.isExit = true;
				animatorStateTransition33714.mute = false;
				animatorStateTransition33714.solo = false;
				animatorStateTransition33714.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33718 = topMountAnimatorState32290.AddTransition(climbLeftIdleAnimatorState33708);
				animatorStateTransition33718.canTransitionToSelf = true;
				animatorStateTransition33718.duration = 0f;
				animatorStateTransition33718.exitTime = 1f;
				animatorStateTransition33718.hasExitTime = false;
				animatorStateTransition33718.hasFixedDuration = true;
				animatorStateTransition33718.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33718.offset = 0f;
				animatorStateTransition33718.orderedInterruption = true;
				animatorStateTransition33718.isExit = false;
				animatorStateTransition33718.mute = false;
				animatorStateTransition33718.solo = false;
				animatorStateTransition33718.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33718.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33720 = topMountAnimatorState32290.AddTransition(topDismountLeftAnimatorState33686);
				animatorStateTransition33720.canTransitionToSelf = true;
				animatorStateTransition33720.duration = 0f;
				animatorStateTransition33720.exitTime = 1.05f;
				animatorStateTransition33720.hasExitTime = true;
				animatorStateTransition33720.hasFixedDuration = true;
				animatorStateTransition33720.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33720.offset = 0f;
				animatorStateTransition33720.orderedInterruption = true;
				animatorStateTransition33720.isExit = false;
				animatorStateTransition33720.mute = false;
				animatorStateTransition33720.solo = false;
				animatorStateTransition33720.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33720.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition33722 = topMountAnimatorState32290.AddExitTransition();
				animatorStateTransition33722.canTransitionToSelf = true;
				animatorStateTransition33722.duration = 0f;
				animatorStateTransition33722.exitTime = 1.05f;
				animatorStateTransition33722.hasExitTime = true;
				animatorStateTransition33722.hasFixedDuration = true;
				animatorStateTransition33722.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33722.offset = 0f;
				animatorStateTransition33722.orderedInterruption = true;
				animatorStateTransition33722.isExit = true;
				animatorStateTransition33722.mute = false;
				animatorStateTransition33722.solo = false;
				animatorStateTransition33722.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33726 = bottomDismountLeftAnimatorState33688.AddExitTransition();
				animatorStateTransition33726.canTransitionToSelf = true;
				animatorStateTransition33726.duration = 0.1f;
				animatorStateTransition33726.exitTime = 1.1f;
				animatorStateTransition33726.hasExitTime = false;
				animatorStateTransition33726.hasFixedDuration = true;
				animatorStateTransition33726.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33726.offset = 0f;
				animatorStateTransition33726.orderedInterruption = true;
				animatorStateTransition33726.isExit = true;
				animatorStateTransition33726.mute = false;
				animatorStateTransition33726.solo = false;
				animatorStateTransition33726.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				// StateMachine Transitions.
				var animatorTransition33694 = ladderClimbAnimatorStateMachine31910.AddStateMachineTransition(climbAnimatorStateMachine33690);
				animatorTransition33694.isExit = true;
				animatorTransition33694.mute = false;
				animatorTransition33694.solo = false;
				animatorTransition33694.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				// State Transitions.
				var animatorStateTransition33738 = climbLeftUpAnimatorState33730.AddTransition(climbLeftIdleAnimatorState33708);
				animatorStateTransition33738.canTransitionToSelf = true;
				animatorStateTransition33738.duration = 0f;
				animatorStateTransition33738.exitTime = 1.05f;
				animatorStateTransition33738.hasExitTime = true;
				animatorStateTransition33738.hasFixedDuration = true;
				animatorStateTransition33738.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33738.offset = 0f;
				animatorStateTransition33738.orderedInterruption = true;
				animatorStateTransition33738.isExit = false;
				animatorStateTransition33738.mute = false;
				animatorStateTransition33738.solo = false;
				animatorStateTransition33738.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition33740 = climbLeftUpAnimatorState33730.AddTransition(topDismountLeftAnimatorState33686);
				animatorStateTransition33740.canTransitionToSelf = true;
				animatorStateTransition33740.duration = 0f;
				animatorStateTransition33740.exitTime = 1.05f;
				animatorStateTransition33740.hasExitTime = true;
				animatorStateTransition33740.hasFixedDuration = true;
				animatorStateTransition33740.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33740.offset = 0f;
				animatorStateTransition33740.orderedInterruption = true;
				animatorStateTransition33740.isExit = false;
				animatorStateTransition33740.mute = false;
				animatorStateTransition33740.solo = false;
				animatorStateTransition33740.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33740.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition33742 = climbLeftUpAnimatorState33730.AddExitTransition();
				animatorStateTransition33742.canTransitionToSelf = true;
				animatorStateTransition33742.duration = 0.25f;
				animatorStateTransition33742.exitTime = 0.5000001f;
				animatorStateTransition33742.hasExitTime = false;
				animatorStateTransition33742.hasFixedDuration = true;
				animatorStateTransition33742.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33742.offset = 0f;
				animatorStateTransition33742.orderedInterruption = true;
				animatorStateTransition33742.isExit = true;
				animatorStateTransition33742.mute = false;
				animatorStateTransition33742.solo = false;
				animatorStateTransition33742.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33744 = climbLeftUpAnimatorState33730.AddTransition(climbRightUpAnimatorState33732);
				animatorStateTransition33744.canTransitionToSelf = true;
				animatorStateTransition33744.duration = 0f;
				animatorStateTransition33744.exitTime = 1.05f;
				animatorStateTransition33744.hasExitTime = true;
				animatorStateTransition33744.hasFixedDuration = true;
				animatorStateTransition33744.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33744.offset = 0f;
				animatorStateTransition33744.orderedInterruption = true;
				animatorStateTransition33744.isExit = false;
				animatorStateTransition33744.mute = false;
				animatorStateTransition33744.solo = false;
				animatorStateTransition33744.AddCondition(AnimatorConditionMode.Greater, 0.001f, "ForwardMovement");
				animatorStateTransition33744.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33746 = climbLeftUpAnimatorState33730.AddTransition(climbLeftDownAnimatorState33734);
				animatorStateTransition33746.canTransitionToSelf = true;
				animatorStateTransition33746.duration = 0f;
				animatorStateTransition33746.exitTime = 1.05f;
				animatorStateTransition33746.hasExitTime = true;
				animatorStateTransition33746.hasFixedDuration = true;
				animatorStateTransition33746.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33746.offset = 0f;
				animatorStateTransition33746.orderedInterruption = true;
				animatorStateTransition33746.isExit = false;
				animatorStateTransition33746.mute = false;
				animatorStateTransition33746.solo = false;
				animatorStateTransition33746.AddCondition(AnimatorConditionMode.Less, -0.001f, "ForwardMovement");
				animatorStateTransition33746.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33750 = climbRightUpAnimatorState33732.AddTransition(climbRightIdleAnimatorState32294);
				animatorStateTransition33750.canTransitionToSelf = true;
				animatorStateTransition33750.duration = 0f;
				animatorStateTransition33750.exitTime = 1.05f;
				animatorStateTransition33750.hasExitTime = true;
				animatorStateTransition33750.hasFixedDuration = true;
				animatorStateTransition33750.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33750.offset = 0f;
				animatorStateTransition33750.orderedInterruption = true;
				animatorStateTransition33750.isExit = false;
				animatorStateTransition33750.mute = false;
				animatorStateTransition33750.solo = false;
				animatorStateTransition33750.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition33752 = climbRightUpAnimatorState33732.AddTransition(topDismountRightAnimatorState33682);
				animatorStateTransition33752.canTransitionToSelf = true;
				animatorStateTransition33752.duration = 0f;
				animatorStateTransition33752.exitTime = 1.05f;
				animatorStateTransition33752.hasExitTime = true;
				animatorStateTransition33752.hasFixedDuration = true;
				animatorStateTransition33752.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33752.offset = 0f;
				animatorStateTransition33752.orderedInterruption = true;
				animatorStateTransition33752.isExit = false;
				animatorStateTransition33752.mute = false;
				animatorStateTransition33752.solo = false;
				animatorStateTransition33752.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33752.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition33754 = climbRightUpAnimatorState33732.AddExitTransition();
				animatorStateTransition33754.canTransitionToSelf = true;
				animatorStateTransition33754.duration = 0.25f;
				animatorStateTransition33754.exitTime = 0.5f;
				animatorStateTransition33754.hasExitTime = false;
				animatorStateTransition33754.hasFixedDuration = true;
				animatorStateTransition33754.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33754.offset = 0f;
				animatorStateTransition33754.orderedInterruption = true;
				animatorStateTransition33754.isExit = true;
				animatorStateTransition33754.mute = false;
				animatorStateTransition33754.solo = false;
				animatorStateTransition33754.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33756 = climbRightUpAnimatorState33732.AddTransition(climbLeftUpAnimatorState33730);
				animatorStateTransition33756.canTransitionToSelf = true;
				animatorStateTransition33756.duration = 0f;
				animatorStateTransition33756.exitTime = 1.05f;
				animatorStateTransition33756.hasExitTime = true;
				animatorStateTransition33756.hasFixedDuration = true;
				animatorStateTransition33756.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33756.offset = 0f;
				animatorStateTransition33756.orderedInterruption = true;
				animatorStateTransition33756.isExit = false;
				animatorStateTransition33756.mute = false;
				animatorStateTransition33756.solo = false;
				animatorStateTransition33756.AddCondition(AnimatorConditionMode.Greater, 0.001f, "ForwardMovement");
				animatorStateTransition33756.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33758 = climbRightUpAnimatorState33732.AddTransition(climbRightDownAnimatorState33736);
				animatorStateTransition33758.canTransitionToSelf = true;
				animatorStateTransition33758.duration = 0f;
				animatorStateTransition33758.exitTime = 1.05f;
				animatorStateTransition33758.hasExitTime = true;
				animatorStateTransition33758.hasFixedDuration = true;
				animatorStateTransition33758.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33758.offset = 0f;
				animatorStateTransition33758.orderedInterruption = true;
				animatorStateTransition33758.isExit = false;
				animatorStateTransition33758.mute = false;
				animatorStateTransition33758.solo = false;
				animatorStateTransition33758.AddCondition(AnimatorConditionMode.Less, -0.001f, "ForwardMovement");
				animatorStateTransition33758.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33762 = climbLeftDownAnimatorState33734.AddTransition(climbRightIdleAnimatorState32294);
				animatorStateTransition33762.canTransitionToSelf = true;
				animatorStateTransition33762.duration = 0f;
				animatorStateTransition33762.exitTime = 1.05f;
				animatorStateTransition33762.hasExitTime = true;
				animatorStateTransition33762.hasFixedDuration = true;
				animatorStateTransition33762.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33762.offset = 0f;
				animatorStateTransition33762.orderedInterruption = true;
				animatorStateTransition33762.isExit = false;
				animatorStateTransition33762.mute = false;
				animatorStateTransition33762.solo = false;
				animatorStateTransition33762.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition33764 = climbLeftDownAnimatorState33734.AddTransition(bottomDismountLeftAnimatorState33688);
				animatorStateTransition33764.canTransitionToSelf = true;
				animatorStateTransition33764.duration = 0f;
				animatorStateTransition33764.exitTime = 1.05f;
				animatorStateTransition33764.hasExitTime = true;
				animatorStateTransition33764.hasFixedDuration = true;
				animatorStateTransition33764.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33764.offset = 0f;
				animatorStateTransition33764.orderedInterruption = true;
				animatorStateTransition33764.isExit = false;
				animatorStateTransition33764.mute = false;
				animatorStateTransition33764.solo = false;
				animatorStateTransition33764.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33764.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");

				var animatorStateTransition33766 = climbLeftDownAnimatorState33734.AddExitTransition();
				animatorStateTransition33766.canTransitionToSelf = true;
				animatorStateTransition33766.duration = 0.25f;
				animatorStateTransition33766.exitTime = 0.4000001f;
				animatorStateTransition33766.hasExitTime = false;
				animatorStateTransition33766.hasFixedDuration = true;
				animatorStateTransition33766.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33766.offset = 0f;
				animatorStateTransition33766.orderedInterruption = true;
				animatorStateTransition33766.isExit = true;
				animatorStateTransition33766.mute = false;
				animatorStateTransition33766.solo = false;
				animatorStateTransition33766.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33768 = climbLeftDownAnimatorState33734.AddTransition(climbRightDownAnimatorState33736);
				animatorStateTransition33768.canTransitionToSelf = true;
				animatorStateTransition33768.duration = 0f;
				animatorStateTransition33768.exitTime = 1.05f;
				animatorStateTransition33768.hasExitTime = true;
				animatorStateTransition33768.hasFixedDuration = true;
				animatorStateTransition33768.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33768.offset = 0f;
				animatorStateTransition33768.orderedInterruption = true;
				animatorStateTransition33768.isExit = false;
				animatorStateTransition33768.mute = false;
				animatorStateTransition33768.solo = false;
				animatorStateTransition33768.AddCondition(AnimatorConditionMode.Less, -0.001f, "ForwardMovement");
				animatorStateTransition33768.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33770 = climbLeftDownAnimatorState33734.AddTransition(climbLeftUpAnimatorState33730);
				animatorStateTransition33770.canTransitionToSelf = true;
				animatorStateTransition33770.duration = 0f;
				animatorStateTransition33770.exitTime = 1.05f;
				animatorStateTransition33770.hasExitTime = true;
				animatorStateTransition33770.hasFixedDuration = true;
				animatorStateTransition33770.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33770.offset = 0f;
				animatorStateTransition33770.orderedInterruption = true;
				animatorStateTransition33770.isExit = false;
				animatorStateTransition33770.mute = false;
				animatorStateTransition33770.solo = false;
				animatorStateTransition33770.AddCondition(AnimatorConditionMode.Greater, 0.001f, "ForwardMovement");
				animatorStateTransition33770.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33774 = climbRightDownAnimatorState33736.AddTransition(climbLeftIdleAnimatorState33708);
				animatorStateTransition33774.canTransitionToSelf = true;
				animatorStateTransition33774.duration = 0f;
				animatorStateTransition33774.exitTime = 1.05f;
				animatorStateTransition33774.hasExitTime = true;
				animatorStateTransition33774.hasFixedDuration = true;
				animatorStateTransition33774.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33774.offset = 0f;
				animatorStateTransition33774.orderedInterruption = true;
				animatorStateTransition33774.isExit = false;
				animatorStateTransition33774.mute = false;
				animatorStateTransition33774.solo = false;
				animatorStateTransition33774.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition33776 = climbRightDownAnimatorState33736.AddExitTransition();
				animatorStateTransition33776.canTransitionToSelf = true;
				animatorStateTransition33776.duration = 0.25f;
				animatorStateTransition33776.exitTime = 0.4000003f;
				animatorStateTransition33776.hasExitTime = false;
				animatorStateTransition33776.hasFixedDuration = true;
				animatorStateTransition33776.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33776.offset = 0f;
				animatorStateTransition33776.orderedInterruption = true;
				animatorStateTransition33776.isExit = true;
				animatorStateTransition33776.mute = false;
				animatorStateTransition33776.solo = false;
				animatorStateTransition33776.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33778 = climbRightDownAnimatorState33736.AddTransition(bottomDismountRightAnimatorState33684);
				animatorStateTransition33778.canTransitionToSelf = true;
				animatorStateTransition33778.duration = 0f;
				animatorStateTransition33778.exitTime = 1.05f;
				animatorStateTransition33778.hasExitTime = true;
				animatorStateTransition33778.hasFixedDuration = true;
				animatorStateTransition33778.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33778.offset = 0f;
				animatorStateTransition33778.orderedInterruption = true;
				animatorStateTransition33778.isExit = false;
				animatorStateTransition33778.mute = false;
				animatorStateTransition33778.solo = false;
				animatorStateTransition33778.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33778.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");

				var animatorStateTransition33780 = climbRightDownAnimatorState33736.AddTransition(climbLeftDownAnimatorState33734);
				animatorStateTransition33780.canTransitionToSelf = true;
				animatorStateTransition33780.duration = 0f;
				animatorStateTransition33780.exitTime = 1.05f;
				animatorStateTransition33780.hasExitTime = true;
				animatorStateTransition33780.hasFixedDuration = true;
				animatorStateTransition33780.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33780.offset = 0f;
				animatorStateTransition33780.orderedInterruption = true;
				animatorStateTransition33780.isExit = false;
				animatorStateTransition33780.mute = false;
				animatorStateTransition33780.solo = false;
				animatorStateTransition33780.AddCondition(AnimatorConditionMode.Less, -0.001f, "ForwardMovement");
				animatorStateTransition33780.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33782 = climbRightDownAnimatorState33736.AddTransition(climbRightUpAnimatorState33732);
				animatorStateTransition33782.canTransitionToSelf = true;
				animatorStateTransition33782.duration = 0f;
				animatorStateTransition33782.exitTime = 1.05f;
				animatorStateTransition33782.hasExitTime = true;
				animatorStateTransition33782.hasFixedDuration = true;
				animatorStateTransition33782.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33782.offset = 0f;
				animatorStateTransition33782.orderedInterruption = true;
				animatorStateTransition33782.isExit = false;
				animatorStateTransition33782.mute = false;
				animatorStateTransition33782.solo = false;
				animatorStateTransition33782.AddCondition(AnimatorConditionMode.Greater, 0.001f, "ForwardMovement");
				animatorStateTransition33782.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33786 = climbLeftIdleAnimatorState33708.AddTransition(climbRightUpAnimatorState33732);
				animatorStateTransition33786.canTransitionToSelf = true;
				animatorStateTransition33786.duration = 0f;
				animatorStateTransition33786.exitTime = 0f;
				animatorStateTransition33786.hasExitTime = false;
				animatorStateTransition33786.hasFixedDuration = true;
				animatorStateTransition33786.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33786.offset = 0f;
				animatorStateTransition33786.orderedInterruption = true;
				animatorStateTransition33786.isExit = false;
				animatorStateTransition33786.mute = false;
				animatorStateTransition33786.solo = false;
				animatorStateTransition33786.AddCondition(AnimatorConditionMode.Greater, 0.001f, "ForwardMovement");
				animatorStateTransition33786.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33788 = climbLeftIdleAnimatorState33708.AddTransition(climbLeftDownAnimatorState33734);
				animatorStateTransition33788.canTransitionToSelf = true;
				animatorStateTransition33788.duration = 0f;
				animatorStateTransition33788.exitTime = 1f;
				animatorStateTransition33788.hasExitTime = false;
				animatorStateTransition33788.hasFixedDuration = true;
				animatorStateTransition33788.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33788.offset = 0f;
				animatorStateTransition33788.orderedInterruption = true;
				animatorStateTransition33788.isExit = false;
				animatorStateTransition33788.mute = false;
				animatorStateTransition33788.solo = false;
				animatorStateTransition33788.AddCondition(AnimatorConditionMode.Less, -0.001f, "ForwardMovement");
				animatorStateTransition33788.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33790 = climbLeftIdleAnimatorState33708.AddTransition(topDismountLeftAnimatorState33686);
				animatorStateTransition33790.canTransitionToSelf = true;
				animatorStateTransition33790.duration = 0.1f;
				animatorStateTransition33790.exitTime = 0f;
				animatorStateTransition33790.hasExitTime = false;
				animatorStateTransition33790.hasFixedDuration = true;
				animatorStateTransition33790.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33790.offset = 0f;
				animatorStateTransition33790.orderedInterruption = true;
				animatorStateTransition33790.isExit = false;
				animatorStateTransition33790.mute = false;
				animatorStateTransition33790.solo = false;
				animatorStateTransition33790.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33790.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition33792 = climbLeftIdleAnimatorState33708.AddExitTransition();
				animatorStateTransition33792.canTransitionToSelf = true;
				animatorStateTransition33792.duration = 0.25f;
				animatorStateTransition33792.exitTime = 0f;
				animatorStateTransition33792.hasExitTime = false;
				animatorStateTransition33792.hasFixedDuration = true;
				animatorStateTransition33792.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33792.offset = 0f;
				animatorStateTransition33792.orderedInterruption = true;
				animatorStateTransition33792.isExit = true;
				animatorStateTransition33792.mute = false;
				animatorStateTransition33792.solo = false;
				animatorStateTransition33792.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33794 = climbLeftIdleAnimatorState33708.AddTransition(bottomDismountRightAnimatorState33684);
				animatorStateTransition33794.canTransitionToSelf = true;
				animatorStateTransition33794.duration = 0f;
				animatorStateTransition33794.exitTime = 0f;
				animatorStateTransition33794.hasExitTime = false;
				animatorStateTransition33794.hasFixedDuration = true;
				animatorStateTransition33794.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33794.offset = 0f;
				animatorStateTransition33794.orderedInterruption = true;
				animatorStateTransition33794.isExit = false;
				animatorStateTransition33794.mute = false;
				animatorStateTransition33794.solo = false;
				animatorStateTransition33794.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33794.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");

				var animatorStateTransition33798 = climbRightIdleAnimatorState32294.AddTransition(climbLeftUpAnimatorState33730);
				animatorStateTransition33798.canTransitionToSelf = true;
				animatorStateTransition33798.duration = 0f;
				animatorStateTransition33798.exitTime = 0f;
				animatorStateTransition33798.hasExitTime = false;
				animatorStateTransition33798.hasFixedDuration = true;
				animatorStateTransition33798.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33798.offset = 0f;
				animatorStateTransition33798.orderedInterruption = true;
				animatorStateTransition33798.isExit = false;
				animatorStateTransition33798.mute = false;
				animatorStateTransition33798.solo = false;
				animatorStateTransition33798.AddCondition(AnimatorConditionMode.Greater, 0.001f, "ForwardMovement");
				animatorStateTransition33798.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33800 = climbRightIdleAnimatorState32294.AddTransition(climbRightDownAnimatorState33736);
				animatorStateTransition33800.canTransitionToSelf = true;
				animatorStateTransition33800.duration = 0f;
				animatorStateTransition33800.exitTime = 1f;
				animatorStateTransition33800.hasExitTime = false;
				animatorStateTransition33800.hasFixedDuration = true;
				animatorStateTransition33800.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33800.offset = 0f;
				animatorStateTransition33800.orderedInterruption = true;
				animatorStateTransition33800.isExit = false;
				animatorStateTransition33800.mute = false;
				animatorStateTransition33800.solo = false;
				animatorStateTransition33800.AddCondition(AnimatorConditionMode.Less, -0.001f, "ForwardMovement");
				animatorStateTransition33800.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");

				var animatorStateTransition33802 = climbRightIdleAnimatorState32294.AddTransition(topDismountRightAnimatorState33682);
				animatorStateTransition33802.canTransitionToSelf = true;
				animatorStateTransition33802.duration = 0.1f;
				animatorStateTransition33802.exitTime = 0f;
				animatorStateTransition33802.hasExitTime = false;
				animatorStateTransition33802.hasFixedDuration = true;
				animatorStateTransition33802.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33802.offset = 0f;
				animatorStateTransition33802.orderedInterruption = true;
				animatorStateTransition33802.isExit = false;
				animatorStateTransition33802.mute = false;
				animatorStateTransition33802.solo = false;
				animatorStateTransition33802.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33802.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition33804 = climbRightIdleAnimatorState32294.AddExitTransition();
				animatorStateTransition33804.canTransitionToSelf = true;
				animatorStateTransition33804.duration = 0.25f;
				animatorStateTransition33804.exitTime = 0f;
				animatorStateTransition33804.hasExitTime = false;
				animatorStateTransition33804.hasFixedDuration = true;
				animatorStateTransition33804.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33804.offset = 0f;
				animatorStateTransition33804.orderedInterruption = true;
				animatorStateTransition33804.isExit = true;
				animatorStateTransition33804.mute = false;
				animatorStateTransition33804.solo = false;
				animatorStateTransition33804.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33806 = climbRightIdleAnimatorState32294.AddTransition(bottomDismountLeftAnimatorState33688);
				animatorStateTransition33806.canTransitionToSelf = true;
				animatorStateTransition33806.duration = 0f;
				animatorStateTransition33806.exitTime = 0f;
				animatorStateTransition33806.hasExitTime = false;
				animatorStateTransition33806.hasFixedDuration = true;
				animatorStateTransition33806.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33806.offset = 0f;
				animatorStateTransition33806.orderedInterruption = true;
				animatorStateTransition33806.isExit = false;
				animatorStateTransition33806.mute = false;
				animatorStateTransition33806.solo = false;
				animatorStateTransition33806.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition33806.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");

				// State Transitions.
				var animatorStateTransition33810 = hangtoLadderClimbLeftAnimatorState32324.AddExitTransition();
				animatorStateTransition33810.canTransitionToSelf = true;
				animatorStateTransition33810.duration = 0.05f;
				animatorStateTransition33810.exitTime = 1f;
				animatorStateTransition33810.hasExitTime = true;
				animatorStateTransition33810.hasFixedDuration = true;
				animatorStateTransition33810.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33810.offset = 0f;
				animatorStateTransition33810.orderedInterruption = true;
				animatorStateTransition33810.isExit = true;
				animatorStateTransition33810.mute = false;
				animatorStateTransition33810.solo = false;
				animatorStateTransition33810.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33812 = hangtoLadderClimbLeftAnimatorState32324.AddTransition(climbLeftIdleAnimatorState33708);
				animatorStateTransition33812.canTransitionToSelf = true;
				animatorStateTransition33812.duration = 0.05f;
				animatorStateTransition33812.exitTime = 1f;
				animatorStateTransition33812.hasExitTime = true;
				animatorStateTransition33812.hasFixedDuration = true;
				animatorStateTransition33812.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33812.offset = 0f;
				animatorStateTransition33812.orderedInterruption = true;
				animatorStateTransition33812.isExit = false;
				animatorStateTransition33812.mute = false;
				animatorStateTransition33812.solo = false;
				animatorStateTransition33812.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");

				var animatorStateTransition33816 = hangtoLadderClimbRightAnimatorState32326.AddExitTransition();
				animatorStateTransition33816.canTransitionToSelf = true;
				animatorStateTransition33816.duration = 0.05f;
				animatorStateTransition33816.exitTime = 1f;
				animatorStateTransition33816.hasExitTime = true;
				animatorStateTransition33816.hasFixedDuration = true;
				animatorStateTransition33816.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33816.offset = 0f;
				animatorStateTransition33816.orderedInterruption = true;
				animatorStateTransition33816.isExit = true;
				animatorStateTransition33816.mute = false;
				animatorStateTransition33816.solo = false;
				animatorStateTransition33816.AddCondition(AnimatorConditionMode.NotEqual, 501f, "AbilityIndex");

				var animatorStateTransition33818 = hangtoLadderClimbRightAnimatorState32326.AddTransition(climbLeftIdleAnimatorState33708);
				animatorStateTransition33818.canTransitionToSelf = true;
				animatorStateTransition33818.duration = 0.05f;
				animatorStateTransition33818.exitTime = 1f;
				animatorStateTransition33818.hasExitTime = true;
				animatorStateTransition33818.hasFixedDuration = true;
				animatorStateTransition33818.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition33818.offset = 0f;
				animatorStateTransition33818.orderedInterruption = true;
				animatorStateTransition33818.isExit = false;
				animatorStateTransition33818.mute = false;
				animatorStateTransition33818.solo = false;
				animatorStateTransition33818.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition32114 = baseStateMachine207580318.AddAnyStateTransition(topMountAnimatorState32290);
				animatorStateTransition32114.canTransitionToSelf = true;
				animatorStateTransition32114.duration = 0f;
				animatorStateTransition32114.exitTime = 0.75f;
				animatorStateTransition32114.hasExitTime = false;
				animatorStateTransition32114.hasFixedDuration = true;
				animatorStateTransition32114.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32114.offset = 0f;
				animatorStateTransition32114.orderedInterruption = true;
				animatorStateTransition32114.isExit = false;
				animatorStateTransition32114.mute = false;
				animatorStateTransition32114.solo = false;
				animatorStateTransition32114.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32114.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition32114.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");

				var animatorStateTransition32116 = baseStateMachine207580318.AddAnyStateTransition(bottomMountAnimatorState32292);
				animatorStateTransition32116.canTransitionToSelf = false;
				animatorStateTransition32116.duration = 0f;
				animatorStateTransition32116.exitTime = 0.75f;
				animatorStateTransition32116.hasExitTime = false;
				animatorStateTransition32116.hasFixedDuration = true;
				animatorStateTransition32116.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32116.offset = 0f;
				animatorStateTransition32116.orderedInterruption = true;
				animatorStateTransition32116.isExit = false;
				animatorStateTransition32116.mute = false;
				animatorStateTransition32116.solo = false;
				animatorStateTransition32116.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32116.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition32116.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");

				var animatorStateTransition32118 = baseStateMachine207580318.AddAnyStateTransition(climbRightIdleAnimatorState32294);
				animatorStateTransition32118.canTransitionToSelf = true;
				animatorStateTransition32118.duration = 0f;
				animatorStateTransition32118.exitTime = 0.75f;
				animatorStateTransition32118.hasExitTime = false;
				animatorStateTransition32118.hasFixedDuration = true;
				animatorStateTransition32118.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32118.offset = 0f;
				animatorStateTransition32118.orderedInterruption = true;
				animatorStateTransition32118.isExit = false;
				animatorStateTransition32118.mute = false;
				animatorStateTransition32118.solo = false;
				animatorStateTransition32118.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32118.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition32118.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition32148 = baseStateMachine207580318.AddAnyStateTransition(hangtoLadderClimbLeftAnimatorState32324);
				animatorStateTransition32148.canTransitionToSelf = false;
				animatorStateTransition32148.duration = 0.05f;
				animatorStateTransition32148.exitTime = 0.75f;
				animatorStateTransition32148.hasExitTime = false;
				animatorStateTransition32148.hasFixedDuration = true;
				animatorStateTransition32148.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32148.offset = 0f;
				animatorStateTransition32148.orderedInterruption = true;
				animatorStateTransition32148.isExit = false;
				animatorStateTransition32148.mute = false;
				animatorStateTransition32148.solo = false;
				animatorStateTransition32148.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32148.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition32148.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");
				animatorStateTransition32148.AddCondition(AnimatorConditionMode.Less, 0f, "HorizontalMovement");

				var animatorStateTransition32150 = baseStateMachine207580318.AddAnyStateTransition(hangtoLadderClimbRightAnimatorState32326);
				animatorStateTransition32150.canTransitionToSelf = false;
				animatorStateTransition32150.duration = 0.05f;
				animatorStateTransition32150.exitTime = 0.75f;
				animatorStateTransition32150.hasExitTime = false;
				animatorStateTransition32150.hasFixedDuration = true;
				animatorStateTransition32150.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition32150.offset = 0f;
				animatorStateTransition32150.orderedInterruption = true;
				animatorStateTransition32150.isExit = false;
				animatorStateTransition32150.mute = false;
				animatorStateTransition32150.solo = false;
				animatorStateTransition32150.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition32150.AddCondition(AnimatorConditionMode.Equals, 501f, "AbilityIndex");
				animatorStateTransition32150.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");
				animatorStateTransition32150.AddCondition(AnimatorConditionMode.Greater, 0f, "HorizontalMovement");
			}
		}
	}
}
