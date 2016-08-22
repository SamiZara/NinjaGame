using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class PhotonSendAndReceiveManager : MonoBehaviour {

    public static PhotonSendAndReceiveManager instance;

	// Use this for initialization
	void Awake () {
        PhotonNetwork.OnEventCall += OnEvent;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEvent(byte eventCode,object content,int senderId)
    {
        switch (eventCode)
        {
            case 0:
                Debug.Log(content);
                break;
        }
    } 

    public void MessageSender(byte eventCode,object content,bool reliable,RaiseEventOptions options)
    {
        PhotonNetwork.RaiseEvent(0, (object)false, true, null);
    }
}
