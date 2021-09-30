using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI texto;

    private bool staminaControl = false;

    public void SetMaxStamina(int stamina) //Establece el aguante maxima
    {
        slider.maxValue = stamina;
        slider.value = stamina;
        texto.text = slider.value + " / " + slider.maxValue;
    }

    public void SetStamina(int stamina) //Establece el aguante actual
    {

        if (stamina < slider.value)
        {
            slider.value = stamina;
            texto.text = slider.value + " / " + slider.maxValue;
            //StartCoroutine(FadeawayHealthBar(stamina));
        }

        if (stamina > slider.value)
        {
            slider.value = stamina;
            texto.text = slider.value + " / " + slider.maxValue;
        }

    }

    public float GetStamina()
    {
        return slider.value;
    }

    IEnumerator RegainStamina()
    {
        yield return new WaitForSeconds(1f);

        while (staminaControl && GetStamina() > 4)
        {


        }

    }
}
