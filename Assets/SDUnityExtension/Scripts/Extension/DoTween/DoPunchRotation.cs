using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoPunchRotation : DoBase
    {
        [SerializeField] private Vector3 originRotation;
        [SerializeField] private Vector3 destRotation;
            
        public override Tween GetTween()
        {
            return transform.DOPunchRotation(destRotation, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return transform.DOPunchRotation(originRotation, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            transform.eulerAngles = originRotation;
        }

        public override void ResetToEnd()
        {
            transform.eulerAngles = destRotation;
        }
    }
}