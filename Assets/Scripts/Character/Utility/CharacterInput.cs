using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{

    public CharachterReferenceManager refManager;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (refManager.character.isPlayer)
        {   
            if (Input.GetKey("a"))
            {
                refManager.characterController.MoveLeft();
            }
            else if (Input.GetKeyUp("a"))
            {
                refManager.characterController.StopHorizontalVelocity();
            }
            if (Input.GetKey("d"))
            {
                refManager.characterController.MoveRight();
            }
            else if (Input.GetKeyUp("d"))
            {
                refManager.characterController.StopHorizontalVelocity();
            }
            if (Input.GetKeyDown("w"))
            {
                refManager.characterController.Jump();
            }
            if (Input.GetKeyDown("e"))
            {
                refManager.characterRope.UseRope();
            }
            if (Input.GetKeyUp("e"))
            {
                refManager.characterRope.CancelRope();
            }
            if (Input.GetMouseButtonDown(1))
            {
                refManager.meleeWeapon.Shoot();
            }
            if (Input.GetMouseButton(0))
            {
                refManager.rangedWeapon.Shoot();
            }
        }
    }
}
