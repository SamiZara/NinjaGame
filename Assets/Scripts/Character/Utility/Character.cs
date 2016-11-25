using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    public CharachterReferenceManager refManager;
    public bool isPlayer;
    private Vector3 targetPos, lastTargetPos;
    private Vector2 targetSpeed;
    private float lerpTime = 0.1f;
    public float health;
    private bool isTeleported;
    public GameObject bloodEffect;

    void Start()
    {
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 60;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        SerializeState(stream, info);
    }

    void SerializeState(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {
            stream.SendNext(transform.position);
            stream.SendNext(refManager.rb.velocity);
        }
        else if (stream.isReading == true)
        {
            targetPos = (Vector3)stream.ReceiveNext();
            refManager.rb.velocity = (Vector2)stream.ReceiveNext();
            if (!isTeleported)
            {
                TeleportToTargetPos();
            }
        }
    }

    void Update()
    {
        if (!isPlayer)
        {
            if (lastTargetPos == targetPos)
            {
                lerpTime *= 2;
            }
            else
            {
                lerpTime = 0.1f;
            }
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
            lastTargetPos = targetPos;

        }
    }

    public void MeleeWeaponDamage(float damage)
    {
        GetComponent<PhotonView>().RPC(
            "RemoteMethodProjectileDamage",
            PhotonTargets.All,
            new object[] { damage
            }

        );
    }

    [PunRPC]
    public void RemoteMethodProjectileDamage(float damage, PhotonMessageInfo info)
    {
        health -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.Euler(0, 0, -30));
        if (health <= 0)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            if (isPlayer)
            {
                Invoke("Resurrect", 2);
            }
        }
    }

    public void ProjectileDamage(float damage, Vector2 pos)
    {
        GetComponent<PhotonView>().RPC(
            "RemoteMethodProjectileDamage",
            PhotonTargets.All,
            new object[] { damage,pos
            }

        );
    }

    [PunRPC]
    public void RemoteMethodProjectileDamage(float damage, Vector2 pos, PhotonMessageInfo info)
    {
        health -= damage;
        float side = transform.position.x - pos.x;
        if (side > 0)
            Instantiate(bloodEffect, transform.position, Quaternion.Euler(0, 0, 30));
        else
            Instantiate(bloodEffect, transform.position, Quaternion.Euler(0, 0, -30));
        if (health <= 0)
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Resurrect", 2);
        }
    }

    protected void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (!info.sender.isLocal)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }

    public void TeleportToTargetPos()
    {
        transform.position = targetPos;
        isTeleported = true;
    }

    private void Resurrect()
    {
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<BoxCollider2D>().enabled = true;
        transform.position = Vector3.zero;
        health = 100;
    }

}
