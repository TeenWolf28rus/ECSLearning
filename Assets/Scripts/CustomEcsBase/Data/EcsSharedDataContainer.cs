using UnityEngine;

namespace CustomEcsBase.Data
{
    public class EcsSharedDataContainer : MonoBehaviour
    {
        [SerializeField] private EcsSharedData[] data;

        public T Get<T>() where T : EcsSharedData
        {
            if (data == null) return null;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] is T)
                {
                    return (T)data[i];
                }
            }

            return null;
        }
    }
}