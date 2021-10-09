using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        GetComponent<TypewriterEffect>().Run("Hello!\nMy name is Maskman.", textLabel);
    }
}
