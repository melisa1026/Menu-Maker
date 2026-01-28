using UnityEngine;
using DG.Tweening;

public class Pulse : MonoBehaviour
{
    public float scaleAmount = 0.95f;
    public float duration = 1;

    void Start()
    {
        transform.DOScale(transform.localScale * scaleAmount, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }
}
