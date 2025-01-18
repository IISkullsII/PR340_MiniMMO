using System;
using Unity.Netcode;
using UnityEngine;

public class MovementNetworkController : NetworkBehaviour
{

    public NetworkVariable<Vector3> Position = new();

    [SerializeField] private float speed = 5.0f;
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

            Vector3 velocity = speed * Time.deltaTime * new Vector3(moveX, _rigidbody.velocity.y, moveZ);

            if (Input.GetButton("Jump"))
            {
                _rigidbody.AddForce(Vector3.up * 10.0f);
            }
            
            t.Translate(velocity, Space.World);
            SubmitPositionRequestServerRPc(t.position);
        }

        if (IsServer)
            transform.position = Position.Value;
    }
}
