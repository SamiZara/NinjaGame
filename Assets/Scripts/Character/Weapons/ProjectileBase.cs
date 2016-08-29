using UnityEngine;
using System.Collections;

public class ProjectileBase : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed,lifeTime;
    public double creationTime;
    public Vector3 startPosition;
    // Use this for initialization
    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        //rb.velocity = new Vector2(speed * Mathf.Cos((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad));
        //float timePassed = (float)(PhotonNetwork.time - creationTime);
        //transform.position += new Vector3(speed * Mathf.Cos((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad),0) * timePassed;
        //Debug.Log(timePassed);
    }
	
	// Update is called once per frame
	public void Update () {
        float timePassed = (float)(PhotonNetwork.time - creationTime);
        transform.position = startPosition + new Vector3(speed * Mathf.Cos((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), 0) * timePassed;
        if (timePassed > lifeTime)
            Destroy(gameObject);
	}
}
