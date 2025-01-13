using System;
using Unity.Netcode;
using UnityEngine;

public class MovementNetworkController : NetworkBehaviour
{

    public NetworkVariable<Vector3> Position = new();

    private Rigidbody _rigidbody;
    

    [Rpc(SendTo.Server)]
    void SubmitPositionRequestServerRPc(Vector3 position, RpcParams rpcParams = default) => Position.Value = position;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsOwner && !IsServer)
        {
            Transform t = transform;
            
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 movement = 5 * Time.deltaTime * new Vector3(moveX, 0f, moveZ);
            t.Translate(movement, Space.World);

            if (Input.GetButton("Jump"))
            {
                _rigidbody.AddForce(Vector3.up * 10.0f);
            }

            SubmitPositionRequestServerRPc(t.position);
        }

        if (IsServer)
            transform.position = Position.Value;
    }
}
