using UnityEngine;

public class SpeechBubble : MonoBehaviour
{

    void Update() {
        if(Input.GetMouseButtonDown(0))
            gameObject.SetActive(false);
    }

    public void showSpeech() {
        gameObject.SetActive(true);
    }
}
