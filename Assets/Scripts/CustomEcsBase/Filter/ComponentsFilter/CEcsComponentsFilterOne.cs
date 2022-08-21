using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.Filter.ComponentsFilter;
using CustomEcsBase.World;
using UnityEngine;

namespace CustomEcsBase.Filter
{
    public class CEcsComponentsFilterOne<T1> : CEcsBaseComponentsFilter where T1 : ICEcsComponent
    {
        private CEcsComponentsPool<T1> pool1;
        private List<T1> components1 = new List<T1>();

        private int componentsCount = 0;
        public List<T1> Get1 => components1;

        public override int ComponentsCount => componentsCount;

        public CEcsComponentsFilterOne(CEcsWorld world) : base(world)
        {
            pool1 = world.GetPool<T1>();
            if (pool1 == null)
            {
                Debug.LogError($"Filter can't find {typeof(T1)} pool");
                return;
            }

            pool1.ComponentsUpdated -= OnPoolChanged;
            pool1.ComponentsUpdated += OnPoolChanged;

            ValidatePools();
        }


        protected override void OnPoolChanged(int poolId) => ValidatePools();

        protected sealed override void ValidatePools()
        {
            componentsCount = 0;
            if (pool1 != null)
            {
                components1 = pool1.GetAllActiveComponents();
            }

            componentsCount = components1.Count;
        }
    }
}