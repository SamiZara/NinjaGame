using UnityEngine;
using System.Collections;

public class RopeProjectile : MonoBehaviour
{
    public float ropeSpeed;
    private Rigidbody2D rb;
    public DistanceJoint2D playerDistanceJoint;
    private Vector3 collisionPoint;
    private GameObject collidedObject;
    private bool inAir = true;
    private LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        float currentDegree = transform.rotation.eulerAngles.z;
        rb.velocity = new Vector2(ropeSpeed * Mathf.Cos((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), ropeSpeed * Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad));
    }

    void Update()
    {
        lr.SetPosition(0, CharacterController.player.transform.position);
        if (!inAir)
        {
            float degree = MathHelper.degreeBetween2Points(CharacterController.player.transform.position, transform.position);
            if(playerDistanceJoint.distance > 0.3f)
                playerDistanceJoint.distance /= 1.020f;    
        }
        else
        {
            lr.SetPosition(1, transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (inAir)
        {
            rb.isKinematic = true;
            inAir = false;
            collidedObject = coll.gameObject;
            playerDistanceJoint.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>(); 
            collisionPoint = coll.contacts[0].point;
            playerDistanceJoint.enabled = true;
            transform.parent = coll.transform;
            playerDistanceJoint.connectedAnchor = transform.localPosition;
            playerDistanceJoint.distance = Vector2.Distance(transform.position, CharacterController.player.transform.position);
            lr.SetPosition(1, transform.position);
        }
    }
}
