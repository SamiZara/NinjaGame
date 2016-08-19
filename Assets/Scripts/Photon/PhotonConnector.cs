using UnityEngine;
using System.Collections;


public class PhotonConnector : Photon.PunBehaviour
{

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void OnJoinedLobby()
    {

        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("Test", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        CharacterController temp = PhotonNetwork.Instantiate("Prefabs/Player/Player", Vector3.zero, Quaternion.identity, 0).GetComponent<CharacterController>();
        temp.refManager.character.isPlayer = true;
        CharacterController.player = temp;
    }
}
