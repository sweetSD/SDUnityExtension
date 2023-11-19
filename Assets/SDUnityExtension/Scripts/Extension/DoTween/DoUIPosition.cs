using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DG
{
    namespace Tweening
    {
        public class DoUIPosition : DoBase
        {
            [SerializeField] private RectTransform rectTransform;
            [SerializeField] private Vector2 originPosition;
            [SerializeField] private Vector2 destPosition;

            private void Awake()
            {
                if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
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
}