using CustomEcsBase.Data;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = nameof(EnemySharedData), menuName = "Game/SharedData/Enemy/" + nameof(EnemySharedData))]
    public class EnemySharedData : EcsSharedData
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float forwardMoveSpeed = 8f;
        [SerializeField] private float sideMoveSpeed = 8f;

        public GameObject EnemyPrefab => enemyPrefab;

        public float ForwardMoveSpeed => forwardMoveSpeed;

        public float SideMoveSpeed => sideMoveSpeed;
    }
}