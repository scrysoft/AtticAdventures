using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeatmapSettings = HeatmapVisualization.HeatmapTextureGenerator.Settings;


namespace HeatmapVisualization
{
	public class Heatmap : MonoBehaviour
	{
		#region Settings
		[SerializeField]
		private Material hetamapMaterial;
		[SerializeField]
		private ComputeShader gaussianComputeShader;
		[SerializeField]
		public Vector3Int resolution = new Vector3Int(64, 64, 64);
		[SerializeField]
		[Range(0.0f, 1.0f)]
		public float cutoffPercentage = 1.0f;
		[SerializeField]
		public float gaussStandardDeviation = 1.0f;
		[SerializeField]
		private Gradient colormap;
		[SerializeField]
		private bool renderOnTop = false;
		[SerializeField]
		private FilterMode textureFilterMode = FilterMode.Bilinear;

		private const int colormapTextureResolution = 256;
		#endregion


		#region Globals
		private MeshRenderer ownRenderer;
		private MeshRenderer OwnRenderer { get { if (ownRenderer == null) { ownRenderer = GetComponent<MeshRenderer>(); } return ownRenderer; } }
		private Material OwnRenderersMaterial { get { InstantiateMaterial(); return OwnRenderer.sharedMaterial; } }
		public Bounds BoundsFromTransform { get => new Bounds { center = transform.position, size = transform.localScale }; }
		private bool materialIsInstanced = false;
		#endregion


		#region Functions
		/// <summary>
		/// After calling this function, the heatmap object will display the density of the given points within bounds determined by the heatmaps transform.
		/// </summary>
		/// <param name="points">List of points to derive density from.</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		public void GenerateHeatmap(List<Vector3> points)
		{
			GenerateHeatmap(points.ToArray());
		}


		/// <summary>
		/// After calling this function, the heatmap object will display the density of the given points within bounds determined by the heatmaps transform.
		/// </summary>
		/// <param name="points">Array of points to derive density from.</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		public void GenerateHeatmap(Vector3[] points)
		{
			if (points == null)
			{
				throw new System.ArgumentNullException();
			}
			if (points.Length == 0)
			{
				Debug.LogWarning("Points list for Heatmap generation is empty.", this);
			}

			HeatmapSettings settings = new HeatmapSettings(BoundsFromTransform, resolution, gaussStandardDeviation);

			//calculate heatmap texture
			HeatmapTextureGenerator heatmapTextureGenerator = new HeatmapTextureGenerator(gaussianComputeShader);
			float[] heatValues = heatmapTextureGenerator.CalculateHeatTexture(points, settings);

			SetAllMaterialValues(heatValues);
		}


		/// <summary>
		/// After calling this function, the heatmap object will display the given heat values.
		/// </summary>
		/// <param name="heatValues">The heat values to display as list (x > y > z). Length must match the resolution setting (x * y * z).</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		/// <exception cref="System.ArgumentException"></exception>
		public void GenerateHeatmapFromHeatValues(List<float> heatValues)
		{
			GenerateHeatmapFromHeatValues(heatValues.ToArray());
		}


		/// <summary>
		/// After calling this function, the heatmap object will display the given heat values.
		/// </summary>
		/// <param name="heatValues">The heat values to display as array (x > y > z). Length must match the resolution setting (x * y * z).</param>
		/// <exception cref="System.ArgumentNullException"></exception>
		/// <exception cref="System.ArgumentException"></exception>
		public void GenerateHeatmapFromHeatValues(float[] heatValues)
		{
			if (heatValues == null)
			{
				throw new System.ArgumentNullException();
			}
			if (heatValues.Length != resolution.x * resolution.y * resolution.z)
			{
				throw new System.ArgumentException("The length of heatValues does not match resolution setting (x * y * z).");
			}

			SetAllMaterialValues(heatValues);
		}


		private void SetAllMaterialValues(float[] heatValues)
		{
			(Texture3D heatTexture, float maxHeat) = ArrayToTexture(heatValues);
			SetAllMaterialValues(heatTexture, maxHeat);
		}


		private void SetAllMaterialValues(Texture3D heatTexture, float maxHeat)
		{
			SetHeatTexture(heatTexture, maxHeat);
			SetColormap();
			SetCutoffPercentage();
			SetRenderOnTop();
			//filter mode is applied with the heat texture.
		}


		private void SetHeatTexture(float[] heatValues)
		{
			(Texture3D heatTexture, float maxHeat) = ArrayToTexture(heatValues);
			SetHeatTexture(heatTexture, GetMaxValue(heatValues));
		}


		private void SetHeatTexture(Texture3D heatTexture, float maxHeat)
		{
			OwnRenderersMaterial.SetTexture("_DataTex", heatTexture);
			OwnRenderersMaterial.SetFloat("_MaxHeat", maxHeat);
			SetTextureFilterMode(); //Set filter mode, because it is a property of the texture.
		}


		private (Texture3D, float) ArrayToTexture(float[] heatValues)
		{
			//create texture object
			Texture3D heatTexture = new Texture3D(resolution.x, resolution.y, resolution.z, TextureFormat.RFloat, false);
			heatTexture.SetPixelData(heatValues, 0);
			heatTexture.wrapMode = TextureWrapMode.Clamp;
			heatTexture.filterMode = textureFilterMode;
			heatTexture.Apply();

			float maxHeat = GetMaxValue(heatValues);

			return (heatTexture, maxHeat);
		}


		public void SetColormap(Gradient colormap)
		{
			this.colormap = colormap;
			SetColormap();
		}


		public void SetColormap()
		{
			OwnRenderersMaterial.SetTexture("_GradientTex", GradientToTexture(colormap, colormapTextureResolution));
		}


		public void SetCutoffPercentage(float cutoffPercentage)
		{
			this.cutoffPercentage = cutoffPercentage;
			SetCutoffPercentage();
		}


		public void SetCutoffPercentage()
		{
			OwnRenderersMaterial.SetFloat("_CutoffPercentage", cutoffPercentage);
		}


		public void SetRenderOnTop(bool renderOnTop)
		{
			this.renderOnTop = renderOnTop;
			SetRenderOnTop();
		}


		public void SetRenderOnTop()
		{
			if (renderOnTop)
			{
				OwnRenderersMaterial.DisableKeyword("USE_SCENE_DEPTH");
			}
			else
			{
				OwnRenderersMaterial.EnableKeyword("USE_SCENE_DEPTH");
			}
		}


		public void SetTextureFilterMode(FilterMode textureFilterMode)
		{
			this.textureFilterMode = textureFilterMode;
			SetTextureFilterMode();
		}


		public void SetTextureFilterMode()
		{
			if (OwnRenderersMaterial.GetTexture("_DataTex") == null)
			{ return; }

			OwnRenderersMaterial.GetTexture("_DataTex").filterMode = textureFilterMode;
		}


		public void Reset()
		{
			SetAllMaterialValues(null, 1);
		}


		/// <summary>
		/// Instantiate the renderer's material to prevent editing the material asset or other heatmaps material.
		/// </summary>
		private void InstantiateMaterial()
		{
			if (materialIsInstanced && (OwnRenderer.sharedMaterial != null))
			{ return; }

			Texture3D heatTexture = null;
			float maxHeat = 1;

			if (OwnRenderer.sharedMaterial != null)
			{
				heatTexture = (Texture3D)OwnRenderer.sharedMaterial.GetTexture("_DataTex");
				maxHeat = OwnRenderer.sharedMaterial.GetFloat("_MaxHeat");
			}

			OwnRenderer.sharedMaterial = new Material(hetamapMaterial);
			materialIsInstanced = true;
			SetAllMaterialValues(heatTexture, maxHeat);
		}


		/// <summary>
		/// Get the maximum value from an array.
		/// </summary>
		private static float GetMaxValue(float[] heats)
		{
			float maxHeat = 0.0f;

			for (int i = 0; i < heats.Length; i++)
			{
				if (heats[i] > maxHeat)
				{
					maxHeat = heats[i];
				}
			}
			
			return maxHeat;
		}


		/// <summary>
		/// Convert a gradient to a texture so it can be used as a material parameter.
		/// </summary>
		/// <param name="gradient">The gradient to convert.</param>
		/// <param name="resolution">The width of the resulting texture. Height is always 1.</param>
		/// <returns>A texture sampled from the gradient.</returns>
		private static Texture2D GradientToTexture(Gradient gradient, int resolution)
		{
			Texture2D texture = new Texture2D(resolution, 1);

			for (int i = 0; i < resolution; i++)
			{
				texture.SetPixel(i, 1, gradient.Evaluate(((float)i) / (resolution - 1)));
			}

			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = FilterMode.Bilinear;
			texture.Apply();
			return texture;
		}


#if UNITY_EDITOR
		private void OnValidate()
		{
			//prevent caling OnValidate on the original prefab asset
			if (!UnityEditor.PrefabUtility.IsPartOfPrefabInstance(gameObject))
			{ return; }

			InstantiateMaterial();
		}
#endif
		#endregion
	}
}