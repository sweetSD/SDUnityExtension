namespace SDUnityExtension.Scripts.Extension
{
    public static class SDArrayExtension
    {
        public static void ForEach<T>(this T[] array, System.Action<T> action)
        {
            foreach (var t in array)
            {
                action(t);
            }
        }
    
        public static void ForEach<T>(this T[] array, System.Action<T, int> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action(array[i], i);
            }
        }
    }
}
