using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private AudioManager am;
    public SceneLoader sc;

    public void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    public void NuevaPartida()
    {
        am.LoadTracks();
        SaveSystem.ResetPlayerPrefs();
        sc.LoadScene();
        am.LoadTracks();
    }

    public void CargarPartida()
    {
        am.LoadTracks();
        if (!PlayerPrefs.HasKey("habitacion")) {
            PlayerPrefs.SetString("habitacion", "Cueva1");
        }
        sc.LoadScene();
        am.LoadTracks();
    }

    public void Salir()
    {
         Application.Quit();
    }

} 

