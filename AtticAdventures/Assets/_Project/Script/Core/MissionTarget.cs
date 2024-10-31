using UnityEngine;

namespace AtticAdventures
{
    public class MissionTarget : MonoBehaviour
    {
        public int index = 0;
        public string questHeader = "";
        public string questObjective = "";

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.5f);

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.yellow;
            style.fontSize = 12;
            style.fontStyle = FontStyle.Bold;

            UnityEditor.Handles.Label(transform.position + Vector3.up, $"Index: {index}", style);
        }
    }
}
