using System;
using Unity.Netcode;
using UnityEngine;

public class ClientScript : MonoBehaviour
{

    private PlayerData _localPlayerData;

    public static ClientScript Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        _localPlayerData = new PlayerData();
    }

    private void OnClientConnected(ulong obj)
    {
        Debug.Log("OnClientConnected: RequestPlayerRPC triggered");
        ServerClientMiddleware.Instance.RequestPlayerRPC(_localPlayerData.PlayerName);
        
        // SendRequestToSpawnToServer
        // GetInformationForSpawnBack
        //  PlayerName - IsLocalAvailable
        //  PlayerColor
        //  PlayerID - IsSynchedByCallback
    }

    public void ConnectToServer()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void SetPlayerName(string playerName)
    {
        _localPlayerData.PlayerName = playerName;
    }

    public void OnRecievePlayerData(PlayerData playerData)
    {
        _localPlayerData = playerData;
        Debug.Log($"[OnReceivePlayerData] PlayerName: {playerData.PlayerColor.ToString()}");
    }
}
