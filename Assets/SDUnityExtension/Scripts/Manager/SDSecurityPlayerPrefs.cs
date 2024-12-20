using UnityEngine;

namespace SDUnityExtension.Scripts.Manager
{
    public class SDSecurityPlayerPrefs
    {
        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetString(key, SDSecurityManager.Encrypt($"{value}"));
        }

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, SDSecurityManager.Encrypt(value));
        }

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetString(key, SDSecurityManager.Encrypt($"{value}"));
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return int.TryParse(SDSecurityManager.Decrypt(PlayerPrefs.GetString(key, string.Empty)), out var value) ? value : defaultValue;
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return SDSecurityManager.Decrypt(PlayerPrefs.GetString(key, defaultValue));
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            return float.TryParse(SDSecurityManager.Decrypt(PlayerPrefs.GetString(key, string.Empty)), out var value) ? value : defaultValue;
        }
    }
}
