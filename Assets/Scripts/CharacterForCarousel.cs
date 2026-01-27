using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CharacterForCarousel : MonoBehaviour
{

    public GameObject[] masks;
    public Image[] parts;

    public void mask(bool visible) {
        foreach(GameObject i in masks) {
            i.SetActive(visible);
        }
    }

    public void show(bool visible) {
        foreach(Image i in parts) {
            i.enabled = visible;
        }
    }

    public void growOnSelect() {
        Vector3 initialScale = transform.localScale;
        transform.localScale = initialScale*1.008f;
        transform.DOScale(initialScale, 0.2f);
    }

}
