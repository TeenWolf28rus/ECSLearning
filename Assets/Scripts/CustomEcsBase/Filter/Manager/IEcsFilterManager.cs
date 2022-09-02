using System;
using CustomEcsBase.Filter.Filters;
using CustomEcsBase.World;

namespace CustomEcsBase.Filter.Manager
{
    public interface IEcsFilterManager
    {
        EcsBaseFilter GetFilter(EcsWorld world, Type filterType);
    }
}