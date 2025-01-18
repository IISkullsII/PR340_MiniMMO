using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class LoginUIScript : MonoBehaviour
{

    [SerializeField] private UIDocument rootDocument;

    private TextField _playerNameInput;
    private Button _submitPlayerNameBtn; 
    

    public void Start()
    {
        _playerNameInput = rootDocument.rootVisualElement.Q<TextField>("PlayerNameInput");
        _playerNameInput.RegisterCallback<KeyDownEvent>((OnSubmitPlayerName), TrickleDown.TrickleDown);

        _submitPlayerNameBtn = rootDocument.rootVisualElement.Q<Button>("SubmitBtn");
        _submitPlayerNameBtn.clicked += OnSubmitBtnClicked;
    }

    private void OnSubmitBtnClicked()
    { 
        SubmitPlayerName();
    }

    private void OnSubmitPlayerName(KeyDownEvent evt)
    {
        if (evt.keyCode != KeyCode.Return) return;
        
        SubmitPlayerName();
        evt.StopPropagation();
    }

    private void SubmitPlayerName()
    {
        Debug.Log($"Player submitted PlayerName: {_playerNameInput.value}");
        ClientScript.Instance.SetPlayerName(_playerNameInput.value);
        ClientScript.Instance.RequestPlayer();
    }
}