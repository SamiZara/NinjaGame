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
		float degree = MathHelper.degreeBetween2Points(transform.position, v3);
		GetComponent<PhotonView> ().RPC (
			"RemoteMethodUseRope",
			PhotonTargets.All,
			new object[] { transform.position,
						   Quaternion.Euler(0, 0, degree),
						   1
			}
				
		);
    }

	[PunRPC]
	public void RemoteMethodUseRope(Vector3 position,Quaternion rotation,int projectileId,PhotonMessageInfo info)
	{
        if(lastRope != null)
        {
            CancelRope();
        }
		lastRope = (GameObject)Instantiate(rope, position, rotation);
        RopeProjectile proj = lastRope.GetComponent<RopeProjectile>();
        proj.playerDistanceJoint = GetComponent<DistanceJoint2D>();
        proj.owner = transform;
        proj.creationTime = info.timestamp;
        proj.ownerRefManager = refManager;
	}

    public void CancelRope()
    {
        GetComponent<PhotonView>().RPC(
            "RemoteMethodCancelRope",
            PhotonTargets.All,
            new object[] { 
            }

        );
    }

    [PunRPC]
    public void RemoteMethodCancelRope()
    {
        DistanceJoint2D joint = GetComponent<DistanceJoint2D>();
        joint.connectedBody = null;
        joint.enabled = false;
        Destroy(lastRope);
    }

	/*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			Debug.Log ("Is Writing");
			var v3 = Input.mousePosition;
			v3.z = 10.0f;
			v3 = Camera.main.ScreenToWorldPoint(v3);
			float degree = MathHelper.degreeBetween2Points(transform.position, v3);
			stream.SendNext (transform.position);
			stream.SendNext (degree);
		} else {
			Debug.Log ("Is Reading");
			Vector3 position = (Vector3)stream.ReceiveNext ();
			float rotation = (float)stream.ReceiveNext ();
		}
	}*/
}
