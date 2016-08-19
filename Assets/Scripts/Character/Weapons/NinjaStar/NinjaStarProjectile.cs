using UnityEngine;
using System.Collections;

public class NinjaStarProjectile : MonoBehaviour {

    public float speed,angularSpeed;
    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        float myRotation = transform.rotation.eulerAngles.z;
        rb.velocity = new Vector2(speed * (float)Mathf.Cos(myRotation * Mathf.PI / 180), speed * (float)Mathf.Sin(myRotation * Mathf.PI / 180));
        rb.angularVelocity = angularSpeed;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
