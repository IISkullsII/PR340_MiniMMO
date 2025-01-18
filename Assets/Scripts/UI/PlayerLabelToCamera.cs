using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerLabelToCamera : NetworkBehaviour
{
    private Transform _mainCameraTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!IsServer)
        {
            if (Camera.main != null) _mainCameraTransform = Camera.main.transform;
            Assert.IsNotNull(_mainCameraTransform);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer) return;
        
        Transform t;
        (t = transform).LookAt(_mainCameraTransform);
        transform.RotateAround(t.position, t.up, 180f);
    }
}
