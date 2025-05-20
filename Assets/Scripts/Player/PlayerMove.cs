using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5;
   
    private Animator Animator;
    private void Start()
    {
        Animator = gameObject.GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (!IsOwner) return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z) * Time.deltaTime * speed;

        if (movement.magnitude > 0.0f)
        {
            Animator.SetBool("Walking", true);
            MovingServerRPC(movement);
        }
        else Animator.SetBool("Walking", false);
    }
    
    [ServerRpc]
    public void MovingServerRPC(Vector3 mv)
    {
        transform.position += mv;
        MovingClientRPC(transform.position);
    }

    [ClientRpc]
    public void MovingClientRPC(Vector3 pos)
    {
        transform.position = pos;
    }
}
