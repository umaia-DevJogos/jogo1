using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBossLevelTrigger : MonoBehaviour
{
    public int Level_Boss;
    void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(3);
            //Delete Last respawnpoint position
            PlayerPrefs.DeleteKey("Xrespawn");
            PlayerPrefs.DeleteKey("Yrespawn");
            PlayerPrefs.DeleteKey("Zrespawn");
    }
}
