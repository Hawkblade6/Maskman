using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPowerUps : MonoBehaviour
{
    private string etiqueta;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        etiqueta = this.tag;
        Debug.Log(this+ "1");

        if ( etiqueta == "dobleSalto")
        {
            Debug.Log(this);
            if (PlayerPrefs.GetInt("maxJumps") == 2) {
                Destroy(this.gameObject);
                Debug.Log(this +"2");
            }
        }
        if (etiqueta == "DamageUp")
        {
            if(PlayerPrefs.GetInt("damage") == 40) {
                Destroy(this.gameObject);
                Debug.Log(this);
            }
        }
    }

}
