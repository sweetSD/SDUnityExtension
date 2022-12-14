using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by sweetSD. (static class)
/// 
/// Unity의 PlayerPrefs를 좀 더 안전하게 사용할 수 있도록
/// AES256을 이용하여 복호화하여 저장합니다.
/// 
/// </summary>
public class SDSecurityPlayerPrefs
{
    #region Utility Functions

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

    #endregion

    #region Setter Functions

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

    #endregion

    #region Getter Functions

    public static int GetInt(string key, int defaultValue = 0)
    {
        if (int.TryParse(SDSecurityManager.Decrypt(PlayerPrefs.GetString(key, string.Empty)), out var value))
        {
            return value;
        }
        return defaultValue;
    }

    public static string GetString(string key, string defaultValue = "")
    {
        return SDSecurityManager.Decrypt(PlayerPrefs.GetString(key, defaultValue));
    }

    public static float GetFloat(string key, float defaultValue = 0f)
    {
        if (float.TryParse(SDSecurityManager.Decrypt(PlayerPrefs.GetString(key, string.Empty)), out var value))
        {
            return value;
        }
        return defaultValue;
    }

    #endregion
}
