namespace SDUnityExtension.Scripts.Extension
{
    public static class SDStringExtension
    {
        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
