using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DG
{
    namespace Tweening
    {
        public class DoController : MonoBehaviour
        {
            [SerializeField] private List<DoBase> _doBases;

#if ODIN_INSPECTOR
            [Button]
            private void GetDoBases()
            {
                _doBases = GetComponents<DoBase>().ToList();
            }
            
            [Button]
            private void GetChildrenDoBases()
            {
                _doBases = GetComponentsInChildren<DoBase>().ToList();
            }
#endif

            public void Play(float delay = 0)
            {
                StartCoroutine(CO_Play(delay));
            }

            private IEnumerator CO_Play(float delay)
            {
                yield return new WaitForSeconds(delay);
                for (int i = 0; i < _doBases.Count; i++) _doBases[i].DOPlay();
            }

            public void PlayReverse(float delay = 0)
            {
                StartCoroutine(CO_PlayReverse(delay));
            }

            private IEnumerator CO_PlayReverse(float delay)
            {
                yield return new WaitForSeconds(delay);
                for (int i = 0; i < _doBases.Count; i++) _doBases[i].DoPlayReverse();
            }
        }

    }
}