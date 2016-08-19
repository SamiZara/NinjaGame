using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public CharachterReferenceManager refManager;
    private Rigidbody2D rb;
    public Transform playerToGroundRayCastPos;
    public float speed;
    public static CharacterController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

    public void MoveLeft()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(-speed, rb.velocity.y), 0.1f);
        }
    }

    public void MoveRight()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(speed, rb.velocity.y), 0.1f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(playerToGroundRayCastPos.position, -Vector3.up, 0.01f);
    }
}

