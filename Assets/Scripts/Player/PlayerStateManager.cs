using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Body,
    Head,
    Death
}

// Hallo hallo test test

public class PlayerStateManager : MonoBehaviour
{

    // Player Body Scripts
    public PlayerMovement playerMovement;




    PlayerState playerState = PlayerState.Body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePlayerState(PlayerState newState)
    {
        Debug.Log("Changing player state to " + newState);
        playerState = newState;
        switch (playerState)
        {
            case PlayerState.Body:
                switchToBody();
                break;
            case PlayerState.Head:
                switchToHead();
                break;
            case PlayerState.Death:
                switchToDeath();
                break;
        }
    }

    void switchToBody()
    {
        playerMovement.canMove = true;
        throwHead.enabled = true;
        GetComponent<ThrowHead>().SetHeadFalse();
    }

    void switchToHead()
    {
        Debug.Log("Switching to head");
        playerMovement.canMove = false;
        throwHead.enabled = false;
    }

    void switchToDeath()
    {
        playerMovement.enabled = false;
        throwHead.enabled = false;
    }
}
