using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckBossScript : MonoBehaviour
{
    [SerializeField] int MainMenu = 0;
    private GameObject boss;
    void Start()
    {
        boss = GameObject.Find("Boss");
    }
    void Update()
    {
        if(boss == null)
        {
            StartCoroutine(backToMenu());
        }
    }
    IEnumerator backToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(MainMenu);
    }
}
