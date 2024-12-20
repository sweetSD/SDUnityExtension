using UnityEngine;

public struct SecureInt
{
    public int Value
    {
        get => GetValue();
        set => SetValue(value);
    }
    
    private int value;
    private readonly int key;

    public SecureInt(int value)
    {
        this.key = Random.Range(int.MinValue, int.MaxValue);
        this.value = default;
        SetValue(value);
    }
    
    public SecureInt(int value, int key)
    {
        this.key = key;
        this.value = default;
        SetValue(value);
    }

    private int GetValue()
    {
        return value ^ key;
    }

    private void SetValue(int newValue)
    {
        value = newValue ^ key;
    }
    
    public static implicit operator int(SecureInt secureFloat)
    {
        return secureFloat.GetValue();
    }
}