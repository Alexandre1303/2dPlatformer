using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
   
    static readonly Vector3 FACE_RIGHT = new(1, 1, 1);
    static readonly Vector3 FACE_LEFT = new(-1, 1, 1);
    const float MIN_FLIP_THRESHOLD = 0.1f;
   
    private Direction facingDirection;
    private Rigidbody2D player;

    private void Start() {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void PlayerDirectionMain() {
        var horizontalV = player.velocity.x;
        if (horizontalV > MIN_FLIP_THRESHOLD) {
            facingDirection = Direction.RIGHT;
            transform.localScale = FACE_RIGHT;
            
        }
        else if (horizontalV < -MIN_FLIP_THRESHOLD) {
            facingDirection = Direction.LEFT;
            transform.localScale = FACE_LEFT;
        }
    }

    public Direction GetPlayerDirection() {
        return facingDirection;
    }
    
}
