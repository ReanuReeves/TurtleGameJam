using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour
{
    PlayerStateManager playerStateManager;

    Camera headCamera;
    Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        // set the player state to head

        // find the player state manager
        playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>();
        playerStateManager.ChangePlayerState(PlayerState.Head);
        // set the main camera
        mainCamera = Camera.main;
        // disable the audio listener on the main camera
        mainCamera.GetComponent<AudioListener>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // if the mouse button is pressed the head will be set to false
        if (Input.GetMouseButtonDown(0))
        {
            playerStateManager.ChangePlayerState(PlayerState.Body);
            // set the camera to not view through the head camera
            headCamera = GetComponentInChildren<Camera>();
            // view through the head camera
            headCamera.enabled = false;
            // switch to the main camera
            mainCamera.enabled = true;
            // enable the audio listener on the main camera
            mainCamera.GetComponent<AudioListener>().enabled = true;
            // destroy the head
            Destroy(gameObject);
        }
    }

    // when the head collides with something it should stick to it when the object is not the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            
            // set the head to not be kinematic
            GetComponent<Rigidbody>().isKinematic = true;
            // set the head to not be a trigger
            GetComponent<Collider>().isTrigger = false;

            // set the camera to view through the headCamera which is a child of the head
            headCamera = GetComponentInChildren<Camera>();
            // view through the head camera
            headCamera.enabled = true;
            // disable the main camera
            Camera.main.enabled = false;
            
        }
    }

}
