using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DG
{
    namespace Tweening
    {
        public class DoUIFill : DoBase
        {
            [SerializeField] protected Image image;
            [SerializeField] protected float originFillAmount;
            [SerializeField] protected float destFillAmount;

            private void Awake()
            {
                if (image == null) image = GetComponent<Image>();
            }

            public override Tween GetTween()
            {
                return image.DOFillAmount(Mathf.Clamp(destFillAmount, 0, 1), duration).SetEase(ease);
            }

            public override Tween GetReversedTween()
            {
                return image.DOFillAmount(Mathf.Clamp(originFillAmount, 0, 1), duration).SetEase(ease);
            }

            public override void ResetToStart()
            {
                image.fillAmount = Mathf.Clamp(originFillAmount, 0, 1);
            }

            public override void ResetToEnd()
            {
                image.fillAmount = Mathf.Clamp(destFillAmount, 0, 1);
            }
        }
    }
}