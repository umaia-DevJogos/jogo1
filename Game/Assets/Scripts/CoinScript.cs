using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    // Play sound, destroy coin and call function on PlayerAdditional
    private void OnTriggerEnter2D(Collider2D collision) // Delete coin
    {
        if (collision.transform.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position); // Play sound AT coin position (needed because of Destroy() method)
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerAdditional>().collectCoin(1); // Add coin and check if hp can be added
        }
    }
}
