using System;
using System.Collections;
using UnityEngine;

namespace RealSoftGames
{
    public static class RTweenExtensions
    {
        public static RTween.RTweenInstance Expand(this Transform target, float duration, RTween.OpenDirection direction, Action onExpand = null)
        {
            var tweenInstance = target.GetComponent<RTween.RTweenInstance>();
            if (tweenInstance == null)
                tweenInstance = target.gameObject.AddComponent<RTween.RTweenInstance>();

            tweenInstance.Initialize(target, duration, direction, onExpand, null);
            tweenInstance.Expand();
            return tweenInstance;
        }

        public static RTween.RTweenInstance Close(this Transform target, float duration, RTween.OpenDirection direction, Action onClose = null)
        {
            var tweenInstance = target.GetComponent<RTween.RTweenInstance>();
            if (tweenInstance == null)
                tweenInstance = target.gameObject.AddComponent<RTween.RTweenInstance>();

            tweenInstance.Initialize(target, duration, direction, null, onClose);
            tweenInstance.Close();
            return tweenInstance;
        }

        public static bool IsExpanded(this Transform target)
        {
            var instance = target.GetComponent<RTween.RTweenInstance>();
            if (instance == null)
                instance = target.gameObject.AddComponent<RTween.RTweenInstance>();

            return instance != null && instance.IsExpanded;
        }
    }

    public class RTween
    {
        public enum OpenDirection { Top, Bottom, Left, Right, Center }

        public class RTweenInstance : MonoBehaviour
        {
            private bool isInitialized = false;
            private Transform target;
            private RectTransform rectTransform;
            private float duration;
            private OpenDirection direction;
            private Action onExpand;
            private Action onClose;
            private Action onComplete;
            private bool isExpanded;
            private bool isTransitioning;
            private Vector2 initialPosition;

            public bool IsInitialized { get => isInitialized; private set => isInitialized = value; }

            public void Initialize(Transform target, float duration, OpenDirection direction, Action onExpand = null, Action onClose = null)
            {
                if (IsInitialized)
                    return;


                this.target = target;
                this.rectTransform = target as RectTransform;
                this.duration = duration;
                this.direction = direction;
                this.onExpand = onExpand;
                this.onClose = onClose;
                this.isExpanded = target.localScale == Vector3.one;
                if (rectTransform != null)
                {
                    this.initialPosition = rectTransform.anchoredPosition;
                }
                else
                {
                    this.initialPosition = target.localPosition;
                }
                isInitialized = true;
            }

            public RTweenInstance OnComplete(Action onComplete)
            {
                this.onComplete = onComplete;
                return this;
            }

            public bool IsExpanded => isExpanded || isTransitioning;

            public void Expand()
            {
                if (!isExpanded && !isTransitioning)
                {
                    isTransitioning = true;
                    MonoBehaviourUtility.Instance.StartCoroutine(Scale(target, Vector3.zero, Vector3.one, duration, direction, () =>
                    {
                        isExpanded = true;
                        isTransitioning = false;
                        onExpand?.Invoke();
                        onComplete?.Invoke();
                    }));
                }
            }

            public void Close()
            {
                if (isExpanded && !isTransitioning)
                {
                    isTransitioning = true;
                    // Ensure we close relative to the current position
                    Vector2 currentPosition = rectTransform != null ? rectTransform.anchoredPosition : target.localPosition;
                    MonoBehaviourUtility.Instance.StartCoroutine(Scale(target, Vector3.one, Vector3.zero, duration, direction, currentPosition, () =>
                    {
                        isExpanded = false;
                        isTransitioning = false;
                        onClose?.Invoke();
                        onComplete?.Invoke();
                    }));
                }
            }

            private IEnumerator Scale(Transform target, Vector3 fromScale, Vector3 toScale, float duration, OpenDirection direction, Action onComplete)
            {
                return Scale(target, fromScale, toScale, duration, direction, initialPosition, onComplete);
            }

            private IEnumerator Scale(Transform target, Vector3 fromScale, Vector3 toScale, float duration, OpenDirection direction, Vector2 currentPosition, Action onComplete)
            {
                float timer = 0f;
                Vector2 fromPosition = currentPosition;
                Vector2 toPosition = CalculatePosition(fromPosition, direction, fromScale, toScale);

                while (timer < duration)
                {
                    float progress = timer / duration;
                    progress = Mathf.SmoothStep(0f, 1f, progress);

                    target.localScale = Vector3.Lerp(fromScale, toScale, progress);
                    if (rectTransform != null)
                    {
                        rectTransform.anchoredPosition = Vector2.Lerp(fromPosition, toPosition, progress);
                    }
                    else
                    {
                        target.localPosition = Vector3.Lerp(fromPosition, toPosition, progress);
                    }

                    timer += Time.deltaTime;
                    yield return null;
                }

                target.localScale = toScale;
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = toPosition;
                }
                else
                {
                    target.localPosition = toPosition;
                }

                onComplete?.Invoke();
            }

            private Vector2 CalculatePosition(Vector2 initialPosition, OpenDirection direction, Vector3 fromScale, Vector3 toScale)
            {
                if (rectTransform != null)
                {
                    float deltaHeight = rectTransform.rect.height * (toScale.y - fromScale.y);
                    float deltaWidth = rectTransform.rect.width * (toScale.x - fromScale.x);

                    switch (direction)
                    {
                        case OpenDirection.Top:
                            return new Vector2(initialPosition.x, initialPosition.y + deltaHeight * (1 - rectTransform.pivot.y));
                        case OpenDirection.Bottom:
                            return new Vector2(initialPosition.x, initialPosition.y - deltaHeight * rectTransform.pivot.y);
                        case OpenDirection.Left:
                            return new Vector2(initialPosition.x - deltaWidth * rectTransform.pivot.x, initialPosition.y);
                        case OpenDirection.Right:
                            return new Vector2(initialPosition.x + deltaWidth * (1 - rectTransform.pivot.x), initialPosition.y);
                        case OpenDirection.Center:
                            return initialPosition; // No movement for center expansion
                        default:
                            return initialPosition;
                    }
                }
                else
                {
                    return initialPosition; // For non-UI elements, position adjustments might need custom logic.
                }
            }
        }
    }

    public class MonoBehaviourUtility : MonoBehaviour
    {
        private static MonoBehaviourUtility _instance;
        public static MonoBehaviourUtility Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("MonoBehaviourUtility");
                    _instance = obj.AddComponent<MonoBehaviourUtility>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (_instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
