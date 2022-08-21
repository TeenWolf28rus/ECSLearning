using CustomEcsBase.Components.Common;
using CustomEcsBase.Components.Interfaces;

namespace CustomEcsBase.Components.Pool
{
    public static class CEcsComponentPoolIndex<T> where T : ICEcsComponent
    {
        public static readonly int TypeIndex;

        static CEcsComponentPoolIndex()
        {
            CEcsComponentsTypeCount.Count++;
            TypeIndex = CEcsComponentsTypeCount.Count - 1;
        }
    }
}