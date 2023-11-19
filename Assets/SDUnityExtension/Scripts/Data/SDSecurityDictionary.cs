using System.Collections.Generic;
using SDUnityExtension.Scripts.Manager;

public class SDSecurityDictionary
{
    private Dictionary<string, string> _internalMap = null;

    public SDSecurityDictionary()
    {
        _internalMap = new Dictionary<string, string>();
    }

    #region Dictionary Functions Override

    public int Count => _internalMap.Count;

    /// <summary>
    /// [중요] 인덱서 연산자를 통해 get한 string은 T2 형식으로 파싱하여 사용해주세요.
    /// 인덱서 연산자를 통해 set할 경우에는 자동으로 저장됩니다.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string this[string key]
    {
        get => SDSecurityManager.Decrypt(_internalMap[key]);
        set => _internalMap[key] = SDSecurityManager.Encrypt(value);
    }

    public void Add<T1, T2>(T1 key, T2 value)
    {
        _internalMap.Add(SDSecurityManager.Encrypt(key.ToString()), SDSecurityManager.Encrypt(value.ToString()));
    }

    public void Remove<T>(T key)
    {
        _internalMap.Remove(SDSecurityManager.Encrypt(key.ToString()));
    }

    public void Clear()
    {
        _internalMap.Clear();
    }

    /// <summary>
    /// 해당 키의 값이 있으면 true를 반환하고 가져옵니다.
    /// 얻은 value는 꼭 T2 형식으로 파싱하여 사용해주세요.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="key">딕셔너리의 Key값</param>
    /// <param name="value">딕셔너리 해당 Key값의 value값. (없을 시 빈 문자열)</param>
    /// <returns></returns>
    public bool TryGetValue<T>(T key, out string value)
    {
        string tempValue = null;
        if(_internalMap.TryGetValue(SDSecurityManager.Encrypt(key.ToString()), out tempValue))
        {
            value = SDSecurityManager.Decrypt(tempValue);
            return true;
        }
        else
        {
            value = string.Empty;
            return false;
        }
    }

    public bool ContainsKey<T>(T key)
    {
        return _internalMap.ContainsKey(SDSecurityManager.Encrypt(key.ToString()));
    }

    public bool ContainsValue<T>(T value)
    {
        return _internalMap.ContainsValue(SDSecurityManager.Encrypt(value.ToString()));
    }

    #endregion

    #region Get Functions

    public int GetIntValue<T>(T key)
    {
        return int.Parse(this[SDSecurityManager.Encrypt(key.ToString())]);
    }

    public float GetFloatValue<T>(T key)
    {
        return float.Parse(this[SDSecurityManager.Encrypt(key.ToString())]);
    }

    public string GetStringValue<T>(T key)
    {
        return this[SDSecurityManager.Encrypt(key.ToString())];
    }

    public double GetDoubleValue<T>(T key)
    {
        return double.Parse(this[SDSecurityManager.Encrypt(key.ToString())]);
    }

    public decimal GetDecimalValue<T>(T key)
    {
        return decimal.Parse(this[SDSecurityManager.Encrypt(key.ToString())]);
    }

    #endregion
}
