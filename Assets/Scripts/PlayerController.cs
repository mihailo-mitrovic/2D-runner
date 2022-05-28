using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    InProgress,
    GameWon,
    GameLost
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public int lives;
    [SerializeField] private LayerMask TilemapLayer;
    [SerializeField] private LayerMask EndPoint;
    [SerializeField] private float jumpPower;
    public GameState gameState;
    private bool facingRight;
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    private bool doubleJumpReady;
    private float horizontalInput;

    private void Awake()
    {
        gameState = GameState.InProgress;
        body = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, EndPoint).collider != null)
        {
            gameState = GameState.GameWon;
        }

        if (lives < 1)
        {
            gameState = GameState.GameLost;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0.1f && facingRight)
        {
            Flip();
        }
        else if (horizontalInput < -0.1f && !facingRight)
        {
            Flip();
        }
        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }
        else
        {
            animator.SetBool("IsJumping", true);
        }
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput * speed));
        body.gravityScale = 7;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                Jump();
                doubleJumpReady = true;
            }
            else if (doubleJumpReady)
            {
                Jump();
                doubleJumpReady = false;
            }
        }
                
        if (transform.position.y < -40f)
        {
            Respawn();
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, TilemapLayer);
        return raycastHit2D.collider != null;
    }


    public void Respawn()
    {
        transform.position = Vector3.zero;
        lives--;
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);

        facingRight = !facingRight;
    }
}
 