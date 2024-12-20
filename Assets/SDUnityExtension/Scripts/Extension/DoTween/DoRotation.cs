using DG.Tweening;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoRotation : DoBase
    {
        [SerializeField] private Vector3 originRotation;
        [SerializeField] private Vector3 destRotation;
        [SerializeField] private Space space = Space.World;
        [SerializeField] private RotateMode mode = RotateMode.FastBeyond360;
            
        public override Tween GetTween()
        {
            return space == Space.World 
                ? transform.DORotate(destRotation, duration, mode).SetEase(ease) 
                : transform.DOLocalRotate(destRotation, duration, mode).SetEase(ease);
        }

        public override Tween GetReversedTween()
        {
            return space == Space.World 
                ? transform.DORotate(originRotation, duration, mode).SetEase(ease) 
                : transform.DOLocalRotate(originRotation, duration, mode).SetEase(ease);
        }

        public override void ResetToStart()
        {
            if (space == Space.World)
            {
                transform.eulerAngles = originRotation;
            }
            else
            {
                transform.localEulerAngles = originRotation;
            }
        }

        public override void ResetToEnd()
        {
            if (space == Space.World)
            {
                transform.eulerAngles = destRotation;
            }
            else
            {
                transform.localEulerAngles = destRotation;
            }
        }
    }
}