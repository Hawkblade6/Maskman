using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAM : MonoBehaviour
{
    public GameObject am;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<AudioManager>())
        {
            return;
        }
        else {
            Instantiate(am, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
