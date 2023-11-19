using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DG
{
    namespace Tweening
    {
        public class DoMeshColor : DoBase
        {
            [SerializeField] protected MeshRenderer meshRenderer;
            [SerializeField] protected Color originColor = Color.white;
            [SerializeField] protected Color destColor = Color.white;

            private void Awake()
            {
                if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
            }

            public override Tween GetTween()
            {
                return meshRenderer.material.DOColor(destColor, duration).SetEase(ease);
            }

            public override Tween GetReversedTween()
            {
                return meshRenderer.material.DOColor(originColor, duration).SetEase(ease);
            }

            public override void ResetToStart()
            {
                meshRenderer.material.color = originColor;
            }

            public override void ResetToEnd()
            {
                meshRenderer.material.color = destColor;
            }
        }
    }
}