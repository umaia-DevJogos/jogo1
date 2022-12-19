using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public int Level_Boss;
    void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(Level_Boss);
            //Delete Last respawnpoint position
            PlayerPrefs.DeleteKey("Xrespawn");
            PlayerPrefs.DeleteKey("Yrespawn");
            PlayerPrefs.DeleteKey("Zrespawn");
    }
}
