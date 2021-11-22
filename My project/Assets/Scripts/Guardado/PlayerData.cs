using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public string habitacion;
    public int entrada;
    public int maxJumps;
    public int canDash;
    public int maxhp;
    public int currenthp;
    public int damage;

    public PlayerData(PlayerController player) 
    {

        habitacion = PlayerPrefs.GetString("habitacion");
        entrada = PlayerPrefs.GetInt("entrada");
        maxJumps = PlayerPrefs.GetInt("maxJumps");
        canDash = PlayerPrefs.GetInt("canDash");
        maxhp = PlayerPrefs.GetInt("maxhp");
        currenthp = PlayerPrefs.GetInt("currenthp");
        damage = PlayerPrefs.GetInt("damage");
    }
}
