using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntToText : MonoBehaviour
{
    [SerializeField] private PlayerAdditional objectPA;
    [SerializeField] private PlayerMovement objectPM;
    [SerializeField] private int ValueHP = 5;
    [SerializeField] private Text ValueTextCoins;
    [SerializeField] private Text ValueTextHP;
    [SerializeField] private Text BugJumpCounter;
    [SerializeField] private Image bugjumpbar;
    // Start is called before the first frame update
    void Start()
    {
        ValueHP = objectPA.hp;
        bugjumpbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHpText();
        ValueTextHP.text = ValueHP.ToString();
        ValueTextCoins.text = objectPA.coins.ToString();
        BugJumpCounter.text = (Mathf.Round(objectPM.bugJumpMeter * 10f) / 10f).ToString();
        bugjumpbar.fillAmount = objectPM.bugJumpMeter;

    }
    public void ChangeHpText()
    {
        if (ValueHP != objectPA.hp)
        {
            if (objectPA.hp < 0)
            {
                ValueHP = 0;
            }
            else
            {
                ValueHP = objectPA.hp;
            }
        }
    }
    public void bugJumpMeter()
    {
        bugjumpbar.fillAmount = 5;
        bugjumpbar.fillAmount = objectPM.bugJumpMeter;
    }
}
