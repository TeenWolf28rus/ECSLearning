using CustomEcsBase.Filter.DataFilter;
using CustomEcsBase.Systems.EcsSystem;
using CustomEcsBase.World;
using Game.Components;
using Game.Data;
using UnityEngine;

namespace Game.Systems
{
    public class GameInitSystem : ICEcsInitSystem
    {
        private CEcsWorld world = null;

        private CEcsSharedDataFilterTwo<PlayerSharedData, EnemySharedData> dataFilter;

        public void Init()
        {
            var playerTransform = CreatePlayer();
            for (int i = 0; i < 10; i++)
            {
                CreateEnemy(playerTransform);
            }
        }

        private Transform CreatePlayer()
        {
            var player = world.GetNewEntity();
            var movableComponent = player.GetComponent<MovableComponent>();
            var inputComponent = player.GetComponent<InputDataComponent>();

            var playerData = dataFilter.Get1;
            var spawnedPlayerPrefab =
                GameObject.Instantiate(playerData.PlayerPrefab, Vector3.zero, Quaternion.identity);

            movableComponent.forwardSpeed = playerData.ForwardMoveSpeed;
            movableComponent.sideSpeed = playerData.SideMoveSpeed;
            movableComponent.transform = spawnedPlayerPrefab.transform;

            return spawnedPlayerPrefab.transform;
        }

        private void CreateEnemy(Transform target)
        {
            var enemy = world.GetNewEntity();
            var movableComponent = enemy.GetComponent<MovableComponent>();
            var followComponent = enemy.GetComponent<FollowComponent>();
            var inputComponent = enemy.GetComponent<InputDataComponent>();


            var enemyData = dataFilter.Get2;
            var playerPosition = target.position;
            var spawnPosition = playerPosition + Vector3.forward * Random.Range(-2f, 2f) +
                                Vector3.right * Random.Range(-2f, 2f);
            var spawnedEnemyPrefab = GameObject.Instantiate(enemyData.EnemyPrefab, spawnPosition, Quaternion.identity);

            movableComponent.transform = spawnedEnemyPrefab.transform;
            movableComponent.forwardSpeed = enemyData.ForwardMoveSpeed;
            movableComponent.sideSpeed = enemyData.SideMoveSpeed;
            followComponent.target = target;
        }


        public void InjectionCompleted()
        {
            dataFilter = new CEcsSharedDataFilterTwo<PlayerSharedData, EnemySharedData>(world);
        }
    }
}