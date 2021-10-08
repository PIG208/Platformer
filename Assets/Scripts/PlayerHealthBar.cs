using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{   
    public Slider _slider;

    public void setSliderMaxHealth(int health){
        _slider.maxValue = health;
    }

    public void setHealth(int health){
        _slider.value = health;
    }
}
