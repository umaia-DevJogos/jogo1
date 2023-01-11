using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckBossScript : MonoBehaviour
{
    [SerializeField] int Level_3 = 4;
    private GameObject boss;
    void Start()
    {
        boss = GameObject.Find("Boss");
    }
    void Update()
    {
        if(boss == null)
        {
            StartCoroutine(goToLevel3());
        }
    }
    IEnumerator goToLevel3()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(Level_3);
    }
}
