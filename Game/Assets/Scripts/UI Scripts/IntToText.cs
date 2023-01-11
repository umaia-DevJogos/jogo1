using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntToText : MonoBehaviour
{
    [SerializeField] private PlayerAdditional objectPA;
    [SerializeField] private int Value = 5;
    [SerializeField] private Text ValueText;
    // Start is called before the first frame update
    void Start()
    {
        Value = objectPA.hp;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHpText();
        ValueText.text = Value.ToString();
        
    }
    public void ChangeHpText()
    {
        if (Value != objectPA.hp)
        {
            Value = objectPA.hp;
        }
    }
}
