using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//enum for the game's scenes
public enum GameLevel
{ 
    Menu,
    One,
    Two
}


public class AdvanceLevel : MonoBehaviour
{
    //game level enum for determing where the goal sends the player
    public GameLevel nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if the player is colliding with the level exit
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            //load the next level
            SceneManager.LoadScene((int)nextLevel);
        }
    }
}
