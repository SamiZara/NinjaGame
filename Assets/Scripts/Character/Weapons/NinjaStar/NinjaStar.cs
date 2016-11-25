using UnityEngine;
using System.Collections;

public class NinjaStar : WeaponBase
{


    void Start()
    {
        //ResourceManager.Instance.AllocateAndStore("Prefabs/WeaponProjectiles/ninjastar", "ninjastar");
    }

    public override void Shoot()
    {
        if (lastUseTime + cooldown < Time.time)
        {
            lastUseTime = Time.time;
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            float degree = MathHelper.degreeBetween2Points(transform.position, v3);
            base.Shoot();
            GetComponent<PhotonView>().RPC(
            "RemoteMethodUseNinjaStar",
            PhotonTargets.All,
            new object[] { transform.position,
                           Quaternion.Euler(0, 0, degree),
                           1
            }

        );
        }
    }

    [PunRPC]
    public void RemoteMethodUseNinjaStar(Vector3 position, Quaternion rotation, int projectileId, PhotonMessageInfo info)
    {
        NinjaStarProjectile temp = ((GameObject)Instantiate(projectile, position, rotation)).GetComponent<NinjaStarProjectile>();
        temp.creationTime = info.timestamp;
        if (!info.sender.isLocal)
        {
            temp.damage = 0;
            temp.gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
        }
    }
}
