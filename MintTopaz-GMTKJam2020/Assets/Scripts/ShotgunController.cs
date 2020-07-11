using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    //current ammo count
    private int ammoCount = 0;

    //cooldown between shots
    [SerializeField]
    private float shotCooldown = 0.3f;
    private bool canShoot = true;

    //particle system for shotgun blast
    [SerializeField]
    private ParticleSystem blast = null;

    //shotgun determination variables
    public bool isRightShotgun = true;
    private KeyCode shotgunKey;

    private void Start()
    {
        //check if this is the left or right shotgun and determine which button is used for shooting
        if(isRightShotgun)
        {
            shotgunKey = KeyCode.Mouse1;
        }
        else
        {
            shotgunKey = KeyCode.Mouse0;
        }
    }

    void Update()
    {
        //allow the player to shoot if they have ammo and the cooldown has ended
        if (Input.GetKeyDown(shotgunKey) && ammoCount > 0 && canShoot)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }

        //allow the player to reload if they are grounded
        if (PlayerController.instance.isGrounded && ammoCount < 3)
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

        //remove one shell from UI depending on gun placement
        if(isRightShotgun)
        {
            InterfaceManager.instance.RightShot();
        }
        else
        {
            InterfaceManager.instance.LeftShot();
        }

        //knock player back
        PlayerController.instance.KnockBack();
    }

    public void Reload()
    {
        //refill ammo count
        ammoCount = 3;

        Debug.Log("Shotgun reloaded");

        //reset UI to have 3 shotgun shells
        if(isRightShotgun)
        {
            InterfaceManager.instance.ReloadRight();
        }
        else
        {
            InterfaceManager.instance.ReloadLeft();
        }
    }

    public IEnumerator Cooldown()
    {
        Debug.Log("Starting cooldown");
        canShoot = false;
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
        Debug.Log("You can shoot now");
    }
}
