using UnityEngine;
using DG.Tweening;

namespace AtticAdventures.Environment
{
    public class PlatformMover : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] Vector3 moveTo = Vector3.zero;
        [SerializeField] float moveTime = 1f;
        [SerializeField] Ease easeType = Ease.InOutQuad;
        [SerializeField] LoopType loopType = LoopType.Yoyo;
        [SerializeField] int loopCount = -1; // -1 = Infinity

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
            
            Move();
        }

        private void Move()
        {
            transform.DOMove(startPosition + moveTo, moveTime)
                .SetEase(easeType)
                .SetLoops(loopCount, loopType);
        }
    }
}
