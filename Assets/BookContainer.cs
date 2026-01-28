using UnityEngine;

public class BookContainer : MonoBehaviour
{
    public book bookScript;

    public void closeAllBooks() {

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
