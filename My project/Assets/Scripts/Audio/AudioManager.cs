using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioClip track1;
    public AudioClip trackBoss;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeBGM(AudioClip music) {

        if (BGM.clip.name != music.name)
        {

            BGM.Stop();
            BGM.clip = music;
            BGM.Play();
        }
    }
    public void LoadTracks(){

        string nameScene = PlayerPrefs.GetString("habitacion");

        if (track1 != null && trackBoss != null)
        {
            if (nameScene != "CuevaBoss" || PlayerPrefs.GetInt("canDash") == 1)
            {
                ChangeBGM(track1);
            }
            else
            {

                ChangeBGM(trackBoss);
            }
        }
    }
    
}
