using System;
using System.Collections.Generic;
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


    public static void SetLabelValue(string labelID, string labelValue)
    {
        Label labelElement = Instance.rootDocument.rootVisualElement.Q<Label>(labelID);
        if (labelElement == null) return;
        labelElement.text = labelValue;
    }


    public static void SetContainerActive(string containerID)
    {
        VisualElement containerElement = Instance.rootDocument.rootVisualElement.Q<VisualElement>(containerID);
        if (containerElement == null) return;

        List<VisualElement> containers = Instance.rootDocument.rootVisualElement.Query<VisualElement>(className: "container").ToList();
        foreach (VisualElement visualElement in containers)
        {
            visualElement.RemoveFromClassList("active");
        }
        
        containerElement.AddToClassList("active");
    }
}
