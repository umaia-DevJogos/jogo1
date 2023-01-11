using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    public int health = 5;
    public int maxhealth = 5;
    

    [SerializeField] private Sprite player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
       player.GetComponent<PlayerAdditional>().takeDamage(health);

        var sn = gameObject.GetComponent<PlayerAdditional>();
        //sn.takeDamage(health);

        for (int i = 0; i < health + 30; i++)
        {
            sn.takeDamage(health);
        }
        
    }
}
