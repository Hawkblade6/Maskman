using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public string nameScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadScene();
    }

    public void LoadScene() {

        SceneManager.LoadSceneAsync(nameScene);
    }
}
