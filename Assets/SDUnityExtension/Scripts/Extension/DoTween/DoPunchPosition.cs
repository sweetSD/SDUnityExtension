using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoPunchPosition : DoBase
        {
            [SerializeField] private Vector3 originPosition;
            [SerializeField] private Vector3 destPosition;
            
            public override Tween GetTween()
            {
                return transform.DOPunchPosition(destPosition, duration).SetEase(ease);
            }

            public override Tween GetReversedTween()
            {
                return transform.DOPunchPosition(originPosition, duration).SetEase(ease);
            }

            public override void ResetToStart()
            {
                transform.position = originPosition;
            }

            public override void ResetToEnd()
            {
                transform.position = destPosition;
            }
        }
    }
}