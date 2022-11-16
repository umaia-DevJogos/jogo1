using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float vJumpForce;
    //[SerializeField] private float hJumpForce; //Talvez venha a ser utilizado (legacy)
    [SerializeField] private LayerMask TerrainL;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float wJumpCooldown;
    private float horizontalInput;
    // Awake is called every time the script is loaded
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Ir buscar componente RigidBody2D aplicada ao player
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //Store horizontal input

        // Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // WallJump (with cooldown)
        if (wJumpCooldown > 0.1f)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // Move horizontally

            if(onWall() && isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            } else //prevents player from levitating
            {
                rb.gravityScale = 3;
            }

            // Call Jump
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded()) // Regular Jump
        {
            rb.velocity = new Vector2(rb.velocity.x, vJumpForce);
        } else if (!isGrounded() && onWall()) { // Wall Jump

            if (horizontalInput == 0) // Check player is moving (for wall to wall jumps)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 15, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, vJumpForce / 2);
            }
            wJumpCooldown = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, TerrainL); // Box shaped RayCast. BoxCast gets the center of the box, the size, the direction (down), the size of the ray and finally the layer mask
        return ray.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, TerrainL); // Box shaped RayCast. BoxCast gets the center of the box, the size, the direction (down), the size of the ray and finally the layer mask
        return ray.collider != null;
    }
}
