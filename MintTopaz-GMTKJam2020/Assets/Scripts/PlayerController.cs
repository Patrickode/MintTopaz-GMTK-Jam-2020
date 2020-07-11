using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //static instance of this object
    public static PlayerController instance = null;

    //character controller component
    private CharacterController controller = null;

    //camera
    [SerializeField]
    private Transform cam = null;

    //movement variables
    public float moveSpeed = 25f;
    public float jumpHeight = 3f;
    public float knockbackForce = 10f;

    //float for applying gravity
    public float gravity = -9.81f;

    //movement vectors
    private Vector3 velocity;
    private Vector3 acceleration = Vector3.zero;

    //reference to ground-checking object
    [SerializeField]
    private Transform groundCheck = null;
    public float checkRadius;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool prevGroundedState;

    void Start()
    {
        //set up static instance
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //check to see if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);
        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }


        //get movement axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //get local movement vector
        Vector3 move = transform.right * x + transform.forward * z;

        //check if SHIFT key is being pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //increase movespeed
            moveSpeed = 40f;
        }
        else
        {
            //reset movespeed
            moveSpeed = 25f;
        }

        //move player in the X and Z axes
        controller.Move(move * moveSpeed * Time.deltaTime);

        //check for jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //add gravity and acceleration to velocity
        velocity.y += gravity * Time.deltaTime;
        //velocity += acceleration * Time.deltaTime;

        //move player in the Y axis
        controller.Move(velocity * Time.deltaTime);

        //set previous grounded state
        prevGroundedState = isGrounded;

        //decelerate when grounded
        if (isGrounded)
        {
            velocity.x *= 0.98f;
            velocity.z *= 0.98f;
        }
    }

    public void KnockBack()
    {
        //zero out velocity in x and z directions
        velocity.x = 0f;
        velocity.z = 0f;

        //apply new force
        velocity += -cam.transform.forward * knockbackForce;
    }
}
