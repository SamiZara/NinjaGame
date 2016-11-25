using UnityEngine;
using System.Collections;

public class ProjectileBase : MonoBehaviour {

    
    public float speed,lifeTime,damage;
    public double creationTime;
    public Vector3 startPosition;
    public Rigidbody2D rb;
    // Use this for initialization

    public void Start () {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        float timePassed = (float)(PhotonNetwork.time - creationTime);
        transform.position = startPosition + new Vector3(speed * Mathf.Cos((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), speed * Mathf.Sin((transform.rotation.eulerAngles.z) * Mathf.Deg2Rad), 0) * timePassed;
        float rotation = transform.eulerAngles.z;
        rb.velocity = new Vector2(speed * (float)Mathf.Cos(rotation * Mathf.PI / 180), speed * (float)Mathf.Sin(rotation * Mathf.PI / 180));
    }
	
	// Update is called once per frame
	public void Update () {
        float timePassed = (float)(PhotonNetwork.time - creationTime);
        if (timePassed > lifeTime)
        {
            Destroy(gameObject);
        }
	}

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
            Character enemy = coll.transform.GetComponent<Character>();
            if (enemy != null)
            {
                //float degree = MathHelper.degreeBetween2Points(coll.transform.position, coll.contacts[0].point);
                enemy.ProjectileDamage(damage, transform.position);
            }
            Destroy(gameObject);
    }
}
