using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditional : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Check if enemy hit player
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        } else if (collision.transform.tag == "Trap")
        {
            Destroy(gameObject);
        }
    }
}

