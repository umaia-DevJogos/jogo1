using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAdditional : MonoBehaviour
{
    public int hp = 5;
    private int starthp = 5;
    private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject UI;
    public Vector3 respawnpoint;
    public int coins = 0;
    [SerializeField] private int coinsMax;
    [SerializeField] private AudioClip DyingSound;
    private float dmgCooldown = 0f;
    private int audioCount = 0;


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
        starthp = hp;
    }
    void Update()
    {
        Debug.Log(UI);
        if(gameObject.transform.position.y < 0)
        {
            takeDamage(10);
        }
        Debug.Log("Coins" + coins);
        Debug.Log("HP: " + hp);

        dmgCooldown -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) // Check if enemy hit player
    {
        if (dmgCooldown <= 0f)
        {
            if (collision.transform.tag == "Enemy" && collision.transform.name != "Boss")
            {
                takeDamage(1);

            }
            else if (collision.transform.tag == "Projectile")
            {
                takeDamage(1);
            }
            else if (collision.transform.tag == "Trap")
            {
                takeDamage(10);
            }
        }
    }
    public void takeDamage(int dmg)
    {
        dmgCooldown = 0.25f;
        hp = hp - dmg;
        sprite.color = new Color(255, 0, 0, 1);
        StartCoroutine(death());
        collectCoin(0); // check if hp can be added
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
        //Destroy(particles.transform.parent.gameObject, 1.5f);
        audioCount++;
        if (audioCount == 1)
        {
            AudioSource.PlayClipAtPoint(DyingSound, transform.position);
        }
        UI.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            PlayerPrefs.SetFloat("Xrespawn", collision.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("Yrespawn", collision.gameObject.transform.position.y);
            PlayerPrefs.SetFloat("Zrespawn", collision.gameObject.transform.position.z);
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
        }
    }

    public void collectCoin(int p_mode) // collectCoin method with two modes: onde to add a coin and another to check if hp can be added
    {
        if (p_mode == 1)
        {
            ++coins;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
            if (coins >= coinsMax && hp < starthp)
            {
                ++hp;
                coins -= coinsMax;
            }
        }
        else if(p_mode == 0)
        {
            if (coins >= coinsMax && hp < starthp)
            {
                ++hp;
                coins -= coinsMax;
            }
        }
    }
}
