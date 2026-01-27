using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void nextScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void startLevel(int difficulty) {
        LevelStarter.difficulty = difficulty;
        SceneManager.LoadScene("Library");
    }
}
