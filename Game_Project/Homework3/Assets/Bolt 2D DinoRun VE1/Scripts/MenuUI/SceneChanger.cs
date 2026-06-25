using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene changed to: " + sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited.");
    }
}
