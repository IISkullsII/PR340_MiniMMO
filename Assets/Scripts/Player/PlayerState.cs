using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerState : NetworkBehaviour
{
    [SerializeField] private TextMeshPro playerNameTMP;

    public NetworkVariable<PlayerData> PlayerInfo { get; private set; } = new NetworkVariable<PlayerData>();

    public void Awake()
    {
        Assert.IsNotNull(playerNameTMP);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        PlayerInfo.OnValueChanged += OnPlayerInfoChanged;
        playerNameTMP.text = PlayerInfo.Value.PlayerName;
    }

    private void OnPlayerInfoChanged(PlayerData previousvalue, PlayerData newvalue)
    {
        //
        playerNameTMP.text = PlayerInfo.Value.PlayerName;
    }

    public void SetPlayerInfo(PlayerData playerInfo)
    {
        PlayerInfo.Value = playerInfo;
        PlayerInfo.SetDirty(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
