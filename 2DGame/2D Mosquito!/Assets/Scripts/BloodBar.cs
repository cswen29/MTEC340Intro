using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BloodBar : MonoBehaviour
{
    public Slider bloodSlider;

    public void SetMaxBlood(int blood)
    {
        bloodSlider.maxValue = blood;
        bloodSlider.value = blood;
    }

    public void SetBlood(int blood)
    {
        bloodSlider.value = blood;
    }
}
