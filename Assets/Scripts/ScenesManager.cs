using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(Object scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
