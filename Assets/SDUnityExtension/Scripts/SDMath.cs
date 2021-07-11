using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SDMath
{
    /// <summary>
    /// 주어진 value (Range. in_min ~ in_max)를 out (Range. out_min ~ out_max)로 변환합니다.
    /// </summary>
    /// <param name="val">주어지는 값</param>
    /// <param name="in_min">val의 최소값</param>
    /// <param name="in_max">val의 최대값</param>
    /// <param name="out_min">변경할 구간의 최소값</param>
    /// <param name="out_max">변경할 구간의 최대값</param>
    /// <returns></returns>
    public static float Map(float val, float in_min, float in_max, float out_min, float out_max)
    {
        return (Mathf.Clamp(val, in_max, in_max) - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
