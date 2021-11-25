using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHealth : MonoBehaviour
{

    public int powerupNumber;
    public float maxHealthUp;

    private PlayerController pc;
    private string healthString;
    private char[] healthChar;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("PowerupHealth"))
        {
            PlayerPrefs.SetString("PowerupHealth", "0000");
        }
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthString = PlayerPrefs.GetString("PowerupHealth");
        if (!healthString.Equals(""))
        {
            healthChar = healthString.ToCharArray();
            if (healthChar[powerupNumber - 1] == '1')
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PowerUp();
        }
    }

    void PowerUp()
    {
        healthString = PlayerPrefs.GetString("PowerupHealth");
        if (healthChar[powerupNumber - 1] == '0')
            healthChar[powerupNumber - 1] = '1';
        string finalstring = "";
        for (int i = 0; i < healthChar.Length; i++)
        {
            finalstring += healthChar[i];
        }
        PlayerPrefs.SetString("PowerupHealth", finalstring);
        pc.maxHealth += maxHealthUp;
        PlayerPrefs.SetInt("maxhp", (int)(PlayerPrefs.GetInt("maxhp") + maxHealthUp));
        pc.healthBar.SetMaxHealth(pc.maxHealth);
        pc.healthBar.SetHealth(pc.maxHealth);
        pc.currentHealth = pc.maxHealth;


        Destroy(gameObject);
    }


}