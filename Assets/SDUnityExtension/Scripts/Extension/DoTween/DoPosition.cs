using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoPosition : DoBase
    {
        [SerializeField] private Vector3 originPosition;
        [SerializeField] private Vector3 destPosition;
        [SerializeField] private Space space = Space.World;
            
        public override Tween GetTween()
        {
            return space == Space.World
                ? transform.DOMove(destPosition, duration).SetEase(ease)
                : transform.DOLocalMove(destPosition, duration).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return space == Space.World
                ? transform.DOMove(originPosition, duration).SetEase(ease)
                : transform.DOLocalMove(originPosition, duration).SetEase(ease);
        }

        public override void ResetToStart()
        {
            if (space == Space.World)
            {
                transform.position = originPosition;
            }
            else
            {
                transform.localPosition = originPosition;
            }
        }

        public override void ResetToEnd()
        {
            if (space == Space.World)
            {
                transform.position = originPosition;
            }
            else
            {
                transform.localPosition = originPosition;
            }
        }
    }
}