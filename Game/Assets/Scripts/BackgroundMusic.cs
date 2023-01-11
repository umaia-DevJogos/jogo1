using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    
   private static BackgroundMusic backgroundMusic;
    
    /*void Awake()
    {
        if(backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }

        else
        {
            Destroy(gameObject);
        }


        void Update()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name != "MainMenu")
            {
                Destroy(backgroundMusic);
            }
        }

    }*/

}
