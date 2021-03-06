﻿using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public CharachterReferenceManager refManager;
    public Transform playerToGroundRayCastPos,playerToLeftRayCastPos,playerToRightRayCastPos;
    public float horizontalSpeed,verticalSpeed,wallSlideSpeed;
    public static CharacterController player;
    private bool doubleJumpUsed = false;

    void Update()
    {

    }

    public void Jump()
    {
        
        if (IsGrounded())
        {
            refManager.rb.velocity = new Vector2(refManager.rb.velocity.x, verticalSpeed);
            doubleJumpUsed = false;
            return;
        }
        else if (IsThereWallAtLeft())
        {
            refManager.rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            doubleJumpUsed = false;
            return;
        }
        else if (IsThereWallAtRight())
        {
            refManager.rb.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
            doubleJumpUsed = false;
            return;
        }
        else if (!doubleJumpUsed)
        {
            doubleJumpUsed = true;
            refManager.rb.velocity = new Vector2(refManager.rb.velocity.x, verticalSpeed);
            return;
        }
    }

    public void MoveLeft()
    {
        if (IsGrounded())
        {
            refManager.rb.velocity = new Vector2(-horizontalSpeed, refManager.rb.velocity.y);
        }
        else if (IsThereWallAtLeft())
        {
            refManager.rb.velocity = new Vector2(0, -wallSlideSpeed);
        }
        else
        {
            refManager.rb.velocity = Vector2.Lerp(refManager.rb.velocity, new Vector2(-horizontalSpeed, refManager.rb.velocity.y), 0.1f);
        }
    }

    public void StopHorizontalVelocity()
    {
        if (IsGrounded())
        {
            refManager.rb.velocity = new Vector2(0, refManager.rb.velocity.y);
        }
        else
        {
            refManager.rb.velocity = Vector2.Lerp(refManager.rb.velocity, new Vector2(0, refManager.rb.velocity.y), 0.3f);

        }
    }

    public void MoveRight()
    {
        if (IsGrounded())
        {
            refManager.rb.velocity = new Vector2(horizontalSpeed, refManager.rb.velocity.y);
        }
        else if (IsThereWallAtRight())
        {
            refManager.rb.velocity = new Vector2(0, -wallSlideSpeed);
        }
        else
        {
            refManager.rb.velocity = Vector2.Lerp(refManager.rb.velocity, new Vector2(horizontalSpeed, refManager.rb.velocity.y), 0.1f);
        }
    }

    private bool IsGrounded()
    {
        int layerMask = LayerMask.GetMask("Obstacle");
        return Physics2D.Raycast(playerToGroundRayCastPos.position, Vector3.down, 0.015f,layerMask);
    }

    private bool IsThereWallAtLeft()
    {
        int layerMask = LayerMask.GetMask("Obstacle");
        return Physics2D.Raycast(playerToLeftRayCastPos.position,Vector3.left,0.02f, layerMask);
    }

    private bool IsThereWallAtRight()
    {
        int layerMask = LayerMask.GetMask("Obstacle");
        return Physics2D.Raycast(playerToRightRayCastPos.position, Vector3.right, 0.02f, layerMask);
    }
}

