using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickaxe : MonoBehaviour
{
    public void Shoot()
    {
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play("hitPickaxe");
    }
}
