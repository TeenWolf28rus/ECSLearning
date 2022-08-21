using CustomEcsBase.Data;
using CustomEcsBase.Systems;
using CustomEcsBase.World;
using Game.Systems;
using UnityEngine;

namespace Game.Loading
{
    public class WorldLoader : MonoBehaviour
    {
        [SerializeField] private CEcsSharedDataContainer sharedDataContainer;

        private CEcsWorld world;
        private CEcsSystems systems;

        private void Start()
        {
            world = new CEcsWorld(sharedDataContainer);
            systems = new CEcsSystems(world);

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

        private void OnDestroy()
        {
            systems?.Dispose();
            world?.Dispose();
        }
    }
}