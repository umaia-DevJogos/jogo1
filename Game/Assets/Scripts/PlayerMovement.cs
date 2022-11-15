using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    // Awake is called every time the script is loaded
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Ir buscar componente RigidBody2D aplicada ao player

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //Store horizontal input

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // Move horizontally

        //Jump
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
