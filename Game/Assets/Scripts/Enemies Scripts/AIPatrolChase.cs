using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolChase : MonoBehaviour
{
    //Patrol
    private bool activePatrol;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 100f;
    [SerializeField] private Transform groundCheck;
    private bool turn;
    [SerializeField] private LayerMask terrainL;
    [SerializeField] private Collider2D collider;
    private bool internalPatrol;

    //Chase
    private bool activeChase;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseDistance;
    private bool internalChase;
    private float cooldown = 1.5f;

    //Die animation
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        activePatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        //Debug.Log(cooldown);
        if (activeChase && target != null)
        {
            //Chase
            chase();
        }
        else
        {
            if (target != null)
            {
                if ((Vector2.Distance(transform.position, target.position) < chaseDistance) && cooldown <= 0)
                {
                    activeChase = true;
                }
                else
                {
                    activeChase = false;
                }
            }
            else
            {
                activeChase = false;
                activePatrol = true;
            }
            if (activePatrol)
            {
                patrol();
            }
        }
    }

    private void FixedUpdate()
    {
         turn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, terrainL);
    }
    void patrol()
    {
        if (turn || collider.IsTouchingLayers(terrainL))
        {
            flip();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void chase()
    {
        if (transform.position.x > target.position.x)
        {
            if (transform.localScale.x < 0)
            {
                flip();
            }
        }
        if (transform.position.x < target.position.x)
        {
            if (transform.localScale.x > 0)
            {
                flip();
            }
        }

        if (turn || collider.IsTouchingLayers(terrainL))
        {
            activeChase = false;
            activePatrol = true;
            flip();
            cooldown = 2f;
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void flip() // change enemy scale on X axis and speed to its negative (Stop chase and patrol to prevent bugs)
    {
        internalPatrol = activePatrol;
        internalChase = activeChase;

        activePatrol = false;
        activeChase = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;

        activePatrol = internalPatrol;
        activeChase = internalChase;
    }
    public void die() // Disables collider, plays death animation and destroys enemy
    {
        collider.enabled = false;
        rb.velocity = new Vector2(0, 0); //Stop enemy
        anim.Play("enemy_die1");
        particles.Emit(20);
        Destroy(gameObject, 0.25f);
        Destroy(particles.transform.parent.gameObject, 1.5f);
    }
}
