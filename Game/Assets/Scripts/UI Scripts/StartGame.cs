using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public int Level1;
    public void StartGame() {
        SceneManager.LoadScene(Level1);
    }
}
