using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI numberText;

    private Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
        SetNumberText(slider.value);
    }

    public void SetNumberText(float value) {
        numberText.text = value.ToString();
    }
}