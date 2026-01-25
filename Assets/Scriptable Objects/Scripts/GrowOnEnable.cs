using UnityEngine;
using DG.Tweening;

public class GrowOnEnable : MonoBehaviour
{
    public float downscale = 0.7f;
    public float duration = 0.3f;

    void OnEnable()
    {
        Vector3 scale = GetComponent<Transform>().localScale;

        GetComponent<Transform>().localScale = scale * downscale;

        GetComponent<Transform>().DOScale(scale, 0.3f);
    }
}
