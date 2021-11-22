using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NuevaPartida()
    {
        //meter qué escena se debe abrir
        SaveSystem.ResetPlayerPrefs();
        SceneManager.LoadScene("Cueva1");
        
    }

    public void CargarPartida() 
    {
        if (!PlayerPrefs.HasKey("habitacion")){
                PlayerPrefs.SetString("habitacion", "Cueva1");
        }

        SceneManager.LoadScene(PlayerPrefs.GetString("habitacion"));
    }

    public void Salir()
    {
        Application.Quit();
    }

}
