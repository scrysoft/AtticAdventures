/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Climbing.Editor.Inspectors.Objects
{
    using Opsive.UltimateCharacterController.AddOns.Climbing.Objects;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Custom inspector for the Ladder component.
    /// </summary>
    [CustomEditor(typeof(Ladder))]
    public class LadderInspector : UnityEditor.Editor
    {
        private Ladder m_Ladder;

        /// <summary>
        /// Initialize the default values.
        /// </summary>
        public void OnEnable()
        {
            m_Ladder = target as Ladder;
        }

        /// <summary>
        /// Draws a gizmo for the top and bottom dismount positions.
        /// </summary>
        private void OnSceneGUI()
        {
            if (Event.current.type != EventType.Repaint) {
                return;
            }

            // Draw the top and bottom locations.
            var localTopOffset = new Vector3(0, m_Ladder.TopDismountOffset, -0.5f);
            var localBottomOffset = new Vector3(0, m_Ladder.BottomDismountOffset, -0.5f);

            var ladderTransform = m_Ladder.transform;
            var rotation = ladderTransform.rotation;
            Handles.matrix = Matrix4x4.TRS(ladderTransform.position, rotation, ladderTransform.lossyScale);
            Handles.color = new Color(1, 1, 1, 0.8f);
            Handles.SphereHandleCap(0, localTopOffset, rotation, 0.2f, EventType.Repaint);
            Handles.SphereHandleCap(0, localBottomOffset, rotation, 0.2f, EventType.Repaint);
            Handles.color = new Color(1, 1, 1, 1f);
            Handles.DrawDottedLine(localBottomOffset, localTopOffset, 1f);

            // Draw the rungs in the ladder.
            var collider = m_Ladder.GetComponent<BoxCollider>();
            if (collider == null || m_Ladder.RungSeparation <= 0) {
                return;
            }

            var position = m_Ladder.RungOffset + (collider.center - collider.size / 2).y;
            var rungSize = new Vector3(0.5f, 0.1f, 0.1f);
            var topPosition = MathUtility.TransformPoint(ladderTransform.position, rotation, new Vector3(0, m_Ladder.TopDismountOffset, -.5f));
            while (true) {
                var worldPosition = m_Ladder.transform.TransformPoint(0, position, 0);
                if (worldPosition.y > topPosition.y) {
                    break;
                }
                Handles.DrawWireCube(new Vector3(0, position, 0), rungSize);
                position += m_Ladder.RungSeparation;
            }
        }
    }
}