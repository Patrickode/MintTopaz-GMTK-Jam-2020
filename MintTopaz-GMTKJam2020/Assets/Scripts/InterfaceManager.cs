using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    //static instance of this class
    public static InterfaceManager instance;

    //array of left and right shotgun bullets
    [SerializeField]
    private Image[] leftBullets;
    [SerializeField]
    private Image[] rightBullets;

    // Start is called before the first frame update
    void Start()
    {
        //set up static instance
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftShot()
    {
        //remove one shell from the UI
        foreach(Image bullet in leftBullets)
        {
            if(bullet.enabled)
            {
                //disable the next bullet and return from the loop
                bullet.enabled = false;
                return;
            }
        }
    }

    public void ReloadLeft()
    {
        //re-enable bullet images
        foreach(Image bullet in leftBullets)
        {
            bullet.enabled = true;
        }
    }

    public void RightShot()
    {
        //remove one shell from the UI
        foreach (Image bullet in rightBullets)
        {
            if (bullet.enabled)
            {
                //disable the next bullet and return from the loop
                bullet.enabled = false;
                return;
            }
        }
    }

    public void ReloadRight()
    {
        foreach (Image bullet in rightBullets)
        {
            bullet.enabled = true;
        }
    }
}
