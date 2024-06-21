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
	/// Implements AbilityDrawer for the Dodge ControlType.
	/// </summary>
	[ControlType(typeof(Dodge))]
	public class DodgeDrawer : AbilityDrawer
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

				var baseStateMachine275562594 = animatorControllers[i].layers[0].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine275562594.stateMachines.Length; ++k) {
						if (baseStateMachine275562594.stateMachines[k].stateMachine.name == "Dodge") {
							baseStateMachine275562594.RemoveStateMachine(baseStateMachine275562594.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var meleeDodgeBwdAnimationClip37626Path = AssetDatabase.GUIDToAssetPath("e7bcf8f3e84813f48a90cd7827dbf231"); 
				var meleeDodgeBwdAnimationClip37626 = AnimatorBuilder.GetAnimationClip(meleeDodgeBwdAnimationClip37626Path, "MeleeDodgeBwd");
				var meleeDodgeFwdAnimationClip37630Path = AssetDatabase.GUIDToAssetPath("a9418386ebd4e1644ae68187307fc18d"); 
				var meleeDodgeFwdAnimationClip37630 = AnimatorBuilder.GetAnimationClip(meleeDodgeFwdAnimationClip37630Path, "MeleeDodgeFwd");
				var meleeDodgeRightFrontAnimationClip37634Path = AssetDatabase.GUIDToAssetPath("51ea8026172e39b41aea0efa128ffa9b"); 
				var meleeDodgeRightFrontAnimationClip37634 = AnimatorBuilder.GetAnimationClip(meleeDodgeRightFrontAnimationClip37634Path, "MeleeDodgeRightFront");
				var meleeDodgeLeftFrontAnimationClip37638Path = AssetDatabase.GUIDToAssetPath("36ff4a8422553284e81570068ac8394c"); 
				var meleeDodgeLeftFrontAnimationClip37638 = AnimatorBuilder.GetAnimationClip(meleeDodgeLeftFrontAnimationClip37638Path, "MeleeDodgeLeftFront");
				var meleeDodgeLeftBackAnimationClip37642Path = AssetDatabase.GUIDToAssetPath("8ff8c865fd67aa147b0247cee5342ed6"); 
				var meleeDodgeLeftBackAnimationClip37642 = AnimatorBuilder.GetAnimationClip(meleeDodgeLeftBackAnimationClip37642Path, "MeleeDodgeLeftBack");
				var meleeDodgeRightBackAnimationClip37646Path = AssetDatabase.GUIDToAssetPath("2540da54527da3f48a4268b798bb77f8"); 
				var meleeDodgeRightBackAnimationClip37646 = AnimatorBuilder.GetAnimationClip(meleeDodgeRightBackAnimationClip37646Path, "MeleeDodgeRightBack");
				var dodgeLeftFrontAnimationClip37650Path = AssetDatabase.GUIDToAssetPath("2924899db8750e145a94f8407e27f52e"); 
				var dodgeLeftFrontAnimationClip37650 = AnimatorBuilder.GetAnimationClip(dodgeLeftFrontAnimationClip37650Path, "DodgeLeftFront");
				var dodgeRightFrontAnimationClip37654Path = AssetDatabase.GUIDToAssetPath("b44a54647fcaab745b64d5fd05e82d23"); 
				var dodgeRightFrontAnimationClip37654 = AnimatorBuilder.GetAnimationClip(dodgeRightFrontAnimationClip37654Path, "DodgeRightFront");
				var dodgeFwdAnimationClip37658Path = AssetDatabase.GUIDToAssetPath("c6577d4489d7c56478e2206131c3f836"); 
				var dodgeFwdAnimationClip37658 = AnimatorBuilder.GetAnimationClip(dodgeFwdAnimationClip37658Path, "DodgeFwd");
				var dodgeBwdAnimationClip37662Path = AssetDatabase.GUIDToAssetPath("e1a75bd20e3b6a449b6cdb0272e141da"); 
				var dodgeBwdAnimationClip37662 = AnimatorBuilder.GetAnimationClip(dodgeBwdAnimationClip37662Path, "DodgeBwd");
				var dodgeRightBackAnimationClip37666Path = AssetDatabase.GUIDToAssetPath("de82364f24793704ba3dbe4388b6d4ee"); 
				var dodgeRightBackAnimationClip37666 = AnimatorBuilder.GetAnimationClip(dodgeRightBackAnimationClip37666Path, "DodgeRightBack");
				var dodgeLeftBackAnimationClip37670Path = AssetDatabase.GUIDToAssetPath("b1aa4ab0d79ce554db3b358804cc79fc"); 
				var dodgeLeftBackAnimationClip37670 = AnimatorBuilder.GetAnimationClip(dodgeLeftBackAnimationClip37670Path, "DodgeLeftBack");
				var bowDodgeBwdAnimationClip37674Path = AssetDatabase.GUIDToAssetPath("1b60dd3de1d599144a0f7abc7647ca5c"); 
				var bowDodgeBwdAnimationClip37674 = AnimatorBuilder.GetAnimationClip(bowDodgeBwdAnimationClip37674Path, "BowDodgeBwd");
				var bowDodgeFwdAnimationClip37678Path = AssetDatabase.GUIDToAssetPath("e2affe2826a89294e9b162cd9898143c"); 
				var bowDodgeFwdAnimationClip37678 = AnimatorBuilder.GetAnimationClip(bowDodgeFwdAnimationClip37678Path, "BowDodgeFwd");
				var bowDodgeRightFrontAnimationClip37682Path = AssetDatabase.GUIDToAssetPath("eb7944ac7f494cd418908bf77250f0f0"); 
				var bowDodgeRightFrontAnimationClip37682 = AnimatorBuilder.GetAnimationClip(bowDodgeRightFrontAnimationClip37682Path, "BowDodgeRightFront");
				var bowDodgeLeftFrontAnimationClip37686Path = AssetDatabase.GUIDToAssetPath("79f1b69833b2c99499766434ee4f1088"); 
				var bowDodgeLeftFrontAnimationClip37686 = AnimatorBuilder.GetAnimationClip(bowDodgeLeftFrontAnimationClip37686Path, "BowDodgeLeftFront");
				var bowDodgeLeftBackAnimationClip37690Path = AssetDatabase.GUIDToAssetPath("08e76a348a853204191cc4b375421fcf"); 
				var bowDodgeLeftBackAnimationClip37690 = AnimatorBuilder.GetAnimationClip(bowDodgeLeftBackAnimationClip37690Path, "BowDodgeLeftBack");
				var bowDodgeRightBackAnimationClip37694Path = AssetDatabase.GUIDToAssetPath("11b8cb3821ea2044ca00f75a857195a8"); 
				var bowDodgeRightBackAnimationClip37694 = AnimatorBuilder.GetAnimationClip(bowDodgeRightBackAnimationClip37694Path, "BowDodgeRightBack");

				// State Machine.
				var dodgeAnimatorStateMachine34266 = baseStateMachine275562594.AddStateMachine("Dodge", new Vector3(624f, 60f, 0f));

				// State Machine.
				var meleeDodgeAnimatorStateMachine37618 = dodgeAnimatorStateMachine34266.AddStateMachine("Melee Dodge", new Vector3(372f, 36f, 0f));

				// States.
				var meleeDodgeBackwardAnimatorState35130 = meleeDodgeAnimatorStateMachine37618.AddState("Melee Dodge Backward", new Vector3(384f, -60f, 0f));
				meleeDodgeBackwardAnimatorState35130.motion = meleeDodgeBwdAnimationClip37626;
				meleeDodgeBackwardAnimatorState35130.cycleOffset = 0f;
				meleeDodgeBackwardAnimatorState35130.cycleOffsetParameterActive = false;
				meleeDodgeBackwardAnimatorState35130.iKOnFeet = true;
				meleeDodgeBackwardAnimatorState35130.mirror = false;
				meleeDodgeBackwardAnimatorState35130.mirrorParameterActive = false;
				meleeDodgeBackwardAnimatorState35130.speed = 1f;
				meleeDodgeBackwardAnimatorState35130.speedParameterActive = false;
				meleeDodgeBackwardAnimatorState35130.writeDefaultValues = false;

				var meleeDodgeForwardAnimatorState35132 = meleeDodgeAnimatorStateMachine37618.AddState("Melee Dodge Forward", new Vector3(384f, -120f, 0f));
				meleeDodgeForwardAnimatorState35132.motion = meleeDodgeFwdAnimationClip37630;
				meleeDodgeForwardAnimatorState35132.cycleOffset = 0f;
				meleeDodgeForwardAnimatorState35132.cycleOffsetParameterActive = false;
				meleeDodgeForwardAnimatorState35132.iKOnFeet = true;
				meleeDodgeForwardAnimatorState35132.mirror = false;
				meleeDodgeForwardAnimatorState35132.mirrorParameterActive = false;
				meleeDodgeForwardAnimatorState35132.speed = 1f;
				meleeDodgeForwardAnimatorState35132.speedParameterActive = false;
				meleeDodgeForwardAnimatorState35132.writeDefaultValues = false;

				var meleeDodgeRightFrontAnimatorState35134 = meleeDodgeAnimatorStateMachine37618.AddState("Melee Dodge Right Front", new Vector3(384f, 120f, 0f));
				meleeDodgeRightFrontAnimatorState35134.motion = meleeDodgeRightFrontAnimationClip37634;
				meleeDodgeRightFrontAnimatorState35134.cycleOffset = 0f;
				meleeDodgeRightFrontAnimatorState35134.cycleOffsetParameterActive = false;
				meleeDodgeRightFrontAnimatorState35134.iKOnFeet = true;
				meleeDodgeRightFrontAnimatorState35134.mirror = false;
				meleeDodgeRightFrontAnimatorState35134.mirrorParameterActive = false;
				meleeDodgeRightFrontAnimatorState35134.speed = 1f;
				meleeDodgeRightFrontAnimatorState35134.speedParameterActive = false;
				meleeDodgeRightFrontAnimatorState35134.writeDefaultValues = false;

				var meleeDodgeLeftFrontAnimatorState35136 = meleeDodgeAnimatorStateMachine37618.AddState("Melee Dodge Left Front", new Vector3(384f, 0f, 0f));
				meleeDodgeLeftFrontAnimatorState35136.motion = meleeDodgeLeftFrontAnimationClip37638;
				meleeDodgeLeftFrontAnimatorState35136.cycleOffset = 0f;
				meleeDodgeLeftFrontAnimatorState35136.cycleOffsetParameterActive = false;
				meleeDodgeLeftFrontAnimatorState35136.iKOnFeet = true;
				meleeDodgeLeftFrontAnimatorState35136.mirror = false;
				meleeDodgeLeftFrontAnimatorState35136.mirrorParameterActive = false;
				meleeDodgeLeftFrontAnimatorState35136.speed = 1f;
				meleeDodgeLeftFrontAnimatorState35136.speedParameterActive = false;
				meleeDodgeLeftFrontAnimatorState35136.writeDefaultValues = false;

				var meleeDodgeLeftBackAnimatorState35138 = meleeDodgeAnimatorStateMachine37618.AddState("Melee Dodge Left Back", new Vector3(384f, 60f, 0f));
				meleeDodgeLeftBackAnimatorState35138.motion = meleeDodgeLeftBackAnimationClip37642;
				meleeDodgeLeftBackAnimatorState35138.cycleOffset = 0f;
				meleeDodgeLeftBackAnimatorState35138.cycleOffsetParameterActive = false;
				meleeDodgeLeftBackAnimatorState35138.iKOnFeet = true;
				meleeDodgeLeftBackAnimatorState35138.mirror = false;
				meleeDodgeLeftBackAnimatorState35138.mirrorParameterActive = false;
				meleeDodgeLeftBackAnimatorState35138.speed = 1f;
				meleeDodgeLeftBackAnimatorState35138.speedParameterActive = false;
				meleeDodgeLeftBackAnimatorState35138.writeDefaultValues = false;

				var meleeDodgeRightBackAnimatorState35140 = meleeDodgeAnimatorStateMachine37618.AddState("Melee Dodge Right Back", new Vector3(384f, 180f, 0f));
				meleeDodgeRightBackAnimatorState35140.motion = meleeDodgeRightBackAnimationClip37646;
				meleeDodgeRightBackAnimatorState35140.cycleOffset = 0f;
				meleeDodgeRightBackAnimatorState35140.cycleOffsetParameterActive = false;
				meleeDodgeRightBackAnimatorState35140.iKOnFeet = true;
				meleeDodgeRightBackAnimatorState35140.mirror = false;
				meleeDodgeRightBackAnimatorState35140.mirrorParameterActive = false;
				meleeDodgeRightBackAnimatorState35140.speed = 1f;
				meleeDodgeRightBackAnimatorState35140.speedParameterActive = false;
				meleeDodgeRightBackAnimatorState35140.writeDefaultValues = false;

				// State Machine Defaults.
				meleeDodgeAnimatorStateMachine37618.anyStatePosition = new Vector3(48f, 36f, 0f);
				meleeDodgeAnimatorStateMachine37618.defaultState = meleeDodgeForwardAnimatorState35132;
				meleeDodgeAnimatorStateMachine37618.entryPosition = new Vector3(48f, -12f, 0f);
				meleeDodgeAnimatorStateMachine37618.exitPosition = new Vector3(800f, 120f, 0f);
				meleeDodgeAnimatorStateMachine37618.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Machine.
				var aimDodgeAnimatorStateMachine37620 = dodgeAnimatorStateMachine34266.AddStateMachine("Aim Dodge", new Vector3(372f, -12f, 0f));

				// States.
				var dodgeLeftFrontAnimatorState35142 = aimDodgeAnimatorStateMachine37620.AddState("Dodge Left Front", new Vector3(432f, 72f, 0f));
				dodgeLeftFrontAnimatorState35142.motion = dodgeLeftFrontAnimationClip37650;
				dodgeLeftFrontAnimatorState35142.cycleOffset = 0f;
				dodgeLeftFrontAnimatorState35142.cycleOffsetParameterActive = false;
				dodgeLeftFrontAnimatorState35142.iKOnFeet = true;
				dodgeLeftFrontAnimatorState35142.mirror = false;
				dodgeLeftFrontAnimatorState35142.mirrorParameterActive = false;
				dodgeLeftFrontAnimatorState35142.speed = 1f;
				dodgeLeftFrontAnimatorState35142.speedParameterActive = false;
				dodgeLeftFrontAnimatorState35142.writeDefaultValues = false;

				var dodgeRightFrontAnimatorState35144 = aimDodgeAnimatorStateMachine37620.AddState("Dodge Right Front", new Vector3(432f, 192f, 0f));
				dodgeRightFrontAnimatorState35144.motion = dodgeRightFrontAnimationClip37654;
				dodgeRightFrontAnimatorState35144.cycleOffset = 0f;
				dodgeRightFrontAnimatorState35144.cycleOffsetParameterActive = false;
				dodgeRightFrontAnimatorState35144.iKOnFeet = true;
				dodgeRightFrontAnimatorState35144.mirror = false;
				dodgeRightFrontAnimatorState35144.mirrorParameterActive = false;
				dodgeRightFrontAnimatorState35144.speed = 1f;
				dodgeRightFrontAnimatorState35144.speedParameterActive = false;
				dodgeRightFrontAnimatorState35144.writeDefaultValues = false;

				var dodgeForwardAnimatorState35146 = aimDodgeAnimatorStateMachine37620.AddState("Dodge Forward", new Vector3(432f, -48f, 0f));
				dodgeForwardAnimatorState35146.motion = dodgeFwdAnimationClip37658;
				dodgeForwardAnimatorState35146.cycleOffset = 0f;
				dodgeForwardAnimatorState35146.cycleOffsetParameterActive = false;
				dodgeForwardAnimatorState35146.iKOnFeet = true;
				dodgeForwardAnimatorState35146.mirror = false;
				dodgeForwardAnimatorState35146.mirrorParameterActive = false;
				dodgeForwardAnimatorState35146.speed = 1f;
				dodgeForwardAnimatorState35146.speedParameterActive = false;
				dodgeForwardAnimatorState35146.writeDefaultValues = true;

				var dodgeBackwardAnimatorState35148 = aimDodgeAnimatorStateMachine37620.AddState("Dodge Backward", new Vector3(432f, 12f, 0f));
				dodgeBackwardAnimatorState35148.motion = dodgeBwdAnimationClip37662;
				dodgeBackwardAnimatorState35148.cycleOffset = 0f;
				dodgeBackwardAnimatorState35148.cycleOffsetParameterActive = false;
				dodgeBackwardAnimatorState35148.iKOnFeet = true;
				dodgeBackwardAnimatorState35148.mirror = false;
				dodgeBackwardAnimatorState35148.mirrorParameterActive = false;
				dodgeBackwardAnimatorState35148.speed = 1f;
				dodgeBackwardAnimatorState35148.speedParameterActive = false;
				dodgeBackwardAnimatorState35148.writeDefaultValues = true;

				var dodgeRightBackAnimatorState35150 = aimDodgeAnimatorStateMachine37620.AddState("Dodge Right Back", new Vector3(432f, 252f, 0f));
				dodgeRightBackAnimatorState35150.motion = dodgeRightBackAnimationClip37666;
				dodgeRightBackAnimatorState35150.cycleOffset = 0f;
				dodgeRightBackAnimatorState35150.cycleOffsetParameterActive = false;
				dodgeRightBackAnimatorState35150.iKOnFeet = true;
				dodgeRightBackAnimatorState35150.mirror = false;
				dodgeRightBackAnimatorState35150.mirrorParameterActive = false;
				dodgeRightBackAnimatorState35150.speed = 1f;
				dodgeRightBackAnimatorState35150.speedParameterActive = false;
				dodgeRightBackAnimatorState35150.writeDefaultValues = false;

				var dodgeLeftBackAnimatorState35152 = aimDodgeAnimatorStateMachine37620.AddState("Dodge Left Back", new Vector3(432f, 132f, 0f));
				dodgeLeftBackAnimatorState35152.motion = dodgeLeftBackAnimationClip37670;
				dodgeLeftBackAnimatorState35152.cycleOffset = 0f;
				dodgeLeftBackAnimatorState35152.cycleOffsetParameterActive = false;
				dodgeLeftBackAnimatorState35152.iKOnFeet = true;
				dodgeLeftBackAnimatorState35152.mirror = false;
				dodgeLeftBackAnimatorState35152.mirrorParameterActive = false;
				dodgeLeftBackAnimatorState35152.speed = 1f;
				dodgeLeftBackAnimatorState35152.speedParameterActive = false;
				dodgeLeftBackAnimatorState35152.writeDefaultValues = false;

				// State Machine Defaults.
				aimDodgeAnimatorStateMachine37620.anyStatePosition = new Vector3(48f, 48f, 0f);
				aimDodgeAnimatorStateMachine37620.defaultState = dodgeForwardAnimatorState35146;
				aimDodgeAnimatorStateMachine37620.entryPosition = new Vector3(48f, 0f, 0f);
				aimDodgeAnimatorStateMachine37620.exitPosition = new Vector3(800f, 120f, 0f);
				aimDodgeAnimatorStateMachine37620.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Machine.
				var bowDodgeAnimatorStateMachine37622 = dodgeAnimatorStateMachine34266.AddStateMachine("Bow Dodge", new Vector3(372f, 84f, 0f));

				// States.
				var bowDodgeBackwardAnimatorState35154 = bowDodgeAnimatorStateMachine37622.AddState("Bow Dodge Backward", new Vector3(384f, -60f, 0f));
				bowDodgeBackwardAnimatorState35154.motion = bowDodgeBwdAnimationClip37674;
				bowDodgeBackwardAnimatorState35154.cycleOffset = 0f;
				bowDodgeBackwardAnimatorState35154.cycleOffsetParameterActive = false;
				bowDodgeBackwardAnimatorState35154.iKOnFeet = true;
				bowDodgeBackwardAnimatorState35154.mirror = false;
				bowDodgeBackwardAnimatorState35154.mirrorParameterActive = false;
				bowDodgeBackwardAnimatorState35154.speed = 1f;
				bowDodgeBackwardAnimatorState35154.speedParameterActive = false;
				bowDodgeBackwardAnimatorState35154.writeDefaultValues = false;

				var bowDodgeForwardAnimatorState35156 = bowDodgeAnimatorStateMachine37622.AddState("Bow Dodge Forward", new Vector3(384f, -120f, 0f));
				bowDodgeForwardAnimatorState35156.motion = bowDodgeFwdAnimationClip37678;
				bowDodgeForwardAnimatorState35156.cycleOffset = 0f;
				bowDodgeForwardAnimatorState35156.cycleOffsetParameterActive = false;
				bowDodgeForwardAnimatorState35156.iKOnFeet = true;
				bowDodgeForwardAnimatorState35156.mirror = false;
				bowDodgeForwardAnimatorState35156.mirrorParameterActive = false;
				bowDodgeForwardAnimatorState35156.speed = 1f;
				bowDodgeForwardAnimatorState35156.speedParameterActive = false;
				bowDodgeForwardAnimatorState35156.writeDefaultValues = false;

				var bowDodgeRightFrontAnimatorState35158 = bowDodgeAnimatorStateMachine37622.AddState("Bow Dodge Right Front", new Vector3(384f, 120f, 0f));
				bowDodgeRightFrontAnimatorState35158.motion = bowDodgeRightFrontAnimationClip37682;
				bowDodgeRightFrontAnimatorState35158.cycleOffset = 0f;
				bowDodgeRightFrontAnimatorState35158.cycleOffsetParameterActive = false;
				bowDodgeRightFrontAnimatorState35158.iKOnFeet = true;
				bowDodgeRightFrontAnimatorState35158.mirror = false;
				bowDodgeRightFrontAnimatorState35158.mirrorParameterActive = false;
				bowDodgeRightFrontAnimatorState35158.speed = 1f;
				bowDodgeRightFrontAnimatorState35158.speedParameterActive = false;
				bowDodgeRightFrontAnimatorState35158.writeDefaultValues = false;

				var bowDodgeLeftFrontAnimatorState35160 = bowDodgeAnimatorStateMachine37622.AddState("Bow Dodge Left Front", new Vector3(384f, 0f, 0f));
				bowDodgeLeftFrontAnimatorState35160.motion = bowDodgeLeftFrontAnimationClip37686;
				bowDodgeLeftFrontAnimatorState35160.cycleOffset = 0f;
				bowDodgeLeftFrontAnimatorState35160.cycleOffsetParameterActive = false;
				bowDodgeLeftFrontAnimatorState35160.iKOnFeet = true;
				bowDodgeLeftFrontAnimatorState35160.mirror = false;
				bowDodgeLeftFrontAnimatorState35160.mirrorParameterActive = false;
				bowDodgeLeftFrontAnimatorState35160.speed = 1f;
				bowDodgeLeftFrontAnimatorState35160.speedParameterActive = false;
				bowDodgeLeftFrontAnimatorState35160.writeDefaultValues = false;

				var bowDodgeLeftBackAnimatorState35162 = bowDodgeAnimatorStateMachine37622.AddState("Bow Dodge Left Back", new Vector3(384f, 60f, 0f));
				bowDodgeLeftBackAnimatorState35162.motion = bowDodgeLeftBackAnimationClip37690;
				bowDodgeLeftBackAnimatorState35162.cycleOffset = 0f;
				bowDodgeLeftBackAnimatorState35162.cycleOffsetParameterActive = false;
				bowDodgeLeftBackAnimatorState35162.iKOnFeet = true;
				bowDodgeLeftBackAnimatorState35162.mirror = false;
				bowDodgeLeftBackAnimatorState35162.mirrorParameterActive = false;
				bowDodgeLeftBackAnimatorState35162.speed = 1f;
				bowDodgeLeftBackAnimatorState35162.speedParameterActive = false;
				bowDodgeLeftBackAnimatorState35162.writeDefaultValues = false;

				var bowDodgeRightBackAnimatorState35164 = bowDodgeAnimatorStateMachine37622.AddState("Bow Dodge Right Back", new Vector3(384f, 180f, 0f));
				bowDodgeRightBackAnimatorState35164.motion = bowDodgeRightBackAnimationClip37694;
				bowDodgeRightBackAnimatorState35164.cycleOffset = 0f;
				bowDodgeRightBackAnimatorState35164.cycleOffsetParameterActive = false;
				bowDodgeRightBackAnimatorState35164.iKOnFeet = true;
				bowDodgeRightBackAnimatorState35164.mirror = false;
				bowDodgeRightBackAnimatorState35164.mirrorParameterActive = false;
				bowDodgeRightBackAnimatorState35164.speed = 1f;
				bowDodgeRightBackAnimatorState35164.speedParameterActive = false;
				bowDodgeRightBackAnimatorState35164.writeDefaultValues = false;

				// State Machine Defaults.
				bowDodgeAnimatorStateMachine37622.anyStatePosition = new Vector3(48f, 48f, 0f);
				bowDodgeAnimatorStateMachine37622.defaultState = bowDodgeForwardAnimatorState35156;
				bowDodgeAnimatorStateMachine37622.entryPosition = new Vector3(48f, 0f, 0f);
				bowDodgeAnimatorStateMachine37622.exitPosition = new Vector3(800f, 120f, 0f);
				bowDodgeAnimatorStateMachine37622.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Machine Defaults.
				dodgeAnimatorStateMachine34266.anyStatePosition = new Vector3(50f, 20f, 0f);
				dodgeAnimatorStateMachine34266.defaultState = dodgeForwardAnimatorState35146;
				dodgeAnimatorStateMachine34266.entryPosition = new Vector3(50f, 120f, 0f);
				dodgeAnimatorStateMachine34266.exitPosition = new Vector3(800f, 120f, 0f);
				dodgeAnimatorStateMachine34266.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Transitions.
				var animatorStateTransition37624 = meleeDodgeBackwardAnimatorState35130.AddExitTransition();
				animatorStateTransition37624.canTransitionToSelf = true;
				animatorStateTransition37624.duration = 0.1f;
				animatorStateTransition37624.exitTime = 0.75f;
				animatorStateTransition37624.hasExitTime = false;
				animatorStateTransition37624.hasFixedDuration = true;
				animatorStateTransition37624.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37624.offset = 0f;
				animatorStateTransition37624.orderedInterruption = true;
				animatorStateTransition37624.isExit = true;
				animatorStateTransition37624.mute = false;
				animatorStateTransition37624.solo = false;
				animatorStateTransition37624.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37628 = meleeDodgeForwardAnimatorState35132.AddExitTransition();
				animatorStateTransition37628.canTransitionToSelf = true;
				animatorStateTransition37628.duration = 0.1f;
				animatorStateTransition37628.exitTime = 0.95f;
				animatorStateTransition37628.hasExitTime = false;
				animatorStateTransition37628.hasFixedDuration = true;
				animatorStateTransition37628.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37628.offset = 0f;
				animatorStateTransition37628.orderedInterruption = true;
				animatorStateTransition37628.isExit = true;
				animatorStateTransition37628.mute = false;
				animatorStateTransition37628.solo = false;
				animatorStateTransition37628.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37632 = meleeDodgeRightFrontAnimatorState35134.AddExitTransition();
				animatorStateTransition37632.canTransitionToSelf = true;
				animatorStateTransition37632.duration = 0.1f;
				animatorStateTransition37632.exitTime = 0.95f;
				animatorStateTransition37632.hasExitTime = false;
				animatorStateTransition37632.hasFixedDuration = true;
				animatorStateTransition37632.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37632.offset = 0f;
				animatorStateTransition37632.orderedInterruption = true;
				animatorStateTransition37632.isExit = true;
				animatorStateTransition37632.mute = false;
				animatorStateTransition37632.solo = false;
				animatorStateTransition37632.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37636 = meleeDodgeLeftFrontAnimatorState35136.AddExitTransition();
				animatorStateTransition37636.canTransitionToSelf = true;
				animatorStateTransition37636.duration = 0.1f;
				animatorStateTransition37636.exitTime = 0.95f;
				animatorStateTransition37636.hasExitTime = false;
				animatorStateTransition37636.hasFixedDuration = true;
				animatorStateTransition37636.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37636.offset = 0f;
				animatorStateTransition37636.orderedInterruption = true;
				animatorStateTransition37636.isExit = true;
				animatorStateTransition37636.mute = false;
				animatorStateTransition37636.solo = false;
				animatorStateTransition37636.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37640 = meleeDodgeLeftBackAnimatorState35138.AddExitTransition();
				animatorStateTransition37640.canTransitionToSelf = true;
				animatorStateTransition37640.duration = 0.1f;
				animatorStateTransition37640.exitTime = 0.95f;
				animatorStateTransition37640.hasExitTime = false;
				animatorStateTransition37640.hasFixedDuration = true;
				animatorStateTransition37640.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37640.offset = 0f;
				animatorStateTransition37640.orderedInterruption = true;
				animatorStateTransition37640.isExit = true;
				animatorStateTransition37640.mute = false;
				animatorStateTransition37640.solo = false;
				animatorStateTransition37640.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37644 = meleeDodgeRightBackAnimatorState35140.AddExitTransition();
				animatorStateTransition37644.canTransitionToSelf = true;
				animatorStateTransition37644.duration = 0.1f;
				animatorStateTransition37644.exitTime = 0.95f;
				animatorStateTransition37644.hasExitTime = false;
				animatorStateTransition37644.hasFixedDuration = true;
				animatorStateTransition37644.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37644.offset = 0f;
				animatorStateTransition37644.orderedInterruption = true;
				animatorStateTransition37644.isExit = true;
				animatorStateTransition37644.mute = false;
				animatorStateTransition37644.solo = false;
				animatorStateTransition37644.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				// State Transitions.
				var animatorStateTransition37648 = dodgeLeftFrontAnimatorState35142.AddExitTransition();
				animatorStateTransition37648.canTransitionToSelf = true;
				animatorStateTransition37648.duration = 0.1f;
				animatorStateTransition37648.exitTime = 0.95f;
				animatorStateTransition37648.hasExitTime = false;
				animatorStateTransition37648.hasFixedDuration = true;
				animatorStateTransition37648.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37648.offset = 0f;
				animatorStateTransition37648.orderedInterruption = true;
				animatorStateTransition37648.isExit = true;
				animatorStateTransition37648.mute = false;
				animatorStateTransition37648.solo = false;
				animatorStateTransition37648.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37652 = dodgeRightFrontAnimatorState35144.AddExitTransition();
				animatorStateTransition37652.canTransitionToSelf = true;
				animatorStateTransition37652.duration = 0.1f;
				animatorStateTransition37652.exitTime = 0.95f;
				animatorStateTransition37652.hasExitTime = false;
				animatorStateTransition37652.hasFixedDuration = true;
				animatorStateTransition37652.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37652.offset = 0f;
				animatorStateTransition37652.orderedInterruption = true;
				animatorStateTransition37652.isExit = true;
				animatorStateTransition37652.mute = false;
				animatorStateTransition37652.solo = false;
				animatorStateTransition37652.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37656 = dodgeForwardAnimatorState35146.AddExitTransition();
				animatorStateTransition37656.canTransitionToSelf = true;
				animatorStateTransition37656.duration = 0.1f;
				animatorStateTransition37656.exitTime = 0.95f;
				animatorStateTransition37656.hasExitTime = false;
				animatorStateTransition37656.hasFixedDuration = true;
				animatorStateTransition37656.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37656.offset = 0f;
				animatorStateTransition37656.orderedInterruption = true;
				animatorStateTransition37656.isExit = true;
				animatorStateTransition37656.mute = false;
				animatorStateTransition37656.solo = false;
				animatorStateTransition37656.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37660 = dodgeBackwardAnimatorState35148.AddExitTransition();
				animatorStateTransition37660.canTransitionToSelf = true;
				animatorStateTransition37660.duration = 0.1f;
				animatorStateTransition37660.exitTime = 0.75f;
				animatorStateTransition37660.hasExitTime = false;
				animatorStateTransition37660.hasFixedDuration = true;
				animatorStateTransition37660.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37660.offset = 0f;
				animatorStateTransition37660.orderedInterruption = true;
				animatorStateTransition37660.isExit = true;
				animatorStateTransition37660.mute = false;
				animatorStateTransition37660.solo = false;
				animatorStateTransition37660.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37664 = dodgeRightBackAnimatorState35150.AddExitTransition();
				animatorStateTransition37664.canTransitionToSelf = true;
				animatorStateTransition37664.duration = 0.1f;
				animatorStateTransition37664.exitTime = 0.95f;
				animatorStateTransition37664.hasExitTime = false;
				animatorStateTransition37664.hasFixedDuration = true;
				animatorStateTransition37664.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37664.offset = 0f;
				animatorStateTransition37664.orderedInterruption = true;
				animatorStateTransition37664.isExit = true;
				animatorStateTransition37664.mute = false;
				animatorStateTransition37664.solo = false;
				animatorStateTransition37664.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37668 = dodgeLeftBackAnimatorState35152.AddExitTransition();
				animatorStateTransition37668.canTransitionToSelf = true;
				animatorStateTransition37668.duration = 0.1f;
				animatorStateTransition37668.exitTime = 0.95f;
				animatorStateTransition37668.hasExitTime = false;
				animatorStateTransition37668.hasFixedDuration = true;
				animatorStateTransition37668.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37668.offset = 0f;
				animatorStateTransition37668.orderedInterruption = true;
				animatorStateTransition37668.isExit = true;
				animatorStateTransition37668.mute = false;
				animatorStateTransition37668.solo = false;
				animatorStateTransition37668.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				// State Transitions.
				var animatorStateTransition37672 = bowDodgeBackwardAnimatorState35154.AddExitTransition();
				animatorStateTransition37672.canTransitionToSelf = true;
				animatorStateTransition37672.duration = 0.1f;
				animatorStateTransition37672.exitTime = 0.75f;
				animatorStateTransition37672.hasExitTime = false;
				animatorStateTransition37672.hasFixedDuration = true;
				animatorStateTransition37672.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37672.offset = 0f;
				animatorStateTransition37672.orderedInterruption = true;
				animatorStateTransition37672.isExit = true;
				animatorStateTransition37672.mute = false;
				animatorStateTransition37672.solo = false;
				animatorStateTransition37672.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37676 = bowDodgeForwardAnimatorState35156.AddExitTransition();
				animatorStateTransition37676.canTransitionToSelf = true;
				animatorStateTransition37676.duration = 0.1f;
				animatorStateTransition37676.exitTime = 0.95f;
				animatorStateTransition37676.hasExitTime = false;
				animatorStateTransition37676.hasFixedDuration = true;
				animatorStateTransition37676.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37676.offset = 0f;
				animatorStateTransition37676.orderedInterruption = true;
				animatorStateTransition37676.isExit = true;
				animatorStateTransition37676.mute = false;
				animatorStateTransition37676.solo = false;
				animatorStateTransition37676.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37680 = bowDodgeRightFrontAnimatorState35158.AddExitTransition();
				animatorStateTransition37680.canTransitionToSelf = true;
				animatorStateTransition37680.duration = 0.1f;
				animatorStateTransition37680.exitTime = 0.95f;
				animatorStateTransition37680.hasExitTime = false;
				animatorStateTransition37680.hasFixedDuration = true;
				animatorStateTransition37680.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37680.offset = 0f;
				animatorStateTransition37680.orderedInterruption = true;
				animatorStateTransition37680.isExit = true;
				animatorStateTransition37680.mute = false;
				animatorStateTransition37680.solo = false;
				animatorStateTransition37680.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37684 = bowDodgeLeftFrontAnimatorState35160.AddExitTransition();
				animatorStateTransition37684.canTransitionToSelf = true;
				animatorStateTransition37684.duration = 0.1f;
				animatorStateTransition37684.exitTime = 0.95f;
				animatorStateTransition37684.hasExitTime = false;
				animatorStateTransition37684.hasFixedDuration = true;
				animatorStateTransition37684.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37684.offset = 0f;
				animatorStateTransition37684.orderedInterruption = true;
				animatorStateTransition37684.isExit = true;
				animatorStateTransition37684.mute = false;
				animatorStateTransition37684.solo = false;
				animatorStateTransition37684.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37688 = bowDodgeLeftBackAnimatorState35162.AddExitTransition();
				animatorStateTransition37688.canTransitionToSelf = true;
				animatorStateTransition37688.duration = 0.1f;
				animatorStateTransition37688.exitTime = 0.95f;
				animatorStateTransition37688.hasExitTime = false;
				animatorStateTransition37688.hasFixedDuration = true;
				animatorStateTransition37688.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37688.offset = 0f;
				animatorStateTransition37688.orderedInterruption = true;
				animatorStateTransition37688.isExit = true;
				animatorStateTransition37688.mute = false;
				animatorStateTransition37688.solo = false;
				animatorStateTransition37688.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				var animatorStateTransition37692 = bowDodgeRightBackAnimatorState35164.AddExitTransition();
				animatorStateTransition37692.canTransitionToSelf = true;
				animatorStateTransition37692.duration = 0.1f;
				animatorStateTransition37692.exitTime = 0.95f;
				animatorStateTransition37692.hasExitTime = false;
				animatorStateTransition37692.hasFixedDuration = true;
				animatorStateTransition37692.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37692.offset = 0f;
				animatorStateTransition37692.orderedInterruption = true;
				animatorStateTransition37692.isExit = true;
				animatorStateTransition37692.mute = false;
				animatorStateTransition37692.solo = false;
				animatorStateTransition37692.AddCondition(AnimatorConditionMode.NotEqual, 101f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition34804 = baseStateMachine275562594.AddAnyStateTransition(meleeDodgeBackwardAnimatorState35130);
				animatorStateTransition34804.canTransitionToSelf = false;
				animatorStateTransition34804.duration = 0.1f;
				animatorStateTransition34804.exitTime = 0.75f;
				animatorStateTransition34804.hasExitTime = false;
				animatorStateTransition34804.hasFixedDuration = true;
				animatorStateTransition34804.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34804.offset = 0f;
				animatorStateTransition34804.orderedInterruption = true;
				animatorStateTransition34804.isExit = false;
				animatorStateTransition34804.mute = false;
				animatorStateTransition34804.solo = false;
				animatorStateTransition34804.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34804.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34804.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition34804.AddCondition(AnimatorConditionMode.Equals, 1f, "MovementSetID");

				var animatorStateTransition34806 = baseStateMachine275562594.AddAnyStateTransition(meleeDodgeForwardAnimatorState35132);
				animatorStateTransition34806.canTransitionToSelf = false;
				animatorStateTransition34806.duration = 0.1f;
				animatorStateTransition34806.exitTime = 0.75f;
				animatorStateTransition34806.hasExitTime = false;
				animatorStateTransition34806.hasFixedDuration = true;
				animatorStateTransition34806.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34806.offset = 0f;
				animatorStateTransition34806.orderedInterruption = true;
				animatorStateTransition34806.isExit = false;
				animatorStateTransition34806.mute = false;
				animatorStateTransition34806.solo = false;
				animatorStateTransition34806.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34806.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34806.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition34806.AddCondition(AnimatorConditionMode.Equals, 1f, "MovementSetID");

				var animatorStateTransition34808 = baseStateMachine275562594.AddAnyStateTransition(meleeDodgeRightFrontAnimatorState35134);
				animatorStateTransition34808.canTransitionToSelf = false;
				animatorStateTransition34808.duration = 0.1f;
				animatorStateTransition34808.exitTime = 0.75f;
				animatorStateTransition34808.hasExitTime = false;
				animatorStateTransition34808.hasFixedDuration = true;
				animatorStateTransition34808.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34808.offset = 0f;
				animatorStateTransition34808.orderedInterruption = true;
				animatorStateTransition34808.isExit = false;
				animatorStateTransition34808.mute = false;
				animatorStateTransition34808.solo = false;
				animatorStateTransition34808.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34808.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34808.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34808.AddCondition(AnimatorConditionMode.Equals, 1f, "MovementSetID");
				animatorStateTransition34808.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition34810 = baseStateMachine275562594.AddAnyStateTransition(meleeDodgeLeftFrontAnimatorState35136);
				animatorStateTransition34810.canTransitionToSelf = false;
				animatorStateTransition34810.duration = 0.1f;
				animatorStateTransition34810.exitTime = 0.75f;
				animatorStateTransition34810.hasExitTime = false;
				animatorStateTransition34810.hasFixedDuration = true;
				animatorStateTransition34810.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34810.offset = 0f;
				animatorStateTransition34810.orderedInterruption = true;
				animatorStateTransition34810.isExit = false;
				animatorStateTransition34810.mute = false;
				animatorStateTransition34810.solo = false;
				animatorStateTransition34810.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34810.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34810.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34810.AddCondition(AnimatorConditionMode.Equals, 1f, "MovementSetID");
				animatorStateTransition34810.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition34812 = baseStateMachine275562594.AddAnyStateTransition(meleeDodgeLeftBackAnimatorState35138);
				animatorStateTransition34812.canTransitionToSelf = false;
				animatorStateTransition34812.duration = 0.1f;
				animatorStateTransition34812.exitTime = 0.75f;
				animatorStateTransition34812.hasExitTime = false;
				animatorStateTransition34812.hasFixedDuration = true;
				animatorStateTransition34812.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34812.offset = 0f;
				animatorStateTransition34812.orderedInterruption = true;
				animatorStateTransition34812.isExit = false;
				animatorStateTransition34812.mute = false;
				animatorStateTransition34812.solo = false;
				animatorStateTransition34812.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34812.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34812.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34812.AddCondition(AnimatorConditionMode.Equals, 1f, "MovementSetID");
				animatorStateTransition34812.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");

				var animatorStateTransition34814 = baseStateMachine275562594.AddAnyStateTransition(meleeDodgeRightBackAnimatorState35140);
				animatorStateTransition34814.canTransitionToSelf = false;
				animatorStateTransition34814.duration = 0.1f;
				animatorStateTransition34814.exitTime = 0.75f;
				animatorStateTransition34814.hasExitTime = false;
				animatorStateTransition34814.hasFixedDuration = true;
				animatorStateTransition34814.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34814.offset = 0f;
				animatorStateTransition34814.orderedInterruption = true;
				animatorStateTransition34814.isExit = false;
				animatorStateTransition34814.mute = false;
				animatorStateTransition34814.solo = false;
				animatorStateTransition34814.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34814.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34814.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34814.AddCondition(AnimatorConditionMode.Equals, 1f, "MovementSetID");
				animatorStateTransition34814.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");

				var animatorStateTransition34816 = baseStateMachine275562594.AddAnyStateTransition(dodgeLeftFrontAnimatorState35142);
				animatorStateTransition34816.canTransitionToSelf = false;
				animatorStateTransition34816.duration = 0.1f;
				animatorStateTransition34816.exitTime = 0.75f;
				animatorStateTransition34816.hasExitTime = false;
				animatorStateTransition34816.hasFixedDuration = true;
				animatorStateTransition34816.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34816.offset = 0f;
				animatorStateTransition34816.orderedInterruption = true;
				animatorStateTransition34816.isExit = false;
				animatorStateTransition34816.mute = false;
				animatorStateTransition34816.solo = false;
				animatorStateTransition34816.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34816.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34816.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34816.AddCondition(AnimatorConditionMode.Equals, 0f, "MovementSetID");
				animatorStateTransition34816.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition34818 = baseStateMachine275562594.AddAnyStateTransition(dodgeRightFrontAnimatorState35144);
				animatorStateTransition34818.canTransitionToSelf = false;
				animatorStateTransition34818.duration = 0.1f;
				animatorStateTransition34818.exitTime = 0.75f;
				animatorStateTransition34818.hasExitTime = false;
				animatorStateTransition34818.hasFixedDuration = true;
				animatorStateTransition34818.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34818.offset = 0f;
				animatorStateTransition34818.orderedInterruption = true;
				animatorStateTransition34818.isExit = false;
				animatorStateTransition34818.mute = false;
				animatorStateTransition34818.solo = false;
				animatorStateTransition34818.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34818.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34818.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34818.AddCondition(AnimatorConditionMode.Equals, 0f, "MovementSetID");
				animatorStateTransition34818.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition34820 = baseStateMachine275562594.AddAnyStateTransition(dodgeForwardAnimatorState35146);
				animatorStateTransition34820.canTransitionToSelf = false;
				animatorStateTransition34820.duration = 0.1f;
				animatorStateTransition34820.exitTime = 0.75f;
				animatorStateTransition34820.hasExitTime = false;
				animatorStateTransition34820.hasFixedDuration = true;
				animatorStateTransition34820.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34820.offset = 0f;
				animatorStateTransition34820.orderedInterruption = true;
				animatorStateTransition34820.isExit = false;
				animatorStateTransition34820.mute = false;
				animatorStateTransition34820.solo = false;
				animatorStateTransition34820.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34820.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34820.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition34820.AddCondition(AnimatorConditionMode.Equals, 0f, "MovementSetID");

				var animatorStateTransition34822 = baseStateMachine275562594.AddAnyStateTransition(dodgeBackwardAnimatorState35148);
				animatorStateTransition34822.canTransitionToSelf = false;
				animatorStateTransition34822.duration = 0.1f;
				animatorStateTransition34822.exitTime = 0.75f;
				animatorStateTransition34822.hasExitTime = false;
				animatorStateTransition34822.hasFixedDuration = true;
				animatorStateTransition34822.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34822.offset = 0f;
				animatorStateTransition34822.orderedInterruption = true;
				animatorStateTransition34822.isExit = false;
				animatorStateTransition34822.mute = false;
				animatorStateTransition34822.solo = false;
				animatorStateTransition34822.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34822.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34822.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition34822.AddCondition(AnimatorConditionMode.Equals, 0f, "MovementSetID");

				var animatorStateTransition34824 = baseStateMachine275562594.AddAnyStateTransition(dodgeRightBackAnimatorState35150);
				animatorStateTransition34824.canTransitionToSelf = false;
				animatorStateTransition34824.duration = 0.1f;
				animatorStateTransition34824.exitTime = 0.75f;
				animatorStateTransition34824.hasExitTime = false;
				animatorStateTransition34824.hasFixedDuration = true;
				animatorStateTransition34824.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34824.offset = 0f;
				animatorStateTransition34824.orderedInterruption = true;
				animatorStateTransition34824.isExit = false;
				animatorStateTransition34824.mute = false;
				animatorStateTransition34824.solo = false;
				animatorStateTransition34824.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34824.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34824.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34824.AddCondition(AnimatorConditionMode.Equals, 0f, "MovementSetID");
				animatorStateTransition34824.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");

				var animatorStateTransition34826 = baseStateMachine275562594.AddAnyStateTransition(dodgeLeftBackAnimatorState35152);
				animatorStateTransition34826.canTransitionToSelf = false;
				animatorStateTransition34826.duration = 0.1f;
				animatorStateTransition34826.exitTime = 0.75f;
				animatorStateTransition34826.hasExitTime = false;
				animatorStateTransition34826.hasFixedDuration = true;
				animatorStateTransition34826.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34826.offset = 0f;
				animatorStateTransition34826.orderedInterruption = true;
				animatorStateTransition34826.isExit = false;
				animatorStateTransition34826.mute = false;
				animatorStateTransition34826.solo = false;
				animatorStateTransition34826.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34826.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34826.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34826.AddCondition(AnimatorConditionMode.Equals, 0f, "MovementSetID");
				animatorStateTransition34826.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");

				var animatorStateTransition34828 = baseStateMachine275562594.AddAnyStateTransition(bowDodgeBackwardAnimatorState35154);
				animatorStateTransition34828.canTransitionToSelf = false;
				animatorStateTransition34828.duration = 0.1f;
				animatorStateTransition34828.exitTime = 0.75f;
				animatorStateTransition34828.hasExitTime = false;
				animatorStateTransition34828.hasFixedDuration = true;
				animatorStateTransition34828.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34828.offset = 0f;
				animatorStateTransition34828.orderedInterruption = true;
				animatorStateTransition34828.isExit = false;
				animatorStateTransition34828.mute = false;
				animatorStateTransition34828.solo = false;
				animatorStateTransition34828.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34828.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34828.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition34828.AddCondition(AnimatorConditionMode.Equals, 2f, "MovementSetID");

				var animatorStateTransition34830 = baseStateMachine275562594.AddAnyStateTransition(bowDodgeForwardAnimatorState35156);
				animatorStateTransition34830.canTransitionToSelf = false;
				animatorStateTransition34830.duration = 0.1f;
				animatorStateTransition34830.exitTime = 0.75f;
				animatorStateTransition34830.hasExitTime = false;
				animatorStateTransition34830.hasFixedDuration = true;
				animatorStateTransition34830.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34830.offset = 0f;
				animatorStateTransition34830.orderedInterruption = true;
				animatorStateTransition34830.isExit = false;
				animatorStateTransition34830.mute = false;
				animatorStateTransition34830.solo = false;
				animatorStateTransition34830.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34830.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34830.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition34830.AddCondition(AnimatorConditionMode.Equals, 2f, "MovementSetID");

				var animatorStateTransition34832 = baseStateMachine275562594.AddAnyStateTransition(bowDodgeRightFrontAnimatorState35158);
				animatorStateTransition34832.canTransitionToSelf = false;
				animatorStateTransition34832.duration = 0.1f;
				animatorStateTransition34832.exitTime = 0.75f;
				animatorStateTransition34832.hasExitTime = false;
				animatorStateTransition34832.hasFixedDuration = true;
				animatorStateTransition34832.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34832.offset = 0f;
				animatorStateTransition34832.orderedInterruption = true;
				animatorStateTransition34832.isExit = false;
				animatorStateTransition34832.mute = false;
				animatorStateTransition34832.solo = false;
				animatorStateTransition34832.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34832.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34832.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34832.AddCondition(AnimatorConditionMode.Equals, 2f, "MovementSetID");
				animatorStateTransition34832.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition34834 = baseStateMachine275562594.AddAnyStateTransition(bowDodgeLeftFrontAnimatorState35160);
				animatorStateTransition34834.canTransitionToSelf = false;
				animatorStateTransition34834.duration = 0.1f;
				animatorStateTransition34834.exitTime = 0.75f;
				animatorStateTransition34834.hasExitTime = false;
				animatorStateTransition34834.hasFixedDuration = true;
				animatorStateTransition34834.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34834.offset = 0f;
				animatorStateTransition34834.orderedInterruption = true;
				animatorStateTransition34834.isExit = false;
				animatorStateTransition34834.mute = false;
				animatorStateTransition34834.solo = false;
				animatorStateTransition34834.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34834.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34834.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34834.AddCondition(AnimatorConditionMode.Equals, 2f, "MovementSetID");
				animatorStateTransition34834.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition34836 = baseStateMachine275562594.AddAnyStateTransition(bowDodgeLeftBackAnimatorState35162);
				animatorStateTransition34836.canTransitionToSelf = false;
				animatorStateTransition34836.duration = 0.1f;
				animatorStateTransition34836.exitTime = 0.75f;
				animatorStateTransition34836.hasExitTime = false;
				animatorStateTransition34836.hasFixedDuration = true;
				animatorStateTransition34836.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34836.offset = 0f;
				animatorStateTransition34836.orderedInterruption = true;
				animatorStateTransition34836.isExit = false;
				animatorStateTransition34836.mute = false;
				animatorStateTransition34836.solo = false;
				animatorStateTransition34836.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34836.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34836.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");
				animatorStateTransition34836.AddCondition(AnimatorConditionMode.Equals, 2f, "MovementSetID");
				animatorStateTransition34836.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");

				var animatorStateTransition34838 = baseStateMachine275562594.AddAnyStateTransition(bowDodgeRightBackAnimatorState35164);
				animatorStateTransition34838.canTransitionToSelf = false;
				animatorStateTransition34838.duration = 0.1f;
				animatorStateTransition34838.exitTime = 0.75f;
				animatorStateTransition34838.hasExitTime = false;
				animatorStateTransition34838.hasFixedDuration = true;
				animatorStateTransition34838.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34838.offset = 0f;
				animatorStateTransition34838.orderedInterruption = true;
				animatorStateTransition34838.isExit = false;
				animatorStateTransition34838.mute = false;
				animatorStateTransition34838.solo = false;
				animatorStateTransition34838.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34838.AddCondition(AnimatorConditionMode.Equals, 101f, "AbilityIndex");
				animatorStateTransition34838.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");
				animatorStateTransition34838.AddCondition(AnimatorConditionMode.Equals, 2f, "MovementSetID");
				animatorStateTransition34838.AddCondition(AnimatorConditionMode.Less, 0.499f, "LegIndex");
			}
		}
	}
}
