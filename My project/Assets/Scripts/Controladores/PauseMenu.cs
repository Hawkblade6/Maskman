using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private AudioManager am;
    public GameObject menuPausa;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPausa.SetActive(!menuPausa.activeSelf);
            if (menuPausa.activeSelf)
            {
                Time.timeScale = 0;
            }
            else {
                Time.timeScale = 1;
            }
        }
    }

    public void LoadMenu() 
    {
        am = FindObjectOfType<AudioManager>();
        //am.StopAllBGM();
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
        am.MenuPrin();
    }

    public void ResumePlay() {

        Time.timeScale = 1;
    }
}
