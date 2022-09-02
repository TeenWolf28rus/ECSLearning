using CustomEcsBase.Filter.Filters.ComponentsFilter;
using CustomEcsBase.Systems.EcsSystem;
using CustomEcsBase.World;
using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsWorld world;
        private EcsComponentsFilterTwo<MovableComponent, InputDataComponent> componentFiler;

        public void Run()
        {
            var moveComponents = componentFiler.Get1;
            var inputDataComponents = componentFiler.Get2;

            for (int i = 0; i < componentFiler.ComponentsCount; i++)
            {
                var moveComponent = moveComponents[i];
                var inputDataComponent = inputDataComponents[i];

                var moveDelta = new Vector3(inputDataComponent.direction.x * moveComponent.sideSpeed, 0f,
                    inputDataComponent.direction.y * moveComponent.forwardSpeed);
                moveComponent.transform.position += moveDelta * Time.deltaTime;

                moveComponent.isMoving = inputDataComponent.direction.sqrMagnitude > 0;
            }
        }
    }
}