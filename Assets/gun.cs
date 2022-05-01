using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public void Shoot()
    {
        GameObject g = Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
        g.GetComponent<Rigidbody2D>().AddForce(transform.right * 7, ForceMode2D.Impulse);
    }
}
