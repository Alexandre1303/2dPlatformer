using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMain: MonoBehaviour {

    PlayerDirection playerDirection;
    PlayerMovement playerMovement;
    PlayerState playerState;

    private void Start() {
        playerDirection = GetComponent<PlayerDirection>();
        playerMovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update() {
        playerMovement.PlayerMovementMain();
        playerDirection.PlayerDirectionMain();
        playerState.PlayerStateMain();
    }
}
