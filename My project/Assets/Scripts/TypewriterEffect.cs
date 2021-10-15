using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 40;
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            //se incrementa a lo largo del tiempo
            t += Time.deltaTime * typewriterSpeed;
            //almacena el valor redondeado de la variable t
            charIndex = Mathf.FloorToInt(t);
            //nos asegura que el valor de charIndex no es mayor que textToType
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }

        textLabel.text = textToType;
    }
}
