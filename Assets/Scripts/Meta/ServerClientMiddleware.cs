using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ServerClientMiddleware : NetworkBehaviour
{   
    public static ServerClientMiddleware Instance { get; private set; }

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


    // Client -> Server
    [Rpc(SendTo.Server)]
    public void RequestPlayerRPC(FixedString64Bytes playerName, RpcParams rpcParams = default)
    {
        Debug.Log("RequestPlayerRPC: Received");
        PlayerData playerData = ServerScript.Instance.AddPlayer(rpcParams.Receive.SenderClientId, playerName.ToString());
        SubmitPlayerRPC(playerData, RpcTarget.Single(rpcParams.Receive.SenderClientId, RpcTargetUse.Temp));
    }
    
    // Server -> SpecificClient
    [Rpc(SendTo.SpecifiedInParams)]
    public void SubmitPlayerRPC(PlayerData playerData, RpcParams rpcParams = default)
    {
        Debug.Log("SubmitPlayerRPC: SubmitPlayerData");
        ClientScript.Instance.OnRecievePlayerData(playerData);
    }
}