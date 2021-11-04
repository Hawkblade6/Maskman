using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 40;

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.6f },
        {new HashSet<char>(){',', ';', ':'}, 0.3f },
    };

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
            int lastCharIndex = charIndex;

            //se incrementa a lo largo del tiempo
            t += Time.deltaTime * typewriterSpeed;

            //almacena el valor redondeado de la variable t
            charIndex = Mathf.FloorToInt(t);
            //nos asegura que el valor de charIndex no es mayor que textToType
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i< charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;
                textLabel.text = textToType.Substring(0, i + 1);
                if (IsPunctuation(textToType[i], out float waitTime)&& !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            
            yield return null;
        }

        textLabel.text = textToType;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if (punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }
        waitTime = default;
        return false;
    }
}
