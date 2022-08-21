using CustomEcsBase.Components.Interfaces;
using UnityEngine;

namespace Game.Components
{
    public class FollowComponent : ICEcsComponent
    {
        public Transform target = null;
        private int entityId = -1;
        public int EntityId => entityId;

        public void Init(int entityId)
        {
            this.entityId = entityId;
        }

        public void Reset()
        {
            target = null;
            entityId = -1;
        }
    }
}