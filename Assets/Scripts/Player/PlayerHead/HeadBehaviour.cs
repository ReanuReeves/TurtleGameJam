using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour
{
    PlayerStateManager playerStateManager;

    // Start is called before the first frame update
    void Awake()
    {
        // set the player state to head

        // find the player state manager
        playerStateManager = GameObject.Find("Player").GetComponent<PlayerStateManager>();
        playerStateManager.ChangePlayerState(PlayerState.Head);

    }

    // Update is called once per frame
    void Update()
    {
        // if the mouse button is pressed the head will be set to false
        if (Input.GetMouseButtonDown(0))
        {
            playerStateManager.ChangePlayerState(PlayerState.Body);
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
        }
    }

}
