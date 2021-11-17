using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject player;
    private int numEntrada;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("entrada")) {
            PlayerPrefs.SetInt("entrada", 1);
        }
        numEntrada = PlayerPrefs.GetInt("entrada");
        GameObject spawn = GameObject.Find("Spawn" + numEntrada);
        Instantiate(player, spawn.transform.position, spawn.transform.rotation);
    }
}
