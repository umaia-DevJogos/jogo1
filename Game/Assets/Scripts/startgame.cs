using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startgame : MonoBehaviour
{
    // Start is called before the first frame update
    public int Level1;

    // Update is called once per frame
    public void StartGame() {
        SceneManager.LoadScene(Level1);
    }
}
