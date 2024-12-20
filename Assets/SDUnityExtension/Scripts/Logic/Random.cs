using System;
using System.Collections.Generic;
using System.Linq;
using SDUnityExtension.Scripts.Object;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SDUnityExtension.Scripts.Logic
{
    public class Random : DelayedEventHandler
    {
        [Serializable]
        public struct RandomCase
        {
            public List<DelayedEvent> events;
        }
        [SerializeField] private List<RandomCase> randomCases = new List<RandomCase>();
        private List<RandomCase> shuffledCases;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void PickRandom()
        {
            RegistEvents(randomCases[UnityEngine.Random.Range(0, randomCases.Count)].events);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void PickRandomShuffle(bool forceShuffle = false)
        {
            if (forceShuffle || shuffledCases == null || shuffledCases.Count == 0)
            {
                shuffledCases = randomCases.OrderBy(e => Guid.NewGuid()).ToList();
            }

            if (shuffledCases.Count <= 0) return;
            RegistEvents(shuffledCases[0].events);
            shuffledCases.RemoveAt(0);
        }
    }
}
