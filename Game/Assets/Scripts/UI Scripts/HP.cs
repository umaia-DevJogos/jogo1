using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    
    [SerializeField] private PlayerAdditional objectPA;
    [SerializeField] private int hp;
    [SerializeField] private  int textHp;


    [SerializeField] private Sprite player;

    // Start is called before the first frame update
    void Start()
    {
        textHp = objectPA.hp;
        hp = objectPA.hp;
    }
    

    // Update is called once per frame
    void Update()
    {
        ChangeHpText();
    }

    public void ChangeHpText()
    {
        if (textHp != hp)
        {
            textHp = hp;
        }
    }
}
