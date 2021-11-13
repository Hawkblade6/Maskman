using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Slider fadeawaySlider;
    public TextMeshProUGUI texto;

    private bool damageAnimation = false;

    public void SetMaxHealth(float health) //Establece la vida maxima
    {
        slider.maxValue = health;
        //slider.value = health;
        fadeawaySlider.maxValue = health;
        fadeawaySlider.value = health;
        texto.text = slider.value + " / " + slider.maxValue;
    }

    public void SetHealth(float health) //Establece la vida actual
    {
        if (health < slider.value)
        {
            slider.value = health;
            texto.text = slider.value + " / " + slider.maxValue;
            damageAnimation = true;
            StartCoroutine(FadeawayHealthBar(health));
        }

        if (health > slider.value) 
        {
            if (damageAnimation) {
                StopCoroutine("FadeawayHealthBar");
                damageAnimation = false;
            }

            slider.value = health;
            texto.text = slider.value + " / " + slider.maxValue;
            fadeawaySlider.value = health;
        }

    }

    public float GetHealth() 
    {
        return slider.value;
    } 

    IEnumerator FadeawayHealthBar(float health) //Se asegura de que la barra de vida de fondo baje poco a poco
    {
        yield return new WaitForSeconds(0.4f);
        while (fadeawaySlider.value > slider.value)
        {
            fadeawaySlider.value -= 1;
            yield return new WaitForSeconds(0.02f);     //TODO Habria que hacer un timer universal para cuadrarlo con el tiempo de invulnerabilidad
            if (fadeawaySlider.value < slider.value) {
                fadeawaySlider.value = slider.value;
            }
        }
        //float valorActual = fadeawaySlider.value;
        //fadeawaySlider.value = Mathf.Lerp(fadeawaySlider.value, slider.value, Time.deltaTime/4);
        fadeawaySlider.value = health;
        damageAnimation = false;
    }
}
