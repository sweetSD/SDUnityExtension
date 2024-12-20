using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public class DoController : MonoBehaviour
    {
        [SerializeField] private List<DoBase> doBases;
        private readonly LinkedList<UniTask> playingTasks = new();
        private CancellationTokenSource tweenCts;

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

        public async UniTask Play(float delay = 0)
        {
            Stop();
            tweenCts = new();
            var token = tweenCts.Token;
            ResetToStart();
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            if (token.IsCancellationRequested) return;
            playingTasks.Clear();
            foreach (var tween in doBases)
            {
                var task = tween.DoPlayAsync();
                playingTasks.AddLast(task);
            }
            await UniTask.WhenAll(playingTasks).AttachExternalCancellation(token);
        }

        public async UniTask PlayReverse(float delay = 0)
        {
            Stop();
            tweenCts = new();
            var token = tweenCts.Token;
            ResetToEnd();
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            if (token.IsCancellationRequested) return;
            playingTasks.Clear();
            foreach (var tween in doBases)
            {
                var task = tween.DoPlayReverseAsync();
                playingTasks.AddLast(task);
            }
            await UniTask.WhenAll(playingTasks).AttachExternalCancellation(token);
        }
        
        public void Pause() => doBases.ForEach(tween => tween.DoPause());
        
        public void Resume() => doBases.ForEach(tween => tween.DoResume());
        
        public void Stop()
        {
            tweenCts?.Cancel();
            tweenCts?.Dispose();
            doBases.ForEach(tween => tween.DoStop());
        }

        public void ResetToStart() => doBases.ForEach(tween => tween.ResetToStart());
        
        public void ResetToEnd() => doBases.ForEach(tween => tween.ResetToEnd());
    }
}