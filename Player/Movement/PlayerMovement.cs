using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D player;
    private float playerPosX;
    private float playerPosY;
    private BoxCollider2D playerBoxCol;
    private float wallJumpCoolDown;
    private PlayerDirection direction;
    

    [SerializeField] private float playerMovementVelocity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask wall;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] float gravityForce = 3f;
    [SerializeField] float wallJumpForce = 5f;
    [SerializeField] float wallSlideSpeed = 2f;
    [SerializeField] float wallCheckDistance = 0.1f;

   
    private void Start() {
        player = GetComponent<Rigidbody2D>();
        playerBoxCol = GetComponent<BoxCollider2D>();
        playerPosX = Input.GetAxis("Horizontal");
        playerPosY = Input.GetAxis("Vertical");
        direction = GetComponent<PlayerDirection>();
    }

    
    public void PlayerMovementMain() {
        playerPosX = GetPlayerPosX();
        Vector2 velocity = player.velocity;
        player.velocity = new Vector2(playerPosX * playerMovementVelocity, velocity.y);

        if (wallJumpCoolDown > 0.2f) {
            HandleGravityScale();
            if (Input.GetButtonDown("Jump")) {
                Jump(player, transform);
            }
        }
        else wallJumpCoolDown += Time.deltaTime;
    }

    private void HandleGravityScale() {
        if (IsWallSliding())
        {
            player.gravityScale = wallSlideSpeed;
            player.velocity = Vector2.zero;
        }
        else player.gravityScale = gravityForce;
    }


    private void Jump(Rigidbody2D player, Transform playerPos) {
        bool isGrounded = IsGrounded();
        bool isWallSliding = IsWallSliding();
        if (isGrounded) {
            jumpSound.Play();
            player.velocity = new Vector2(player.velocity.x, jumpVelocity);
        } else if (isWallSliding) {
            jumpSound.Play();
            WallJump(playerPos);
            wallJumpCoolDown = 0;
        }
    }

    private void WallJump(Transform playerPos) {
        player.velocity = new Vector2(-Mathf.Sign(playerPos.localScale.x) * wallJumpForce, jumpVelocity);
    }

    public bool IsGrounded() {
        Bounds bounds = playerBoxCol.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, wallCheckDistance, ground);
        return raycastHit.collider != null;
    }

    public bool OnWall() {
        Bounds bounds = playerBoxCol.bounds;
        
        Direction facingDirection = direction.GetPlayerDirection();

        RaycastHit2D raycastHit = 
            (facingDirection == Direction.LEFT) 
            ? Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.left, wallCheckDistance, wall)
            : Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.right, wallCheckDistance, wall);
    
        return raycastHit.collider != null;
    }

    public bool CanRun() {
        return IsGrounded() && !OnWall();
    }

    public bool IsWallSliding() { 
        return !IsGrounded() && OnWall(); 
    }
    public Vector2 GetPlayerVelocity() {
        return player.velocity;
    }
    public float GetPlayerPosX() {
        return Input.GetAxisRaw("Horizontal");
    }
    
}
