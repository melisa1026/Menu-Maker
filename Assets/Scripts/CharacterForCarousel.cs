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



    /*
    public CharacterCreator characterCreatorScript;

    Vector3 initialScale;

    void Awake() {
        initialScale = transform.localScale;
    }

    public void select()
    {
        foreach(GameObject i in masks) 
            i.SetActive(false);

        GetComponent<RectTransform>().SetAsLastSibling();

        pulse();

    }

    public void unselect() {
        foreach(GameObject i in masks) 
            i.SetActive(true);
    }

    void pulse() {
        float duration = 0.4f;
        transform.localScale = initialScale*0.98f;
        transform.DOScale(initialScale, duration);
    }
    */

}
