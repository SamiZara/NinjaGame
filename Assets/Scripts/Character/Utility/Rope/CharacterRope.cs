using UnityEngine;
using System.Collections;

public class CharacterRope : MonoBehaviour
{
    public CharachterReferenceManager refManager;
    public GameObject rope;
    private GameObject lastRope;
    public CharacterController character;

    void Start()
    {

    }

    public void UseRope()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        lastRope = (GameObject)Instantiate(rope, transform.position, Quaternion.Euler(0, 0, MathHelper.degreeBetween2Points(transform.position, v3)));
        lastRope.GetComponent<RopeProjectile>().playerDistanceJoint = GetComponent<DistanceJoint2D>();
        PhotonNetwork.RaiseEvent(0, (object)false, true, null);
    }

    public void CancelRope()
    {
        DistanceJoint2D joint = GetComponent<DistanceJoint2D>();
        joint.connectedBody = null;
        joint.enabled = false;
        Destroy(lastRope);
    }
}
