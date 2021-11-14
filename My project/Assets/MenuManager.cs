using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void BotonNuevaPartida()
    {
        //meter qué escena se debe abrir
        SceneManager.LoadScene("SceneEstibali");
    }

    public void BotonCargarPartida() 
    {
        SceneManager.LoadScene("");
    }

    public void BotonSalir()
    {
        Debug.Log("Salimos de la aplicacion");
        Application.Quit();
    }

}
