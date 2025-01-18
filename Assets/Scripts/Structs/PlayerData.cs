using Unity.Netcode;
using UnityEngine;

public struct PlayerData : INetworkSerializable
{
    public ulong PlayerID;
    public string PlayerName;
    public Vector3 PlayerColor;

    public PlayerData(ulong playerID, string playerName, Vector3 playerColor)
    {
        PlayerID = playerID;
        PlayerName = playerName;
        PlayerColor = playerColor;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PlayerID);
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref PlayerColor);
    }
}