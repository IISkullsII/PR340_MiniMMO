using Unity.Netcode;
using UnityEngine;

public class NetworkUptime : NetworkBehaviour
{
    private readonly NetworkVariable<float> _serverUptimeNetworkVariable = new NetworkVariable<float>();
    private float _tLast = 0.0f;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _serverUptimeNetworkVariable.Value = 0.0f;
            Debug.Log("Server's uptime var initialized to: " + _serverUptimeNetworkVariable.Value);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            float tNow = Time.time;
            _serverUptimeNetworkVariable.Value = _serverUptimeNetworkVariable.Value + 0.1f;
            
            if (!(tNow - _tLast > 0.5f)) return;
            _tLast = tNow;
            Debug.Log("Server uptime var has been updated to: " + _serverUptimeNetworkVariable.Value);
        }

        if (!IsServer)
        {
            UIController.Instance.ServerUptimeField.text =
                _serverUptimeNetworkVariable.Value.ToString();
            // text.text = _serverUptimeNetworkVariable.Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
