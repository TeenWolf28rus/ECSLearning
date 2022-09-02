using CustomEcsBase.World;

namespace CustomEcsBase.Filter.Filters
{
    public abstract class EcsBaseFilter
    {
        protected  EcsWorld world;
        
        public virtual void Init(EcsWorld world)
        {
            this.world = world;
        }
    }
}