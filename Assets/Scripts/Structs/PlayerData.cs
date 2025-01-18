using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct PlayerData : INetworkSerializable
{
    public ulong PlayerID;
    private FixedString64Bytes _playerName;

    public string PlayerName
    {
        get => _playerName.ToString();
        set => _playerName = new FixedString64Bytes(value);
    }
    public Vector3 PlayerColor;

    public PlayerData(ulong playerID, string playerName, Vector3 playerColor)
    {
        PlayerID = playerID;
        _playerName = new FixedString64Bytes(playerName);
        PlayerColor = playerColor;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PlayerID);
        serializer.SerializeValue(ref _playerName);
        serializer.SerializeValue(ref PlayerColor);
    }
}