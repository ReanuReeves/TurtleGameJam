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
    [SerializeField] PlayerMovement playerMovement;




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
        playerState = newState;
        switch (playerState)
        {
            case PlayerState.Body:
                playerMovement.enabled = true;
                break;
            case PlayerState.Head:
                playerMovement.enabled = false;
                break;
            case PlayerState.Death:
                playerMovement.enabled = false;
                break;
        }
    }
}
