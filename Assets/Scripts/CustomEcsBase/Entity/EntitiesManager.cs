using System.Collections.Generic;
using CustomEcsBase.World;

namespace CustomEcsBase.Entity
{
    public class EntitiesManager : IEcsEntityManager
    {
        private List<EcsEntity> activeEntities = new List<EcsEntity>();
        private List<EcsEntity> pooledEntity = new List<EcsEntity>();

        public List<EcsEntity> Entities => activeEntities;

        public EcsEntity GetNewEntity(EcsWorld world)
        {
            EcsEntity entity;

            if (pooledEntity.Count > 0)
            {
                entity = pooledEntity[0];
            }
            else
            {
                var id = pooledEntity.Count + activeEntities.Count;
                entity = new EcsEntity(world, id);
            }

            activeEntities.Add(entity);

            return activeEntities[^1];
        }


        public void RemoveEntity(EcsEntity entity)
        {
            activeEntities.Remove(entity);
            pooledEntity.Add(entity);
        }

        public void Dispose()
        {
            activeEntities.Clear();
            pooledEntity.Clear();
        }

        public bool TryGetEntityBy(int id, out EcsEntity entity)
        {
            for (int i = 0; i < activeEntities.Count; i++)
            {
                if (activeEntities[i].id == id)
                {
                    entity = activeEntities[i];
                    return true;
                }
            }

            entity = null;
            return false;
        }
    }
}