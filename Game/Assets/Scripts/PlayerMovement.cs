using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float vJumpForce;
    [SerializeField] private float hJumpForce; //Em testes
    [SerializeField] private LayerMask TerrainL;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float wJumpCooldown;
    private float vJumpCooldown;
    [SerializeField] private float vJumpCooldownOut;
    private float horizontalInput;
    private bool jumped;

    // Awake is called every time the script is loaded (EM TESTES!)
    /*
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Ir buscar componente RigidBody2D aplicada ao player
        boxCollider = GetComponent<BoxCollider2D>();
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Ir buscar componente RigidBody2D aplicada ao player
        boxCollider = GetComponent<BoxCollider2D>();
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
            transform.localScale = new Vector3(-1, 1, 1);
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        // WallJump (with cooldown)
        if (wJumpCooldown > 0.1f)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // Move horizontally

            if(onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            } else //prevents player from levitating
            {
                rb.gravityScale = 3;
            }

            // Call Jump
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            wJumpCooldown += Time.deltaTime;
        }
        vJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        //Jump Logic
        if ((isGrounded() || (!isGrounded() && jumped)) && vJumpCooldown >= vJumpCooldownOut) // Regular Jump and Double Jump
        {
            rb.velocity = new Vector2(rb.velocity.x, vJumpForce);
            jumped = !jumped;
        }
        else if (!isGrounded() && onWall()) { // Wall Jump
            if (horizontalInput == 0) // Check player is moving (for wall to wall jumps)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * hJumpForce, 0); // Force flip
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, vJumpForce);

            }
            wJumpCooldown = 0f;
            jumped = !jumped;
        }

        if (isGrounded() && vJumpCooldown >= vJumpCooldownOut) //reset cooldown
        {
            vJumpCooldown = 0f; 
        }
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
