using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace HeatmapVisualization
{
	[CustomEditor(typeof(Heatmap))]
	public class HeatmapEditor : Editor
	{
		#region Settings
		private int resolutionWarningThreshold = 256 * 256 * 256;
		#endregion


		#region Globals
		private new Heatmap target;
		private bool foldoutReferences = false;
		private bool foldoutGenerationSettings = true;
		private bool foldoutRenderingSettings = true;

		#region Target Properties
		private SerializedProperty hetamapMaterial;
		private SerializedProperty gaussianComputeShader;
		private SerializedProperty resolution;
		private SerializedProperty cutoffPercentage;
		private SerializedProperty gaussStandardDeviation;
		private SerializedProperty colormap;
		private SerializedProperty renderOnTop;
		private SerializedProperty textureFilterMode;
		#endregion
		#endregion


		#region Functions
		private void OnEnable()
		{
			target = (Heatmap)base.target;

			//get serialized properties
			hetamapMaterial = serializedObject.FindProperty("hetamapMaterial");
			gaussianComputeShader = serializedObject.FindProperty("gaussianComputeShader");
			resolution = serializedObject.FindProperty("resolution");
			cutoffPercentage = serializedObject.FindProperty("cutoffPercentage");
			gaussStandardDeviation = serializedObject.FindProperty("gaussStandardDeviation");
			colormap = serializedObject.FindProperty("colormap");
			renderOnTop = serializedObject.FindProperty("renderOnTop");
			textureFilterMode = serializedObject.FindProperty("textureFilterMode");

			//get foldout flags
			foldoutGenerationSettings = EditorPrefs.GetBool("HeatmapEditor-foldoutGenerationSettings", foldoutGenerationSettings);
			foldoutRenderingSettings = EditorPrefs.GetBool("HeatmapEditor-foldoutRenderingSettings", foldoutRenderingSettings);
		}


		private void OnDestroy()
		{
			//save foldout flags
			EditorPrefs.SetBool("HeatmapEditor-foldoutGenerationSettings", foldoutGenerationSettings);
			EditorPrefs.SetBool("HeatmapEditor-foldoutRenderingSettings", foldoutRenderingSettings);
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			foldoutReferences = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutReferences, "Prefab References");
			if (foldoutReferences)
			{
				EditorGUILayout.PropertyField(hetamapMaterial);
				EditorGUILayout.PropertyField(gaussianComputeShader);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			foldoutGenerationSettings = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutGenerationSettings, "Generation Settings");
			if (foldoutGenerationSettings)
			{
				EditorGUILayout.PropertyField(resolution);
				Vector3Int res = resolution.vector3IntValue;
				if ((res.x * res.y * res.z) > resolutionWarningThreshold)
				{
					EditorGUILayout.HelpBox("High resolutions can cause compute shader timeouts and therefore crash Unity.", MessageType.Warning);
				}
				if (res.x > 2048
					|| res.y > 2048
					|| res.z > 2048)
				{
					EditorGUILayout.HelpBox("Texture3D does not support resolutions higher than 2048 per axis.", MessageType.Error);
				}

				EditorGUILayout.PropertyField(gaussStandardDeviation);
			}
			EditorGUILayout.EndFoldoutHeaderGroup();


			foldoutRenderingSettings = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutRenderingSettings, "Rendering Settings");
			if (foldoutRenderingSettings)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(colormap);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetColormap();
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(cutoffPercentage);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetCutoffPercentage();
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(renderOnTop);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetRenderOnTop();
				}
				if (!renderOnTop.boolValue)
				{
					//GraphicsSettings.currentRenderPipeline.GetType().ToString() returns:
					//For 2019.2 and earlier:
					//  UnityEngine.Experimental.Rendering.HDPipeline.HDRenderPipelineAsset
					//For 2019.3+:
					//  UnityEngine.Rendering.HighDefinition.HDRenderPipelineAsset
					if (UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("HighDefinition")
						|| UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("HDPipeline"))
					{
						EditorGUILayout.HelpBox("Disabling RenderOnTop can cause problems on the HDRenderPipeline.", MessageType.Warning);
					}
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(textureFilterMode);
				if (EditorGUI.EndChangeCheck())
				{
					serializedObject.ApplyModifiedProperties();
					target.SetTextureFilterMode();
				}
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			serializedObject.ApplyModifiedProperties();

			if (GUILayout.Button("Reset Heatmap Data"))
			{
				Undo.RecordObject(target.GetComponent<Heatmap>(), "Reset heatmap Data");
				Undo.RecordObject(target.GetComponent<MeshRenderer>(), "Reset heatmap Data");
				Undo.RecordObject(target.GetComponent<MeshRenderer>().sharedMaterial, "Reset heatmap Data");
				PrefabUtility.RecordPrefabInstancePropertyModifications(target);
				target.Reset();
			}
		}


		[DrawGizmo(GizmoType.InSelectionHierarchy)]
		static void DrawGizmos(Heatmap target, GizmoType gizmoType)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(target.BoundsFromTransform.center, target.BoundsFromTransform.size);
		}
		#endregion
	}
}
