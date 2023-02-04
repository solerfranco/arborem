using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpdateSliderText : MonoBehaviour
{
    public TextMeshProUGUI sliderText;
    public Slider slider;

    public void ChangeSliderText(float value)
    {
        sliderText.text = ((slider.maxValue * 100) * slider.value).ToString("0") + "%";
    }
}
