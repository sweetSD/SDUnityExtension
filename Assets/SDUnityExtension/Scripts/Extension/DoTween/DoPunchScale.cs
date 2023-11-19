namespace DG
{
    namespace Tweening
    {
        public class DoPunchScale : DoBase
        {
            public override Tween GetTween()
            {
                return transform.DOPunchScale(_destValue, _duration).SetEase(_ease);
            }

            public override Tween GetReversedTween()
            {
                return transform.DOPunchScale(_originValue, _duration).SetEase(_ease);
            }

            public override void ResetToStart() { }

            public override void ResetToEnd() { }
        }
    }
}