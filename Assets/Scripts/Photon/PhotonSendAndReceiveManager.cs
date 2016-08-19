using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class PhotonSendAndReceiveManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
}
