using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoColor : DoBase
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Color originColor = Color.white;
        [SerializeField] protected Color destColor = Color.white;

        private void Awake()
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override Tween GetTween()
        {
            return spriteRenderer.DOColor(destColor, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return spriteRenderer.DOColor(originColor, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            spriteRenderer.color = originColor;
        }

        public override void ResetToEnd()
        {
            spriteRenderer.color = destColor;
        }
    }
}