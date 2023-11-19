using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoPosition : DoBase
        {
            [SerializeField] private Vector3 originPosition;
            [SerializeField] private Vector3 destPosition;
            [SerializeField] private Space space = Space.World;
            
            public override Tween GetTween()
            {
                if (space == Space.World)
                    return transform.DOMove(destPosition, duration).SetEase(ease);
                return transform.DOLocalMove(destPosition, duration).SetEase(ease);
            }

            public override Tween GetReversedTween()
            {
                if (space == Space.World)
                    return transform.DOMove(originPosition, duration).SetEase(ease);
                return transform.DOLocalMove(originPosition, duration).SetEase(ease);
            }

            public override void ResetToStart()
            {
                if (space == Space.World)
                    transform.position = originPosition;
                else
                    transform.localPosition = originPosition;
            }

            public override void ResetToEnd()
            {
                if (space == Space.World)
                    transform.position = originPosition;
                else
                    transform.localPosition = originPosition;
            }
        }

    }
}