using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int Level_1;

    // Update is called once per frame
    public void startGame() {
        SceneManager.LoadScene(Level_1);
    }
}
