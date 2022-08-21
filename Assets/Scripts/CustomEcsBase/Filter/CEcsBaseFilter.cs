using CustomEcsBase.World;

namespace CustomEcsBase.Filter
{
    public abstract class CEcsBaseFilter
    {
        protected readonly CEcsWorld world;

        public CEcsBaseFilter(CEcsWorld world)
        {
            this.world = world;
        }
    }
}