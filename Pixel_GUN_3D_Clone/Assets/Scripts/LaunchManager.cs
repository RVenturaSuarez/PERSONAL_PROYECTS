using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using EasyUI.Toast;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject LobbyPanel;
    
    
    
    #region Unity Methods


    void Start()
    {
        // Activamos el panel de inicio para introducir el nombre del jugador
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    #endregion

    #region Public Methods

    public void ConnecToPhotonServer()
    {
        // No conectamos a la red de photon con el usuario introducido por parámetro
        if (!PhotonNetwork.IsConnected)
        {
            if (!string.IsNullOrEmpty(PhotonNetwork.NickName))
            {
                PhotonNetwork.ConnectUsingSettings();
                ConnectionStatusPanel.SetActive(true);
                EnterGamePanel.SetActive(false);
            }
            else
            {
                Toast.Show("Introduce a username", 2f, ToastColor.Black);
            }

        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    

    #endregion
    
    #region PhotonCallbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to photon Server.");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }


    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion


    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;
        
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    

    #endregion


}
