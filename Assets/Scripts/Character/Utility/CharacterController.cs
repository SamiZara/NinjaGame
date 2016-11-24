using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public CharachterReferenceManager refManager;
    private Rigidbody2D rb;
    public Transform playerToGroundRayCastPos,playerToLeftRayCastPos,playerToRightRayCastPos;
    public float horizontalSpeed,verticalSpeed,wallSlideSpeed;
    public static CharacterController player;
    private bool doubleJumpUsed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("Is there wall at right:"+ IsThereWallAtRight());
        Debug.Log("Is there wall at left:" + IsThereWallAtLeft());
    }

    public void Jump()
    {
        
        if (IsGrounded())
        {
            Debug.Log("Single Jump");
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            doubleJumpUsed = false;
            return;
        }
        else if (IsThereWallAtLeft())
        {
            rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            doubleJumpUsed = false;
            return;
        }
        else if (IsThereWallAtRight())
        {
            rb.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
            doubleJumpUsed = false;
            return;
        }
        else if (!doubleJumpUsed)
        {
            doubleJumpUsed = true;
            rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
            Debug.Log("Double jump");
            return;
        }
    }

    public void MoveLeft()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(-horizontalSpeed, rb.velocity.y);
        }
        else if (IsThereWallAtLeft())
        {
            rb.velocity = new Vector2(0, -wallSlideSpeed);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(-horizontalSpeed, rb.velocity.y), 0.1f);
        }
    }

    public void StopHorizontalVelocity()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, rb.velocity.y), 0.3f);

        }
    }

    public void MoveRight()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
        }
        else if (IsThereWallAtRight())
        {
            rb.velocity = new Vector2(0, -wallSlideSpeed);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(horizontalSpeed, rb.velocity.y), 0.1f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(playerToGroundRayCastPos.position, Vector3.down, 0.015f);
    }

    private bool IsThereWallAtLeft()
    {
        return Physics2D.Raycast(playerToLeftRayCastPos.position,Vector3.left,0.01f);
    }

    private bool IsThereWallAtRight()
    {
        return Physics2D.Raycast(playerToRightRayCastPos.position, Vector3.right, 0.01f);
    }
}

