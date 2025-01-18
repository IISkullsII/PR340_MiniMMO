using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerState : NetworkBehaviour
{
    [SerializeField] private TextMeshPro playerNameTMP;
    public NetworkVariable<PlayerData> PlayerInfo { get; private set; }

    public void Awake()
    {
        PlayerInfo = new NetworkVariable<PlayerData>();
        PlayerInfo.OnValueChanged += OnPlayerInfoChanged;
        
        Assert.IsNotNull(playerNameTMP);
    }

    private void OnPlayerInfoChanged(PlayerData previousvalue, PlayerData newvalue)
    {
        //
        playerNameTMP.text = newvalue.PlayerName;
    }

    public void SetPlayerInfo(PlayerData playerInfo)
    {
        PlayerInfo.Value = playerInfo;
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
