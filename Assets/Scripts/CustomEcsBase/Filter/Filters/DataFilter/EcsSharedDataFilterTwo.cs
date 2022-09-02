using CustomEcsBase.Data;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Filter.Filters.DataFilter
{
    public class EcsSharedDataFilterTwo<T1, T2> : EcsBaseSharedDataFilter
        where T1 : EcsSharedData
        where T2 : EcsSharedData
    {
        private T1 data1;
        private T2 data2;

        public T1 Get1 => data1;
        public T2 Get2 => data2;

        public override void Init(EcsWorld world)
        {
            base.Init(world);
            
            if (world.SharedDataContainer == null)
            {
                Debug.LogError("Data container does not exist!");
                return;
            }

            data1 = world.SharedDataContainer.Get<T1>();
            data2 = world.SharedDataContainer.Get<T2>();
        }
    }
}