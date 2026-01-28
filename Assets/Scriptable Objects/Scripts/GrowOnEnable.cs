using UnityEngine;
using DG.Tweening;

public class GrowOnEnable : MonoBehaviour
{
    public float downscale = 0.7f;
    public float duration = 0.3f;

    public Vector3 scale;

    void Awake() {
        scale = GetComponent<Transform>().localScale;
    }

    void OnEnable()
    {
        if(GetComponent<Transform>() == null)
            return;    

        GetComponent<Transform>().localScale = scale * downscale;

        GetComponent<Transform>().DOScale(scale, 0.3f).SetLink(gameObject, LinkBehaviour.KillOnDestroy);
    }

    void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
