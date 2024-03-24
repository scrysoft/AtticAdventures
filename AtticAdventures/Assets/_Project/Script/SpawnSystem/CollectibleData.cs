using UnityEngine;

namespace AtticAdventures.SpawnSystem
{
    [CreateAssetMenu(fileName = "CollectibleData", menuName = "AtticAdventures/CollectibleData")]
    public class CollectibleData : EntityData
    {
        public int score;

        // additional properties specific to collectibles
    }
}
