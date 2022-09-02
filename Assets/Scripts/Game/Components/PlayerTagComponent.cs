using CustomEcsBase.Components.Interfaces;

namespace Game.Components
{
    public class PlayerTagComponent : IEcsComponent
    {
        private int entityId;

        public int EntityId { get; }

        public void Init(int entityId)
        {
            this.entityId = entityId;
        }

        public void Reset()
        {
            this.entityId = -1;
        }
    }
}