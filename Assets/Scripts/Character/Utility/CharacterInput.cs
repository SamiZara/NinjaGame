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
            if (Input.GetKey("w"))
            {
                refManager.characterController.Jump();
            }
            if (Input.GetKey("a"))
            {
                refManager.characterController.MoveLeft();
            }
            if (Input.GetKey("d"))
            {
                refManager.characterController.MoveRight();
            }
            if (Input.GetKeyDown("e"))
            {
                refManager.characterRope.UseRope();
            }
            if (Input.GetKeyUp("e"))
            {
                refManager.characterRope.CancelRope();
            }
            if (Input.GetKeyDown("q"))
            {
                refManager.katana.Dash();
            }
            if (Input.GetMouseButtonDown(0))
            {
                refManager.ninjaStar.ThrowNinjaStar();
            }
        }
    }
}
