/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.AddOns.Climbing.Objects
{
    using Opsive.UltimateCharacterController.Character;
    using Opsive.UltimateCharacterController.Objects.CharacterAssist;
    using Opsive.UltimateCharacterController.Utility;
    using UnityEngine;

    /// <summary>
    /// Represents an object that can be climbed vertically.
    /// </summary>
    public class Ladder : MonoBehaviour
    {
        [Tooltip("A reference to the bottom Move Towards Location.")]
        [SerializeField] protected MoveTowardsLocation m_BottomMountMoveTowardsLocation;
        [Tooltip("A reference to the top Move Towards Location.")]
        [SerializeField] protected MoveTowardsLocation m_TopMountMoveTowardsLocation;
        [Tooltip("The vertical offset of the bottom dismount location.")]
        [SerializeField] protected float m_BottomDismountOffset = -1;
        [Tooltip("The vertical offset of the top dismount location.")]
        [SerializeField] protected float m_TopDismountOffset = 1;
        [Tooltip("The offset between the rung location and the character's pivot position.")]
        [SerializeField] protected float m_RungOffset = 0.2f;
        [Tooltip("The amount of space between ladder rungs.")]
        [SerializeField] protected float m_RungSeparation = 0.2f;
        [Tooltip("The offset between the object and the character when the character mounts in the air.")]
        [SerializeField] protected float m_AirborneMountZOffset = -0.2f;

        public MoveTowardsLocation BottomMountMoveTowardsLocation { get { return m_BottomMountMoveTowardsLocation; } set { m_BottomMountMoveTowardsLocation = value; } }
        public MoveTowardsLocation TopMountMoveTowardsLocation { get { return m_TopMountMoveTowardsLocation; } set { m_TopMountMoveTowardsLocation = value; } }
        public float BottomDismountOffset { get { return m_BottomDismountOffset; } set { m_BottomDismountOffset = value; } }
        public float TopDismountOffset { get { return m_TopDismountOffset; } set { m_TopDismountOffset = value; } }
        public Vector3 BottomDismountLocation { get { return MathUtility.TransformPoint(m_Transform.position, m_Transform.rotation, new Vector3(0, m_BottomDismountOffset, 0)); } }
        public Vector3 TopDismountLocation { get { return MathUtility.TransformPoint(m_Transform.position, m_Transform.rotation, new Vector3(0, m_TopDismountOffset, 0)); } }
        public float RungOffset { get { return m_RungOffset; } }
        public float RungSeparation { get { return m_RungSeparation; } }

        private Transform m_Transform;
        private BoxCollider m_BoxCollider;

        /// <summary>
        /// Initializes the default values.
        /// </summary>
        private void Awake()
        {
            m_Transform = transform;
            m_BoxCollider = GetComponent<BoxCollider>();
            if (m_RungSeparation > 0 && m_BoxCollider == null) {
                Debug.LogError($"Error: The ladder {name} must have a BoxCollider to determine the rung positions.");
            }
        }

        /// <summary>
        /// Can the character mount on the ladder?
        /// </summary>
        /// <param name="character">The character that wants to mount on the ladder.</param>
        /// <returns>True if the character can mount on the ladder.</returns>
        public virtual bool CanMount(UltimateCharacterLocomotion character)
        {
            if (character.Grounded) {
                return true;
            }

            var bottom = m_Transform.TransformPoint(0, (m_BoxCollider.center - m_BoxCollider.size / 2).y, 0);
            var localPosition = MathUtility.InverseTransformPoint(bottom, m_Transform.rotation, character.transform.position);
            return localPosition.y > 0 && localPosition.y < m_TopDismountOffset;
        }

        /// <summary>
        /// Returns the desired mount position of the airborne character.
        /// </summary>
        /// <param name="characterLocomotion">The character that is mounting on the ladder.</param>
        /// <returns>The desired mount position of the airborne character.</returns>
        public Vector3 GetAirborneMountPosition(UltimateCharacterLocomotion characterLocomotion)
        {
            var bottom = m_Transform.TransformPoint(0, (m_BoxCollider.center - m_BoxCollider.size / 2).y, 0);
            var rotation = m_Transform.rotation;
            var localPosition = MathUtility.InverseTransformPoint(bottom, rotation, characterLocomotion.transform.position);
            return MathUtility.TransformPoint(bottom, rotation, 
                new Vector3(0, m_RungOffset + Mathf.FloorToInt(localPosition.y / m_RungSeparation) * m_RungSeparation - characterLocomotion.ColliderSpacing, m_AirborneMountZOffset));
        }

        /// <summary>
        /// Returns the look direction of the mounted character.
        /// </summary>
        /// <param name="characterLocomotion">The character's locomotion</param>
        /// <returns>The look direction of the mounted character.</returns>
        public Vector3 GetLookDirection(CharacterLocomotion characterLocomotion)
        {
            var forward = m_Transform.InverseTransformPoint(characterLocomotion.Position).z > 0;
            return m_Transform.forward * (forward ? -1 : 1);
        }
    }
}