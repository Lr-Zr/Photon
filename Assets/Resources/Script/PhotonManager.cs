using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{

    private readonly string Version = "1.0";
    private string userID = "Inha";
    private string logText;
    // Start is called before the first frame update

    private void Awake()
    {
        Screen.SetResolution(800, 4080, false);
        logText = "Connection to Master";
        Debug.Log(logText);

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        logText = "Connected Master";
        Debug.Log(logText);
        PhotonNetwork.JoinRandomRoom();

    }

    //join 실패
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        logText = "Failed to join random room";
        Debug.Log(logText);
        CreateRoom();
    }




    void CreateRoom()
    {
        logText = "Creating Room";
        Debug.Log(logText);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 20
        };

        PhotonNetwork.CreateRoom("TT_Test Room", roomOptions);
    }

    //room 생성 실패
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        logText = "Failed to Create Room.... try again";
        Debug.Log(logText) ;
        CreateRoom();
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }
    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 300, 20), logText);  
    }
}



