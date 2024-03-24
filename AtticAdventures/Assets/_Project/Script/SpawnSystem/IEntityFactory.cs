using AtticAdventures.Core;
using UnityEngine;

namespace AtticAdventures.SpawnSystem
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(Transform spawnPoint);
    }
}
