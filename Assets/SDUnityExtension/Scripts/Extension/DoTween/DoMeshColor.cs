using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    namespace Tweening
    {
        public class DoMeshColor : DoBase
        {
            [SerializeField] protected Renderer _renderer;
            [SerializeField] protected Color _originColor = Color.white;
            [SerializeField] protected Color _destColor = Color.white;

            private void Awake()
            {
                if (_renderer == null) _renderer = GetComponent<Renderer>();
            }

            public override Tween GetTween()
            {
                return _renderer.material.DOColor(_destColor, _duration).SetEase(_ease);
            }

            public override Tween GetReversedTween()
            {
                return _renderer.material.DOColor(_originColor, _duration).SetEase(_ease);
            }

            public override void ResetToStart()
            {
                _renderer.material.color = _originColor;
            }

            public override void ResetToEnd()
            {
                _renderer.material.color = _destColor;
            }
        }
    }
}