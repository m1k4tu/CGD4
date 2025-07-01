using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField]
    private float speed = 5; 
    [SerializeField]
    private float rotationSpeed = 5;

    private Rigidbody rb;
   
    private Animator Animator;
    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        Animator = gameObject.GetComponentInChildren<Animator>();
    }
    void FixedUpdate()
    {
        if (!IsOwner) return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z);

        if (movement.magnitude > 0.0f)
        {
            Animator.SetBool("Walking", true);
            MovingServerRPC(movement.normalized);
        }
        else Animator.SetBool("Walking", false);
    }

    [ServerRpc]
    public void MovingServerRPC(Vector3 mv)
    {
        Vector3 move = mv * speed * Time.fixedDeltaTime;
        transform.position += move;

        var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        MovingClientRPC(transform.position, transform.rotation);
    }    

    [ClientRpc]
    public void MovingClientRPC(Vector3 pos, Quaternion rot)
    {
        rb.MovePosition(pos);
        rb.MoveRotation(rot);
    }
}
