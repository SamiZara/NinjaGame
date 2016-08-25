using UnityEngine;
using System.Collections;

public class ProjectileBase : MonoBehaviour {

    public Rigidbody2D rb;
    public float creationTime,speed,lifeTime;
    // Use this for initialization
    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(PhotonNetwork.time);
        rb.velocity = new Vector2(speed * Mathf.Cos((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad));
        float timePassed = (float)(PhotonNetwork.time - creationTime);
        transform.position += transform.forward * speed * timePassed;
    }
	
	// Update is called once per frame
	public void Update () {
        float timePassed = (float)(PhotonNetwork.time - creationTime);
        if (timePassed > lifeTime)
            Destroy(gameObject);
	}
}
