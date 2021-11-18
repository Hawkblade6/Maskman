using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string nameScene;
    public int numEntrada;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("entrada", numEntrada);
        PlayerPrefs.SetString("habitacion", nameScene);
        LoadScene();
    }

    public void LoadScene() {

        SceneManager.LoadScene(nameScene);
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
