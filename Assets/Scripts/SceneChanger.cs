using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public void nextScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void startLevel(int difficulty) {
        DOTween.KillAll(false);
        LevelStarter.difficulty = difficulty;
        SceneManager.LoadScene("Library");
    }
}
