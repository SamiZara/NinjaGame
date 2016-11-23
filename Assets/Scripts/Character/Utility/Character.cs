using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{

    public CharachterReferenceManager refManager;
    public bool isPlayer;
    public int state;
    private Vector3 targetPos;

    void Start()
    {

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        SerializeState(stream, info);
    }

    void SerializeState(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting == true)
        {
            stream.SendNext(transform.position);
        }
        else if(stream.isReading == true)
        {
            targetPos = (Vector3)stream.ReceiveNext();
            
        }
    }

    void Update()
    {
        if(!isPlayer)
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
    }
}
