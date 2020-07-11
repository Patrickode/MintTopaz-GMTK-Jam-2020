using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    //current ammo count
    private int ammoCount = 3;

    //particle system for shotgun blast
    [SerializeField]
    private ParticleSystem blast;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //allow the player to shoot if they have ammo
        if(Input.GetKeyDown(KeyCode.Mouse0) && ammoCount > 0)
        {
            Shoot();
        }

        //allow the player to reload if they are grounded
        if(PlayerController.instance.isGrounded && ammoCount < 3)
        {
            //reload the shotgun
            Reload();
        }
    }

    //shoot a blast from the shotgun
    public void Shoot()
    {
        //play particle effect animation
        blast.Play();

        //decrease ammo count
        ammoCount--;
        Debug.Log("Shot the gun, ammo left: " + ammoCount);

        //TODO: remove one shell from UI

        //knock player back
        PlayerController.instance.KnockBack();
    }

    public void Reload()
    {
        //refill ammo count
        ammoCount = 3;

        Debug.Log("Shotgun reloaded");

        //TODO: reset UI to have 3 shotgun shells
    }
}
