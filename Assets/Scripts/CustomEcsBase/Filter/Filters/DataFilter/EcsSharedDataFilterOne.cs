using CustomEcsBase.Data;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Filter.Filters.DataFilter
{
    public class EcsSharedDataFilterOne<T1> : EcsBaseSharedDataFilter where T1 : EcsSharedData
    {
        private T1 data1;

        public T1 Get1 => data1;


        public override void Init(EcsWorld world)
        {
            base.Init(world);
          
            if (world.SharedDataContainer == null)
            {
                Debug.LogError("Data container does not exist!");
                return;
            }

            data1 = world.SharedDataContainer.Get<T1>();
        }
    }
}