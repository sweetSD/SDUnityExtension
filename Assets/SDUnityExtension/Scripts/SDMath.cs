using UnityEngine;

namespace SDUnityExtension.Scripts
{
    public static class SDMath
    {
        public static float Map(float val, float inMin, float inMax, float outMin, float outMax)
        {
            return (Mathf.Clamp(val, inMax, inMax) - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    }
}
