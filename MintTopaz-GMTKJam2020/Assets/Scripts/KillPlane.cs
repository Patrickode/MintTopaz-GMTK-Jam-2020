using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    //transform to spawn the player back at
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the player is colliding with the kill plane
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            //move the player to the spawnpoint
            collision.gameObject.transform.position = spawnPoint.position;
        }
    }
}
