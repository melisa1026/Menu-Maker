using UnityEngine;
using DG.Tweening;

public class GrowOnEnable : MonoBehaviour
{
    public float downscale = 0.7f;
    public float duration = 0.3f;

    void OnEnable()
    {
        if(GetComponent<Transform>() == null)
            return;
            
        Vector3 scale = GetComponent<Transform>().localScale;

        GetComponent<Transform>().localScale = scale * downscale;

        GetComponent<Transform>().DOScale(scale, 0.3f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
