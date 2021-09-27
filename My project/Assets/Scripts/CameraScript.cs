using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject character;
    public float offset = 3f;   //Controla el desplazamiento de la camara con respecto al jugador

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = character.transform.position.x;
        pos.y = character.transform.position.y + offset;
        transform.position = pos;
    }
}
