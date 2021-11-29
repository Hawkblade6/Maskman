using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string nameScene;
    public int numEntrada;

    private AudioManager am;
    public GameObject player;

    public void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("entrada", numEntrada);
        PlayerPrefs.SetString("habitacion", nameScene);
        LoadScene();
    }

    public void LoadScene() {
        if (nameScene == "") { 
            nameScene = PlayerPrefs.GetString("habitacion");
        }
        SceneManager.LoadScene(nameScene);

        am.LoadTracks();
    }

    public void DeathScene() {


        PlayerPrefs.SetInt("currenthp", PlayerPrefs.GetInt("maxhp"));
        SceneManager.LoadScene("DeadScene");
    }
}
