using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 4;
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed; 
    }

    void OnCollisionEnter2D(Collision2D col) {

        Destroy(gameObject);
    }
}
