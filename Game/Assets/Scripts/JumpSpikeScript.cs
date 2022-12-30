using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpikeScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Debug.Log("321");
            anim.Play("SpikeJump");
        }
    }
}
