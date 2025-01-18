using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class NetworkMetaData : NetworkBehaviour
{

    private readonly NetworkVariable<int> _connectedClientsCount = new NetworkVariable<int>();
    private readonly NetworkVariable<float> _serverUptimeCount = new NetworkVariable<float>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _connectedClientsCount.Value = 0;
            _serverUptimeCount.Value = 0.0f;
            
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
        else
        {
            _serverUptimeCount.OnValueChanged += OnServerUptimeCountChanged;
            _connectedClientsCount.OnValueChanged += OnConnectedClientsChanged;
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
        else
        {
            _serverUptimeCount.OnValueChanged -= OnServerUptimeCountChanged;
            _connectedClientsCount.OnValueChanged -= OnConnectedClientsChanged;
        }
    }

    // General
    public void Update()
    {
        if (IsServer)
        {
            // _serverUptimeCount.Value += 0.1f;
        }
    }
    
    
    // Client
    private void OnServerUptimeCountChanged(float previousvalue, float newvalue)
    {
        UIController.Instance.ServerUptimeField.text = newvalue.ToString(CultureInfo.CurrentCulture);
    }
    private void OnConnectedClientsChanged(int previousvalue, int newvalue)
    {
        UIController.Instance.ConnectedClientsField.text = newvalue.ToString();
    }
    
    
    // Server
    private void OnClientConnected(ulong obj)
    {
        _connectedClientsCount.Value++;
    }

    private void OnClientDisconnected(ulong obj)
    {
        _connectedClientsCount.Value--;
    }
}
