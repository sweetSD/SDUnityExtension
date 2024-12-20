using UnityEngine;

public struct SecureFloat
{
    public float Value
    {
        get => GetValue();
        set => SetValue(value);
    }
    
    private float value;
    private readonly ushort key;
    
    public SecureFloat(float value)
    {
        this.key = (ushort)Random.Range(ushort.MinValue + 1, ushort.MaxValue);
        this.value = default;
        SetValue(value);
    }

    public SecureFloat(float value, ushort key)
    {
        this.key = key;
        this.value = default;
        SetValue(value);
    }

    private float GetValue()
    {
        return value * key;
    }

    private void SetValue(float newValue)
    {
        value = newValue / key;
    }
    
    public static implicit operator float(SecureFloat secureFloat)
    {
        return secureFloat.GetValue();
    }
}
