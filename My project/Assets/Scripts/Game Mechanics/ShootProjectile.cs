using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    public ProjectileBehaviour pr;
    public Transform offset ;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
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
        Debug.Log("aquidentro");
        canShoot = false;
        yield return new WaitForSeconds(5f);
        Instantiate(pr, offset.position, transform.rotation);
        canShoot = true;
    }
}
