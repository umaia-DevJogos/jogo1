using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    private float finalDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        finalDistance = Mathf.Lerp(finalDistance, (distance * player.localScale.x), Time.deltaTime * speed);
        transform.position = new Vector3(player.position.x + finalDistance, transform.position.y, transform.position.z); // Follow the player on the X axis
        
        //Debug.Log(finalDistance);
    }
}
