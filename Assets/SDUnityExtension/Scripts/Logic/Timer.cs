using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Object;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace SDUnityExtension.Scripts.Logic
{
    public class Timer : DelayedEventHandler
    {
        [SerializeField] private bool useRandomTime;
    
#if ODIN_INSPECTOR
        [ShowIf("useRandomTime")]
#endif
        [SerializeField] private float minRefireTime;
#if ODIN_INSPECTOR
        [ShowIf("useRandomTime")]
#endif
        [SerializeField] private float maxRefireTime;
#if ODIN_INSPECTOR
        [HideIf("useRandomTime")]
#endif
        [SerializeField] private float refireTime;

        [SerializeField] private List<DelayedEvent> onTimer;
        private CancellationTokenSource taskCancellationTokenSource;

        private void OnEnable()
        {
            taskCancellationTokenSource?.Dispose();
            taskCancellationTokenSource = new CancellationTokenSource();
            TimerTask(taskCancellationTokenSource.Token).Forget();
        }

        private void OnDisable()
        {
            taskCancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid TimerTask(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                var timerDelay = useRandomTime ? UnityEngine.Random.Range(minRefireTime, maxRefireTime) : refireTime;
                await UniTask.Delay((int)(timerDelay * 1000), cancellationToken: token);
                RegistEvents(onTimer);
            }
        }
    }
}
