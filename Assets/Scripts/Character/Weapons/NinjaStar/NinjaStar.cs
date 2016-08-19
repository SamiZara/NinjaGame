using UnityEngine;
using System.Collections;

public class NinjaStar : MonoBehaviour
{

    public float cooldown;
    private float lastUseTime = -float.MaxValue;
    public GameObject projectile;

    void Start()
    {
        //ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/ninjastar", "ninjastar");
    }

    public void ThrowNinjaStar()
    {
        if (lastUseTime + cooldown < Time.time)
        {
            lastUseTime = Time.time;
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            float rotation = MathHelper.degreeBetween2Points(transform.position, v3);
            GameObject temp = (GameObject)Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, rotation)));
        }
    }
}
