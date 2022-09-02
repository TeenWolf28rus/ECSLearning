using CustomEcsBase.Components.Common;
using CustomEcsBase.Components.Interfaces;

namespace CustomEcsBase.Components.Pool
{
    public static class EcsComponentPoolIndex<T> where T : IEcsComponent
    {
        public static readonly int TypeIndex;

        static EcsComponentPoolIndex()
        {
            CEcsComponentsTypeCount.Count++;
            TypeIndex = CEcsComponentsTypeCount.Count - 1;
        }
    }
}