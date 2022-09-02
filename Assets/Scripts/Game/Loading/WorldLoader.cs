using CustomEcsBase.Data;
using CustomEcsBase.Systems;
using CustomEcsBase.World;
using Game.Systems;
using UnityEngine;

namespace Game.Loading
{
    public class WorldLoader : MonoBehaviour
    {
        [SerializeField] private EcsSharedDataContainer sharedDataContainer;

        private EcsWorld world;
        private EcsSystems systems;

        private void Start()
        {
            world = new EcsWorld(sharedDataContainer);
            systems = new EcsSystems(world);

            systems?.Add(new GameInitSystem());
            systems?.Add(new PlayerInputSystem());
            systems?.Add(new FollowSystem());
            systems?.Add(new PlayerMoveSystem());

            systems?.Init();
        }

        private void Update()
        {
            systems?.Run();
        }

        private void FixedUpdate()
        {
            systems?.FixedRun();
        }

        private void OnDestroy()
        {
            systems?.Dispose();
            world?.Dispose();
        }
    }
}