using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public GameObject pr;
    public Transform di;
    public Transform dd;
    public float speed;
    private Animator anim;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(shoot());
        }
    }

    public IEnumerator shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(5f);

        if (anim.GetBool("Left"))
        {
            
            GameObject pro = Instantiate(pr, di.position, transform.rotation);
            pro.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
        }
        else {
            GameObject pro = Instantiate(pr, dd.position, transform.rotation);
            pro.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);

        }
        
        
        canShoot = true;
    }
}
