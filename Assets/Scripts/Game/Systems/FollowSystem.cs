using CustomEcsBase.Filter.ComponentsFilter;
using CustomEcsBase.Systems.EcsSystem;
using CustomEcsBase.World;
using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public class FollowSystem : ICEcsRunSystem
    {
        private const float RICH_DISTANCE = 1f;
        
        private CEcsWorld world;

        private CEcsComponentsFilterThree<FollowComponent, InputDataComponent, MovableComponent> componentsFilter;

        public void InjectionCompleted()
        {
            componentsFilter =
                new CEcsComponentsFilterThree<FollowComponent, InputDataComponent, MovableComponent>(world);
        }

        public void Run()
        {
            var follows = componentsFilter.Get1;
            var inputDatas = componentsFilter.Get2;
            var movables = componentsFilter.Get3;

            for (int i = 0; i < componentsFilter.ComponentsCount; i++)
            {
                var follow = follows[i];
                var input = inputDatas[i];
                var movable = movables[i];

                var direction = (follow.target.position - movable.transform.position);
                if (direction.sqrMagnitude < RICH_DISTANCE)
                {
                    input.direction = Vector2.zero;
                    return;
                }

                var normalizedDirection = direction.normalized;
                input.direction = new Vector2(normalizedDirection.x, normalizedDirection.z);
            }
        }
    }
}