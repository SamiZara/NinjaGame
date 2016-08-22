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
		//lastRope = (GameObject)Instantiate(rope, transform.position, Quaternion.Euler(0, 0, degree));
        //lastRope.GetComponent<RopeProjectile>().playerDistanceJoint = GetComponent<DistanceJoint2D>();
		Struct.V3Float content = new Struct.V3Float ();
		content.v3 = transform.position;
		content.f = degree;
		GetComponent<PhotonView> ().RPC (
			"RemoteMethodUseRope",
			PhotonTargets.All,
			new object[] { transform.position,
						   Quaternion.Euler(0, 0, degree),
						   1
			}
				
		);
		//PhotonSendAndReceiveManager.instance.MessageSender (0,content,true,null);
    }

	[PunRPC]
	public void RemoteMethodUseRope(Vector3 position,Quaternion rotation,int projectileId,PhotonMessageInfo info)
	{
		lastRope = (GameObject)Instantiate(rope, position, rotation);
		lastRope.GetComponent<RopeProjectile>().playerDistanceJoint = GetComponent<DistanceJoint2D>();
		Debug.Log (refManager.character.isPlayer+",");
	}

    public void CancelRope()
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
