using System.Collections.Generic;
using CustomEcsBase.World;

namespace CustomEcsBase.Entity
{
    public interface IEcsEntityManager
    {
        List<EcsEntity> Entities { get; }

        bool TryGetEntityBy(int id, out EcsEntity entity);

        EcsEntity GetNewEntity(EcsWorld world);
        void RemoveEntity(EcsEntity entity);

        void Dispose();
    }
}