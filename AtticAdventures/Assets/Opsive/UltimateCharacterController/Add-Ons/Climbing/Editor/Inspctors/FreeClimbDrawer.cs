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
	/// Draws a custom inspector for the FreeClimb Ability.
	/// </summary>
	[ControlType(typeof(Opsive.UltimateCharacterController.AddOns.Climbing.FreeClimb))]
	public class FreeClimbDrawer : DetectObjectAbilityBaseDrawer
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

				var baseStateMachine1807513540 = animatorControllers[i].layers[12].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine1807513540.stateMachines.Length; ++k) {
						if (baseStateMachine1807513540.stateMachines[k].stateMachine.name == "Free Climb") {
							baseStateMachine1807513540.RemoveStateMachine(baseStateMachine1807513540.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var freeClimbBottomMountAnimationClip56248Path = AssetDatabase.GUIDToAssetPath("8dc308b940751ce4793ad64a943730c1"); 
				var freeClimbBottomMountAnimationClip56248 = AnimatorBuilder.GetAnimationClip(freeClimbBottomMountAnimationClip56248Path, "FreeClimbBottomMount");
				var freeClimbTopMountAnimationClip56254Path = AssetDatabase.GUIDToAssetPath("594f727febd17b043894b835045c039e"); 
				var freeClimbTopMountAnimationClip56254 = AnimatorBuilder.GetAnimationClip(freeClimbTopMountAnimationClip56254Path, "FreeClimbTopMount");
				var freeClimbLeftDiagonalUpAnimationClip56282Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbLeftDiagonalUpAnimationClip56282 = AnimatorBuilder.GetAnimationClip(freeClimbLeftDiagonalUpAnimationClip56282Path, "FreeClimbLeftDiagonalUp");
				var freeClimbRightDiagonalUpAnimationClip56284Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbRightDiagonalUpAnimationClip56284 = AnimatorBuilder.GetAnimationClip(freeClimbRightDiagonalUpAnimationClip56284Path, "FreeClimbRightDiagonalUp");
				var freeClimbUpAnimationClip56286Path = AssetDatabase.GUIDToAssetPath("ddeba2ae54198e040b5f752b0da3be7e"); 
				var freeClimbUpAnimationClip56286 = AnimatorBuilder.GetAnimationClip(freeClimbUpAnimationClip56286Path, "FreeClimbUp");
				var freeClimbDownAnimationClip56288Path = AssetDatabase.GUIDToAssetPath("ddeba2ae54198e040b5f752b0da3be7e"); 
				var freeClimbDownAnimationClip56288 = AnimatorBuilder.GetAnimationClip(freeClimbDownAnimationClip56288Path, "FreeClimbDown");
				var freeClimbLeftDiagonalDownAnimationClip56290Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbLeftDiagonalDownAnimationClip56290 = AnimatorBuilder.GetAnimationClip(freeClimbLeftDiagonalDownAnimationClip56290Path, "FreeClimbLeftDiagonalDown");
				var freeClimbRightDiagonalDownAnimationClip56292Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbRightDiagonalDownAnimationClip56292 = AnimatorBuilder.GetAnimationClip(freeClimbRightDiagonalDownAnimationClip56292Path, "FreeClimbRightDiagonalDown");
				var freeClimbLeftJumpAnimationClip56314Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbLeftJumpAnimationClip56314 = AnimatorBuilder.GetAnimationClip(freeClimbLeftJumpAnimationClip56314Path, "FreeClimbLeftJump");
				var freeClimbLeftAnimationClip56316Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbLeftAnimationClip56316 = AnimatorBuilder.GetAnimationClip(freeClimbLeftAnimationClip56316Path, "FreeClimbLeft");
				var freeClimbIdleAnimationClip56318Path = AssetDatabase.GUIDToAssetPath("64f8f5acd8a248049bab290095ab89cb"); 
				var freeClimbIdleAnimationClip56318 = AnimatorBuilder.GetAnimationClip(freeClimbIdleAnimationClip56318Path, "FreeClimbIdle");
				var freeClimbRightAnimationClip56320Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbRightAnimationClip56320 = AnimatorBuilder.GetAnimationClip(freeClimbRightAnimationClip56320Path, "FreeClimbRight");
				var freeClimbRightJumpAnimationClip56322Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbRightJumpAnimationClip56322 = AnimatorBuilder.GetAnimationClip(freeClimbRightJumpAnimationClip56322Path, "FreeClimbRightJump");
				var freeClimbBottomDismountAnimationClip56326Path = AssetDatabase.GUIDToAssetPath("48bf46b5af3fb8048b881434cf6bd2b4"); 
				var freeClimbBottomDismountAnimationClip56326 = AnimatorBuilder.GetAnimationClip(freeClimbBottomDismountAnimationClip56326Path, "FreeClimbBottomDismount");
				var freeClimbTopDismountAnimationClip56330Path = AssetDatabase.GUIDToAssetPath("c6ee3468f2225ea439bafc3bc61eabb5"); 
				var freeClimbTopDismountAnimationClip56330 = AnimatorBuilder.GetAnimationClip(freeClimbTopDismountAnimationClip56330Path, "FreeClimbTopDismount");
				var freeClimbInnerRightTurnAnimationClip56338Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbInnerRightTurnAnimationClip56338 = AnimatorBuilder.GetAnimationClip(freeClimbInnerRightTurnAnimationClip56338Path, "FreeClimbInnerRightTurn");
				var freeClimbOuterRightTurnAnimationClip56346Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbOuterRightTurnAnimationClip56346 = AnimatorBuilder.GetAnimationClip(freeClimbOuterRightTurnAnimationClip56346Path, "FreeClimbOuterRightTurn");
				var freeClimbOuterLeftTurnAnimationClip56354Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbOuterLeftTurnAnimationClip56354 = AnimatorBuilder.GetAnimationClip(freeClimbOuterLeftTurnAnimationClip56354Path, "FreeClimbOuterLeftTurn");
				var freeClimbInnerLeftTurnAnimationClip56362Path = AssetDatabase.GUIDToAssetPath("513fbf4a8043fcf41b0b8a794c099c40"); 
				var freeClimbInnerLeftTurnAnimationClip56362 = AnimatorBuilder.GetAnimationClip(freeClimbInnerLeftTurnAnimationClip56362Path, "FreeClimbInnerLeftTurn");
				var freeClimbLeftDiagonalJumpUpLeftAnimationClip56368Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbLeftDiagonalJumpUpLeftAnimationClip56368 = AnimatorBuilder.GetAnimationClip(freeClimbLeftDiagonalJumpUpLeftAnimationClip56368Path, "FreeClimbLeftDiagonalJumpUpLeft");
				var freeClimbJumpUpLeftAnimationClip56370Path = AssetDatabase.GUIDToAssetPath("ddeba2ae54198e040b5f752b0da3be7e"); 
				var freeClimbJumpUpLeftAnimationClip56370 = AnimatorBuilder.GetAnimationClip(freeClimbJumpUpLeftAnimationClip56370Path, "FreeClimbJumpUpLeft");
				var freeClimbRightDiagonalJumpUpLeftAnimationClip56372Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbRightDiagonalJumpUpLeftAnimationClip56372 = AnimatorBuilder.GetAnimationClip(freeClimbRightDiagonalJumpUpLeftAnimationClip56372Path, "FreeClimbRightDiagonalJumpUpLeft");
				var freeClimbLeftDiagonalJumpUpRightAnimationClip56378Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbLeftDiagonalJumpUpRightAnimationClip56378 = AnimatorBuilder.GetAnimationClip(freeClimbLeftDiagonalJumpUpRightAnimationClip56378Path, "FreeClimbLeftDiagonalJumpUpRight");
				var freeClimbJumpUpRightAnimationClip56380Path = AssetDatabase.GUIDToAssetPath("ddeba2ae54198e040b5f752b0da3be7e"); 
				var freeClimbJumpUpRightAnimationClip56380 = AnimatorBuilder.GetAnimationClip(freeClimbJumpUpRightAnimationClip56380Path, "FreeClimbJumpUpRight");
				var freeClimbRightDiagonalJumpUpRightAnimationClip56382Path = AssetDatabase.GUIDToAssetPath("204c49d338799a44b8762fca301508da"); 
				var freeClimbRightDiagonalJumpUpRightAnimationClip56382 = AnimatorBuilder.GetAnimationClip(freeClimbRightDiagonalJumpUpRightAnimationClip56382Path, "FreeClimbRightDiagonalJumpUpRight");
				var hangtoFreeClimbLeftAnimationClip56394Path = AssetDatabase.GUIDToAssetPath("f42dffbc7133a3c4fbf6085ebb7d7ad5"); 
				var hangtoFreeClimbLeftAnimationClip56394 = AnimatorBuilder.GetAnimationClip(hangtoFreeClimbLeftAnimationClip56394Path, "HangtoFreeClimbLeft");
				var hangtoFreeClimbRightAnimationClip56400Path = AssetDatabase.GUIDToAssetPath("f42dffbc7133a3c4fbf6085ebb7d7ad5"); 
				var hangtoFreeClimbRightAnimationClip56400 = AnimatorBuilder.GetAnimationClip(hangtoFreeClimbRightAnimationClip56400Path, "HangtoFreeClimbRight");
				var hangtoFreeClimbVerticalAnimationClip56406Path = AssetDatabase.GUIDToAssetPath("47cec5d05251a604694ab3c623eb83e0"); 
				var hangtoFreeClimbVerticalAnimationClip56406 = AnimatorBuilder.GetAnimationClip(hangtoFreeClimbVerticalAnimationClip56406Path, "HangtoFreeClimbVertical");

				// State Machine.
				var freeClimbAnimatorStateMachine55652 = baseStateMachine1807513540.AddStateMachine("Free Climb", new Vector3(650f, 500f, 0f));

				// States.
				var freeClimbBottomMountAnimatorState55666 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Bottom Mount", new Vector3(-460f, -270f, 0f));
				freeClimbBottomMountAnimatorState55666.motion = freeClimbBottomMountAnimationClip56248;
				freeClimbBottomMountAnimatorState55666.cycleOffset = 0f;
				freeClimbBottomMountAnimatorState55666.cycleOffsetParameterActive = false;
				freeClimbBottomMountAnimatorState55666.iKOnFeet = false;
				freeClimbBottomMountAnimatorState55666.mirror = false;
				freeClimbBottomMountAnimatorState55666.mirrorParameterActive = false;
				freeClimbBottomMountAnimatorState55666.speed = 1f;
				freeClimbBottomMountAnimatorState55666.speedParameterActive = false;
				freeClimbBottomMountAnimatorState55666.writeDefaultValues = true;

				var freeClimbTopMountAnimatorState55668 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Top Mount", new Vector3(-460f, -200f, 0f));
				freeClimbTopMountAnimatorState55668.motion = freeClimbTopMountAnimationClip56254;
				freeClimbTopMountAnimatorState55668.cycleOffset = 0f;
				freeClimbTopMountAnimatorState55668.cycleOffsetParameterActive = false;
				freeClimbTopMountAnimatorState55668.iKOnFeet = false;
				freeClimbTopMountAnimatorState55668.mirror = false;
				freeClimbTopMountAnimatorState55668.mirrorParameterActive = false;
				freeClimbTopMountAnimatorState55668.speed = 1f;
				freeClimbTopMountAnimatorState55668.speedParameterActive = false;
				freeClimbTopMountAnimatorState55668.writeDefaultValues = true;

				var freeClimbVerticalAnimatorState55670 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Vertical", new Vector3(-90f, -120f, 0f));
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280 = new BlendTree();
				AssetDatabase.AddObjectToAsset(freeClimbVerticalAnimatorState55670blendTreeBlendTree56280, animatorControllers[i]);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.hideFlags = HideFlags.HideInHierarchy;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.blendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.blendParameterY = "ForwardMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.blendType = BlendTreeType.FreeformCartesian2D;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.maxThreshold = 0.4340277f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.minThreshold = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.name = "Blend Tree";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.useAutomaticThresholds = true;
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0 =  new ChildMotion();
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.motion = freeClimbLeftDiagonalUpAnimationClip56282;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.directBlendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.mirror = false;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.position = new Vector2(-1f, 1f);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.threshold = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0.timeScale = 1f;
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1 =  new ChildMotion();
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.motion = freeClimbRightDiagonalUpAnimationClip56284;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.directBlendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.mirror = false;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.position = new Vector2(1f, 1f);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.threshold = 0.08680554f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1.timeScale = 1f;
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2 =  new ChildMotion();
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.motion = freeClimbUpAnimationClip56286;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.directBlendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.mirror = false;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.position = new Vector2(0f, 1f);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.threshold = 0.1736111f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2.timeScale = 1f;
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3 =  new ChildMotion();
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.motion = freeClimbDownAnimationClip56288;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.directBlendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.mirror = false;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.position = new Vector2(0f, -1f);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.threshold = 0.2604166f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3.timeScale = 1f;
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4 =  new ChildMotion();
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.motion = freeClimbLeftDiagonalDownAnimationClip56290;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.directBlendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.mirror = false;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.position = new Vector2(-1f, -1f);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.threshold = 0.3472222f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4.timeScale = 1f;
				var freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5 =  new ChildMotion();
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.motion = freeClimbRightDiagonalDownAnimationClip56292;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.directBlendParameter = "HorizontalMovement";
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.mirror = false;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.position = new Vector2(1f, -1f);
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.threshold = 0.4340277f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5.timeScale = 1f;
				freeClimbVerticalAnimatorState55670blendTreeBlendTree56280.children = new ChildMotion[] {
					freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child0,
					freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child1,
					freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child2,
					freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child3,
					freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child4,
					freeClimbVerticalAnimatorState55670blendTreeBlendTree56280Child5
				};
				freeClimbVerticalAnimatorState55670.motion = freeClimbVerticalAnimatorState55670blendTreeBlendTree56280;
				freeClimbVerticalAnimatorState55670.cycleOffset = 0f;
				freeClimbVerticalAnimatorState55670.cycleOffsetParameterActive = false;
				freeClimbVerticalAnimatorState55670.iKOnFeet = false;
				freeClimbVerticalAnimatorState55670.mirror = false;
				freeClimbVerticalAnimatorState55670.mirrorParameterActive = false;
				freeClimbVerticalAnimatorState55670.speed = 1f;
				freeClimbVerticalAnimatorState55670.speedParameterActive = false;
				freeClimbVerticalAnimatorState55670.writeDefaultValues = true;

				var freeClimbIdleAnimatorState55672 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Idle", new Vector3(-90f, -240f, 0f));
				var freeClimbIdleAnimatorState55672blendTreeBlendTree56312 = new BlendTree();
				AssetDatabase.AddObjectToAsset(freeClimbIdleAnimatorState55672blendTreeBlendTree56312, animatorControllers[i]);
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.hideFlags = HideFlags.HideInHierarchy;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.blendParameter = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.blendParameterY = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.blendType = BlendTreeType.Simple1D;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.maxThreshold = 2f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.minThreshold = -2f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.name = "Blend Tree";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.useAutomaticThresholds = false;
				var freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0 =  new ChildMotion();
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.motion = freeClimbLeftJumpAnimationClip56314;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.cycleOffset = 0f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.directBlendParameter = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.mirror = false;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.position = new Vector2(0f, 0f);
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.threshold = -2f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0.timeScale = 1f;
				var freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1 =  new ChildMotion();
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.motion = freeClimbLeftAnimationClip56316;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.cycleOffset = 0f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.directBlendParameter = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.mirror = false;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.position = new Vector2(0f, 0f);
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.threshold = -1f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1.timeScale = 1f;
				var freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2 =  new ChildMotion();
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.motion = freeClimbIdleAnimationClip56318;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.cycleOffset = 0f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.directBlendParameter = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.mirror = false;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.position = new Vector2(0f, 0f);
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.threshold = 0f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2.timeScale = 1f;
				var freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3 =  new ChildMotion();
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.motion = freeClimbRightAnimationClip56320;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.cycleOffset = 0f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.directBlendParameter = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.mirror = false;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.position = new Vector2(0f, 0f);
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.threshold = 1f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3.timeScale = 1f;
				var freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4 =  new ChildMotion();
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.motion = freeClimbRightJumpAnimationClip56322;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.cycleOffset = 0f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.directBlendParameter = "HorizontalMovement";
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.mirror = false;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.position = new Vector2(0f, 0f);
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.threshold = 2f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4.timeScale = 1f;
				freeClimbIdleAnimatorState55672blendTreeBlendTree56312.children = new ChildMotion[] {
					freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child0,
					freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child1,
					freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child2,
					freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child3,
					freeClimbIdleAnimatorState55672blendTreeBlendTree56312Child4
				};
				freeClimbIdleAnimatorState55672.motion = freeClimbIdleAnimatorState55672blendTreeBlendTree56312;
				freeClimbIdleAnimatorState55672.cycleOffset = 0f;
				freeClimbIdleAnimatorState55672.cycleOffsetParameterActive = false;
				freeClimbIdleAnimatorState55672.iKOnFeet = false;
				freeClimbIdleAnimatorState55672.mirror = false;
				freeClimbIdleAnimatorState55672.mirrorParameterActive = false;
				freeClimbIdleAnimatorState55672.speed = 1f;
				freeClimbIdleAnimatorState55672.speedParameterActive = false;
				freeClimbIdleAnimatorState55672.writeDefaultValues = true;

				var freeClimbBottomDismountAnimatorState55674 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Bottom Dismount", new Vector3(270f, -220f, 0f));
				freeClimbBottomDismountAnimatorState55674.motion = freeClimbBottomDismountAnimationClip56326;
				freeClimbBottomDismountAnimatorState55674.cycleOffset = 0f;
				freeClimbBottomDismountAnimatorState55674.cycleOffsetParameterActive = false;
				freeClimbBottomDismountAnimatorState55674.iKOnFeet = false;
				freeClimbBottomDismountAnimatorState55674.mirror = false;
				freeClimbBottomDismountAnimatorState55674.mirrorParameterActive = false;
				freeClimbBottomDismountAnimatorState55674.speed = 1f;
				freeClimbBottomDismountAnimatorState55674.speedParameterActive = false;
				freeClimbBottomDismountAnimatorState55674.writeDefaultValues = true;

				var freeClimbTopDismountAnimatorState55676 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Top Dismount", new Vector3(270f, -170f, 0f));
				freeClimbTopDismountAnimatorState55676.motion = freeClimbTopDismountAnimationClip56330;
				freeClimbTopDismountAnimatorState55676.cycleOffset = 0f;
				freeClimbTopDismountAnimatorState55676.cycleOffsetParameterActive = false;
				freeClimbTopDismountAnimatorState55676.iKOnFeet = false;
				freeClimbTopDismountAnimatorState55676.mirror = false;
				freeClimbTopDismountAnimatorState55676.mirrorParameterActive = false;
				freeClimbTopDismountAnimatorState55676.speed = 1f;
				freeClimbTopDismountAnimatorState55676.speedParameterActive = false;
				freeClimbTopDismountAnimatorState55676.writeDefaultValues = true;

				var innerRightTurnAnimatorState55678 = freeClimbAnimatorStateMachine55652.AddState("Inner Right Turn", new Vector3(-250f, -450f, 0f));
				innerRightTurnAnimatorState55678.motion = freeClimbInnerRightTurnAnimationClip56338;
				innerRightTurnAnimatorState55678.cycleOffset = 0f;
				innerRightTurnAnimatorState55678.cycleOffsetParameterActive = false;
				innerRightTurnAnimatorState55678.iKOnFeet = false;
				innerRightTurnAnimatorState55678.mirror = false;
				innerRightTurnAnimatorState55678.mirrorParameterActive = false;
				innerRightTurnAnimatorState55678.speed = 1f;
				innerRightTurnAnimatorState55678.speedParameterActive = false;
				innerRightTurnAnimatorState55678.writeDefaultValues = true;

				var outerRightTurnAnimatorState55680 = freeClimbAnimatorStateMachine55652.AddState("Outer Right Turn", new Vector3(20f, -450f, 0f));
				outerRightTurnAnimatorState55680.motion = freeClimbOuterRightTurnAnimationClip56346;
				outerRightTurnAnimatorState55680.cycleOffset = 0f;
				outerRightTurnAnimatorState55680.cycleOffsetParameterActive = false;
				outerRightTurnAnimatorState55680.iKOnFeet = true;
				outerRightTurnAnimatorState55680.mirror = false;
				outerRightTurnAnimatorState55680.mirrorParameterActive = false;
				outerRightTurnAnimatorState55680.speed = 1f;
				outerRightTurnAnimatorState55680.speedParameterActive = false;
				outerRightTurnAnimatorState55680.writeDefaultValues = true;

				var outerLeftTurnAnimatorState55682 = freeClimbAnimatorStateMachine55652.AddState("Outer Left Turn", new Vector3(20f, -510f, 0f));
				outerLeftTurnAnimatorState55682.motion = freeClimbOuterLeftTurnAnimationClip56354;
				outerLeftTurnAnimatorState55682.cycleOffset = 0f;
				outerLeftTurnAnimatorState55682.cycleOffsetParameterActive = false;
				outerLeftTurnAnimatorState55682.iKOnFeet = false;
				outerLeftTurnAnimatorState55682.mirror = false;
				outerLeftTurnAnimatorState55682.mirrorParameterActive = false;
				outerLeftTurnAnimatorState55682.speed = 1f;
				outerLeftTurnAnimatorState55682.speedParameterActive = false;
				outerLeftTurnAnimatorState55682.writeDefaultValues = true;

				var innerLeftTurnAnimatorState55684 = freeClimbAnimatorStateMachine55652.AddState("Inner Left Turn", new Vector3(-250f, -510f, 0f));
				innerLeftTurnAnimatorState55684.motion = freeClimbInnerLeftTurnAnimationClip56362;
				innerLeftTurnAnimatorState55684.cycleOffset = 0f;
				innerLeftTurnAnimatorState55684.cycleOffsetParameterActive = false;
				innerLeftTurnAnimatorState55684.iKOnFeet = false;
				innerLeftTurnAnimatorState55684.mirror = false;
				innerLeftTurnAnimatorState55684.mirrorParameterActive = false;
				innerLeftTurnAnimatorState55684.speed = 1f;
				innerLeftTurnAnimatorState55684.speedParameterActive = false;
				innerLeftTurnAnimatorState55684.writeDefaultValues = true;

				var freeClimbJumpLeftAnimatorState55686 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Jump Left", new Vector3(-210f, 0f, 0f));
				var freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366 = new BlendTree();
				AssetDatabase.AddObjectToAsset(freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366, animatorControllers[i]);
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.hideFlags = HideFlags.HideInHierarchy;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.blendParameter = "HorizontalMovement";
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.blendParameterY = "HorizontalMovement";
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.blendType = BlendTreeType.Simple1D;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.maxThreshold = 1f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.minThreshold = -1f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.name = "Blend Tree";
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.useAutomaticThresholds = false;
				var freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0 =  new ChildMotion();
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.motion = freeClimbLeftDiagonalJumpUpLeftAnimationClip56368;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.cycleOffset = 0f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.directBlendParameter = "HorizontalMovement";
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.mirror = false;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.position = new Vector2(0f, 0f);
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.threshold = -1f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0.timeScale = 1f;
				var freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1 =  new ChildMotion();
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.motion = freeClimbJumpUpLeftAnimationClip56370;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.cycleOffset = 0f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.directBlendParameter = "HorizontalMovement";
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.mirror = false;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.position = new Vector2(0f, 0f);
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.threshold = 0f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1.timeScale = 1f;
				var freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2 =  new ChildMotion();
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.motion = freeClimbRightDiagonalJumpUpLeftAnimationClip56372;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.cycleOffset = 0f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.directBlendParameter = "HorizontalMovement";
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.mirror = false;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.position = new Vector2(0f, 0f);
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.threshold = 1f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2.timeScale = 1f;
				freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366.children = new ChildMotion[] {
					freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child0,
					freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child1,
					freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366Child2
				};
				freeClimbJumpLeftAnimatorState55686.motion = freeClimbJumpLeftAnimatorState55686blendTreeBlendTree56366;
				freeClimbJumpLeftAnimatorState55686.cycleOffset = 0f;
				freeClimbJumpLeftAnimatorState55686.cycleOffsetParameterActive = false;
				freeClimbJumpLeftAnimatorState55686.iKOnFeet = false;
				freeClimbJumpLeftAnimatorState55686.mirror = false;
				freeClimbJumpLeftAnimatorState55686.mirrorParameterActive = false;
				freeClimbJumpLeftAnimatorState55686.speed = 1f;
				freeClimbJumpLeftAnimatorState55686.speedParameterActive = false;
				freeClimbJumpLeftAnimatorState55686.writeDefaultValues = true;

				var freeClimbJumpRightAnimatorState55688 = freeClimbAnimatorStateMachine55652.AddState("Free Climb Jump Right", new Vector3(30f, 0f, 0f));
				var freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376 = new BlendTree();
				AssetDatabase.AddObjectToAsset(freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376, animatorControllers[i]);
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.hideFlags = HideFlags.HideInHierarchy;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.blendParameter = "HorizontalMovement";
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.blendParameterY = "HorizontalMovement";
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.blendType = BlendTreeType.Simple1D;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.maxThreshold = 1f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.minThreshold = -1f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.name = "Blend Tree";
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.useAutomaticThresholds = false;
				var freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0 =  new ChildMotion();
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.motion = freeClimbLeftDiagonalJumpUpRightAnimationClip56378;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.cycleOffset = 0f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.directBlendParameter = "HorizontalMovement";
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.mirror = false;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.position = new Vector2(0f, 0f);
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.threshold = -1f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0.timeScale = 1f;
				var freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1 =  new ChildMotion();
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.motion = freeClimbJumpUpRightAnimationClip56380;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.cycleOffset = 0f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.directBlendParameter = "HorizontalMovement";
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.mirror = false;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.position = new Vector2(0f, 0f);
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.threshold = 0f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1.timeScale = 1f;
				var freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2 =  new ChildMotion();
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.motion = freeClimbRightDiagonalJumpUpRightAnimationClip56382;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.cycleOffset = 0f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.directBlendParameter = "HorizontalMovement";
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.mirror = false;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.position = new Vector2(0f, 0f);
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.threshold = 1f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2.timeScale = 1f;
				freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376.children = new ChildMotion[] {
					freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child0,
					freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child1,
					freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376Child2
				};
				freeClimbJumpRightAnimatorState55688.motion = freeClimbJumpRightAnimatorState55688blendTreeBlendTree56376;
				freeClimbJumpRightAnimatorState55688.cycleOffset = 0f;
				freeClimbJumpRightAnimatorState55688.cycleOffsetParameterActive = false;
				freeClimbJumpRightAnimatorState55688.iKOnFeet = false;
				freeClimbJumpRightAnimatorState55688.mirror = false;
				freeClimbJumpRightAnimatorState55688.mirrorParameterActive = false;
				freeClimbJumpRightAnimatorState55688.speed = 1f;
				freeClimbJumpRightAnimatorState55688.speedParameterActive = false;
				freeClimbJumpRightAnimatorState55688.writeDefaultValues = true;

				// State Machine.
				var hangtoFreeClimbAnimatorStateMachine55690 = freeClimbAnimatorStateMachine55652.AddStateMachine("Hang to Free Climb", new Vector3(300f, -10f, 0f));

				// States.
				var hangtoFreeClimbLeftAnimatorState56384 = hangtoFreeClimbAnimatorStateMachine55690.AddState("Hang to Free Climb Left", new Vector3(390f, -20f, 0f));
				hangtoFreeClimbLeftAnimatorState56384.motion = hangtoFreeClimbLeftAnimationClip56394;
				hangtoFreeClimbLeftAnimatorState56384.cycleOffset = 0f;
				hangtoFreeClimbLeftAnimatorState56384.cycleOffsetParameterActive = false;
				hangtoFreeClimbLeftAnimatorState56384.iKOnFeet = false;
				hangtoFreeClimbLeftAnimatorState56384.mirror = false;
				hangtoFreeClimbLeftAnimatorState56384.mirrorParameterActive = false;
				hangtoFreeClimbLeftAnimatorState56384.speed = 1.5f;
				hangtoFreeClimbLeftAnimatorState56384.speedParameterActive = false;
				hangtoFreeClimbLeftAnimatorState56384.writeDefaultValues = true;

				var hangtoFreeClimbRightAnimatorState56386 = hangtoFreeClimbAnimatorStateMachine55690.AddState("Hang to Free Climb Right", new Vector3(390f, 50f, 0f));
				hangtoFreeClimbRightAnimatorState56386.motion = hangtoFreeClimbRightAnimationClip56400;
				hangtoFreeClimbRightAnimatorState56386.cycleOffset = 0f;
				hangtoFreeClimbRightAnimatorState56386.cycleOffsetParameterActive = false;
				hangtoFreeClimbRightAnimatorState56386.iKOnFeet = false;
				hangtoFreeClimbRightAnimatorState56386.mirror = false;
				hangtoFreeClimbRightAnimatorState56386.mirrorParameterActive = false;
				hangtoFreeClimbRightAnimatorState56386.speed = 1.5f;
				hangtoFreeClimbRightAnimatorState56386.speedParameterActive = false;
				hangtoFreeClimbRightAnimatorState56386.writeDefaultValues = true;

				var hangtoFreeClimbVerticalAnimatorState56388 = hangtoFreeClimbAnimatorStateMachine55690.AddState("Hang to Free Climb Vertical", new Vector3(390f, 120f, 0f));
				hangtoFreeClimbVerticalAnimatorState56388.motion = hangtoFreeClimbVerticalAnimationClip56406;
				hangtoFreeClimbVerticalAnimatorState56388.cycleOffset = 0f;
				hangtoFreeClimbVerticalAnimatorState56388.cycleOffsetParameterActive = false;
				hangtoFreeClimbVerticalAnimatorState56388.iKOnFeet = false;
				hangtoFreeClimbVerticalAnimatorState56388.mirror = false;
				hangtoFreeClimbVerticalAnimatorState56388.mirrorParameterActive = false;
				hangtoFreeClimbVerticalAnimatorState56388.speed = 1f;
				hangtoFreeClimbVerticalAnimatorState56388.speedParameterActive = false;
				hangtoFreeClimbVerticalAnimatorState56388.writeDefaultValues = true;

				// State Machine Defaults.
				hangtoFreeClimbAnimatorStateMachine55690.anyStatePosition = new Vector3(50f, 20f, 0f);
				hangtoFreeClimbAnimatorStateMachine55690.defaultState = hangtoFreeClimbLeftAnimatorState56384;
				hangtoFreeClimbAnimatorStateMachine55690.entryPosition = new Vector3(50f, -20f, 0f);
				hangtoFreeClimbAnimatorStateMachine55690.exitPosition = new Vector3(800f, 120f, 0f);
				hangtoFreeClimbAnimatorStateMachine55690.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Machine Defaults.
				freeClimbAnimatorStateMachine55652.anyStatePosition = new Vector3(-740f, -220f, 0f);
				freeClimbAnimatorStateMachine55652.defaultState = freeClimbBottomMountAnimatorState55666;
				freeClimbAnimatorStateMachine55652.entryPosition = new Vector3(-740f, -280f, 0f);
				freeClimbAnimatorStateMachine55652.exitPosition = new Vector3(610f, -180f, 0f);
				freeClimbAnimatorStateMachine55652.parentStateMachinePosition = new Vector3(580f, -280f, 0f);

				// State Transitions.
				var animatorStateTransition56244 = freeClimbBottomMountAnimatorState55666.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56244.canTransitionToSelf = true;
				animatorStateTransition56244.duration = 0f;
				animatorStateTransition56244.exitTime = 1f;
				animatorStateTransition56244.hasExitTime = false;
				animatorStateTransition56244.hasFixedDuration = true;
				animatorStateTransition56244.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56244.offset = 0f;
				animatorStateTransition56244.orderedInterruption = true;
				animatorStateTransition56244.isExit = false;
				animatorStateTransition56244.mute = false;
				animatorStateTransition56244.solo = false;
				animatorStateTransition56244.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56244.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition56246 = freeClimbBottomMountAnimatorState55666.AddTransition(freeClimbBottomDismountAnimatorState55674);
				animatorStateTransition56246.canTransitionToSelf = true;
				animatorStateTransition56246.duration = 0f;
				animatorStateTransition56246.exitTime = 1.05f;
				animatorStateTransition56246.hasExitTime = true;
				animatorStateTransition56246.hasFixedDuration = true;
				animatorStateTransition56246.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56246.offset = 0f;
				animatorStateTransition56246.orderedInterruption = true;
				animatorStateTransition56246.isExit = false;
				animatorStateTransition56246.mute = false;
				animatorStateTransition56246.solo = false;
				animatorStateTransition56246.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56246.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition56250 = freeClimbTopMountAnimatorState55668.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56250.canTransitionToSelf = true;
				animatorStateTransition56250.duration = 0.15f;
				animatorStateTransition56250.exitTime = 1f;
				animatorStateTransition56250.hasExitTime = false;
				animatorStateTransition56250.hasFixedDuration = true;
				animatorStateTransition56250.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56250.offset = 0f;
				animatorStateTransition56250.orderedInterruption = true;
				animatorStateTransition56250.isExit = false;
				animatorStateTransition56250.mute = false;
				animatorStateTransition56250.solo = false;
				animatorStateTransition56250.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56250.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition56252 = freeClimbTopMountAnimatorState55668.AddTransition(freeClimbTopDismountAnimatorState55676);
				animatorStateTransition56252.canTransitionToSelf = true;
				animatorStateTransition56252.duration = 0f;
				animatorStateTransition56252.exitTime = 1.05f;
				animatorStateTransition56252.hasExitTime = true;
				animatorStateTransition56252.hasFixedDuration = true;
				animatorStateTransition56252.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56252.offset = 0f;
				animatorStateTransition56252.orderedInterruption = true;
				animatorStateTransition56252.isExit = false;
				animatorStateTransition56252.mute = false;
				animatorStateTransition56252.solo = false;
				animatorStateTransition56252.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56252.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");

				var animatorStateTransition56256 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56256.canTransitionToSelf = true;
				animatorStateTransition56256.duration = 0.1f;
				animatorStateTransition56256.exitTime = 0.35f;
				animatorStateTransition56256.hasExitTime = false;
				animatorStateTransition56256.hasFixedDuration = true;
				animatorStateTransition56256.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56256.offset = 0f;
				animatorStateTransition56256.orderedInterruption = true;
				animatorStateTransition56256.isExit = false;
				animatorStateTransition56256.mute = false;
				animatorStateTransition56256.solo = false;
				animatorStateTransition56256.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56256.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56256.AddCondition(AnimatorConditionMode.Greater, -0.1f, "HorizontalMovement");
				animatorStateTransition56256.AddCondition(AnimatorConditionMode.Less, 0.1f, "HorizontalMovement");
				animatorStateTransition56256.AddCondition(AnimatorConditionMode.Greater, -0.1f, "ForwardMovement");
				animatorStateTransition56256.AddCondition(AnimatorConditionMode.Less, 0.1f, "ForwardMovement");

				var animatorStateTransition56258 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56258.canTransitionToSelf = true;
				animatorStateTransition56258.duration = 0.05f;
				animatorStateTransition56258.exitTime = 0.7f;
				animatorStateTransition56258.hasExitTime = false;
				animatorStateTransition56258.hasFixedDuration = true;
				animatorStateTransition56258.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56258.offset = 0f;
				animatorStateTransition56258.orderedInterruption = true;
				animatorStateTransition56258.isExit = false;
				animatorStateTransition56258.mute = false;
				animatorStateTransition56258.solo = false;
				animatorStateTransition56258.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56258.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56258.AddCondition(AnimatorConditionMode.Less, -0.1f, "HorizontalMovement");
				animatorStateTransition56258.AddCondition(AnimatorConditionMode.Greater, -0.1f, "ForwardMovement");
				animatorStateTransition56258.AddCondition(AnimatorConditionMode.Less, 0.1f, "ForwardMovement");

				var animatorStateTransition56260 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56260.canTransitionToSelf = true;
				animatorStateTransition56260.duration = 0.05f;
				animatorStateTransition56260.exitTime = 0.7f;
				animatorStateTransition56260.hasExitTime = false;
				animatorStateTransition56260.hasFixedDuration = true;
				animatorStateTransition56260.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56260.offset = 0f;
				animatorStateTransition56260.orderedInterruption = true;
				animatorStateTransition56260.isExit = false;
				animatorStateTransition56260.mute = false;
				animatorStateTransition56260.solo = false;
				animatorStateTransition56260.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56260.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56260.AddCondition(AnimatorConditionMode.Greater, 0.1f, "HorizontalMovement");
				animatorStateTransition56260.AddCondition(AnimatorConditionMode.Greater, -0.1f, "ForwardMovement");
				animatorStateTransition56260.AddCondition(AnimatorConditionMode.Less, 0.1f, "ForwardMovement");

				var animatorStateTransition56262 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbBottomDismountAnimatorState55674);
				animatorStateTransition56262.canTransitionToSelf = true;
				animatorStateTransition56262.duration = 0.05f;
				animatorStateTransition56262.exitTime = 0.7f;
				animatorStateTransition56262.hasExitTime = false;
				animatorStateTransition56262.hasFixedDuration = true;
				animatorStateTransition56262.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56262.offset = 0f;
				animatorStateTransition56262.orderedInterruption = true;
				animatorStateTransition56262.isExit = false;
				animatorStateTransition56262.mute = false;
				animatorStateTransition56262.solo = false;
				animatorStateTransition56262.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56262.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition56264 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbTopDismountAnimatorState55676);
				animatorStateTransition56264.canTransitionToSelf = true;
				animatorStateTransition56264.duration = 0.05f;
				animatorStateTransition56264.exitTime = 0.7f;
				animatorStateTransition56264.hasExitTime = false;
				animatorStateTransition56264.hasFixedDuration = true;
				animatorStateTransition56264.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56264.offset = 0f;
				animatorStateTransition56264.orderedInterruption = true;
				animatorStateTransition56264.isExit = false;
				animatorStateTransition56264.mute = false;
				animatorStateTransition56264.solo = false;
				animatorStateTransition56264.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56264.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");

				var animatorStateTransition56266 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbJumpLeftAnimatorState55686);
				animatorStateTransition56266.canTransitionToSelf = true;
				animatorStateTransition56266.duration = 0f;
				animatorStateTransition56266.exitTime = 1f;
				animatorStateTransition56266.hasExitTime = false;
				animatorStateTransition56266.hasFixedDuration = true;
				animatorStateTransition56266.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56266.offset = 0f;
				animatorStateTransition56266.orderedInterruption = true;
				animatorStateTransition56266.isExit = false;
				animatorStateTransition56266.mute = false;
				animatorStateTransition56266.solo = false;
				animatorStateTransition56266.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56266.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56266.AddCondition(AnimatorConditionMode.Greater, 0f, "ForwardMovement");
				animatorStateTransition56266.AddCondition(AnimatorConditionMode.Greater, 1.5f, "Speed");
				animatorStateTransition56266.AddCondition(AnimatorConditionMode.Greater, 0.5f, "LegIndex");

				var animatorStateTransition56268 = freeClimbVerticalAnimatorState55670.AddTransition(innerLeftTurnAnimatorState55684);
				animatorStateTransition56268.canTransitionToSelf = true;
				animatorStateTransition56268.duration = 0.1f;
				animatorStateTransition56268.exitTime = 0.7f;
				animatorStateTransition56268.hasExitTime = false;
				animatorStateTransition56268.hasFixedDuration = true;
				animatorStateTransition56268.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56268.offset = 0f;
				animatorStateTransition56268.orderedInterruption = true;
				animatorStateTransition56268.isExit = false;
				animatorStateTransition56268.mute = false;
				animatorStateTransition56268.solo = false;
				animatorStateTransition56268.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56268.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition56268.AddCondition(AnimatorConditionMode.Less, 0f, "AbilityFloatData");

				var animatorStateTransition56270 = freeClimbVerticalAnimatorState55670.AddTransition(innerRightTurnAnimatorState55678);
				animatorStateTransition56270.canTransitionToSelf = true;
				animatorStateTransition56270.duration = 0.1f;
				animatorStateTransition56270.exitTime = 0.7f;
				animatorStateTransition56270.hasExitTime = false;
				animatorStateTransition56270.hasFixedDuration = true;
				animatorStateTransition56270.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56270.offset = 0f;
				animatorStateTransition56270.orderedInterruption = true;
				animatorStateTransition56270.isExit = false;
				animatorStateTransition56270.mute = false;
				animatorStateTransition56270.solo = false;
				animatorStateTransition56270.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56270.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition56270.AddCondition(AnimatorConditionMode.Greater, 0f, "AbilityFloatData");

				var animatorStateTransition56272 = freeClimbVerticalAnimatorState55670.AddTransition(outerRightTurnAnimatorState55680);
				animatorStateTransition56272.canTransitionToSelf = true;
				animatorStateTransition56272.duration = 0.1f;
				animatorStateTransition56272.exitTime = 0.7f;
				animatorStateTransition56272.hasExitTime = false;
				animatorStateTransition56272.hasFixedDuration = true;
				animatorStateTransition56272.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56272.offset = 0f;
				animatorStateTransition56272.orderedInterruption = true;
				animatorStateTransition56272.isExit = false;
				animatorStateTransition56272.mute = false;
				animatorStateTransition56272.solo = false;
				animatorStateTransition56272.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56272.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");
				animatorStateTransition56272.AddCondition(AnimatorConditionMode.Greater, 0f, "AbilityFloatData");

				var animatorStateTransition56274 = freeClimbVerticalAnimatorState55670.AddTransition(outerLeftTurnAnimatorState55682);
				animatorStateTransition56274.canTransitionToSelf = true;
				animatorStateTransition56274.duration = 0.1f;
				animatorStateTransition56274.exitTime = 0.7f;
				animatorStateTransition56274.hasExitTime = false;
				animatorStateTransition56274.hasFixedDuration = true;
				animatorStateTransition56274.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56274.offset = 0f;
				animatorStateTransition56274.orderedInterruption = true;
				animatorStateTransition56274.isExit = false;
				animatorStateTransition56274.mute = false;
				animatorStateTransition56274.solo = false;
				animatorStateTransition56274.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56274.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");
				animatorStateTransition56274.AddCondition(AnimatorConditionMode.Less, 0f, "AbilityFloatData");

				var animatorStateTransition56276 = freeClimbVerticalAnimatorState55670.AddExitTransition();
				animatorStateTransition56276.canTransitionToSelf = true;
				animatorStateTransition56276.duration = 0.1f;
				animatorStateTransition56276.exitTime = 0.7f;
				animatorStateTransition56276.hasExitTime = false;
				animatorStateTransition56276.hasFixedDuration = true;
				animatorStateTransition56276.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56276.offset = 0f;
				animatorStateTransition56276.orderedInterruption = true;
				animatorStateTransition56276.isExit = true;
				animatorStateTransition56276.mute = false;
				animatorStateTransition56276.solo = false;
				animatorStateTransition56276.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56278 = freeClimbVerticalAnimatorState55670.AddTransition(freeClimbJumpRightAnimatorState55688);
				animatorStateTransition56278.canTransitionToSelf = true;
				animatorStateTransition56278.duration = 0f;
				animatorStateTransition56278.exitTime = 0.608578f;
				animatorStateTransition56278.hasExitTime = false;
				animatorStateTransition56278.hasFixedDuration = true;
				animatorStateTransition56278.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56278.offset = 0f;
				animatorStateTransition56278.orderedInterruption = true;
				animatorStateTransition56278.isExit = false;
				animatorStateTransition56278.mute = false;
				animatorStateTransition56278.solo = false;
				animatorStateTransition56278.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56278.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56278.AddCondition(AnimatorConditionMode.Greater, 0f, "ForwardMovement");
				animatorStateTransition56278.AddCondition(AnimatorConditionMode.Greater, 1.5f, "Speed");
				animatorStateTransition56278.AddCondition(AnimatorConditionMode.Less, 0.5f, "LegIndex");

				var animatorStateTransition56294 = freeClimbIdleAnimatorState55672.AddTransition(freeClimbVerticalAnimatorState55670);
				animatorStateTransition56294.canTransitionToSelf = true;
				animatorStateTransition56294.duration = 0.1f;
				animatorStateTransition56294.exitTime = 0.775f;
				animatorStateTransition56294.hasExitTime = false;
				animatorStateTransition56294.hasFixedDuration = true;
				animatorStateTransition56294.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56294.offset = 0f;
				animatorStateTransition56294.orderedInterruption = true;
				animatorStateTransition56294.isExit = false;
				animatorStateTransition56294.mute = false;
				animatorStateTransition56294.solo = false;
				animatorStateTransition56294.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56294.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56294.AddCondition(AnimatorConditionMode.Less, -0.1f, "ForwardMovement");

				var animatorStateTransition56296 = freeClimbIdleAnimatorState55672.AddTransition(freeClimbVerticalAnimatorState55670);
				animatorStateTransition56296.canTransitionToSelf = true;
				animatorStateTransition56296.duration = 0.1f;
				animatorStateTransition56296.exitTime = 0.775f;
				animatorStateTransition56296.hasExitTime = false;
				animatorStateTransition56296.hasFixedDuration = true;
				animatorStateTransition56296.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56296.offset = 0f;
				animatorStateTransition56296.orderedInterruption = true;
				animatorStateTransition56296.isExit = false;
				animatorStateTransition56296.mute = false;
				animatorStateTransition56296.solo = false;
				animatorStateTransition56296.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56296.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");
				animatorStateTransition56296.AddCondition(AnimatorConditionMode.Greater, 0.1f, "ForwardMovement");

				var animatorStateTransition56298 = freeClimbIdleAnimatorState55672.AddTransition(freeClimbBottomDismountAnimatorState55674);
				animatorStateTransition56298.canTransitionToSelf = true;
				animatorStateTransition56298.duration = 0.05f;
				animatorStateTransition56298.exitTime = 0.82f;
				animatorStateTransition56298.hasExitTime = false;
				animatorStateTransition56298.hasFixedDuration = true;
				animatorStateTransition56298.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56298.offset = 0f;
				animatorStateTransition56298.orderedInterruption = true;
				animatorStateTransition56298.isExit = false;
				animatorStateTransition56298.mute = false;
				animatorStateTransition56298.solo = false;
				animatorStateTransition56298.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56298.AddCondition(AnimatorConditionMode.Equals, 5f, "AbilityIntData");

				var animatorStateTransition56300 = freeClimbIdleAnimatorState55672.AddTransition(freeClimbTopDismountAnimatorState55676);
				animatorStateTransition56300.canTransitionToSelf = true;
				animatorStateTransition56300.duration = 0.15f;
				animatorStateTransition56300.exitTime = 0.82f;
				animatorStateTransition56300.hasExitTime = false;
				animatorStateTransition56300.hasFixedDuration = true;
				animatorStateTransition56300.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56300.offset = 0f;
				animatorStateTransition56300.orderedInterruption = true;
				animatorStateTransition56300.isExit = false;
				animatorStateTransition56300.mute = false;
				animatorStateTransition56300.solo = false;
				animatorStateTransition56300.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56300.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");

				var animatorStateTransition56302 = freeClimbIdleAnimatorState55672.AddTransition(innerRightTurnAnimatorState55678);
				animatorStateTransition56302.canTransitionToSelf = true;
				animatorStateTransition56302.duration = 0.05f;
				animatorStateTransition56302.exitTime = 0.82f;
				animatorStateTransition56302.hasExitTime = false;
				animatorStateTransition56302.hasFixedDuration = true;
				animatorStateTransition56302.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56302.offset = 0f;
				animatorStateTransition56302.orderedInterruption = true;
				animatorStateTransition56302.isExit = false;
				animatorStateTransition56302.mute = false;
				animatorStateTransition56302.solo = false;
				animatorStateTransition56302.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56302.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition56302.AddCondition(AnimatorConditionMode.Greater, 0f, "AbilityFloatData");

				var animatorStateTransition56304 = freeClimbIdleAnimatorState55672.AddTransition(outerRightTurnAnimatorState55680);
				animatorStateTransition56304.canTransitionToSelf = true;
				animatorStateTransition56304.duration = 0.05f;
				animatorStateTransition56304.exitTime = 0.82f;
				animatorStateTransition56304.hasExitTime = false;
				animatorStateTransition56304.hasFixedDuration = true;
				animatorStateTransition56304.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56304.offset = 0f;
				animatorStateTransition56304.orderedInterruption = true;
				animatorStateTransition56304.isExit = false;
				animatorStateTransition56304.mute = false;
				animatorStateTransition56304.solo = false;
				animatorStateTransition56304.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56304.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");
				animatorStateTransition56304.AddCondition(AnimatorConditionMode.Greater, 0f, "AbilityFloatData");

				var animatorStateTransition56306 = freeClimbIdleAnimatorState55672.AddTransition(innerLeftTurnAnimatorState55684);
				animatorStateTransition56306.canTransitionToSelf = true;
				animatorStateTransition56306.duration = 0.05f;
				animatorStateTransition56306.exitTime = 0.82f;
				animatorStateTransition56306.hasExitTime = false;
				animatorStateTransition56306.hasFixedDuration = true;
				animatorStateTransition56306.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56306.offset = 0f;
				animatorStateTransition56306.orderedInterruption = true;
				animatorStateTransition56306.isExit = false;
				animatorStateTransition56306.mute = false;
				animatorStateTransition56306.solo = false;
				animatorStateTransition56306.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56306.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition56306.AddCondition(AnimatorConditionMode.Less, 0f, "AbilityFloatData");

				var animatorStateTransition56308 = freeClimbIdleAnimatorState55672.AddTransition(outerLeftTurnAnimatorState55682);
				animatorStateTransition56308.canTransitionToSelf = true;
				animatorStateTransition56308.duration = 0.05f;
				animatorStateTransition56308.exitTime = 0.82f;
				animatorStateTransition56308.hasExitTime = false;
				animatorStateTransition56308.hasFixedDuration = true;
				animatorStateTransition56308.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56308.offset = 0f;
				animatorStateTransition56308.orderedInterruption = true;
				animatorStateTransition56308.isExit = false;
				animatorStateTransition56308.mute = false;
				animatorStateTransition56308.solo = false;
				animatorStateTransition56308.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56308.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");
				animatorStateTransition56308.AddCondition(AnimatorConditionMode.Less, 0f, "AbilityFloatData");

				var animatorStateTransition56310 = freeClimbIdleAnimatorState55672.AddExitTransition();
				animatorStateTransition56310.canTransitionToSelf = true;
				animatorStateTransition56310.duration = 0.1f;
				animatorStateTransition56310.exitTime = 0.82f;
				animatorStateTransition56310.hasExitTime = false;
				animatorStateTransition56310.hasFixedDuration = true;
				animatorStateTransition56310.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56310.offset = 0f;
				animatorStateTransition56310.orderedInterruption = true;
				animatorStateTransition56310.isExit = true;
				animatorStateTransition56310.mute = false;
				animatorStateTransition56310.solo = false;
				animatorStateTransition56310.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56324 = freeClimbBottomDismountAnimatorState55674.AddExitTransition();
				animatorStateTransition56324.canTransitionToSelf = true;
				animatorStateTransition56324.duration = 0f;
				animatorStateTransition56324.exitTime = 1f;
				animatorStateTransition56324.hasExitTime = true;
				animatorStateTransition56324.hasFixedDuration = true;
				animatorStateTransition56324.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56324.offset = 0f;
				animatorStateTransition56324.orderedInterruption = true;
				animatorStateTransition56324.isExit = true;
				animatorStateTransition56324.mute = false;
				animatorStateTransition56324.solo = false;

				var animatorStateTransition56328 = freeClimbTopDismountAnimatorState55676.AddExitTransition();
				animatorStateTransition56328.canTransitionToSelf = true;
				animatorStateTransition56328.duration = 0f;
				animatorStateTransition56328.exitTime = 1f;
				animatorStateTransition56328.hasExitTime = true;
				animatorStateTransition56328.hasFixedDuration = true;
				animatorStateTransition56328.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56328.offset = 0f;
				animatorStateTransition56328.orderedInterruption = true;
				animatorStateTransition56328.isExit = true;
				animatorStateTransition56328.mute = false;
				animatorStateTransition56328.solo = false;

				var animatorStateTransition56332 = innerRightTurnAnimatorState55678.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56332.canTransitionToSelf = true;
				animatorStateTransition56332.duration = 0.05f;
				animatorStateTransition56332.exitTime = 1f;
				animatorStateTransition56332.hasExitTime = false;
				animatorStateTransition56332.hasFixedDuration = true;
				animatorStateTransition56332.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56332.offset = 0f;
				animatorStateTransition56332.orderedInterruption = true;
				animatorStateTransition56332.isExit = false;
				animatorStateTransition56332.mute = false;
				animatorStateTransition56332.solo = false;
				animatorStateTransition56332.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56332.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition56334 = innerRightTurnAnimatorState55678.AddTransition(innerLeftTurnAnimatorState55684);
				animatorStateTransition56334.canTransitionToSelf = true;
				animatorStateTransition56334.duration = 0.05f;
				animatorStateTransition56334.exitTime = 1f;
				animatorStateTransition56334.hasExitTime = true;
				animatorStateTransition56334.hasFixedDuration = true;
				animatorStateTransition56334.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56334.offset = 0f;
				animatorStateTransition56334.orderedInterruption = true;
				animatorStateTransition56334.isExit = false;
				animatorStateTransition56334.mute = false;
				animatorStateTransition56334.solo = false;
				animatorStateTransition56334.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56334.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition56334.AddCondition(AnimatorConditionMode.Less, 0f, "AbilityFloatData");

				var animatorStateTransition56336 = innerRightTurnAnimatorState55678.AddExitTransition();
				animatorStateTransition56336.canTransitionToSelf = true;
				animatorStateTransition56336.duration = 0.05f;
				animatorStateTransition56336.exitTime = 0.7f;
				animatorStateTransition56336.hasExitTime = false;
				animatorStateTransition56336.hasFixedDuration = true;
				animatorStateTransition56336.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56336.offset = 0f;
				animatorStateTransition56336.orderedInterruption = true;
				animatorStateTransition56336.isExit = true;
				animatorStateTransition56336.mute = false;
				animatorStateTransition56336.solo = false;
				animatorStateTransition56336.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56340 = outerRightTurnAnimatorState55680.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56340.canTransitionToSelf = true;
				animatorStateTransition56340.duration = 0.15f;
				animatorStateTransition56340.exitTime = 1f;
				animatorStateTransition56340.hasExitTime = false;
				animatorStateTransition56340.hasFixedDuration = true;
				animatorStateTransition56340.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56340.offset = 0f;
				animatorStateTransition56340.orderedInterruption = true;
				animatorStateTransition56340.isExit = false;
				animatorStateTransition56340.mute = false;
				animatorStateTransition56340.solo = false;
				animatorStateTransition56340.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56340.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition56342 = outerRightTurnAnimatorState55680.AddTransition(outerLeftTurnAnimatorState55682);
				animatorStateTransition56342.canTransitionToSelf = true;
				animatorStateTransition56342.duration = 0.05f;
				animatorStateTransition56342.exitTime = 1f;
				animatorStateTransition56342.hasExitTime = true;
				animatorStateTransition56342.hasFixedDuration = true;
				animatorStateTransition56342.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56342.offset = 0f;
				animatorStateTransition56342.orderedInterruption = true;
				animatorStateTransition56342.isExit = false;
				animatorStateTransition56342.mute = false;
				animatorStateTransition56342.solo = false;
				animatorStateTransition56342.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56342.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");
				animatorStateTransition56342.AddCondition(AnimatorConditionMode.Less, 0f, "AbilityFloatData");

				var animatorStateTransition56344 = outerRightTurnAnimatorState55680.AddExitTransition();
				animatorStateTransition56344.canTransitionToSelf = true;
				animatorStateTransition56344.duration = 0.05f;
				animatorStateTransition56344.exitTime = 0.7f;
				animatorStateTransition56344.hasExitTime = false;
				animatorStateTransition56344.hasFixedDuration = true;
				animatorStateTransition56344.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56344.offset = 0f;
				animatorStateTransition56344.orderedInterruption = true;
				animatorStateTransition56344.isExit = true;
				animatorStateTransition56344.mute = false;
				animatorStateTransition56344.solo = false;
				animatorStateTransition56344.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56348 = outerLeftTurnAnimatorState55682.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56348.canTransitionToSelf = true;
				animatorStateTransition56348.duration = 0.15f;
				animatorStateTransition56348.exitTime = 1f;
				animatorStateTransition56348.hasExitTime = false;
				animatorStateTransition56348.hasFixedDuration = true;
				animatorStateTransition56348.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56348.offset = 0f;
				animatorStateTransition56348.orderedInterruption = true;
				animatorStateTransition56348.isExit = false;
				animatorStateTransition56348.mute = false;
				animatorStateTransition56348.solo = false;
				animatorStateTransition56348.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56348.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition56350 = outerLeftTurnAnimatorState55682.AddTransition(outerRightTurnAnimatorState55680);
				animatorStateTransition56350.canTransitionToSelf = true;
				animatorStateTransition56350.duration = 0.05f;
				animatorStateTransition56350.exitTime = 1f;
				animatorStateTransition56350.hasExitTime = true;
				animatorStateTransition56350.hasFixedDuration = true;
				animatorStateTransition56350.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56350.offset = 0f;
				animatorStateTransition56350.orderedInterruption = true;
				animatorStateTransition56350.isExit = false;
				animatorStateTransition56350.mute = false;
				animatorStateTransition56350.solo = false;
				animatorStateTransition56350.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56350.AddCondition(AnimatorConditionMode.Equals, 4f, "AbilityIntData");
				animatorStateTransition56350.AddCondition(AnimatorConditionMode.Greater, 0f, "AbilityFloatData");

				var animatorStateTransition56352 = outerLeftTurnAnimatorState55682.AddExitTransition();
				animatorStateTransition56352.canTransitionToSelf = true;
				animatorStateTransition56352.duration = 0.05f;
				animatorStateTransition56352.exitTime = 0.7f;
				animatorStateTransition56352.hasExitTime = false;
				animatorStateTransition56352.hasFixedDuration = true;
				animatorStateTransition56352.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56352.offset = 0f;
				animatorStateTransition56352.orderedInterruption = true;
				animatorStateTransition56352.isExit = true;
				animatorStateTransition56352.mute = false;
				animatorStateTransition56352.solo = false;
				animatorStateTransition56352.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56356 = innerLeftTurnAnimatorState55684.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56356.canTransitionToSelf = true;
				animatorStateTransition56356.duration = 0.05f;
				animatorStateTransition56356.exitTime = 1f;
				animatorStateTransition56356.hasExitTime = false;
				animatorStateTransition56356.hasFixedDuration = true;
				animatorStateTransition56356.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56356.offset = 0f;
				animatorStateTransition56356.orderedInterruption = true;
				animatorStateTransition56356.isExit = false;
				animatorStateTransition56356.mute = false;
				animatorStateTransition56356.solo = false;
				animatorStateTransition56356.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56356.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition56358 = innerLeftTurnAnimatorState55684.AddTransition(innerRightTurnAnimatorState55678);
				animatorStateTransition56358.canTransitionToSelf = true;
				animatorStateTransition56358.duration = 0.05f;
				animatorStateTransition56358.exitTime = 1f;
				animatorStateTransition56358.hasExitTime = true;
				animatorStateTransition56358.hasFixedDuration = true;
				animatorStateTransition56358.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56358.offset = 0f;
				animatorStateTransition56358.orderedInterruption = true;
				animatorStateTransition56358.isExit = false;
				animatorStateTransition56358.mute = false;
				animatorStateTransition56358.solo = false;
				animatorStateTransition56358.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition56358.AddCondition(AnimatorConditionMode.Equals, 3f, "AbilityIntData");
				animatorStateTransition56358.AddCondition(AnimatorConditionMode.Greater, 0f, "AbilityFloatData");

				var animatorStateTransition56360 = innerLeftTurnAnimatorState55684.AddExitTransition();
				animatorStateTransition56360.canTransitionToSelf = true;
				animatorStateTransition56360.duration = 0.05f;
				animatorStateTransition56360.exitTime = 0.7f;
				animatorStateTransition56360.hasExitTime = false;
				animatorStateTransition56360.hasFixedDuration = true;
				animatorStateTransition56360.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56360.offset = 0f;
				animatorStateTransition56360.orderedInterruption = true;
				animatorStateTransition56360.isExit = true;
				animatorStateTransition56360.mute = false;
				animatorStateTransition56360.solo = false;
				animatorStateTransition56360.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56364 = freeClimbJumpLeftAnimatorState55686.AddTransition(freeClimbVerticalAnimatorState55670);
				animatorStateTransition56364.canTransitionToSelf = true;
				animatorStateTransition56364.duration = 0.05f;
				animatorStateTransition56364.exitTime = 1f;
				animatorStateTransition56364.hasExitTime = true;
				animatorStateTransition56364.hasFixedDuration = true;
				animatorStateTransition56364.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56364.offset = 0.5f;
				animatorStateTransition56364.orderedInterruption = true;
				animatorStateTransition56364.isExit = false;
				animatorStateTransition56364.mute = false;
				animatorStateTransition56364.solo = false;

				var animatorStateTransition82108 = freeClimbJumpLeftAnimatorState55686.AddTransition(freeClimbTopDismountAnimatorState55676);
				animatorStateTransition82108.canTransitionToSelf = true;
				animatorStateTransition82108.duration = 0.15f;
				animatorStateTransition82108.exitTime = 0.7f;
				animatorStateTransition82108.hasExitTime = false;
				animatorStateTransition82108.hasFixedDuration = true;
				animatorStateTransition82108.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition82108.offset = 0f;
				animatorStateTransition82108.orderedInterruption = true;
				animatorStateTransition82108.isExit = false;
				animatorStateTransition82108.mute = false;
				animatorStateTransition82108.solo = false;
				animatorStateTransition82108.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition82108.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");

				var animatorStateTransition56374 = freeClimbJumpRightAnimatorState55688.AddTransition(freeClimbVerticalAnimatorState55670);
				animatorStateTransition56374.canTransitionToSelf = true;
				animatorStateTransition56374.duration = 0.05f;
				animatorStateTransition56374.exitTime = 1f;
				animatorStateTransition56374.hasExitTime = true;
				animatorStateTransition56374.hasFixedDuration = true;
				animatorStateTransition56374.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56374.offset = 0f;
				animatorStateTransition56374.orderedInterruption = true;
				animatorStateTransition56374.isExit = false;
				animatorStateTransition56374.mute = false;
				animatorStateTransition56374.solo = false;

				var animatorStateTransition72704 = freeClimbJumpRightAnimatorState55688.AddTransition(freeClimbTopDismountAnimatorState55676);
				animatorStateTransition72704.canTransitionToSelf = true;
				animatorStateTransition72704.duration = 0.15f;
				animatorStateTransition72704.exitTime = 0.7f;
				animatorStateTransition72704.hasExitTime = false;
				animatorStateTransition72704.hasFixedDuration = true;
				animatorStateTransition72704.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition72704.offset = 0f;
				animatorStateTransition72704.orderedInterruption = true;
				animatorStateTransition72704.isExit = false;
				animatorStateTransition72704.mute = false;
				animatorStateTransition72704.solo = false;
				animatorStateTransition72704.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition72704.AddCondition(AnimatorConditionMode.Equals, 6f, "AbilityIntData");

				// State Transitions.
				var animatorStateTransition56390 = hangtoFreeClimbLeftAnimatorState56384.AddExitTransition();
				animatorStateTransition56390.canTransitionToSelf = true;
				animatorStateTransition56390.duration = 0.05f;
				animatorStateTransition56390.exitTime = 1f;
				animatorStateTransition56390.hasExitTime = false;
				animatorStateTransition56390.hasFixedDuration = true;
				animatorStateTransition56390.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56390.offset = 0f;
				animatorStateTransition56390.orderedInterruption = true;
				animatorStateTransition56390.isExit = true;
				animatorStateTransition56390.mute = false;
				animatorStateTransition56390.solo = false;
				animatorStateTransition56390.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56392 = hangtoFreeClimbLeftAnimatorState56384.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56392.canTransitionToSelf = true;
				animatorStateTransition56392.duration = 0.05f;
				animatorStateTransition56392.exitTime = 1f;
				animatorStateTransition56392.hasExitTime = true;
				animatorStateTransition56392.hasFixedDuration = true;
				animatorStateTransition56392.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56392.offset = 0f;
				animatorStateTransition56392.orderedInterruption = true;
				animatorStateTransition56392.isExit = false;
				animatorStateTransition56392.mute = false;
				animatorStateTransition56392.solo = false;
				animatorStateTransition56392.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");

				var animatorStateTransition56396 = hangtoFreeClimbRightAnimatorState56386.AddExitTransition();
				animatorStateTransition56396.canTransitionToSelf = true;
				animatorStateTransition56396.duration = 0.05f;
				animatorStateTransition56396.exitTime = 1f;
				animatorStateTransition56396.hasExitTime = false;
				animatorStateTransition56396.hasFixedDuration = true;
				animatorStateTransition56396.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56396.offset = 0f;
				animatorStateTransition56396.orderedInterruption = true;
				animatorStateTransition56396.isExit = true;
				animatorStateTransition56396.mute = false;
				animatorStateTransition56396.solo = false;
				animatorStateTransition56396.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				var animatorStateTransition56398 = hangtoFreeClimbRightAnimatorState56386.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56398.canTransitionToSelf = true;
				animatorStateTransition56398.duration = 0.05f;
				animatorStateTransition56398.exitTime = 1f;
				animatorStateTransition56398.hasExitTime = true;
				animatorStateTransition56398.hasFixedDuration = true;
				animatorStateTransition56398.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56398.offset = 0f;
				animatorStateTransition56398.orderedInterruption = true;
				animatorStateTransition56398.isExit = false;
				animatorStateTransition56398.mute = false;
				animatorStateTransition56398.solo = false;
				animatorStateTransition56398.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");

				var animatorStateTransition56402 = hangtoFreeClimbVerticalAnimatorState56388.AddTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition56402.canTransitionToSelf = true;
				animatorStateTransition56402.duration = 0.05f;
				animatorStateTransition56402.exitTime = 1f;
				animatorStateTransition56402.hasExitTime = true;
				animatorStateTransition56402.hasFixedDuration = true;
				animatorStateTransition56402.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56402.offset = 0f;
				animatorStateTransition56402.orderedInterruption = true;
				animatorStateTransition56402.isExit = false;
				animatorStateTransition56402.mute = false;
				animatorStateTransition56402.solo = false;
				animatorStateTransition56402.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");

				var animatorStateTransition56404 = hangtoFreeClimbVerticalAnimatorState56388.AddExitTransition();
				animatorStateTransition56404.canTransitionToSelf = true;
				animatorStateTransition56404.duration = 0.05f;
				animatorStateTransition56404.exitTime = 1f;
				animatorStateTransition56404.hasExitTime = false;
				animatorStateTransition56404.hasFixedDuration = true;
				animatorStateTransition56404.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition56404.offset = 0f;
				animatorStateTransition56404.orderedInterruption = true;
				animatorStateTransition56404.isExit = true;
				animatorStateTransition56404.mute = false;
				animatorStateTransition56404.solo = false;
				animatorStateTransition56404.AddCondition(AnimatorConditionMode.NotEqual, 503f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition55940 = baseStateMachine1807513540.AddAnyStateTransition(freeClimbBottomMountAnimatorState55666);
				animatorStateTransition55940.canTransitionToSelf = true;
				animatorStateTransition55940.duration = 0.15f;
				animatorStateTransition55940.exitTime = 0.75f;
				animatorStateTransition55940.hasExitTime = false;
				animatorStateTransition55940.hasFixedDuration = true;
				animatorStateTransition55940.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition55940.offset = 0f;
				animatorStateTransition55940.orderedInterruption = true;
				animatorStateTransition55940.isExit = false;
				animatorStateTransition55940.mute = false;
				animatorStateTransition55940.solo = false;
				animatorStateTransition55940.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition55940.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition55940.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");

				var animatorStateTransition55942 = baseStateMachine1807513540.AddAnyStateTransition(freeClimbTopMountAnimatorState55668);
				animatorStateTransition55942.canTransitionToSelf = true;
				animatorStateTransition55942.duration = 0.15f;
				animatorStateTransition55942.exitTime = 0.75f;
				animatorStateTransition55942.hasExitTime = false;
				animatorStateTransition55942.hasFixedDuration = true;
				animatorStateTransition55942.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition55942.offset = 0f;
				animatorStateTransition55942.orderedInterruption = true;
				animatorStateTransition55942.isExit = false;
				animatorStateTransition55942.mute = false;
				animatorStateTransition55942.solo = false;
				animatorStateTransition55942.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition55942.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition55942.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");

				var animatorStateTransition55944 = baseStateMachine1807513540.AddAnyStateTransition(freeClimbIdleAnimatorState55672);
				animatorStateTransition55944.canTransitionToSelf = true;
				animatorStateTransition55944.duration = 0.15f;
				animatorStateTransition55944.exitTime = 0.75f;
				animatorStateTransition55944.hasExitTime = false;
				animatorStateTransition55944.hasFixedDuration = true;
				animatorStateTransition55944.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition55944.offset = 0f;
				animatorStateTransition55944.orderedInterruption = true;
				animatorStateTransition55944.isExit = false;
				animatorStateTransition55944.mute = false;
				animatorStateTransition55944.solo = false;
				animatorStateTransition55944.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition55944.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition55944.AddCondition(AnimatorConditionMode.Equals, 2f, "AbilityIntData");

				var animatorStateTransition55958 = baseStateMachine1807513540.AddAnyStateTransition(hangtoFreeClimbLeftAnimatorState56384);
				animatorStateTransition55958.canTransitionToSelf = false;
				animatorStateTransition55958.duration = 0.05f;
				animatorStateTransition55958.exitTime = 0.75f;
				animatorStateTransition55958.hasExitTime = false;
				animatorStateTransition55958.hasFixedDuration = true;
				animatorStateTransition55958.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition55958.offset = 0f;
				animatorStateTransition55958.orderedInterruption = true;
				animatorStateTransition55958.isExit = false;
				animatorStateTransition55958.mute = false;
				animatorStateTransition55958.solo = false;
				animatorStateTransition55958.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition55958.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition55958.AddCondition(AnimatorConditionMode.Equals, 7f, "AbilityIntData");
				animatorStateTransition55958.AddCondition(AnimatorConditionMode.Less, 0f, "HorizontalMovement");

				var animatorStateTransition55960 = baseStateMachine1807513540.AddAnyStateTransition(hangtoFreeClimbRightAnimatorState56386);
				animatorStateTransition55960.canTransitionToSelf = false;
				animatorStateTransition55960.duration = 0.05f;
				animatorStateTransition55960.exitTime = 0.75f;
				animatorStateTransition55960.hasExitTime = false;
				animatorStateTransition55960.hasFixedDuration = true;
				animatorStateTransition55960.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition55960.offset = 0f;
				animatorStateTransition55960.orderedInterruption = true;
				animatorStateTransition55960.isExit = false;
				animatorStateTransition55960.mute = false;
				animatorStateTransition55960.solo = false;
				animatorStateTransition55960.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition55960.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition55960.AddCondition(AnimatorConditionMode.Equals, 7f, "AbilityIntData");
				animatorStateTransition55960.AddCondition(AnimatorConditionMode.Greater, 0f, "HorizontalMovement");

				var animatorStateTransition55966 = baseStateMachine1807513540.AddAnyStateTransition(hangtoFreeClimbVerticalAnimatorState56388);
				animatorStateTransition55966.canTransitionToSelf = false;
				animatorStateTransition55966.duration = 0.05f;
				animatorStateTransition55966.exitTime = 0.75f;
				animatorStateTransition55966.hasExitTime = false;
				animatorStateTransition55966.hasFixedDuration = true;
				animatorStateTransition55966.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition55966.offset = 0f;
				animatorStateTransition55966.orderedInterruption = true;
				animatorStateTransition55966.isExit = false;
				animatorStateTransition55966.mute = false;
				animatorStateTransition55966.solo = false;
				animatorStateTransition55966.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition55966.AddCondition(AnimatorConditionMode.Equals, 503f, "AbilityIndex");
				animatorStateTransition55966.AddCondition(AnimatorConditionMode.Equals, 8f, "AbilityIntData");
			}
		}
	}
}
