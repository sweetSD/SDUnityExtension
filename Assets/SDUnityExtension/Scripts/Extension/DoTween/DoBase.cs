using UnityEngine;
using UnityEngine.Events;

namespace DG
{
    namespace Tweening
    {
        public abstract class DoBase : MonoBehaviour
        {
            [SerializeField] protected bool playOrigin = true;
            [SerializeField] protected bool playOnEnable = true;
            [SerializeField] protected float startDelay = 0f;
            [SerializeField] protected float startReverseDelay = 0f;
            [SerializeField] protected float duration = 1f;
            [SerializeField] protected int loopCount = 0;
            [SerializeField] protected LoopType loopType = LoopType.Restart;
            [SerializeField] protected Ease ease = Ease.Linear;
            [SerializeField] protected UpdateType updateType = UpdateType.Normal;
            [SerializeField] protected UnityEvent onComplete;
            protected Sequence _sequence = null;

            public Tween Tween => _sequence;
            public UnityEvent OnComplete => onComplete;

            private void OnEnable()
            {
                if (playOnEnable) DoPlay();
            }

            public virtual void DoPlay()
            {
                InitializeTween();
                if (playOrigin) ResetToStart();
                _sequence.SetDelay(startDelay);
                _sequence.SetLoops(loopCount, loopType);
                _sequence.SetUpdate(updateType);
                _sequence.OnComplete(() => onComplete?.Invoke());
                _sequence.Play();
            }

            public virtual void DoPlayReverse()
            {
                InitializeReversedTween();
                if (playOrigin) ResetToEnd();
                _sequence.SetDelay(startReverseDelay);
                _sequence.SetLoops(loopCount, loopType);
                _sequence.SetUpdate(updateType);
                _sequence.OnComplete(() => onComplete?.Invoke());
                _sequence.Play();
            }

            public virtual void DoPause()
            {
                if (_sequence != null) _sequence.Pause();
            }

            public virtual void DoResume()
            {
                if (_sequence != null) _sequence.Play();
            }

            public virtual void DoStop()
            {
                if (_sequence != null) _sequence.Kill();
                _sequence = null;
            }

            public void InitializeTween()
            {
                if (_sequence != null) _sequence.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(GetTween());
            }

            public void InitializeReversedTween()
            {
                if (_sequence != null) _sequence.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(GetReversedTween());
            }

            public abstract Tween GetTween();

            public abstract Tween GetReversedTween();

            public abstract void ResetToStart();

            public abstract void ResetToEnd();
        }
    }
}
