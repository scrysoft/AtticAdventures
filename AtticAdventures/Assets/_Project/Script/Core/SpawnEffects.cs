using DG.Tweening;
using UnityEngine;

namespace AtticAdventures.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class SpawnEffects : MonoBehaviour
    {
        [SerializeField] GameObject spawnVFX;
        [SerializeField] float animationDuration = 1f;
        [SerializeField] Ease easeType = Ease.OutBack;

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, animationDuration)
                .SetEase(easeType);

            if(spawnVFX != null )
            {
                Instantiate(spawnVFX, transform.position, Quaternion.identity);
            }

            GetComponent<AudioSource>().Play();
        }

    }
}
