using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenuTrigger : MonoBehaviour
{
    public int Main_Menu;
    void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(0);
            //Delete Last respawnpoint position
            PlayerPrefs.DeleteKey("Xrespawn");
            PlayerPrefs.DeleteKey("Yrespawn");
            PlayerPrefs.DeleteKey("Zrespawn");
    }
}
