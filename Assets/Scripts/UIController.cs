using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private UIDocument rootDocument;
    public Label ServerUptimeField { get; private set; }
    public Label ConnectedClientsField { get; private set; }

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
        ServerUptimeField = rootDocument.rootVisualElement.Q<Label>("ServerUptimeVal");
        Assert.IsNotNull(ServerUptimeField);

        ConnectedClientsField = rootDocument.rootVisualElement.Q<Label>("ConnectedClientsVal");
        Assert.IsNotNull(ConnectedClientsField);
    }
}
