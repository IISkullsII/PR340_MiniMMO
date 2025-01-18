using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;



public class ServerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    public static ServerScript Instance { get; private set; }

    private List<PlayerData> _players;

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

        _players = new List<PlayerData>();
    }

    public void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
    }

    public void OnDisable()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
    }

    public PlayerData AddPlayer(ulong playerID, string playerName)
    {
        Vector3 playerColor = new Vector3(Random.Range(.4f, .9f), Random.Range(.4f, .9f), Random.Range(.4f, .9f));
        PlayerData playerData = new PlayerData(playerID, playerName, playerColor);
        _players.Add(playerData);
        
        GameObject playerGameObject = Instantiate(NetworkManager.Singleton.GetNetworkPrefabOverride(playerPrefab));
        NetworkObject instanceNetworkObject = playerGameObject.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
        PlayerState instancePlayerState = playerGameObject.GetComponent<PlayerState>();
        instancePlayerState.SetPlayerInfo(playerData);
        
        return playerData;
    }

    private void RemovePlayer(ulong playerID)
    {
        try
        {
            PlayerData playerData = GetPlayerData(playerID);
            _players.Remove(playerData);
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    private void SetPlayerName(ulong playerID, string playerName)
    {
        PlayerData playerData = GetPlayerData(playerID);
        playerData.PlayerName = playerName;
    }

    private PlayerData GetPlayerData(ulong playerID)
    {
        return _players.Find((data => data.PlayerID == playerID));
    }

    private void OnClientDisconnect(ulong obj)
    {
        RemovePlayer(obj);
    }
}
