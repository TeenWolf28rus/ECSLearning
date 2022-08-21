using UnityEngine;

namespace CustomEcsBase.Data
{
    public class CEcsSharedDataContainer : MonoBehaviour
    {
        [SerializeField] private CEcsSharedData[] data;

        public T Get<T>() where T : CEcsSharedData
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