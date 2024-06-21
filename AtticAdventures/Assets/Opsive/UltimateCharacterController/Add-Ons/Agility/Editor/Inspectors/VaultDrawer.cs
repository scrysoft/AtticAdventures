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
	/// Implements AbilityDrawer for the Vault ControlType.
	/// </summary>
	[ControlType(typeof(Vault))]
	public class VaultDrawer : DetectObjectAbilityBaseDrawer
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

				var baseStateMachine345817176 = animatorControllers[i].layers[0].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine345817176.stateMachines.Length; ++k) {
						if (baseStateMachine345817176.stateMachines[k].stateMachine.name == "Vault") {
							baseStateMachine345817176.RemoveStateMachine(baseStateMachine345817176.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var vaultAnimationClip37748Path = AssetDatabase.GUIDToAssetPath("0399bfd67aded3d45b9a9478528c1759"); 
				var vaultAnimationClip37748 = AnimatorBuilder.GetAnimationClip(vaultAnimationClip37748Path, "Vault");
				var vaultWalkAnimationClip37750Path = AssetDatabase.GUIDToAssetPath("efb0ea01363a21e41adf6d34dd4ba46c"); 
				var vaultWalkAnimationClip37750 = AnimatorBuilder.GetAnimationClip(vaultWalkAnimationClip37750Path, "VaultWalk");
				var vaultRunAnimationClip37752Path = AssetDatabase.GUIDToAssetPath("258a82687d525724f8c461b7279ae336"); 
				var vaultRunAnimationClip37752 = AnimatorBuilder.GetAnimationClip(vaultRunAnimationClip37752Path, "VaultRun");

				// State Machine.
				var vaultAnimatorStateMachine34272 = baseStateMachine345817176.AddStateMachine("Vault", new Vector3(624f, 204f, 0f));

				// States.
				var vaultAnimatorState35186 = vaultAnimatorStateMachine34272.AddState("Vault", new Vector3(384f, 36f, 0f));
				var vaultAnimatorState35186blendTreeBlendTree37746 = new BlendTree();
				AssetDatabase.AddObjectToAsset(vaultAnimatorState35186blendTreeBlendTree37746, animatorControllers[i]);
				vaultAnimatorState35186blendTreeBlendTree37746.hideFlags = HideFlags.HideInHierarchy;
				vaultAnimatorState35186blendTreeBlendTree37746.blendParameter = "AbilityFloatData";
				vaultAnimatorState35186blendTreeBlendTree37746.blendParameterY = "HorizontalMovement";
				vaultAnimatorState35186blendTreeBlendTree37746.blendType = BlendTreeType.Simple1D;
				vaultAnimatorState35186blendTreeBlendTree37746.maxThreshold = 8f;
				vaultAnimatorState35186blendTreeBlendTree37746.minThreshold = 0f;
				vaultAnimatorState35186blendTreeBlendTree37746.name = "Blend Tree";
				vaultAnimatorState35186blendTreeBlendTree37746.useAutomaticThresholds = false;
				var vaultAnimatorState35186blendTreeBlendTree37746Child0 =  new ChildMotion();
				vaultAnimatorState35186blendTreeBlendTree37746Child0.motion = vaultAnimationClip37748;
				vaultAnimatorState35186blendTreeBlendTree37746Child0.cycleOffset = 0f;
				vaultAnimatorState35186blendTreeBlendTree37746Child0.directBlendParameter = "HorizontalMovement";
				vaultAnimatorState35186blendTreeBlendTree37746Child0.mirror = false;
				vaultAnimatorState35186blendTreeBlendTree37746Child0.position = new Vector2(0f, 0f);
				vaultAnimatorState35186blendTreeBlendTree37746Child0.threshold = 0f;
				vaultAnimatorState35186blendTreeBlendTree37746Child0.timeScale = 1.75f;
				var vaultAnimatorState35186blendTreeBlendTree37746Child1 =  new ChildMotion();
				vaultAnimatorState35186blendTreeBlendTree37746Child1.motion = vaultWalkAnimationClip37750;
				vaultAnimatorState35186blendTreeBlendTree37746Child1.cycleOffset = 0f;
				vaultAnimatorState35186blendTreeBlendTree37746Child1.directBlendParameter = "HorizontalMovement";
				vaultAnimatorState35186blendTreeBlendTree37746Child1.mirror = false;
				vaultAnimatorState35186blendTreeBlendTree37746Child1.position = new Vector2(0f, 0f);
				vaultAnimatorState35186blendTreeBlendTree37746Child1.threshold = 4f;
				vaultAnimatorState35186blendTreeBlendTree37746Child1.timeScale = 1.75f;
				var vaultAnimatorState35186blendTreeBlendTree37746Child2 =  new ChildMotion();
				vaultAnimatorState35186blendTreeBlendTree37746Child2.motion = vaultRunAnimationClip37752;
				vaultAnimatorState35186blendTreeBlendTree37746Child2.cycleOffset = 0f;
				vaultAnimatorState35186blendTreeBlendTree37746Child2.directBlendParameter = "HorizontalMovement";
				vaultAnimatorState35186blendTreeBlendTree37746Child2.mirror = false;
				vaultAnimatorState35186blendTreeBlendTree37746Child2.position = new Vector2(0f, 0f);
				vaultAnimatorState35186blendTreeBlendTree37746Child2.threshold = 8f;
				vaultAnimatorState35186blendTreeBlendTree37746Child2.timeScale = 1.75f;
				vaultAnimatorState35186blendTreeBlendTree37746.children = new ChildMotion[] {
					vaultAnimatorState35186blendTreeBlendTree37746Child0,
					vaultAnimatorState35186blendTreeBlendTree37746Child1,
					vaultAnimatorState35186blendTreeBlendTree37746Child2
				};
				vaultAnimatorState35186.motion = vaultAnimatorState35186blendTreeBlendTree37746;
				vaultAnimatorState35186.cycleOffset = 0f;
				vaultAnimatorState35186.cycleOffsetParameterActive = false;
				vaultAnimatorState35186.iKOnFeet = true;
				vaultAnimatorState35186.mirror = false;
				vaultAnimatorState35186.mirrorParameterActive = false;
				vaultAnimatorState35186.speed = 1f;
				vaultAnimatorState35186.speedParameterActive = false;
				vaultAnimatorState35186.writeDefaultValues = true;

				// State Machine Defaults.
				vaultAnimatorStateMachine34272.anyStatePosition = new Vector3(50f, 20f, 0f);
				vaultAnimatorStateMachine34272.defaultState = vaultAnimatorState35186;
				vaultAnimatorStateMachine34272.entryPosition = new Vector3(50f, 120f, 0f);
				vaultAnimatorStateMachine34272.exitPosition = new Vector3(800f, 120f, 0f);
				vaultAnimatorStateMachine34272.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

				// State Transitions.
				var animatorStateTransition37744 = vaultAnimatorState35186.AddExitTransition();
				animatorStateTransition37744.canTransitionToSelf = true;
				animatorStateTransition37744.duration = 0.15f;
				animatorStateTransition37744.exitTime = 0.99f;
				animatorStateTransition37744.hasExitTime = false;
				animatorStateTransition37744.hasFixedDuration = true;
				animatorStateTransition37744.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition37744.offset = 0f;
				animatorStateTransition37744.orderedInterruption = true;
				animatorStateTransition37744.isExit = true;
				animatorStateTransition37744.mute = false;
				animatorStateTransition37744.solo = false;
				animatorStateTransition37744.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition34860 = baseStateMachine345817176.AddAnyStateTransition(vaultAnimatorState35186);
				animatorStateTransition34860.canTransitionToSelf = false;
				animatorStateTransition34860.duration = 0.1f;
				animatorStateTransition34860.exitTime = 0.75f;
				animatorStateTransition34860.hasExitTime = false;
				animatorStateTransition34860.hasFixedDuration = true;
				animatorStateTransition34860.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition34860.offset = 0f;
				animatorStateTransition34860.orderedInterruption = true;
				animatorStateTransition34860.isExit = false;
				animatorStateTransition34860.mute = false;
				animatorStateTransition34860.solo = false;
				animatorStateTransition34860.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition34860.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");

				if (animatorControllers[i].layers.Length <= 5) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine362142470 = animatorControllers[i].layers[5].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine362142470.stateMachines.Length; ++k) {
						if (baseStateMachine362142470.stateMachines[k].stateMachine.name == "Vault") {
							baseStateMachine362142470.RemoveStateMachine(baseStateMachine362142470.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var assaultRifleVaultAnimationClip41038Path = AssetDatabase.GUIDToAssetPath("194b765e69b16a54c95f816909686574"); 
				var assaultRifleVaultAnimationClip41038 = AnimatorBuilder.GetAnimationClip(assaultRifleVaultAnimationClip41038Path, "AssaultRifleVault");
				var sniperRifleVaultAnimationClip41042Path = AssetDatabase.GUIDToAssetPath("5acbfc759a7379b438891af30302e377"); 
				var sniperRifleVaultAnimationClip41042 = AnimatorBuilder.GetAnimationClip(sniperRifleVaultAnimationClip41042Path, "SniperRifleVault");
				var pistolVaultAnimationClip41046Path = AssetDatabase.GUIDToAssetPath("6be7d0bcdc299f048902da3da3f6db84"); 
				var pistolVaultAnimationClip41046 = AnimatorBuilder.GetAnimationClip(pistolVaultAnimationClip41046Path, "PistolVault");
				var shieldVaultAnimationClip41050Path = AssetDatabase.GUIDToAssetPath("1bccb806fb9e01f40ae2a500cdc3cc6e"); 
				var shieldVaultAnimationClip41050 = AnimatorBuilder.GetAnimationClip(shieldVaultAnimationClip41050Path, "ShieldVault");
				var rocketLauncherVaultAnimationClip41054Path = AssetDatabase.GUIDToAssetPath("b1e8de7271784f84dbb18ac8391720d7"); 
				var rocketLauncherVaultAnimationClip41054 = AnimatorBuilder.GetAnimationClip(rocketLauncherVaultAnimationClip41054Path, "RocketLauncherVault");
				var dualPistolVaultAnimationClip41058Path = AssetDatabase.GUIDToAssetPath("f991d0293f754c442ad09f19059391c1"); 
				var dualPistolVaultAnimationClip41058 = AnimatorBuilder.GetAnimationClip(dualPistolVaultAnimationClip41058Path, "DualPistolVault");
				var shotgunVaultAnimationClip41062Path = AssetDatabase.GUIDToAssetPath("6b5a0abe350de1f46a9931f384ae8667"); 
				var shotgunVaultAnimationClip41062 = AnimatorBuilder.GetAnimationClip(shotgunVaultAnimationClip41062Path, "ShotgunVault");
				var bowVaultAnimationClip41066Path = AssetDatabase.GUIDToAssetPath("5a127a4aefca9f04797502ef9a805037"); 
				var bowVaultAnimationClip41066 = AnimatorBuilder.GetAnimationClip(bowVaultAnimationClip41066Path, "BowVault");

				// State Machine.
				var vaultAnimatorStateMachine40638 = baseStateMachine362142470.AddStateMachine("Vault", new Vector3(852f, 156f, 0f));

				// States.
				var assaultRifleAnimatorState40806 = vaultAnimatorStateMachine40638.AddState("Assault Rifle", new Vector3(384f, -48f, 0f));
				assaultRifleAnimatorState40806.motion = assaultRifleVaultAnimationClip41038;
				assaultRifleAnimatorState40806.cycleOffset = 0f;
				assaultRifleAnimatorState40806.cycleOffsetParameterActive = false;
				assaultRifleAnimatorState40806.iKOnFeet = false;
				assaultRifleAnimatorState40806.mirror = false;
				assaultRifleAnimatorState40806.mirrorParameterActive = false;
				assaultRifleAnimatorState40806.speed = 1.75f;
				assaultRifleAnimatorState40806.speedParameterActive = false;
				assaultRifleAnimatorState40806.writeDefaultValues = true;

				var sniperRifleAnimatorState40810 = vaultAnimatorStateMachine40638.AddState("Sniper Rifle", new Vector3(384f, 372f, 0f));
				sniperRifleAnimatorState40810.motion = sniperRifleVaultAnimationClip41042;
				sniperRifleAnimatorState40810.cycleOffset = 0f;
				sniperRifleAnimatorState40810.cycleOffsetParameterActive = false;
				sniperRifleAnimatorState40810.iKOnFeet = false;
				sniperRifleAnimatorState40810.mirror = false;
				sniperRifleAnimatorState40810.mirrorParameterActive = false;
				sniperRifleAnimatorState40810.speed = 1.75f;
				sniperRifleAnimatorState40810.speedParameterActive = false;
				sniperRifleAnimatorState40810.writeDefaultValues = true;

				var pistolAnimatorState40804 = vaultAnimatorStateMachine40638.AddState("Pistol", new Vector3(384f, 132f, 0f));
				pistolAnimatorState40804.motion = pistolVaultAnimationClip41046;
				pistolAnimatorState40804.cycleOffset = 0f;
				pistolAnimatorState40804.cycleOffsetParameterActive = false;
				pistolAnimatorState40804.iKOnFeet = false;
				pistolAnimatorState40804.mirror = false;
				pistolAnimatorState40804.mirrorParameterActive = false;
				pistolAnimatorState40804.speed = 1.75f;
				pistolAnimatorState40804.speedParameterActive = false;
				pistolAnimatorState40804.writeDefaultValues = true;

				var shieldAnimatorState40800 = vaultAnimatorStateMachine40638.AddState("Shield", new Vector3(384f, 252f, 0f));
				shieldAnimatorState40800.motion = shieldVaultAnimationClip41050;
				shieldAnimatorState40800.cycleOffset = 0f;
				shieldAnimatorState40800.cycleOffsetParameterActive = false;
				shieldAnimatorState40800.iKOnFeet = false;
				shieldAnimatorState40800.mirror = false;
				shieldAnimatorState40800.mirrorParameterActive = false;
				shieldAnimatorState40800.speed = 1.75f;
				shieldAnimatorState40800.speedParameterActive = false;
				shieldAnimatorState40800.writeDefaultValues = true;

				var rocketLauncherAnimatorState40814 = vaultAnimatorStateMachine40638.AddState("Rocket Launcher", new Vector3(384f, 192f, 0f));
				rocketLauncherAnimatorState40814.motion = rocketLauncherVaultAnimationClip41054;
				rocketLauncherAnimatorState40814.cycleOffset = 0f;
				rocketLauncherAnimatorState40814.cycleOffsetParameterActive = false;
				rocketLauncherAnimatorState40814.iKOnFeet = false;
				rocketLauncherAnimatorState40814.mirror = false;
				rocketLauncherAnimatorState40814.mirrorParameterActive = false;
				rocketLauncherAnimatorState40814.speed = 1.75f;
				rocketLauncherAnimatorState40814.speedParameterActive = false;
				rocketLauncherAnimatorState40814.writeDefaultValues = true;

				var dualPistolAnimatorState40802 = vaultAnimatorStateMachine40638.AddState("Dual Pistol", new Vector3(384f, 72f, 0f));
				dualPistolAnimatorState40802.motion = dualPistolVaultAnimationClip41058;
				dualPistolAnimatorState40802.cycleOffset = 0f;
				dualPistolAnimatorState40802.cycleOffsetParameterActive = false;
				dualPistolAnimatorState40802.iKOnFeet = false;
				dualPistolAnimatorState40802.mirror = false;
				dualPistolAnimatorState40802.mirrorParameterActive = false;
				dualPistolAnimatorState40802.speed = 1.75f;
				dualPistolAnimatorState40802.speedParameterActive = false;
				dualPistolAnimatorState40802.writeDefaultValues = true;

				var shotgunAnimatorState40812 = vaultAnimatorStateMachine40638.AddState("Shotgun", new Vector3(384f, 312f, 0f));
				shotgunAnimatorState40812.motion = shotgunVaultAnimationClip41062;
				shotgunAnimatorState40812.cycleOffset = 0f;
				shotgunAnimatorState40812.cycleOffsetParameterActive = false;
				shotgunAnimatorState40812.iKOnFeet = false;
				shotgunAnimatorState40812.mirror = false;
				shotgunAnimatorState40812.mirrorParameterActive = false;
				shotgunAnimatorState40812.speed = 1.75f;
				shotgunAnimatorState40812.speedParameterActive = false;
				shotgunAnimatorState40812.writeDefaultValues = true;

				var bowAnimatorState40808 = vaultAnimatorStateMachine40638.AddState("Bow", new Vector3(384f, 12f, 0f));
				bowAnimatorState40808.motion = bowVaultAnimationClip41066;
				bowAnimatorState40808.cycleOffset = 0f;
				bowAnimatorState40808.cycleOffsetParameterActive = false;
				bowAnimatorState40808.iKOnFeet = false;
				bowAnimatorState40808.mirror = false;
				bowAnimatorState40808.mirrorParameterActive = false;
				bowAnimatorState40808.speed = 1.75f;
				bowAnimatorState40808.speedParameterActive = false;
				bowAnimatorState40808.writeDefaultValues = true;

				// State Machine Defaults.
				vaultAnimatorStateMachine40638.anyStatePosition = new Vector3(48f, 144f, 0f);
				vaultAnimatorStateMachine40638.defaultState = assaultRifleAnimatorState40806;
				vaultAnimatorStateMachine40638.entryPosition = new Vector3(48f, 96f, 0f);
				vaultAnimatorStateMachine40638.exitPosition = new Vector3(780f, 156f, 0f);
				vaultAnimatorStateMachine40638.parentStateMachinePosition = new Vector3(756f, 72f, 0f);

				// State Transitions.
				var animatorStateTransition41036 = assaultRifleAnimatorState40806.AddExitTransition();
				animatorStateTransition41036.canTransitionToSelf = true;
				animatorStateTransition41036.duration = 0.15f;
				animatorStateTransition41036.exitTime = 0f;
				animatorStateTransition41036.hasExitTime = false;
				animatorStateTransition41036.hasFixedDuration = true;
				animatorStateTransition41036.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41036.offset = 0f;
				animatorStateTransition41036.orderedInterruption = true;
				animatorStateTransition41036.isExit = true;
				animatorStateTransition41036.mute = false;
				animatorStateTransition41036.solo = false;
				animatorStateTransition41036.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41040 = sniperRifleAnimatorState40810.AddExitTransition();
				animatorStateTransition41040.canTransitionToSelf = true;
				animatorStateTransition41040.duration = 0.15f;
				animatorStateTransition41040.exitTime = 0f;
				animatorStateTransition41040.hasExitTime = false;
				animatorStateTransition41040.hasFixedDuration = true;
				animatorStateTransition41040.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41040.offset = 0f;
				animatorStateTransition41040.orderedInterruption = true;
				animatorStateTransition41040.isExit = true;
				animatorStateTransition41040.mute = false;
				animatorStateTransition41040.solo = false;
				animatorStateTransition41040.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41044 = pistolAnimatorState40804.AddExitTransition();
				animatorStateTransition41044.canTransitionToSelf = true;
				animatorStateTransition41044.duration = 0.15f;
				animatorStateTransition41044.exitTime = 0f;
				animatorStateTransition41044.hasExitTime = false;
				animatorStateTransition41044.hasFixedDuration = true;
				animatorStateTransition41044.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41044.offset = 0f;
				animatorStateTransition41044.orderedInterruption = true;
				animatorStateTransition41044.isExit = true;
				animatorStateTransition41044.mute = false;
				animatorStateTransition41044.solo = false;
				animatorStateTransition41044.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41048 = shieldAnimatorState40800.AddExitTransition();
				animatorStateTransition41048.canTransitionToSelf = true;
				animatorStateTransition41048.duration = 0.15f;
				animatorStateTransition41048.exitTime = 0f;
				animatorStateTransition41048.hasExitTime = false;
				animatorStateTransition41048.hasFixedDuration = true;
				animatorStateTransition41048.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41048.offset = 0f;
				animatorStateTransition41048.orderedInterruption = true;
				animatorStateTransition41048.isExit = true;
				animatorStateTransition41048.mute = false;
				animatorStateTransition41048.solo = false;
				animatorStateTransition41048.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41052 = rocketLauncherAnimatorState40814.AddExitTransition();
				animatorStateTransition41052.canTransitionToSelf = true;
				animatorStateTransition41052.duration = 0.15f;
				animatorStateTransition41052.exitTime = 0f;
				animatorStateTransition41052.hasExitTime = false;
				animatorStateTransition41052.hasFixedDuration = true;
				animatorStateTransition41052.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41052.offset = 0f;
				animatorStateTransition41052.orderedInterruption = true;
				animatorStateTransition41052.isExit = true;
				animatorStateTransition41052.mute = false;
				animatorStateTransition41052.solo = false;
				animatorStateTransition41052.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41056 = dualPistolAnimatorState40802.AddExitTransition();
				animatorStateTransition41056.canTransitionToSelf = true;
				animatorStateTransition41056.duration = 0.15f;
				animatorStateTransition41056.exitTime = 0f;
				animatorStateTransition41056.hasExitTime = false;
				animatorStateTransition41056.hasFixedDuration = true;
				animatorStateTransition41056.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41056.offset = 0f;
				animatorStateTransition41056.orderedInterruption = true;
				animatorStateTransition41056.isExit = true;
				animatorStateTransition41056.mute = false;
				animatorStateTransition41056.solo = false;
				animatorStateTransition41056.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41060 = shotgunAnimatorState40812.AddExitTransition();
				animatorStateTransition41060.canTransitionToSelf = true;
				animatorStateTransition41060.duration = 0.15f;
				animatorStateTransition41060.exitTime = 0f;
				animatorStateTransition41060.hasExitTime = false;
				animatorStateTransition41060.hasFixedDuration = true;
				animatorStateTransition41060.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41060.offset = 0f;
				animatorStateTransition41060.orderedInterruption = true;
				animatorStateTransition41060.isExit = true;
				animatorStateTransition41060.mute = false;
				animatorStateTransition41060.solo = false;
				animatorStateTransition41060.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41064 = bowAnimatorState40808.AddExitTransition();
				animatorStateTransition41064.canTransitionToSelf = true;
				animatorStateTransition41064.duration = 0.15f;
				animatorStateTransition41064.exitTime = 0f;
				animatorStateTransition41064.hasExitTime = false;
				animatorStateTransition41064.hasFixedDuration = true;
				animatorStateTransition41064.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41064.offset = 0f;
				animatorStateTransition41064.orderedInterruption = true;
				animatorStateTransition41064.isExit = true;
				animatorStateTransition41064.mute = false;
				animatorStateTransition41064.solo = false;
				animatorStateTransition41064.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition40720 = baseStateMachine362142470.AddAnyStateTransition(shieldAnimatorState40800);
				animatorStateTransition40720.canTransitionToSelf = false;
				animatorStateTransition40720.duration = 0.1f;
				animatorStateTransition40720.exitTime = 0.75f;
				animatorStateTransition40720.hasExitTime = false;
				animatorStateTransition40720.hasFixedDuration = true;
				animatorStateTransition40720.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40720.offset = 0f;
				animatorStateTransition40720.orderedInterruption = true;
				animatorStateTransition40720.isExit = false;
				animatorStateTransition40720.mute = false;
				animatorStateTransition40720.solo = false;
				animatorStateTransition40720.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40720.AddCondition(AnimatorConditionMode.Equals, 25f, "Slot1ItemID");

				var animatorStateTransition40722 = baseStateMachine362142470.AddAnyStateTransition(dualPistolAnimatorState40802);
				animatorStateTransition40722.canTransitionToSelf = false;
				animatorStateTransition40722.duration = 0.1f;
				animatorStateTransition40722.exitTime = 0.75f;
				animatorStateTransition40722.hasExitTime = false;
				animatorStateTransition40722.hasFixedDuration = true;
				animatorStateTransition40722.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40722.offset = 0f;
				animatorStateTransition40722.orderedInterruption = true;
				animatorStateTransition40722.isExit = false;
				animatorStateTransition40722.mute = false;
				animatorStateTransition40722.solo = false;
				animatorStateTransition40722.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40722.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition40722.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot1ItemID");

				var animatorStateTransition40724 = baseStateMachine362142470.AddAnyStateTransition(pistolAnimatorState40804);
				animatorStateTransition40724.canTransitionToSelf = false;
				animatorStateTransition40724.duration = 0.1f;
				animatorStateTransition40724.exitTime = 0.75f;
				animatorStateTransition40724.hasExitTime = false;
				animatorStateTransition40724.hasFixedDuration = true;
				animatorStateTransition40724.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40724.offset = 0f;
				animatorStateTransition40724.orderedInterruption = true;
				animatorStateTransition40724.isExit = false;
				animatorStateTransition40724.mute = false;
				animatorStateTransition40724.solo = false;
				animatorStateTransition40724.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40724.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition40724.AddCondition(AnimatorConditionMode.NotEqual, 2f, "Slot1ItemID");

				var animatorStateTransition40726 = baseStateMachine362142470.AddAnyStateTransition(assaultRifleAnimatorState40806);
				animatorStateTransition40726.canTransitionToSelf = false;
				animatorStateTransition40726.duration = 0.1f;
				animatorStateTransition40726.exitTime = 0.75f;
				animatorStateTransition40726.hasExitTime = false;
				animatorStateTransition40726.hasFixedDuration = true;
				animatorStateTransition40726.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40726.offset = 0f;
				animatorStateTransition40726.orderedInterruption = true;
				animatorStateTransition40726.isExit = false;
				animatorStateTransition40726.mute = false;
				animatorStateTransition40726.solo = false;
				animatorStateTransition40726.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40726.AddCondition(AnimatorConditionMode.Equals, 1f, "Slot0ItemID");

				var animatorStateTransition40728 = baseStateMachine362142470.AddAnyStateTransition(bowAnimatorState40808);
				animatorStateTransition40728.canTransitionToSelf = false;
				animatorStateTransition40728.duration = 0.1f;
				animatorStateTransition40728.exitTime = 0.75f;
				animatorStateTransition40728.hasExitTime = false;
				animatorStateTransition40728.hasFixedDuration = true;
				animatorStateTransition40728.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40728.offset = 0f;
				animatorStateTransition40728.orderedInterruption = true;
				animatorStateTransition40728.isExit = false;
				animatorStateTransition40728.mute = false;
				animatorStateTransition40728.solo = false;
				animatorStateTransition40728.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40728.AddCondition(AnimatorConditionMode.Equals, 4f, "Slot1ItemID");

				var animatorStateTransition40730 = baseStateMachine362142470.AddAnyStateTransition(sniperRifleAnimatorState40810);
				animatorStateTransition40730.canTransitionToSelf = false;
				animatorStateTransition40730.duration = 0.1f;
				animatorStateTransition40730.exitTime = 0.75f;
				animatorStateTransition40730.hasExitTime = false;
				animatorStateTransition40730.hasFixedDuration = true;
				animatorStateTransition40730.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40730.offset = 0f;
				animatorStateTransition40730.orderedInterruption = true;
				animatorStateTransition40730.isExit = false;
				animatorStateTransition40730.mute = false;
				animatorStateTransition40730.solo = false;
				animatorStateTransition40730.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40730.AddCondition(AnimatorConditionMode.Equals, 5f, "Slot0ItemID");

				var animatorStateTransition40732 = baseStateMachine362142470.AddAnyStateTransition(shotgunAnimatorState40812);
				animatorStateTransition40732.canTransitionToSelf = false;
				animatorStateTransition40732.duration = 0.1f;
				animatorStateTransition40732.exitTime = 0.75f;
				animatorStateTransition40732.hasExitTime = false;
				animatorStateTransition40732.hasFixedDuration = true;
				animatorStateTransition40732.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40732.offset = 0f;
				animatorStateTransition40732.orderedInterruption = true;
				animatorStateTransition40732.isExit = false;
				animatorStateTransition40732.mute = false;
				animatorStateTransition40732.solo = false;
				animatorStateTransition40732.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40732.AddCondition(AnimatorConditionMode.Equals, 3f, "Slot0ItemID");

				var animatorStateTransition40734 = baseStateMachine362142470.AddAnyStateTransition(rocketLauncherAnimatorState40814);
				animatorStateTransition40734.canTransitionToSelf = false;
				animatorStateTransition40734.duration = 0.1f;
				animatorStateTransition40734.exitTime = 0.75f;
				animatorStateTransition40734.hasExitTime = false;
				animatorStateTransition40734.hasFixedDuration = true;
				animatorStateTransition40734.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition40734.offset = 0f;
				animatorStateTransition40734.orderedInterruption = true;
				animatorStateTransition40734.isExit = false;
				animatorStateTransition40734.mute = false;
				animatorStateTransition40734.solo = false;
				animatorStateTransition40734.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition40734.AddCondition(AnimatorConditionMode.Equals, 6f, "Slot0ItemID");

				if (animatorControllers[i].layers.Length <= 6) {
					Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
					return;
				}

				var baseStateMachine310764996 = animatorControllers[i].layers[6].stateMachine;

				// The state machine should start fresh.
				for (int j = 0; j < animatorControllers[i].layers.Length; ++j) {
					for (int k = 0; k < baseStateMachine310764996.stateMachines.Length; ++k) {
						if (baseStateMachine310764996.stateMachines[k].stateMachine.name == "Vault") {
							baseStateMachine310764996.RemoveStateMachine(baseStateMachine310764996.stateMachines[k].stateMachine);
							break;
						}
					}
				}

				// AnimationClip references.
				var swordVaultAnimationClip41510Path = AssetDatabase.GUIDToAssetPath("120b0f3e59a781a44b765e2205535382"); 
				var swordVaultAnimationClip41510 = AnimatorBuilder.GetAnimationClip(swordVaultAnimationClip41510Path, "SwordVault");
				var katanaVaultAnimationClip41526Path = AssetDatabase.GUIDToAssetPath("f64e92e464549d343ac586eb3ca9f381"); 
				var katanaVaultAnimationClip41526 = AnimatorBuilder.GetAnimationClip(katanaVaultAnimationClip41526Path, "KatanaVault");
				var knifeVaultAnimationClip41532Path = AssetDatabase.GUIDToAssetPath("2c917cc202a5edc4e8dda8662217d2d5"); 
				var knifeVaultAnimationClip41532 = AnimatorBuilder.GetAnimationClip(knifeVaultAnimationClip41532Path, "KnifeVault");
				var fragGrenadeVaultAnimationClip41536Path = AssetDatabase.GUIDToAssetPath("ed618ef5a99858840b36d7837d93418a"); 
				var fragGrenadeVaultAnimationClip41536 = AnimatorBuilder.GetAnimationClip(fragGrenadeVaultAnimationClip41536Path, "FragGrenadeVault");

				// State Machine.
				var vaultAnimatorStateMachine41084 = baseStateMachine310764996.AddStateMachine("Vault", new Vector3(852f, 108f, 0f));

				// States.
				var swordAnimatorState41290 = vaultAnimatorStateMachine41084.AddState("Sword", new Vector3(384f, 372f, 0f));
				swordAnimatorState41290.motion = swordVaultAnimationClip41510;
				swordAnimatorState41290.cycleOffset = 0f;
				swordAnimatorState41290.cycleOffsetParameterActive = false;
				swordAnimatorState41290.iKOnFeet = false;
				swordAnimatorState41290.mirror = false;
				swordAnimatorState41290.mirrorParameterActive = false;
				swordAnimatorState41290.speed = 1.75f;
				swordAnimatorState41290.speedParameterActive = false;
				swordAnimatorState41290.writeDefaultValues = true;

				var shotgunAnimatorState41300 = vaultAnimatorStateMachine41084.AddState("Shotgun", new Vector3(384f, 252f, 0f));
				shotgunAnimatorState41300.motion = shotgunVaultAnimationClip41062;
				shotgunAnimatorState41300.cycleOffset = 0f;
				shotgunAnimatorState41300.cycleOffsetParameterActive = false;
				shotgunAnimatorState41300.iKOnFeet = false;
				shotgunAnimatorState41300.mirror = false;
				shotgunAnimatorState41300.mirrorParameterActive = false;
				shotgunAnimatorState41300.speed = 1.75f;
				shotgunAnimatorState41300.speedParameterActive = false;
				shotgunAnimatorState41300.writeDefaultValues = true;

				var pistolAnimatorState41302 = vaultAnimatorStateMachine41084.AddState("Pistol", new Vector3(384f, 132f, 0f));
				pistolAnimatorState41302.motion = pistolVaultAnimationClip41046;
				pistolAnimatorState41302.cycleOffset = 0f;
				pistolAnimatorState41302.cycleOffsetParameterActive = false;
				pistolAnimatorState41302.iKOnFeet = false;
				pistolAnimatorState41302.mirror = false;
				pistolAnimatorState41302.mirrorParameterActive = false;
				pistolAnimatorState41302.speed = 1.75f;
				pistolAnimatorState41302.speedParameterActive = false;
				pistolAnimatorState41302.writeDefaultValues = true;

				var sniperRifleAnimatorState41308 = vaultAnimatorStateMachine41084.AddState("Sniper Rifle", new Vector3(384f, 312f, 0f));
				sniperRifleAnimatorState41308.motion = sniperRifleVaultAnimationClip41042;
				sniperRifleAnimatorState41308.cycleOffset = 0f;
				sniperRifleAnimatorState41308.cycleOffsetParameterActive = false;
				sniperRifleAnimatorState41308.iKOnFeet = false;
				sniperRifleAnimatorState41308.mirror = false;
				sniperRifleAnimatorState41308.mirrorParameterActive = false;
				sniperRifleAnimatorState41308.speed = 1.75f;
				sniperRifleAnimatorState41308.speedParameterActive = false;
				sniperRifleAnimatorState41308.writeDefaultValues = true;

				var dualPistolAnimatorState41298 = vaultAnimatorStateMachine41084.AddState("Dual Pistol", new Vector3(384f, -108f, 0f));
				dualPistolAnimatorState41298.motion = dualPistolVaultAnimationClip41058;
				dualPistolAnimatorState41298.cycleOffset = 0f;
				dualPistolAnimatorState41298.cycleOffsetParameterActive = false;
				dualPistolAnimatorState41298.iKOnFeet = false;
				dualPistolAnimatorState41298.mirror = false;
				dualPistolAnimatorState41298.mirrorParameterActive = false;
				dualPistolAnimatorState41298.speed = 1.75f;
				dualPistolAnimatorState41298.speedParameterActive = false;
				dualPistolAnimatorState41298.writeDefaultValues = true;

				var assaultRifleAnimatorState41306 = vaultAnimatorStateMachine41084.AddState("Assault Rifle", new Vector3(384f, -228f, 0f));
				assaultRifleAnimatorState41306.motion = assaultRifleVaultAnimationClip41038;
				assaultRifleAnimatorState41306.cycleOffset = 0f;
				assaultRifleAnimatorState41306.cycleOffsetParameterActive = false;
				assaultRifleAnimatorState41306.iKOnFeet = false;
				assaultRifleAnimatorState41306.mirror = false;
				assaultRifleAnimatorState41306.mirrorParameterActive = false;
				assaultRifleAnimatorState41306.speed = 1.75f;
				assaultRifleAnimatorState41306.speedParameterActive = false;
				assaultRifleAnimatorState41306.writeDefaultValues = true;

				var rocketLauncherAnimatorState41304 = vaultAnimatorStateMachine41084.AddState("Rocket Launcher", new Vector3(384f, 192f, 0f));
				rocketLauncherAnimatorState41304.motion = rocketLauncherVaultAnimationClip41054;
				rocketLauncherAnimatorState41304.cycleOffset = 0f;
				rocketLauncherAnimatorState41304.cycleOffsetParameterActive = false;
				rocketLauncherAnimatorState41304.iKOnFeet = false;
				rocketLauncherAnimatorState41304.mirror = false;
				rocketLauncherAnimatorState41304.mirrorParameterActive = false;
				rocketLauncherAnimatorState41304.speed = 1.75f;
				rocketLauncherAnimatorState41304.speedParameterActive = false;
				rocketLauncherAnimatorState41304.writeDefaultValues = true;

				var katanaAnimatorState41294 = vaultAnimatorStateMachine41084.AddState("Katana", new Vector3(384f, 12f, 0f));
				katanaAnimatorState41294.motion = katanaVaultAnimationClip41526;
				katanaAnimatorState41294.cycleOffset = 0f;
				katanaAnimatorState41294.cycleOffsetParameterActive = false;
				katanaAnimatorState41294.iKOnFeet = false;
				katanaAnimatorState41294.mirror = false;
				katanaAnimatorState41294.mirrorParameterActive = false;
				katanaAnimatorState41294.speed = 1.75f;
				katanaAnimatorState41294.speedParameterActive = false;
				katanaAnimatorState41294.writeDefaultValues = true;

				var bowAnimatorState41296 = vaultAnimatorStateMachine41084.AddState("Bow", new Vector3(384f, -168f, 0f));
				bowAnimatorState41296.motion = bowVaultAnimationClip41066;
				bowAnimatorState41296.cycleOffset = 0f;
				bowAnimatorState41296.cycleOffsetParameterActive = false;
				bowAnimatorState41296.iKOnFeet = false;
				bowAnimatorState41296.mirror = false;
				bowAnimatorState41296.mirrorParameterActive = false;
				bowAnimatorState41296.speed = 1.75f;
				bowAnimatorState41296.speedParameterActive = false;
				bowAnimatorState41296.writeDefaultValues = true;

				var knifeAnimatorState41292 = vaultAnimatorStateMachine41084.AddState("Knife", new Vector3(384f, 72f, 0f));
				knifeAnimatorState41292.motion = knifeVaultAnimationClip41532;
				knifeAnimatorState41292.cycleOffset = 0f;
				knifeAnimatorState41292.cycleOffsetParameterActive = false;
				knifeAnimatorState41292.iKOnFeet = false;
				knifeAnimatorState41292.mirror = false;
				knifeAnimatorState41292.mirrorParameterActive = false;
				knifeAnimatorState41292.speed = 1.75f;
				knifeAnimatorState41292.speedParameterActive = false;
				knifeAnimatorState41292.writeDefaultValues = true;

				var fragGrenadeAnimatorState41310 = vaultAnimatorStateMachine41084.AddState("Frag Grenade", new Vector3(384f, -48f, 0f));
				fragGrenadeAnimatorState41310.motion = fragGrenadeVaultAnimationClip41536;
				fragGrenadeAnimatorState41310.cycleOffset = 0f;
				fragGrenadeAnimatorState41310.cycleOffsetParameterActive = false;
				fragGrenadeAnimatorState41310.iKOnFeet = false;
				fragGrenadeAnimatorState41310.mirror = false;
				fragGrenadeAnimatorState41310.mirrorParameterActive = false;
				fragGrenadeAnimatorState41310.speed = 1.75f;
				fragGrenadeAnimatorState41310.speedParameterActive = false;
				fragGrenadeAnimatorState41310.writeDefaultValues = true;

				// State Machine Defaults.
				vaultAnimatorStateMachine41084.anyStatePosition = new Vector3(48f, 72f, 0f);
				vaultAnimatorStateMachine41084.defaultState = assaultRifleAnimatorState41306;
				vaultAnimatorStateMachine41084.entryPosition = new Vector3(48f, 24f, 0f);
				vaultAnimatorStateMachine41084.exitPosition = new Vector3(768f, 72f, 0f);
				vaultAnimatorStateMachine41084.parentStateMachinePosition = new Vector3(756f, 0f, 0f);

				// State Transitions.
				var animatorStateTransition41508 = swordAnimatorState41290.AddExitTransition();
				animatorStateTransition41508.canTransitionToSelf = true;
				animatorStateTransition41508.duration = 0.15f;
				animatorStateTransition41508.exitTime = 0f;
				animatorStateTransition41508.hasExitTime = false;
				animatorStateTransition41508.hasFixedDuration = true;
				animatorStateTransition41508.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41508.offset = 0f;
				animatorStateTransition41508.orderedInterruption = true;
				animatorStateTransition41508.isExit = true;
				animatorStateTransition41508.mute = false;
				animatorStateTransition41508.solo = false;
				animatorStateTransition41508.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41512 = shotgunAnimatorState41300.AddExitTransition();
				animatorStateTransition41512.canTransitionToSelf = true;
				animatorStateTransition41512.duration = 0.15f;
				animatorStateTransition41512.exitTime = 0f;
				animatorStateTransition41512.hasExitTime = false;
				animatorStateTransition41512.hasFixedDuration = true;
				animatorStateTransition41512.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41512.offset = 0f;
				animatorStateTransition41512.orderedInterruption = true;
				animatorStateTransition41512.isExit = true;
				animatorStateTransition41512.mute = false;
				animatorStateTransition41512.solo = false;
				animatorStateTransition41512.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41514 = pistolAnimatorState41302.AddExitTransition();
				animatorStateTransition41514.canTransitionToSelf = true;
				animatorStateTransition41514.duration = 0.15f;
				animatorStateTransition41514.exitTime = 0f;
				animatorStateTransition41514.hasExitTime = false;
				animatorStateTransition41514.hasFixedDuration = true;
				animatorStateTransition41514.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41514.offset = 0f;
				animatorStateTransition41514.orderedInterruption = true;
				animatorStateTransition41514.isExit = true;
				animatorStateTransition41514.mute = false;
				animatorStateTransition41514.solo = false;
				animatorStateTransition41514.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41516 = sniperRifleAnimatorState41308.AddExitTransition();
				animatorStateTransition41516.canTransitionToSelf = true;
				animatorStateTransition41516.duration = 0.15f;
				animatorStateTransition41516.exitTime = 0f;
				animatorStateTransition41516.hasExitTime = false;
				animatorStateTransition41516.hasFixedDuration = true;
				animatorStateTransition41516.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41516.offset = 0f;
				animatorStateTransition41516.orderedInterruption = true;
				animatorStateTransition41516.isExit = true;
				animatorStateTransition41516.mute = false;
				animatorStateTransition41516.solo = false;
				animatorStateTransition41516.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41518 = dualPistolAnimatorState41298.AddExitTransition();
				animatorStateTransition41518.canTransitionToSelf = true;
				animatorStateTransition41518.duration = 0.15f;
				animatorStateTransition41518.exitTime = 0f;
				animatorStateTransition41518.hasExitTime = false;
				animatorStateTransition41518.hasFixedDuration = true;
				animatorStateTransition41518.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41518.offset = 0f;
				animatorStateTransition41518.orderedInterruption = true;
				animatorStateTransition41518.isExit = true;
				animatorStateTransition41518.mute = false;
				animatorStateTransition41518.solo = false;
				animatorStateTransition41518.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41520 = assaultRifleAnimatorState41306.AddExitTransition();
				animatorStateTransition41520.canTransitionToSelf = true;
				animatorStateTransition41520.duration = 0.15f;
				animatorStateTransition41520.exitTime = 0f;
				animatorStateTransition41520.hasExitTime = false;
				animatorStateTransition41520.hasFixedDuration = true;
				animatorStateTransition41520.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41520.offset = 0f;
				animatorStateTransition41520.orderedInterruption = true;
				animatorStateTransition41520.isExit = true;
				animatorStateTransition41520.mute = false;
				animatorStateTransition41520.solo = false;
				animatorStateTransition41520.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41522 = rocketLauncherAnimatorState41304.AddExitTransition();
				animatorStateTransition41522.canTransitionToSelf = true;
				animatorStateTransition41522.duration = 0.15f;
				animatorStateTransition41522.exitTime = 0f;
				animatorStateTransition41522.hasExitTime = false;
				animatorStateTransition41522.hasFixedDuration = true;
				animatorStateTransition41522.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41522.offset = 0f;
				animatorStateTransition41522.orderedInterruption = true;
				animatorStateTransition41522.isExit = true;
				animatorStateTransition41522.mute = false;
				animatorStateTransition41522.solo = false;
				animatorStateTransition41522.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41524 = katanaAnimatorState41294.AddExitTransition();
				animatorStateTransition41524.canTransitionToSelf = true;
				animatorStateTransition41524.duration = 0.15f;
				animatorStateTransition41524.exitTime = 0f;
				animatorStateTransition41524.hasExitTime = false;
				animatorStateTransition41524.hasFixedDuration = true;
				animatorStateTransition41524.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41524.offset = 0f;
				animatorStateTransition41524.orderedInterruption = true;
				animatorStateTransition41524.isExit = true;
				animatorStateTransition41524.mute = false;
				animatorStateTransition41524.solo = false;
				animatorStateTransition41524.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41528 = bowAnimatorState41296.AddExitTransition();
				animatorStateTransition41528.canTransitionToSelf = true;
				animatorStateTransition41528.duration = 0.15f;
				animatorStateTransition41528.exitTime = 0f;
				animatorStateTransition41528.hasExitTime = false;
				animatorStateTransition41528.hasFixedDuration = true;
				animatorStateTransition41528.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41528.offset = 0f;
				animatorStateTransition41528.orderedInterruption = true;
				animatorStateTransition41528.isExit = true;
				animatorStateTransition41528.mute = false;
				animatorStateTransition41528.solo = false;
				animatorStateTransition41528.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41530 = knifeAnimatorState41292.AddExitTransition();
				animatorStateTransition41530.canTransitionToSelf = true;
				animatorStateTransition41530.duration = 0.15f;
				animatorStateTransition41530.exitTime = 0f;
				animatorStateTransition41530.hasExitTime = false;
				animatorStateTransition41530.hasFixedDuration = true;
				animatorStateTransition41530.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41530.offset = 0f;
				animatorStateTransition41530.orderedInterruption = true;
				animatorStateTransition41530.isExit = true;
				animatorStateTransition41530.mute = false;
				animatorStateTransition41530.solo = false;
				animatorStateTransition41530.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				var animatorStateTransition41534 = fragGrenadeAnimatorState41310.AddExitTransition();
				animatorStateTransition41534.canTransitionToSelf = true;
				animatorStateTransition41534.duration = 0.15f;
				animatorStateTransition41534.exitTime = 0f;
				animatorStateTransition41534.hasExitTime = false;
				animatorStateTransition41534.hasFixedDuration = true;
				animatorStateTransition41534.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41534.offset = 0f;
				animatorStateTransition41534.orderedInterruption = true;
				animatorStateTransition41534.isExit = true;
				animatorStateTransition41534.mute = false;
				animatorStateTransition41534.solo = false;
				animatorStateTransition41534.AddCondition(AnimatorConditionMode.NotEqual, 105f, "AbilityIndex");

				// State Machine Transitions.
				var animatorStateTransition41178 = baseStateMachine310764996.AddAnyStateTransition(swordAnimatorState41290);
				animatorStateTransition41178.canTransitionToSelf = false;
				animatorStateTransition41178.duration = 0.1f;
				animatorStateTransition41178.exitTime = 0.75f;
				animatorStateTransition41178.hasExitTime = false;
				animatorStateTransition41178.hasFixedDuration = true;
				animatorStateTransition41178.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41178.offset = 0f;
				animatorStateTransition41178.orderedInterruption = true;
				animatorStateTransition41178.isExit = false;
				animatorStateTransition41178.mute = false;
				animatorStateTransition41178.solo = false;
				animatorStateTransition41178.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41178.AddCondition(AnimatorConditionMode.Equals, 22f, "Slot0ItemID");

				var animatorStateTransition41180 = baseStateMachine310764996.AddAnyStateTransition(knifeAnimatorState41292);
				animatorStateTransition41180.canTransitionToSelf = false;
				animatorStateTransition41180.duration = 0.1f;
				animatorStateTransition41180.exitTime = 0.75f;
				animatorStateTransition41180.hasExitTime = false;
				animatorStateTransition41180.hasFixedDuration = true;
				animatorStateTransition41180.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41180.offset = 0f;
				animatorStateTransition41180.orderedInterruption = true;
				animatorStateTransition41180.isExit = false;
				animatorStateTransition41180.mute = false;
				animatorStateTransition41180.solo = false;
				animatorStateTransition41180.AddCondition(AnimatorConditionMode.If, 0f, "AbilityChange");
				animatorStateTransition41180.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41180.AddCondition(AnimatorConditionMode.Equals, 23f, "Slot0ItemID");

				var animatorStateTransition41182 = baseStateMachine310764996.AddAnyStateTransition(katanaAnimatorState41294);
				animatorStateTransition41182.canTransitionToSelf = false;
				animatorStateTransition41182.duration = 0.1f;
				animatorStateTransition41182.exitTime = 0.75f;
				animatorStateTransition41182.hasExitTime = false;
				animatorStateTransition41182.hasFixedDuration = true;
				animatorStateTransition41182.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41182.offset = 0f;
				animatorStateTransition41182.orderedInterruption = true;
				animatorStateTransition41182.isExit = false;
				animatorStateTransition41182.mute = false;
				animatorStateTransition41182.solo = false;
				animatorStateTransition41182.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41182.AddCondition(AnimatorConditionMode.Equals, 24f, "Slot0ItemID");

				var animatorStateTransition41184 = baseStateMachine310764996.AddAnyStateTransition(bowAnimatorState41296);
				animatorStateTransition41184.canTransitionToSelf = false;
				animatorStateTransition41184.duration = 0.1f;
				animatorStateTransition41184.exitTime = 0.75f;
				animatorStateTransition41184.hasExitTime = false;
				animatorStateTransition41184.hasFixedDuration = true;
				animatorStateTransition41184.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41184.offset = 0f;
				animatorStateTransition41184.orderedInterruption = true;
				animatorStateTransition41184.isExit = false;
				animatorStateTransition41184.mute = false;
				animatorStateTransition41184.solo = false;
				animatorStateTransition41184.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41184.AddCondition(AnimatorConditionMode.Equals, 4f, "Slot1ItemID");

				var animatorStateTransition41186 = baseStateMachine310764996.AddAnyStateTransition(dualPistolAnimatorState41298);
				animatorStateTransition41186.canTransitionToSelf = false;
				animatorStateTransition41186.duration = 0.1f;
				animatorStateTransition41186.exitTime = 0.75f;
				animatorStateTransition41186.hasExitTime = false;
				animatorStateTransition41186.hasFixedDuration = true;
				animatorStateTransition41186.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41186.offset = 0f;
				animatorStateTransition41186.orderedInterruption = true;
				animatorStateTransition41186.isExit = false;
				animatorStateTransition41186.mute = false;
				animatorStateTransition41186.solo = false;
				animatorStateTransition41186.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41186.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition41186.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot1ItemID");

				var animatorStateTransition41188 = baseStateMachine310764996.AddAnyStateTransition(shotgunAnimatorState41300);
				animatorStateTransition41188.canTransitionToSelf = false;
				animatorStateTransition41188.duration = 0.1f;
				animatorStateTransition41188.exitTime = 0.75f;
				animatorStateTransition41188.hasExitTime = false;
				animatorStateTransition41188.hasFixedDuration = true;
				animatorStateTransition41188.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41188.offset = 0f;
				animatorStateTransition41188.orderedInterruption = true;
				animatorStateTransition41188.isExit = false;
				animatorStateTransition41188.mute = false;
				animatorStateTransition41188.solo = false;
				animatorStateTransition41188.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41188.AddCondition(AnimatorConditionMode.Equals, 3f, "Slot0ItemID");

				var animatorStateTransition41190 = baseStateMachine310764996.AddAnyStateTransition(pistolAnimatorState41302);
				animatorStateTransition41190.canTransitionToSelf = false;
				animatorStateTransition41190.duration = 0.1f;
				animatorStateTransition41190.exitTime = 0.75f;
				animatorStateTransition41190.hasExitTime = false;
				animatorStateTransition41190.hasFixedDuration = true;
				animatorStateTransition41190.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41190.offset = 0f;
				animatorStateTransition41190.orderedInterruption = true;
				animatorStateTransition41190.isExit = false;
				animatorStateTransition41190.mute = false;
				animatorStateTransition41190.solo = false;
				animatorStateTransition41190.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41190.AddCondition(AnimatorConditionMode.Equals, 2f, "Slot0ItemID");
				animatorStateTransition41190.AddCondition(AnimatorConditionMode.NotEqual, 2f, "Slot1ItemID");

				var animatorStateTransition41192 = baseStateMachine310764996.AddAnyStateTransition(rocketLauncherAnimatorState41304);
				animatorStateTransition41192.canTransitionToSelf = false;
				animatorStateTransition41192.duration = 0.1f;
				animatorStateTransition41192.exitTime = 0.75f;
				animatorStateTransition41192.hasExitTime = false;
				animatorStateTransition41192.hasFixedDuration = true;
				animatorStateTransition41192.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41192.offset = 0f;
				animatorStateTransition41192.orderedInterruption = true;
				animatorStateTransition41192.isExit = false;
				animatorStateTransition41192.mute = false;
				animatorStateTransition41192.solo = false;
				animatorStateTransition41192.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41192.AddCondition(AnimatorConditionMode.Equals, 6f, "Slot0ItemID");

				var animatorStateTransition41194 = baseStateMachine310764996.AddAnyStateTransition(assaultRifleAnimatorState41306);
				animatorStateTransition41194.canTransitionToSelf = false;
				animatorStateTransition41194.duration = 0.1f;
				animatorStateTransition41194.exitTime = 0.75f;
				animatorStateTransition41194.hasExitTime = false;
				animatorStateTransition41194.hasFixedDuration = true;
				animatorStateTransition41194.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41194.offset = 0f;
				animatorStateTransition41194.orderedInterruption = true;
				animatorStateTransition41194.isExit = false;
				animatorStateTransition41194.mute = false;
				animatorStateTransition41194.solo = false;
				animatorStateTransition41194.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41194.AddCondition(AnimatorConditionMode.Equals, 1f, "Slot0ItemID");

				var animatorStateTransition41196 = baseStateMachine310764996.AddAnyStateTransition(sniperRifleAnimatorState41308);
				animatorStateTransition41196.canTransitionToSelf = false;
				animatorStateTransition41196.duration = 0.1f;
				animatorStateTransition41196.exitTime = 0.75f;
				animatorStateTransition41196.hasExitTime = false;
				animatorStateTransition41196.hasFixedDuration = true;
				animatorStateTransition41196.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41196.offset = 0f;
				animatorStateTransition41196.orderedInterruption = true;
				animatorStateTransition41196.isExit = false;
				animatorStateTransition41196.mute = false;
				animatorStateTransition41196.solo = false;
				animatorStateTransition41196.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41196.AddCondition(AnimatorConditionMode.Equals, 5f, "Slot0ItemID");

				var animatorStateTransition41198 = baseStateMachine310764996.AddAnyStateTransition(fragGrenadeAnimatorState41310);
				animatorStateTransition41198.canTransitionToSelf = false;
				animatorStateTransition41198.duration = 0.1f;
				animatorStateTransition41198.exitTime = 0.75f;
				animatorStateTransition41198.hasExitTime = false;
				animatorStateTransition41198.hasFixedDuration = true;
				animatorStateTransition41198.interruptionSource = TransitionInterruptionSource.None;
				animatorStateTransition41198.offset = 0f;
				animatorStateTransition41198.orderedInterruption = true;
				animatorStateTransition41198.isExit = false;
				animatorStateTransition41198.mute = false;
				animatorStateTransition41198.solo = false;
				animatorStateTransition41198.AddCondition(AnimatorConditionMode.Equals, 105f, "AbilityIndex");
				animatorStateTransition41198.AddCondition(AnimatorConditionMode.Equals, 41f, "Slot0ItemID");
			}
		}
	}
}
