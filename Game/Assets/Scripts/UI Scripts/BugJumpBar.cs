using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugJumpBar : MonoBehaviour
{

    [SerializeField] private Image bugjumpbar;
    [SerializeField] private PlayerMovement objectPM;
    // Start is called before the first frame update
    void Start()
    {
        bugjumpbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bugjumpbar.fillAmount = objectPM.bugJumpMeter;
    }
}
