using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioManager am;

    public void NuevaPartida()
    {
        //meter qué escena se debe abrir
        SaveSystem.ResetPlayerPrefs();
        am.LoadTracks();
        SceneManager.LoadScene("Cueva1");

       
    }

    public void CargarPartida()
    {
        if (!PlayerPrefs.HasKey("habitacion")) {
            PlayerPrefs.SetString("habitacion", "Cueva1");
        }
        am.LoadTracks();
        SceneManager.LoadScene(PlayerPrefs.GetString("habitacion"));
    }

    public void Salir()
    {
         Application.Quit();
    }

} 

