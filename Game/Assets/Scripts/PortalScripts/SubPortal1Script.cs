using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPortal1Script : MonoBehaviour
{
    [SerializeField] GameObject otherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.position = otherPortal.transform.position + new Vector3(2,0,0);
        }
    }
}
