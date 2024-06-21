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
	/// Implements AbilityDrawer for the Crawl ControlType.
	/// </summary>
	[ControlType(typeof(Crawl))]
	public class CrawlDrawer : AbilityDrawer
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

				var baseStateMachine366798342 = animatorControllers[i].layers[12].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine366798342.stateMachines.Length; ++k) {
						if (baseStateMachine366798342.stateMachines[k].stateMachine.name == "Crawl") {
							baseStateMachine366798342.RemoveStateMachine(baseStateMachine366798342.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var crawlBwdTurnLeftAnimationClip43438Path = AssetDatabase.GUIDToAssetPath("75b8972d94de91d4598aa090536b8daf"); 
				var crawlBwdTurnLeftAnimationClip43438 = AnimatorBuilder.GetAnimationClip(crawlBwdTurnLeftAnimationClip43438Path, "CrawlBwdTurnLeft");
				var crawlBwdAnimationClip43440Path = AssetDatabase.GUIDToAssetPath("e66f1ee8858e9694cbd519fd40d37b71"); 
				var crawlBwdAnimationClip43440 = AnimatorBuilder.GetAnimationClip(crawlBwdAnimationClip43440Path, "CrawlBwd");
				var crawlBwdTurnRightAnimationClip43442Path = AssetDatabase.GUIDToAssetPath("75b8972d94de91d4598aa090536b8daf"); 
				var crawlBwdTurnRightAnimationClip43442 = AnimatorBuilder.GetAnimationClip(crawlBwdTurnRightAnimationClip43442Path, "CrawlBwdTurnRight");
				var crawlStrafeAnimationClip43444Path = AssetDatabase.GUIDToAssetPath("416047b55078be24a9e0531434953979"); 
				var crawlStrafeAnimationClip43444 = AnimatorBuilder.GetAnimationClip(crawlStrafeAnimationClip43444Path, "CrawlStrafe");
				var crawlIdleAnimationClip43446Path = AssetDatabase.GUIDToAssetPath("dad2b6b732153c743bc5764038943df6"); 
				var crawlIdleAnimationClip43446 = AnimatorBuilder.GetAnimationClip(crawlIdleAnimationClip43446Path, "CrawlIdle");
				var crawlFwdTurnLeftAnimationClip43448Path = AssetDatabase.GUIDToAssetPath("75b8972d94de91d4598aa090536b8daf"); 
				var crawlFwdTurnLeftAnimationClip43448 = AnimatorBuilder.GetAnimationClip(crawlFwdTurnLeftAnimationClip43448Path, "CrawlFwdTurnLeft");
				var crawlFwdAnimationClip43450Path = AssetDatabase.GUIDToAssetPath("40930071f4f826547a10efb4ca391bd3"); 
				var crawlFwdAnimationClip43450 = AnimatorBuilder.GetAnimationClip(crawlFwdAnimationClip43450Path, "CrawlFwd");
				var crawlFwdTurnRightAnimationClip43452Path = AssetDatabase.GUIDToAssetPath("75b8972d94de91d4598aa090536b8daf"); 
				var crawlFwdTurnRightAnimationClip43452 = AnimatorBuilder.GetAnimationClip(crawlFwdTurnRightAnimationClip43452Path, "CrawlFwdTurnRight");
				var crawlStartAnimationClip43462Path = AssetDatabase.GUIDToAssetPath("87de1b796b9f8184ab60676491a00064"); 
				var crawlStartAnimationClip43462 = AnimatorBuilder.GetAnimationClip(crawlStartAnimationClip43462Path, "CrawlStart");
				var crawlCrouchStartAnimationClip43464Path = AssetDatabase.GUIDToAssetPath("d1b7e9ce6c89fc64581d452313b5d25b"); 
				var crawlCrouchStartAnimationClip43464 = AnimatorBuilder.GetAnimationClip(crawlCrouchStartAnimationClip43464Path, "CrawlCrouchStart");
				var crawlStopAnimationClip43472Path = AssetDatabase.GUIDToAssetPath("88d87302bce822749814fdaca20ed08d"); 
				var crawlStopAnimationClip43472 = AnimatorBuilder.GetAnimationClip(crawlStopAnimationClip43472Path, "CrawlStop");
				var crawlCrouchStopAnimationClip43474Path = AssetDatabase.GUIDToAssetPath("dd9fead20f0e0914b9cbb9b4edcfb7d3"); 
				var crawlCrouchStopAnimationClip43474 = AnimatorBuilder.GetAnimationClip(crawlCrouchStopAnimationClip43474Path, "CrawlCrouchStop");
				var crawlIdleTurnAnimationClip43482Path = AssetDatabase.GUIDToAssetPath("822d43966b2a09948b0887e36a4749ac"); 
				var crawlIdleTurnAnimationClip43482 = AnimatorBuilder.GetAnimationClip(crawlIdleTurnAnimationClip43482Path, "CrawlIdleTurn");

				// State Machine.
				var crawlAnimatorStateMachine41918 = baseStateMachine366798342.AddStateMachine("Crawl", new Vector3(624f, 156f, 0f));

				// States.
				var crawlMovementAnimatorState43426 = crawlAnimatorStateMachine41918.AddState("Crawl Movement", new Vector3(624f, 36f, 0f));
				var crawlMovementAnimatorState43426blendTreeBlendTree43436 = new BlendTree();
				AssetDatabase.AddObjectToAsset(crawlMovementAnimatorState43426blendTreeBlendTree43436, animatorControllers[i]);
				crawlMovementAnimatorState43426blendTreeBlendTree43436.hideFlags = HideFlags.HideInHierarchy;
				crawlMovementAnimatorState43426blendTreeBlendTree43436.blendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436.blendParameterY = "ForwardMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436.blendType = BlendTreeType.SimpleDirectional2D;
				crawlMovementAnimatorState43426blendTreeBlendTree43436.maxThreshold = 7f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436.minThreshold = -1f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436.name = "Blend Tree";
				crawlMovementAnimatorState43426blendTreeBlendTree43436.useAutomaticThresholds = false;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child0 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.motion = crawlBwdTurnLeftAnimationClip43438;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.position = new Vector2(-1f, -1f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.threshold = -1f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child0.timeScale = -1.25f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child1 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.motion = crawlBwdAnimationClip43440;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.position = new Vector2(0f, -1f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.threshold = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child1.timeScale = 1.25f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child2 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.motion = crawlBwdTurnRightAnimationClip43442;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.position = new Vector2(1f, -1f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.threshold = 1f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child2.timeScale = -1.25f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child3 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.motion = crawlStrafeAnimationClip43444;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.position = new Vector2(-1f, 0f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.threshold = 2f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child3.timeScale = 1.5f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child4 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.motion = crawlIdleAnimationClip43446;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.position = new Vector2(0f, 0f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.threshold = 3f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child4.timeScale = 1f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child5 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.motion = crawlStrafeAnimationClip43444;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.position = new Vector2(1f, 0f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.threshold = 4f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child5.timeScale = -1.5f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child6 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.motion = crawlFwdTurnLeftAnimationClip43448;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.position = new Vector2(-1f, 1f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.threshold = 5f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child6.timeScale = 1.5f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child7 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.motion = crawlFwdAnimationClip43450;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.position = new Vector2(0f, 1f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.threshold = 6f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child7.timeScale = 1.5f;
				var crawlMovementAnimatorState43426blendTreeBlendTree43436Child8 =  new ChildMotion();
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.motion = crawlFwdTurnRightAnimationClip43452;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.cycleOffset = 0f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.directBlendParameter = "HorizontalMovement";
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.mirror = false;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.position = new Vector2(1f, 1f);
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.threshold = 7f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436Child8.timeScale = 1.5f;
				crawlMovementAnimatorState43426blendTreeBlendTree43436.children = new ChildMotion[] {
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child0,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child1,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child2,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child3,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child4,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child5,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child6,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child7,
					crawlMovementAnimatorState43426blendTreeBlendTree43436Child8
				};
				crawlMovementAnimatorState43426.motion = crawlMovementAnimatorState43426blendTreeBlendTree43436;
				crawlMovementAnimatorState43426.cycleOffset = 0f;
				crawlMovementAnimatorState43426.cycleOffsetParameterActive = false;
				crawlMovementAnimatorState43426.iKOnFeet = true;
				crawlMovementAnimatorState43426.mirror = false;
				crawlMovementAnimatorState43426.mirrorParameterActive = false;
				crawlMovementAnimatorState43426.speed = 1f;
				crawlMovementAnimatorState43426.speedParameterActive = false;
				crawlMovementAnimatorState43426.writeDefaultValues = true;

				var crawlStartAnimatorState42244 = crawlAnimatorStateMachine41918.AddState("Crawl Start", new Vector3(264f, -108f, 0f));
				var crawlStartAnimatorState42244blendTreeBlendTree43460 = new BlendTree();
				AssetDatabase.AddObjectToAsset(crawlStartAnimatorState42244blendTreeBlendTree43460, animatorControllers[i]);
				crawlStartAnimatorState42244blendTreeBlendTree43460.hideFlags = HideFlags.HideInHierarchy;
				crawlStartAnimatorState42244blendTreeBlendTree43460.blendParameter = "Height";
				crawlStartAnimatorState42244blendTreeBlendTree43460.blendParameterY = "HorizontalMovement";
				crawlStartAnimatorState42244blendTreeBlendTree43460.blendType = BlendTreeType.Simple1D;
				crawlStartAnimatorState42244blendTreeBlendTree43460.maxThreshold = 1f;
				crawlStartAnimatorState42244blendTreeBlendTree43460.minThreshold = 0f;
				crawlStartAnimatorState42244blendTreeBlendTree43460.name = "Blend Tree";
				crawlStartAnimatorState42244blendTreeBlendTree43460.useAutomaticThresholds = false;
				var crawlStartAnimatorState42244blendTreeBlendTree43460Child0 =  new ChildMotion();
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.motion = crawlStartAnimationClip43462;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.cycleOffset = 0f;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.directBlendParameter = "HorizontalMovement";
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.mirror = false;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.position = new Vector2(0f, 0f);
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.threshold = 0f;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child0.timeScale = 1f;
				var crawlStartAnimatorState42244blendTreeBlendTree43460Child1 =  new ChildMotion();
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.motion = crawlCrouchStartAnimationClip43464;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.cycleOffset = 0f;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.directBlendParameter = "HorizontalMovement";
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.mirror = false;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.position = new Vector2(0f, 0f);
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.threshold = 1f;
				crawlStartAnimatorState42244blendTreeBlendTree43460Child1.timeScale = 1f;
				crawlStartAnimatorState42244blendTreeBlendTree43460.children = new ChildMotion[] {
					crawlStartAnimatorState42244blendTreeBlendTree43460Child0,
					crawlStartAnimatorState42244blendTreeBlendTree43460Child1
				};
				crawlStartAnimatorState42244.motion = crawlStartAnimatorState42244blendTreeBlendTree43460;
				crawlStartAnimatorState42244.cycleOffset = 0f;
				crawlStartAnimatorState42244.cycleOffsetParameterActive = false;
				crawlStartAnimatorState42244.iKOnFeet = true;
				crawlStartAnimatorState42244.mirror = false;
				crawlStartAnimatorState42244.mirrorParameterActive = false;
				crawlStartAnimatorState42244.speed = 1.5f;
				crawlStartAnimatorState42244.speedParameterActive = false;
				crawlStartAnimatorState42244.writeDefaultValues = true;

				var crawlStopAnimatorState43428 = crawlAnimatorStateMachine41918.AddState("Crawl Stop", new Vector3(624f, 168f, 0f));
				var crawlStopAnimatorState43428blendTreeBlendTree43470 = new BlendTree();
				AssetDatabase.AddObjectToAsset(crawlStopAnimatorState43428blendTreeBlendTree43470, animatorControllers[i]);
				crawlStopAnimatorState43428blendTreeBlendTree43470.hideFlags = HideFlags.HideInHierarchy;
				crawlStopAnimatorState43428blendTreeBlendTree43470.blendParameter = "Height";
				crawlStopAnimatorState43428blendTreeBlendTree43470.blendParameterY = "HorizontalMovement";
				crawlStopAnimatorState43428blendTreeBlendTree43470.blendType = BlendTreeType.Simple1D;
				crawlStopAnimatorState43428blendTreeBlendTree43470.maxThreshold = 1f;
				crawlStopAnimatorState43428blendTreeBlendTree43470.minThreshold = 0f;
				crawlStopAnimatorState43428blendTreeBlendTree43470.name = "Blend Tree";
				crawlStopAnimatorState43428blendTreeBlendTree43470.useAutomaticThresholds = false;
				var crawlStopAnimatorState43428blendTreeBlendTree43470Child0 =  new ChildMotion();
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.motion = crawlStopAnimationClip43472;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.cycleOffset = 0f;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.directBlendParameter = "HorizontalMovement";
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.mirror = false;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.position = new Vector2(0f, 0f);
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.threshold = 0f;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child0.timeScale = 1f;
				var crawlStopAnimatorState43428blendTreeBlendTree43470Child1 =  new ChildMotion();
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.motion = crawlCrouchStopAnimationClip43474;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.cycleOffset = 0f;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.directBlendParameter = "HorizontalMovement";
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.mirror = false;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.position = new Vector2(0f, 0f);
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.threshold = 1f;
				crawlStopAnimatorState43428blendTreeBlendTree43470Child1.timeScale = 1f;
				crawlStopAnimatorState43428blendTreeBlendTree43470.children = new ChildMotion[] {
					crawlStopAnimatorState43428blendTreeBlendTree43470Child0,
					crawlStopAnimatorState43428blendTreeBlendTree43470Child1
				};
				crawlStopAnimatorState43428.motion = crawlStopAnimatorState43428blendTreeBlendTree43470;
				crawlStopAnimatorState43428.cycleOffset = 0f;
				crawlStopAnimatorState43428.cycleOffsetParameterActive = false;
				crawlStopAnimatorState43428.iKOnFeet = true;
				crawlStopAnimatorState43428.mirror = false;
				crawlStopAnimatorState43428.mirrorParameterActive = false;
				crawlStopAnimatorState43428.speed = 1.5f;
				crawlStopAnimatorState43428.speedParameterActive = false;
				crawlStopAnimatorState43428.writeDefaultValues = true;

				var crawlIdleAnimatorState43430 = crawlAnimatorStateMachine41918.AddState("Crawl Idle", new Vector3(264f, 36f, 0f));
				var crawlIdleAnimatorState43430blendTreeBlendTree43480 = new BlendTree();
				AssetDatabase.AddObjectToAsset(crawlIdleAnimatorState43430blendTreeBlendTree43480, animatorControllers[i]);
				crawlIdleAnimatorState43430blendTreeBlendTree43480.hideFlags = HideFlags.HideInHierarchy;
				crawlIdleAnimatorState43430blendTreeBlendTree43480.blendParameter = "Yaw";
				crawlIdleAnimatorState43430blendTreeBlendTree43480.blendParameterY = "ForwardMovement";
				crawlIdleAnimatorState43430blendTreeBlendTree43480.blendType = BlendTreeType.Simple1D;
				crawlIdleAnimatorState43430blendTreeBlendTree43480.maxThreshold = 12f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480.minThreshold = -12f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480.name = "Blend Tree";
				crawlIdleAnimatorState43430blendTreeBlendTree43480.useAutomaticThresholds = false;
				var crawlIdleAnimatorState43430blendTreeBlendTree43480Child0 =  new ChildMotion();
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.motion = crawlIdleTurnAnimationClip43482;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.cycleOffset = 0f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.directBlendParameter = "HorizontalMovement";
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.mirror = false;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.position = new Vector2(-1f, 0f);
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.threshold = -12f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child0.timeScale = 2f;
				var crawlIdleAnimatorState43430blendTreeBlendTree43480Child1 =  new ChildMotion();
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.motion = crawlIdleTurnAnimationClip43482;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.cycleOffset = 0f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.directBlendParameter = "HorizontalMovement";
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.mirror = false;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.position = new Vector2(-0.96f, 0.26f);
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.threshold = -6f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child1.timeScale = 1f;
				var crawlIdleAnimatorState43430blendTreeBlendTree43480Child2 =  new ChildMotion();
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.motion = crawlIdleAnimationClip43446;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.cycleOffset = 0f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.directBlendParameter = "HorizontalMovement";
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.mirror = false;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.position = new Vector2(-0.87f, 0.5f);
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.threshold = 0f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child2.timeScale = 1f;
				var crawlIdleAnimatorState43430blendTreeBlendTree43480Child3 =  new ChildMotion();
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.motion = crawlIdleTurnAnimationClip43482;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.cycleOffset = 0f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.directBlendParameter = "HorizontalMovement";
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.mirror = false;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.position = new Vector2(-0.71f, 0.71f);
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.threshold = 6f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child3.timeScale = -1f;
				var crawlIdleAnimatorState43430blendTreeBlendTree43480Child4 =  new ChildMotion();
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.motion = crawlIdleTurnAnimationClip43482;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.cycleOffset = 0f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.directBlendParameter = "HorizontalMovement";
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.mirror = false;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.position = new Vector2(-0.5f, 0.87f);
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.threshold = 12f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480Child4.timeScale = -2f;
				crawlIdleAnimatorState43430blendTreeBlendTree43480.children = new ChildMotion[] {
					crawlIdleAnimatorState43430blendTreeBlendTree43480Child0,
					crawlIdleAnimatorState43430blendTreeBlendTree43480Child1,
					crawlIdleAnimatorState43430blendTreeBlendTree43480Child2,
					crawlIdleAnimatorState43430blendTreeBlendTree43480Child3,
					crawlIdleAnimatorState43430blendTreeBlendTree43480Child4
				};
				crawlIdleAnimatorState43430.motion = crawlIdleAnimatorState43430blendTreeBlendTree43480;
				crawlIdleAnimatorState43430.cycleOffset = 0f;
				crawlIdleAnimatorState43430.cycleOffsetParameterActive = false;
				crawlIdleAnimatorState43430.iKOnFeet = true;
				crawlIdleAnimatorState43430.mirror = false;
				crawlIdleAnimatorState43430.mirrorParameterActive = false;
				crawlIdleAnimatorState43430.speed = 1f;
				crawlIdleAnimatorState43430.speedParameterActive = false;
				crawlIdleAnimatorState43430.writeDefaultValues = true;

				// State Machine Defaults.
				crawlAnimatorStateMachine41918.anyStatePosition = new Vector3(48f, 48f, 0f);
				crawlAnimatorStateMachine41918.defaultState = crawlIdleAnimatorState43430;
				crawlAnimatorStateMachine41918.entryPosition = new Vector3(72f, -36f, 0f);
				crawlAnimatorStateMachine41918.exitPosition = new Vector3(876f, 48f, 0f);
				crawlAnimatorStateMachine41918.parentStateMachinePosition = new Vector3(852f, -48f, 0f);

				// State Transitions.
				var animatorStateTransition43432 = crawlMovementAnimatorState43426.AddTransition(crawlStopAnimatorState43428);
				animatorStateTransition43432.canTransitionToSelf = true;
				animatorStateTransition43432.duration = 0.15f;
				animatorStateTransition43432.exitTime = 0.92f;
				animatorStateTransition43432.hasExitTime = false;
				animatorStateTransition43432.hasFixedDuration = true;
				animatorStateTransition43432.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43432.offset = 0f;
				animatorStateTransition43432.orderedInterruption = true;
				animatorStateTransition43432.isExit = false;
				animatorStateTransition43432.mute = false;
				animatorStateTransition43432.solo = false;
				animatorStateTransition43432.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");

				var animatorStateTransition43434 = crawlMovementAnimatorState43426.AddTransition(crawlIdleAnimatorState43430);
				animatorStateTransition43434.canTransitionToSelf = true;
				animatorStateTransition43434.duration = 0.2f;
				animatorStateTransition43434.exitTime = 0.8849694f;
				animatorStateTransition43434.hasExitTime = false;
				animatorStateTransition43434.hasFixedDuration = true;
				animatorStateTransition43434.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43434.offset = 0f;
				animatorStateTransition43434.orderedInterruption = true;
				animatorStateTransition43434.isExit = false;
				animatorStateTransition43434.mute = false;
				animatorStateTransition43434.solo = false;
				animatorStateTransition43434.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition43454 = crawlStartAnimatorState42244.AddTransition(crawlMovementAnimatorState43426);
				animatorStateTransition43454.canTransitionToSelf = true;
				animatorStateTransition43454.duration = 0.2f;
				animatorStateTransition43454.exitTime = 0.8f;
				animatorStateTransition43454.hasExitTime = true;
				animatorStateTransition43454.hasFixedDuration = true;
				animatorStateTransition43454.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43454.offset = 0f;
				animatorStateTransition43454.orderedInterruption = true;
				animatorStateTransition43454.isExit = false;
				animatorStateTransition43454.mute = false;
				animatorStateTransition43454.solo = false;
				animatorStateTransition43454.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				var animatorStateTransition43456 = crawlStartAnimatorState42244.AddTransition(crawlIdleAnimatorState43430);
				animatorStateTransition43456.canTransitionToSelf = true;
				animatorStateTransition43456.duration = 0.2f;
				animatorStateTransition43456.exitTime = 0.8f;
				animatorStateTransition43456.hasExitTime = true;
				animatorStateTransition43456.hasFixedDuration = true;
				animatorStateTransition43456.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43456.offset = 0f;
				animatorStateTransition43456.orderedInterruption = true;
				animatorStateTransition43456.isExit = false;
				animatorStateTransition43456.mute = false;
				animatorStateTransition43456.solo = false;
				animatorStateTransition43456.AddCondition(AnimatorConditionMode.IfNot, 0f, "Moving");

				var animatorStateTransition43458 = crawlStartAnimatorState42244.AddTransition(crawlStopAnimatorState43428);
				animatorStateTransition43458.canTransitionToSelf = true;
				animatorStateTransition43458.duration = 0.15f;
				animatorStateTransition43458.exitTime = 0.8f;
				animatorStateTransition43458.hasExitTime = false;
				animatorStateTransition43458.hasFixedDuration = true;
				animatorStateTransition43458.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43458.offset = 0f;
				animatorStateTransition43458.orderedInterruption = true;
				animatorStateTransition43458.isExit = false;
				animatorStateTransition43458.mute = false;
				animatorStateTransition43458.solo = false;
				animatorStateTransition43458.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");

				var animatorStateTransition43466 = crawlStopAnimatorState43428.AddTransition(crawlStartAnimatorState42244);
				animatorStateTransition43466.canTransitionToSelf = true;
				animatorStateTransition43466.duration = 0.15f;
				animatorStateTransition43466.exitTime = 0.8f;
				animatorStateTransition43466.hasExitTime = false;
				animatorStateTransition43466.hasFixedDuration = true;
				animatorStateTransition43466.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43466.offset = 0f;
				animatorStateTransition43466.orderedInterruption = true;
				animatorStateTransition43466.isExit = false;
				animatorStateTransition43466.mute = false;
				animatorStateTransition43466.solo = false;
				animatorStateTransition43466.AddCondition(AnimatorConditionMode.Equals, 103f, "AbilityIndex");
				animatorStateTransition43466.AddCondition(AnimatorConditionMode.Equals, 0f, "AbilityIntData");

				var animatorStateTransition43468 = crawlStopAnimatorState43428.AddExitTransition();
				animatorStateTransition43468.canTransitionToSelf = true;
				animatorStateTransition43468.duration = 0.25f;
				animatorStateTransition43468.exitTime = 0.95f;
				animatorStateTransition43468.hasExitTime = false;
				animatorStateTransition43468.hasFixedDuration = true;
				animatorStateTransition43468.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43468.offset = 0f;
				animatorStateTransition43468.orderedInterruption = false;
				animatorStateTransition43468.isExit = true;
				animatorStateTransition43468.mute = false;
				animatorStateTransition43468.solo = false;
				animatorStateTransition43468.AddCondition(AnimatorConditionMode.NotEqual, 103f, "AbilityIndex");

				var animatorStateTransition43476 = crawlIdleAnimatorState43430.AddTransition(crawlMovementAnimatorState43426);
				animatorStateTransition43476.canTransitionToSelf = true;
				animatorStateTransition43476.duration = 0.2f;
				animatorStateTransition43476.exitTime = 0.8231132f;
				animatorStateTransition43476.hasExitTime = false;
				animatorStateTransition43476.hasFixedDuration = true;
				animatorStateTransition43476.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43476.offset = 0f;
				animatorStateTransition43476.orderedInterruption = true;
				animatorStateTransition43476.isExit = false;
				animatorStateTransition43476.mute = false;
				animatorStateTransition43476.solo = false;
				animatorStateTransition43476.AddCondition(AnimatorConditionMode.If, 0f, "Moving");

				var animatorStateTransition43478 = crawlIdleAnimatorState43430.AddTransition(crawlStopAnimatorState43428);
				animatorStateTransition43478.canTransitionToSelf = true;
				animatorStateTransition43478.duration = 0.15f;
				animatorStateTransition43478.exitTime = 0.8231132f;
				animatorStateTransition43478.hasExitTime = false;
				animatorStateTransition43478.hasFixedDuration = true;
				animatorStateTransition43478.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition43478.offset = 0f;
				animatorStateTransition43478.orderedInterruption = true;
				animatorStateTransition43478.isExit = false;
				animatorStateTransition43478.mute = false;
				animatorStateTransition43478.solo = false;
				animatorStateTransition43478.AddCondition(AnimatorConditionMode.Equals, 1f, "AbilityIntData");

				// State Machine Transitions.
				var animatorStateTransition42110 = baseStateMachine366798342.AddAnyStateTransition(crawlStartAnimatorState42244);
				animatorStateTransition42110.canTransitionToSelf = false;
				animatorStateTransition42110.duration = 0.15f;
				animatorStateTransition42110.exitTime = 0.75f;
				animatorStateTransition42110.hasExitTime = false;
				animatorStateTransition42110.hasFixedDuration = true;
				animatorStateTransition42110.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition42110.offset = 0f;
				animatorStateTransition42110.orderedInterruption = true;
				animatorStateTransition42110.isExit = false;
				animatorStateTransition42110.mute = false;
				animatorStateTransition42110.solo = false;
				animatorStateTransition42110.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition42110.AddCondition(AnimatorConditionMode.Equals, 103f, "AbilityIndex");
			}
		}
	}
}
