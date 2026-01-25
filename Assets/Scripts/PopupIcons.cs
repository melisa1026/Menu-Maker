using UnityEngine;

public class PopupIcons : MonoBehaviour
{
    public void togglePopup(GameObject popup) {
        popup.SetActive(!popup.activeSelf);
    }
}
