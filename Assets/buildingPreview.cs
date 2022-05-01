using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingPreview : MonoBehaviour
{
    public bool touching;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "building") touching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "building") touching = false;
    }
}
