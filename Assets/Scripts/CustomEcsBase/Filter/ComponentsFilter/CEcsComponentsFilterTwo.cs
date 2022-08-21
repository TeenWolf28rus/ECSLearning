﻿using System.Collections.Generic;
using CustomEcsBase.Components.Interfaces;
using CustomEcsBase.Components.Pool;
using CustomEcsBase.World;

namespace CustomEcsBase.Filter.ComponentsFilter
{
    public class CEcsComponentsFilterTwo<T1, T2> : CEcsBaseComponentsFilter
        where T1 : ICEcsComponent
        where T2 : ICEcsComponent
    {
        private CEcsComponentsPool<T1> pool1;
        private List<T1> components1 = new List<T1>();

        private CEcsComponentsPool<T2> pool2;
        private List<T2> components2 = new List<T2>();

        private int componentsCount = 0;

        public List<T1> Get1 => components1;
        public List<T2> Get2 => components2;
        public override int ComponentsCount => componentsCount;

        public CEcsComponentsFilterTwo(CEcsWorld world) : base(world)
        {
            pool1 = world.GetPool<T1>();
            if (pool1 != null)
            {
                pool1.ComponentsUpdated -= OnPoolChanged;
                pool1.ComponentsUpdated += OnPoolChanged;
            }

            pool2 = world.GetPool<T2>();
            if (pool2 != null)
            {
                pool2.ComponentsUpdated -= OnPoolChanged;
                pool2.ComponentsUpdated += OnPoolChanged;
            }

            ValidatePools();
        }

        protected override void OnPoolChanged(int poolId) => ValidatePools();


        protected sealed override void ValidatePools()
        {
            componentsCount = 0;
            var entityIndexes1 = new List<int>();
            List<T1> allComponents1 = new List<T1>();
            if (pool1 != null)
            {
                allComponents1 = pool1.GetAllActiveComponents();
                if (allComponents1.Count <= 0) return;
                for (int i = 0; i < allComponents1.Count; i++)
                {
                    entityIndexes1.Add(allComponents1[i].EntityId);
                }
            }

            var entityIndexes2 = new List<int>();
            List<T2> allComponents2 = new List<T2>();
            if (pool2 != null)
            {
                allComponents2 = pool2.GetAllActiveComponents();
                if (allComponents2.Count <= 0) return;

                for (int i = 0; i < allComponents2.Count; i++)
                {
                    var id = allComponents2[i].EntityId;

                    if (entityIndexes1.Contains(id))
                    {
                        entityIndexes2.Add(id);
                    }
                }
            }

            components1.Clear();
            for (int i = 0; i < allComponents1.Count; i++)
            {
                if (entityIndexes2.Contains(allComponents1[i].EntityId))
                {
                    components1.Add(allComponents1[i]);
                }
            }

            components2.Clear();
            for (int i = 0; i < allComponents2.Count; i++)
            {
                if (entityIndexes2.Contains(allComponents2[i].EntityId))
                {
                    components2.Add(allComponents2[i]);
                }
            }

            componentsCount = entityIndexes2.Count;
        }
    }
}