namespace DG
{
    namespace Tweening
    {
        public class DoPunchRotation : DoBase
        {
            public override Tween GetTween()
            {
                return transform.DOPunchRotation(_destValue, _duration).SetEase(_ease);
            }

            public override Tween GetReversedTween()
            {
                return transform.DOPunchRotation(_originValue, _duration).SetEase(_ease);
            }

            public override void ResetToStart() { }

            public override void ResetToEnd() { }
        }
    }
}