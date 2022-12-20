using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Collider2D swordCollider;
    [SerializeField] private LayerMask enemyL;
    private bool attackCheck;
    private float cooldown = 1f;
    void Update()
    {
        cooldown -= Time.deltaTime;
        //Sword attack
        if ((cooldown <= 0) && (attackCheck = true))
        {
            attackCheck = false;
        }
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && cooldown <= 0)
        {
            cooldown = 1f;
            anim.Play("attack_sword");
            attackCheck = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // Check sword hit enemy
    {
        if (collision.transform.tag == "Enemy" && attackCheck)
        {
            if (collision.transform.name == "NPC t1") // Selector based on enemy type
            {
                collision.gameObject.GetComponent<AIPatrol>().die();
            }
            else if (collision.transform.name == "NPC t2")
            {
                collision.gameObject.GetComponent<AIPatrolChase>().die();
            }
            else if (collision.transform.name == "Boss")
            {
                collision.gameObject.GetComponent<BossScript>().takeDamage(1);
            }
            //Destroy(collision.transform.gameObject);
        }
    }
}
