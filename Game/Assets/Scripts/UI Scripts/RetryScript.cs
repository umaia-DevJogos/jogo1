using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    private int currentScene;
    [SerializeField] private int mainMenu = 0;
    public PlayerAdditional pa;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void Retry()
    {
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadScene(currentScene);
        Debug.Log(pa.respawnpoint);
    }
    public void MainMenu()
    {
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadScene(mainMenu);
        PlayerPrefs.DeleteKey("Xrespawn");
        PlayerPrefs.DeleteKey("Yrespawn");
        PlayerPrefs.DeleteKey("Zrespawn");
    }
}
