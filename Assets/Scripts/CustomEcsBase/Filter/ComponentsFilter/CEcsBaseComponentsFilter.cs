using CustomEcsBase.World;

namespace CustomEcsBase.Filter.ComponentsFilter
{
    public abstract class CEcsBaseComponentsFilter : CEcsBaseFilter
    {
        public abstract int ComponentsCount { get; }

        public CEcsBaseComponentsFilter(CEcsWorld world) : base(world)
        {
        }

        protected abstract void OnPoolChanged(int poolId);
        protected abstract void ValidatePools();
    }
}