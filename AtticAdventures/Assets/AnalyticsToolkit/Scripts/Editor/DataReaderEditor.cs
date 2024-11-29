using HeatmapVisualization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DataReader))]
public class DataReaderEditor : Editor
{
	#region Globals
	private new DataReader target;
	#endregion


	#region Functions
	private void Awake()
	{
		target = (DataReader)base.target;
	}


	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Generate Heatmap"))
		{
			Undo.RecordObject(target.GetComponent<Heatmap>(), "Generate heatmap");
			Undo.RecordObject(target.GetComponent<MeshRenderer>(), "Generate heatmap");
			Undo.RecordObject(target.GetComponent<MeshRenderer>().sharedMaterial, "Generate heatmap");
			PrefabUtility.RecordPrefabInstancePropertyModifications(target);

			target.GenerateHeatmap();
		}
	}
	#endregion
}
