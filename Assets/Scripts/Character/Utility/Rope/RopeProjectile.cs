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
            if (playerDistanceJoint.distance > 0.3f)
                playerDistanceJoint.distance -= 2f * Time.deltaTime;
            else
            {
                playerDistanceJoint.distance = 0.3f;
            }
        }
        else
        {
            base.Update();
            lr.SetPosition(1, transform.position);
        }
    }

    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (inAir)
        {
            lr.SetPosition(1, transform.position);
            inAir = false;
            rb.isKinematic = true;

            playerDistanceJoint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
            playerDistanceJoint.enabled = true;
            transform.parent = coll.transform;
            playerDistanceJoint.connectedAnchor = transform.localPosition;
            playerDistanceJoint.distance = Vector2.Distance(transform.position, owner.position);
            Debug.Log(PhotonNetwork.time);
        }
    }
}
