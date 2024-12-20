using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace SDUnityExtension.Scripts.Extension.DoTween
{
    public abstract class DoBase : MonoBehaviour
    {
        [SerializeField] protected bool playOrigin = true;
        [SerializeField] protected bool playOnEnable = true;
        [SerializeField] protected float startDelay = 0f;
        [SerializeField] protected float startReverseDelay = 0f;
        [SerializeField] protected float duration = 1f;
        [SerializeField] protected int loopCount = 0;
        [SerializeField] protected bool isIndependentUpdate = false;
        [SerializeField] protected LoopType loopType = LoopType.Restart;
        [SerializeField] protected Ease ease = Ease.OutQuad;
        [SerializeField] protected UpdateType updateType = UpdateType.Normal;
        [SerializeField] protected UnityEvent onComplete;
        [SerializeField] protected UnityEvent onReverseComplete;
        protected Sequence sequence = null;

        private void OnEnable()
        {
            if (playOnEnable)
            {
                DoPlay();
            }
        }

        public virtual async UniTask DoPlayAsync()
        {
            InitializeTween();
            if (playOrigin)
            {
                ResetToStart();
            }
            sequence.SetDelay(startDelay);
            sequence.SetLoops(loopCount, loopType);
            sequence.SetUpdate(updateType);
            sequence.OnComplete(() => onComplete?.Invoke());
            await sequence.Play().AsyncWaitForCompletion();
        }

        public void DoPlay()
        {
            DoPlayAsync().Forget();
        }

        public virtual async UniTask DoPlayReverseAsync()
        {
            InitializeReversedTween();
            if (playOrigin)
            {
                ResetToEnd();
            }
            sequence.SetDelay(startReverseDelay);
            sequence.SetLoops(loopCount, loopType);
            sequence.SetUpdate(updateType);
            sequence.OnComplete(() => onReverseComplete?.Invoke());
            await sequence.Play().AsyncWaitForCompletion();
        }

        public void DoPlayReverse()
        {
            DoPlayReverseAsync().Forget();
        }

        public virtual void DoPause()
        {
            sequence?.Pause();
        }

        public virtual void DoResume()
        {
            sequence?.Play();
        }

        public virtual void DoStop()
        {
            sequence?.Kill();
            sequence = null;
        }

        public void InitializeTween()
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(GetTween());
        }

        public void InitializeReversedTween()
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(GetReversedTween());
        }

        public abstract Tween GetTween();

        public abstract Tween GetReversedTween();

        public abstract void ResetToStart();

        public abstract void ResetToEnd();
    }
}
