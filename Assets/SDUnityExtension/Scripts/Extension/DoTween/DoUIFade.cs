using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DoUIFade : DoBase
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected float originAlpha;
        [SerializeField] protected float destAlpha;

        private void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public override Tween GetTween()
        {
            return canvasGroup.DOFade(destAlpha, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return canvasGroup.DOFade(originAlpha, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            canvasGroup.alpha = originAlpha;
        }

        public override void ResetToEnd()
        {
            canvasGroup.alpha = destAlpha;
        }
    }
}