using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    //Patrol
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 100f;
    [SerializeField] private LayerMask terrainL;
    [SerializeField] private Collider2D collider;

    //Die animation
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem particles;

    private Vector2 velocity;

    private void Start()
    {
        //die();
        randomDirection();
    }
    private void Update()
    {
        if(rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            randomDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        randomDirection();
    }
    private void randomDirection()
    {
        rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed;
        Debug.Log("rew");
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
