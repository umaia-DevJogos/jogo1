using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditional : MonoBehaviour
{
    public int hp = 50;
    private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject UI;
    public Vector3 respawnpoint;

    private void Awake()
    {
        respawnpoint = new Vector3(PlayerPrefs.GetFloat("Xrespawn"), PlayerPrefs.GetFloat("Yrespawn"), PlayerPrefs.GetFloat("Zrespawn"));
        if(respawnpoint != new Vector3(0,0,0))
        transform.position = respawnpoint;
    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        respawnpoint = gameObject.transform.position;
    }
    void Update()
    {
        if(gameObject.transform.position.y < 0)
        {
            takeDamage(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Check if enemy hit player
    {
        if (collision.transform.tag == "Enemy" && collision.transform.name != "Boss")
        {
            takeDamage(1);

        } else if(collision.transform.tag == "Projectile")
        {
            takeDamage(1);
        }
        else if (collision.transform.tag == "Trap")
        {
            takeDamage(10);
        }
    }

    private void takeDamage(int dmg)
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
    private void die()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); //Stop player
        gameObject.GetComponent<Animator>().Play("player_die1");
        particles.Emit(20);
        Destroy(gameObject, 0.25f);
        Destroy(particles.transform.parent.gameObject, 1.5f);
        UI.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            PlayerPrefs.SetFloat("Xrespawn", collision.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("Yrespawn", collision.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("Zrespawn", collision.gameObject.transform.position.z);
            PlayerPrefs.Save();
        }
    }
}

