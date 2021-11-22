using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage;

    private PlayerController player;

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
