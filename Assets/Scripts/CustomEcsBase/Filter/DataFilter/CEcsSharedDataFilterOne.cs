using CustomEcsBase.Data;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Filter.DataFilter
{
    public class CEcsSharedDataFilterOne<T1> : CEcsBaseFilter where T1 : CEcsSharedData
    {
        private T1 data1;

        public T1 Get1 => data1;

        public CEcsSharedDataFilterOne(CEcsWorld world) : base(world)
        {
            if (world.SharedDataContainer == null)
            {
                Debug.LogError("Data container does not exist!");
                return;
            }

            data1 = world.SharedDataContainer.Get<T1>();
        }
    }
}