using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoUIPosition : DoBase
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector2 originPosition;
        [SerializeField] private Vector2 destPosition;

        private void Awake()
        {
            if (rectTransform == null)
            {
                rectTransform = transform as RectTransform;
            }
        }

        public override Tween GetTween()
        {
            return rectTransform.DOAnchorPos(destPosition, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return rectTransform.DOAnchorPos(originPosition, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            rectTransform.anchoredPosition = originPosition;
        }

        public override void ResetToEnd()
        {
            rectTransform.anchoredPosition = destPosition;
        }
    }
}