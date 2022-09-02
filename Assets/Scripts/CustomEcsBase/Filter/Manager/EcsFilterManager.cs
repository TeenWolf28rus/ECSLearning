using System;
using CustomEcsBase.Filter.Filters;
using CustomEcsBase.Filter.Filters.ComponentsFilter;
using CustomEcsBase.Filter.Filters.DataFilter;
using CustomEcsBase.World;

namespace CustomEcsBase.Filter.Manager
{
    public class EcsFilterManager : IEcsFilterManager
    {
        public EcsBaseComponentsFilter[] componentsFilters;
        public EcsBaseSharedDataFilter[] sharedDataFilters;


        public EcsBaseFilter GetFilter(EcsWorld world, Type filterType)
        {
            if (!filterType.IsSubclassOf(typeof(EcsBaseFilter))) return null;

            if (filterType.IsSubclassOf(typeof(EcsBaseComponentsFilter)))
            {
                return GetComponentFilter(world, filterType);
            }

            if (filterType.IsSubclassOf(typeof(EcsBaseSharedDataFilter)))
            {
                return GetSharedDataFilter(world, filterType);
            }

            return null;
        }

        private EcsBaseComponentsFilter GetComponentFilter(EcsWorld world, Type filterType)
        {
            EcsBaseComponentsFilter filter;
            if (componentsFilters == null)
            {
                filter = (EcsBaseComponentsFilter) Activator.CreateInstance(filterType);
                filter.Init(world);
                componentsFilters = new[]
                {
                    filter
                };


                return componentsFilters[0];
            }

            for (int i = 0; i < componentsFilters.Length; i++)
            {
                if (componentsFilters[i].GetType() == filterType)
                {
                    return componentsFilters[i];
                }
            }

            filter = (EcsBaseComponentsFilter) Activator.CreateInstance(filterType);
            filter.Init(world);
            Array.Resize(ref componentsFilters, componentsFilters.Length + 1);
            componentsFilters[^1] = filter;
            return filter;
        }

        private EcsBaseSharedDataFilter GetSharedDataFilter(EcsWorld world, Type filterType)
        {
            EcsBaseSharedDataFilter filter;
            if (sharedDataFilters == null)
            {
                filter = (EcsBaseSharedDataFilter) Activator.CreateInstance(filterType);
                filter.Init(world);

                sharedDataFilters = new[]
                {
                    filter
                };


                return filter;
            }

            for (int i = 0; i < componentsFilters.Length; i++)
            {
                if (sharedDataFilters[i].GetType() == filterType)
                {
                    return sharedDataFilters[i];
                }
            }

            filter = (EcsBaseSharedDataFilter) Activator.CreateInstance(filterType);
            filter.Init(world);
            Array.Resize(ref sharedDataFilters, sharedDataFilters.Length + 1);
            sharedDataFilters[^1] = filter;
            return filter;
        }
    }
}