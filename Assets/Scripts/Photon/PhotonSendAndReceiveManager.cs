using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class PhotonSendAndReceiveManager : MonoBehaviour {

    public static PhotonSendAndReceiveManager instance;

	// Use this for initialization
	void Awake () {
        PhotonNetwork.OnEventCall += OnEvent;
		instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEvent(byte eventCode,object content,int senderId)
    {
		Debug.Log ("incoming event");
        switch (eventCode)
        {
		case 0:
			Struct.V3Float data = (Struct.V3Float)content;
			Debug.Log (data.f+","+data.v3);
                break;
        }
    } 

    public void MessageSender(byte eventCode,object content,bool reliable,RaiseEventOptions options)
    {
		Debug.Log ("Sending message");
		PhotonNetwork.RaiseEvent(eventCode, content, reliable, options);
    }
}
