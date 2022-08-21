using System;

namespace Common.Utils
{
    public static class ArrayUtils
    {
        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {
                arr[a] = arr[a + 1];
            }

            Array.Resize(ref arr, arr.Length - 1);
        }
    }
}