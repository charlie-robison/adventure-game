using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void setHealthUI(float newHealth)
    {
        slider.value = newHealth;
    }

    public void setMaxHealthUI(float newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
    }
}

