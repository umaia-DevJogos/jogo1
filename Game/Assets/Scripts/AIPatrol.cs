using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    private bool active;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 100f;
    [SerializeField] private Transform groundCheck;
    private bool turn;
    [SerializeField] private LayerMask terrainL;
    [SerializeField] private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            patrol();
        }
    }

    private void FixedUpdate()
    {
        if (active)
        {
            turn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, terrainL);
        }
    }
    void patrol()
    {
        if (turn || collider.IsTouchingLayers(terrainL))
        {
            flip();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void flip()
    {
        active = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        active = true;
    }
}
