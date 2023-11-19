using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DG
{
    namespace Tweening
    {
        [RequireComponent(typeof(CanvasGroup))]
        public class DoUIFade : DoBase
        {
            [SerializeField] protected CanvasGroup canvasGroup;
            [SerializeField] protected float originAlpha;
            [SerializeField] protected float destAlpha;

            private void Awake()
            {
                if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
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
}