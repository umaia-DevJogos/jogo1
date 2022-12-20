using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float vJumpForce;
    [SerializeField] private float hJumpForce;
    [SerializeField] private float bugJumpForce;
    private float bugJumpMeter;
    [SerializeField] private float bugJumpMeterOut;
    private bool isGrounded;
    private bool onWall;
    [SerializeField] private LayerMask terrainL;
    private Rigidbody2D rb;
    private float wJumpCooldown;
    private float doubleJumpCooldown;
    [SerializeField] private float doubleJumpCooldownOut;
    private float horizontalInput;
    private bool canDoubleJump;
    //Dash
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private TrailRenderer trail;

    void Start()
    {
        bugJumpMeter = bugJumpMeterOut;
        rb = GetComponent<Rigidbody2D>(); //Ir buscar componente RigidBody2D aplicada ao player
    }

    void Update()
    {
        Debug.Log(bugJumpMeter);
        if (isDashing) // Prevents other motion if player is dashing
        {
            return;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && bugJumpMeter > 0) // Check if contradicting horizontal inputs are being given
        {
            rb.velocity = new Vector2(0, bugJumpForce); // Bug Jump
            bugJumpMeter -= Time.deltaTime;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal"); //Store horizontal input

            // Flip Sprite
            if (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            // WallJump (with cooldown)
            if (wJumpCooldown > 0.1f)
            {
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y); // Move horizontally

                if (onWall && !isGrounded)
                {
                    rb.gravityScale = 0;
                    rb.velocity = Vector2.zero;
                }
                else //prevents player from levitating
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
            doubleJumpCooldown += Time.deltaTime;

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && canDash) // Call coroutine Dash
            {
                StartCoroutine(Dash());
            }
        }
    }

    //Jump Logic
    private void Jump()
    {
        if (isGrounded) // Regular Jump
        {
            rb.velocity = new Vector2(rb.velocity.x, vJumpForce);
        }
        else if ((!isGrounded && canDoubleJump) && doubleJumpCooldown >= doubleJumpCooldownOut) // Double Jump
        {
            rb.velocity = new Vector2(rb.velocity.x, vJumpForce);
            canDoubleJump = false;
        }
        else if (!isGrounded && onWall) { // Wall Jump
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.right, 1f, terrainL); // Raycast that only hits terrain
            if (ray && transform.localScale.x > 0) // Wall to the right and facing right
            {
                climbJump();
            }
            else if(!ray && transform.localScale.x < 0) // Wall to the left and facing left
            {
                climbJump();
            }
            else // Facing away from wall
            {
                wallToWall();
            }
                wJumpCooldown = 0f;
            canDoubleJump = true;
        }

        if (isGrounded && doubleJumpCooldown >= doubleJumpCooldownOut) //reset cooldown
        {
            doubleJumpCooldown = 0f;
        }
    }
    //Auxiliary methods for jumping
    private void climbJump()
    {
        rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, vJumpForce);
    }
    private void wallToWall()
    {
        rb.velocity = new Vector2(transform.localScale.x * hJumpForce, 10);
    }

    //IsGrounded and OnWall logic
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            onWall = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            canDoubleJump = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            onWall = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall") && bugJumpMeter < bugJumpMeterOut)
        {
            bugJumpMeter += Time.deltaTime;
        }
    }

    private IEnumerator Dash() // Coroutine responsible for dashing. Stores gravity, handles dash logic with booleans, turns on and off trail and handles the dash's cooldown
    {
        canDash = false;
        isDashing = true;
        float storeGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trail.emitting = false;
        rb.gravityScale = storeGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
