using System;
using UnityEngine;

namespace SDUnityExtension.Scripts.Logic.Filter
{
    /// <summary>
    /// Trigger에 사용되는 Filter입니다.
    /// 특정 Tag에만 Trigger가 반응하도록 합니다.
    /// Inspector가 아닌 Script로 필터를 지정할 때 사용됩니다.
    /// </summary>
    [Serializable]
    public class TagFilterImpl : IFilter
    {
        [SerializeField] private string targetTag = "Player";

        public string TargetTag
        {
            get => targetTag;
            set => targetTag = value;
        }
    
        public bool Test(GameObject other)
        {
            return other.CompareTag(targetTag);
        }
    }

    /// <summary>
    /// Trigger에 사용되는 Filter입니다.
    /// 특정 Tag에만 Trigger가 반응하도록 합니다.
    /// </summary>
    public class TagFilter : MonoBehaviour, IFilter
    {
        [SerializeField] private TagFilterImpl tagFilter;
        
        public string TargetTag
        {
            get => tagFilter.TargetTag;
            set => tagFilter.TargetTag = value;
        }
    
        public bool Test(GameObject other)
        {
            return tagFilter.Test(other);
        }
    }
}