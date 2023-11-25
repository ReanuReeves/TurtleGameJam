using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMovement : MonoBehaviour
{
    // create a camera variable
    Camera headCamera;
    public float lookSpeed = 2.0f;
    float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        // set the camera to the camera child to the game object
        headCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the camera based on the mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        
        // multiply the mouse movement by the look speed
        mouseX *= lookSpeed;
        
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -70f, 70f);
        headCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        // rotate the camera based on the mouse movement
        transform.Rotate(0, mouseX, 0);


    }
}
