using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntToText : MonoBehaviour
{

    [SerializeField] private int Value = 5;
    [SerializeField] private Text ValueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ValueText.text = Value.ToString();
        
    }
}
