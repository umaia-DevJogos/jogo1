using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private bool isGrounded;
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

        // Call Jump
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && isGrounded)
        {
            Jump();
        }

        // Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            isGrounded = true;
        }        
    }
}
