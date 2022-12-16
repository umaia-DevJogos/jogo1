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

    public int hp = 20;
    public int starthp = 20;
    private SpriteRenderer sprite;
    private void Start()
    {
        starthp = hp;
        sprite = GetComponent<SpriteRenderer>();
        randomDirection();
    }
    private void Update()
    {
        if(rb.velocity.x == 0 || rb.velocity.y == 0)
        {
            randomDirection();
        }
    }
    private void randomDirection()
    {
        float dirX = 0f;
        float dirY = 0f;
        if (Random.Range(-1f, 1f) >= 0)
        {
            dirX = Random.Range(0f, 1f);
        }
        else
        {
            dirX = Random.Range(-1f, 0f);
        }

        if (Random.Range(-1f, 1f) >= 0)
        {
            dirY = Random.Range(0f, 1f);
        }
        else
        {
            dirY = Random.Range(-1f, 0f);
        }

        rb.velocity = new Vector2(dirX, dirY) * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        randomDirection();        
    }

    public void takeDamage(int dmg)
    {
        hp = hp - dmg;
        sprite.color = new Color(255, 0, 0, 1);
        StartCoroutine(death());
    }
    IEnumerator death()
    {
        if (hp <= 0)
        {
            die();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            sprite.color = new Color(255, 255, 255, 1);
        }
    }

    public void die() // Disables collider, plays death animation and destroys enemy
    {
        collider.enabled = false;
        rb.velocity = new Vector2(0, 0); //Stop Boss
        anim.Play("boss_die1");
        particles.Emit(80);
        Destroy(gameObject, 0.25f);
        Destroy(particles.transform.parent.gameObject, 2f);
    }
}
