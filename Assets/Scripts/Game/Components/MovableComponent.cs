using CustomEcsBase.Components.Interfaces;
using UnityEngine;

namespace Game.Components
{
    public class MovableComponent : ICEcsComponent
    {
        public Transform transform =  null;
        public float forwardSpeed = 0f;
        public float sideSpeed = 0f;
        public bool isMoving = false;

        private int entityId;

        public int EntityId => entityId;

        public void Init(int entityId)
        {
            this.entityId = entityId;
        }

        public void Reset()
        {
            forwardSpeed = 0f;
            sideSpeed = 0f;
            entityId = -1;
            transform = null;
        }
    }
}