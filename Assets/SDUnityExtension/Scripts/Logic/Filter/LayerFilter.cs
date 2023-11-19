using System;
using UnityEngine;

namespace SDUnityExtension.Scripts.Logic.Filter
{
    /// <summary>
    /// Trigger에 사용되는 Filter입니다.
    /// 특정 Layer에만 Trigger가 반응하도록 합니다.
    /// Inspector가 아닌 Script로 필터를 지정할 때 사용됩니다.
    /// </summary>
    [Serializable]
    public class LayerFilterImpl : IFilter
    {
        [SerializeField] private LayerMask targetLayer = int.MaxValue;
    
        public LayerMask TargetLayer
        {
            get => targetLayer;
            set => targetLayer = value;
        }

        public bool Test(GameObject other)
        {
            return (TargetLayer & (1 << other.gameObject.layer)) != 0;
        }
    }


    /// <summary>
    /// Trigger에 사용되는 Filter입니다.
    /// 특정 Layer에만 Trigger가 반응하도록 합니다.
    /// </summary>
    public class LayerFilter : MonoBehaviour, IFilter
    {
        [SerializeField] private LayerFilterImpl layerFilter;
    
        public bool Test(GameObject other)
        {
            return layerFilter.Test(other);
        }
    }
}