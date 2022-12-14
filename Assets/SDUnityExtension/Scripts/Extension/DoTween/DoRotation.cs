using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoRotation : DoBase
        {
            public override Tween GetTween()
            {
                return transform.DOLocalRotate(_destValue, _duration).SetEase(_ease);
            }

            public override Tween GetReversedTween()
            {
                return transform.DOLocalRotate(_originValue, _duration).SetEase(_ease);
            }

            public override void ResetToStart()
            {
                transform.localEulerAngles = _originValue;
            }

            public override void ResetToEnd()
            {
                transform.localEulerAngles = _destValue;
            }
        }
    }
}