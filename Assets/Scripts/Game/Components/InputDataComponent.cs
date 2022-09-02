using CustomEcsBase.Components.Interfaces;
using UnityEngine;

namespace Game.Components
{
    public class InputDataComponent : IEcsComponent
    {
        public Vector2 direction = Vector2.zero;

        private int entityId = -1;

        public int EntityId => entityId;

        public void Init(int entityId)
        {
            this.entityId = entityId;
        }

        public void Reset()
        {
            entityId = -1;
            direction = Vector2.zero;
        }
    }
}