using CustomEcsBase.Filter;
using CustomEcsBase.Systems.EcsSystem;
using CustomEcsBase.World;
using Game.Components;
using UnityEngine;

namespace Game.Systems
{
    public class PlayerInputSystem : ICEcsRunSystem
    {
        private CEcsWorld world;
        private CEcsComponentsFilterOne<InputDataComponent> componentFiler;

        public void InjectionCompleted()
        {
            componentFiler = new CEcsComponentsFilterOne<InputDataComponent>(world);
        }

        public void Run()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            var components = componentFiler.Get1;
            if (components == null)
            {
                Debug.LogError($"{nameof(PlayerInputSystem)} filter not found components");
                return;
            }

            for (int i = 0; i < componentFiler.ComponentsCount; i++)
            {
                var inputDataComponent = components[i];
                inputDataComponent.direction = new Vector2(x, y);
            }
        }
    }
}