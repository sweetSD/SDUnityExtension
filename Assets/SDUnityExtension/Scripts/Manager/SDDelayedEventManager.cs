using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Object;
using SDUnityExtension.Scripts.Pattern;
using UnityEngine.Events;

namespace SDUnityExtension.Scripts.Manager
{
    public class SDDelayedEventManager : SDSingleton<SDDelayedEventManager>
    {
        public void RegistEvents(List<DelayedEvent> events)
        {
            foreach (var delayedEvent in events)
            {
                DelayedEvent(delayedEvent.delay, delayedEvent.events).Forget();
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
