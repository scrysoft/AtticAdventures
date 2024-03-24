using UnityEngine;

namespace AtticAdventures.SpawnSystem
{
    public interface ISpawnPointStrategy
    {
        Transform NextSpawnPoint();
    }
}
