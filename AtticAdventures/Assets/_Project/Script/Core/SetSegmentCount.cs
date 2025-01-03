using UnityEngine;

public class SetSegmentCount : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            SetSegmentCountValue(material.GetInt("_SegmentCount"));
        }
    }

    public void SetSegmentCountValue(float value)
    {
        if (material != null)
        {
            int segmentCount = material.GetInt("_SegmentCount");
            int difference = segmentCount - Mathf.RoundToInt(value);

            material.SetInt("_RemovedSegments", difference);
        }
    }
}
