using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //mouse sensitivity float
    public float mouseSensitivty = 100f;

    //player transform
    [SerializeField]
    private Transform player;

    //x-axis rotation variable
    private float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //hide cursor and lock it to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivty * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivty * Time.deltaTime;

        //rotate camera around the X axis and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //rotate player's body on the Y axis only
        player.Rotate(Vector3.up * mouseX);
    }
}
