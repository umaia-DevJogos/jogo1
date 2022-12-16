using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public Sprite rageProjectileSprite;
    [SerializeField] public FireScript fireScript; // Get ragemode
    private void OnEnable() // Called when projectile becomes active
    {
        Invoke("Destroy", 3f);
    }

    void Start()
    {
        speed = 5f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (fireScript.rageMode)
        {
            spriteRenderer.sprite = rageProjectileSprite;
        }
    }

    public void setDirection(Vector2 p_direction)
    {
        direction = p_direction;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(); // Resolve issue where Destroy could be invoked when projectile was inactive
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
