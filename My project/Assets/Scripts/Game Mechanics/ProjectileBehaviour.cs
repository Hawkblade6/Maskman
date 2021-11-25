using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        
        Destroy(gameObject);

        if (col.collider.tag == "Player")
        {
            Debug.Log(col.collider.tag);
            col.collider.GetComponent<PlayerController>().hurt(20f);
        }
    }
}
