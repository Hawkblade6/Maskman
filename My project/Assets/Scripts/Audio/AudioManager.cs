using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioClip trackMenu;
    public AudioClip track1;
    public AudioClip trackBoss;

    public void Start()
    { 
            DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        ChangeBGM(trackMenu);
        Debug.Log(BGM.clip.name + "1");

    }
    public void ChangeBGM(AudioClip music) {
        Debug.Log(BGM.clip.name + " este clip");
        Debug.Log(music.name + " esta musica");
        Debug.Log(BGM.clip.name != music.name);
        if (BGM.clip.name != music.name)
        {
            BGM.Stop();
            BGM.clip = music;
            BGM.Play();
        }
    }
    public void LoadTracks(){

        string nameScene = PlayerPrefs.GetString("habitacion");
        Debug.Log(nameScene + "2");
        if (track1 != null && trackBoss != null && trackMenu != null)
        {
            if (nameScene != "CuevaBoss" || PlayerPrefs.GetInt("canDash") == 1)
            {
                Debug.Log(BGM.clip.name + "3");
                ChangeBGM(track1);
            }
            else
            {
                ChangeBGM(trackBoss);
            }
        }
    }

    public void StopAllBGM() {

        BGM.Stop();
    }

    public void MenuPrin() {
  
            ChangeBGM(trackMenu);
    }
    
}
