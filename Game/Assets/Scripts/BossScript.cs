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

    private void Start()
    {
        //die();
        rb.velocity = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        //patrol();
    }
    void patrol() //Patrol state. Goes from wall to wall or end of platform and turns before the end or when it hits anything on the terrain layer
    {
        if (collider.IsTouchingLayers(terrainL))
        {
            ricochet();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void ricochet() // change enemy scale on X axis and speed to its negative
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
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
