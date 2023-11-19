using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoRotation : DoBase
        {
            [SerializeField] private Vector3 originRotation;
            [SerializeField] private Vector3 destRotation;
            [SerializeField] private Space space = Space.World;
            [SerializeField] private RotateMode mode = RotateMode.FastBeyond360;
            
            public override Tween GetTween()
            {
                if (space == Space.World)
                    return transform.DORotate(destRotation, duration, mode).SetEase(ease);
                return transform.DOLocalRotate(destRotation, duration, mode).SetEase(ease);
            }

            public override Tween GetReversedTween()
            {
                if (space == Space.World)
                    return transform.DORotate(originRotation, duration, mode).SetEase(ease);
                return transform.DOLocalRotate(originRotation, duration, mode).SetEase(ease);
            }

            public override void ResetToStart()
            {
                transform.localEulerAngles = originRotation;
            }

            public override void ResetToEnd()
            {
                transform.localEulerAngles = destRotation;
            }
        }
    }
}