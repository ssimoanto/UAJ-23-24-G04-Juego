using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Código implementado por:
//SIMONA ANTONOVA

public class LifeBar : MonoBehaviour
{
    public Slider slider;

    // actualizar la barra de vida

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
