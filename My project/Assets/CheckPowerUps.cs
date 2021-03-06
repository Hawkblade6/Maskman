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

        if ( etiqueta == "dobleSalto")
        {
            if (PlayerPrefs.GetInt("maxJumps") == 2) {
                Destroy(this.gameObject);
            }
        }
        if (etiqueta == "DamageUp")
        {
            if(PlayerPrefs.GetInt("damage") == 40) {
                Destroy(this.gameObject);
            }
        }
    }

}
