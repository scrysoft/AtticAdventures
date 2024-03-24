using AtticAdventures.Core;

namespace AtticAdventures.SpawnSystem
{
    public class EntitySpawner<T> where T : Entity
    {
        private IEntityFactory<T> entityFactory;
        private ISpawnPointStrategy spawnPointStrategy;

        public EntitySpawner(IEntityFactory<T> entityFactory, ISpawnPointStrategy spawnPointStrategy)
        {
            this.entityFactory = entityFactory;
            this.spawnPointStrategy = spawnPointStrategy;
        }

        public T Spawn()
        {
            return entityFactory.Create(spawnPointStrategy.NextSpawnPoint());
        }
    }
}
