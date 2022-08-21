using CustomEcsBase.Data;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = nameof(PlayerSharedData), menuName = "Game/SharedData/Player/" + nameof(PlayerSharedData))]
    public class PlayerSharedData : CEcsSharedData
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private float forwardMoveSpeed = 10f;
        [SerializeField] private float sideMoveSpeed = 10f;

        public GameObject PlayerPrefab => playerPrefab;

        public float ForwardMoveSpeed => forwardMoveSpeed;

        public float SideMoveSpeed => sideMoveSpeed;
    }
}