using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (collision.collider.tag.Equals("Player"))
        {
            Debug.Log("aau");
            player.hurt(damage);
        }
    }
}
