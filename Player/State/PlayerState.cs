using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator playerAnimator;
    private State state;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
    }

    public void PlayerStateMain() {
        GroundState();
        AirState();
    }

    private void GroundState()
    {
        float currentPlayerPosX = playerMovement.GetPlayerPosX(); 
        bool canRun = playerMovement.CanRun();
        if ((currentPlayerPosX > 0f || currentPlayerPosX < 0f) && canRun) {
            state = State.Running;
        }
        else {
            state = State.Idle;
        }
        SetPlayerState((int) state);
    }
    
    private void AirState(){
        bool isWallSliding = playerMovement.IsWallSliding();
        Vector2 velocity = playerMovement.GetPlayerVelocity();
        if(isWallSliding) {
            state = State.WallSliding;
        }
        else if (velocity.y > .1f || isWallSliding) {
            state = State.Jumping;
        }
        else if (velocity.y < -.1f) {
            state = State.Falling;
        }
        SetPlayerState((int) state);
    }

    private void SetPlayerState(int newState) {
        playerAnimator.SetInteger("state", newState);
    }

    public State GetPlayerState() { return state; }
}

