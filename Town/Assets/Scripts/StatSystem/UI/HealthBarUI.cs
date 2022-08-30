using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image fillBar;

    public void setHealthUI(float newHealth)
    {
        slider.value = newHealth;
    }

    public void setMaxHealthUI(float newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
    }


    private void Update()
    {
        float sliderFraction = (slider.value / slider.maxValue);

        if (sliderFraction >= 0.5f)
        {
            fillBar.color = Color.green;
        }
        else if (sliderFraction <= 0.5f && sliderFraction >= 0.25f)
        {
            fillBar.color = Color.yellow;
        }
        else if (sliderFraction <= 0.25f)
        {
            fillBar.color = Color.red;
        }
    }
}

