using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class StartServer : MonoBehaviour
{
    [SerializeField] private bool StartImmediately = false;
    [SerializeField] private bool IsServer = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StartImmediately && NetworkManager.Singleton != null)
        {
            if (IsServer)
            {
                NetworkManager.Singleton.StartServer();
            }
            else
            {
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
