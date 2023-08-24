using UnityEngine;


public class PlayerMov : MonoBehaviour {

    private Rigidbody2D player;
    private Animator animator;
    private float playerPosX;
    private float playerPosY;
    private BoxCollider2D playerBoxCol;
    private SpriteRenderer spriteRenderer;
    private float wallJumpCoolDown;


    [SerializeField] private float playerMovementVelocity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask wall;
    [SerializeField] AudioSource jumpSound;
   

    private enum State { Idle, Running, Jumping, Falling, Death }


    // works like a constructor
    private void Start() {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerBoxCol = GetComponent<BoxCollider2D>();
        playerPosX = Input.GetAxis("Horizontal");
        playerPosY = Input.GetAxis("Vertical");
    }

    
    private void Update() {
        playerPosX = GetPlayerPosX();
        Vector2 velocity = player.velocity;
        player.velocity = new Vector2(playerPosX * playerMovementVelocity, velocity.y);
        if (wallJumpCoolDown > 0.2f) {
            if (OnWall(playerBoxCol) && !IsGrounded(playerBoxCol)) {
                player.gravityScale = 2f;
                player.velocity = Vector2.zero;
            } else player.gravityScale = 3;

            if (Input.GetButtonDown("Jump")) {
                Jump(playerBoxCol, player, transform);
            }
        } else wallJumpCoolDown += Time.deltaTime;

        PlayerFacingDirection(playerPosX);
        SetPlayerState(CalculatePlayerState(playerPosX, player));   
    }


    private int CalculatePlayerState(float currentPlayerPosX, Rigidbody2D player) {
        State newState;
        bool canRun = CanRun();
        bool isWallJumping = IsWallJumping();

        if ((currentPlayerPosX > 0f || currentPlayerPosX < 0f) && canRun) {
            newState = State.Running; 
        } else {
            newState = State.Idle;
        }

        if (player.velocity.y > .1f || isWallJumping) {
            newState = State.Jumping;

        } else if(player.velocity.y < -.1f) {
            newState = State.Falling;
        }
        
        return (int) newState;
    }

    private void Jump(BoxCollider2D playerBoxCol, Rigidbody2D player, Transform playerPos) {

        bool isGrounded = IsGrounded(playerBoxCol);
        bool isWallJumping = IsWallJumping();
        if (isGrounded) {
            jumpSound.Play();
            player.velocity = new Vector2(player.velocity.x, jumpVelocity);
        } else if (isWallJumping) {
            jumpSound.Play();
            if(playerPosX == 0) player.velocity = new Vector2(-Mathf.Sign(playerPos.localScale.x) * 10, jumpVelocity);  
            else player.velocity = new Vector2(-Mathf.Sign(playerPos.localScale.x) * 3, jumpVelocity);
            wallJumpCoolDown = 0;
        }
    }

    private bool IsGrounded(BoxCollider2D playerBoxCol) {
        Bounds bounds = playerBoxCol.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, ground);
        return raycastHit.collider != null;
    }

    private bool OnWall(BoxCollider2D playerBoxCol) {
        Bounds bounds = playerBoxCol.bounds;
        RaycastHit2D raycastHit;
        switch (spriteRenderer.flipX)
        {
            case true:
                raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.left, .1f, wall);
                break;
            case false:
                raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.right, .1f, wall);
                break;
        }
    
        return raycastHit.collider != null;
    }

    private bool CanRun() {
        return IsGrounded(playerBoxCol) && !OnWall(playerBoxCol);
    }

    private bool IsWallJumping() { 
        return !IsGrounded(playerBoxCol) && OnWall(playerBoxCol); 
    }

    private void PlayerFacingDirection(float currentPlayerPosX) {
        if (currentPlayerPosX < 0f) {
            spriteRenderer.flipX = true;
        }
        else if (currentPlayerPosX > 0f) {
            spriteRenderer.flipX = false;
        }
    }
    private void SetPlayerState(int newState) {
        animator.SetInteger("state", newState);
    }
    private float GetPlayerPosX() {
        return Input.GetAxisRaw("Horizontal");
    }
    
}
