using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoPunchScale : DoBase
        {
            [SerializeField] private Vector3 originScale;
            [SerializeField] private Vector3 destScale;
            
            public override Tween GetTween()
            {
                return transform.DOPunchScale(destScale, duration).SetEase(ease);
            }

            public override Tween GetReversedTween()
            {
                return transform.DOPunchScale(originScale, duration).SetEase(ease);
            }

            public override void ResetToStart() { }

            public override void ResetToEnd() { }
        }
    }
}