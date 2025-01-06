using Opsive.UltimateCharacterController.Camera;
using UnityEngine;
using System.Collections;

namespace AtticAdventures
{
    public class CameraPositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform cameraAnchor;
        [SerializeField] private Transform resetAnchor;
        [SerializeField] private float transitionDuration = 2f;
        [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private CameraController cameraController;

        public void SetCameraAnchorSmooth()
        {
            cameraController = FindObjectOfType<CameraController>();

            if (cameraController != null && cameraAnchor != null)
            {
                StartCoroutine(TransitionCamera(cameraController.transform, cameraAnchor.position, cameraAnchor.rotation));
            }
        }

        public void ResetCameraPositionSmooth()
        {
            cameraController = FindObjectOfType<CameraController>();

            if (cameraController != null && resetAnchor != null)
            {
                StartCoroutine(TransitionCamera(cameraController.transform, resetAnchor.position, resetAnchor.rotation));
            }
        }

        private IEnumerator TransitionCamera(Transform cameraTransform, Vector3 targetPosition, Quaternion targetRotation)
        {
            Vector3 startPosition = cameraTransform.position;
            Quaternion startRotation = cameraTransform.rotation;
            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / transitionDuration);
                float easedT = easeCurve.Evaluate(t);
                cameraTransform.position = Vector3.Lerp(startPosition, targetPosition, easedT);
                cameraTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, easedT);
                yield return null;
            }

            cameraTransform.position = targetPosition;
            cameraTransform.rotation = targetRotation;
        }
    }
}
