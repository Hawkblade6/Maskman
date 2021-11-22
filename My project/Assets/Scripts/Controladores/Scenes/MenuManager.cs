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
        SceneManager.LoadScene("CargarPartida");
    }

    public void Salir()
    {
        Debug.Log("Salimos de la aplicacion");
        Application.Quit();
    }

}
