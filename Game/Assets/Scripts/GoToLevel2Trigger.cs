using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel2Trigger : MonoBehaviour
{
    public int Level_2;
    void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(2);
            //Delete Last respawnpoint position
            PlayerPrefs.DeleteKey("Xrespawn");
            PlayerPrefs.DeleteKey("Yrespawn");
            PlayerPrefs.DeleteKey("Zrespawn");
    }
}

