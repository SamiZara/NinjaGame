using UnityEngine;
using System.Collections;

public class RopeProjectile : ProjectileBase
{
    public DistanceJoint2D playerDistanceJoint;
    private bool inAir = true;
    private LineRenderer lr;
    public Transform owner;

    new void Start()
    {
        base.Start();
        lr = GetComponent<LineRenderer>();
    }

    new void Update()
    { 
        lr.SetPosition(0, owner.position);
        if (!inAir)
        {
            float degree = MathHelper.degreeBetween2Points(CharacterController.player.transform.position, transform.position);
            if(playerDistanceJoint.distance > 0.3f)
                playerDistanceJoint.distance /= 1.020f;    
        }
        else
        {
            base.Update();
            lr.SetPosition(1, transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (inAir)
        {
            rb.isKinematic = true;
            inAir = false;
            playerDistanceJoint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>(); 
            playerDistanceJoint.enabled = true;
            transform.parent = coll.transform;
            playerDistanceJoint.connectedAnchor = transform.localPosition;
            playerDistanceJoint.distance = Vector2.Distance(transform.position, CharacterController.player.transform.position);
            lr.SetPosition(1, transform.position);
        }
    }
}
