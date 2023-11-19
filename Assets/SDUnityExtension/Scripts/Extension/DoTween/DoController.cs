using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SDUnityExtension.Scripts.Extension;
using UnityEngine;
using UnityEngine.Serialization;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DG
{
    namespace Tweening
    {
        public class DoController : MonoBehaviour
        {
            [SerializeField] private List<DoBase> doBases;

#if ODIN_INSPECTOR
            [Button]
            private void GetDoBases()
            {
                doBases = GetComponents<DoBase>().ToList();
            }
            
            [Button]
            private void GetChildrenDoBases()
            {
                doBases = GetComponentsInChildren<DoBase>().ToList();
            }
#endif

            public void Play(float delay = 0)
            {
                StartCoroutine(CO_Play(delay));
            }

            private IEnumerator CO_Play(float delay)
            {
                yield return SDCache.WaitForSeconds(delay);
                for (int i = 0; i < doBases.Count; i++) doBases[i].DoPlay();
            }

            public void PlayReverse(float delay = 0)
            {
                StartCoroutine(CO_PlayReverse(delay));
            }

            private IEnumerator CO_PlayReverse(float delay)
            {
                yield return SDCache.WaitForSeconds(delay);
                for (int i = 0; i < doBases.Count; i++) doBases[i].DoPlayReverse();
            }
        }

    }
}