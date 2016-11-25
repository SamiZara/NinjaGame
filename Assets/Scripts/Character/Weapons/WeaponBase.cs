using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour {

    public float cooldown,lastUseTime;
    public GameObject projectile;


    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public virtual void Shoot()
    {
        lastUseTime = Time.time;
    }
}
