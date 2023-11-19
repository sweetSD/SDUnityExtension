using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Manager;
using UnityEngine;
using UnityEngine.Events;

namespace SDUnityExtension.Scripts.Object
{
    [Serializable]
    public struct DelayedEvent
    {
        public float delay;
        public UnityEvent events;

        public DelayedEvent(float d = 0)
        {
            delay = d;
            events = new UnityEvent();
        }
    }

    [Serializable]
    public struct DelayedEvents
    {
        public List<DelayedEvent> events;
    }

    /// <summary>
    /// Inspector에서 수정이 가능한 UnityEvent 시스템입니다.
    /// 기존의 UnityEvent와는 다르게 delay를 줄 수 있는 특징이 있습니다.
    /// </summary>
    public class DelayedEventHandler : MonoBehaviour
    {
        /// <summary>
        /// 이벤트를 발생시킵니다.
        /// </summary>
        /// <param name="events">이벤트 리스트</param>
        protected void RegistEvents(List<DelayedEvent> events)
        {
            if (events == null)
                return;
            if (SDDelayedEventManager.I != null)
                SDDelayedEventManager.I.RegistEvents(events);
            else
            {
                foreach (var delayedEvent in events)
                {
                    DelayedEvent(delayedEvent.delay, delayedEvent.events).Forget();
                }
            }
        }
    
        async UniTaskVoid DelayedEvent(float delay, UnityEvent uEvent)
        {
            if (delay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: this.GetCancellationTokenOnDestroy());
            uEvent?.Invoke();
        }
    }
}