using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public static class TweenAnimation
{
    public static void ToTimeEnd(Transform text, Image blind, UnityAction onComplete = null)
    {
        DOTween.Sequence()
            .Append(text.DOLocalMoveX(0, 0.8f).SetEase(Ease.OutCubic))
            .AppendInterval(0.3f)
            .Append(text.DOLocalMoveX(2160, 0.8f).SetEase(Ease.InCubic))
            .Insert(1.1f, blind.DOFade(1, 0.8f))
            .OnComplete(() => { onComplete?.Invoke(); });
    }

    public static void ToBossFight(Transform text, Image blind, UnityAction onComplete = null)
    {
        DOTween.Sequence()
            .AppendInterval(0.8f)
            .Append(text.DOLocalMoveX(0, 0.8f).SetEase(Ease.OutCubic))
            .AppendInterval(0.8f)
            .Append(text.DOLocalMoveX(2160, 0.8f).SetEase(Ease.InCubic))
            .Insert(0.8f, blind.DOFade(0, 0.8f))
            .OnComplete(() => { onComplete?.Invoke(); });
    }

    public static void ToPunchScale(this Transform target)
    {
        target
            .DOPunchScale(Vector3.one * 0.2f, 0.4f, 1, 1)
            .OnStart(() =>
            {
                target.localScale = Vector3.zero;
            });
    }

    public static void ToScaleLoop(this Transform target)
    {
        target
            .DOScale(Vector3.one * 1.2f, 0.3f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public static void ToScaleY(this Transform target, float delay = 0)
    {
        target
            .DOScaleY(1f, 0.4f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay);
    }

    public static void ToOrthoSize(this Camera camera, float endValue)
    {
        camera.DOOrthoSize(endValue, 0.4f).SetEase(Ease.OutSine);       
    }
}
