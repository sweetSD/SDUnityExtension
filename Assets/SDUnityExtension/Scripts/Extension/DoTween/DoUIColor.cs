﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoUIColor : DoBase
    {
        [SerializeField] protected Graphic graphic;
        [SerializeField] protected Color originColor = Color.white;
        [SerializeField] protected Color destColor = Color.white;

        private void Awake()
        {
            if (graphic == null)
            {
                graphic = GetComponent<Graphic>();
            }
        }

        public override Tween GetTween()
        {
            return graphic.DOColor(destColor, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return graphic.DOColor(originColor, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            graphic.color = originColor;
        }

        public override void ResetToEnd()
        {
            graphic.color = destColor;
        }
    }
}