using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    //Patrol
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 100f;
    [SerializeField] private Transform groundCheck;
    private bool turn;
    [SerializeField] private LayerMask terrainL;
    [SerializeField] private Collider2D collider;

    //Die animation
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem particles;

    private void Start()
    {
        //die();

    }

    // Update is called once per frame
    void Update()
    {
        patrol();
    }

    private void FixedUpdate()
    {
        turn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, terrainL);
    }
    void patrol() //Patrol state. Goes from wall to wall or end of platform and turns before the end or when it hits anything on the terrain layer
    {
        if (turn || collider.IsTouchingLayers(terrainL))
        {
            flip();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void flip() // change enemy scale on X axis and speed to its negative
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            die();
        }
    }

    public void die() // Disables collider, plays death animation and destroys enemy
    {
        collider.enabled = false;
        rb.velocity = new Vector2(0,0); //Stop enemy
        anim.Play("enemy_die1");
        particles.Emit(20);
        Destroy(gameObject, 0.25f);
        Destroy(particles.transform.parent.gameObject, 1.5f);
        
    }
}
