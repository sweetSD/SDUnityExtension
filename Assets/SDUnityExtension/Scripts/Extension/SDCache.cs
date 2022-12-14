using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SDCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    
    private static Dictionary<float, WaitForSeconds> wfsCache = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitForSeconds(float sec)
    {
        if (!wfsCache.TryGetValue(sec, out var wfs))
            wfsCache.Add(sec, wfs = new WaitForSeconds(sec));
        return wfs;
    }
    
    private static Dictionary<float, WaitForSecondsRealtime> wfsrCache = new Dictionary<float, WaitForSecondsRealtime>();
    public static WaitForSecondsRealtime WaitForSecondsRealtime(float sec)
    {
        if (!wfsrCache.TryGetValue(sec, out var wfs))
            wfsrCache.Add(sec, wfs = new WaitForSecondsRealtime(sec));
        return wfs;
    }
}