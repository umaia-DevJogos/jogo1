using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play sound and destroy coin
    private void OnTriggerEnter2D(Collider2D collision) // Delete coin
    {
        if (collision.transform.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position); // Play sound AT coin position (needed because of Destroy() method)
            Destroy(gameObject);
        }
    }
}
