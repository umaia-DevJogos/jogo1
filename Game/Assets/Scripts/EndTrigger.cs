using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public int Level_Boss;
    void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(Level_Boss);
        }
}
