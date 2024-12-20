using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoScale : DoBase
    {
        [SerializeField] private Vector3 originScale;
        [SerializeField] private Vector3 destScale;
            
        public override Tween GetTween()
        {
            return transform.DOScale(destScale, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return transform.DOScale(originScale, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            transform.localScale = originScale;
        }

        public override void ResetToEnd()
        {
            transform.localScale = destScale;
        }
    }
}