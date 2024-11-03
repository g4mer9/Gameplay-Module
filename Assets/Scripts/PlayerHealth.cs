using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider selfSlider;
    public Image fillImage;
    public float currentHealth = 100;

    public void OnSliderValueChanged()
    {
        fillImage.enabled = selfSlider.value > 0;
    }
}
