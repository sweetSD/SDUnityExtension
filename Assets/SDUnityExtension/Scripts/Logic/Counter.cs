using System.Collections.Generic;
using SDUnityExtension.Scripts.Object;
using UnityEngine;

namespace SDUnityExtension.Scripts.Logic
{
    public class Counter : DelayedEventHandler
    {
        [SerializeField] private int initialValue = 0;
        [SerializeField] private int minValue = 0;
        [SerializeField] private int maxValue = 1;
        [SerializeField] private List<DelayedEvent> onHitMaxValue;
        [SerializeField] private List<DelayedEvent> onHitMinValue;

        private int value;
        public int Value => value;
        public int MaxValue => maxValue;
        public int MinValue => minValue;

        private void Start()
        {
            value = initialValue;
        }

        public void Add(int val)
        {
            int prev = value;
            value += val;
            ValidateEvents(prev, value);
        }

        public void Subtract(int val)
        {
            int prev = value;
            value -= val;
            ValidateEvents(prev, value);
        }

        private void ValidateEvents(int prev, int next)
        {
            if (prev != MaxValue && next >= MaxValue)
            {
                RegistEvents(onHitMaxValue);
            }
        
            if (prev != MinValue && next <= MinValue)
            {
                RegistEvents(onHitMinValue);
            }
        }
    }
}
