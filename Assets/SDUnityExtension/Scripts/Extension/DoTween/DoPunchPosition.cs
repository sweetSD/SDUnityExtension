namespace DG
{
    namespace Tweening
    {
        public class DoPunchPosition : DoBase
        {
            public override Tween GetTween()
            {
                return transform.DOPunchPosition(_destValue, _duration).SetEase(_ease);
            }

            public override Tween GetReversedTween()
            {
                return transform.DOPunchPosition(_originValue, _duration).SetEase(_ease);
            }

            public override void ResetToStart() { }

            public override void ResetToEnd() { }
        }
    }
}