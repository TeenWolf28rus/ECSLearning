using CustomEcsBase.Data;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Filter.DataFilter
{
    public class CEcsSharedDataFilterTwo<T1,T2> : CEcsBaseFilter 
        where T1 : CEcsSharedData
        where T2 : CEcsSharedData
    {
        private T1 data1;
        private T2 data2;

        public T1 Get1 => data1;
        public T2 Get2 => data2;
        
        public CEcsSharedDataFilterTwo(CEcsWorld world) : base(world)
        {
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